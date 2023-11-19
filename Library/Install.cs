﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
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
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var fileStream = response.Content.ReadAsStream();


                string zipFilePath = Path.Combine(Path.GetTempPath(), "temp.zip");

                // Stažení souboru
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                
                //vytvoření
                File.WriteAllBytes(zipFilePath, fileBytes);
                
                //Extrahování souborů z archivu přepsání souborů
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, Uložit, true);

                //Smazaní archivu
                File.Delete(zipFilePath);
                //MessageBox.Show("Instalace dokončena.");

                return true;
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
        public string? Apid { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
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
