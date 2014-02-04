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

        const int MAINFORM_WIDTH = 605;
        const int MAINFORM_MAX_WIDTH = 925;

        const int MAINFORM_HEIGHT = 480;
        const int MAINFORM_MAX_HEIGHT = 650;

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

            int lineStart = 0;
            int lineLength = 0;
            char firstChar;
            int lastCursorPos = txtSource.SelectionStart;

            // Loop through all the lines
            for (int i = 0;  i < txtSource.Lines.Count(); i++)
            {

                lineStart = txtSource.GetFirstCharIndexFromLine(i);
                lineLength = txtSource.Lines[i].Length;
                
                if (lineLength > 0) {

                    firstChar = txtSource.Lines[i][0];

                    // Set color of comment lines
                    if (firstChar == '*')
                    {

                        // Entire line
                        txtSource.Select(lineStart, lineLength); 
                        txtSource.SelectionColor = Color.Green;
                        
                    }

                }

                txtSource.DeselectAll();

            }

            txtSource.Select(lastCursorPos, lastCursorPos);
            txtSource.DeselectAll();

        }

        private void menuToolsRegisters_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolsRegisters.Checked == true)
            {

                this.Size = new Size(MAINFORM_MAX_WIDTH, this.Height);
                this.Location = new Point(this.Location.X - ((MAINFORM_MAX_WIDTH - MAINFORM_WIDTH) / 2), this.Location.Y);
                gbRegisters.Visible = true;

            }

            else
            {

                this.Size = new Size(MAINFORM_WIDTH, this.Height);
                this.Location = new Point(this.Location.X + ((MAINFORM_MAX_WIDTH - MAINFORM_WIDTH) / 2), this.Location.Y);
                gbRegisters.Visible = false;

            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            lvMemory.Items.Add("00000000", 0);
            lvMemory.Items[0].SubItems.Add("F810F010F82F00141A12501F001807FE");
            lvMemory.Items[0].SubItems.Add("..0.......&.....");

            lvMemory.Items.Add("00000010", 1);
            lvMemory.Items[1].SubItems.Add("0000000400000006F5F5F5F5F5F5F5F5");
            lvMemory.Items[1].SubItems.Add("........55555555");

            lvMemory.Items.Add("00000020", 2);
            lvMemory.Items[2].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[2].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000030", 3);
            lvMemory.Items[3].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[3].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000040", 4);
            lvMemory.Items[4].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[4].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000050", 5);
            lvMemory.Items[5].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[5].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000060", 6);
            lvMemory.Items[6].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[6].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000070", 7);
            lvMemory.Items[7].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[7].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000080", 8);
            lvMemory.Items[8].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[8].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000090", 9);
            lvMemory.Items[9].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[9].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("000000A0", 10);
            lvMemory.Items[10].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[10].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("000000B0", 11);
            lvMemory.Items[11].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[11].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("000000C0", 12);
            lvMemory.Items[12].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[12].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("000000D0", 13);
            lvMemory.Items[13].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[13].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("000000E0", 14);
            lvMemory.Items[14].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[14].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("000000F0", 15);
            lvMemory.Items[15].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[15].SubItems.Add("5555555555555555");

            lvMemory.Items.Add("00000100", 16);
            lvMemory.Items[16].SubItems.Add("F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5");
            lvMemory.Items[16].SubItems.Add("5555555555555555");
            
        }

        private void menuToolsMemory_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolsMemory.Checked == true)
            {

                this.Size = new Size(this.Width, MAINFORM_MAX_HEIGHT);
                this.Location = new Point(this.Location.X, this.Location.Y - ((MAINFORM_MAX_HEIGHT - MAINFORM_HEIGHT) / 2));
                lvMemory.Visible = true;

            }

            else
            {

                this.Size = new Size(this.Width, MAINFORM_HEIGHT);
                this.Location = new Point(this.Location.X, this.Location.Y + ((MAINFORM_MAX_HEIGHT - MAINFORM_HEIGHT) / 2));
                lvMemory.Visible = false;

            }
        }
      
    }
}
