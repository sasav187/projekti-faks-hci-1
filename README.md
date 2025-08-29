#  ProdavnicaApp - WPF Aplikacija za Upravljanje Prodavnicom

##  Sadržaj
- [O aplikaciji](#o-aplikaciji)
- [Prijava na sistem](#prijava)
  - [Registracija na sistem](#registracija)
- [Prozor za običnog korisnika](#prozor-za-obicnog-korisnika)
  - [Odabir teme i jezika](#odabir-teme-i-jezika) 
  - [Naručivanje proizvoda](#narucivanje-proizvoda)
  - [Pregled prethodnih narudžbi](#istorija-narudzbi)
- [Prozor za admina](#prozor-za-admina)
  - [Proizvodi i kategorije](#proizvodi-i-kategorije)
  - [Pregled korisničkih naloga](#korisnicki-nalozi)
  - [Pregled prethodnih narudžbi](#prethodne-narudzbe)
  - [Pregled kupona](#kuponi)

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

### <span id="registracija"> Registracija na sistem

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

## <span id="prozor-za-obicnog-korisnika"> Prozor za običnog korisnika (kupca)

Korisnik kada se prijavi na sistem, prikazuje mu se glavni korisnički prozor na kojem može da napravi narudžbu.
<p align="center">
    <img src="screenshots/user-order.png" alt="Prozor za kupca" /><br>
</p>

### <span id="odabir-teme-i-jezika"> Odabir teme i jezika

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

### <span id="narucivanje-proizvoda"> Naručivanje proizvoda 

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

Kada korisnik odabere neki proizvod koji želi da naruči treba da upiše količinu proizvoda koju želi da naruči u odgovarajućem tekst boksu.
<p align="center">
    <img src="screenshots/user-order-button.png" alt="Naruci" />
</p>

Kada izabere količinu proizvod se dodaje u korpu i ispisuje se odgovarajuća poruka na status baru.
<p align="center">
    <img src="screenshots/status-bar-order.png" alt="Proizvod dodat u korpu" />
</p>

Ukoliko nije dostupna količina proizvoda ispisaće se odgovarajuća poruka na status baru.
<p align="center">
    <img src="screenshots/status-bar-error.png" alt="Nedovoljna kolicina proizvoda na stanju" />
</p>

Prije nego što završi narudžbu korisnik može da unese odgovarajući kupon kod koji bi mu dao određeni popust.
<p align="center">
    <img src="screenshots/user-order-coupon.png" alt="Unesi kupon" />
</p>

Korisnik kada odabere sve proizvode koje želi da naruči, da završi narudžbu bira odgovarajuće dugme za završetak narudžbe, nakon koje se otvara prozor za potvrdu narudžbe na kojem se nalaze detalji narudžbe.
<p align="center">
    <img src="screenshots/user-order-finish.png" alt="Zavrsi narudzbu" /><br>
    <img src="screenshots/confirm-order.png" alt="Potvrdi narudzbu" />
</p>

Kada korisnik potvrdi narudžbu otvara se prozor za unos adrese narudžbe, nakon unosa otvara se prozor za odabir načina plaćanja.
<p align="center">
    <img src="screenshots/order-address.png" alt="Detalji adrese narudzbe" /><br>
    <img src="screenshots/payment.png" alt="Nacin placanja" />
</p>

Nakon uspješnog plaćanja dobija se odgovarajuća poruka sistema.
<p align="center">
    <img src="screenshots/payment-successful.png" alt="Placanje uspjesno" />
</p>

### <span id="istorija-narudzbi"> Pregled prethodnih narudžbi

Korisnik ima uvid u svoje prethodne narudžbe.
<p align="center">
    <img src="screenshots/order-history.png" alt="Istorija narudzbi" />
</p>

Korisnik može da odabere neku narudžbu, nakon čega se otvara prozor koji prikazuje detalje te narudžbe.
<p align="center">
    <img src="screenshots/order-details.png" alt="Detalji narudzbe" />
</p>

## <span id="prozor-za-admina"> Prozor za admina

Admin meni nudi niz opcija od kojih je podrazumijevana dodavanje, uređivanje i brisanje proizvoda i kategorija. 

### <span id="proizvodi-i-kategorije"> Proizvodi i kategorije
<p align="center">
    <img src="screenshots/admin-products.png" alt="Admin glavni" />
</p>

Admin može da izabere kategoriju ili proizvod koji želi da promijeni ili obriše klikom na odgovorajući podatak u listi. Takođe, isto može da uradi kucanjem u tekst box ili ako želi da doda neku novu kategoriju ili proizvod.
<p align="center">
    <img src="screenshots/admin-category.png" alt="Admin kategorije" />
  <img src="screenshots/admin-product.png" alt="Admin proizvodi" />
</p>

### <span id="korisnicki-nalozi"> Pregled korisničkih naloga

Admin ima i uvid u sve korisničke naloge i njihove osnovne podatke.
<p align="center">
    <img src="screenshots/admin-users.png" alt="Admin korisnici" />
</p>

### <span id="prethodne-narudzbe"> Pregled prethodnih narudžbi

Admin ima uvid u sve narudžbe svih korisnika gdje može da mijenja status narudžbe klikom na odgovarajuću narudžbu.
<p align="center">
    <img src="screenshots/admin-orders.png" alt="Admin narudzbe" />
    <img src="screenshots/order-status.png" alt="Admin status narudzbe" />
    <img src="screenshots/order-status-list.png" alt="Admin status narudzbe lista" />
</p>

Kada promijeni status narudžbe, sistem šalje odgovarajuću poruku.
<p align="center">
    <img src="screenshots/status-updated.png" alt="Potvrda promjene statusa" />
</p>

### <span id="kuponi"> Pregled kupona

Admin ima i mogućnost pregleda svih kupona koje korisnici mogu da koriste da bi dobili određeni popust, kao i dodavanje istih.
<p align="center">
    <img src="screenshots/admin-coupons.png" alt="Admin kuponi" />
    <img src="screenshots/add-coupon.png" alt="Dodaj kupon" /><br>
    <img src="screenshots/coupon-date.png" alt="Datum kupona" />
</p>

Nakon dodavanja kupona, sistem šalje odgovarajuću poruku.
<p align="center">
    <img src="screenshots/coupon-added.png" alt="Kupon dodat" />
</p>
