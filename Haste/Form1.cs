using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Haste
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, double> StrengthMapping = new Dictionary<string, double>()
        {
            {"",0 },
            {"无", 0 },
            {"1级",0.005 },
            {"2级",0.013 },
            {"3级",0.024 },
            {"4级",0.038 },
            {"5级",0.055 },
            {"6级",0.075 },
            {"7级",0.098 },
            {"8级",0.124 }
        };

        public Form1()
        {
            InitializeComponent();
            allcombo.SelectedIndex = 0;
            //初始化下拉框
        }
        public void gethaste(Object sender, EventArgs e)
        {
            List<decimal> equipValueOutput = new List<decimal>();
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
            foreach (string value in equipValueInput)
            {
                string valueTrim = value.Trim();
                if (!string.IsNullOrEmpty(valueTrim) && decimal.TryParse(valueTrim, out decimal parseResult) && parseResult != 0M)
                {
                    equipValueOutput.Add(parseResult);
                }
            }
            //equipValueOutput.ToArray
            //测一下推送

            MessageBox.Show(string.Join(",", equipValueOutput));
        }
        private void returnhaste(object sender, EventArgs e)
        {

        }
        private void number(object sender, KeyPressEventArgs e)
        //限制输入数字和退格，属性里限制输入法，IME为disable
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        //帽子0.9 上衣裤子1 腰带护腕鞋子0.7 首饰0.5 暗器0.6
        private void armor3_TextChanged(object sender, EventArgs e)
        //checkbox,click;textbox,textchanged；combo,textchanged
        {
            float x;
            if (armor3.Text == "" || !checkBoxArmor3.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(armor3.Text) * 0.35036 * 0.9 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboarmor3.Text] + 0.5);
            }
            armor3output.Text = x.ToString();
        }
        private void armor2_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (armor2.Text == "" || !checkBoxArmor2.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(armor2.Text) * 0.35036 * 1 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboarmor2.Text] + 0.5);
            }
            armor2output.Text = x.ToString();
        }
        private void aromr6_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (armor6.Text == "" || !checkBoxArmor6.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(armor6.Text) * 0.35036 * 0.7 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboarmor6.Text] + 0.5);
            }
            armor6output.Text = x.ToString();
        }
        private void armor10_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (armor10.Text == "" || !checkBoxArmor10.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(armor10.Text) * 0.35036 * 0.7 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboarmor10.Text] + 0.5);
            }
            armor10output.Text = x.ToString();
        }
        private void armor8_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (armor8.Text == "" || !checkBoxArmor8.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(armor8.Text) * 0.35036 * 1 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboarmor8.Text] + 0.5);
            }
            armor8output.Text = x.ToString();
        }
        private void armor9_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (armor9.Text == "" || !checkBoxArmor9.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(armor9.Text) * 0.35036 * 0.7 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboarmor9.Text] + 0.5);
            }
            armor9output.Text = x.ToString();
        }
        private void trinket4_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (trinket4.Text == "" || !checkBoxTrinket4.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(trinket4.Text) * 0.35036 * 0.5 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[combotrinket4.Text] + 0.5);
            }
            trinket4output.Text = x.ToString();
        }
        private void trinket7_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (trinket7.Text == "" || !checkBoxTrinket7.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(trinket7.Text) * 0.35036 * 0.5 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[combotrinket7.Text] + 0.5);
            }
            trinket7output.Text = x.ToString();
        }
        private void trinket51_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (trinket51.Text == "" || !checkBoxTrinket51.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(trinket51.Text) * 0.35036 * 0.5 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[combotrinket51.Text] + 0.5);
            }
            trinket51output.Text = x.ToString();
        }
        private void trinket52_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (trinket52.Text == "" || !checkBoxTrinket52.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(trinket52.Text) * 0.35036 * 0.5 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[combotrinket52.Text] + 0.5);
            }
            trinket52output.Text = x.ToString();
        }
        private void weapon1_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (weapon1.Text == "" || !checkBoxWeapon1.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(weapon1.Text) * 0.35036 * 0.6 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboweapon1.Text] + 0.5);
            }
            weapon1output.Text = x.ToString();
        }
        private void weapon0_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (weapon0.Text == "" || !checkBoxWeapon0.Checked)
                x = 0;
            else
            {
                x = (int)(float.Parse(weapon0.Text) * 0.35036 * 1.2 + 0.5);
                //品质等级乘品质等级转加速系数乘部位系数
                x = x + (int)(x * StrengthMapping[comboweapon0.Text] + 0.5);
            }
            weapon0output.Text = x.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (alltext.Text == "")
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

        private void armor3output_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
