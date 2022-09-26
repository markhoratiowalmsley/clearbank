// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validation;

namespace ClearBank.DeveloperTest.Tests.Validation
{
    public class ValidationTests
    {
        [Theory]
        [InlineData(false, AllowedPaymentSchemes.FasterPayments, 100.0, 10.0, true)]
        [InlineData(true, AllowedPaymentSchemes.FasterPayments, 100.0, 10.0, false)]
        [InlineData(false, AllowedPaymentSchemes.Bacs, 100.0, 0.0, false)]
        [InlineData(false, AllowedPaymentSchemes.Chaps, 100.0, 10.0, false)]
        [InlineData(false, AllowedPaymentSchemes.FasterPayments, 0.0, 100.0, false)]
        public void FasterPaymentValidator_IsValid(
            bool accountIsNull,
            AllowedPaymentSchemes requestPaymentScheme,
            decimal accountBalance,
            decimal requestAmount,
            bool expected)
        {
            Account account = null;

            if (!accountIsNull)
            {
                account = new Account
                {
                    Balance = accountBalance,
                    AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
                };
            }

            var request = new MakePaymentRequest
            {
                Amount = requestAmount,
                PaymentScheme = requestPaymentScheme
            };


            var validator = new FasterPaymentValidator();
            var isValid = validator.IsValid(account, request);

            Assert.Equal(expected, isValid);
        }

        [Theory]
        [InlineData(false, AllowedPaymentSchemes.Chaps, AccountStatus.Live, true)]
        [InlineData(true, AllowedPaymentSchemes.Chaps, AccountStatus.Live, false)]
        [InlineData(false, AllowedPaymentSchemes.FasterPayments, AccountStatus.Live, false)]
        [InlineData(false, AllowedPaymentSchemes.Bacs, AccountStatus.Live, false)]
        [InlineData(false, AllowedPaymentSchemes.Chaps, AccountStatus.InboundPaymentsOnly, false)]
        [InlineData(false, AllowedPaymentSchemes.Chaps, AccountStatus.Disabled, false)]
        public void ChapsValidator_IsValid(
            bool accountIsNull,
            AllowedPaymentSchemes requestPaymentScheme,
            AccountStatus status,
            bool expected)
        {
            Account account = null;

            if (!accountIsNull)
            {
                account = new Account
                {
                    Status = status,
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
                };
            }

            var request = new MakePaymentRequest
            {
                PaymentScheme = requestPaymentScheme
            };


            var validator = new ChapsValidator();
            var isValid = validator.IsValid(account, request);
            Assert.Equal(expected, isValid);
        }

        [Theory]
        [InlineData(false, AllowedPaymentSchemes.Bacs, true)]
        [InlineData(true, AllowedPaymentSchemes.Bacs, false)]
        [InlineData(false, AllowedPaymentSchemes.FasterPayments, false)]
        [InlineData(false, AllowedPaymentSchemes.Chaps, false)]
        public void BacsValidator_IsValid(
            bool accountIsNull,
            AllowedPaymentSchemes requestPaymentScheme,
            bool expected)
        {
            Account account = null;

            if (!accountIsNull)
            {
                account = new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
                };
            }

            var request = new MakePaymentRequest
            {
                PaymentScheme = requestPaymentScheme
            };

            var validator = new BacsValidator();
            var isValid = validator.IsValid(account, request);
            Assert.Equal(expected, isValid);
        }
    }
}
