namespace BulkMerchantInsertion;
internal record MerchantFromCsv
{
    public int PartnerId { get; init; }
    public string BrandName { get; init; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string PartnerCountry { get; init; } = "ՀՀ";
    public string PartnerCommunity { get; init; } = "YEREVAN";
    public string PartnerCity { get; init; } = "YEREVAN";
    public string PartnerStreet { get; init; } = string.Empty;
    public string PartnerBuilding { get; init; } = string.Empty;
    public string PartnerZipcode { get; init; } = string.Empty;
    public string PartnerCategory { get; init; } = string.Empty;
    public string PartnerAccountNumber { get; init; } = string.Empty;
    public double QrFee { get; init; }
    public string ClientCode { get; init; } = string.Empty;

}
