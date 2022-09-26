using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{

    /// <summary>
    /// Validator to be typed to a single PaymentSchemeType
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// The PaymentScheme that this validator can act on.
        /// </summary>
        public AllowedPaymentSchemes PaymentScheme { get; }

        /// <summary>
        /// Using MakePaymentRequest could be a bit of a leaky abstraction.
        /// Used to check the request is valid for a given account.
        /// </summary>
        /// <returns></returns>
        public bool IsValid(
            Account account,
            MakePaymentRequest request);
    }
}
