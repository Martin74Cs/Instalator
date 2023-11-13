using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Install
    {
        public static async Task<string> Load(string downloadUrl)
        {
            //downloadUrl = "http://192.168.1.210/audio/0bo2drkd.cgx"; // Změňte na skutečnou URL programu ke stažení
            string destinationPath = @"c:\Z";

            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(downloadUrl);
                if (response.IsSuccessStatusCode)
                {
                    // Stažení souboru
                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                    string zipFilePath = Path.Combine(Path.GetTempPath(), "temp.zip");
                    File.WriteAllBytes(zipFilePath, fileBytes);

                    //Extrahování souborů z archivu
                    System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, destinationPath);

                    //Smazaní archivu
                    File.Delete(zipFilePath);
                    //MessageBox.Show("Instalace dokončena.");
                }
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Chyba při instalaci: {ex.Message}");
                return null;
            }
        }

        public static void Ukončení()
        {
            //Hlavní program se podívá přes HttpClient a zjistí novou verzi programu.
        }
    }
}
