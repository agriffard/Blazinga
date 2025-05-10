namespace Blazinga.Components;
public class RadioButtonItem<T>
{
    public T? Value { get; set; }
    public string Text { get; set; } = string.Empty;

    public RadioButtonItem() { }

    public RadioButtonItem(T? value, string text)
    {
        Value = value;
        Text = text;
    }
}
