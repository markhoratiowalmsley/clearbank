using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public interface IDataStore
    {
        /// <summary>
        /// Is string the best type for an account number? Guid or similar.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        Account GetAccount(string accountNumber);

        /// <summary>
        /// This would most likely return a Task and turn Async if the method did something useful.
        /// </summary>
        /// <param name="account"></param>
        void UpdateAccount(Account account);
    }
}
