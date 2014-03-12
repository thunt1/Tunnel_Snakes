using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**********************************************************
 * 
 * Name: OptionsForm class
 * 
 * ========================================================
 * 
 * Description: This class is the options form part of the
 *              main GUI. 
 *                            
 * ========================================================         
 * 
 * Modification History
 * --------------------
 * 3/9/2014     ACA     Original version.
 * 3/9/2014     ACA     Added data members and options form
 *                      functionality. Documented code.
 *  
 **********************************************************/

namespace Assist_UNA
{
    public partial class OptionsForm : Form
    {
        
        /* Private members. */
        private int maxLines = 900;
        private int maxInstructions = 9000;
        private int maxPages = 900;
        private int maxSize = 2700;


        /* Public methods. */

        /**********************************************************
         * 
         * Name:        OptionsForm
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the options 
         *              form.
         *  
         **********************************************************/
        public OptionsForm()
        {
            InitializeComponent();      
        }


        /**********************************************************
         * 
         * Name:        GetMaxLines
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The maxLines data member (int).
         * Description: This method will return maxLines.
         *  
         **********************************************************/
        public int GetMaxLines()
        {
            return maxLines;
        }


        /**********************************************************
         * 
         * Name:        SetMaxLines
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       The current maximum number of lines (int).
         * Return:      N/A
         * Description: This method will set the maxLines data
         *              member equal to the given integer (which is
         *              usually the current maxLines in MainForm).
         *              Also sets the correct number in the max 
         *              lines text box.
         *  
         **********************************************************/
        public void SetMaxLines(int e)
        {
            maxLines = e;
            txtMaxLines.Clear();
            txtMaxLines.AppendText(Convert.ToString(e));
        }


        /**********************************************************
         * 
         * Name:        GetMaxInstructions
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The maxInstructions data member (int).
         * Description: This method will return maxInstructions.
         *  
         **********************************************************/
        public int GetMaxInstructions()
        {
            return maxInstructions;

        }


        /**********************************************************
         * 
         * Name:        SetMaxInstructions
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       The current maximum number of instructions
         *              (int).
         * Return:      N/A
         * Description: This method will set the maxInstructions
         *              data member equal to the given integer 
         *              (which is usually the current 
         *              maxInstructions in MainForm). Also sets the 
         *              correct number in the max instructions text
         *              box.
         *  
         **********************************************************/
        public void SetMaxInstructions(int e)
        {
            maxInstructions = e;
            txtMaxInstructions.Clear();
            txtMaxInstructions.AppendText(Convert.ToString(e));
        }


        /**********************************************************
         * 
         * Name:        GetMaxPages
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The maxPages data member (int).
         * Description: This method will return maxPages.
         *  
         **********************************************************/
        public int GetMaxPages()
        {
            return maxPages;
        }


        /**********************************************************
         * 
         * Name:        SetMaxPages
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       The current maximum number of pages (int).
         * Return:      N/A
         * Description: This method will set the maxPages data
         *              member equal to the given integer (which is
         *              usually the current maxPages in MainForm).
         *              Also sets the correct number in the max 
         *              pages text box.
         *  
         **********************************************************/
        public void SetMaxPages(int e)
        {
            maxPages = e;
            txtMaxPages.Clear();
            txtMaxPages.AppendText(Convert.ToString(e));
        }


        /**********************************************************
         * 
         * Name:        GetMaxSize
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      The maxSize data member (int).
         * Description: This method will return maxSize.
         *  
         **********************************************************/
        public int GetMaxSize()
        {
            return maxSize;
        }


        /**********************************************************
         * 
         * Name:        SetMaxSize
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       The current maximum file size in bytes 
         *              (int).
         * Return:      N/A
         * Description: This method will set the maxSize data
         *              member equal to the given integer (which is
         *              usually the current maxSize in MainForm).
         *              Also sets the correct number in the max 
         *              size text box.
         *  
         **********************************************************/
        public void SetMaxSize(int e)
        {
            maxSize = e;
            txtMaxSize.Clear();
            txtMaxSize.AppendText(Convert.ToString(e));
        }


        /* Private methods. */

        /**********************************************************
         * 
         * Name:        btnOptionsApply_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: Will update the data members to the values
         *              currently in their respective text boxes.
         *              Also will close the options form after.
         *  
         **********************************************************/
        private void btnOptionsApply_Click(object sender, EventArgs e)
        {
            maxLines = Convert.ToInt32(txtMaxLines.Text);
            maxInstructions = Convert.ToInt32(txtMaxInstructions.Text);
            maxPages = Convert.ToInt32(txtMaxPages.Text);
            maxSize = Convert.ToInt32(txtMaxSize.Text);
            this.Close();
        }

    }
}
