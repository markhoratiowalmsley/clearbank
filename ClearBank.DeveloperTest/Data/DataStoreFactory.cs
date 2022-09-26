using System;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Data
{
    /// <summary>
    /// Not a fan of writing application code that is used to build something for testing.
    /// This should really be done in Dependency injection and the correct store would be injected
    /// at service composition, but we don't have DI so this will do. That would keep the code cleaner.
    /// </summary>
    public class DataStoreFactory : IDataStoreFactory
    {
        private readonly DataOptions _options;

        public DataStoreFactory(IOptions<DataOptions> options)
        {
            // The standard is now to pass configuration by IOptions rather than use Configuration["BLA"] as it allows strongly typed config.
            // It also gives you more flexibility for the chance to use monitors etc.
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public IDataStore BuildDataStore()
        {
            if (_options.DataStoreType.Equals(DataStoreType.Backup))
            {
                return new BackupAccountDataStore();
            }
            else
            {
                return new AccountDataStore();
            }
        }
    }
}
