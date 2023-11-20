using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instalator
{
    public class Cesty
    {
        public string Start { get; set; } = string.Empty; 
        public Cesty()
        {
            // Získání informací o aktuální aplikaci
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            Start = Path.GetDirectoryName(assembly.Location);
        }

        //adresář kde budou soubory pro instalaci
        public static string Instal
        {
            get 
            {
                //var Cesta = new Cesty();
                var Cesta = Path.Combine(new Cesty().Start, "ZdrojInstalace");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Cesta;
            }
        }

        public static string Manifest
        {
            get
            {
                //var Cesta = new Cesty();
                var Cesta = Path.Combine(new Cesty().Start, "Manifest");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Cesta;
            }
        }

        public static string Zdroj
        {
            get
            {
                string Cesta = string.Empty;
                if (Environment.MachineName.ToUpperInvariant() == "KANCELAR")
                    Cesta = @"c:\Users\Martin\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\publish\";
                Cesta = @"d:\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\publish\";


                //var cesta = Path.Combine(Cesta, "Zdroj");
                //if (!Directory.Exists(cesta))
                //    Directory.CreateDirectory(cesta);
                return Cesta;
            }
        }

        public static string ZIP
        {
            get
            {
                var Cesta = Path.Combine(new Cesty().Start, "ZIP", "Zip.zip");
                if (!Directory.Exists(Path.GetDirectoryName(Cesta)))
                    Directory.CreateDirectory(Path.GetDirectoryName(Cesta));
                return Cesta;
            }
        }

        public static string UnZip
        {
            get
            {
                var Cesta = Path.Combine(new Cesty().Start, "UnZip");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Cesta;
            }
        }
    }
}
