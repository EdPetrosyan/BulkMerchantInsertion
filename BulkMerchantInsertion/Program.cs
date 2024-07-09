using BulkMerchantInsertion;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

_ = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    NewLine = Environment.NewLine,
};

List<MerchantFromCsv> listOfMerchants = [];

using (var reader = new StreamReader("C:\\Users\\Eduard H. Petrosyan\\Desktop\\100 merchant's info_V2.txt"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<MerchantFromCsv>();
    listOfMerchants = records.ToList();
}

if (listOfMerchants.Count > 0)
{
    using HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://dgpaypartners.stg.ameriabank.am/api/")
    };
    var categories = await httpClient.GetAsync("v1/Category");
    var listOfCategories = await categories.Content.ReadFromJsonAsync<List<Category>>();

    var groups = await httpClient.GetAsync("v2/Group?pageNumber=1&count=15000");
    var listOfGroups = await groups.Content.ReadFromJsonAsync<GroupResponse>();

    var merchants = await httpClient.GetAsync("v2/Merchant?pageNumber=1&count=15000");
    var listOfInsertedMerchants = await merchants.Content.ReadFromJsonAsync<MerchantList>();
    var listOfInsertedMerchantIds = listOfInsertedMerchants!.Data.Select(x => x.Id).ToList();

    foreach (var merchant in listOfMerchants)
    {
        if (listOfInsertedMerchantIds.Contains(merchant.PartnerId))
        {
            Console.WriteLine($"Merchant With Id: {merchant.PartnerId} already exists");
            continue;
        }
        int? categoryId = listOfCategories!.FirstOrDefault(x => x.Name == merchant.PartnerCategory)?.Id;
        if (categoryId == null)
        {
            Console.WriteLine($"Category with name {merchant.PartnerCategory} doesn't exists");
        }
        else
        {
            int groupId = default;

            if (!listOfGroups!.Groups.Any(x => x.Name.Trim().ToLower() == merchant.GroupName.Trim().ToLower()))
            {
                var newGroup = new Group
                {
                    Name = merchant.GroupName.Trim(),
                    Fee = merchant.QrFee,
                    CategoryId = categoryId.Value,
                    Terms = []
                };
                try
                {
                    var groupResponse = await httpClient.PostAsJsonAsync("v2/Group", newGroup);
                    if (!groupResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error While inserting GroupName {merchant.GroupName} for Merchant {merchant.PartnerId} with message {JsonSerializer.Serialize(await groupResponse.Content.ReadAsStringAsync())}");
                    }

                    newGroup = await groupResponse.Content.ReadFromJsonAsync<Group>();
                    groupId = newGroup!.Id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error While inserting GroupName {merchant.BrandName} for Merchant {merchant.PartnerId} with message {ex.Message}");
                }
            }
            else
            {
                groupId = listOfGroups.Groups.First(x => x.Name.Trim().ToLower() == merchant.GroupName.Trim().ToLower()).Id;
            }

            var newMerchant = new Merchant
            {
                AccountNumber = merchant.PartnerAccountNumber,
                Community = merchant.PartnerCommunity,
                Building = merchant.PartnerBuilding,
                CategoryId = categoryId.Value,
                City = merchant.PartnerCity,
                ClientId = merchant.ClientCode,
                Name = merchant.BrandName.Trim(),
                GroupId = groupId,
                Country = merchant.PartnerCountry,
                IsActive = true,
                Street = merchant.PartnerStreet,
                MerchantTypeId = 1,
                Cashback = 0,
                Fee = merchant.QrFee,
                Id = merchant.PartnerId
            };

            var merchantResponse = await httpClient.PostAsJsonAsync("v2/Merchant", new List<Merchant>() { newMerchant });

            if (!merchantResponse.IsSuccessStatusCode)
            {
                string responseContent = await merchantResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Error While inserting Merchant {merchant.PartnerId} with message {JsonSerializer.Serialize(responseContent)}");
            }
            Console.WriteLine($"Merchant With Id: {merchant.PartnerId} successfully added");
        }
    }
}