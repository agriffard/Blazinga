namespace Blazinga.Components;
public partial class Alert
{
    [Parameter] public string CssClass { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
