using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**********************************************************
 * 
 * Name: MainForm class
 * 
 * ========================================================
 * 
 * Description: This class is the main form user interface
 *              window. 
 *                            
 * ========================================================         
 * 
 * Modification History
 * --------------------
 * 3/9/2014     ACA     Original version.
 * 3/9/2014     ACA     Added data members and options form
 *                      functionality. Documented code.
 * 3/10/2014    ACA     Added more data members and methods.
 *  
 **********************************************************/

namespace Assist_UNA
{
    public partial class MainForm : Form
    {
        
        /* Constants. */
        const int MAINFORM_WIDTH = 935;
        const int MAINFORM_HEIGHT = 648;

        /* Private members. */
        private bool isSaved = false;
        private bool projectExists = false;
        private int maxInstructions = 9000;
        private int maxLines = 900;
        private int maxPages = 900;
        private int maxSize = 2700;
        private string fileNameProject = "Project";
        private string fileNamePRT = "";
        private string identifier = "";

        /* Public methods. */

        /**********************************************************
         * 
         * Name:        MainForm
         * 
         * Author(s):   Drew Aaron
         *              Michael Beaver
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the main form
         *  
         **********************************************************/
        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(MAINFORM_WIDTH, this.Height);
            this.Size = new Size(this.Width, MAINFORM_HEIGHT);
            this.MinimumSize = new Size(935, 648);
        }


        /**********************************************************
         * 
         * Name:        GetFileNameProject
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The fileNameProject (string).
         * Description: This method will return the project name.
         *  
         **********************************************************/
        public string GetFileNameProject(string e)
        {
            return fileNameProject;
        }


        /**********************************************************
         * 
         * Name:        GetFileNamePRT
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The fileNamePRT data member (string).
         * Description: This method will return the PRT file name.
         *  
         **********************************************************/
        public string GetFileNamePRT()
        {
            return fileNamePRT;
        }


        /**********************************************************
         * 
         * Name:        GetID
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The identifier data member (string).
         * Description: This method will return the identifier.
         *  
         **********************************************************/
        public string GetID()
        {
            return identifier;
        }


        /**********************************************************
         * 
         * Name:        SetID
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be the project name
         * Return:      N/A
         * Description: This method will set the project name.
         *  
         **********************************************************/
        public void SetFileNameProject(string e)
        {
            fileNameProject = e;
        }


        /**********************************************************
         * 
         * Name:        SetFileNamePRT
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be the PRT file name
         * Return:      N/A
         * Description: This method will set the PRT file name.
         *  
         **********************************************************/
        public void SetFileNamePRT(string e)
        {
            fileNamePRT = e;
        }


        /**********************************************************
         * 
         * Name:        SetID
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be the identifier
         * Return:      N/A
         * Description: This method will set the identifier.
         *  
         **********************************************************/
        public void SetID(string e)
        {
            identifier = e;
        }


        /* Private methods. */

        /**********************************************************
         * 
         * Name:        clearDisplay
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will set the register display,
         *              symbol table display, and memory display
         *              back to their default states.
         *  
         **********************************************************/
        private void clearDisplay()
        {
            /* Clear displays */
            txtRegister00.Clear();
            txtRegister01.Clear();
            txtRegister02.Clear();
            txtRegister03.Clear();
            txtRegister04.Clear();
            txtRegister05.Clear();
            txtRegister06.Clear();
            txtRegister07.Clear();
            txtRegister08.Clear();
            txtRegister09.Clear();
            txtRegister10.Clear();
            txtRegister11.Clear();
            txtRegister12.Clear();
            txtRegister13.Clear();
            txtRegister14.Clear();
            txtRegister15.Clear();
            txtRegisterPSW.Clear();
            lvMemory.Clear();
            lvSymbolTable.Clear();

            /* Set default values */
            txtRegister00.AppendText("F4F4F4F4");
            txtRegister01.AppendText("F4F4F4F4");
            txtRegister02.AppendText("F4F4F4F4");
            txtRegister03.AppendText("F4F4F4F4");
            txtRegister04.AppendText("F4F4F4F4");
            txtRegister05.AppendText("F4F4F4F4");
            txtRegister06.AppendText("F4F4F4F4");
            txtRegister07.AppendText("F4F4F4F4");
            txtRegister08.AppendText("F4F4F4F4");
            txtRegister09.AppendText("F4F4F4F4");
            txtRegister10.AppendText("F4F4F4F4");
            txtRegister11.AppendText("F4F4F4F4");
            txtRegister12.AppendText("F4F4F4F4");
            txtRegister13.AppendText("F4F4F4F4");
            txtRegister14.AppendText("F4F4F4F4");
            txtRegister15.AppendText("F4F4F4F4");
            txtRegisterPSW.AppendText("F4F4F4F4 F4F4F4F4");

        }


