using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class PlecGenerator
    {
        internal static string GeneratePlec(Random random)
        {
            var plecList = new List<string>
        {
            "Mezczyzna",
            "Kobieta"
        };
            int randomNumber = random.Next(0, plecList.Count);

            return plecList[randomNumber];
        }
    }
}
