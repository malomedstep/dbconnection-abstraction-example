using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace ConsoleApplication1 {
    internal class Program {
        public static void Main(string[] args) {
            var connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;

            var db = new SqlDatabase(connStr, providerName);
            var repo = new BooksRepository(db);

            var books = repo.GetBooks();

            foreach (var book in books) {
                Console.WriteLine(book);
            }

            Console.WriteLine();

            Console.WriteLine(repo.FindBook(2));
        }
    }
}