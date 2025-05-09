namespace Blazinga.Components;
public partial class BadgeLabel
{
    [Parameter] public string CssClass { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
