namespace iterator.Services;

class LibraryIterator : Iterator<Book>
{
    private readonly List<Book> _books;
    private int _iteration = 0;

    public LibraryIterator(List<Book> books)
    {
        _books = books;
    }

    public Book GetNext()
    {
        return _books[_iteration++];
    }

    public bool HasNext()
    {
        return _iteration < _books.Count;
    }
}
