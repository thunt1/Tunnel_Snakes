using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assist_UNA
{
    /**********************************************************************************************
     * 
     * Name: ViewPRTForm class
     * 
     * ============================================================================================
     * 
     * Description: This class is the view PRT form window.
     *                            
     * ============================================================================================         
     * 
     * Modification History
     * --------------------
     * 03/16/2014   ACA     Original version.
     * 03/17/2014   ACA     Added functionality.
     * 03/17/2014   THH     Made the start location center screen.
     * 
     *********************************************************************************************/
    public partial class ViewPRTForm : Form
    {
        
        /* Public methods. */

        /******************************************************************************************
         * 
         * Name:        ViewPRTForm
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the view PRT form
         *  
         *****************************************************************************************/
        public ViewPRTForm()
        {
            InitializeComponent();
        }


        /******************************************************************************************
         * 
         * Name:        LoadPRT
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       pathPRT is a string containing the path to the .PRT file.
         * Return:      N/A
         * Description: This method will load the contents of the .PRT file into the text box.
         *  
         *****************************************************************************************/
        public void LoadPRT(string pathPRT)
        {
            StreamReader sr = new StreamReader(pathPRT);
            txtPRT.Text = sr.ReadToEnd();
            sr.Close();
        }


        /******************************************************************************************
         * 
         * Name:        BtnCloseClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will close the view PRT form.
         *  
         *****************************************************************************************/
        private void BtnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }


        /******************************************************************************************
         * 
         * Name:        BtnPrintClick
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: This method will print the PRT contents in landscape.
         *  
         *****************************************************************************************/
        private void BtnPrintClick(object sender, EventArgs e)
        {

        }


    }
}
