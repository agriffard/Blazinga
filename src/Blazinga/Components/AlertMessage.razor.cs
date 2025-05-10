namespace Blazinga.Components;
public partial class AlertMessage
{
    [Parameter] public string CssClass { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
