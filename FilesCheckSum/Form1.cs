using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Xml.Linq;

namespace FilesCheckSum
{
    public partial class Form1 : Form
    {
        private readonly List<(string? name, string? hash)> _fileList = new();
        private string _filePath = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FileList.csv");

        public Form1()
        {
            InitializeComponent();
            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);
                if (lines.Length > 0)
                {
                    string[] parts = lines[0].Split(',');
                    if (Clean(parts[0]) == "name" && Clean(parts[1]) == "hash")
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
            else
            {
                File.CreateText(_filePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Properties.Settings.Default.Path;
            RefreshListView();
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
                        AddFileToList(file);
                    }
                    SaveListToFile();
                    RefreshListView();
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
                        AddFileToList(file);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Path = this.textBox1.Text;
            Properties.Settings.Default.Save();

            lvFoundFiles.Items.Clear();
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            pictureBox1.Image = Properties.Resources.running;
            this.label6.Text = "";
            this.backgroundWorker1.RunWorkerAsync();
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
                    var (name, hash) = GetFileWithHash(file);
                    var lvi = new ListViewItem([file.Replace(textBox1.Text, "..") ?? "UNKNOWN"]) { Tag = file };
                    if (!_fileList.Any(flf => flf.hash?.ToUpper() == hash?.ToUpper()))
                    {
                        lvi.BackColor = Color.LightPink;
                    }
                    else
                    {
                        lvi.BackColor = Color.LightGreen;
                    }
                    this.Invoke(new Action(() => lvFoundFiles.Items.Add(lvi)));
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
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
                if (path != null && this.RemoveFileFromList(path))
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
                SaveListToFile();
                RefreshListView();
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
                if (path != null && this.AddFileToList(path))
                {
                    added++;
                }
            }
            if (added > 0)
            {
                SaveListToFile();
                RefreshListView();
                this.label6.Text = $"{added} files added to list";
            }
            else
            {
                this.label6.Text = "No files were added to the list";
            }
        }

        private void RefreshListView()
        {
            lvAllowedFiles.Items.Clear();
            foreach (var (name, hash) in _fileList)
            {
                if (name != null && hash != null)
                {
                    lvAllowedFiles.Items.Add(new ListViewItem([name, hash]));
                    lvAllowedFiles.Refresh();
                }
            }
        }

        private void SaveListToFile()
        {
            string text = "\"name\",\"hash\"" + Environment.NewLine;

            foreach (var (name, hash) in _fileList)
            {
                text += $"\"{name}\",\"{hash}\"{Environment.NewLine}";
            }
            File.WriteAllText(_filePath, text);
        }


        private bool AddFileToList(string path)
        {
            var f = GetFileWithHash(path);
            if (f.hash != null)
            {
                var found = _fileList.Find(fli => fli.hash == f.hash);
                if (found != default)
                {
                    var index = _fileList.IndexOf(found);
                    _fileList.RemoveAt(index);
                    _fileList.Insert(index, f);
                    return true;
                }
                else
                {
                    _fileList.Add(f);
                    return true;
                }
            }
            return false;
        }

        private bool RemoveFileFromList(string path)
        {
            var f = GetFileWithHash(path);
            if (f.hash != null)
            {
                var found = _fileList.Find(fli => fli.hash == f.hash);
                if (found != default)
                {
                    var index = _fileList.IndexOf(found);
                    _fileList.RemoveAt(index);
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

        private static (string? name, string? hash) GetFileWithHash(string path)
        {
            string? name = null;
            string? hash = null;
            if (File.Exists(path))
            {
                name = Path.GetFileName(path);
                hash = GetHash(path);
            }
            return (name, hash);
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
