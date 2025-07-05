using ProdavnicaApp.Models;

namespace ProdavnicaApp.Helpers
{
    public static class SessionManager
    {
        public static Korisnik? CurrentUser { get; set; }
    }
}
