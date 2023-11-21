using Library;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            var zip = await Install.GetSearchAsync("zip.zip");
            string RandomFilename = zip.Last().StoredFileName ?? "";

            string Cesta = string.Empty;
            if (Environment.MachineName.ToUpperInvariant() == "KANCELAR")
                Cesta = @"c:\Users\Martin\OneDriveKopie\Instalator\Instalator\Instalator\bin\Debug\net8.0\ZIP\";
            Cesta = @"d:\OneDrive.ZALOHA\Instalator\Instalator\bin\Debug\net8.0\FullInstall";
            string Kontrola = Path.GetDirectoryName(Cesta);
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
            var result = await Install.ManifestDownloadAsync();
            label1.Text = result.Version;
        }
    }
}
