using System;
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
            //input导入各部位加速数值,[点数, index, 文字描述, 选择状态]
            string[,] equipValueInput =
            {
                { armor3output.Text,"1",$"帽子（{armor3output.Text}点）",checkBoxArmor3.Checked.ToString() },
                { armor2output.Text,"2",$"衣服（{armor2output.Text}点）",checkBoxArmor2.Checked.ToString() },
                { armor6output.Text,"3",$"腰带（{armor6output.Text}点）",checkBoxArmor6.Checked.ToString() },
                { armor10output.Text,"4",$"护腕（{armor10output.Text}点）",checkBoxArmor10.Checked.ToString() },
                { armor8output.Text,"5",$"下装（{armor8output.Text}点）",checkBoxArmor8.Checked.ToString() },
                { armor9output.Text,"6",$"鞋子（{armor9output.Text}点）",checkBoxArmor9.Checked.ToString() },
                { trinket4output.Text,"7",$"项链（{trinket4output.Text}点）",checkBoxTrinket4.Checked.ToString() },
                { trinket7output.Text,"8",$"腰坠（{trinket7output.Text}点）",checkBoxTrinket7.Checked.ToString() },
                { trinket51output.Text,"9",$"戒指1（{trinket51output.Text}点）",checkBoxTrinket51.Checked.ToString() },
                { trinket52output.Text,"10",$"戒指2（{trinket52output.Text}点）",checkBoxTrinket52.Checked.ToString() },
                { weapon1output.Text,"11",$"暗器（{weapon1output.Text}点）",checkBoxWeapon1.Checked.ToString() },
                { weapon0output.Text,"12",$"武器（{weapon0output.Text}点）",checkBoxWeapon0.Checked.ToString() },
                { enchantTextbox1.Text,"13",$"附魔1({enchantTextbox1.Text}点)",enchant1.Checked.ToString() },
                { enchantTextbox2.Text,"14",$"附魔2（{enchantTextbox2.Text}点）",enchant2.Checked.ToString() },
                { enchantTextbox3.Text,"15",$"附魔3（{enchantTextbox3.Text}点）",enchant3.Checked.ToString() },
                { medicine.Text,"16",$"增强药品（{medicine.Text}点）",checkboxMedicine.Checked.ToString() },
                { food.Text,"17",$"增强食品（{food.Text}点）",checkBoxFood.Checked.ToString() },
                { wine.Text,"18",$"家园藏酒（{wine.Text}点）",checkBoxWine.Checked.ToString() }
            };
            //判断数据类型，排除不需要计算的部位，并用output返回装备序号  -- 二维数组为什么要用花括号救命
            for (int arr = 0; arr < equipValueInput.GetLength(0); arr++)
            {
                string valueTrim = equipValueInput[arr, 0];
                if (equipValueInput[arr, 3] == "True" && !string.IsNullOrEmpty(valueTrim) && decimal.TryParse(valueTrim, out decimal parseResult) && parseResult != 0M)
                {
                    equipValueOutput.Add(equipValueInput[arr, 1]);
                    //输出需要规划求解（穷举）的部位的index
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
            //MessageBox.Show(result.Count.ToString());
            //重建数组,排序    排查这里bug
            List<decimal> newpass = new List<decimal>();
            List<decimal> newresult = new List<decimal>();
            var sortedPairs = result.Zip(pass, (r, p) => new { Result = r, Pass = p })
                    .OrderBy(pair => pair.Pass)
                    .ToList();

            newresult = sortedPairs.Select(pair => pair.Result).ToList();
            newpass = sortedPairs.Select(pair => pair.Pass).ToList();

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
                ui.Add($"溢出{newpass.ElementAt(k)}点，所用部位： {string.Join("，", m)}");

            }
            //输出最终需求
            string o = string.Join(Environment.NewLine, ui);
            if (result.Count > 0)
                //MessageBox.Show(o);
                new Form2(o,result.Count).ShowDialog();

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
            globalTextChanged(armor3, checkBoxArmor3, armor3output, comboarmor3, 0.9m);
        }
        private void armor2_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(armor2, checkBoxArmor2, armor2output, comboarmor2, 1m);
        }
        private void aromr6_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(armor6, checkBoxArmor6, armor6output, comboarmor6, 0.7m);
        }
        private void armor10_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(armor10, checkBoxArmor10, armor10output, comboarmor10, 0.7m);
        }
        private void armor8_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(armor8, checkBoxArmor8, armor8output, comboarmor8, 1m);
        }
        private void armor9_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(armor9, checkBoxArmor9, armor9output, comboarmor9, 0.7m);
        }
        private void trinket4_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(trinket4, checkBoxTrinket4, trinket4output, combotrinket7, 0.5m);
        }
        private void trinket7_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(trinket7, checkBoxTrinket7, trinket7output, combotrinket7, 0.5m);
        }
        private void trinket51_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(trinket51, checkBoxTrinket51, trinket51output, combotrinket51, 0.5m);
        }
        private void trinket52_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(trinket52, checkBoxTrinket52, trinket52output, combotrinket52, 0.5m);
        }
        private void weapon1_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(weapon1, checkBoxWeapon1, weapon1output, comboweapon1, 0.6m);
        }
        private void weapon0_TextChanged(object sender, EventArgs e)
        {
            globalTextChanged(weapon0, checkBoxWeapon0, weapon0output, comboweapon0, 1.2m);
        }
        private string globalTextChanged(TextBox level, CheckBox checkbox, TextBox outputText, ComboBox combo, decimal y)
        {
            decimal x;
            try
            {
                if (level.Text == "" || !checkbox.Checked)
                    x = 0;
                else
                {
                    x = (int)(decimal.Parse(level.Text) * 0.35036m * y + 0.5m);
                    //品质等级乘品质等级转加速系数乘部位系数
                    x = x + (int)(x * StrengthMapping[combo.Text] + 0.5m);
                }
            }
            catch
            {
                x = 0;
            }
            outputText.Text = x.ToString();
            return outputText.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = {
            armor3, armor2, armor6, armor10, armor8, armor9,
            trinket4, trinket7, trinket51, trinket52, weapon1, weapon0
            };
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = alltext.Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ComboBox[] comboBoxes = {
            comboarmor3, comboarmor2, comboarmor6, comboarmor10,
            comboarmor8, comboarmor9, combotrinket4, combotrinket7,
            combotrinket51, combotrinket52, comboweapon1, comboweapon0
            };
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.SelectedItem = allcombo.SelectedItem;
            }
        }
    }
}
