namespace iterator.Services;

interface Iterable<T>
{
    Iterator<T> CreateIterator();
}
