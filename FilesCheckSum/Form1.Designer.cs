using System.Data.Common;

namespace FilesCheckSum
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            lvAllowedFiles = new DoubleBufferedListView();
            ColumnHash = new ColumnHeader();
            ColumnOrigin = new ColumnHeader();
            columnSize = new ColumnHeader();
            columnModified = new ColumnHeader();
            contextMenuStrip3 = new ContextMenuStrip(components);
            toolStripMenuItemRun = new ToolStripMenuItem();
            toolStripMenuItemProps = new ToolStripMenuItem();
            toolStripMenuItemDisallow = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItemPlay = new ToolStripMenuItem();
            toolStripMenuItemAdd = new ToolStripMenuItem();
            toolStripMenuItemRemove = new ToolStripMenuItem();
            toolStripMenuItemProperties = new ToolStripMenuItem();
            label2 = new Label();
            label3 = new Label();
            lvFoundFiles = new DoubleBufferedListView();
            columnHeader1 = new ColumnHeader();
            button3 = new Button();
            label4 = new Label();
            label5 = new Label();
            button4 = new Button();
            pictureBox1 = new PictureBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label6 = new Label();
            button5 = new Button();
            label7 = new Label();
            button6 = new Button();
            toolTip1 = new ToolTip(components);
            button7 = new Button();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            pictureBox2 = new PictureBox();
            textBox2 = new TextBox();
            splitContainer1 = new SplitContainer();
            contextMenuStrip3.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = Color.GhostWhite;
            textBox1.Font = new Font("Segoe UI", 15F);
            textBox1.ForeColor = Color.FromArgb(128, 128, 255);
            textBox1.Location = new Point(21, 9);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(617, 34);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackgroundImage = Properties.Resources.folder_scale_200;
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(743, 6);
            button1.Name = "button1";
            button1.Size = new Size(45, 40);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.BackgroundImage = Properties.Resources.right_arrow;
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(644, 26);
            button2.Name = "button2";
            button2.Size = new Size(114, 189);
            button2.TabIndex = 2;
            toolTip1.SetToolTip(button2, "Start Search");
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // lvAllowedFiles
            // 
            lvAllowedFiles.Activation = ItemActivation.OneClick;
            lvAllowedFiles.AllowColumnReorder = true;
            lvAllowedFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvAllowedFiles.AutoArrange = false;
            lvAllowedFiles.BackColor = Color.Honeydew;
            lvAllowedFiles.Columns.AddRange(new ColumnHeader[] { ColumnHash, ColumnOrigin, columnSize, columnModified });
            lvAllowedFiles.ContextMenuStrip = contextMenuStrip3;
            lvAllowedFiles.ForeColor = Color.DarkOliveGreen;
            lvAllowedFiles.FullRowSelect = true;
            lvAllowedFiles.GridLines = true;
            lvAllowedFiles.Location = new Point(0, 24);
            lvAllowedFiles.Name = "lvAllowedFiles";
            lvAllowedFiles.Size = new Size(320, 530);
            lvAllowedFiles.TabIndex = 4;
            lvAllowedFiles.UseCompatibleStateImageBehavior = false;
            lvAllowedFiles.View = View.Details;
            // 
            // ColumnHash
            // 
            ColumnHash.Text = "Hash";
            ColumnHash.Width = 70;
            // 
            // ColumnOrigin
            // 
            ColumnOrigin.Text = "Origin";
            ColumnOrigin.Width = 500;
            // 
            // columnSize
            // 
            columnSize.Text = "Size";
            // 
            // columnModified
            // 
            columnModified.Text = "Modified";
            columnModified.Width = 150;
            // 
            // contextMenuStrip3
            // 
            contextMenuStrip3.Items.AddRange(new ToolStripItem[] { toolStripMenuItemRun, toolStripMenuItemProps, toolStripMenuItemDisallow });
            contextMenuStrip3.Name = "contextMenuStrip3";
            contextMenuStrip3.Size = new Size(128, 70);
            // 
            // toolStripMenuItemRun
            // 
            toolStripMenuItemRun.Name = "toolStripMenuItemRun";
            toolStripMenuItemRun.Size = new Size(127, 22);
            toolStripMenuItemRun.Text = "&Run";
            toolStripMenuItemRun.Click += toolStripMenuItemRun_Click;
            // 
            // toolStripMenuItemProps
            // 
            toolStripMenuItemProps.Name = "toolStripMenuItemProps";
            toolStripMenuItemProps.Size = new Size(127, 22);
            toolStripMenuItemProps.Text = "&Properties";
            toolStripMenuItemProps.Click += toolStripMenuItemProps_Click;
            // 
            // toolStripMenuItemDisallow
            // 
            toolStripMenuItemDisallow.Name = "toolStripMenuItemDisallow";
            toolStripMenuItemDisallow.Size = new Size(127, 22);
            toolStripMenuItemDisallow.Text = "&Remove";
            toolStripMenuItemDisallow.Click += toolStripMenuItemDisallow_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItemPlay, toolStripMenuItemAdd, toolStripMenuItemRemove, toolStripMenuItemProperties });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(128, 92);
            contextMenuStrip1.ItemClicked += contextMenuStrip1_ItemClicked;
            // 
            // toolStripMenuItemPlay
            // 
            toolStripMenuItemPlay.Name = "toolStripMenuItemPlay";
            toolStripMenuItemPlay.Size = new Size(127, 22);
            toolStripMenuItemPlay.Text = "&Run";
            // 
            // toolStripMenuItemAdd
            // 
            toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            toolStripMenuItemAdd.Size = new Size(127, 22);
            toolStripMenuItemAdd.Text = "&Add";
            // 
            // toolStripMenuItemRemove
            // 
            toolStripMenuItemRemove.Name = "toolStripMenuItemRemove";
            toolStripMenuItemRemove.Size = new Size(127, 22);
            toolStripMenuItemRemove.Text = "Remove";
            // 
            // toolStripMenuItemProperties
            // 
            toolStripMenuItemProperties.Name = "toolStripMenuItemProperties";
            toolStripMenuItemProperties.Size = new Size(127, 22);
            toolStripMenuItemProperties.Text = "P&roperties";
            // 
            // label2
            // 
            label2.BackColor = Color.Navy;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(192, 192, 255);
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(320, 22);
            label2.TabIndex = 5;
            label2.Text = "Allowed Files";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.Green;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(192, 255, 192);
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(439, 21);
            label3.TabIndex = 7;
            label3.Text = "Checked Files";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lvFoundFiles
            // 
            lvFoundFiles.Activation = ItemActivation.OneClick;
            lvFoundFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvFoundFiles.BackColor = Color.Lavender;
            lvFoundFiles.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            lvFoundFiles.ContextMenuStrip = contextMenuStrip1;
            lvFoundFiles.ForeColor = Color.MidnightBlue;
            lvFoundFiles.FullRowSelect = true;
            lvFoundFiles.GridLines = true;
            lvFoundFiles.HotTracking = true;
            lvFoundFiles.HoverSelection = true;
            lvFoundFiles.Location = new Point(0, 24);
            lvFoundFiles.Name = "lvFoundFiles";
            lvFoundFiles.Size = new Size(439, 531);
            lvFoundFiles.TabIndex = 6;
            lvFoundFiles.UseCompatibleStateImageBehavior = false;
            lvFoundFiles.View = View.Details;
            lvFoundFiles.MouseDoubleClick += lvFoundFiles_MouseDoubleClick_1;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "File Path";
            columnHeader1.Width = 450;
            // 
            // button3
            // 
            button3.BackgroundImage = Properties.Resources.add;
            button3.BackgroundImageLayout = ImageLayout.Zoom;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.CheckedBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(125, 106);
            button3.Name = "button3";
            button3.Size = new Size(30, 28);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 7F);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(21, 115);
            label4.Name = "label4";
            label4.Size = new Size(109, 12);
            label4.TabIndex = 5;
            label4.Text = "Add files ----------------";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 7F);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(21, 86);
            label5.Name = "label5";
            label5.Size = new Size(109, 12);
            label5.TabIndex = 5;
            label5.Text = "Add folder (recursive) --";
            // 
            // button4
            // 
            button4.BackgroundImage = Properties.Resources.add;
            button4.BackgroundImageLayout = ImageLayout.Zoom;
            button4.Cursor = Cursors.Hand;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.CheckedBackColor = Color.Transparent;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(125, 77);
            button4.Name = "button4";
            button4.Size = new Size(30, 28);
            button4.TabIndex = 8;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Enabled = false;
            pictureBox1.Image = Properties.Resources._13_31_09_736_512;
            pictureBox1.Location = new Point(286, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(228, 147);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(611, 180);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 10;
            // 
            // button5
            // 
            button5.BackgroundImage = Properties.Resources.add;
            button5.BackgroundImageLayout = ImageLayout.Zoom;
            button5.Cursor = Cursors.Hand;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.CheckedBackColor = Color.Transparent;
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(125, 135);
            button5.Name = "button5";
            button5.Size = new Size(30, 28);
            button5.TabIndex = 12;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 7F);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(21, 144);
            label7.Name = "label7";
            label7.Size = new Size(106, 12);
            label7.TabIndex = 11;
            label7.Text = "Add from CSV ---------";
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.BackgroundImage = Properties.Resources.stop_down;
            button6.BackgroundImageLayout = ImageLayout.Zoom;
            button6.Cursor = Cursors.Hand;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button6.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Location = new Point(644, 78);
            button6.Name = "button6";
            button6.Size = new Size(93, 113);
            button6.TabIndex = 13;
            toolTip1.SetToolTip(button6, "Stop Searching");
            button6.UseVisualStyleBackColor = true;
            button6.Visible = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackgroundImage = Properties.Resources.stop_down;
            button7.BackgroundImageLayout = ImageLayout.Zoom;
            button7.Cursor = Cursors.Hand;
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button7.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Location = new Point(286, 82);
            button7.Name = "button7";
            button7.Size = new Size(93, 113);
            button7.TabIndex = 15;
            toolTip1.SetToolTip(button7, "Stop Adding (does not revert files already added)");
            button7.UseVisualStyleBackColor = true;
            button7.Visible = false;
            button7.Click += button7_Click;
            // 
            // backgroundWorker2
            // 
            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += backgroundWorker2_RunWorkerCompleted;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Enabled = false;
            pictureBox2.Image = Properties.Resources._13_34_17_270_512;
            pictureBox2.Location = new Point(-29, -20);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(423, 350);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox2.BackColor = Color.GhostWhite;
            textBox2.Font = new Font("Segoe UI", 15F);
            textBox2.ForeColor = Color.FromArgb(128, 128, 255);
            textBox2.Location = new Point(653, 9);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(75, 34);
            textBox2.TabIndex = 16;
            textBox2.Text = "*.*";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(18, 205);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lvAllowedFiles);
            splitContainer1.Panel1.Controls.Add(label2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvFoundFiles);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Size = new Size(763, 555);
            splitContainer1.SplitterDistance = 320;
            splitContainer1.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 772);
            Controls.Add(button7);
            Controls.Add(splitContainer1);
            Controls.Add(textBox2);
            Controls.Add(label6);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button6);
            Controls.Add(pictureBox2);
            Controls.Add(button5);
            Controls.Add(label7);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label5);
            Controls.Add(label4);
            ForeColor = Color.Navy;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Check Files - Checksum List";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            contextMenuStrip3.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private FilesCheckSum.DoubleBufferedListView lvAllowedFiles;
        private Label label2;
        private Label label3;
        private FilesCheckSum.DoubleBufferedListView lvFoundFiles;
        private ColumnHeader ColumnOrigin;
        private ColumnHeader ColumnHash;
        private Button button3;
        private Label label4;
        private Label label5;
        private Button button4;
        private ColumnHeader columnHeader1;
        private PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label6;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItemPlay;
        private ToolStripMenuItem toolStripMenuItemAdd;
        private ToolStripMenuItem toolStripMenuItemProperties;
        private ToolStripMenuItem toolStripMenuItemRemove;
        private Button button5;
        private Label label7;
        private Button button6;
        private ToolTip toolTip1;
        private ContextMenuStrip contextMenuStrip3;
        private ToolStripMenuItem toolStripMenuItemRun;
        private ToolStripMenuItem toolStripMenuItemProps;
        private ToolStripMenuItem toolStripMenuItemDisallow;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private PictureBox pictureBox2;
        private Button button7;
        private TextBox textBox2;
        private SplitContainer splitContainer1;
        private ColumnHeader columnSize;
        private ColumnHeader columnModified;
    }
}
