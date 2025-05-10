namespace Blazinga.Components;
public partial class Badge
{
    [Parameter] public string CssClass { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
