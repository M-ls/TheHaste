using System.Windows.Forms;

namespace Haste
{
    public partial class Form2 : Form
    {
        public Form2(string content,int count)
        {
            InitializeComponent();
            this.Text = $"计算结果： {count} 个";
            txtOutput.Text = content;

        }
    }
}
