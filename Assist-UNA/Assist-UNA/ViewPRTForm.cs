using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assist_UNA
{
    public partial class ViewPRTForm : Form
    {
        /* Private members. */
        private string fileName = "TRY.PRT";

        /* Public methods. */

        public ViewPRTForm()
        {
            InitializeComponent();
        }

        public string GetFileName()
        {
            return fileName;
        }

        public void SetID(string e)
        {
            fileName = e;
        }
    }
}
