using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using Assist_UNA;

namespace Assist_UNA
{
    /**********************************************************************************************
     * 
     * Name: MainForm class
     * 
     * ============================================================================================
     * 
     * Description: This class is the main form user interface window. 
     *                            
     * ============================================================================================         
     * 
     * Modification History
     * --------------------
     * 03/09/2014   ACA     Original version.
     * 03/09/2014   ACA     Added data members and options form functionality. 
     *                      Documented code.
     * 03/10/2014   ACA     Added more data members and methods.
     * 03/11/2014   ACA     Added more functionality and documentation.
     * 03/13/2014   ACA     Added/fixed documentation.
     * 03/13/2014   CLB     Added data members and methods.
     * 03/14/2014   CLB     Added functionality to some of the buttons on the Main Form.
     * 03/14/2014   ACA     Added functionality to line and column labels. Added column ruler.
     *                      Added first version of line numbers.
     *                      Added custom textbox component.
     *                      Added data members and some event methods. Added/fixed documentation.
     * 03/15/2014   ACA     Added saving and opening projects and importing .txt code with THH.
     * 03/16/2014   THH     Some formatting, code and commenting cleanup. 
     *                      Added some functionality to the printing functions.
     * 03/17/2014   ACA     Added tab stops. 
     *                      Commented out line numbers. 
     *                      Added "are you sure" upon closing without saving. 
     *                      Added/fixed documentation. 
     *                      Added functionality to the save menu button. 
     *                      Added the ability to insert into both the memory listview and the 
     *                          symbol table listview. 
     *                      Added the ability to update the register displays. 
     *                      Added functionality to various methods. 
     *                      Fixed some oversights. 
     *                      Added view PRT functionality. 
     *                      Added exporting to .txt and .rtf files.
     * 03/17/2014   THH     More code cleanup, modulized some of the functions that are used on 
     *                          multiple points (ex. print, open, save, etc.).
     * 03/17/2014   ACA     Added shortcuts and tooltips. 
     * 03/19/2014   ACA     Changed to rich text box and fixed most features back.
     *                      Added minimal, slightly buggy syntax highlighting.
     *                      Disabled zoom and font size change.
     *                      Added many data members for instructions colors and strings.
     *                      Added some convenience data members.
     *                      Disabled word wrap and disallowed typing past line 79, for highlights.
     *                      Other various tweaks.
     *  
     *********************************************************************************************/
    public partial class MainForm : Form
    {
        
        /* Constants. */
        const int DEFAULT_MAX_INSTRUCTIONS = 5000;
        const int DEFAULT_MAX_LINES = 500;
        const int DEFAULT_MAX_PAGES = 100;
        const int DEFAULT_MAX_SIZE = 2700;
        const int MAINFORM_WIDTH = 1048;
        const int MAINFORM_HEIGHT = 648;

        /* Private members. */
        private bool isSaved = false;
        private bool projectExists = false;
        private Color commentColor = Color.Green;
        private Color defaultColor = Color.Black;
        private Color directivesColor = Color.Indigo;
        private Color instructionBranchColor = Color.DarkBlue;
        private Color instructionRRColor = Color.Blue;
        private Color instructionRSColor = Color.Turquoise;
        private Color instructionRXColor = Color.Aqua;
        private Color instructionSSColor = Color.AliceBlue;
        private Color macroColor = Color.LightBlue;
        private int cursorColumn;
        private int cursorLine = 0;
        private int cursorLineIndex = 0;
        private int cursorLineLength = 0;
        private int maxInstructions = 5000;
        private int maxLines = 500;
        private int maxPages = 100;
        private int maxSize = 2700;
        private string directory = "";
        private string fileName = "Unsaved Project";
        private string identifier = "";
        private string pathPRT = "";
        private string[] directives;
        private string[] instructionsBranch;
        private string[] instructionsRR;
        private string[] instructionsRS;
        private string[] instructionsRX;
        private string[] instructionsSS;
        private string[] macros;

        /* Public methods. */

        /******************************************************************************************
         * 
         * Name:        MainForm
         * 
         * Author(s):   Drew Aaron
         *              Michael Beaver
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the main form.
         *  
         *****************************************************************************************/
        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(MAINFORM_WIDTH, this.Height);
            this.Size = new Size(this.Width, MAINFORM_HEIGHT);
            this.MinimumSize = new Size(MAINFORM_WIDTH, MAINFORM_HEIGHT);
            
            txtSource.BringToFront();

            directives = new string[8] { "START", "END", "DS", "DC", "USING", "SPACE", "EJECT",
                "TITLE"};
                
            instructionsBranch = new string[4] { "BCT", "BCR", "B", "BR"  };

            instructionsRR = new string[6] { "AR", "SR", "LR", "MR", "DR", "CR" };

            instructionsRS = new string[4] { "STM", "LM", "BAL", "BALR"};

            instructionsRX = new string[7] { "A", "S", "L", "M", "D", "C", "ST" };

            instructionsSS = new string[4] { "MVC", "MVI", "CLC", "CLI" };

