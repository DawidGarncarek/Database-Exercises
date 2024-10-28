using System.Collections.Generic;
using System;

internal class OrderGenerator
{
    internal static string GenerateCompany(Random random)
    {
        var companyList = new List<string>
        {
            "MRCOOK",
            "KOGUCIK",
            "MAGDAL",
            "ACOR",
            "ROLMEX"
        };
        int randomNumber = random.Next(0, companyList.Count);

        return companyList[randomNumber];
    }
}