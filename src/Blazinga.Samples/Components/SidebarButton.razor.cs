namespace Blazinga.Samples.Components;
public partial class SidebarButton
{
    [Inject] NavMenuStateService NavState { get; set; } = default!;

    private async Task ToggleNav()
    {
        await NavState.ToggleAsync();
    }
}
