﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Zip
    {
        /// <summary>
        /// ZIPOVÁNNÍ ZADANÉ SLOŽKY
        /// </summary>
        /// <param name="DirZip"></param>
        public static void Start(string DirZip, string zipSoubor = "")
        {
            // Zkontrolujte, zda zadaná složka existuje
            if (Directory.Exists(DirZip))
            {
                // Nastavte název výsledného zip souboru
                if(string.IsNullOrEmpty(zipSoubor))
                    zipSoubor = @"c:\Z\Zip.zip";

                // Zazipujte obsah složky
                if(File.Exists(zipSoubor))
                       File.Delete(zipSoubor);
                ZipFile.CreateFromDirectory(DirZip, zipSoubor);
            }
            else
            {
                Console.WriteLine("Zadaná složka neexistuje.");
            }
        }

        /// <summary>
        /// Rozipování zadaného souboru do vygenerované složkdy tem
        /// </summary>
        /// <param name="slozkaunzip"></param>
        /// <returns></returns>
        public static string UnStart(string slozkaunzip)
        {
            // Zkontrolujte, zda zadaná soubor existuje
            if (File.Exists(slozkaunzip))
            {
                DirectoryInfo Cesta = Directory.CreateTempSubdirectory();
                string zipFilePath = Path.Combine(Cesta.FullName);

                // Extrahování souborů z archivu
                System.IO.Compression.ZipFile.ExtractToDirectory(slozkaunzip, zipFilePath );
                return zipFilePath;
            }
            return null;
        }

        /// <summary>
        /// Kopírování souboru složek a podsložek rekuzivní
        /// </summary>
        public static void KopirovatSlozku(string zdrojovaSlozka, string cilovaSlozka)
        {
            try
            {
                // Získání seznamu všech souborů a podsložek ve zdrojové složce
                string[] vsechnySoubory = Directory.GetFileSystemEntries(zdrojovaSlozka);

                // Kopírování souborů a podsložek
                foreach (string souborOboji in vsechnySoubory)
                {
                    // Sestavení cílového názvu souboru nebo složky
                    string cilovySouborNeboSlozka = Path.Combine(cilovaSlozka, Path.GetFileName(souborOboji));

                    if (Directory.Exists(souborOboji))
                    {
                        if (!Directory.Exists(cilovySouborNeboSlozka))
                            Directory.CreateDirectory(cilovySouborNeboSlozka);
                        // Rekurzivní kopírování podsložky
                        KopirovatSlozku(souborOboji, cilovySouborNeboSlozka);
                    }
                    else
                    {
                        if (File.Exists(cilovySouborNeboSlozka))
                            File.Delete(cilovySouborNeboSlozka);
                        // Kopírování souboru
                        File.Copy(souborOboji, cilovySouborNeboSlozka);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při kopírování složky: {ex.Message}");
            }
        }
    }

}

