using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library
{
    public class Install
    {
        //public static async Task<string> Load(string downloadUrl, string destinationPath)
        //{
        //    try
        //    {
        //        using var httpClient = new HttpClient();
        //        using var response = await httpClient.GetAsync(downloadUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Stažení souboru
        //            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
        //            string zipFilePath = Path.Combine(Path.GetTempPath(), "temp.zip");
        //            File.WriteAllBytes(zipFilePath, fileBytes);

        //            //Extrahování souborů z archivu
        //            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, destinationPath);

        //            //Smazaní archivu
        //            File.Delete(zipFilePath);
        //            //MessageBox.Show("Instalace dokončena.");
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show($"Chyba při instalaci: {ex.Message}");
        //        return null;
        //    }
        //}

        /// <summary>
        /// Nahraní souboru na WEB
        /// </summary>
        /// <param name="file"></param>
        public static async Task<string> Upload(string file)
        {

            var fileStream = System.IO.File.OpenRead(file);
            var streamContent = new StreamContent(fileStream);
            MultipartFormDataContent content = new MultipartFormDataContent();

            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Zip);

            //fileNames.Add(file.Name);
            content.Add(content: streamContent, name: "\"files\"", fileName: Path.GetFileName(file));

            var http = new HttpApi();
            var response = await http.PostAsync("/api/File", content);
            //zpětné načtení souboru který byl uložen
            var newUploadResult = await response.Content.ReadFromJsonAsync<List<Upload>>();
            if (newUploadResult is not null)
            {
                List<Upload> uploads = new List<Upload>();
                uploads = uploads.Concat(newUploadResult).ToList();
                return uploads.First().StoredFileName;
            }
            return null;
        }


        public static async Task<List<Upload>> GetSearchAsync(string FileName)
        {
            var http = new HttpApi();
            var response = await http.GetFromJsonAsync<List<Upload>>($"/api/File/Search/{FileName}");
            if (response == null)
                return new();
            return response;
        }

        /// <summary>
        /// Download zadaného souboru
        /// </summary>
        public static async Task<bool> Download(string StoredFileName, string Uložit)
        {
            var http = new HttpApi();
            var response = await http.GetAsync($"/api/File/{StoredFileName}");
            if (response.IsSuccessStatusCode)
            {
                var fileStream = response.Content.ReadAsStream();

                string zipFilePath = Path.Combine(Path.GetTempPath(), "temp.zip");

                // Stažení souboru
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                
                //vytvoření
                File.WriteAllBytes(zipFilePath, fileBytes);
                
                //Extrahování souborů z archivu přepsání souborů
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, Uložit);

                //Smazaní archivu
                File.Delete(zipFilePath);
                //MessageBox.Show("Instalace dokončena.");

                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> ManifestDownloadAsync()
        {
            var http = new HttpApi();
            var response = await http.GetAsync($"/api/File/Manifest");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> ManifestUploadAsync()
        {
            ProgramInfo program = new() { Version = "0.0.1", ReleaseDate = DateTime.Now.ToString(), DownloadUrl = "192.168.1.210" };
            string Json = System.Text.Json.JsonSerializer.Serialize(program);

            // Převedení JSON řetězce na pole bytů
            byte[] jsonBytes = Encoding.UTF8.GetBytes(Json);

            // Vytvoření MemoryStream z pole bytů
            using (MemoryStream memoryStream = new MemoryStream(jsonBytes))
            {
                // Vytvoření instance StreamContent z MemoryStream
                var streamContent = new StreamContent(memoryStream);

                // Zde můžete použít 'streamContent' pro další operace s HTTP požadavkem

            
                MultipartFormDataContent content = new MultipartFormDataContent();

                //typ spoboru txt
                //streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Text.Plain);

                //fileNames.Add(file.Name);
                content.Add(content: streamContent, name: "\"files\"", fileName: "Manifest.txt");

                // Zobrazit obsah, který bude odeslán v HTTP požadavku
                string requestBody = await content.ReadAsStringAsync();

                var http = new HttpApi();
                var response = await http.PostAsync($"/api/File/Manifest", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }



        public static void Ukončení()
        {
            //Hlavní program se podívá přes HttpClient a zjistí novou verzi programu.

            //Bude spuštěn instalátor

            //Hlavní program bude ukončen

            //Proběhne kopírování souboru

            //Hlavní program spušten

            //Instalátor ukončen
        }
    }

    public class Upload
    {
        public int Id { get; set; }
        public string Apid { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }

    class ProgramInfo
    {
        public string Version { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
    }

    public class HttpApi : HttpClient
    {
        public HttpApi()
        {
            if(Environment.MachineName.ToUpperInvariant() == "KANCELAR")
                BaseAddress = new Uri("http://192.168.1.210/");
            else 
                BaseAddress = new Uri("http://10.55.1.100/");
        }
    }
}
