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
 * Name: AboutForm class
 * 
 * ========================================================
 * 
 * Description: This class is the about form part of the
 *              main GUI. 
 *                            
 * ========================================================         
 * 
 * Modification History
 * --------------------
 * 3/9/2014     ACA     Original version.
 *  
 **********************************************************/
namespace Assist_UNA
{
    public partial class AboutForm : Form
    {
       
        /* Public methods. */

        /**********************************************************
         * 
         * Name:        OptionsForm
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the about 
         *              form.
         *  
         **********************************************************/
        public AboutForm()
        {
            InitializeComponent();
        }


        /* Private Methods */

        /**********************************************************
         * 
         * Name:        btnAboutClose_Click
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       User click event
         * Return:      N/A
         * Description: Will close the about form.
         *  
         **********************************************************/
        private void btnAboutClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
