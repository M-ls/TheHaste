﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Haste
{
    public partial class Form1 : Form
    {
        //精炼等级及系数
        private readonly Dictionary<string, decimal> StrengthMapping = new Dictionary<string, decimal>()
        {
            {"",0M },
            {"无", 0M },
            {"1级",0.005M },
            {"2级",0.013M },
            {"3级",0.024M },
            {"4级",0.038M },
            {"5级",0.055M },
            {"6级",0.075M },
            {"7级",0.098M },
            {"8级",0.124M }
        };

        public Form1()
        {
            InitializeComponent();
            allcombo.SelectedIndex = 0;
            //初始化下拉框
        }
        public void gethaste(Object sender, EventArgs e)
        {
            //创建所需计算的list，预先排除非勾选项
            List<string> equipValueOutput = new List<string>();
            //input导入各部位加速数值
            string[,] equipValueInput =
            {
                { armor3output.Text,"1","帽子",checkBoxArmor3.Checked.ToString() },
                { armor2output.Text,"2","衣服",checkBoxArmor2.Checked.ToString() },
                { armor6output.Text,"3","腰带",checkBoxArmor6.Checked.ToString() },
                { armor10output.Text,"4","护腕",checkBoxArmor10.Checked.ToString() },
                { armor8output.Text,"5","下装",checkBoxArmor8.Checked.ToString() },
                { armor9output.Text,"6","鞋子",checkBoxArmor9.Checked.ToString() },
                { trinket4output.Text,"7","项链",checkBoxTrinket4.Checked.ToString() },
                { trinket7output.Text,"8","腰坠",checkBoxTrinket7.Checked.ToString() },
                { trinket51output.Text,"9","戒指1",checkBoxTrinket51.Checked.ToString() },
                { trinket52output.Text,"10","戒指2",checkBoxTrinket52.Checked.ToString() },
                { weapon1output.Text,"11","暗器",checkBoxWeapon1.Checked.ToString() },
                { weapon0output.Text,"12","武器",checkBoxWeapon0.Checked.ToString() },
                { enchantTextbox1.Text,"13","附魔1",enchant1.Checked.ToString() },
                { enchantTextbox2.Text,"14","附魔2",enchant2.Checked.ToString() },
                { enchantTextbox3.Text,"15","附魔3",enchant3.Checked.ToString() },
                { medicine.Text,"16","增强药品",checkboxMedicine.Checked.ToString() },
                { food.Text,"17","增强食品",checkBoxFood.Checked.ToString() },
                { wine.Text,"18","家园藏酒",checkBoxWine.Checked.ToString() }
            };
            //判断数据类型，排除不需要计算的部位，并用output返回装备序号
            for (int arr = 0; arr < equipValueInput.GetLength(0); arr++)
            {
                string valueTrim = equipValueInput[arr, 0];
                if (equipValueInput[arr, 3] == "True" && !string.IsNullOrEmpty(valueTrim) && decimal.TryParse(valueTrim, out decimal parseResult) && parseResult != 0M)
                {
                    equipValueOutput.Add(equipValueInput[arr, 1]);
                    //输出需要规划求解（穷举）的部位
                }
            }
            //穷举计算 根据equipValueOutput这个list来计算 这里组合不是实际值是唯一标识 list为组合的list 在最后需要将list转换为原始部位
            List<decimal[]> list = new List<decimal[]>();
            foreach (string s in equipValueOutput)
            {
                List<decimal[]> lst = list.GetRange(0, list.Count);
                decimal[] nArr = { decimal.Parse(s) };
                list.Add(nArr);
                foreach (decimal[] ss in lst)
                {
                    list.Add(ss.Concat(nArr).ToArray());
                }
            }
            //输出满足加速范围的list下标及溢出值，一一对应
            List<decimal> result = new List<decimal>(); //list的下标
            List<decimal> pass = new List<decimal>(); //溢出值
            for (int j = 0; j < list.Count; j++)
            {
                //列出list的下标循环
                decimal[] down = list.ElementAt(j);
                List<decimal> sum = new List<decimal>();
                //把装备部位标记转化为对应部位数值并求和
                foreach (decimal s in down)
                {
                    sum.Add(decimal.Parse(equipValueInput[(int)(s - 1m), 0]));
                }
                //sum为所需求和的各个实际数值
                decimal sums = sum.Sum();
                try
                {
                    if (sums >= decimal.Parse(tohaste.Text) && sums <= (decimal.Parse(tohaste.Text) + decimal.Parse(addhaste.Text)))
                    {
                        result.Add(j);
                        pass.Add(sums - decimal.Parse(tohaste.Text));
                    }
                }
                catch
                {

                }
            }
            //重建数组,排序
            List<decimal> newpass = new List<decimal>();
            List<decimal> newresult = new List<decimal>();
            for(int j = 0; j < pass.Count; j++)
            {
                int i = pass.IndexOf(pass.Min());
                newpass.Add(pass.ElementAt(i));
                pass.RemoveAt(i);
                newresult.Add(result.ElementAt(i));
                result.RemoveAt(i);
            }
            //将list的下标转换为部件名字
            List<string> ui = new List<string>();
            for (int k = 0; k < newresult.Count; k++)
            {
                //result下标转result值，即list下标 list下标转部件名字 顺手把溢出值捎上
                decimal[] downs = list.ElementAt((int)newresult.ElementAt(k));
                List<string> m = new List<string>();
                foreach (decimal s in downs)
                {
                    m.Add(equipValueInput[(int)(s - 1), 2]);
                }
                ui.Add($"溢出{ newpass.ElementAt(k)}点，所用部位： { string.Join("，", m)}");

            }
            //输出最终需求
            string o = string.Join(Environment.NewLine, ui);
            if (result.Count > 0)
                //MessageBox.Show(o);
                new Form2(o).ShowDialog();

            else
                MessageBox.Show("无结果！");
        }
        private void number(object sender, KeyPressEventArgs e)
        //限制输入数字和退格，属性里限制输入法，IME为disable,限制右键菜单ShortcutEnabled
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        //帽子0.9 上衣裤子1 腰带护腕鞋子0.7 首饰0.5 暗器0.6
        //checkbox,click;textbox,textchanged；combo,textchanged
        private void armor3_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (armor3.Text == "" || !checkBoxArmor3.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(armor3.Text) * 0.35036M * 0.9M + 0.5M);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboarmor3.Text] + 0.5M);
                }
            }
            catch
            {
                x = 0;
            }
            armor3output.Text = x.ToString();
        }
        private void armor2_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (armor2.Text == "" || !checkBoxArmor2.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(armor2.Text) * 0.35036M * 1M + 0.5M);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboarmor2.Text] + 0.5M);
                }
            }
            catch
            {
                x = 0;
            }
            armor2output.Text = x.ToString();
        }
        private void aromr6_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (armor6.Text == "" || !checkBoxArmor6.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(armor6.Text) * 0.35036M * 0.7M + 0.5M);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboarmor6.Text] + 0.5M);
                }
            }
            catch
            {
                x = 0;
            }
            armor6output.Text = x.ToString();
        }
        private void armor10_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (armor10.Text == "" || !checkBoxArmor10.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(armor10.Text) * 0.35036m * 0.7m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboarmor10.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            armor10output.Text = x.ToString();
        }
        private void armor8_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (armor8.Text == "" || !checkBoxArmor8.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(armor8.Text) * 0.35036m * 1m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboarmor8.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            armor8output.Text = x.ToString();
        }
        private void armor9_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (armor9.Text == "" || !checkBoxArmor9.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(armor9.Text) * 0.35036m * 0.7m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboarmor9.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            armor9output.Text = x.ToString();
        }
        private void trinket4_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (trinket4.Text == "" || !checkBoxTrinket4.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(trinket4.Text) * 0.35036m * 0.5m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[combotrinket4.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            trinket4output.Text = x.ToString();
        }
        private void trinket7_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (trinket7.Text == "" || !checkBoxTrinket7.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(trinket7.Text) * 0.35036m * 0.5m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[combotrinket7.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            trinket7output.Text = x.ToString();
        }
        private void trinket51_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (trinket51.Text == "" || !checkBoxTrinket51.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(trinket51.Text) * 0.35036m * 0.5m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[combotrinket51.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            trinket51output.Text = x.ToString();
        }
        private void trinket52_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (trinket52.Text == "" || !checkBoxTrinket52.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(trinket52.Text) * 0.35036m * 0.5m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[combotrinket52.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            trinket52output.Text = x.ToString();
        }
        private void weapon1_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (weapon1.Text == "" || !checkBoxWeapon1.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(weapon1.Text) * 0.35036m * 0.6m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboweapon1.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            weapon1output.Text = x.ToString();
        }
        private void weapon0_TextChanged(object sender, EventArgs e)
        {
            decimal x;
            try
            {
                if (weapon0.Text == "" || !checkBoxWeapon0.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(weapon0.Text) * 0.35036m * 1.2m + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[comboweapon0.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            weapon0output.Text = x.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            armor3.Text = alltext.Text;
            armor2.Text = alltext.Text;
            armor6.Text = alltext.Text;
            armor10.Text = alltext.Text;
            armor8.Text = alltext.Text;
            armor9.Text = alltext.Text;
            trinket4.Text = alltext.Text;
            trinket7.Text = alltext.Text;
            trinket51.Text = alltext.Text;
            trinket52.Text = alltext.Text;
            weapon1.Text = alltext.Text;
            weapon0.Text = alltext.Text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            comboarmor3.SelectedItem = allcombo.SelectedItem;
            comboarmor2.SelectedItem = allcombo.SelectedItem;
            comboarmor6.SelectedItem = allcombo.SelectedItem;
            comboarmor10.SelectedItem = allcombo.SelectedItem;
            comboarmor8.SelectedItem = allcombo.SelectedItem;
            comboarmor9.SelectedItem = allcombo.SelectedItem;
            combotrinket4.SelectedItem = allcombo.SelectedItem;
            combotrinket7.SelectedItem = allcombo.SelectedItem;
            combotrinket51.SelectedItem = allcombo.SelectedItem;
            combotrinket52.SelectedItem = allcombo.SelectedItem;
            comboweapon1.SelectedItem = allcombo.SelectedItem;
            comboweapon0.SelectedItem = allcombo.SelectedItem;
        }
    }
}
