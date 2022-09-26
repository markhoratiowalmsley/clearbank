namespace ClearBank.DeveloperTest.Data
{
    /// <summary>
    /// Modern pattern for config should bind to a POCO using IOptions pattern.
    /// </summary>
    public class DataOptions
    {
        public DataStoreType DataStoreType { get; set; }
    }
}
