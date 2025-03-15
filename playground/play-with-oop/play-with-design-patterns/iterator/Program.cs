using iterator.Services;

class Program
{
    static void Main()
    {
        Library library = new Library();
        library.AddBook(new Book("Design Patterns"));
        library.AddBook(new Book("Clean Code"));
        library.AddBook(new Book("Refactoring"));

        Iterator<Book> iterator = library.CreateIterator();

        while (iterator.HasNext())
        {
            Book book = iterator.GetNext();
            Console.WriteLine("Reading: " + book.Title);
        }
    }
}