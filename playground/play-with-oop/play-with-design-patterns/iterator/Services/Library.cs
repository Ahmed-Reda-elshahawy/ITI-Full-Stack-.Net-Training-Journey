namespace iterator.Services;

class Library:Iterable<Book>
{
    private readonly List<Book> _books = new();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Iterator<Book> CreateIterator()
    {
        return new LibraryIterator(_books);
    }
}
