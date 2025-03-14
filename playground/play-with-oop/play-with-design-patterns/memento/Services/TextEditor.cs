using System.Text;

namespace memento.Services;

class TextEditor
{
    private StringBuilder _contentBuffer = new StringBuilder();
    public string Content => _contentBuffer.ToString();

    public void Type(string words)
    {
        _contentBuffer.Append(words);
    }

    public TextEditorMemento Save()
    {
        return new(Content);
    }

    public void Restore(TextEditorMemento memento)
    {
        _contentBuffer = new StringBuilder(memento.Content);
    }
}
