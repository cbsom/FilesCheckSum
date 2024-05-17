using System.Diagnostics;
using System.Reflection;

namespace FilesCheckSum
{
    public partial class Form1 : Form
    {
        private readonly List<(string? hash, string? origin)> _fileList = [];
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
            string[] lines = Properties.Settings.Default.AllowedFilesCsv.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (lines.Length > 0)
            {
                string[] parts = lines[0].Split(',');
                if (Clean(parts[0]) == "hash" && Clean(parts[1]) == "origin")
                {
                    lines = lines.Skip(1).ToArray();
                }
                foreach (string line in lines)
                {
                    parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        _fileList.Add((Clean(parts[0]), Clean(parts[1])));
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Properties.Settings.Default.Path;
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
                var result = openFileDialog.ShowDialog();
                if (openFileDialog.FileNames.Length > 0)
                {
                    foreach (var file in openFileDialog.FileNames)
                    {
                        AddFileToAllowedList(file);
                    }
                    SaveAllowedList();
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openFileDialog = new();
            {
                var result = openFileDialog.ShowDialog();
                if (openFileDialog.SelectedPath.Length > 0)
                {
                    foreach (var file in Directory.GetFiles(openFileDialog.SelectedPath, "*.*", SearchOption.AllDirectories))
                    {
                        AddFileToAllowedList(file);
                    }
                    SaveAllowedList();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Path = this.textBox1.Text;
            Properties.Settings.Default.Save();

            lvFoundFiles.Items.Clear();
            lvFoundFiles.SuspendLayout();
            pictureBox1.Visible = true;
            this.button6.Visible = true;
            button2.Visible = false;
            pictureBox1.Enabled = true;
            pictureBox1.Image = Properties.Resources.running;
            this.label6.Text = "";
            this.backgroundWorker1.RunWorkerAsync();
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
                        if (Clean(parts[0]) == "hash" && Clean(parts[1]) == "origin")
                        {
                            csvLines = csvLines.Skip(1).ToArray();
                        }
                        foreach (string line in csvLines)
                        {
                            parts = line.Split(',');
                            if (parts.Length == 2)
                            {
                                AddToAllowedList((Clean(parts[0]), Clean(parts[1])));
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
            if (Directory.Exists(textBox1.Text))
            {
                foreach (var file in Directory.GetFiles(textBox1.Text, "*.*", SearchOption.AllDirectories))
                {
                    if (_stopSearch)
                    {
                        break;
                    }
                    var (hash, origin) = GetFileWithHash(file);
                    var lvi = new ListViewItem([file.Replace(textBox1.Text, "..") ?? "UNKNOWN"]) { Tag = file };
                    if (!_fileList.Any(flf => flf.hash?.ToUpper() == hash?.ToUpper()))
                    {
                        lvi.BackColor = Color.LightPink;
                    }
                    else
                    {
                        lvi.BackColor = Color.LightGreen;
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
            foreach (var (hash, origin) in _fileList)
            {
                if (hash != null && origin != null)
                {
                    ListViewItem lvi = new([hash, origin]);
                    lvAllowedFiles.Items.Add(lvi);
                    lvi.EnsureVisible();
                }
            }
        }

        private void SaveAllowedList()
        {
            string text = "\"hash\",\"origin\"" + Environment.NewLine;

            foreach (var (hash, origin) in _fileList)
            {
                text += $"\"{hash}\",\"{origin}\"{Environment.NewLine}";
            }
            Properties.Settings.Default.AllowedFilesCsv = text;
            Properties.Settings.Default.Save();
            File.WriteAllText(_filePath, text);
        }


        private bool AddFileToAllowedList(string path)
        {
            return AddToAllowedList(GetFileWithHash(path));
        }

        private bool AddToAllowedList((string? hash, string? origin) f)
        {
            if (f.origin != null && f.hash != null)
            {
                var found = _fileList.Find(fli => fli.hash == f.hash);
                if (found != default)
                {
                    found.origin = f.origin;
                    foreach (ListViewItem lvi in this.lvAllowedFiles.Items)
                    {
                        if (lvi.Text == f.hash)
                        {
                            lvi.SubItems[0].Text = f.origin;
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    _fileList.Add(f);
                    ListViewItem lvi = new([f.hash, f.origin]);
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
            if (f.hash != null)
            {
                var found = _fileList.Find(fli => fli.hash == f.hash);
                if (found != default)
                {
                    var index = _fileList.IndexOf(found);
                    _fileList.RemoveAt(index);
                    ListViewItem? lviFound = null;
                    foreach (ListViewItem lvi in this.lvAllowedFiles.Items)
                    {
                        if (lvi.Text == f.hash)
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

        private static (string? hash, string? origin) GetFileWithHash(string path)
        {
            string? hash = null;
            string? origin = null;
            if (File.Exists(path))
            {
                hash = GetHash(path);
                origin = path;
            }
            return (hash, origin);
        }

        private static void OpenWithDefaultProgram(string path)
        {
            using Process p = new();
            p.StartInfo.FileName = "explorer";
            p.StartInfo.Arguments = "\"" + path + "\"";
            p.Start();
        }        
    }
}
