using System.Collections.Generic;
using System;
internal class QuantityGenerator
{
    internal static string GenerateQuantity(Random random)
    {
        int losowaLiczba = random.Next(1, 720);
        int ilosc = losowaLiczba * 25;
        return ilosc.ToString();
    }
}