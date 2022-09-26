using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStoreFactory _dataStoreFactory;
        private readonly IEnumerable<IValidator> _validators;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IDataStoreFactory dataStoreFactory,
            IEnumerable<IValidator> validators,
            ILogger<PaymentService> logger)
        {
            _dataStoreFactory = dataStoreFactory ?? throw new ArgumentNullException(nameof(dataStoreFactory));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public MakePaymentResult MakePayment(
            MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            try
            {
                var dataStore = _dataStoreFactory.BuildDataStore();
                var account = dataStore.GetAccount(request.DebtorAccountNumber);

                // This technically doesn't handle everything as moved the flags to the request to save time, in theory
                // you could have multiple validators that need to run against the request, this method should change to reflect this
                // but don't have the time to address this.

                // Fluent validator or another package would make sense instead of hand cranked code
                // but not sure it allows you to validate X against Y. Do we want to validate            
                var validator = _validators.First(MatchesPaymentScheme(request));
                result.Success = validator.IsValid(account, request);

                // Could argue to be fully SOLID you would move the payment out of the method
                // I would go with YAGNI for now as it's likely to be in one place and the abstraction
                // wouldn't add much value
                if (result.Success)
                {
                    ProcessBalance(account, request);
                    dataStore.UpdateAccount(account);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MakePayment)} failed", ex);

                // I don't like wrapping exceptions with a bool like this and prefer to rethrow,
                // but as we can't edit the signature this will do.
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// I like splitting out linq statements to private methods for readability.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static Func<IValidator, bool> MatchesPaymentScheme(
            MakePaymentRequest request)
        {
            return x => x.PaymentScheme.Equals(request.PaymentScheme);
        }

        /// <summary>
        /// As the comment above says this could be split outto a separate class but as this
        /// isn't done a private method ensures readability of code.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="request"></param>
        private static void ProcessBalance(
            Account account, 
            MakePaymentRequest request)
        {
            account.Balance -= request.Amount;
        }
    }
}
