import tkinter as tk
from tkinter import ttk
import oracledb
import random
import datetime
from datetime import date, timedelta

class App:
    def __init__(self, master):
        self.master = master
        self.master.title("Generator")
        self.master.configure(background="#faf7e5")

        self.login_frame = tk.Frame(master)
        self.login_frame.pack(fill='both', expand=True)
        self.login_frame.configure(background="#faf7e5")
        tk.Label(self.login_frame, background='#faf7e5', text="Proszę zaloguj się do bazy danych:",
                 font=('Courier New', 12)).pack(pady=5)


        tk.Label(self.login_frame, background='#faf7e5', text="Host:", font=('Courier New',12)).pack()
        self.host_var = tk.StringVar(value='217.173.198.135')
        self.host_entry = tk.Entry(self.login_frame,textvariable=self.host_var, font=('Courier New',12))
        self.host_entry.pack()

        tk.Label(self.login_frame, background='#faf7e5', text="Port:", font=('Courier New', 12)).pack()
        self.port_var = tk.StringVar(value='1521')
        self.port_entry = tk.Entry(self.login_frame,textvariable=self.port_var, font=('Courier New', 12))
        self.port_entry.pack(pady=5)

        tk.Label(self.login_frame, background='#faf7e5', text="Nazwa serwisu:", font=('Courier New', 12)).pack()
        self.service_var = tk.StringVar(value='tpdb')
        self.service_entry = tk.Entry(self.login_frame,textvariable=self.service_var, font=('Courier New', 12))
        self.service_entry.pack(pady=5)

        tk.Label(self.login_frame, background='#faf7e5', text="Nazwa użytkownika:", font=('Courier New', 12)).pack()
        self.username_var = tk.StringVar(value='s101095')
        self.username_entry = tk.Entry(self.login_frame,textvariable=self.username_var, font=('Courier New', 12))
        self.username_entry.pack(pady=5) # Pakuje pole wprowadzania tekstu w ramce login_frame

        tk.Label(self.login_frame, background='#faf7e5', text="Hasło:", font=('Courier New', 12)).pack()
        self.password_var = tk.StringVar(value='')
        self.password_entry = tk.Entry(self.login_frame,textvariable=self.password_var, show='*',
                                       font=('Courier New', 12))
        self.password_entry.pack(pady=5)

        login_btn = tk.Button(self.login_frame, background='#808000', text="Zaloguj się", font=('Courier New', 12)
                              ,command=self.login)
        login_btn.pack(pady=10)


    def login(self):
        username = self.username_entry.get()
        password = self.password_entry.get()
        host = self.host_entry.get()
        port = self.port_entry.get()
        service = self.service_entry.get()
        try:
            dsn = oracledb.makedsn(host=host, port=port,service_name=service)  # Tworzenie DSN (Data Source Name)
            self.connection = oracledb.connect(user=username, password=password,dsn=dsn)
            print("Udane połączenie z bazą danych")
            self.login_frame.destroy()
            self.display_options()
        except oracledb.DatabaseError:
            error_label = tk.Label(self.login_frame, text="Nieporawne hasło lub login.", font=('Arial', 12),fg='red')
            error_label.pack()

    def display_options(self):
        options_frame = tk.Frame(self.master)
        options_frame.pack(fill='both',expand=True)
        tk.Label(options_frame, text="Wybierz opcję:", font=('Arial', 12)).pack(pady=5)
        options = ['Czyść tabele','Pobierz', 'Wpisz', 'Wpisz losowe', 'Wpisz losowe do wszystkich tabel']
        for option in options:
            if option == 'Czyść tabele':
                btn = tk.Button(options_frame, text=option, font=('Arial', 12),
                                command=lambda option=option: self.display_menuClear())
                btn.pack(padx=0, pady=0)
            elif (option == 'Pobierz'):
                btn = tk.Button(options_frame, text=option, font=('Arial', 12),
                                command=lambda option=option: self.display_menuGet())
                btn.pack(padx=0, pady=0)
            elif (option == 'Wpisz'):
                btn = tk.Button(options_frame, text=option, font=('Arial', 12),
                                command=lambda option=option: self.display_menuInsert())
                btn.pack(padx=0, pady=1)
            elif (option == 'Wpisz losowe'):
                btn = tk.Button(options_frame, text=option, font=('Arial', 12),
                                command=lambda option=option: self.display_menuRandom())
                btn.pack(padx=0, pady=1)
            else:
                btn = tk.Button(options_frame, text=option, font=('Arial', 12),
                                command=lambda option=option: self.display_menuRandom_All())
                btn.pack(padx=0, pady=1)

    def display_menuRandom(self):
        menu_frame = tk.Frame(self.master)
        menu_frame.pack(fill='both',expand=True)
        tk.Label(menu_frame, text="Wybierz tabelę do wprowadzenia losowych danych:", font=('Arial', 12)).pack(pady=5)

        iteration_label = tk.Label(menu_frame, text="Liczba iteracji:", font=('Arial', 12))
        iteration_label.pack()
        self.iteration_var = tk.StringVar(value='10')  # Domyślna wartość 10
        self.iteration_entry = tk.Entry(menu_frame, textvariable=self.iteration_var, font=('Courier New', 12))
        self.iteration_entry.pack()

        tables = ['samochod', 'pracownik', 'zamowienie', 'klient', 'wyjazdy', 'dostawy', 'firma', 'produkt', 'strefa_skladowania',
                  'magazyn']
        for table in tables:
            btn = tk.Button(menu_frame, text=table, font=('Arial', 12),
                            command=lambda table=table: self.insert_into_table_random(table))
            btn.pack(side=tk.LEFT, padx=5, pady=2)

    def display_menuRandom_All(self):
        menu_frame = tk.Frame(self.master)
        menu_frame.pack(fill='both',expand=True)
        tk.Label(menu_frame, text="Wprowadź liczbę iteracji:", font=('Arial', 12)).pack(pady=5)

        iteration_label = tk.Label(menu_frame, text="Liczba iteracji:", font=('Arial', 12))
        iteration_label.pack()
        self.iteration_var = tk.StringVar(value='10')  # Domyślna wartość 10
        self.iteration_entry = tk.Entry(menu_frame, textvariable=self.iteration_var, font=('Courier New', 12))
        self.iteration_entry.pack()

        btn = tk.Button(menu_frame, text="Wprowadź losowe dane do wszystkich tabeli", font=('Arial', 12),
                        command=lambda: self.insert_into_all_table_random())
        btn.pack(pady=2)

    def display_menuGet(self):
        menu_frame = tk.Frame(self.master)
        menu_frame.pack(fill='both',expand=True)
        tk.Label(menu_frame, text="Wybierz tabelę do podglądu:", font=('Arial', 12)).pack(pady=5)
        tables = ['samochod', 'pracownik', 'zamowienie', 'klient', 'wyjazdy', 'dostawy', 'firma', 'produkt', 'strefa_skladowania',
                  'magazyn']
        for table in tables:
            btn = tk.Button(menu_frame, text=table, font=('Arial', 12),
                            command=lambda table=table: self.display_data(table))
            btn.pack(side=tk.LEFT, padx=5, pady=2)

    def display_menuClear(self):
        menu_frame = tk.Frame(self.master)
        menu_frame.pack(fill='both', expand=True)
        tk.Label(menu_frame, text="Kliknij by wyczyścić dane z wszystkich tabeli:", font=('Arial', 12)).pack(pady=5)
        btn = tk.Button(menu_frame, text="WYCZYŚĆ TABELE", font=('Arial', 12),
                        command=lambda: self.clear_tables())
        btn.pack(pady=2)

    def display_menuInsert(self):
        menu_frame = tk.Frame(self.master)
        menu_frame.pack(fill='both',expand=True)
        tk.Label(menu_frame, text="Wybierz tabele do dodania danych:", font=('Arial', 12)).pack(pady=5)
        tables = ['samochod', 'pracownik', 'zamowienie', 'klient', 'wyjazdy', 'dostawy', 'firma', 'produkt', 'strefa_skladowania',
                  'magazyn']
        for table in tables:
            btn = tk.Button(menu_frame, text=table, font=('Arial', 12),
                            command=lambda table=table: self.insert_data(table))
            btn.pack(side=tk.LEFT, padx=5, pady=2)

    def insert_data(self, table):
        insert_window = tk.Toplevel(self.master)
        insert_window.title(f"Wprowadz do {table}")
        for i, name in enumerate(self.get_column_names(table)):
            label = tk.Label(insert_window, text=name, font=('Arial', 12, 'bold'))
            label.pack(pady=5)
            entry = tk.Entry(insert_window, font=('Arial', 12))
            entry.pack(pady=5)
        insert_btn = tk.Button(insert_window, text="Wpisz", font=('Arial', 12),
                               command=lambda: self.insert_into_table(table,insert_window))
        insert_btn.pack(pady=5)

    def display_data(self, table):
        data_window = tk.Toplevel(self.master)
        data_window.title(f"{table} dane")

        column_names = self.get_column_names(table)

        for i, name in enumerate(column_names):
            label = tk.Label(data_window, text=name, font=('Arial', 12, 'bold'))
            label.grid(row=0, column=i, padx=1, pady=1)

        data_rows = self.get_data_rows(table)

        canvas = tk.Canvas(data_window)
        canvas.grid(row=1, column=0, columnspan=len(column_names), sticky='nsew')
        frame = tk.Frame(canvas)
        scrollbar = tk.Scrollbar(data_window, orient="vertical", command=canvas.yview)
        canvas.configure(yscrollcommand=scrollbar.set)
        scrollbar.grid(row=1, column=len(column_names), sticky='ns')
        canvas.grid_rowconfigure(0, weight=1)
        canvas.grid_columnconfigure(0, weight=1)
        canvas.create_window((4, 4), window=frame, anchor="nw")

        for i, row in enumerate(data_rows):
            for j, val in enumerate(row):
                label = tk.Label(frame, text=val, font=('Arial', 12))
                label.grid(row=i, column=j, padx=1, pady=1)

        frame.update_idletasks()
        canvas.configure(scrollregion=canvas.bbox("all"))

    def clear_tables(self):
        cursor = self.connection.cursor()
        cursor.execute("DELETE FROM dostawy")
        cursor.execute("DELETE FROM klient")
        cursor.execute("DELETE FROM magazyn")
        cursor.execute("DELETE FROM pracownik")
        cursor.execute("DELETE FROM produkt")
        cursor.execute("DELETE FROM samochod")
        cursor.execute("DELETE FROM strefa_skladowania")
        cursor.execute("DELETE FROM wyjazdy")
        cursor.execute("DELETE FROM zamowienie")
        cursor.execute("DELETE FROM firma")
        self.connection.commit()
        cursor.close()

        text_label = tk.Label(self.master, text="Wszystkie dane usunięte z danych", font=('Arial', 12), fg='green')
        text_label.pack()


    def get_column_names(self, table):
        cursor = self.connection.cursor()
        cursor.execute(f"SELECT * FROM {table}")
        column_names = [column[0] for column in cursor.description]
        return column_names

    def get_data_rows(self, table):
        cursor = self.connection.cursor()
        cursor.execute(f"SELECT * FROM {table}")
        data_rows = cursor.fetchall()
        return data_rows

    def insert_into_table_random(self, table):
        #try:
            iteration = int(self.iteration_entry.get())
            cursor = self.connection.cursor()
            for i in range(iteration):
                data = {}

                # Generowanie danych tylko dla wybranej tabeli
                if table == 'magazyn':
                    data['magazyn'] = self.generate_random_zamowienie_data()
                elif table == 'strefa_skladowania':
                    data['strefa_skladowania'] = self.generate_random_strefa_skladowania_data()
                elif table == 'produkt':
                    data['produkt'] = self.generate_random_produkt_data()
                elif table == 'firma':
                    data['firma'] = self.generate_random_firma_data()
                elif table == 'dostawy':
                    data['dostawy'] = self.generate_random_dostawy_data()
                elif table == 'wyjazdy':
                    data['wyjazdy'] = self.generate_random_wyjazdy_data()
                elif table == 'klient':
                    data['klient'] = self.generate_random_klient_data()
                elif table == 'zamowienie':
                    data['zamowienie'] = self.generate_random_zamowienie_data()
                elif table == 'pracownik':
                    data['pracownik'] = self.generate_random_pracownik_data()
                elif table == 'samochod':
                    data['samochod'] = self.generate_random_samochod_data()

                for table, values in data.items():
                    with open(f"{table}.txt", "a") as file:
                        query = f"INSERT INTO {table} VALUES({','.join([':' + str(k) for k in values.keys()])})"
                        cursor.execute(query, values)
                        values_str = ', '.join([f"'{v}'" if isinstance(v, str) else str(v) for v in values.values()])
                        file.write(f"INSERT INTO {table} VALUES ({values_str});\n")
                self.connection.commit()
            cursor.close()
            correct_label = tk.Label(self.master, text=f"Dodano losowe dane do tabeli '{table}'", font=('Arial', 12),
                                     fg='green')
            correct_label.pack()
        #except oracledb.DatabaseError:
            #error_label = tk.Label(self.master, text="Błąd dodania losowych danych", font=('Arial', 12), fg='red')
            #error_label.pack()

    def insert_into_all_table_random(self):
        try:
            iteration = int(self.iteration_entry.get())
            cursor = self.connection.cursor()
            for i in range(iteration):
                data = {'samochod': self.generate_random_samochod_data(),
                        'pracownik': self.generate_random_pracownik_data(),
                        'zamowienie': self.generate_random_zamowienie_data(),
                        'klient': self.generate_random_klient_data(),
                        'wyjazdy': self.generate_random_wyjazdy_data(),
                        'dostawy': self.generate_random_dostawy_data(),
                        'firma': self.generate_random_firma_data(),
                        'produkt': self.generate_random_produkt_data(),
                        'strefa_skladowania': self.generate_random_strefa_skladowania_data(),
                        'magazyn': self.generate_random_magazyn_data(),
                        }

                for table, values in data.items():
                    with open(f"{table}.txt", "a") as file:
                        query = f"INSERT INTO {table} VALUES({','.join([':' + str(k) for k in values.keys()])})"
                        cursor.execute(query, values)
                        values_str = ', '.join([f"'{v}'" if isinstance(v, str) else str(v) for v in values.values()])
                        file.write(f"INSERT INTO {table} VALUES ({values_str});\n")
                self.connection.commit()
            cursor.close()
            correct_label = tk.Label(self.master, text="Dodano losowe dane do wszystkich tabeli", font=('Arial', 12),
                                         fg='green')
            correct_label.pack()
        except oracledb.DatabaseError:
             error_label = tk.Label(self.master, text="Błąd wprowadzenia losowych danych", font=('Arial', 12), fg='red')
             error_label.pack()


    def insert_into_table(self, table, window):
        cursor = self.connection.cursor()
        entries = window.winfo_children()
        values = []
        for i, entry in enumerate(entries):
            if i % 2 == 1:
                values.append(entry.get())
        values = tuple(values) # Zamiana listy na krotkę
        cursor.execute(f"INSERT INTO {table} VALUES {values}")
        with open(f"{table}.txt", "a") as file:
            file.write(f"INSERT INTO {table} VALUES {values};\n")
        self.connection.commit()
        cursor.close()
        window.destroy()

    def generate_random_samochod_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(id_auta) FROM samochod")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            id_auta = 1
        else:
            id_auta = result[0][0] + 1
            marka = f"{random.choice(['Fiat', 'MAN', 'Mercedes', 'Opel', 'Seat'])}"
            nr_rejestracyjny = f"{random.randint(10, 99)}-{random.randint(1000, 9999)}"
            pojemnosc_zaladunkowa = random.randint(1, 6)
            dostepnosc = random.randint(0, 1)

        return {'id_auta': id_auta,
                'marka': marka,
                'nr_rejestracyjny': nr_rejestracyjny,
                'pojemnosc_zaladunkowa': pojemnosc_zaladunkowa,
                'dostepnosc': dostepnosc}

    def generate_random_pracownik_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(id_pracownik) FROM pracownik")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            id_pracownik = 1
        else:
            id_pracownik = result[0][0] + 1
        imie = f"{random.choice(['Adam', 'Anna', 'Piotr', 'Ewa', 'Dawid', 'Ilona', 'Beata', 'Grzegorz', 'Jaroslaw', 'Przemyslaw', 'Kinga', 'Kacper', 'Slawomir', 'Jedrzej', 'Czeslawa'])}"
        nazwisko = f"{random.choice(['Kowalski', 'Nowak', 'Wójcik', 'Kowalczyk', 'Pawelczak', 'Bak', 'Put', 'Garncarek', 'Wcislo', 'Sobiegraj', 'Pongowski', 'Kot', 'Koscielny'])}"
        nr_telefonu = f"{random.randint(100000000, 999999999)}"
        stanowisko = random.choice(['Pracownik Magazynu', 'Ksiegowosc', 'Personel', 'Kierowca', 'Sprzataczka', 'Administrator', 'Wozkowy', 'Kierownik', 'Pracownik'])
        plec = random.choice(['Mezczyzna', 'Kobieta'])

        return {'id_pracownik': id_pracownik,
                'imie': imie,
                'nazwisko': nazwisko,
                'nr_telefonu': nr_telefonu,
                'stanowisko': stanowisko,
                'plec': plec}

    def generate_random_zamowienie_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(nr_zamowienia) FROM zamowienie")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            nr_zamowienia = 1
        else:
            nr_zamowienia = result[0][0] + 1
        kod_zamowienia = random.randint(1000, 9999)
        id_produkt = random.randint(1, 1000)
        ilosc_produktu = random.randint(1, 50)
        data_zaplaty = datetime.date.today()
        sposob_zaplaty = random.choice(['przelew', 'gotowka'])
        nazwa_firmy = nazwa_firmy = f"{random.choice(['Aptekol', 'Miesno', 'Zarcie', 'RTV I AGD', 'Przysnaki', 'Kotlin', 'X-KOM'])} "

        return {'nr_zamowienia': nr_zamowienia,
                'kod_zamowienia': kod_zamowienia,
                'id_produkt': id_produkt,
                'ilosc_produktu': ilosc_produktu,
                'data_zaplaty': data_zaplaty,
                'sposob_zaplaty': sposob_zaplaty,
                'nazwa_firmy': nazwa_firmy}

    def generate_random_klient_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(id_klient) FROM klient")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            id_klient = 1
        else:
            id_klient = result[0][0] + 1
        nr_zamowienia = random.randint(1000, 9999)
        nr_telefonu = f"{random.randint(100000000, 999999999)}"
        nazwa_firmy = f"{random.choice(['Aptekol', 'Miesno', 'Zarcie', 'RTV I AGD', 'Przysnaki', 'Kotlin', 'X-KOM'])} "
        kod_pocztowy = f"{random.randint(10, 99)}-{random.randint(100, 999)}"
        nr_lokalu = random.randint(1, 999)
        email = "{imie.lower()}@wp.com"
        zamowienie_nr_zamowienia = random.randint(1, 2)

        return {'id_klient': id_klient,
                'nr_zamowienia': nr_zamowienia,
                'nr_telefonu': nr_telefonu,
                'nazwa_firmy': nazwa_firmy,
                'kod_pocztowy': kod_pocztowy,
                'email': email,
                'zamowienie_nr_zamowienia': zamowienie_nr_zamowienia}

    def generate_random_wyjazdy_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(nr_wyjazdu) FROM wyjazdy")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            nr_wyjazdu = 1
        else:
            nr_wyjazdu = result[0][0] + 1
        nr_auta = random.randint(0, 100)
        data_wyjazdu = datetime.date.today()
        id_pracownik = random.randint(0, 1000)
        nr_zamowienia = random.randint(0, 100)
        samochod_id_auta = random.randint(1, 2)
        zamowienie_nr_zamowienia = random.randint(1, 2)

        return {'nr_wyjazdu': nr_wyjazdu,
                'nr_auta': nr_auta,
                'data_wyjazdu': data_wyjazdu,
                'id_pracownik': id_pracownik,
                'nr_zamowienia': id_pracownik,
                'samochod_id_auta': id_pracownik,
                'zamowienie_nr_zamowienia': zamowienie_nr_zamowienia}
    
    def generate_random_dostawy_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(id_dostawy) FROM dostawy")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            id_dostawy = 1
        else:
            id_dostawy = result[0][0] + 1
        nr_auta = random.randint(1, 1000)
        id_produktu= random.randint(1, 1000)
        data_przyjazdu = datetime.date.today()
        id_pracownik = random.randint(1, 2)
        samochod_id_auta = random.randint(1, 2)
                
        return {'id_dostawy': id_dostawy,
                'nr_auta': nr_auta,
                'id_produktu': id_produktu,
                'data_przyjazdu': data_przyjazdu,
                'id_pracownik': id_pracownik,
                'samochod_id_auta': samochod_id_auta}
    
    def generate_random_firma_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(nr_lokalu) FROM firma")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            nr_lokalu = 1
        else:
            nr_lokalu = result[0][0] + 1
        specjalizacja = f"{random.choice(['produkty miesne', 'farmacja', 'slodycze', 'sprzet AGD' , 'owoce i warzywa'])} "
        wielkosc_firmy = f"{random.choice(['lokalna', 'krajowa', 'europejska'])} "
        relacje = f"{random.choice(['dobre', 'srednie', 'slabe'])} "
        nazwa_firmy = f"{random.choice(['Aptekol', 'Miesno', 'Zarcie', 'RTV I AGD', 'Przysnaki', 'Kotlin', 'X-KOM'])} "
        klient_id_klient = random.randint(1, 2)

        return {'nr_lokalu': nr_lokalu,
                'specjalizacja': specjalizacja,
                'relacje': relacje,
                'nazwa_firmy': nazwa_firmy,
                'klient_id_klient': klient_id_klient}
    
    def generate_random_produkt_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(id_produkt) FROM produkt")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            id_produkt = 1
        else:
            id_produkt = result[0][0] + 1
            waga = random.randint(1, 200)
            wielkosc = random.randint(1, 5)
            rodzaj = f"{random.choice(['opakowanie', 'karton', 'paleta'])} "
            data_waznosci = datetime.date.today()
            dostawy_id_dostawy = random.randint(1, 2)

        return {'id_produkt': id_produkt,
                'waga': waga,
                'wielkosc': wielkosc,
                'rodzaj': rodzaj,
                'data_waznosci': data_waznosci,
                'dostawy_id_dostawy': dostawy_id_dostawy}
    
    def generate_random_magazyn_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT COUNT(*) FROM magazyn")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0]==0:
            nazwa_magazynu = 'W.P.R.O.S.T'
        else:
            nazwa_magazynu = f'Motel{result[0][0]}'
            id_obszar = random.randint(1, 1000)
            kod_pocztowy = f"{random.randint(10, 99)}-{random.randint(100, 999)}"
            miasto = random.choice(['Opole', 'Krakow', 'Warszawa'])
            dostawy = random.randint(1, 100)
            wyjazdy = random.randint(1, 100)
            wyjazdy_nr_wyjazdu = random.randint(1, 2)
            strefa_skladowania_id_strefa = random.randint(1, 2)

        return {'nazwa_magazynu': nazwa_magazynu,
                'id_obszar': id_obszar,
                'kod_pocztowy': kod_pocztowy,
                'miasto': miasto,
                'dostawy': dostawy,
                'wyjazdy': wyjazdy,
                'wyjazdy_nr_wyjazdu': wyjazdy_nr_wyjazdu,
                'strefa_skladowania_id_strefa': strefa_skladowania_id_strefa}
    
    def generate_random_strefa_skladowania_data(self):
        cursor = self.connection.cursor()
        cursor.execute("SELECT MAX(id_strefa) FROM strefa_skladowania")
        result = cursor.fetchall()
        cursor.close()
        if result[0][0] is None:
            id_strefa = 1
        else:
            id_strefa = result[0][0] + 1
        nr_obszar = random.randint(1, 100)
        wolne_miejsce = random.choice(['wolne', 'brak'])
        rodzaj_skladowania = random.choice(['Jednostki paletowe', 'Kartony', 'Chlodnia', 'Opakowania zbiorcze'])
        nr_pracownik = random.randint(0, 50)
        id_produkt = random.randint(1, 2)
        produkt_id_produkt = random.randint(0, 2)

        return {'id_strefa': id_strefa,
                'nr_obszar': nr_obszar,
                'wolne_miejsce': wolne_miejsce,
                'rodzaj_skladowania': rodzaj_skladowania,
                'nr_pracownik': nr_pracownik,
                'id_produkt': id_produkt,
                'produkt_id_produkt': produkt_id_produkt}



if __name__ == '__main__':
    root = tk.Tk()

    # Wielkość/pozycja okna
    screen_height = root.winfo_screenheight()
    window_height = int(screen_height * 0.6)

    screen_width = root.winfo_screenwidth()
    window_width = int(screen_width * 0.4)

    root.geometry("{0}x{1}+500+50".format(window_width, window_height))

    app = App(root)
    root.mainloop()

