using memento.Services;

class Program
{
    static void Main()
    {
        TextEditor editor = new();
        TextEditorHistory history = new(editor);

        editor.Type("Hello ");
        history.Save();

        editor.Type("World!");
        history.Save();

        Console.WriteLine("Current Content: " + editor.Content); // "Hello World!"

        history.Undo();
        Console.WriteLine("After Undo: " + editor.Content); // "Hello "

        history.Undo();
        Console.WriteLine("After Second Undo: " + editor.Content); // ""

        history.Redo();
        Console.WriteLine("After Redo: " + editor.Content); // "Hello "

        history.Redo();
        Console.WriteLine("After Second Redo: " + editor.Content); // "Hello World!"
    }
}