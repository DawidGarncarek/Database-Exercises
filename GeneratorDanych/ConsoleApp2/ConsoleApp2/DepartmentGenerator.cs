using System.Collections.Generic;
using System;

internal class DepartmentGenerator
{
    internal static string GenerateDepartment(Random random)
    {
        var departmentList = new List<string>
        {
            "Jakość",
            "Magazyn",
            "Zakupy",
            "Księgowość",
            "Sprzedaż"
        };
        int randomNumber = random.Next(0, departmentList.Count);

        return departmentList[randomNumber];
    }
}