using System;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml;
using System.Data;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Odczytaj dane z bazy i zapisz do pliku XML");
            Console.WriteLine("2. Odczytaj dane z pliku XML i zapisz do bazy");
            Console.WriteLine("0. Wyjście");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Podaj nazwę tabeli, z której chcesz pobrać dane:");
                    string nazwaTabeli = Console.ReadLine();
                    OdczytajZBazyDoXML(nazwaTabeli);
                    break;

                case "2":                  
                    ObslugaZapisu();
                    break;

                case "0":
                    Console.WriteLine("Zakończono program.");
                    return;

                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }

    static void OdczytajZBazyDoXML(string nazwaTabeli)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Zapytanie SQL do pobrania danych
                string query = $"SELECT * FROM {nazwaTabeli}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Tworzenie obiektu DataSet
                        DataSet dataSet = new DataSet();
                        DataTable dataTable = new DataTable(nazwaTabeli);
                        dataSet.Tables.Add(dataTable);
                        dataTable.Load(reader);

                        // Zapis do pliku XML
                        string xmlFileName = $"DaneZ{nazwaTabeli}.xml";
                        dataSet.WriteXml(xmlFileName);
                        Console.WriteLine("Dane zostały zapisane do pliku XML.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ObslugaZapisu()
    {
        Console.WriteLine("Wybierz tabelę, do której chcesz wrzucic dane z pliku XML");
        Console.WriteLine("1. Tabela pracownik");
        Console.WriteLine("2. Tabela dostawy");
        Console.WriteLine("3. Tabela klient");
        Console.WriteLine("4. Tabela magazyn");
        Console.WriteLine("5. Tabela produkt");
        Console.WriteLine("6. Tabela samochod");
        Console.WriteLine("7. Tabela strefa skladowania");
        Console.WriteLine("8. Tabela wyjazdy");
        Console.WriteLine("9. Tabela zamowienie");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml1 = Console.ReadLine();
                    ZapiszDoPracownikZXML(nazwaPlikuXml1);
                break;

            case "2":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml2 = Console.ReadLine();
                    ZapiszDoDostawyZXML(nazwaPlikuXml2);
                break;

            case "3":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml3 = Console.ReadLine();
                    ZapiszDoKlientZXML(nazwaPlikuXml3);
                break;

            case "4":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml4 = Console.ReadLine();
                    ZapiszDoMagazynZXML(nazwaPlikuXml4);
                break;

            case "5":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml5 = Console.ReadLine();
                    ZapiszDoProduktZXML(nazwaPlikuXml5);
                break;

            case "6":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml6 = Console.ReadLine();
                    ZapiszDoSamochodZXML(nazwaPlikuXml6);
                break;

            case "7":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml7 = Console.ReadLine();
                    ZapiszDoStrefaSkladowaniaZXML(nazwaPlikuXml7);
                break;

            case "8":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml8 = Console.ReadLine();
                    ZapiszDoWyjazdyZXML(nazwaPlikuXml8);
                break;

            case "9":
                Console.WriteLine("Podaj nazwę pliku XML, z którego chcesz załadować dane (z rozszerzeniem .xml):");
                string nazwaPlikuXml9 = Console.ReadLine();
                    ZapiszDoZamowienieZXML(nazwaPlikuXml9);
                break;

            default:
                Console.WriteLine("Brak takiej opcji!");
                break;
        }
    }
    static void ZapiszDoPracownikZXML(string nazwaPlikuXml1)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml1);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(id_pracownik) FROM pracownik";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["id_pracownik"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO pracownik (id_pracownik, imie, nazwisko, nr_telefonu, stanowisko, plec) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["id_pracownik"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["imie"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["nazwisko"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["nr_telefonu"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["stanowisko"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["plec"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoDostawyZXML(string nazwaPlikuXml2)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml2);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(id_dostawy) FROM dostawy";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["id_dostawy"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO dostawy (id_dostawy, nr_auta, id_produktu, data_przyjazdu, id_pracownik, samochod_id_auta) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["id_dostawy"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["nr_auta"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["id_produktu"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["data_przyjazdu"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["id_pracownik"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["samochod_id_auta"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoKlientZXML(string nazwaPlikuXml3)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml3);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(id_klient) FROM klient";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["id_klient"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO klient (id_klient, nr_zamowienia, nr_telefonu, nazwa_apteki, kod_pocztowy, nr_lokalu, e-mail, zamowienie_nr_zamowienia) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6, @Kolumna7, @Kolumna8)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["id_klient"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["nr_zamowienia"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["nr_telefonu"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["nazwa_apteki"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["kod_pocztowy"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["nr_lokalu"]);
                            command.Parameters.AddWithValue("@Kolumna7", row["e-mail"]);
                            command.Parameters.AddWithValue("@Kolumna8", row["zamowienie_nr_zamowienia"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoMagazynZXML(string nazwaPlikuXml4)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml4);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(nazwa_magazynu) FROM magazyn";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["nazwa_magazynu"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO magazyn (nazwa_magazynu, id_obszar, kod_pocztowy, miasto, dostawy, wyjazdy, wyjazdy_nr_wyjazdu, strefa_skladowania_id_strefa) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6, @Kolumna7, @Kolumna8)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["nazwa_magazynu"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["id_obszar"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["kod_pocztowy"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["miasto"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["dostawy"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["wyjazdy"]);
                            command.Parameters.AddWithValue("@Kolumna7", row["wyjazdy_nr_wyjazdu"]);
                            command.Parameters.AddWithValue("@Kolumna8", row["strefa_skladowania_id_strefa"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoProduktZXML(string nazwaPlikuXml5)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml5);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(id_produkt) FROM produkt";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["id_produkt"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO produkt (id_produkt, waga, wielkosc, rodzaj, data_waznosci, dostawy_id_dostawy) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["id_produkt"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["waga"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["wielkosc"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["rodzaj"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["data_waznosci"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["dostawy_id_dostawy"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoSamochodZXML(string nazwaPlikuXml6)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml6);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(id_auta) FROM samochod";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["id_auta"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO samochod (id_auta, marka, nr_rejestracyjny, pojemnosc_zaladunkowa, dostepnosc) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["id_auta"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["marka"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["nr_rejestracyjny"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["pojemnosc_zaladunkowa"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["dostepnosc"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoStrefaSkladowaniaZXML(string nazwaPlikuXml7)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml7);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(id_strefa) FROM strefa_skladowania";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["id_strefa"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO strefa_skladowania (id_strefa, nr_obszar, wolne_miejsce, rodzaj_skladowania, nr_pracownik, id_produkt, produkt_id_produkt) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6, @Kolumna7)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["id_strefa"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["nr_obszar"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["wolne_miejsce"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["rodzaj_skladowania"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["nr_pracownik"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["id_produkt"]);
                            command.Parameters.AddWithValue("@Kolumna7", row["produkt_id_produkt"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoWyjazdyZXML(string nazwaPlikuXml8)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml8);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(nr_wyjazdu) FROM wyjazdy";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["nr_wyjazdu"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO wyjazdy (nr_wyjazdu, nr_auta, data_wyjazdu, id_pracownik, nr_zamowienia, samochod_id_auta, zamowienie_nr_zamowienia) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6, @Kolumna7)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["nr_wyjazdu"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["nr_auta"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["data_wyjazdu"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["id_pracownik"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["nr_zamowienia"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["samochod_id_auta"]);
                            command.Parameters.AddWithValue("@Kolumna7", row["zamowienie_nr_zamowienia"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
    static void ZapiszDoZamowienieZXML(string nazwaPlikuXml9)
    {
        string connectionString = "Data Source=DESKTOP-0JAUF99\\SQLEXPRESS;Initial Catalog=Magazyn;Integrated Security=True;";

        try
        {
            // Wczytanie danych XML do DataSet
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(nazwaPlikuXml9);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string maxIdQuery = "SELECT MAX(nr_zamowienia) FROM zamowienie";
                SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, connection);
                int maxIdInTable = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                int newIdStart = maxIdInTable + 1;

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        row["nr_zamowienia"] = newIdStart++;
                        // Zapytanie SQL do wstawienia danych
                        string insertQuery = $"INSERT INTO zamowienie (nr_zamowienia, kod_zamowienia, id_produkt, ilosc_produktu, data_zaplaty, sposob_zaplaty, nazwa_apteki) VALUES (@Kolumna1, @Kolumna2, @Kolumna3, @Kolumna4 ,@Kolumna5, @Kolumna6, @Kolumna7)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Dodaj parametry z DataRow
                            command.Parameters.AddWithValue("@Kolumna1", row["nr_zamowienia"]);
                            command.Parameters.AddWithValue("@Kolumna2", row["kod_zamowienia"]);
                            command.Parameters.AddWithValue("@Kolumna3", row["id_produkt"]);
                            command.Parameters.AddWithValue("@Kolumna4", row["ilosc_produktu"]);
                            command.Parameters.AddWithValue("@Kolumna5", row["data_zaplaty"]);
                            command.Parameters.AddWithValue("@Kolumna6", row["sposob_zaplaty"]);
                            command.Parameters.AddWithValue("@Kolumna7", row["nazwa_apteki"]);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            Console.WriteLine("Dane zostały zapisane do bazy danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
}