        /**********************************************************
         * 
         * Name:        menuToolsOptions_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will open the options menu and 
         *              apply the given options to the main program. 
         *  
         **********************************************************/
        private void menuToolsOptions_Click(object sender, EventArgs e)
        {
            OptionsForm opt = new OptionsForm();
            
            /* Set textbox options to correct numbers. */
            opt.SetMaxLines(maxLines);
            opt.SetMaxInstructions(maxInstructions);
            opt.SetMaxPages(maxPages);
            opt.SetMaxSize(maxSize);
            
            opt.ShowDialog();

            /* Get the new values from options form. */
            maxLines = opt.GetMaxLines();
            maxInstructions = opt.GetMaxInstructions();
            maxPages = opt.GetMaxPages();
            maxSize = opt.GetMaxSize();

            /* Apply the new values. */
            txtSource.MaxLength = maxLines * 70;
        }


        /**********************************************************
         * 
         * Name:        menuHelpAbout_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will open the about form. 
         *  
         **********************************************************/
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {           
            AboutForm abt = new AboutForm();
            abt.ShowDialog();
        }


        /**********************************************************
         * 
         * Name:        menuToolsViewPRT_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will open the PRT, if it exists. 
         *  
         **********************************************************/
        private void menuToolsViewPRT_Click(object sender, EventArgs e)
        {
            if (fileNamePRT != "")
                Process.Start("wordpad.exe", fileNamePRT);
            else
                MessageBox.Show("No PRT associated with this project, please assemble first",
                    "Error");
        }


