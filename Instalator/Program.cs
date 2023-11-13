// See https://aka.ms/new-console-template for more information
using Library;

//Manifest.Vypis();
//Console.Write("\n\n");
Manifest.Aktualni();
//Console.ReadKey();

Console.Write("Zip .....");
Zip.Start(@"c:\Users\Martin\OneDriveKopie\Instalator\Instalator\WinForms\bin\Release\net8.0-windows\publish1\");
Console.Write("Složka byla úspěšně zazipována.....");
Console.WriteLine("Ok");
Console.ReadKey();

Console.Write("UnZip ....");
string Cesta = Zip.UnStart(@"c:\Z\ZipSouboru.zip");
Console.WriteLine("Ok. - " + Cesta);
Console.ReadKey();

Console.Write("Kopie složky .... ");
Zip.KopirovatSlozku(Cesta, @"c:\Z");
Console.WriteLine("Ok");
Console.ReadKey();

Console.Write("Dočasný Dir Smazán > " + Cesta  + " ... .");
Directory.Delete(Cesta,true);
Console.WriteLine("Ok");
Console.ReadKey();