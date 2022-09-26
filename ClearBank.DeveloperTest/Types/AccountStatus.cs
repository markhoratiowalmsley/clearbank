namespace ClearBank.DeveloperTest.Types
{
    public enum AccountStatus
    {
        Default, // Added to ensure the default value isn't Live.
        Live,
        Disabled,
        InboundPaymentsOnly
    }
}
