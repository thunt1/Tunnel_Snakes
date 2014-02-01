using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
        }

        private void cbSavePRT_CheckedChanged(object sender, EventArgs e)
        {
            btnOptionsApply.Enabled = true;
        }

        private void txtMaxLines_TextChanged(object sender, EventArgs e)
        {
            btnOptionsApply.Enabled = true;
        }

        private void txtMaxInstructions_TextChanged(object sender, EventArgs e)
        {
            btnOptionsApply.Enabled = true;
        }

        private void txtMaxPages_TextChanged(object sender, EventArgs e)
        {
            btnOptionsApply.Enabled = true;
        }

        private void txtMaxSize_TextChanged(object sender, EventArgs e)
        {
            btnOptionsApply.Enabled = true;
        }

        private void btnOptionsApply_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
