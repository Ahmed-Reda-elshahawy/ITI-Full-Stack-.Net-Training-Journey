namespace memento.Services;

class TextEditorHistory
{
    private readonly TextEditor _textEditor;
    private Stack<TextEditorMemento> _history = new();
    private Stack<TextEditorMemento> _cachedHistory = new();

    public TextEditorHistory(TextEditor textEditor)
    {
        _textEditor = textEditor;
        _history.Push(textEditor.Save()); // Ensure initial state is saved
    }

    public void Save()
    {
        _history.Push(_textEditor.Save());
        _cachedHistory.Clear(); // Reset redo history
    }

    public void Undo()
    {
        if (_history.Count > 1)
        {
            _cachedHistory.Push(_history.Pop());
            _textEditor.Restore(_history.Peek());
        }
    }

    public void Redo()
    {
        if (_cachedHistory.Count > 0)
        {
            _history.Push(_cachedHistory.Pop());
            _textEditor.Restore(_history.Peek());
        }
    }
}