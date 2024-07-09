namespace BulkMerchantInsertion;
internal record Category
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
