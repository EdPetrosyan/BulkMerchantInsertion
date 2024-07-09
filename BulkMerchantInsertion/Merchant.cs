using System.Text.Json.Serialization;

namespace BulkMerchantInsertion;
internal record Merchant
{
    [JsonPropertyName("id")]
    public int? Id { get; init; }

    [JsonPropertyName("clientId")]
    public string? ClientId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("accountNumber")]
    public string? AccountNumber { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("community")]
    public string? Community { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("street")]
    public string? Street { get; init; }

    [JsonPropertyName("building")]
    public string? Building { get; init; }

    [JsonPropertyName("zipCode")]
    public string? ZipCode { get; init; }

    [JsonPropertyName("area")]
    public string? Area { get; init; }

    [JsonPropertyName("categoryId")]
    public int? CategoryId { get; init; }

    [JsonPropertyName("merchantTypeId")]
    public int? MerchantTypeId { get; init; }

    [JsonPropertyName("iconPath")]
    public string? IconPath { get; init; }

    [JsonPropertyName("fee")]
    public double? Fee { get; init; }

    [JsonPropertyName("isActive")]
    public bool? IsActive { get; init; }

    [JsonPropertyName("cashback")]
    public double? Cashback { get; init; }

    [JsonPropertyName("groupId")]
    public int? GroupId { get; init; }
}
