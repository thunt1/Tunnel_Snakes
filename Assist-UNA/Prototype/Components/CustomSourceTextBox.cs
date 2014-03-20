using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype
{
    public partial class CustomSourceTextBox : TextBox
    {
        private int lineNum = 0;
        private Label label = new Label();
        public CustomSourceTextBox()
        {
            //Controls.Add(label);
            InitializeComponent();
            
        }

        public CustomSourceTextBox(IContainer container) 
        {

            container.Add(this);
            InitializeComponent();
            //
            //Label label = new Label();
            //label.Text = ("1");
            //container.Add(label);
        }

        private void CustomSourceTextBox_TextChanged(object sender, EventArgs e)
        {
            lineNum += 1;
            label.Text += "\n" + lineNum.ToString();
            
        }

        private void CustomSourceTextBox_Enter(object sender, EventArgs e)
        {
            this.Controls.Add(label);
        }


        
    }
}
