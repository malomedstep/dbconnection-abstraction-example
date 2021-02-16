using System.Collections.Generic;

namespace ConsoleApplication1 {
    public interface IBooksRepository {
        IEnumerable<Book> GetBooks();
        Book FindBook(int id);
        void DeleteBook(int id);
        void UpdateBook(int id, string title);
    }
}