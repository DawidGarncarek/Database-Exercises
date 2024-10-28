using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class DateGenerator
{
    internal static string GenerateDate(Random random)
    {
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = DateTime.Now;
        TimeSpan timeSpan = endDate - startDate;
        int losowaLiczbaDni = random.Next((int)timeSpan.TotalDays);
        DateTime losowaData = startDate.AddDays(losowaLiczbaDni);
        return losowaData.ToString("yyyy-MM-dd");
    }
}