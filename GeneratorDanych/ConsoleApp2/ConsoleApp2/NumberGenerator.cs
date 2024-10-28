using System.Collections.Generic;
using System;

internal class NumberGenerator
{
    internal static string GenerateNumber(Random random)
    {
        int min = 100000000; // Minimalna liczba dziewięciocyfrowa (100 000 000)
        int max = 999999999; // Maksymalna liczba dziewięciocyfrowa (999 999 999)
        int number = random.Next(min, max + 1);
        return number.ToString();
    }
}