﻿namespace Numbers
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAnalyseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(987, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAnalyseToolStripMenuItem,
            this.openAnalysisToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newAnalyseToolStripMenuItem
            // 
            this.newAnalyseToolStripMenuItem.Name = "newAnalyseToolStripMenuItem";
            this.newAnalyseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newAnalyseToolStripMenuItem.Text = "New Analysis";
            this.newAnalyseToolStripMenuItem.Click += new System.EventHandler(this.newAnalyseToolStripMenuItem_Click);
            // 
            // openAnalysisToolStripMenuItem
            // 
            this.openAnalysisToolStripMenuItem.Name = "openAnalysisToolStripMenuItem";
            this.openAnalysisToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openAnalysisToolStripMenuItem.Text = "Open Analysis";
            this.openAnalysisToolStripMenuItem.Click += new System.EventHandler(this.openAnalysisToolStripMenuItem_Click);
            // 
            // gToolStripMenuItem
            // 
            this.gToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createSetToolStripMenuItem});
            this.gToolStripMenuItem.Name = "gToolStripMenuItem";
            this.gToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.gToolStripMenuItem.Text = "Generate";
            // 
            // createSetToolStripMenuItem
            // 
            this.createSetToolStripMenuItem.Name = "createSetToolStripMenuItem";
            this.createSetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.createSetToolStripMenuItem.Text = "Create set";
            this.createSetToolStripMenuItem.Click += new System.EventHandler(this.createSetToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 545);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAnalyseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSetToolStripMenuItem;
    }
}

