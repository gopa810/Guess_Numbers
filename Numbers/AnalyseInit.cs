using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Numbers
{
    public partial class AnalyseInit : Form
    {
        public AnalyseInit()
        {
            InitializeComponent();
        }

        public int Number
        {
            get
            {
                return Convert.ToInt32(numericUpDown1.Value);
            }
        }
    }
}
