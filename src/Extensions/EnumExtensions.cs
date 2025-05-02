namespace Blazinga.Extensions;

public static class EnumExtensions
{
    public static string GetLocalizedDisplayName(this Enum enumValue)
    {
        var displayAttribute = enumValue.GetType()
                                        .GetField(enumValue.ToString())
                                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;

        if (displayAttribute != null && displayAttribute.ResourceType != null)
        {
            var resourceManager = new ResourceManager(displayAttribute.ResourceType);
            return resourceManager.GetString(displayAttribute.Name) ?? enumValue.ToString();
        }

        return enumValue.ToString();
    }
}
