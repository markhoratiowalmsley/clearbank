// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validation;
using MELT;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private readonly ITestLoggerFactory _loggerFactory;
        private readonly PaymentService _paymentService;
        private readonly Mock<IDataStore> _dataStore;
        private readonly Mock<IDataStoreFactory> _dataStoreFactory;

        private const string AccountNumber = "1234";

        public PaymentServiceTests()
        {
            _dataStore = new Mock<IDataStore>();

            _dataStoreFactory = new Mock<IDataStoreFactory>();
            _dataStoreFactory.Setup(x => x.BuildDataStore())
                .Returns(_dataStore.Object);

            //Should mock at this level really but this is quicker
            var validator = new FasterPaymentValidator();
            var validators = new List<IValidator> { validator };

            _loggerFactory = TestLoggerFactory.Create();

            _paymentService = new PaymentService(
                _dataStoreFactory.Object,
                validators,
                _loggerFactory.CreateLogger<PaymentService>());
        }

        [Fact]
        public void MakePayment_UpdatesAccountIfSuccessful()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100.0m
            };

            var request = new MakePaymentRequest
            {
                Amount = 50.0m,
                PaymentScheme = AllowedPaymentSchemes.FasterPayments,
                DebtorAccountNumber = AccountNumber
            };

            _dataStore.Setup(x => x.GetAccount(AccountNumber))
                .Returns(account);

            var result = _paymentService.MakePayment(request);

            Assert.True(result.Success);
            _dataStore.Verify(x => x.UpdateAccount(account), Times.Once);
        }

        [Fact]
        public void MakePayment_UpdatesAccountIfUnSuccessful()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100.0m,
                AccountNumber = AccountNumber
            };

            var request = new MakePaymentRequest
            {
                Amount = 200.0m
            };

            _dataStore.Setup(x => x.GetAccount(AccountNumber))
                .Returns(account);

            var result = _paymentService.MakePayment(request);

            Assert.False(result.Success);
            _dataStore.Verify(x => x.UpdateAccount(account), Times.Never);
        }

        [Fact]
        public void MakePayment_LogsIfExceptionThrown()
        {
            // Not really valid but quicker to set up for purpose of the coding challenge.
            _dataStoreFactory.Setup(x => x.BuildDataStore())
                .Throws<InvalidOperationException>();

            var request = new MakePaymentRequest();

            var result = _paymentService.MakePayment(request);

            Assert.False(result.Success);
            Assert.Contains<LogEntry>(
                _loggerFactory.Sink.LogEntries,
                x => x.Message == "MakePayment failed" && x.LogLevel == LogLevel.Error);
        }


    }
}