            macros = new string[4] { "XREAD", "XPRNT", "XDECI", "XDECO" };
        }


        /******************************************************************************************
         * 
         * Name:        AddMemoryEntry
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       Three strings, representing the three columns
         * Return:      N/A
         * Description: This method will add an item to the memory display on the GUI.
         *  
         *****************************************************************************************/
        public void AddMemoryEntry(string address, string contents, string charRepresentation)
        {
            string[] arr = new string[3];
            ListViewItem lvItem;
            arr[0] = address;
            arr[1] = contents;
            arr[2] = charRepresentation;
            lvItem = new ListViewItem(arr);
            lvMemory.Items.Add(lvItem);
        }


        /******************************************************************************************
         * 
         * Name:        AddSymbolTableEntry
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       Two strings, representing the two columns.
         * Return:      N/A
         * Description: This method will add an item to the symbol table display on the GUI.
         *  
         *****************************************************************************************/
        public void AddSymbolTableEntry(string symbol, string location)
        {
            string[] arr = new string[2];
            ListViewItem lvItem;
            arr[0] = symbol;
            arr[1] = location;
            lvItem = new ListViewItem(arr);
            lvMemory.Items.Add(lvItem);
        }


        /******************************************************************************************
         * 
         * Name:        GetDirectory
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The path to the folder containing the project (string).
         * Description: This method will return the path to the project.
         *  
         *****************************************************************************************/
        public string GetDirectory()
        {
            return directory;
        }


        /******************************************************************************************
         * 
         * Name:        GetFileNameProject
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The fileNameProject (string).
         * Description: This method will return the project name.
         *  
         *****************************************************************************************/
        public string GetFileNameProject()
        {
            return fileName;
        }


        /******************************************************************************************
         * 
         * Name:        GetID
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The identifier data member (string).
         * Description: This method will return the identifier.
         *  
         *****************************************************************************************/
        public string GetID()
        {
            return identifier;
        }


        /******************************************************************************************
         * 
         * Name:        GetPathPRT
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The pathPRT data member (string).
         * Description: This method will return the PRT file path.
         *  
         *****************************************************************************************/
        public string GetPathPRT()
        {
            return pathPRT;
        }


        /******************************************************************************************
         * 
         * Name:        SetFileNameProject
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be the project name.
         * Return:      N/A
         * Description: This method will set the project name.
         *  
         *****************************************************************************************/
        public void SetFileNameProject(string newFileName)
        {
            fileName = newFileName;
        }


        /******************************************************************************************
         * 
         * Name:        SetID
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be the identifier.
         * Return:      N/A
         * Description: This method will set the identifier.
         *  
         *****************************************************************************************/
        public void SetID(string e)
        {
            identifier = e;
        }

        /******************************************************************************************
         * 
         * Name:        SetPathPRT
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be the PRT file name.
         * Return:      N/A
         * Description: This method will set the PRT file name.
         *  
         *****************************************************************************************/
        public void SetPathPRT(string e)
        {
            pathPRT = e;
        }