        /**********************************************************
         * 
         * Name:        menuHelpOnline_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will open the user's default
         *              web browser to the online help manual. 
         *  
         **********************************************************/
        private void menuHelpOnline_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.una.edu");
        }


        /**********************************************************
         * 
         * Name:        menuAssembleAssemble_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will assemble the source code
         *              and save a PRT file.
         *  
         **********************************************************/
        private void menuAssembleAssemble_Click(object sender, EventArgs e)
        {
            /* Assembly funcion here. */

            /* Also, some way to set prt name, perhaps return it from assembly function? */

        }


        /**********************************************************
         * 
         * Name:        menuAssembleDebug_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will assemble the source code
         *              and step through the execution.
         *  
         **********************************************************/
        private void menuAssembleDebug_Click(object sender, EventArgs e)
        {
            /* Assembly function here. */

            /* Run in debug mode. */

        }


        /**********************************************************
         * 
         * Name:        menuAssembleFinalRun_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will assemble the source code,
         *              save a PRT file, and execute the program.
         *  
         **********************************************************/
        private void menuAssembleFinalRun_Click(object sender, EventArgs e)
        {
            /* Assembly funcion here. */

            /* Also, some way to set prt name, perhaps return it from assembly function? */

            /* Run the user's program */
        }


        /**********************************************************
         * 
         * Name:        menuEditCut_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will delete the selected text
         *              from the source box and place it on the
         *              clipboard
         *  
         **********************************************************/
        private void menuEditCut_Click(object sender, EventArgs e)
        {
            txtSource.Cut();
        }


        /**********************************************************
         * 
         * Name:        menuEditCopy_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will place the selected text on
         *              the clipboard
         *  
         **********************************************************/
        private void menuEditCopy_Click(object sender, EventArgs e)
        {
            txtSource.Copy();
        }


        /**********************************************************
         * 
         * Name:        menuEditPaste_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will paste the contents of the 
         *              clipboard to the cursor location.
         *  
         **********************************************************/
        private void menuEditPaste_Click(object sender, EventArgs e)
        {
            txtSource.Paste();
        }


        /**********************************************************
         * 
         * Name:        menuFileNew_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will restore all defaults if
         *              the project has been saved. If it has not
         *              been saved, then it will ask to save first.
         *  
         **********************************************************/
        private void menuFileNew_Click(object sender, EventArgs e)
        {
            DialogResult choice;
            
            if (isSaved == false)
            {
                choice = MessageBox.Show("Your source code contains unsaved changes, " +
                    "would you like to save first?", "Save?", MessageBoxButtons.YesNoCancel);

                if (choice == DialogResult.Yes)
                {
                    if (projectExists)
                        saveProject();
                    else
                        saveProjectAs();
                }
                else if (choice == DialogResult.Cancel)
                    return;
            }
            
            /* This code will always, be executed, unless the cancel button is pressed. */
            restoreDefaults();
        }


        /**********************************************************
         * 
         * Name:        menuFileOpen_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will open a .una file and load
         *              its settings and text into ASSIST/UNA.
         *  
         **********************************************************/
        private void menuFileOpen_Click(object sender, EventArgs e)
        {

        }


        /**********************************************************
         * 
         * Name:        menuFileSave_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will call the saveProject
         *              method if the project exists, else it
         *              will call the saveProjectAs method.
         *  
         **********************************************************/
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            if (projectExists)
                saveProject();
            else
                saveProjectAs();
        }


        /**********************************************************
         * 
         * Name:        menuFileSaveAs_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will call the saveProjectAs
         *              method.
         *  
         **********************************************************/
        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            saveProjectAs();
        }


        /**********************************************************
         * 
         * Name:        restoreDefaults
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will restore all data members
         *              and display elements to their default state.
         *  
         **********************************************************/
        private void restoreDefaults()
        {
            isSaved = false;
            projectExists = false;
            maxInstructions = 9000;
            maxLines = 900;
            maxPages = 900;
            maxSize = 2700;
            fileNameProject = "Project";
            fileNamePRT = "";
            identifier = "";
            txtSource.Clear();
            clearDisplay();
            
        }


        /**********************************************************
         * 
         * Name:        saveProject
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will overwrite the old version
         *              of the project with the new one.
         *  
         **********************************************************/
        private void saveProject()
        {
            /* Save over current file. */
            

            /* The project now is saved. */
            isSaved = true;
        }


        /**********************************************************
         * 
         * Name:        saveProjectAs
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will open a save file dialog
         *              which allows the user to save the project
         *              as a .una file to a specified location.
         *  
         **********************************************************/
        private void saveProjectAs()
        {
            /* Save project to a specified location. */
            dlgSave.ShowDialog();

            /* The project now exists and is saved */
            projectExists = true;
            isSaved = true;
        }


        /**********************************************************
         * 
         * Name:        txtSource_TextChanged
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User event (changes text in txtSource)
         * Return:      N/A
         * Description: This method will perform various actions
         *              whenever the source code text is changed.
         *              This includes: setting the isSaved boolean
         *              to false, since the save is no longer 
         *              current.
         *  
         **********************************************************/
        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            /* Since the text has been changed, its save is no longer current */
            isSaved = false;
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            DialogResult choice;

            if (isSaved == false)
            {
                choice = MessageBox.Show("Your source code contains unsaved changes, " +
                    "would you like to save first?", "Save?", MessageBoxButtons.YesNoCancel);

                if (choice == DialogResult.Yes)
                {
                    if (projectExists)
                        saveProject();
                    else
                        saveProjectAs();
                }
                else if (choice == DialogResult.Cancel)
                    return;
            }

            /* This code will always, be executed, unless the cancel button is pressed. */
            this.Close();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {

        }
    }
}
