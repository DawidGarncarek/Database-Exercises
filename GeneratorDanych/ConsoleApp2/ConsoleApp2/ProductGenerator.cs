using System.Collections.Generic;
using System;
internal class ProductGenerator
{
    internal static string GenerateProduct(Random random)
    {
        var ProductList = new List<string>
        {
            "Czosnek",
            "Kardamon",
            "Chilli",
            "Kurkuma",
            "Lukrecja"
        };
        int randomNumber = random.Next(0, ProductList.Count);

        return ProductList[randomNumber];
    }
}