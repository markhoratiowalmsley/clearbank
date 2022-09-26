namespace ClearBank.DeveloperTest.Types
{
    /// <summary>
    /// Left in to completeness but I exposed AllowablePaymentSchemes to the root.
    /// Again this was a bit of an assumption but it sped up dev.
    /// </summary>
    public enum PaymentScheme
    {
        FasterPayments,
        Bacs,
        Chaps
    }
}
