namespace Blazinga.Components;
public partial class CheckboxList<TItem>
{
    [Parameter] public IEnumerable<TItem> Items { get; set; }
    [Parameter] public Func<TItem, string> TextField { get; set; }
    [Parameter] public Func<TItem, int> ValueField { get; set; }
    [Parameter] public int[] SelectedValues { get; set; }
    [Parameter] public EventCallback<int[]> SelectedValuesChanged { get; set; }

    public async void OnChange(int selectedId, object isChecked)
    {
        if ((bool)isChecked)
        {

            if (!SelectedValues.Contains(selectedId))
            {
                var newArray = new int[SelectedValues.Length + 1];

                for (var i = 0; i < SelectedValues.Length; i++)
                {
                    newArray[i] = SelectedValues[i];
                }
                newArray[newArray.Length - 1] = selectedId;
                SelectedValues = newArray;
            }
        }
        else
        {
            if (SelectedValues.Contains(selectedId))
            {
                var newArray = SelectedValues.Where(x => x != selectedId).ToArray();
                SelectedValues = newArray;
            }
        }
        await SelectedValuesChanged.InvokeAsync(SelectedValues);
    }
}
