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
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            lvAllowedFiles = new DoubleBufferedListView();
            ColumnHash = new ColumnHeader();
            ColumnOrigin = new ColumnHeader();
            label2 = new Label();
            label3 = new Label();
            lvFoundFiles = new DoubleBufferedListView();
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
            button5 = new Button();
            label7 = new Label();
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
            lvAllowedFiles.Columns.AddRange(new ColumnHeader[] { ColumnHash, ColumnOrigin });
            lvAllowedFiles.ForeColor = Color.DarkOliveGreen;
            lvAllowedFiles.GridLines = true;
            lvAllowedFiles.Location = new Point(18, 201);
            lvAllowedFiles.Name = "lvAllowedFiles";
            lvAllowedFiles.Size = new Size(378, 559);
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 181);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 5;
            label2.Text = "Allowed Files:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(411, 181);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 7;
            label3.Text = "Checked Files:";
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
            lvFoundFiles.MouseDoubleClick += lvFoundFiles_MouseDoubleClick_1;
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
            // button3
            // 
            button3.BackgroundImage = Properties.Resources.add;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
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
            label5.Location = new Point(21, 86);
            label5.Name = "label5";
            label5.Size = new Size(109, 12);
            label5.TabIndex = 5;
            label5.Text = "Add folder (recursive) --";
            // 
            // button4
            // 
            button4.BackgroundImage = Properties.Resources.add;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.Location = new Point(125, 77);
            button4.Name = "button4";
            button4.Size = new Size(30, 28);
            button4.TabIndex = 8;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Enabled = false;
            pictureBox1.Image = Properties.Resources.running;
            pictureBox1.Location = new Point(494, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(144, 125);
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
            button5.BackgroundImageLayout = ImageLayout.Stretch;
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
            label7.Location = new Point(21, 144);
            label7.Name = "label7";
            label7.Size = new Size(106, 12);
            label7.TabIndex = 11;
            label7.Text = "Add from CSV ---------";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 772);
            Controls.Add(button5);
            Controls.Add(label7);
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
            FormClosing += Form1_FormClosing;
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
    }
}
