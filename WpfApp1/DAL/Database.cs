using System.Configuration;

namespace ProdavnicaApp.DAL
{
    public static class Database
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    }
}
