using Instalator;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;


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
        /// 

        public static async Task<string> Upload(string file)
        {
            var fileStream = System.IO.File.OpenRead(file);
            var streamContent = new StreamContent(fileStream);
            var content = new MultipartFormDataContent();

            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Zip);

            //fileNames.Add(file.Name);
            content.Add(content: streamContent, name: "\"files\"", fileName: Path.GetFileName(file));

            var http = new HttpApi();
            var response = await http.PostAsync("/api/File", content);
            //zpětné načtení souboru který byl uložen
            var newUploadResult = await response.Content.ReadFromJsonAsync<List<Upload>>();
            if (newUploadResult != null)
            {
                var uploads = new List<Upload>();
                //uploads = uploads.Concat(newUploadResult).ToList();
                uploads = [.. uploads, .. newUploadResult];
                return uploads.First().StoredFileName;
            }
            return null;
        }


        public static async Task<List<Upload>> GetSearchAsync(string FileName)
        {
            var http = new HttpApi();
            var response = await http.GetFromJsonAsync<List<Upload>>($"/api/File/Search/{FileName}");
            if (response == null)
                return [];
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
                //var fileStream = response.Content.ReadAsStream();

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

        public static async Task<ProgramInfo> ManifestDownloadAsync()
        {
            var http = new HttpApi();
            var response = await http.GetAsync($"/api/File/Manifest");
            if (response.IsSuccessStatusCode)
            {
                var fileStream = response.Content.ReadAsStream();
                //StreamReader reader = new StreamReader(fileStream);
               //JsonTextReader jsonReader = new JsonTextReader(reader);               
                ProgramInfo myData = JsonSerializer.Deserialize<ProgramInfo>(fileStream);
                return myData;
                //Porovnat se stávajícím uloženým souborem 
                //Spustit aktualizaci
            }
            return null;

        }

        public static async Task<bool> ManifestUploadAsync(string Verze)
        {
            //var fileStream = System.IO.File.OpenRead(file);
            //var streamContent = new StreamContent(fileStream);
            //MultipartFormDataContent content = new MultipartFormDataContent();

            //streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Zip);

            ////fileNames.Add(file.Name);
            //content.Add(content: streamContent, name: "\"files\"", fileName: Path.GetFileName(file));

            //var http = new HttpApi();
            //var response = await http.PostAsync("/api/File", content);

            string Cesta = Path.Combine(Cesty.Manifest, "Manifest.txt");

            //ProgramInfo program = new() { Version = Verze, ReleaseDate = DateTime.Now.ToString(), DownloadUrl = "192.168.1.210" };
            ProgramInfo program = new() { Version = Verze, ReleaseDate = DateTime.Now, DownloadUrl = HttpApi.IP() };
            string Json = System.Text.Json.JsonSerializer.Serialize(program);

            byte[] jsonBytes = Encoding.UTF8.GetBytes(Json);
            var memoryStream = new MemoryStream(jsonBytes);

            //StreamWriter streamWriter = new StreamWriter(Cesta);
            //streamWriter.Write(Json);
            //streamWriter.Close();
            //streamWriter.Dispose();

            //var fileStream = System.IO.File.OpenRead(Cesta);
            var streamContent = new StreamContent(memoryStream);
            var content = new MultipartFormDataContent();

            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Text.Plain);

            //fileNames.Add(file.Name);
            content.Add(content: streamContent, name: "\"files\"", fileName: Path.GetFileName(Cesta));

            var http = new HttpApi();
            var response = await http.PostAsync("/api/File/Manifest", content);
            var newUploadResult = await response.Content.ReadAsStringAsync();
            if (newUploadResult != null)
            {
                return true;
            }
            return false;
            //if (response.IsSuccessStatusCode)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            //zpětné načtení souboru který byl uložen
            //var newUploadResult = await response.Content.ReadFromJsonAsync<List<Upload>>();
            //if (newUploadResult is not null)
            //{
            //    List<Upload> uploads = new List<Upload>();
            //    uploads = uploads.Concat(newUploadResult).ToList();
            //    return uploads.First().StoredFileName;
            //}
            //return true;

            //var http = new HttpApi();
            //    var response = await http.PostAsync($"/api/File/Manifest", content);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //  }
            // }

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

    public class ProgramInfo
    {
        public string Version { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } //= string.Empty;
        public Uri? DownloadUrl { get; set; } //= new Uri(""); //= string.Empty;
    }

    public class HttpApi : HttpClient
    {
        public HttpApi()
        {
            BaseAddress = IP();
            //if(Environment.MachineName.ToUpperInvariant() == "KANCELAR")
            //    BaseAddress = new Uri("http://192.168.1.210/");
            //else 
            //    BaseAddress = new Uri("http://10.55.1.100/");
            //    //BaseAddress = new Uri("https://localhost:7208/");
        }

        public static Uri IP()
        {
            if (Environment.MachineName.Equals("KANCELAR", StringComparison.InvariantCultureIgnoreCase))
                return new Uri("http://192.168.1.210/");
            else
                return new Uri("http://10.55.1.100/");
            //BaseAddress = new Uri("https://localhost:7208/");
        }
    }
}
