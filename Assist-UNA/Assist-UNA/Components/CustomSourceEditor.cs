using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Assist_UNA
{
    /**********************************************************
     * 
     * Name: CustomSourceEditor class
     * 
     * ========================================================
     * 
     * Description: This class is the custom text editor used
     *              for both the user's source code and the
     *              line numbers. It inherits from TextBox and
     *              overrides/adds some features, such as the
     *              ability for two CustomSourceEditor 
     *              components to scroll together.
     *                            
     * ========================================================         
     * 
     * Modification History
     * --------------------
     * 3/14/2014     ACA     Original version.
     *  
     **********************************************************/
    public partial class CustomSourceEditor : TextBox
    {
        
        /* Public Members */
        public Control Buddy { get; set; }      
  
        /* Private members */
        private static bool scrolling;

        /* Public methods */

        /**********************************************************
         * 
         * Name:        CustomSourceEditor
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the editor.
         *  
         **********************************************************/
        public CustomSourceEditor()
        {
            this.Multiline = true;
            this.ScrollBars = ScrollBars.Vertical;
        }


        /* Protected methods */

        /**********************************************************
         * 
         * Name:        WndProc (Override method)
         * 
         * Author(s):   Drew Aaron
         *              Reference 1
         *              
         * Input:       Scroll message
         * Return:      N/A
         * Description: This method will synchronize scrolling
         *              between two CustomSourceEditor components.
         *  
         **********************************************************/
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            
            /* Trap WM_VSCROLL message and pass to buddy. */
            if ((m.Msg == 0x115 || m.Msg == 0x20a) && !scrolling && Buddy != null && Buddy.IsHandleCreated)
            {
                scrolling = true;
                SendMessage(Buddy.Handle, m.Msg, m.WParam, m.LParam);
                scrolling = false;
            }
        }

        /* Private methods. */

        /**********************************************************
         * 
         * Name:        SendMessage (external method)
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will send the scrolling message
         *              to the CustomSourceEditor that is
         *              associated with it, if any.
         *  
         **********************************************************/
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}
