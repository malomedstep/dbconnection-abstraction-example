using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1 {
    public class BooksRepository : IBooksRepository {
        private readonly SqlDatabase _db;

        public BooksRepository(SqlDatabase db) {
            _db = db;
        }

        public IEnumerable<Book> GetBooks() {
            using (var conn = _db.GetConnection()) {
                return conn.ExecuteReader<Book>("SELECT * FROM Books");
            }
        }

        public Book FindBook(int id) {
            using (var conn = _db.GetConnection()) {
                return conn.ExecuteReader<Book>("SELECT * FROM Books WHERE Id = @Id", new {Id = id}).FirstOrDefault();
            }
        }

        public void DeleteBook(int id) {
            _db.ExecuteInTransaction((conn, trans) => {
                conn.ExecuteNonQuery("DELETE FROM Books WHERE Id = @Id", new {Id = id}, trans);
            });
        }

        public void UpdateBook(int id, string title) {
            _db.ExecuteInTransaction((conn, trans) => {
                conn.ExecuteNonQuery("UPDATE Books SET Title = @Title WHERE Id = @Id", new {Id = id, Title = title}, trans);
            });
        }
    }
}