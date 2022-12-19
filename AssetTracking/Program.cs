using AssetTracking;
using System.Linq;

MyDbContext Context = new MyDbContext();

// Loop to gather inputs from user and save as objects and add to list 'assets'.
while (true)
{
    Console.WriteLine("To enter a new asset - follow the steps | To quit and show list of assets - type: 'Q'");

    Console.Write("Enter Type: ");
    string type = Console.ReadLine();

    // Ends the loop when the user types 'Q'.
    if (type.ToUpper().Trim() == "Q") 
    {
        break;
    }

    else if (type.ToLower().Trim() == "computer" || type.ToLower().Trim() == "phone")
    {
        Asset asset = new Asset();

        Console.Write("Enter Brand: ");
        string brand = Console.ReadLine();

        Console.Write("Enter Model: ");
        string model = Console.ReadLine();

        Console.Write("Enter Purchase Date: ");
        bool isDate = DateTime.TryParse(Console.ReadLine(), out DateTime purchaseDate);

        Console.Write("Enter Price in USD: ");
        bool isDouble = double.TryParse(Console.ReadLine(), out double price);

        Console.Write("Enter Office (Sweden, Spain or USA): ");
        string country = Console.ReadLine();

        asset.Type = type;
        asset.Brand = brand;
        asset.Model = model;
        asset.PurchaseDate = purchaseDate;
        asset.Price = price;
        asset.Country = country;

        // Adds and saves the asset to the database.
        Context.Add(asset);
        Context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The asset was successfully added!");
        Console.ResetColor();
        Console.WriteLine("-----------------------------------------");
    }

    else
    {
        Console.WriteLine("Invalid type, try again");
    }
}

Console.WriteLine();

Console.WriteLine("Type".PadRight(10) 
    + "Brand".PadRight(10) 
    + "Model".PadRight(15) 
    + "Office".PadRight(10)
    + "Purchase Date".PadRight(15) 
    + "Price in USD".PadRight(15)
    + "Currency".PadRight(15)
    + "Local price today");

Console.WriteLine("----".PadRight(10)
    + "-----".PadRight(10)
    + "-----".PadRight(15)
    + "------".PadRight(10)
    + "-------------".PadRight(15)
    + "------------".PadRight(15)
    + "--------".PadRight(15)
    + "-----------------");

// Creates a list of all assets in the database.
List<Asset> assets = Context.Assets.ToList();
// Creates a new sorted list by office and then purchase date.
List<Asset> sortedAssets = assets.OrderBy(x => x.Country).ThenBy(x => x.PurchaseDate).ToList();

// Goes through every object in the list and writes it to the console.
// Depending on if the asset has expired/will expire within 3 or 6 months the text color changes.
foreach (var sortedAsset in sortedAssets)
{
    DateTime threeMonthsBefore = DateTime.Now.AddYears(-3).AddMonths(3);
    DateTime sixMonthsBefore = DateTime.Now.AddYears(-3).AddMonths(6);

    string currency = sortedAsset.CurrencyPerCountry(sortedAsset.Country.ToLower());
    double localPrice = sortedAsset.ConvertedCurrency(sortedAsset.Country.ToLower(), sortedAsset.Price);

    if (sortedAsset.PurchaseDate < sixMonthsBefore)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    if (sortedAsset.PurchaseDate < threeMonthsBefore)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    Console.WriteLine(sortedAsset.Type.PadRight(10)
        + sortedAsset.Brand.PadRight(10)
        + sortedAsset.Model.PadRight(15)
        + sortedAsset.Country.PadRight(10)
        + sortedAsset.PurchaseDate.ToString("d").PadRight(15)
        + sortedAsset.Price.ToString().PadRight(15)
        + currency.PadRight(15)
        + localPrice);

    Console.ResetColor();
}

Console.ReadLine();