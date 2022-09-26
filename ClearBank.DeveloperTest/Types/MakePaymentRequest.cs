using System;

namespace ClearBank.DeveloperTest.Types
{
    public class MakePaymentRequest
    {
        /// <summary>
        /// string is probably not the optimal thing for this
        /// </summary>
        public string CreditorAccountNumber { get; set; }

        /// <summary>
        /// string is probably not the optimal thing for this
        /// </summary>
        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// DateTimeOffset should be used instead for extra precision of TimeZone.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Exposed the Allowed enum to this level to make the code tidier.
        /// Probably a leaky abstraction and kind of change the signature to allow multiple flags.
        /// </summary>
        public AllowedPaymentSchemes PaymentScheme { get; set; }
    }
}
