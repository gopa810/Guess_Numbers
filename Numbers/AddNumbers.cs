﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Numbers
{
    public partial class AddNumbers : Form
    {
        public AddNumbers()
        {
            InitializeComponent();
        }

        public string[] Numbers
        {
            get
            {
                return textBox1.Text.Replace(" ", "").Split(',');
            }
        }
    }
}
