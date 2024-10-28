using System.Collections.Generic;
using System;

internal class WorkNumbGenerator
{
    internal static string GenerateWorkNumb()
    {
        Random random = new Random();
        int min = 2; // Minimalna liczba dziewięciocyfrowa (100 000 000)
        int max = 8; // Maksymalna liczba dziewięciocyfrowa (999 999 999)
        int number = random.Next(min, max + 1);
        return number.ToString();
    }
}