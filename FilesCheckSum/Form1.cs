using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace FilesCheckSum
{
    public partial class Form1 : Form
    {
        private readonly List<FileInfo> _fileList = [];
        private bool _stopSearch = false;
        private static readonly string _filePath = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "FileList.csv");

        public Form1()
        {
            InitializeComponent();
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "");
            }
            string[] lines = Properties.Settings.Default.AllowedFilesCsv.Split(Environment.NewLine,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (lines.Length > 0)
            {
                string[] parts = lines[0].Split(',');
                if (Clean(parts[0]) == "hash")
                {
                    lines = lines.Skip(1).ToArray();
                }
                foreach (string line in lines)
                {
                    parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        var fi = new FileInfo
                        {
                            Hash = Clean(parts[0]),
                            Origin = Clean(parts[1]),
                            Size = long.Parse(Clean(parts[2])),
                            LastModified = DateTime.Parse(Clean(parts[3])),
                        };
                        _fileList.Add(fi);
                    }
                }
            }

            string[] paths = Properties.Settings.Default.Path.Split(',',
               StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (paths.Length > 0)
            {
                foreach (string path in paths)
                {
                    AddSearchPath(path);
                }
            }
            RefreshPathButtons(paths);
        }

        private void RefreshPathButtons(string[]? paths = null)
        {
            paths ??= Properties.Settings.Default.Path.Split(',',
               StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            this.flowLayoutPanel1.Controls.Clear();
            foreach (string path in paths)
            {
                Button b = new() { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Text = path };
                b.Click += (s, e) => OpenWithDefaultProgram(path);
                toolTip1.SetToolTip(b, "Open " + path);
                b.ContextMenuStrip = contextMenuStrip2;
                this.flowLayoutPanel1.Controls.Add(b);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox2.Text = Properties.Settings.Default.Filter;
            LoadAllowedListView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllowedList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openFileDialog = new();
            {
                var result = openFileDialog.ShowDialog();
                if (openFileDialog.SelectedPath.Length > 0)
                {
                    AddSearchPath(openFileDialog.SelectedPath);
                    RefreshPathButtons();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new()
            {
                Multiselect = true,
                CheckFileExists = true,
                DefaultExt = ".mp3",
                Title = "Choose Files",
                Filter = "\"mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*\""
            };
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileNames.Length > 0)
                {
                    var files = openFileDialog.FileNames;
                    lvAllowedFiles.SuspendLayout();
                    pictureBox2.Enabled = true;
                    pictureBox2.Visible = true;
                    this.button7.Visible = true;
                    backgroundWorker2.RunWorkerAsync(files);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Filter = this.textBox2.Text;
            Properties.Settings.Default.Save();

            using FolderBrowserDialog openFileDialog = new();
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.SelectedPath.Length > 0)
                {
                    lvAllowedFiles.SuspendLayout();
                    pictureBox2.Enabled = true;
                    pictureBox2.Visible = true;

                    this.button7.Visible = true;
                    var files = Directory.GetFiles(openFileDialog.SelectedPath,
                        Properties.Settings.Default.Filter,
                        SearchOption.AllDirectories);
                    backgroundWorker2.RunWorkerAsync(files);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Filter = this.textBox2.Text;
            Properties.Settings.Default.Save();
            List<string> files = [];
            foreach (string path in Properties.Settings.Default.Path.Split(','))
            {
                if (Directory.Exists(path))
                {
                    files.AddRange(Directory.GetFiles(path,
                        Properties.Settings.Default.Filter,
                        SearchOption.AllDirectories));
                }
            }
            if (files.Count > 0)
            {
                lvFoundFiles.Items.Clear();
                lvFoundFiles.SuspendLayout();
                pictureBox1.Enabled = true;
                pictureBox1.Visible = true;
                this.button6.Visible = true;
                button2.Visible = false;
                this.label6.Text = "";
                try
                {
                    this.backgroundWorker1.RunWorkerAsync(files);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                    "Exception raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No files found!",
                    "Files not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new()
            {
                Multiselect = false,
                CheckFileExists = true,
                DefaultExt = ".csv",
                Title = "Choose CSV File",
                Filter = "\"csv files (*.csv)|*.csv|All files (*.*)|*.*\""
            };
            {
                var result = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != null)
                {
                    var csvLines = File.ReadAllLines(openFileDialog.FileName);
                    if (csvLines.Length > 0)
                    {
                        string[] parts = csvLines[0].Split(',');
                        if (Clean(parts[0]) == "hash")
                        {
                            csvLines = csvLines.Skip(1).ToArray();
                        }
                        foreach (string line in csvLines)
                        {
                            parts = line.Split(',');
                            if (parts.Length == 4)
                            {
                                var fi = new FileInfo
                                {
                                    Hash = Clean(parts[0]),
                                    Origin = Clean(parts[1]),
                                    Size = long.Parse(Clean(parts[2])),
                                    LastModified = DateTime.Parse(Clean(parts[3])),
                                };
                                AddToAllowedList(fi);
                            }
                        }
                    }
                    SaveAllowedList();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this._stopSearch = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this._stopSearch = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this._stopSearch = true;
            _fileList.Clear();
            Properties.Settings.Default.Save();
            this.lvAllowedFiles.Items.Clear();
        }

        private void lvFoundFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvFoundFiles.SelectedItems.Count > 0)
            {
                var lvi = this.lvFoundFiles.SelectedItems[0];
                if (lvi != null)
                {
                    if (lvi.Tag is FileInfo fi && File.Exists(fi.Origin))
                    {
                        OpenWithDefaultProgram(fi.Origin);
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (e.Argument is List<string> files && e.Argument != null)
            {
                foreach (var file in files)
                {
                    if (_stopSearch)
                    {
                        break;
                    }
                    var sysFile = new System.IO.FileInfo(file);
                    var fi = new FileInfo { Hash = string.Empty, Origin = file, Size = sysFile.Length, LastModified = sysFile.LastWriteTime };
                    var lvi = new ListViewItem([file]) { Tag = fi };
                    if (_fileList.Any(f => f.HasSameInfo(fi)))
                    {
                        lvi.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        var hash = CalculateMD5(file);
                        if (_fileList.Any(f => f.Hash.Equals(hash, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            lvi.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lvi.BackColor = Color.LightPink;
                        }
                    }

                    this.Invoke(new Action(() =>
                    {
                        lvFoundFiles.Items.Add(lvi);
                        lvi.EnsureVisible();
                    }));
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            _stopSearch = false;
            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
            this.button6.Visible = false;
            button2.Visible = true;
            lvFoundFiles.ResumeLayout();
            int count = 0;
            foreach (ListViewItem lvi in lvFoundFiles.Items)
            {
                if (lvi.BackColor == Color.LightPink)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                this.label6.Text = $"{count} illegal files found";
            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (e.Argument is string[] files && files.Length > 0)
            {
                foreach (var file in files)
                {
                    if (this._stopSearch)
                    {
                        break;
                    }
                    var f = GetFileWithHash(file);
                    if (f != null && f.Origin != null && f.Hash != null)
                    {
                        if (!_fileList.Any(fli => fli.Hash == f.Hash))
                        {
                            _fileList.Add(f);
                            ListViewItem lvi = new([f.Hash, f.Origin, f.Size.ToString(), f.LastModified.ToString()]) { Tag = f };
                            this.Invoke(new Action(() =>
                            {
                                lvAllowedFiles.Items.Add(lvi);
                                lvi.EnsureVisible();
                            }));
                        }
                    }
                }
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._stopSearch = false;
            this.pictureBox2.Visible = false;
            this.button7.Visible = false;
            lvAllowedFiles.ResumeLayout();
            SaveAllowedList();
        }

        private void toolStripMenuItemRemovePath_Click(object sender, EventArgs e)
        {
            var c = GetClickedControl((ToolStripItem)sender);
            if (c != null)
            {
                RemoveSearchPath(c.Text);
                RefreshPathButtons();
            }
        }

        private void toolStripMenuItemOpenPath_Click(object sender, EventArgs e)
        {
            var c = GetClickedControl((ToolStripItem)sender);
            if (c != null)
            {
                OpenWithDefaultProgram(c.Text);
            }
        }

        private void toolStripMenuItemRun_Click(object sender, EventArgs e)
        {
            if (this.lvAllowedFiles.SelectedItems.Count > 0)
            {
                ListViewItem s = this.lvAllowedFiles.SelectedItems[0];
                var path = s.SubItems[1].Text;
                if (File.Exists(path))
                {
                    OpenWithDefaultProgram(path);
                }
            }
        }

        private void toolStripMenuItemProps_Click(object sender, EventArgs e)
        {
            ListViewItem s = this.lvAllowedFiles.SelectedItems[0];
            var path = s.SubItems[1].Text;
            if (File.Exists(path))
            {
                ShowFileProperties.Show(path);
            }
        }

        private void toolStripMenuItemDisallow_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem s in this.lvAllowedFiles.SelectedItems)
            {
                var path = s.SubItems[1].Text;
                RemoveFileFromAllowedList(path);
            }
            SaveAllowedList();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (this.lvFoundFiles.SelectedItems.Count > 0)
            {
                switch (e.ClickedItem?.Name)
                {
                    case "toolStripMenuItemPlay":
                        RunSelectedFile();
                        break;
                    case "toolStripMenuItemAdd":
                        AddSelectedNewItems();
                        break;
                    case "toolStripMenuItemProperties":
                        ShowSelectedItemProperties();
                        break;
                    case "toolStripMenuItemRemove":
                        RemoveSelectedItems();
                        break;

                }
            }
        }

        private void lvFoundFiles_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            RunSelectedFile();
        }

        private void ShowSelectedItemProperties()
        {
            ListViewItem s = this.lvFoundFiles.SelectedItems[0];
            var fi = s.Tag as FileInfo;
            if (File.Exists(fi?.Origin))
            {
                ShowFileProperties.Show(fi.Origin);
            }
        }

        private void RunSelectedFile()
        {
            if (this.lvFoundFiles.SelectedItems.Count > 0)
            {
                ListViewItem s = this.lvFoundFiles.SelectedItems[0];
                var fi = s.Tag as FileInfo;
                if (File.Exists(fi?.Origin))
                {
                    OpenWithDefaultProgram(fi.Origin);
                }
            }
        }

        private void RemoveSelectedItems()
        {
            FileInfo? fi;
            int removed = 0;
            foreach (ListViewItem f in this.lvFoundFiles.SelectedItems)
            {
                fi = f.Tag as FileInfo;
                if (fi != null && this.RemoveFileFromAllowedList(fi.Origin))
                {
                    removed++;
                }
                if (fi != null && File.Exists(fi.Origin))
                {
                    File.Delete(fi.Origin);
                    this.lvFoundFiles.Items.Remove(f);
                }
            }
            if (removed > 0)
            {
                SaveAllowedList();
                this.label6.Text = $"{removed} files removed from list";
            }
            else
            {
                this.label6.Text = "No files were removed from list";
            }
        }

        private void AddSelectedNewItems()
        {
            int added = 0;
            foreach (ListViewItem f in this.lvFoundFiles.SelectedItems)
            {
                if (f.Tag is FileInfo fi && this.AddFileToAllowedList(fi.Origin))
                {
                    added++;
                }
            }
            if (added > 0)
            {
                SaveAllowedList();
                this.label6.Text = $"{added} files added to list";
            }
            else
            {
                this.label6.Text = "No files were added to the list";
            }
        }

        private void LoadAllowedListView()
        {
            lvAllowedFiles.Items.Clear();
            foreach (var fi in _fileList)
            {
                ListViewItem lvi = new([fi.Hash, fi.Origin, fi.Size.ToString(), fi.LastModified.ToString()]) { Tag = fi };
                lvAllowedFiles.Items.Add(lvi);
                lvi.EnsureVisible();
            }
        }

        private void SaveAllowedList()
        {
            string text = "\"hash\",\"origin\",\"size\",\"modified\"" + Environment.NewLine;

            foreach (var fi in _fileList)
            {
                text += $"\"{fi.Hash}\",\"{fi.Origin}\",\"{fi.Size}\",\"{fi.LastModified}\"{Environment.NewLine}";
            }
            Properties.Settings.Default.AllowedFilesCsv = text;
            Properties.Settings.Default.Save();
            File.WriteAllText(_filePath, text);
        }


        private bool AddFileToAllowedList(string path)
        {
            var a = GetFileWithHash(path);
            return a != null && AddToAllowedList(a);
        }

        private bool AddToAllowedList(FileInfo f)
        {
            if (f.Origin != null && f.Hash != null)
            {
                var found = _fileList.Find(fli => fli.Hash == f.Hash);
                if (found != default)
                {
                    found.Origin = f.Origin;
                    found.Size = f.Size;
                    found.LastModified = f.LastModified;
                    foreach (ListViewItem lvi in this.lvAllowedFiles.Items)
                    {
                        if (lvi.Tag is FileInfo fi && fi.Hash == f.Hash)
                        {
                            lvi.Tag = f;
                            lvi.SubItems[1].Text = f.Origin;
                            lvi.SubItems[2].Text = f.Size.ToString();
                            lvi.SubItems[3].Text = f.LastModified.ToString();
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    _fileList.Add(f);
                    ListViewItem lvi = new([f.Hash, f.Origin, f.Size.ToString(), f.LastModified.ToString()]) { Tag = f };
                    lvAllowedFiles.Items.Add(lvi);
                    lvi.EnsureVisible();
                    return true;
                }
            }
            return false;
        }

        private bool RemoveFileFromAllowedList(string path)
        {
            var f = GetFileWithHash(path);
            if (f != null && f.Hash != null)
            {
                var found = _fileList.Find(fli => fli.Hash == f.Hash || f.HasSameInfo(fli));
                if (found != default)
                {
                    var index = _fileList.IndexOf(found);
                    _fileList.RemoveAt(index);
                    ListViewItem? lviFound = null;
                    foreach (ListViewItem lvi in this.lvAllowedFiles.Items)
                    {
                        if (lvi.Tag is FileInfo fi && fi.Hash == f.Hash)
                        {
                            lviFound = lvi;
                            break;
                        }
                    }
                    if (lviFound != null)
                    {
                        this.lvAllowedFiles.Items.Remove(lviFound);
                    }
                    return true;
                }
            }
            return false;
        }

        private void AddSearchPath(string path)
        {
            string[] paths = Properties.Settings.Default.Path.Split(",",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (!paths.Contains(path, StringComparer.InvariantCultureIgnoreCase))
            {
                if (paths.Length > 0)
                {
                    Properties.Settings.Default.Path += ",";
                }
                Properties.Settings.Default.Path += path;
                Properties.Settings.Default.Save();
            }
        }

        private static void RemoveSearchPath(string path)
        {
            string[] paths = Properties.Settings.Default.Path.Split(",",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (paths.Contains(path, StringComparer.InvariantCultureIgnoreCase))
            {
                var s = paths.Where(str => !path.Equals(str, StringComparison.InvariantCultureIgnoreCase));
                Properties.Settings.Default.Path = String.Join(',', s);
                Properties.Settings.Default.Save();
            }
        }

        private static Control? GetClickedControl(ToolStripItem sender)
        {
            if (sender is ToolStripItem menuItem)
            {
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    return owner.SourceControl;
                }
            }
            return null;
        }

        private static string Clean(string s)
        {
            return s.Trim().Trim('"');
        }

        private static string? CalculateMD5(string filename)
        {
            using MD5 md5 = MD5.Create();
            using FileStream fileStream = File.OpenRead(filename);
            try
            {
                fileStream.Position = 0;
                byte[] hashValue = md5.ComputeHash(fileStream);
                return BitConverter.ToString(md5.ComputeHash(hashValue)).Replace("-", "").ToLower();
            }
            catch (IOException e)
            {
                Console.WriteLine($"I/O Exception: {e.Message}");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access Exception: {e.Message}");
            }
            return null;
        }

        private static FileInfo? GetFileWithHash(string path)
        {
            System.IO.FileInfo file = new(path);
            if (file.Exists)
            {
                return new FileInfo
                {
                    Hash = CalculateMD5(path) ?? "",
                    Origin = path,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                };
            }
            return null;
        }

        private static void OpenWithDefaultProgram(string path)
        {
            using Process p = new();
            p.StartInfo.FileName = "explorer";
            p.StartInfo.Arguments = "\"" + path + "\"";
            p.Start();
        }
    }
    public class FileInfo
    {
        public required string Hash { get; set; }
        public required string Origin { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }

        public bool HasSameInfo(FileInfo fi2)
        {
            return
                this.Size == fi2.Size &&
                this.LastModified == fi2.LastModified &&
                Path.GetFileName(this.Origin).Equals(Path.GetFileName(fi2.Origin), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
