using Instalator;
using Library;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms.Design;

namespace WinForms
{
    public partial class Form1 : Form
    {
        /// <summary>Cesta pro intalace </summary>
        public string Instalace { get; set; } = string.Empty;

        /// <summary>HLAVN� FORMUL�� </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            var zip = await Install.GetSearchAsync("zip.zip");
            string RandomFilename = zip.Last().StoredFileName ?? "";

            string Cesta = textBox1.Text;
            //if (Environment.MachineName.ToUpperInvariant() == "KANCELAR")
            //Cesta = @"c:\Users\Martin\OneDriveKopie\Instalator\Instalator\Instalator\bin\Debug\net8.0\ZIP\";
            //Cesta = @"d:\OneDrive.ZALOHA\Instalator\Instalator\bin\Debug\net8.0\FullInstall";
            //string Kontrola = Path.GetDirectoryName(Cesta);

            // Z�sk�n� aktu�ln�ch p��stupov�ch pr�v pomoc� DirectoryInfo
            DirectoryInfo directoryInfo = new DirectoryInfo(Cesty.Spusteno);
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

            // Nastaven� nov�ch p��stupov�ch pr�v (nap��klad READWRITE pro v�echny u�ivatele)
            //directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));

            // Nastaven� nov�ch p��stupov�ch pr�v (nap��klad READWRITE pro v�echny u�ivatele)
            //directorySecurity.AddAccessRule(new FileSystemAccessRule("Administrators", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));

            FileSystemAccessRule rule = new FileSystemAccessRule("Users", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
            directorySecurity.AddAccessRule(rule);

            // Aplikace nov�ch p��stupov�ch pr�v
            directoryInfo.SetAccessControl(directorySecurity);

            if (!Directory.Exists(Cesta))
                Directory.CreateDirectory(Cesta);

            await Install.Download(RandomFilename, Cesta);

            Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var qwe = await Install.ManifestUploadAsync(TManifest.Text);
            var result = await Install.ManifestDownloadAsync();
            label1.Text = result.Version;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Cesty.Tezak;
            var result = await Install.ManifestDownloadAsync();
            if (result != null)
                label1.Text = result.Version;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog()
            {
                //UseDescriptionForTitle = true,
                InitialDirectory = textBox1.Text,
            };
            if (folder.ShowDialog() == DialogResult.OK)
                textBox1.Text = folder.SelectedPath;

            //OpenFileDialog dialog = new()
            //{
            //    Title = "Vyper datab�zi Dbf",
            //    InitialDirectory = @"G:\env",
            //    Filter = "DB Files|*.dbf",
            //    FileName = "Tezak.dbf"
            //};

            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    textBox1.Text = dialog.FileName;
            //}
            //return dialog.FileName;
        }
    }
}
