using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void butAddCancel_Click(object sender, EventArgs e)
        {
            groupAdd.Visible = false;
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            groupAdd.Visible = true;
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            groupEdit.Visible = true; 
        }

        private void butEditCancel_Click(object sender, EventArgs e)
        {
            groupEdit.Visible = false;
        }
    }
}
