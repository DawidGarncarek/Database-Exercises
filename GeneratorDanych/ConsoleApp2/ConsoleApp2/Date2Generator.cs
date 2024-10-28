using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal class Date2Generator
{
    internal static string GenerateDate2(Random random)
    {

        DateTime today = DateTime.Now;
        int maxDays = 365 * 3;
        int daysAmount = random.Next(0, maxDays);
        DateTime randomDate = today.AddDays(daysAmount);

        return randomDate.ToString("yyyy-MM-dd");
    }
}