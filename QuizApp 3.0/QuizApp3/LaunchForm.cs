using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp3
{
    public partial class LaunchForm : Form
    {
        QuizEntryForm frm1;
        public LaunchForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm1 = new QuizEntryForm();
            frm1.ShowDialog(this);

        }
    }
}
