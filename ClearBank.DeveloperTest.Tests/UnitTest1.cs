using ClearBank.DeveloperTest.Data;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Tests;

public class DataStoreFactoryTests
{
    [Theory]
    [InlineData(DataStoreType.Backup, typeof(BackupAccountDataStore))]
    [InlineData(DataStoreType.Default, typeof(AccountDataStore))]
    [InlineData((DataStoreType)10, typeof(AccountDataStore))]
    public void Test1(DataStoreType configuredType, Type storeType)
    {
        var options = new DataOptions
        {
            DataStoreType = configuredType
        };

        var factory = new DataStoreFactory(Options.Create(options));

        var dataStore = factory.BuildDataStore();

        Assert.IsAssignableFrom(storeType, dataStore);
    }
}
