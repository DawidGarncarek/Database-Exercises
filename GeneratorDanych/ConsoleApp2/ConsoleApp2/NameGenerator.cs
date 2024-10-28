using System.Collections.Generic;
using System;

internal class NameGenerator
{
    internal static string GenerateName(Random random)
    {
        var nameList = new List<string>
        {          
            "Adam",
            "Anna",
            "Piotr",
            "Ewa",
            "Dawid",
            "Ilona", 
            "Beata", 
            "Grzegorz", 
            "Jaroslaw", 
            "Przemyslaw", 
            "Kinga", 
            "Kacper", 
            "Slawomir", 
            "Jedrzej", 
            "Czeslawa"
        };
        int randomNumber = random.Next(0, nameList.Count);

        return nameList[randomNumber];
    }
}