        /******************************************************************************************
         * 
         * Name:        SetPSW
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for PSW.
         * Return:      N/A
         * Description: This method will set the PSW text box text.
         *  
         *****************************************************************************************/
        public void SetPSW(string e)
        {
            txtRegisterPSW.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister0
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register0.
         * Return:      N/A
         * Description: This method will set the Register00 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister0(string e)
        {
            txtRegister00.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister1
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register1.
         * Return:      N/A
         * Description: This method will set the Register01 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister1(string e)
        {
            txtRegister01.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister2
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register2.
         * Return:      N/A
         * Description: This method will set the Register02 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister2(string e)
        {
            txtRegister02.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister3
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register3.
         * Return:      N/A
         * Description: This method will set the Register03 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister3(string e)
        {
            txtRegister03.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister4
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register4.
         * Return:      N/A
         * Description: This method will set the Register04 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister4(string e)
        {
            txtRegister04.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister5
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register5.
         * Return:      N/A
         * Description: This method will set the Register05 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister5(string e)
        {
            txtRegister05.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister6
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register6.
         * Return:      N/A
         * Description: This method will set the Register06 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister6(string e)
        {
            txtRegister06.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister7
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register7.
         * Return:      N/A
         * Description: This method will set the Register07 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister7(string e)
        {
            txtRegister07.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister8
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register8.
         * Return:      N/A
         * Description: This method will set the Register08 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister8(string e)
        {
            txtRegister08.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister9
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register9.
         * Return:      N/A
         * Description: This method will set the Register09 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister9(string e)
        {
            txtRegister09.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister10
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register10.
         * Return:      N/A
         * Description: This method will set the Register10 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister10(string e)
        {
            txtRegister10.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister11
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register11.
         * Return:      N/A
         * Description: This method will set the Register11 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister11(string e)
        {
            txtRegister11.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister12
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register12.
         * Return:      N/A
         * Description: This method will set the Register12 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister12(string e)
        {
            txtRegister12.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister13
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register13.
         * Return:      N/A
         * Description: This method will set the Register13 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister13(string e)
        {
            txtRegister13.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister14
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register14.
         * Return:      N/A
         * Description: This method will set the Register14 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister14(string e)
        {
            txtRegister14.Text = e;
        }


        /******************************************************************************************
         * 
         * Name:        SetRegister15
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       New string to be displayed for Register15.
         * Return:      N/A
         * Description: This method will set the Register15 text box text.
         *  
         *****************************************************************************************/
        public void SetRegister15(string e)
        {
            txtRegister15.Text = e;
        }


        /* Private methods. */


        /******************************************************************************************
         * 
         * Name:        Assemble
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will assemble the source code and save a PRT file.             
         *  
         *****************************************************************************************/
        private void Assemble()
        {
            /* Make sure the project has been saved. */
            if ((projectExists == false) || (directory == ""))
            {
                MessageBox.Show("Project must be saved at least once before assembling", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /* Set path to the PRT file to be created. */
                pathPRT = directory + "\\" + fileName + ".PRT";

                /* Assembly function here. */
            }
        }


        /******************************************************************************************
         * 
         * Name:        AssembleDebug
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will assemble the source code and step through the execution.
         *  
         *****************************************************************************************/
        private void AssembleDebug()
        {
            /* Assembly function here. */

            /* Run in debug mode. */
            MessageBox.Show("Assemble with debugger option!");

        }


        /******************************************************************************************
         * 
         * Name:        AssembleFinalRun
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will assemble the source code,save a PRT file, and execute the 
         *              program.
         *  
         *****************************************************************************************/
        private void AssembleFinalRun()
        {
            /* Assembly funcion here. */

            /* Also, some way to set prt name, perhaps return it from assembly function? */

            /* Run the user's program */
            MessageBox.Show("Assemble with final run option!");
        }


        /******************************************************************************************
         * 
         * Name:        ClearDisplay
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will set the register display, symbol table display, and memory 
         *              display back to their default states.              
         *  
         *****************************************************************************************/
        private void ClearDisplay()
        {
            /* Clear litview displays. */
            lvMemory.Items.Clear();
            lvSymbolTable.Items.Clear();

            /* Set default values. */
            txtRegister00.Text = "F4F4F4F4";
            txtRegister01.Text = "F4F4F4F4";
            txtRegister02.Text = "F4F4F4F4";
            txtRegister03.Text = "F4F4F4F4";
            txtRegister04.Text = "F4F4F4F4";
            txtRegister05.Text = "F4F4F4F4";
            txtRegister06.Text = "F4F4F4F4";
            txtRegister07.Text = "F4F4F4F4";
            txtRegister08.Text = "F4F4F4F4";
            txtRegister09.Text = "F4F4F4F4";
            txtRegister10.Text = "F4F4F4F4";
            txtRegister11.Text = "F4F4F4F4";
            txtRegister12.Text = "F4F4F4F4";
            txtRegister13.Text = "F4F4F4F4";
            txtRegister14.Text = "F4F4F4F4";
            txtRegister15.Text = "F4F4F4F4";
            txtRegisterPSW.Text = "F4F4F4F4 F4F4F4F4";

        }


        /******************************************************************************************
         * 
         * Name:        MenuAssembleAssembleClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the Assemble method when the menu option is clicked.             
         *  
         *****************************************************************************************/
        private void MenuAssembleAssembleClick(object sender, EventArgs e)
        {
            Assemble();
        }


        /******************************************************************************************
         * 
         * Name:        MenuAssembleDebugClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the AssembleDebug method when the menu option is
         *              clicked.
         *  
         *****************************************************************************************/
        private void MenuAssembleDebugClick(object sender, EventArgs e)
        {
            AssembleDebug();
        }


        /******************************************************************************************
         * 
         * Name:        MenuAssembleFinalRunClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the AssembleFinalRun method when the menu option is
         *              clicked.
         *  
         *****************************************************************************************/
        private void MenuAssembleFinalRunClick(object sender, EventArgs e)
        {
            AssembleFinalRun();
        }


        /******************************************************************************************
         * 
         * Name:        MenuEditCopyClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will place the selected text on the clipboard.
         *  
         *****************************************************************************************/
        private void MenuEditCopyClick(object sender, EventArgs e)
        {
            txtSource.Copy();
        }


        /******************************************************************************************
         * 
         * Name:        MenuEditCutClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will delete the selected text from the source box and place it
         *              on the clipboard.
         *  
         *****************************************************************************************/
        private void MenuEditCutClick(object sender, EventArgs e)
        {
            txtSource.Cut();
        }


        /*****************************************************************************************
         * 
         * Name:        MenuEditPasteClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will paste contents of the clipboard to the cursor location.
         *              
         *  
         *****************************************************************************************/
        private void MenuEditPasteClick(object sender, EventArgs e)
        {
            Clipboard.SetText(Clipboard.GetText().ToUpper());
            txtSource.Paste();
            
            /* Format text to standards. */
            if(Clipboard.GetText().Contains("\t"))
                RemoveTabs();

            FormatAllText(txtSource.GetFirstCharIndexFromLine(cursorLine) + cursorColumn);
        }


        /******************************************************************************************
         * 
         * Name:        MenuHelpAboutClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will open the about form. 
         *  
         *****************************************************************************************/
        private void MenuHelpAboutClick(object sender, EventArgs e)
        {
            AboutForm abt = new AboutForm();
            abt.ShowDialog();
        }


        /******************************************************************************************
         * 
         * Name:        MenuToolsOptionsClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will open the options menu and apply the given options to the
         *              main program. 
         *  
         *****************************************************************************************/
        private void MenuToolsOptionsClick(object sender, EventArgs e)
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

            /* Save is no longer current. */
            isSaved = false;
            this.Text = "ASSIST/UNA - " + fileName + "*";
        }



        /******************************************************************************************
         * 
         * Name:        MenuToolsViewPRTClick
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the ViewPRT method to open the .PRT file when the 
         *              menu option is clicked. 
         *  
         *****************************************************************************************/
        private void MenuToolsViewPRTClick(object sender, EventArgs e)
        {
            ViewPRT();
        }


        /******************************************************************************************
         * 
         * Name:        MenuHelpOnlineClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will open the user's default web browser to the online help 
         *              manual. 
         *  
         *****************************************************************************************/
        private void MenuHelpOnlineClick(object sender, EventArgs e)
        {
            Process.Start("http://www.una.edu");
        }



        /******************************************************************************************
         * 
         * Name:        MenuFileNewClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the NewProject method to create a new project from 
         *              the menu option.
         *  
         *****************************************************************************************/
        private void MenuFileNewClick(object sender, EventArgs e)
        {
            NewProject();
        }


        /******************************************************************************************
         * 
         * Name:        MenuFileOpenClick
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the OpenProject method to open an existing project
         *              when clicking the menu option.
         *  
         *****************************************************************************************/
        private void MenuFileOpenClick(object sender, EventArgs e)
        {
            OpenProject();
        }


        /******************************************************************************************
         * 
         * Name:        MenuFileSaveClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the saveProject method if the project exists, else it
         *              will call the saveProjectAs method.
         *  
         *****************************************************************************************/
        private void MenuFileSaveClick(object sender, EventArgs e)
        {
            SaveProject();
        }


        
        /******************************************************************************************
         * 
         * Name:        MenuFileSaveAsClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the saveProjectAs method.
         *  
         *****************************************************************************************/
        private void MenuFileSaveAsClick(object sender, EventArgs e)
        {
            SaveProjectAs();
        }


        /******************************************************************************************
         * 
         * Name:        NewProject
         * 
         * Author(s):   Travis Hunt
         *              Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will restore all defaults if the project has been saved. If it
         *              has not been saved, then it will ask to save first.
         *  
         *****************************************************************************************/
        private void NewProject()
        {
            /* Don't ask if it's a new and empty project. */
            if ((projectExists == false) && (txtSource.Text == ""))
                isSaved = true;

            /* Make sure that the project is saved before making a new project. */
            if (isSaved == false)
            {
                DialogResult choice = MessageBox.Show("Your source code contains unsaved changes,"
                    + " would you like to save first?", "Save?", MessageBoxButtons.YesNoCancel);

                if (choice == DialogResult.Yes)
                    SaveProject();

                else if (choice == DialogResult.Cancel)
                    return;
            }

            /* This code will always be executed, unless the cancel button is pressed. */
            RestoreDefaults();
        }


        /******************************************************************************************
         * 
         * Name:        OpenProject
         * 
         * Author(s):   Travis Hunt
         *              Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will open a .una file and load its settings and text into 
         *              ASSIST/UNA.
         *  
         *****************************************************************************************/
        private void OpenProject()
        {
            DialogResult choice;

            /* Check to see if the project save is current. */
            if (isSaved == false && txtSource.Text != "")
            {
                choice = MessageBox.Show("Your source code contains unsaved changes, " +
                "would you like to save first?", "Save?", MessageBoxButtons.YesNoCancel);

                if (choice == DialogResult.Yes)
                        SaveProject();
                else if (choice == DialogResult.Cancel)
                    return;
            }

            /* This code will always execute as long as cancel is not pressed. */

            /* Open file dialog and set filter. */
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "ASSIST/UNA Files (*.una)|*.una";
            choice = dlgOpen.ShowDialog();

            if (choice == DialogResult.OK)
            {
                /* Set file name and directory data members. */
                fileName = Path.GetFileNameWithoutExtension(dlgOpen.FileName);
                directory = Path.GetDirectoryName(dlgOpen.FileName);

                /* Read in the lines and set the data members. */
                StreamReader unaFileReader = new StreamReader(dlgOpen.FileName);
                unaFileReader.ReadLine();
                maxInstructions = Convert.ToInt32(unaFileReader.ReadLine());
                maxLines = Convert.ToInt32(unaFileReader.ReadLine());
                maxPages = Convert.ToInt32(unaFileReader.ReadLine());
                maxSize = Convert.ToInt32(unaFileReader.ReadLine());
                unaFileReader.ReadLine();

                /* Fill the source editor with saved source code. */
                txtSource.Text = unaFileReader.ReadToEnd();

                unaFileReader.Close();

                /* Update title bar. */
                this.Text = "ASSIST/UNA - " + fileName;

                /* Project now exists and the save is current. */
                projectExists = true;
                isSaved = true;

                /* Format the text and check for syntax highlighting. */
                txtSource.Text = txtSource.Text.ToUpper();
                RemoveTabs();
                FormatAllText(0);
            }            
        }


        /******************************************************************************************
         * 
         * Name:        RestoreDefaults
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will restore all data members and display elements to their
         *              default state.
         *  
         *****************************************************************************************/
        private void RestoreDefaults()
        {
            isSaved = false;
            projectExists = false;
            maxInstructions = DEFAULT_MAX_INSTRUCTIONS;
            maxLines = DEFAULT_MAX_LINES;
            maxPages = DEFAULT_MAX_PAGES;
            maxSize = DEFAULT_MAX_SIZE;
            directory = "";
            fileName = "Unsaved Project";
            pathPRT = "";
            identifier = "";
            txtSource.Clear();
            this.Text = "ASSIST/UNA - Unsaved Project*";
            ClearDisplay();
        }


        /******************************************************************************************
         * 
         * Name:        SaveProject
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will overwrite the old version of the project with the new one.
         *  
         *****************************************************************************************/
        private void SaveProject()
        {
            /* Overwrite previous if the project exists. If not, open the dialog to save as. */
            if (projectExists)
            {
                /* Set options into a string. */
                string fileContents = "#\n" + maxInstructions + "\n" + maxLines + "\n" + maxPages
                    + "\n" + maxSize + "\n#\n";

                /* Set source text into the string. */
                fileContents += txtSource.Text;

                /* Write string to file. */
                StreamWriter unaFile = new StreamWriter(@dlgSave.FileName);
                unaFile.Write(fileContents);
                unaFile.Close();

                /* The project is now saved. */
                isSaved = true;
                this.Text = "ASSIST/UNA - " + fileName;
            }
            else
                SaveProjectAs();
            
        }


        /******************************************************************************************
         * 
         * Name:        SaveProjectAs
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will open a save file dialog which allows the user to save the
         *              project as a .una file to a specified location.
         *  
         *****************************************************************************************/
        private void SaveProjectAs()
        {
            /* Save project to a specified location. */
            DialogResult choice = dlgSave.ShowDialog();

            if ((dlgSave.FileName != "") && (choice == DialogResult.OK))
            {
                /* Set data members. */
                directory = Path.GetDirectoryName(dlgSave.FileName);
                fileName = Path.GetFileNameWithoutExtension(dlgSave.FileName);

                /* Set options into a string. */
                string fileContents = "#\n" + maxInstructions + "\n" + maxLines + "\n" + maxPages
                    + "\n" + maxSize + "\n#\n";
                
                /* Set source text into the string. */
                fileContents += txtSource.Text;

                /* Write string to file. */
                StreamWriter unaFile = new StreamWriter(@dlgSave.FileName);
                unaFile.Write(fileContents);
                unaFile.Close();
                
                /* The project now exists and is saved. */
                projectExists = true;
                isSaved = true;

                /* Set initial directory for next time. */
                dlgSave.InitialDirectory = directory;

                /* Update title bar. */
                this.Text = "ASSIST/UNA - " + fileName;
            }
        }


        /******************************************************************************************
         * 
         * Name:        TxtSourceTextChanged
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       User event (changes text in txtSource).
         * Return:      N/A
         * Description: This method will perform various actions whenever the source code text is
         *              changed. 
         *              
         *              This includes: 
         *                  setting the isSaved boolean,
         *                  updating the line and column labels, and
         *                  formatting the text.            
         *  
         *****************************************************************************************/
        private void TxtSourceTextChanged(object sender, EventArgs e)
        {         
            /* Since the text has been changed, its save is no longer current. */
            isSaved = false;
            this.Text = "ASSIST/UNA - " + fileName + "*";

            /* Set the new cursor location and update it in the labels. */
            UpdateCursorLocation();
            
            /* Text formatting. */
            FormatLineText();
        }


        /******************************************************************************************
         * 
         * Name:        FormatAllText
         * 
         * Author(s):   Drew Aaron
         *              Michael Beaver
         *              
         * Input:       The index location to place the cursor after formatting (integer).
         * Return:      N/A
         * Description: This method will check the entire source editor and  highlight certain 
         *              syntax strings.
         *  
         *****************************************************************************************/
        private void FormatAllText(int position)
        {
            for (int i = 0; i < txtSource.Lines.Length; i++)
            {
                ;
            }

            txtSource.Select(position, 0);
            txtSource.SelectionColor = defaultColor;
        }


        /******************************************************************************************
         * 
         * Name:        FormatLineText
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will check the current line of the editor and highlight certain
         *              syntax strings.
         *  
         *****************************************************************************************/
        private void FormatLineText()
        {
            /* Temporary variables for finding syntax to highlight. */
            bool colorChanged = false;
            char firstChar = 'x';
            int lineStart = txtSource.GetFirstCharIndexFromLine(cursorLine);
            int currentLineNumber = cursorLine;
            int currentLineIndex = cursorColumn;
            int highlightStart = 0;
            string currentLine = "";
            string instruction = "";
            
            if (txtSource.Text != "")
            {
                try
                {
                    currentLine = txtSource.Text.Substring(lineStart);
                }
                catch (Exception e)
                {

                }
                    
                if (currentLine != "")
                firstChar = currentLine[0];
            }

            /* Highlight comment lines. */
            if (firstChar == '*')
            {
                txtSource.Select(lineStart, currentLine.Length);
                txtSource.SelectionColor = commentColor;
                colorChanged = true;
            }
            else
            {
                /* Highlight end of line comments */
                if (currentLine.Length > 15)
                {
                    /* Find if there is a space after the last field. */
                    for (int i = currentLine.Length - 1; i >= 15; i--)
                    {
                        if (currentLine[i] == ' ')
                            highlightStart = lineStart + i;
                    }

                    /* If so, highlight the rest of the line. */
                    if (highlightStart > 0)
                    {
                        txtSource.Select(highlightStart, currentLine.Length);
                        txtSource.SelectionColor = commentColor;
                        colorChanged = true;
                    }

                }

                /* Highlight instructions. */
                if ((currentLineIndex > 9) && (currentLine.Length < 15))
                {
                    instruction = currentLine.Substring(8, currentLine.Length - 8);

                    highlightStart = lineStart + 9;

                    foreach (string line in instructionsRR)
                    {
                        if ((instruction.Substring(1, 1) != " ") && (instruction.Substring(0, 1) == " "))
                        {
                            if (instruction.Length > line.Length)
                            {
                                if (line.Contains(instruction.Substring(1, line.Length)))
                                {
                                    txtSource.Select(highlightStart, 5);
                                    txtSource.SelectionColor = instructionRRColor;
                                    colorChanged = true;
                                }
                            }

                            else
                            {
                                if (line.Contains(instruction.Substring(1, currentLine.Length - 9)))
                                {
                                    txtSource.Select(highlightStart, 5);
                                    txtSource.SelectionColor = instructionRRColor;
                                    colorChanged = true;
                                }
                            }
                            
                        }

                    }

                    if (colorChanged == false)
                    {
                        txtSource.Select(highlightStart, 5);
                        txtSource.SelectionColor = defaultColor;
                        colorChanged = true;
                    }
                }
            }

            /* Change text color back to default afterwards. */
            if (colorChanged)
            {
                
                txtSource.Select(lineStart + currentLineIndex, 0);
                txtSource.SelectionColor = defaultColor;
            }

        }


        /******************************************************************************************
         * 
         * Name:        MenuFileExitClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will exit ASSIST/UNA if the project's save is current, if not
         *              it will ask to save first.
         *  
         *****************************************************************************************/
        private void MenuFileExitClick(object sender, EventArgs e)
        {
            /* The MainFormClosing method will handle the "are you sure" box. */
            this.Close();
        }


        /******************************************************************************************
         * 
         * Name:        TsAssembleClick
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the Assemble method when the option is clicked from 
         *              the toolbar.
         *  
         *****************************************************************************************/
        private void TsAssembleClick(object sender, EventArgs e)
        {
            Assemble();
        }


        /******************************************************************************************
         * 
         * Name:        TsAssembleDebugClick
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the AssembleDebug method when the option is clicked
         *              from the toolbar.
         *  
         *****************************************************************************************/
        private void TsAssembleDebugClick(object sender, EventArgs e)
        {
            AssembleDebug();
        }


        /******************************************************************************************
         * 
         * Name:        TsAssembleFinalRunClick
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the AssembleFinalRun method when the option is 
         *              clicked from the toolbar.
         *  
         *****************************************************************************************/
        private void TsAssembleFinalRunClick(object sender, EventArgs e)
        {
            AssembleFinalRun();
        }

                
        /******************************************************************************************
         * 
         * Name:        TsNewClick
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the NewProject method to create a new .una project
         *              when the tool bar option is clicked.
         *  
         *****************************************************************************************/
        private void TsNewClick(object sender, EventArgs e)
        {
            NewProject();
        }


        /******************************************************************************************
         * 
         * Name:        TsPrintClick
         * 
         * Author(s):   Clay Boren
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the PrintSource method to print the source code in 
         *              the editor when the toolbar option is clicked.
         *  
         *****************************************************************************************/
        private void TsPrintClick(object sender, EventArgs e)
        {
            PrintSource();
        }


        /******************************************************************************************
         * 
         * Name:        PrintDocumentOnPrintPage
         * 
         * Author(s):   Clay Boren
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will format the source code to a printable format.
         *  
         *****************************************************************************************/
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(this.txtSource.Text, this.txtSource.Font, Brushes.Black, 10, 25);
        }


        /******************************************************************************************
         * 
         * Name:        PrintSource
         * 
         * Author(s):   Clay Boren
         *              Travis Hunt
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will display the print dialog box and print out the source
         *              code.
         *  
         *****************************************************************************************/
        private void PrintSource()
        {
            DialogResult choice;
            PrintDocument printSource = new PrintDocument();
            PrintDialog printOptions = new PrintDialog();

            choice = printOptions.ShowDialog();

            if (choice == DialogResult.OK)
            {
                printSource.PrintPage += PrintDocumentOnPrintPage;
                printSource.PrinterSettings = printOptions.PrinterSettings;
                printSource.Print();
            }
        }


        /******************************************************************************************
         * 
         * Name:        TsViewPRTClick
         * 
         * Author(s):   Drew Aaron
         *              Clay Boren
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the ViewPRT method to open the .PRT file whenever the
         *              option in the toolbar is clicked.
         *  
         *****************************************************************************************/
        private void TsViewPRTClick(object sender, EventArgs e)
        {
            ViewPRT();
        }


        /******************************************************************************************
         * 
         * Name:        TsOpenClick
         * 
         * Author(s):   Clay Boren
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the OpenProject method to open a .una project when
         *              the toolbar option is clicked.
         *  
         *****************************************************************************************/
        private void TsOpenClick(object sender, EventArgs e)
        {
            OpenProject();
        }


        /******************************************************************************************
         * 
         * Name:        TsSaveAsClick
         * 
         * Author(s):   Clay Boren
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the SaveProject method if the project has been
         *              previously saved. If not, the SaveProjectAs method is called. 
         *              This handles when the toobar option is clicked.
         *  
         *****************************************************************************************/
        private void TsSaveAsClick(object sender, EventArgs e)
        {
           SaveProjectAs();
        }


        /******************************************************************************************
         * 
         * Name:        TsSaveClick
         * 
         * Author(s):   Clay Boren
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the SaveProject method if the project has been
         *              previously saved. If not, the SaveProjectAs method is called. 
         *              This handles when the toobar option is clicked.
         *  
         *****************************************************************************************/
        private void TsSaveClick(object sender, EventArgs e)
        {
            SaveProject();
        }


        /******************************************************************************************
         * 
         * Name:        TxtSourceMouseUp
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will set the line and column numbers to the new cursor location
         *              whenever the mouse button is released.
         *  
         *****************************************************************************************/
        private void TxtSourceMouseUp(object sender, MouseEventArgs e)
        {
            UpdateCursorLocation();
        }


        /******************************************************************************************
         * 
         * Name:        TxtSourceKeyDown
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User key press event.
         * Return:      N/A
         * Description: This method will perform various actions depending on which key is pressed.
         *  
         *****************************************************************************************/
        private void TxtSourceKeyDown(object sender, KeyEventArgs e)
        {
            /* Currently used for testing purposes */
            SetRegister0(Convert.ToString(txtSource.Lines.Length));

            /* Do not allow font size change shortcut. */
            e.SuppressKeyPress = e.Control && e.Shift & 
                (e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemPeriod);
        }


        /******************************************************************************************
         * 
         * Name:        UpdateCursorLocation
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will set the line and column labels to the current cursor 
         *              position.
         *  
         *****************************************************************************************/
        private void UpdateCursorLocation()
        {
            cursorLineIndex = txtSource.GetFirstCharIndexOfCurrentLine();
            
            cursorLine =
                txtSource.GetLineFromCharIndex(cursorLineIndex);

            cursorColumn = txtSource.SelectionStart -
                txtSource.GetFirstCharIndexFromLine(cursorLine);

            if (txtSource.Text == "")
                cursorLineLength = 0;

            else
                cursorLineLength = txtSource.Lines[cursorLine].Length;

            lblLine.Text = "Line: " + Convert.ToString(cursorLine + 1);
            lblColumn.Text = "Column: " + Convert.ToString(cursorColumn + 1);
        }


        /******************************************************************************************
         * 
         * Name:        ViewPRT
         * 
         * Author(s):   Travis Hunt
         *              Drew Aaron
         *              Clay Boren
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will open the PRT, if it exists.
         *  
         *****************************************************************************************/
        private void ViewPRT()
        {
            if (pathPRT != "")
            {
                ViewPRTForm PRT = new ViewPRTForm();
                PRT.LoadPRT(pathPRT);
                PRT.ShowDialog();
            }

            else
                MessageBox.Show("No PRT associated with this project, please assemble first",
                    "Error");
        }


        /******************************************************************************************
         * 
         * Name:        TxtSourceKeyUp
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User key press event.
         * Return:      N/A
         * Description: This method will perform various actions depending on which key is pressed.
         *  
         *****************************************************************************************/
        private void TxtSourceKeyUp(object sender, KeyEventArgs e)
        {
            /* On arrow key release, set the new cursor location and update it in the labels */
            if ((e.KeyData == Keys.Up) || (e.KeyData == Keys.Down) ||
                (e.KeyData == Keys.Left) || (e.KeyData == Keys.Right))
                    UpdateCursorLocation();

            /* Set tab stops. */
            if (e.KeyCode == Keys.Tab)
            {
                /* On tab press, add enough spaces to get to the next tab stop line. */
                if (cursorColumn <= 8)
                {
                    for (int i = cursorColumn; i < 9; i++)
                        txtSource.SelectedText = " ";
                }

                else if (cursorColumn <= 15)
                {
                    for (int i = cursorColumn; i < 15; i++)
                        txtSource.SelectedText = " ";
                }
            }

            if (e.KeyCode == Keys.Return)
            {
                
                //if ((cursorLine <= txtSource.Lines.Length - 1) && (txtSource.Lines[cursorLine][0] != '*'))
                {
                    txtSource.Select(cursorLineIndex, cursorLineLength);
                    txtSource.SelectionColor = defaultColor;
                    txtSource.Select(cursorLineIndex, 0);
                }
            }
        }


        /******************************************************************************************
         * 
         * Name:        MenuFileImportClick
         * 
         * Author(s):   Drew Aaron
         *              Clay Boren
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will import the contents of a txt file into the source editor.
         *  
         *****************************************************************************************/
        private void MenuFileImportClick(object sender, EventArgs e)
        {
            OpenFileDialog dlgImport = new OpenFileDialog();
            dlgImport.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (dlgImport.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new
                StreamReader(dlgImport.FileName);
                txtSource.Text = sr.ReadToEnd();
                sr.Close();
            }
            
            /* Remove all tabs and check for syntax highlighting. */
            txtSource.Text = txtSource.Text.ToUpper();
            RemoveTabs();
            FormatAllText(0);
        }


        /******************************************************************************************
         * 
         * Name:        RemoveTabs
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will replace all tabs from source code with spaces.
         *  
         *****************************************************************************************/
        private void RemoveTabs()
        {
            int pos = txtSource.GetFirstCharIndexFromLine(cursorLine) + cursorColumn;
            txtSource.Text = txtSource.Text.Replace("\t", " ");
            txtSource.Select(pos, 0);
        }


        /******************************************************************************************
         * 
         * Name:        MainFormClosing
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User closes the main form.
         * Return:      N/A
         * Description: This method will open an "are you sure" box if closing while unsaved.
         *  
         *****************************************************************************************/
        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            /* Don't ask if it's a new and empty project. */
            if ((projectExists == false) && (txtSource.Text == ""))
                isSaved = true;

            /* This will pop up the "are you sure" box if the save is not current. */
            if (isSaved == false)
            {
                DialogResult choice = MessageBox.Show("Your source code contains unsaved changes,"
                    + " would you like to save first?", "Save?", MessageBoxButtons.YesNoCancel);

                if (choice == DialogResult.Yes)
                    SaveProject();
                else if (choice == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }


        /******************************************************************************************
         * 
         * Name:        TxtSourceKeyPress
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User key press event.
         * Return:      N/A
         * Description: This method will capture key presses.
         *  
         *****************************************************************************************/
        private void TxtSourceKeyPress(object sender, KeyPressEventArgs e)
        {
            /* This will disable the normal tab key function. */
            if (e.KeyChar == '\t')
                e.Handled = true;

            /* Force uppercase characters. */
            e.KeyChar = Char.ToUpper(e.KeyChar);

            /* Do not allow typing past line 79. */
            if ((cursorColumn >= 79) && (e.KeyChar != '\n') && (e.KeyChar != Convert.ToChar(Keys.Back)))
                e.Handled = true;
        }


        /******************************************************************************************
         * 
         * Name:        MenuFileExportClick
         * 
         * Author(s):   Drew Aaron
         *              Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will export the contents of the source code editor into 
         *              either a .txt or .rtf file.
         *  
         *****************************************************************************************/
        private void MenuFileExportClick(object sender, EventArgs e)
        {
            /* Set the options and settings for the export dialog. */
            DialogResult choice;
            SaveFileDialog dlgExport = new SaveFileDialog();
            dlgExport.DefaultExt = ".txt";
            dlgExport.FileName = fileName + ".txt";
            dlgExport.Filter = "Text Files |*.txt | Rich Text Format |*.rtf";
            dlgExport.Title = "Save Project As";
            choice = dlgExport.ShowDialog();

            if (choice == DialogResult.OK)
            {
                /* Write source code to file. */
                StreamWriter unaFile = new StreamWriter(@dlgExport.FileName);
                unaFile.Write(txtSource.Text);
                unaFile.Close();
            }
        }


        /******************************************************************************************
         * 
         * Name:        MenuFilePrintSourceClick
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       User click event.
         * Return:      N/A
         * Description: This method will call the PrintSource method when the menu option is 
         *              clicked.
         *  
         *****************************************************************************************/
        private void MenuFilePrintSourceClick(object sender, EventArgs e)
        {
            PrintSource();
        }
    }
}