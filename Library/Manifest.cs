using System.Reflection;

namespace Library
{
    public class Manifest
    {
        public static void Vypis()
        {
            // Získání aktuálního domény aplikace
            AppDomain currentDomain = AppDomain.CurrentDomain;

            // Získání všech aktuálně načtených sestavení v doméně aplikace
            Assembly[] assemblies = currentDomain.GetAssemblies();

            // Vypsání informací o každém sestavení
            foreach (Assembly assembly in assemblies)
            {
                Console.WriteLine($"Název sestavení: {assembly.GetName().Name}");
                Console.WriteLine($"Verze sestavení: {assembly.GetName().Version}");
                Console.WriteLine("----------------------------------");
            }
        }

        public static void Aktualni()
        {
            Console.WriteLine("Aktualni:");

            // Získání aktuálního sestavení
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var item in assembly.GetType().GetProperties())
            {
                Console.WriteLine("{0,-30} :  {1}", item.Name, assembly.GetType().GetProperty(item.Name).GetValue(assembly));
            }
            Console.WriteLine("\n\nGetName:");


            foreach (var item in assembly.GetName().GetType().GetProperties())
            {
                try
                {
                    Console.WriteLine("{0,-30} :  {1}", item.Name, assembly.GetName().GetType().GetProperty(item.Name).GetValue(assembly.GetName()));
                }
                catch (Exception)  {    }
            }


            // Získání informací o sestavení
            Console.WriteLine("\n\nNázev sestavení:");
            Console.WriteLine($"Název sestavení: {assembly.GetName().Name}");
            Console.WriteLine($"Verze sestavení: {assembly.GetName().Version}");

            // Načítání typů z sestavení a reflexe
            Type myType = assembly.GetType("Namespace.MyClass");
            if (myType != null)
            {
                // Práce s typem...
            }
        }
    }
}
