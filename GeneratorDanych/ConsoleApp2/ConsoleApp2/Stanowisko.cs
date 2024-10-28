using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class StanowiskoGenerator
    {
        internal static string GenerateStanowisko(Random random)
        {
            var stanowiskoList = new List<string>
        {
            "Pracownik Magazynu",
            "Ksiegowosc",
            "Personel",
            "Kierowca", 
            "Sprzataczka", 
            "Administrator",
            "Wozkowy",
            "Kierownik", 
            "Pracownik"
        };
            int randomNumber = random.Next(0, stanowiskoList.Count);

            return stanowiskoList[randomNumber];
        }
    }
}
