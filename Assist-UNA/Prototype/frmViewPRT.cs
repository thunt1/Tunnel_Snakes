using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Prototype
{
    public partial class frmViewPRT : Form
    {
        public frmViewPRT()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            //dlgPrint.ShowDialog();
            if (dlgPrint.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Your .PRT is now printing!!");
            }
        }
    }
}
