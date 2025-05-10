namespace Blazinga.Samples.Components;
public partial class SidebarButton
{
    private async Task ToggleNav()
    {
        await NavState.ToggleAsync();
    }
}
