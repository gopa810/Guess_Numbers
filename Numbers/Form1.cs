using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Numbers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newAnalyseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyseInit ai = new AnalyseInit();

            if (ai.ShowDialog() == DialogResult.OK)
            {
                AnalysisForm af = new AnalysisForm();
                af.Text = "Analysis" + ai.Number.ToString();
                af.MdiParent = this;
                af.Show();
            }
        }

        private void openAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Numbers files (*.nums)|*.nums";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AnalysisForm af = new AnalysisForm();
                af.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
                af.MdiParent = this;
                af.LoadFile(ofd.FileName);
                af.Show();
            }
        }

        private void createSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllGeneratorForm af = new AllGeneratorForm();
            af.MdiParent = this;
            af.Show();
        }
    }
}
