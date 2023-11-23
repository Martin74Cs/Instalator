// See https://aka.ms/new-console-template for more information
using Instalator;
using Library;

Console.WriteLine(Environment.MachineName.ToUpperInvariant());

//Příprava instalce
Console.WriteLine("Příprava INSTALAČNÍHO SOUBORU .....(ANO/NE)");
if (Console.ReadKey().Key == ConsoleKey.A)
{ 
    Console.WriteLine("Poslat soubor na WEB .....");
    string Soubor = await Install.Upload(Cesty.Instalator);
    if(string.IsNullOrEmpty(Soubor))
        Console.WriteLine("Chyba nahrání souboru");
    else
    Console.WriteLine($"Byl nahran soubor : {Soubor}");
    //return;
}

Console.Write("Příprava instalce .....(ANO/NE)");
if (Console.ReadKey().Key == ConsoleKey.A)
{ 
    //Příprava instalce
    Console.WriteLine("Příprava instalce .....");
    //Příprava souboru kopirování do složky Install
    Zip.KopirovatSlozku(Cesty.Zdroj, Cesty.Instal);
    Console.WriteLine("Ok");

    Console.Write("Zip .....");
    Zip.Start(Cesty.Instal, Cesty.ZIP);
    Console.WriteLine("Ok");

    Console.WriteLine("Poslat soubor na WEB .....");
    string SoubourCode = await Install.Upload(Cesty.ZIP);
    if (string.IsNullOrEmpty(SoubourCode))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourCode}");

    //Console.ReadKey();

    Console.Write("Stažení souboru z WEB .....");
    if (await Install.Download(SoubourCode, Cesty.UnZip))
        Console.WriteLine($"Byl nahran soubor : {SoubourCode}");
    else
        Console.WriteLine("Chyba stahování");

    Console.WriteLine("Stiskni klavesu .......");
    Console.ReadKey();
}

////Manifest.Vypis();
////Console.Write("\n\n");
//Manifest.Aktualni();
////Console.ReadKey();

//Console.Write("Zip .....");
//Zip.Start(@"c:\Users\Martin\OneDriveKopie\Instalator\Instalator\WinForms\bin\Release\net8.0-windows\publish1\");
//Console.Write("Složka byla úspěšně zazipována.....");
//Console.WriteLine("Ok");
//Console.ReadKey();

//Console.Write("UnZip ....");
//string Cesta = Zip.UnStart(@"c:\Z\ZipSouboru.zip");
//Console.WriteLine("Ok. - " + Cesta);
//Console.ReadKey();

//Console.Write("Kopie složky .... ");
//Zip.KopirovatSlozku(Cesta, @"c:\Z");
//Console.WriteLine("Ok");
//Console.ReadKey();

//Console.Write("Dočasný Dir Smazán > " + Cesta  + " ... .");
//Directory.Delete(Cesta,true);
//Console.WriteLine("Ok");
//Console.ReadKey();