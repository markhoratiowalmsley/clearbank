using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public class FasterPaymentValidator : IValidator
    {
        public AllowedPaymentSchemes PaymentScheme => AllowedPaymentSchemes.FasterPayments;

        /// <summary>
        /// Original if statements have been inverted to check for validity
        /// </summary>
        /// <param name="account"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsValid(
            Account account,
            MakePaymentRequest request)
        {
            return account is not null
                && account.AllowedPaymentSchemes.HasFlag(request.PaymentScheme)
                && account.Balance >= request.Amount;
        }
    }
}
