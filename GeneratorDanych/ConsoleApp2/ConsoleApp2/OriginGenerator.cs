using System.Collections.Generic;
using System;
internal class OriginGenerator
{
    internal static string GenerateOrigin(Random random)
    {
        var originList = new List<string>
        {
            "Chiny",
            "Hiszpania",
            "Indie",
            "Meksyk",
            "Maroko"
        };
        int randomNumber = random.Next(0, originList.Count);
        return originList[randomNumber];
    } 
}