namespace Blazinga.Samples.Enums;

public enum Status
{
    [Display(Name = nameof(EnumsRes.Status_Active), ResourceType = typeof(EnumsRes))]
    Active,
    [Display(Name = nameof(EnumsRes.Status_Closed), ResourceType = typeof(EnumsRes))]
    Closed,
    [Display(Name = nameof(EnumsRes.Status_Pending), ResourceType = typeof(EnumsRes))]
    Pending
}
