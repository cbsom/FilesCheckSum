using System.Diagnostics;
using System.Reflection;

namespace FilesCheckSum
{
    public partial class Form1 : Form
    {
        private readonly List<FileInfo> _fileList = [];
        private readonly string _filePath = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "FileList.csv");
        private bool _stopSearch = false;

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Properties.Settings.Default.Path;
            this.textBox2.Text = Properties.Settings.Default.Filter;
            LoadAllowedListView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllowedList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            {
                var result = openFileDialog.ShowDialog();
                if (openFileDialog.SelectedPath.Length > 0)
                {
                    this.textBox1.Text = openFileDialog.SelectedPath;
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
                    pictureBox2.Visible = true;
                    this.button7.Visible = true;
                    backgroundWorker2.RunWorkerAsync(files);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openFileDialog = new();
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.SelectedPath.Length > 0)
                {
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
            Properties.Settings.Default.Path = this.textBox1.Text;
            Properties.Settings.Default.Filter = this.textBox2.Text;
            Properties.Settings.Default.Save();

            if (Directory.Exists(textBox1.Text))
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
                    var files = Directory.GetFiles(textBox1.Text,
                        Properties.Settings.Default.Filter,
                        SearchOption.AllDirectories);
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
                MessageBox.Show($"{textBox1.Text} does not exist!",
                    "Directory not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void lvFoundFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvFoundFiles.SelectedItems.Count > 0)
            {
                var s = this.lvFoundFiles.SelectedItems[0];
                if (s != null)
                {
                    var path = $"{this.textBox1.Text}{s.Text.Trim('.')}";
                    if (File.Exists(path))
                    {
                        OpenWithDefaultProgram(path);
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (e.Argument is string[] files && e.Argument != null)
            {
                foreach (var file in files)
                {
                    if (_stopSearch)
                    {
                        break;
                    }
                    var sysFile = new System.IO.FileInfo(file);
                    var fi = new { Origin = file, Size = sysFile.Length, LastModified = sysFile.LastWriteTime };
                    var lvi = new ListViewItem([file.Replace(textBox1.Text, "..") ?? "UNKNOWN"]);
                    if (_fileList.Any(f => f.Size == fi.Size &&
                                        f.LastModified == fi.LastModified &&
                                        Path.GetFileName(f.Origin).Equals(sysFile.Name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        lvi.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        var hash = GetHash(file);
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
                    this.Invoke(new Action(() =>
                    {
                        AddFileToAllowedList(file);
                    }));
                }
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._stopSearch = false;
            this.pictureBox2.Visible = false;
            this.button7.Visible = false;
            SaveAllowedList();
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
            var path = s.Tag as String;
            if (File.Exists(path))
            {
                ShowFileProperties.Show(path);
            }
        }

        private void RunSelectedFile()
        {
            if (this.lvFoundFiles.SelectedItems.Count > 0)
            {
                ListViewItem s = this.lvFoundFiles.SelectedItems[0];
                var path = s.Tag as String;
                if (File.Exists(path))
                {
                    OpenWithDefaultProgram(path);
                }
            }
        }

        private void RemoveSelectedItems()
        {
            string? path;
            int removed = 0;
            foreach (ListViewItem f in this.lvFoundFiles.SelectedItems)
            {
                path = f.Tag as String;
                if (path != null && this.RemoveFileFromAllowedList(path))
                {
                    removed++;
                }
                if (path != null && File.Exists(path))
                {
                    File.Delete(path);
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
                string? path = f.Tag as String;
                if (path != null && this.AddFileToAllowedList(path))
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
                ListViewItem lvi = new([fi.Hash, fi.Origin, fi.Size.ToString(), fi.LastModified.ToString()]);
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
                        if (lvi.Text == f.Hash)
                        {
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
                    ListViewItem lvi = new([f.Hash, f.Origin, f.Size.ToString(), f.LastModified.ToString()]);
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
                var found = _fileList.Find(fli => fli.Hash == f.Hash);
                if (found != default)
                {
                    var index = _fileList.IndexOf(found);
                    _fileList.RemoveAt(index);
                    ListViewItem? lviFound = null;
                    foreach (ListViewItem lvi in this.lvAllowedFiles.Items)
                    {
                        if (lvi.Text == f.Hash)
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

        private static string Clean(string s)
        {
            return s.Trim().Trim('"');
        }
        private static string GetHash(string file)
        {
            var md5Checksum = "";
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C  CertUtil -hashfile \"{file}\" MD5 | find /i /v \"md5\" | find /i /v \"certutil\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            using var process = new Process { StartInfo = startInfo };
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    md5Checksum = e.Data;
                }
            });

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            return md5Checksum;
        }

        private static FileInfo? GetFileWithHash(string path)
        {
            System.IO.FileInfo file = new(path);
            if (file.Exists)
            {
                return new FileInfo
                {
                    Hash = GetHash(path),
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
    }
}
