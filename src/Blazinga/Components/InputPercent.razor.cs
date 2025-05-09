namespace Blazinga.Components;
public partial class InputPercent
{
    [Parameter] public string Class { get; set; } = "form-control";
    [Parameter] public string GroupClass { get; set; }
    [Parameter] public double Value { get; set; }
    [Parameter] public EventCallback<double> ValueChanged { get; set; }
    [Parameter] public Expression<Func<double>> ValueExpression { get; set; }

    private double _value => Value * 100;

    private async Task OnValueChanged(double value)
        => await ValueChanged.InvokeAsync(value / 100);
}
