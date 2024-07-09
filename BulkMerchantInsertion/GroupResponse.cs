using System.Text.Json.Serialization;

namespace BulkMerchantInsertion;
internal record GroupResponse
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    [JsonPropertyName("data")]
    public List<Group> Groups { get; init; } = [];
}
