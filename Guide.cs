using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HG
{
    public partial class Guide : Form
    {
        public Guide()
        {
            InitializeComponent();
        }

        private void Guide_Load(object sender, EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }
    }
}
