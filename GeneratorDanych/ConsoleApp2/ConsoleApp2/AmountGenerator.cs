using System.Collections.Generic;
using System;
internal class AmountGenerator
{
    internal static string GenerateAmount(Random random)
    {
        int randomAmount = random.Next(1000, 150001);

        return randomAmount.ToString();
    }
}