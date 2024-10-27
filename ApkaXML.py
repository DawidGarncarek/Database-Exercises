import pyodbc
import xml.etree.ElementTree as ET

# Funkcja do pobierania danych z bazy danych MSSQL Server i zapisywania do pliku XML
def export_to_xml(server, database, username, password, xml_filename):
    connection_string = f"DRIVER={{SQL Server}};SERVER={server};DATABASE={DESKTOP-0JAUF99\\SQLEXPRESS};UID={username};PWD={password}"
    connection = pyodbc.connect(connection_string)
    cursor = connection.cursor()

    # Pobierz dane z bazy danych
    cursor.execute("SELECT * FROM pracownik")
    rows = cursor.fetchall()

    # Utwórz element główny XML
    root = ET.Element("data")

    # Dodaj elementy dla każdego wiersza z bazy danych
    for row in rows:
        item = ET.SubElement(root, "item")
        for i, column_name in enumerate(cursor.description):
            column_value = row[i]
            ET.SubElement(item, column_name[0]).text = str(column_value)

    # Zapisz do pliku XML
    tree = ET.ElementTree(root)
    tree.write(xml_filename, encoding="utf-8", xml_declaration=True)

    connection.close()

# Funkcja do importowania danych z pliku XML do bazy danych MSSQL Server
def import_from_xml(server, database, username, password, xml_filename):
    connection_string = f"DRIVER={{SQL Server}};SERVER={server};DATABASE={database};UID={username};PWD={password}"
    connection = pyodbc.connect(connection_string)
    cursor = connection.cursor()

    # Usuń istniejące dane w tabeli
    cursor.execute("DELETE FROM your_table_name")
    connection.commit()

    # Odczytaj dane z pliku XML
    tree = ET.parse(xml_filename)
    root = tree.getroot()

    # Wstaw dane do bazy danych
    for item in root.findall("item"):
        values = [element.text for element in item]
        cursor.execute("INSERT INTO your_table_name VALUES (?, ?, ?)", values)

    connection.commit()
    connection.close()

# Przykładowe użycie
export_to_xml("your_server", "your_database", "your_username", "your_password", "data.xml")
import_from_xml("your_server", "your_database", "your_username", "your_password", "data.xml")
