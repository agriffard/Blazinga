namespace Blazinga.Components;
public partial class RadioButtonList<TValue>
{
    [Parameter] public string IdPrefix { get; set; } = Guid.NewGuid().ToString("N");
    [Parameter] public string CssClass { get; set; } = "radio-button-list";
    [Parameter] public string GroupName { get; set; } = Guid.NewGuid().ToString("N");
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public List<RadioButtonItem<TValue>>? Items { get; set; }

    private async Task OnValueChanged(TValue? newValue)
    {
        Value = newValue;
        await ValueChanged.InvokeAsync(Value);
    }
}
