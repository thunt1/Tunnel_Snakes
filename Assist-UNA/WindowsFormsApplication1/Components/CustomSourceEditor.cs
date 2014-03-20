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
    /**********************************************************************************************
     * 
     * Name: CustomSourceEditor class
     * 
     * ============================================================================================
     * 
     * Description: This class is the custom text editor used
     *              for both the user's source code and the
     *              line numbers. It inherits from TextBox and
     *              overrides/adds some features, such as the
     *              ability for two CustomSourceEditor 
     *              components to scroll together.
     *                            
     * ============================================================================================       
     * 
     * Modification History
     * --------------------
     * 03/14/2014   ACA     Original version.
     * 03/19/2014   ACA     Version 2, richtexbox.
     *  
     *********************************************************************************************/
    public partial class CustomSourceEditor : RichTextBox
    {
        
        /* Constants */
        const int EM_SETZOOM = 0x04E1;
        const int WM_MOUSEWHEEL = 0x020A;

        /* Public methods */

        /******************************************************************************************
         * 
         * Name:        CustomSourceEditor
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method will initialize the editor.
         *  
         *****************************************************************************************/
        public CustomSourceEditor()
        {
        }


        /******************************************************************************************
         * 
         * Name:        SendMessage (external)
         * 
         * Author(s):   Drew Aaron
         *              Reference 1
         *              
         * Input:       Mouse wheel scroll.
         * Return:      N/A
         * Description: This method will override the mouse wheel scroll message.
         *  
         *****************************************************************************************/
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]

        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        /* Protected methods. */

        /******************************************************************************************
         * 
         * Name:        SendMessage (external)
         * 
         * Author(s):   Drew Aaron
         *              
         * Input:       Mouse wheel scroll.
         * Return:      N/A
         * Description: This method will override the mouse wheel scroll message.
         *  
         *****************************************************************************************/
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            /* Override if the message is the mouse wheel while holding control. */
            if (m.Msg == WM_MOUSEWHEEL)
            {
                if (Control.ModifierKeys == Keys.Control)
                    SendMessage(this.Handle, EM_SETZOOM, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
