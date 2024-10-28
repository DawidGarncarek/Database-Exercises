using ConsoleApp2;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Threading;


class DataGenerator
{
    private const string ConnectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";

    public static void Main()
    {
        Console.WriteLine("Ile danych chcesz wpisac ");
        string b = Console.ReadLine();
        try
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                GenerateData(connection, int.Parse(b) + 1);

                Console.WriteLine("Dane wygenerowane pomyślnie.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd: " + ex.Message);
        }
    }
    private static void GenerateData(SqlConnection connection, int v)
    {
        //GenerateDepartments(connection);
        //GenerateInvoices(connection, v);
        GeneratePracownik(connection, v);
        //GenerateAccounts(connection, v);
        //GenerateStorage(connection, v);
        //GenerateOrderHistory(connection, v);
        //GenerateClients(connection, v);
        //GenerateOrders(connection, v);            
        //GenerateWorkers(connection, v);
        //GenerateImports(connection, v);
        //GenerateExports(connection, v);
    }

    //private static void GenerateDepartments(SqlConnection connection)
    //{
    //    string insertQuery = "INSERT INTO działy (id_działu, dział, ilość_pracowników) " +
    //                         "VALUES (@id_działu, @dział, @ilość_pracowników)";
    //    string[,] DepartmentData = new string[,]
    //    {
    //    { "1", "Jakość", "6" },
    //    { "2", "Magazyn", "8" },
    //    { "3", "Zakupy", "5" },
    //    { "4", "Sprzedaż", "5" },
    //    { "5", "Księgowość", "2" }
    //    };

    //    for (int i = 0; i < DepartmentData.GetLength(0); i++)
    //    {
    //        string idDziału = DepartmentData[i, 0];

    //        // Sprawdzanie czy działy zostały już dodane (opcja zadziała od drugiego uruchomienia programu)
    //        if (!DepartmentExists(connection, idDziału))
    //        {
    //            using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //            {
    //                command.Parameters.AddWithValue("@id_działu", idDziału);
    //                command.Parameters.AddWithValue("@dział", DepartmentData[i, 1]);
    //                command.Parameters.AddWithValue("@ilość_pracowników", DepartmentData[i, 2]);

    //                int rowAffected = command.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}
    //private static void GenerateExports(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = ExpID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        var data_eksportu = DateGenerator.GenerateDate(random);
    //        string ilość_produktu = QuantityGenerator.GenerateQuantity(random);
    //        int id_produktu1 = productID(connection);
    //        string insertQuery = "INSERT INTO eksporty (id_eksportu, data_eksportu, id_produktu, ilość_produktu, id_produktu1) " +
    //                             "VALUES (@id_eksportu, @data_eksportu, @id_produktu, @ilość_produktu, @id_produktu1)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_eksportu", LastID);
    //            command.Parameters.AddWithValue("@data_eksportu", data_eksportu);
    //            command.Parameters.AddWithValue("@id_produktu", LastID);
    //            command.Parameters.AddWithValue("@ilość_produktu", ilość_produktu);
    //            command.Parameters.AddWithValue("@id_produktu1", id_produktu1);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    //private static void GenerateInvoices(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = InvID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        string firma = OrderGenerator.GenerateCompany(random);
    //        string kwota = AmountGenerator.GenerateAmount(random);
    //        string numer_faktury = NumberGenerator.GenerateNumber(random);
    //        var data_zamówienia = DateGenerator.GenerateDate(random);
    //        DateTime data_zamówieniaDateTime = DateTime.Parse(data_zamówienia);
    //        DateTime termin_płatności = data_zamówieniaDateTime.AddDays(30);
    //        string insertQuery = "INSERT INTO faktury (id_faktury, numer_faktury, firma, kwota, termin_płatności) " +
    //                             "VALUES (@id_faktury, @numer_faktury, @firma, @kwota, @termin_płatności)";

    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_faktury", LastID);
    //            command.Parameters.AddWithValue("@numer_faktury", "FV" + numer_faktury);
    //            command.Parameters.AddWithValue("@firma", firma);
    //            command.Parameters.AddWithValue("@kwota", kwota);
    //            command.Parameters.AddWithValue("@termin_płatności ", termin_płatności);

    //            command.ExecuteNonQuery();
    //        }
    //    }

    //}
    //private static void GenerateOrderHistory(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = HisOrdID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        string firma = OrderGenerator.GenerateCompany(random);
    //        var data_zamówienia = DateGenerator.GenerateDate(random);
    //        DateTime data_zamówieniaDateTime = DateTime.Parse(data_zamówienia);
    //        string produkty = ProductGenerator.GenerateProduct(random);
    //        int id_produktu = productID(connection);
    //        string insertQuery = "INSERT INTO historia_zakupów (id_zakupu, firma, produkty, data_zamówienia, id_produktu) " +
    //                             "VALUES (@id_zakupu, @firma, @produkty, @data_zamówienia, @id_produktu)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_zakupu", LastID);
    //            command.Parameters.AddWithValue("@firma", firma);
    //            command.Parameters.AddWithValue("@produkty", produkty);
    //            command.Parameters.AddWithValue("@data_zamówienia", data_zamówienia);
    //            command.Parameters.AddWithValue("@id_produktu", id_produktu);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    //private static void GenerateImports(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = ImpID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        var data_importu = DateGenerator.GenerateDate(random);
    //        string ilość_produktu = QuantityGenerator.GenerateQuantity(random);
    //        int id_produktu1 = productID(connection);
    //        string insertQuery = "INSERT INTO importy (id_importu, data_importu, id_produktu, ilość_produktu, id_produktu1) " +
    //                             "VALUES (@id_importu, @data_importu, @id_produktu, @ilość_produktu, @id_produktu1)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_importu", LastID);
    //            command.Parameters.AddWithValue("@data_importu", data_importu);
    //            command.Parameters.AddWithValue("@id_produktu", LastID);
    //            command.Parameters.AddWithValue("@ilość_produktu", ilość_produktu);
    //            command.Parameters.AddWithValue("@id_produktu1", id_produktu1);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    //private static void GenerateClients(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = CliID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        string imię = NameGenerator.GenerateName(random);
    //        string nazwisko = SurnameGenerator.GenerateSurname(random);
    //        string firma = OrderGenerator.GenerateCompany(random);
    //        string numer_telefonu = NumberGenerator.GenerateNumber(random);
    //        string insertQuery = "INSERT INTO klienci (id_klienta, imię, nazwisko, firma, numer_telefonu, email) " +
    //                             "VALUES (@id_klienta, @imię, @nazwisko, @firma, @numer_telefonu, @email)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_klienta", LastID);
    //            command.Parameters.AddWithValue("@imię", imię);
    //            command.Parameters.AddWithValue("@nazwisko", nazwisko);
    //            command.Parameters.AddWithValue("@firma", firma);
    //            command.Parameters.AddWithValue("@numer_telefonu", numer_telefonu);
    //            command.Parameters.AddWithValue("@email", imię + "@gmail.com");

    //            command.ExecuteNonQuery();
    //        }
    //    } 
    //}
    //private static void GenerateAccounts(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = AccID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        string firma = OrderGenerator.GenerateCompany(random);
    //        string numer_faktury = NumberGenerator.GenerateNumber(random);
    //        int id_faktury1 = inv1ID(connection);
    //        string insertQuery = "INSERT INTO księgowość (id_faktury, numer_faktury, dni_po_terminie, firma, id_faktury1) " +
    //                             "VALUES (@id_faktury, @numer_faktury, @dni_po_terminie, @firma, @id_faktury1)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_faktury", LastID);
    //            command.Parameters.AddWithValue("@numer_faktury", "Fv" + numer_faktury);
    //            command.Parameters.AddWithValue("@dni_po_terminie", 0);
    //            command.Parameters.AddWithValue("@firma", firma);
    //            command.Parameters.AddWithValue("@id_faktury1", id_faktury1);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    //private static void GenerateStorage(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = StorageID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        string nazwa_produktu = ProductGenerator.GenerateProduct(random);
    //        string ilość = QuantityGenerator.GenerateQuantity(random);
    //        string kraj_pochodzenia = OriginGenerator.GenerateOrigin(random);
    //        string data_ważności = Date2Generator.GenerateDate2(random);


    //        string insertQuery = "INSERT INTO magazyn (id_produktu, nazwa_produktu, ilość, kraj_pochodzenia, data_ważności) " +
    //                             "VALUES (@id_produktu, @nazwa_produktu, @ilość, @kraj_pochodzenia, @data_ważności)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_produktu", LastID);
    //            command.Parameters.AddWithValue("@nazwa_produktu", nazwa_produktu);
    //            command.Parameters.AddWithValue("@ilość", ilość);
    //            command.Parameters.AddWithValue("@kraj_pochodzenia", kraj_pochodzenia);
    //            command.Parameters.AddWithValue("@data_ważności", data_ważności);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    //private static void GenerateWorkers(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = WorkID(connection);
    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;
    //        string imię = NameGenerator.GenerateName(random);
    //        string nazwisko = SurnameGenerator.GenerateSurname(random);
    //        string dział = DepartmentGenerator.GenerateDepartment(random);
    //        string telefon_kontaktowy = NumberGenerator.GenerateNumber(random);
    //        int id_faktury = inv1ID(connection);
    //        int id_klienta = clientID(connection);
    //        int id_zamówienia = orderID(connection);
    //        int id_działu = departmentID(connection);


    //        string insertQuery = "INSERT INTO pracownicy (id_pracownika, imię, nazwisko, dział, telefon_kontaktowy, email, id_faktury, id_klienta, id_zamówienia, id_działu) " +
    //                             "VALUES (@id_pracownika, @imię, @nazwisko, @dział, @telefon_kontaktowy, @email, @id_faktury, @id_klienta, @id_zamówienia, @id_działu)";

    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_pracownika", LastID);
    //            command.Parameters.AddWithValue("@imię", imię);
    //            command.Parameters.AddWithValue("@nazwisko", nazwisko);
    //            command.Parameters.AddWithValue("@dział", dział);
    //            command.Parameters.AddWithValue("@telefon_kontaktowy ", telefon_kontaktowy);
    //            command.Parameters.AddWithValue("@email", imię + "@op.pl");
    //            command.Parameters.AddWithValue("@id_faktury", id_faktury);
    //            command.Parameters.AddWithValue("@id_klienta", id_klienta);
    //            command.Parameters.AddWithValue("@id_zamówienia", id_zamówienia);
    //            command.Parameters.AddWithValue("@id_działu", id_działu);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    private static void GeneratePracownik(SqlConnection connection, int count)
    {
        Random random = new Random();
        int LastID = WorkID(connection);
        for (int i = 1; i < count; i++)
        {
            LastID++;
            string imie = NameGenerator.GenerateName(random);
            string nazwisko = SurnameGenerator.GenerateSurname(random);
            string nr_telefonu = NumberGenerator.GenerateNumber(random);
            string stanowisko = StanowiskoGenerator.GenerateStanowisko(random);
            string plec = PlecGenerator.GeneratePlec(random);


            string insertQuery = "INSERT INTO pracownik (id_pracownik, imie, nazwisko, nr_telefonu, stanowisko, plec) " +
                                 "VALUES (@id_pracownik, @imie, @nazwisko, @nr_telefonu, @stanowisko, @plec)";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id_pracownik", LastID);
                command.Parameters.AddWithValue("@imie", imie);
                command.Parameters.AddWithValue("@nazwisko", nazwisko);
                command.Parameters.AddWithValue("@nr_telefonu ", nr_telefonu);
                command.Parameters.AddWithValue("@stanowisko", stanowisko);
                command.Parameters.AddWithValue("@plec", plec);

                command.ExecuteNonQuery();
            }
        }
    }
    //private static void GenerateOrders(SqlConnection connection, int count)
    //{
    //    Random random = new Random();
    //    int LastID = OrdID(connection);

    //    for (int i = 1; i < count; i++)
    //    {
    //        LastID++;

    //        string firma = OrderGenerator.GenerateCompany(random);
    //        var data_zamówienia = DateGenerator.GenerateDate(random);
    //        DateTime data_zamówieniaDateTime = DateTime.Parse(data_zamówienia);
    //        DateTime data_załadunku = data_zamówieniaDateTime.AddDays(2);
    //        string produkty = ProductGenerator.GenerateProduct(random);
    //        string ilość_produktów = QuantityGenerator.GenerateQuantity(random);
    //        int id_produktu = productID(connection);

    //        string insertQuery = "INSERT INTO zamówienia (id_zamówienia, firma, data_zamówienia, data_załadunku, produkty, ilość_produktów, id_produktu) " +
    //                             "VALUES (@id_zamówienia, @firma, @data_zamówienia, @data_załadunku, @produkty, @ilość_produktów, @id_produktu)";
    //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
    //        {
    //            command.Parameters.AddWithValue("@id_zamówienia", LastID);
    //            command.Parameters.AddWithValue("@firma", firma);
    //            command.Parameters.AddWithValue("@data_zamówienia", data_zamówienia);
    //            command.Parameters.AddWithValue("@data_załadunku", data_załadunku);
    //            command.Parameters.AddWithValue("@produkty", produkty);
    //            command.Parameters.AddWithValue("@ilość_produktów", ilość_produktów);
    //            command.Parameters.AddWithValue("@id_produktu", id_produktu);

    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
    //private static int productID(SqlConnection connection)
    //{
    //    using (SqlCommand cmd = connection.CreateCommand())
    //    {
    //        cmd.CommandText = "SELECT TOP 1 id_produktu FROM magazyn ORDER BY NEWID()";
    //        object result = cmd.ExecuteScalar();
    //        return (result != null) ? Convert.ToInt32(result) : 1;
    //    }
    //}
    //private static int inv1ID(SqlConnection connection)
    //{
    //    using (SqlCommand cmd = connection.CreateCommand())
    //    {
    //        cmd.CommandText = "SELECT TOP 1 id_faktury FROM faktury ORDER BY id_faktury";
    //        object result = cmd.ExecuteScalar();
    //        return (result != null) ? Convert.ToInt32(result) : 1;
    //    }
    //}
    //private static int clientID(SqlConnection connection)
    //{
    //    using (SqlCommand cmd = connection.CreateCommand())
    //    {
    //        cmd.CommandText = "SELECT TOP 1 id_klienta FROM klienci ORDER BY id_klienta";
    //        object result = cmd.ExecuteScalar();
    //        return (result != null) ? Convert.ToInt32(result) : 1;
    //    }
    //}
    //private static int orderID(SqlConnection connection)
    //{
    //    using (SqlCommand cmd = connection.CreateCommand())
    //    {
    //        cmd.CommandText = "SELECT TOP 1 id_zamówienia FROM zamówienia ORDER BY id_zamówienia";
    //        object result = cmd.ExecuteScalar();
    //        return (result != null) ? Convert.ToInt32(result) : 1;
    //    }
    //}
    //private static int departmentID(SqlConnection connection)
    //{
    //    using (SqlCommand cmd = connection.CreateCommand())
    //    {
    //        cmd.CommandText = "SELECT TOP 1 id_działu FROM działy ORDER BY id_działu";
    //        object result = cmd.ExecuteScalar();
    //        return (result != null) ? Convert.ToInt32(result) : 1;
    //    }
    //}
    //private static bool DepartmentExists(SqlConnection connection, string idDziału)
    //{
    //    string query = "SELECT COUNT(*) FROM działy WHERE id_działu = @id_działu";
    //    SqlCommand command = new SqlCommand(query, connection);
    //    command.Parameters.AddWithValue("@id_działu", idDziału);

    //    int count = Convert.ToInt32(command.ExecuteScalar());

    //    // Jeśli count > 0, to działy już zostały dodane
    //    return count > 0;
    //}
    //static int OrdID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z tabeli zamówienia
    //    string query = "SELECT MAX(id_zamówienia) FROM zamówienia";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }
    //    return LastID;
    //}
    static int WorkID(SqlConnection connection)
    {
        int LastID = 0;

        // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
        string query = "SELECT MAX(id_pracownik) FROM pracownik";
        SqlCommand command = new SqlCommand(query, connection);

        // Wykonaj zapytanie i pobierz wynik
        object result = command.ExecuteScalar();
        if (result != DBNull.Value)
        {
            LastID = Convert.ToInt32(result);
        }

        return LastID;
    }
    //static int ExpID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
    //    string query = "SELECT MAX(id_eksportu) FROM eksporty";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }

    //    return LastID;
    //}
    //static int ImpID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
    //    string query = "SELECT MAX(id_importu) FROM importy";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }

    //    return LastID;
    //}
    //static int InvID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
    //    string query = "SELECT MAX(id_faktury) FROM faktury";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }

    //    return LastID;
    //}
    //static int StorageID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    string query = "SELECT MAX(id_produktu) FROM magazyn";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }
    //    return LastID;
    //}
    //static int CliID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
    //    string query = "SELECT MAX(id_klienta) FROM klienci";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }

    //    return LastID;
    //}
    //static int AccID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
    //    string query = "SELECT MAX(id_faktury) FROM księgowość";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }

    //    return LastID;
    //}
    //static int HisOrdID(SqlConnection connection)
    //{
    //    int LastID = 0;

    //    // Tutaj napisz zapytanie SQL do pobrania ostatniego ID z bazy danych
    //    string query = "SELECT MAX(id_zakupu) FROM historia_zakupów";
    //    SqlCommand command = new SqlCommand(query, connection);

    //    // Wykonaj zapytanie i pobierz wynik
    //    object result = command.ExecuteScalar();
    //    if (result != DBNull.Value)
    //    {
    //        LastID = Convert.ToInt32(result);
    //    }

    //    return LastID;
    //}
}