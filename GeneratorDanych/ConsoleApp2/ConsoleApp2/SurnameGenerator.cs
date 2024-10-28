using System.Collections.Generic;
using System;

internal class SurnameGenerator
{
    internal static string GenerateSurname(Random random)
    {
        var surnameList = new List<string>
        {
            "Kowalski",
            "Nowak", 
            "Wójcik", 
            "Kowalczyk", 
            "Pawelczak", 
            "Bak", 
            "Put", 
            "Garncarek", 
            "Wcislo", 
            "Sobiegraj", 
            "Pongowski", 
            "Kot", 
            "Koscielny"
        };
        int randomNumber = random.Next(0, surnameList.Count);

        return surnameList[randomNumber];
    }
}