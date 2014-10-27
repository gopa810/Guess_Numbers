using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Numbers
{
    public partial class AllGeneratorForm : Form
    {
        string progText = string.Empty;
        int countTotal = 0;
        NumberItem ni = new NumberItem();
        int nums = 0;
        int[] record = null;
        int maxdig = 49;
        int gens = 0;
        ArrayList list = new ArrayList();

        public AllGeneratorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                button1.Text = "Stopping...";
                button1.Enabled = false;
                backgroundWorker1.CancelAsync();
            }
            else
            {
                button1.Text = "Stop";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            nums = Convert.ToInt32(numericUpDown1.Value);
            record = new int[nums];
            list.Clear();

            GenRecords(0, nums-1, 1, maxdig);

        }

        public void GenRecords(int indexToRecord, int maxIndex, int nFrom, int nTo)
        {
            if (backgroundWorker1.CancellationPending)
                return;
            for (int i = nFrom; i <= nTo; i++)
            {
                record[indexToRecord] = i;
                if (indexToRecord + 1 > maxIndex)
                {
                    TestRecord();
                    if (gens > 100000)
                    {
                        gens = 0;
                        backgroundWorker1.ReportProgress(1);
                    }
                }
                else
                {
                    GenRecords(indexToRecord + 1, maxIndex, i + 1, nTo);
                }
            }
        }

        public bool TestNumberInRange(int num, int start, int end)
        {
            return num >= start && num <= end;
        }
        public void TestRecord()
        {
            if (backgroundWorker1.CancellationPending)
                return;
            ni.Clear();
            foreach (int i in record)
            {
                ni.Add(i);
            }
            if (ni.Count > 0 && !ni.TestNumberInRange(0, 1, 10))
                return;
            if (ni.Count > 1 && !ni.TestNumberInRange(1, 4, 25))
                return;
            if (ni.Count > 2 && !ni.TestNumberInRange(2, 6, 26))
                return;
            if (ni.Count > 3 && !ni.TestNumberInRange(3, 17, 31))
                return;
            if (ni.Count > 4 && !ni.TestNumberInRange(4, 22, 35))
                return;
            //if (ni.Count > 5 && !ni.TestNumberInRange(5, 32, 49))
            //    return;
            if (ni.Count == 6)
            {
                if (ni.Number(5) - ni.Number(0) < 11)
                    return;
            }
            if (!TestNumberInRange(ni.Sum(), 68, 130))
                return;

            NumberItem ni2 = ni.Derivation(1);

            NumberItem ni3 = ni2.Derivation(1);

            if (ni3.CountOfLessThan(0) != 2)
                return;

            lock (progText)
            {
                progText = ni.ToString();
            }
            list.Add(ni.Copy());
            gens++;
            countTotal++;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = progText;
            label2.Text = countTotal.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = string.Empty;
            button1.Enabled = true;
            button1.Text = "Start";

            /*listBox1.BeginUpdate();
            foreach (string nis in list)
            {
                listBox1.Items.Add(nis);
            }
            listBox1.EndUpdate();*/

            listBox1.Items.Add("Generated " + list.Count + " sequences");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName))
            {
                foreach (NumberItem ni in list)
                {
                    sw.WriteLine(ni.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy)
            {
                backgroundWorker2.CancelAsync();
                button3.Enabled = false;
            }
            else
            {
                backgroundWorker2.RunWorkerAsync();
                button3.Text = "Cancel reordering";
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList arr = list;
            list = new ArrayList();
            Random r = new Random();
            foreach (NumberItem ni in arr)
            {
                ni.mark = false;
            }
            while (backgroundWorker2.CancellationPending == false)
            {
                int i = r.Next(arr.Count);
                NumberItem ni = arr[i] as NumberItem;
                if (ni.mark == false)
                {
                    list.Add(ni);
                    ni.mark = true;
                }
            }
            foreach (NumberItem ni in arr)
            {
                if (ni.mark == false)
                {
                    list.Add(ni);
                }
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button3.Text = "Reorder";
            button3.Enabled = true;
            listBox1.Items.Add("Reordered " + list.Count + " items");
        }
    }
}
