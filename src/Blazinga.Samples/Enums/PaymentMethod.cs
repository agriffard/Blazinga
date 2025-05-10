namespace Blazinga.Samples.Enums;

public enum PaymentMethod
{
    [Display(Name = nameof(EnumsRes.PaymentMethod_Card), ResourceType = typeof(EnumsRes))]
    Card,
    [Display(Name = nameof(EnumsRes.PaymentMethod_PayPal), ResourceType = typeof(EnumsRes))]
    PayPal,
    [Display(Name = nameof(EnumsRes.PaymentMethod_BankTransfer), ResourceType = typeof(EnumsRes))]
    BankTransfer
}
