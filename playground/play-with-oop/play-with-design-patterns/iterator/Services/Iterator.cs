namespace iterator.Services;

interface Iterator<T>
{
    T GetNext();
    bool HasNext();
}
