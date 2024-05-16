﻿using System.Data.Common;

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
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            lvAllowedFiles = new ListView();
            ColumnName = new ColumnHeader();
            ColumnHash = new ColumnHeader();
            label2 = new Label();
            label3 = new Label();
            lvFoundFiles = new ListView();
            columnHeader1 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItemPlay = new ToolStripMenuItem();
            toolStripMenuItemAdd = new ToolStripMenuItem();
            toolStripMenuItemRemove = new ToolStripMenuItem();
            toolStripMenuItemProperties = new ToolStripMenuItem();
            button3 = new Button();
            label4 = new Label();
            label5 = new Label();
            button4 = new Button();
            pictureBox1 = new PictureBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label6 = new Label();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Font = new Font("Segoe UI", 15F);
            textBox1.Location = new Point(18, 31);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(703, 34);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackgroundImage = Properties.Resources.Folder;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(727, 24);
            button1.Name = "button1";
            button1.Size = new Size(56, 49);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top;
            button2.BackgroundImage = Properties.Resources.right_arrow;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Location = new Point(363, 94);
            button2.Name = "button2";
            button2.Size = new Size(75, 63);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(385, 76);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 3;
            label1.Text = "Start";
            // 
            // lvAllowedFiles
            // 
            lvAllowedFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lvAllowedFiles.AutoArrange = false;
            lvAllowedFiles.BackColor = Color.Honeydew;
            lvAllowedFiles.Columns.AddRange(new ColumnHeader[] { ColumnName, ColumnHash });
            lvAllowedFiles.ForeColor = Color.DarkOliveGreen;
            lvAllowedFiles.GridLines = true;
            lvAllowedFiles.Location = new Point(18, 201);
            lvAllowedFiles.Name = "lvAllowedFiles";
            lvAllowedFiles.Size = new Size(378, 559);
            lvAllowedFiles.TabIndex = 4;
            lvAllowedFiles.UseCompatibleStateImageBehavior = false;
            lvAllowedFiles.View = View.Details;
            // 
            // ColumnName
            // 
            ColumnName.Text = "Name";
            ColumnName.Width = 200;
            // 
            // ColumnHash
            // 
            ColumnHash.Text = "Hash";
            ColumnHash.Width = 180;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 181);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 5;
            label2.Text = "Allowed Files List";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(411, 181);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 7;
            label3.Text = "Checked Files";
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
            lvFoundFiles.Location = new Point(410, 201);
            lvFoundFiles.Name = "lvFoundFiles";
            lvFoundFiles.Size = new Size(378, 559);
            lvFoundFiles.TabIndex = 6;
            lvFoundFiles.UseCompatibleStateImageBehavior = false;
            lvFoundFiles.View = View.Details;
            lvFoundFiles.ItemActivate += lvFoundFiles_ItemActivate;
            lvFoundFiles.MouseDoubleClick += lvFoundFiles_MouseDoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "File Path";
            columnHeader1.Width = 450;
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
            toolStripMenuItemPlay.Click += toolStripMenuItemPlay_Click;
            // 
            // toolStripMenuItemAdd
            // 
            toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            toolStripMenuItemAdd.Size = new Size(127, 22);
            toolStripMenuItemAdd.Text = "&Add";
            toolStripMenuItemAdd.Click += toolStripMenuItemAdd_Click;
            // 
            // toolStripMenuItemRemove
            // 
            toolStripMenuItemRemove.Name = "toolStripMenuItemRemove";
            toolStripMenuItemRemove.Size = new Size(127, 22);
            toolStripMenuItemRemove.Text = "Remove";
            toolStripMenuItemRemove.Click += toolStripMenuItemRemove_Click;
            // 
            // toolStripMenuItemProperties
            // 
            toolStripMenuItemProperties.Name = "toolStripMenuItemProperties";
            toolStripMenuItemProperties.Size = new Size(127, 22);
            toolStripMenuItemProperties.Text = "P&roperties";
            toolStripMenuItemProperties.Click += toolStripMenuItemProperties_Click;
            // 
            // button3
            // 
            button3.BackgroundImage = Properties.Resources.add;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.Location = new Point(366, 173);
            button3.Name = "button3";
            button3.Size = new Size(30, 28);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(315, 181);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 5;
            label4.Text = "Add files";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(146, 180);
            label5.Name = "label5";
            label5.Size = new Size(121, 15);
            label5.TabIndex = 5;
            label5.Text = "Add folder (recursive)";
            // 
            // button4
            // 
            button4.BackgroundImage = Properties.Resources.add;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.Location = new Point(263, 173);
            button4.Name = "button4";
            button4.Size = new Size(30, 28);
            button4.TabIndex = 8;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Enabled = false;
            pictureBox1.Image = Properties.Resources.running;
            pictureBox1.Location = new Point(195, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(415, 174);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 772);
            Controls.Add(label6);
            Controls.Add(pictureBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(lvFoundFiles);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(lvAllowedFiles);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "Form1";
            Load += Form1_Load;
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Label label1;
        private ListView lvAllowedFiles;
        private Label label2;
        private Label label3;
        private ListView lvFoundFiles;
        private ColumnHeader ColumnName;
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
    }
}
