namespace Blazinga.Components;
public partial class EnumSelect<TEnum>
{
    protected override bool TryParseValueFromString(string value, out TEnum result, out string validationErrorMessage)
    {
        if (BindConverter.TryConvertTo(value, CultureInfo.CurrentCulture, out TEnum parsedValue))
        {
            result = parsedValue;
            validationErrorMessage = "";
            return true;
        }

        if (string.IsNullOrEmpty(value))
        {
            var nullableType = Nullable.GetUnderlyingType(typeof(TEnum));
            if (nullableType != null)
            {
                result = default;
                validationErrorMessage = "";
                return true;
            }
        }

        result = default;
        validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
        return false;
    }
}
