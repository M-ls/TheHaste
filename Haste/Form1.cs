using System;
using System.Collections.Generic;
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
            List<decimal> equipValueOutput = new List<decimal>();
            //input导入各部位加速数值
            string[] equipValueInput =
            {
                armor3output.Text,
                armor2output.Text,
                armor6output.Text,
                armor10output.Text,
                armor8output.Text,
                armor9output.Text,
                trinket4output.Text,
                trinket7output.Text,
                trinket51output.Text,
                trinket52output.Text,
                weapon1output.Text,
                weapon0output.Text
            };
            //判断数据类型
            foreach (string value in equipValueInput)
            {
                string valueTrim = value.Trim();
                if (!string.IsNullOrEmpty(valueTrim) && decimal.TryParse(valueTrim, out decimal parseResult) && parseResult != 0M)
                {
                    equipValueOutput.Add(parseResult);
                    //输出并显示需要规划求解（穷举）的部位
                }
            }
            //equipValueOutput.ToArray ←不知道干啥的 先不删


            MessageBox.Show(string.Join(",", equipValueOutput));
        }
        private void returnhaste(object sender, EventArgs e)
        {

        }
        private void number(object sender, KeyPressEventArgs e)
        //限制输入数字和退格，属性里限制输入法，IME为disable
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
            //int sz = alltext.TextLength;
            //int k = 0;
            //for (int i = 0; i < sz; i++)
            //{
            //    if (char.IsNumber((char)i))
            //    {
            //        k++;
            //        MessageBox.Show(k.ToString());
            //    }
            //    else
            //        continue;
            //}
            //判断是否为数字 写错了 暂时注释掉
            if (alltext.Text == "")
            //一键设置装分，判断数据类型和是否为空
            {
                armor3.Text = "0";
                armor2.Text = "0";
                armor6.Text = "0";
                armor10.Text = "0";
                armor8.Text = "0";
                armor9.Text = "0";
                trinket4.Text = "0";
                trinket7.Text = "0";
                trinket51.Text = "0";
                trinket52.Text = "0";
                weapon1.Text = "0";
                weapon0.Text = "0";
            }
            else
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
