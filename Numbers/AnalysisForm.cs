using System;
using System.Collections;
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
    public partial class AnalysisForm : Form
    {
        public string FileName = string.Empty;
        public ArrayList list = new ArrayList();
        public bool Modified = false;

        public AnalysisForm()
        {
            InitializeComponent();
        }

        public void LoadFile(string fileName)
        {
            FileName = fileName;
            this.Text = Path.GetFileNameWithoutExtension(fileName);
            list.Clear();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string s = sr.ReadLine();
                while (s != null)
                {
                    string[] w = s.Split(',');
                    NumberItem ni = new NumberItem();
                    ni.Set(w);
                    list.Add(ni);
                    s = sr.ReadLine();
                }
            }

            RefreshList();
        }

        public void SaveFile()
        {
            if (FileName.Length == 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileName = sfd.FileName;
                }
            }

            if (FileName.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    foreach (NumberItem ni in list)
                    {
                        sw.WriteLine(ni.CSV);
                    }
                }
            }
        }

        public void RefreshList()
        {
            listBox1.Items.Clear();

            listBox1.BeginUpdate();
            foreach (NumberItem ni in list)
            {
                listBox1.Items.Add(ni);
            }
            listBox1.EndUpdate();
        }

        private void AnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
                SaveFile();
        }

        /// <summary>
        /// analysis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            int[,] ranges = new int[31,2];
            int sum = 0;

            // initialize
            for (int i = 0; i < 31; i++)
            {
                ranges[i, 0] = -1;
                ranges[i, 1] = -1;
            }

            // calculation
            foreach (NumberItem ni in list)
            {
                sum = 0;
                for (int j = 0; j < ni.Count; j++)
                {
                    int d = ni.Number(j);
                    sum += d;
                    if (ranges[j, 0] == -1 || ranges[j, 0] > d)
                        ranges[j, 0] = d;
                    if (ranges[j, 1] == -1 || ranges[j, 1] < d)
                        ranges[j, 1] = d;
                }
                if (ni.Count > 0)
                {
                    if (ranges[30, 0] == -1 || ranges[30, 0] > sum)
                        ranges[30, 0] = sum;
                    if (ranges[30, 1] == -1 || ranges[30, 1] < sum)
                        ranges[30, 1] = sum;
                }
            }

            // results show
            for (int i = 0; i < 30; i++)
            {
                if (ranges[i, 0] != -1 && ranges[i, 1] != -1)
                    listBox2.Items.Add(string.Format("RANGE[{0}] {1,2} - {2,2}", i + 1, ranges[i, 0], ranges[i, 1]));
            }
            if (ranges[30, 0] != -1 && ranges[30, 1] != -1)
                listBox2.Items.Add(string.Format("SUM_RANG {0,2} - {1,2}", ranges[30, 0], ranges[30, 1]));

            // ---- num position prob
            double[] nextProb = new double[100];
            for(int a= 0; a < 100; a++)
                nextProb[a] = 0.0;
            int i2 = list.Count - 1;
            while (i2 >= 0)
            {
                ArrayList ratios = new ArrayList();
                NumberItem ni = list[i2] as NumberItem;
                foreach (int b in ni.nums)
                {
                    ratios.Add(nextProb[b]);
                    nextProb[b] += 1.0;
                }
                NormalizeDoubleArray(nextProb);
                ratios.Sort();
                richTextBox1.AppendText(DoubleArrayListToString(ratios) + "\n");
                i2--;
            }

            // -- common with prev
            richTextBox2.Text = "";
            for (int i = 0; i < list.Count - 1; i++)
            {
                ArrayList arr = new ArrayList();
                for (int b = i + 1; b < list.Count; b++)
                {
                    arr.Add((list[i] as NumberItem).CountOfIntersect2(list[b] as NumberItem));
                }
                richTextBox2.AppendText(ArrayListToString(arr) + "\n");
            }

            // - distances
            richTextBox3.Text = "";
            for (int i = 0; i < list.Count - 1; i++)
            {
                ArrayList arr = new ArrayList();
                for (int b = i + 1; b < list.Count; b++)
                {
                    arr.Add((list[i] as NumberItem).TotalDistance(list[b] as NumberItem));
                }
                richTextBox3.AppendText(ArrayListToString(arr) + "\n");
            }

            // derivation
            richTextBox4.Text = "";
            for (int i = 0; i < list.Count - 1; i++)
            {
                ArrayList arr = new ArrayList();
                for (int b = i + 1; b < list.Count; b++)
                {
                    arr.Add((list[i] as NumberItem).MaxDistance(list[b] as NumberItem));
                }
                richTextBox4.AppendText(ArrayListToString(arr) + "\n");
            }

            // kolko cisel bolo teraz tahanych, ktore neboli pouzite za poslednych X tahov
            richTextBox5.Text = "--- kolko cisel bolo teraz tahanych, ktore neboli pouzite za poslednych X tahov ---\n\n";
            int[] dar = new int[81];
            for (int i = 0; i < (list.Count - 1); i++)
            {
                NumberItem mi = list[i] as NumberItem;
                ArrayList arri = new ArrayList();
                // clearing nonunique counters
                for (int a = 0; a <= 80; a++)
                    dar[a] = 0;
                for (int j = i+1; j < list.Count; j++)
                {
                    NumberItem ni = list[j] as NumberItem;
                    // adding NumberItem to nonunique counters
                    foreach (int z in ni.nums)
                    {
                        dar[z]++;
                    }
                    // counting unique numbers
                    int cn = 0;
                    foreach(int ja in mi.nums)
                    {
                        if (dar[ja] == 0)
                            cn++;
                    }
                    arri.Add(cn);
                }
                richTextBox5.AppendText(ArrayListToString(arri) + "\n");
            }

            richTextBox5.AppendText("\n\n-- kolko cisiel bolo obsadenych poslednymi X tahmi---\n\n\n");
            for (int i = 0; i < (list.Count - 1); i++)
            {
                NumberItem mi = list[i] as NumberItem;
                ArrayList arri = new ArrayList();
                // clearing nonunique counters
                for (int a = 0; a <= 80; a++)
                    dar[a] = 0;
                for (int j = i + 1; j < list.Count; j++)
                {
                    NumberItem ni = list[j] as NumberItem;
                    // adding NumberItem to nonunique counters
                    foreach (int z in ni.nums)
                    {
                        dar[z]++;
                    }
                    // counting unique numbers
                    int cn = 0;
                    foreach (int ja in dar)
                    {
                        if (ja > 0)
                            cn++;
                    }
                    arri.Add(cn);
                }
                richTextBox5.AppendText(ArrayListToString(arri) + "\n");
            }

            richTextBox5.AppendText("\n\n-- volne cisla---\n\n\n");
            // clearing nonunique counters
            for (int a = 0; a <= 80; a++)
                dar[a] = 0;
            for (int i = 0; i < (list.Count - 1); i++)
            {
                NumberItem mi = list[i] as NumberItem;
                ArrayList arri = new ArrayList();
                // adding NumberItem to nonunique counters
                foreach (int z in mi.nums)
                {
                    dar[z]++;
                }
                // counting unique numbers
                for (int za = 1; za < 81; za++)
                {
                    if (dar[za] == 0)
                        arri.Add(za);
                }
                richTextBox5.AppendText(ArrayListToString(arri) + "\n");
            }

        }

        public void DivideDoubleArray(double[] arr, double ratio)
        {
            if (ratio > 0.0)
            {
                for (int b = 0; b < arr.Length; b++)
                {
                    arr[b] = arr[b] / ratio;
                }
            }
        }

        public void NormalizeDoubleArray(double[] arr)
        {
            double max = 0.0;
            for (int a = 0; a < arr.Length; a++)
            {
                if (max < arr[a])
                    max = arr[a];
            }
            DivideDoubleArray(arr, max);
        }
        public string DoubleArrayListToString(ArrayList arr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (double b in arr)
            {
                sb.AppendFormat("{0},", Convert.ToInt32(b*100));
            }
            return sb.ToString();
        }

        public string ArrayIntToString(int [] arr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int b in arr)
            {
                sb.AppendFormat("{0},", b);
            }
            return sb.ToString();
        }

        public string ArrayListToString(ArrayList arr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (object b in arr)
            {
                sb.AppendFormat("{0},", b.ToString());
            }
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNumbers an = new AddNumbers();

            if (an.ShowDialog() == DialogResult.OK)
            {
                NumberItem ni = new NumberItem();
                ni.Set(an.Numbers);
                list.Add(ni);
                Modified = true;
            }

            RefreshList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete item?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ArrayList arr = new ArrayList();
                foreach (object o in listBox1.SelectedItems)
                {
                    arr.Add(o);
                }
                foreach (object o in arr)
                {
                    listBox1.Items.Remove(o);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnalysisForm af = new AnalysisForm();

            af.MdiParent = MdiParent;
            foreach (NumberItem ni in list)
            {
                af.AddNumbers(ni.Derivation(1));
            }
            af.RefreshList();
            af.Show();
        }

        public void AddNumbers(NumberItem ni)
        {
            list.Add(ni);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AnalysisForm af = new AnalysisForm();

            af.MdiParent = MdiParent;
            foreach (NumberItem ni in list)
            {
                af.AddNumbers(ni.Derivation(2));
            }
            af.RefreshList();
            af.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AnalysisForm af = new AnalysisForm();

            af.MdiParent = MdiParent;
            foreach (NumberItem ni in list)
            {
                af.AddNumbers(ni.Derivation(3));
            }
            af.RefreshList();
            af.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AnalysisForm af = new AnalysisForm();

            af.MdiParent = MdiParent;
            foreach (NumberItem ni in list)
            {
                af.AddNumbers(ni.Derivation(4));
            }
            af.RefreshList();
            af.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            AnalysisForm af = new AnalysisForm();

            af.MdiParent = MdiParent;
            foreach (NumberItem ni in list)
            {
                af.AddNumbers(ni.Derivation(5));
            }
            af.RefreshList();
            af.Show();
        }


    }
}
