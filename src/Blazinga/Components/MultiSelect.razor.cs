namespace Blazinga.Components;
/// <summary>
/// MultiSelect component.
/// </summary>
/// <remarks>Inspired by https://github.com/BorisGerretzen/SimpleBlazorMultiselect</remarks>
public partial class MultiSelect<TItem, TValue>
{
    [Parameter] public string Placeholder { get; set; }
    [Parameter] public List<TItem> Items { get; set; } = new List<TItem>();
    [Parameter] public List<TValue> SelectedValues { get; set; } = new List<TValue>();
    [Parameter] public EventCallback<List<TValue>> SelectedValuesChanged { get; set; }
    [Parameter] public Expression<Func<TItem, TValue>> ValueSelector { get; set; }
    [Parameter] public Expression<Func<TItem, string>> TextSelector { get; set; }
    [Parameter] public int Height { get; set; } = 200;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    private bool IsDropdownVisible = false;

    private ElementReference _dropdownContainerRef;
    private DotNetObjectReference<MultiSelect<TItem, TValue>>? _dotNetHelper;
    private IJSObjectReference? _module;
    private IJSObjectReference? _clickHandler;

    protected override void OnInitialized()
    {
        _dotNetHelper = DotNetObjectReference.Create(this);
    }

    [JSInvokable]
    public async Task CloseDropdown()
    {
        if (_clickHandler != null)
        {
            await _clickHandler.InvokeVoidAsync("dispose");
            _clickHandler = null;
        }

        IsDropdownVisible = false;
        await InvokeAsync(StateHasChanged);
    }

    public async ValueTask DisposeAsync()
    {
        if (_clickHandler != null)
        {
            await _clickHandler.InvokeVoidAsync("dispose");
        }

        if (_module != null)
        {
            await _module.DisposeAsync();
        }

        _dotNetHelper?.Dispose();
    }

    private async Task ToggleDropdown()
    {
        IsDropdownVisible = !IsDropdownVisible;
        if (IsDropdownVisible)
        {
            _module ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/blazinga/js/multiselect.js");
            _clickHandler = await _module.InvokeAsync<IJSObjectReference>("register", _dotNetHelper, _dropdownContainerRef);
        }
        else
        {
            await CloseDropdown();
        }
    }

    private void ToggleSelection(TValue value)
    {
        if (SelectedValues.Contains(value))
        {
            SelectedValues.Remove(value);
        }
        else
        {
            SelectedValues.Add(value);
        }

        SelectedValuesChanged.InvokeAsync(SelectedValues);
    }

    private void RemoveSelection(TValue value)
    {
        SelectedValues.Remove(value);
        SelectedValuesChanged.InvokeAsync(SelectedValues);
    }

    // private RenderFragment<TItem> RenderItem => item =>
    //     @<div class="dropdown-item" @onclick="() => ToggleOption(item)">
    //         <label @onclick:stopPropagation="true">
    //             <input type="checkbox" checked="@IsSelected(item)"
    //                    @onchange="() => ToggleOption(item)"/> @TextSelector(item)
    //         </label>
    //     </div>;

    // private async Task ToggleOption(TItem option)
    // {
    //     var newSelected = new List<TItem>(SelectedValues);
    //     if (!newSelected.Remove(option))
    //     {
    //         newSelected.Add(option);
    //     }

    //     SelectedValues = newSelected;
    //     await SelectedValuesChanged.InvokeAsync(SelectedValues);
    // }

    // private bool IsSelected(TItem value)
    // {
    //     return SelectedValues.Contains(value);
    // }
}
