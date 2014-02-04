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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            frmAbout ab = new frmAbout();
            ab.ShowDialog();
        }

        private void menuToolsOptions_Click(object sender, EventArgs e)
        {
            frmOptions opt = new frmOptions();
            opt.ShowDialog();
        }

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've made a new project!", "Create a New Project", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsToolbarNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've made a new project!", "Create a New Project", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            dlgOpen.ShowDialog();
        }

        private void tsToolbarOpen_Click(object sender, EventArgs e)
        {
            dlgOpen.ShowDialog();
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            dlgSave.ShowDialog();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            dlgSave.ShowDialog();
        }

        private void tsToolbarSave_ButtonClick(object sender, EventArgs e)
        {
            dlgSave.ShowDialog();
        }

        private void tsToolbarSaveSave_Click(object sender, EventArgs e)
        {
            dlgSave.ShowDialog();
        }

        private void tsToolbarSaveSaveAs_Click(object sender, EventArgs e)
        {
            dlgSave.ShowDialog();
        }

        private void menuFileImport_Click(object sender, EventArgs e)
        {
            dlgImport.ShowDialog();
        }

        private void menuFileExport_Click(object sender, EventArgs e)
        {
            dlgExport.ShowDialog();
        }

        private void menuAssembleAssemble_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've assembled your code!", "Assembled Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuAssembleAssembleDebug_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've assembled your code and are debugging it!", "Assembled Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuAssembleAssembleFinalRun_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've assembled your code and are doing a final run!", "Assembled Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsToolbarAssemble_ButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've assembled your code!", "Assembled Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsToolbarAssembleAssembleDebug_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've assembled your code and are debugging it!", "Assembled Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsToolbarAssembleAssembleFinalRun_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You've assembled your code and are doing a final run!", "Assembled Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsToolbarViewPRT_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You are now viewing your .PRT file!", "Viewing .PRT File", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuToolsViewPRT_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You are now viewing your .PRT file!", "Viewing .PRT File", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuHelpOnlineHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.una.edu");
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {

        }

        
      
    }
}
