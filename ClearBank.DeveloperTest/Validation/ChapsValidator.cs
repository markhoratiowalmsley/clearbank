using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public class ChapsValidator : IValidator
    {
        public AllowedPaymentSchemes PaymentScheme => AllowedPaymentSchemes.Chaps;

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
                && account.Status == AccountStatus.Live;
        }
    }
}
