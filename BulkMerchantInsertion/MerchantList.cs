using System.Text.Json.Serialization;

namespace BulkMerchantInsertion;
internal class MerchantList
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    [JsonPropertyName("data")]
    public List<Merchant> Data { get; init; } = [];
}
