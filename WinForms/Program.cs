using Instalator;
using Microsoft.Extensions.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var form = new Form1();

            //form.Instalace = "predání funguje";
            if (Args.Length > 0)
            {
                form.Instalace = Args[0].ToString();
                //string Instalace = Arg.GetValue(0).ToString();
            }

            // Pøíklad ètení hodnot
            var configuration = LoadKonfigurace("appsettings.json");//.GetConnectionString("CestaProInstal");
            var test = configuration.GetConnectionString("AdresaRestApi");
            string AplikaceInstal = configuration["ConnectionStrings:AplikaceInstal"];

            Application.Run(form);
        }

        public static IConfigurationRoot LoadKonfigurace(string Cesta)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(Cesta, optional: true, reloadOnChange: true)
                .Build();

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile(Cesta, optional: true, reloadOnChange: true)
            //    .Build();

            return configuration;
        }
    }
}