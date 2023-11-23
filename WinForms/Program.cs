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

            //foreach (string arg in Args)
            //{
            //    Console.WriteLine(arg);
            //}
            //Console.ReadKey();

            //form.Instalace = "predání funguje";
            if (Args.Length > 0)
            {
                form.Instalace = Args[0].ToString();
                //string Instalace = Arg.GetValue(0).ToString();
            }
            
            Application.Run(form);
        }
    }
}