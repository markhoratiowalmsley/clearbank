using System;

namespace ClearBank.DeveloperTest.Types
{
    /// <summary>
    /// A bit of an assumption but I think the Flags attribute was missing.
    /// </summary>
    [Flags]
    public enum AllowedPaymentSchemes
    {
        None = 0, //Default should be added to enums to prevent the default value falling to an actual value
        FasterPayments = 1 << 0,
        Bacs = 1 << 1,
        Chaps = 1 << 2
    }
}
