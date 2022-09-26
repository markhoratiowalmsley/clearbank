namespace ClearBank.DeveloperTest.Types
{
    public class Account
    {
        /// <summary>
        /// Not sure a string is the best type for this.
        /// Would a guid or similar be more useful?
        /// </summary>
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }
    }
}
