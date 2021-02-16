namespace ConsoleApplication1 {
    public class Book {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString() => $"{Id} {Title}";
    }
}