using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;

public static class AuthService
{
    public static Korisnik? Login(string email, string password)
    {
        var korisnici = KorisnikDAO.GetAll();
        return korisnici.FirstOrDefault(k =>
            k.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
            k.Lozinka == password); 
    }
}
