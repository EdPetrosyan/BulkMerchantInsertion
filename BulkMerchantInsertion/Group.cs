using System.Text.Json.Serialization;

namespace BulkMerchantInsertion;
internal record Group
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("fee")]
    public double Fee { get; init; }

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; init; }

    public List<TerchantTerm> Terms { get; init; } = [];
}

internal record TerchantTerm
{
    public double Fee { get; init; }
    public double Term { get; init; }
    public double InterestRate { get; init; }
    public double MinimumAmount { get; init; }
    public double MaximumAmount { get; init; }
}
