using System.Windows.Forms;

namespace Haste
{
    public partial class Form2 : Form
    {
        public Form2(string content)
        {
            InitializeComponent();
            txtOutput.Text = content;

        }
    }
}
