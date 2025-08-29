#  ProdavnicaApp - WPF Aplikacija za Upravljanje Prodavnicom

##  Sadržaj
- [O aplikaciji](#o-aplikaciji)
- [Prijava na sistem](#prijava)
- [Registracija na sistem](#registracija)
- [Funkcionalnosti](#funkcionalnosti)
- [Tehnologije](#tehnologije)
- [Sistemski zahtevi](#sistemski-zahtevi)
- [Instalacija i pokretanje](#instalacija-i-pokretanje)
- [Konfiguracija baze podataka](#konfiguracija-baze-podataka)
- [Uputstvo za korišćenje](#uputstvo-za-korišćenje)
- [Struktura projekta](#struktura-projekta)
- [Licenca](#licenca)

## <span id="o-aplikaciji"> O aplikaciji

ProdavnicaApp je desktop aplikacija napisana u WPF (Windows Presentation Foundation) koja omogućava upravljanje online prodavnicom. Aplikacija podržava dve uloge korisnika: **kupac** i **administrator**, sa različitim funkcionalnostima za svaku ulogu. Sastoji se od 3 glavna prozora: prozor za prijavu, prozor za običnog korisnika (kupca) i prozor za admina.

### Ključne karakteristike
- **Dvojezičnost**: Podržava srpski i engleski jezik
- **Material Design**: Moderan i intuitivan korisnički interfejs
- **Baza podataka**: MySQL integracija sa Entity Framework
- **Responsive dizajn**: Prilagođava se različitim veličinama prozora

## <span id="prijava"> Prijava na sistem

Korisnik se prijavljuje na sistem tako što unosi svoje podatke za prijavu.
<p align="center">
    <img src="screenshots/login.png" alt="Prozor za prijavu" /><br>
</p>

Ukoliko podaci nisu ispravni izbacuje se greška.
<p align="center">
    <img src="screenshots/login-error.png" alt="Prozor za gresku pri prijavi" />
</p>

## <span id="registracija"> Registracija na sistem

Ukoliko korisnik nema nalog, može da se registruje.
<p align="center">
    <img src="screenshots/register.png" alt="Prozor za registraciju" />
</p>

Pri regisraciji korisnik mora da unese validne podatke, pravilan format e-maila, lozinke, kao i ponovljena lozinka.

<p align="center">
    <img src="screenshots/register-error-email.png" alt="Prozor za gresku u emailu u registraciji" /> <br>
    <img src="screenshots/register-error-password.png" alt="Prozor za gresku u lozinci u registraciji" /> <br>
    <img src="screenshots/register-error-password-match.png" alt="Prozor za gresku u lozinkama u registraciji" />
</p>

## Prozor za običnog korisnika (kupca)

Korisnik kada se prijavi na sistem, prikazuje mu se glavni korisnički prozor na kojem može da napravi narudžbu.
<p align="center">
    <img src="screenshots/user-order.png" alt="Prozor za kupca" /><br>
</p>

Korisnik može da izabere u gornjem desnom ćošku željenu temu i jezik, čiji se izbor čuva za datog korisnika sljedeći put kada se prijavi na sistem.
<p align="center">
    <img src="screenshots/theme.png" alt="Prozor za odabir teme" />
    <img src="screenshots/language.png" alt="Prozor za odabir jezika" />
</p>

Na sljedećim slikama prikazan je korisnički prozor u svijetloj temi i zelenoj.
<p align="center">
    <img src="screenshots/user-light.png" alt="Svijetla tema" />
    <img src="screenshots/user-green.png" alt="Zelena tema" />
</p>

Na sljedećoj slici prikazan je korisnički prozor na engleskom.
<p align="center">
    <img src="screenshots/user-english.png" alt="Prozor na engleskom" />
</p>

### Naručivanje proizvoda 

Korisnik može da filtrira željene proizvode po odabiru kategorije, da ukuca ime proizvoda ili njegov opis.

<p align="center">
    <img src="screenshots/user-order-category.png" alt="Izaberi kategoriju" /><br>
    <img src="screenshots/user-order-category-list.png" alt="Odabir kategorije" /><br>
    <img src="screenshots/user-order-search.png" alt="Trazi proizvod" />
</p>

Filtriranje po kategoriji:
<p align="center">
    <img src="screenshots/user-order-by-category.png" alt="Pretraga po kategoriji" />
</p>

Filtriranje po kucanju ključnih riječi:
<p align="center">
    <img src="screenshots/user-order-by-search.png" alt="Pretraga po kljucnim rijecima" />
</p>

### 👤 Funkcionalnosti za kupce
- **Registracija i prijava**: Kreiranje novog naloga i prijava u sistem
- **Pregled proizvoda**: Pretraga i filtriranje proizvoda po kategorijama
- **Korpa za kupovinu**: Dodavanje proizvoda u korpu i upravljanje količinama
- **Kupon kodovi**: Unos kupon kodova za popuste
- **Adresa dostave**: Unos i čuvanje adresa za dostavu
- **Plaćanje**: Više metoda plaćanja (kartica, gotovina, PayPal)
- **Istorija porudžbina**: Pregled prethodnih porudžbina i njihovog statusa

### 🔧 Funkcionalnosti za administratore
- **Upravljanje proizvodima**: Dodavanje, izmena i brisanje proizvoda
- **Upravljanje kategorijama**: Kreiranje i upravljanje kategorijama proizvoda
- **Upravljanje kupovima**: Kreiranje i upravljanje kupon kodovima
- **Upravljanje korisnicima**: Pregled svih korisničkih naloga
- **Upravljanje porudžbinama**: Pregled i promena statusa porudžbina
- **Izveštaji**: Pregled statistika prodaje

## 🛠️ Tehnologije

- **.NET 8.0**: Glavni framework
- **WPF**: Korisnički interfejs
- **Material Design**: UI komponente
- **Entity Framework Core**: ORM za bazu podataka
- **MySQL**: Baza podataka
- **BCrypt**: Enkripcija lozinki
- **Newtonsoft.Json**: JSON serijalizacija

## 💻 Sistemski zahtevi

- **Operativni sistem**: Windows 7 ili noviji
- **.NET Runtime**: .NET 8.0 Desktop Runtime
- **MySQL Server**: 8.0 ili noviji
- **RAM**: Minimum 4GB
- **Prostor na disku**: 500MB slobodnog prostora

## 🚀 Instalacija i pokretanje

### 1. Preuzimanje i kloniranje
```bash
# Kloniranje repozitorijuma
git clone https://github.com/vas-username/ProdavnicaApp.git

# Ulazak u direktorijum projekta
cd ProdavnicaApp
```

### 2. Instalacija .NET 8.0
Preuzmite i instalirajte .NET 8.0 Desktop Runtime sa [Microsoft sajta](https://dotnet.microsoft.com/download/dotnet/8.0).

### 3. Instalacija MySQL Server
1. Preuzmite MySQL Server sa [MySQL sajta](https://dev.mysql.com/downloads/mysql/)
2. Instalirajte MySQL Server
3. Zabilježite root lozinku

### 4. Pokretanje aplikacije
```bash
# Restauracija NuGet paketa
dotnet restore

# Kompajliranje projekta
dotnet build

# Pokretanje aplikacije
dotnet run
```

## 🗄️ Konfiguracija baze podataka

### 1. Kreiranje baze podataka
```sql
CREATE DATABASE prodavnicadb;
USE prodavnicadb;
```

### 2. Konfiguracija konekcije
Otvorite `App.config` fajl i izmenite connection string:

```xml
<connectionStrings>
    <add name="connectionString" 
         providerName="MySql.Data.MySqlClient" 
         connectionString="Server=localhost;Port=3306;Database=prodavnicadb;UserId=root;Password=VASA_LOZINKA;" />
</connectionStrings>
```

### 3. Migracija baze podataka
```bash
# Kreiranje migracije
dotnet ef migrations add InitialCreate

# Primena migracije na bazu
dotnet ef database update
```

## 📖 Uputstvo za korišćenje

### 👤 Korišćenje kao kupac

#### 1. Registracija novog naloga
1. Pokrenite aplikaciju
2. Kliknite na "Register here" link
3. Popunite sve obavezne polja:
   - **First Name**: Vaše ime
   - **Last Name**: Vaše prezime
   - **Email**: Vaša email adresa
   - **Password**: Lozinka (minimum 8 karaktera, jedno veliko slovo, jedno malo slovo, jedan broj i jedan specijalni simbol)
   - **Confirm Password**: Potvrda lozinke
4. Kliknite "Register" dugme

#### 2. Prijava u sistem
1. Unesite email adresu
2. Unesite lozinku
3. Kliknite "Login" dugme

#### 3. Pregled i kupovina proizvoda
1. **Odabir kategorije**: Iz padajuće liste odaberite kategoriju proizvoda
2. **Pretraga proizvoda**: Koristite polje za pretragu da pronađete specifične proizvode
3. **Dodavanje u korpu**:
   - Odaberite proizvod iz liste
   - Unesite željenu količinu
   - Kliknite "Order" dugme
4. **Završavanje kupovine**:
   - Kliknite "Finish Order" dugme
   - Unesite kupon kod (opciono)
   - Kliknite "Confirm"

#### 4. Unos adrese dostave
1. Popunite sva polja:
   - **Street**: Ulica i broj
   - **City**: Grad
   - **Post Code**: Poštanski broj
   - **Country**: Država
   - **Address Type**: Odaberite "Delivery"
2. Kliknite "Confirm"

#### 5. Plaćanje
1. Odaberite metodu plaćanja:
   - **Kartica**: Kreditna/debitna kartica
   - **Gotovina**: Plaćanje prilikom preuzimanja
   - **PayPal**: PayPal nalog
2. Kliknite "Confirm"

#### 6. Pregled porudžbina
1. Idite na "Orders history" tab
2. Pregledajte sve vaše porudžbine
3. Pratite status svake porudžbine

### 🔧 Korišćenje kao administrator

#### 1. Prijava kao admin
1. Prijavite se sa admin nalogom
2. Sistem će vas automatski preusmeriti na admin panel

#### 2. Upravljanje kategorijama
1. Idite na "Products and categories" tab
2. **Dodavanje kategorije**:
   - Unesite naziv kategorije
   - Kliknite "Add" dugme
3. **Brisanje kategorije**: Odaberite kategoriju i kliknite "Delete"

#### 3. Upravljanje proizvodima
1. **Dodavanje proizvoda**:
   - Odaberite kategoriju
   - Unesite naziv proizvoda
   - Unesite opis
   - Unesite cenu (u KM)
   - Unesite količinu na stanju
   - Kliknite "Add Product"
2. **Izmena proizvoda**: Odaberite proizvod i kliknite "Edit"
3. **Brisanje proizvoda**: Odaberite proizvod i kliknite "Delete"

#### 4. Upravljanje kupovima
1. Idite na "Coupons" tab
2. **Dodavanje kupona**:
   - Unesite kod kupona
   - Unesite procenat popusta (0-100)
   - Odaberite datum isteka
   - Kliknite "Add Coupon"

#### 5. Upravljanje porudžbinama
1. Idite na "Orders" tab
2. Pregledajte sve porudžbine
3. **Promena statusa**:
   - Odaberite porudžbinu
   - Kliknite "Change" dugme
   - Odaberite novi status:
     - **In process**: U obradi
     - **Sent**: Poslato
     - **Finished**: Završeno
   - Kliknite "Save"

#### 6. Upravljanje korisnicima
1. Idite na "User accounts" tab
2. Pregledajte sve registrovane korisnike
3. Pratite aktivnost korisnika

### 🌐 Promena jezika
1. Kliknite na zastavu u gornjem desnom uglu
2. Odaberite željeni jezik (srpski/engleski)

### 🎨 Promena teme
1. Kliknite na dugme za temu u gornjem delu aplikacije
2. Odaberite željenu temu:
   - **Light**: Svetla tema
   - **Dark**: Tamna tema
   - **Green**: Zelena tema

## 📁 Struktura projekta

```
WpfApp1/
├── DAL/                    # Data Access Layer
│   ├── Database.cs        # Konfiguracija baze podataka
│   ├── KorisnikDAO.cs     # Operacije sa korisnicima
│   ├── ProizvodDAO.cs     # Operacije sa proizvodima
│   └── ...                # Ostali DAO klase
├── Models/                 # Modeli podataka
│   ├── Korisnik.cs        # Korisnik model
│   ├── Proizvod.cs        # Proizvod model
│   └── ...                # Ostali modeli
├── Views/                  # Korisnički interfejs
│   ├── LoginView.xaml     # Prijava
│   ├── MainView.xaml      # Glavni meni
│   ├── AdminView.xaml     # Admin panel
│   └── ...                # Ostali view-ovi
├── Services/              # Servisi
│   └── AuthService.cs     # Autentifikacija
├── Resources/             # Resursi
│   ├── StringResources.en.xaml  # Engleski stringovi
│   ├── StringResources.sr.xaml  # Srpski stringovi
│   └── ...                # Slike i ikone
└── App.xaml               # Glavna aplikacija
```

## 🔧 Rešavanje problema

### Česti problemi i rešenja

#### 1. Greška pri povezivanju sa bazom podataka
- **Problem**: "Unable to connect to database"
- **Rešenje**: 
  - Proverite da li je MySQL Server pokrenut
  - Proverite connection string u `App.config`
  - Proverite da li su kredencijali tačni

#### 2. Greška pri kompajliranju
- **Problem**: "Build failed"
- **Rešenje**:
  - Proverite da li je .NET 8.0 instaliran
  - Pokrenite `dotnet restore`
  - Proverite da li su svi NuGet paketi instalirani

#### 3. Aplikacija se ne pokreće
- **Problem**: "Application failed to start"
- **Rešenje**:
  - Proverite sistemske zahteve
  - Proverite da li su svi dependencies instalirani
  - Pokušajte pokretanje iz komandne linije za detaljnije greške

#### 4. Problemi sa jezikom
- **Problem**: Stringovi se ne prikazuju na odabranom jeziku
- **Rešenje**:
  - Proverite da li su resource fajlovi uključeni u projekat
  - Proverite da li su putanje do resource fajlova tačne

## 📞 Podrška

Za dodatnu podršku ili prijavu grešaka:
- Otvorite issue na GitHub repozitorijumu
- Kontaktirajte developere putem email-a

## 📄 Licenca

Ovaj projekat je licenciran pod MIT licencom. Pogledajte [LICENSE](LICENSE) fajl za detalje.

---

**Napomena**: Ova aplikacija je razvijena kao deo akademskog projekta za predmet HCI (Human-Computer Interaction). Za produkcijsko korišćenje preporučuje se dodatna testiranja i sigurnosne provere.
