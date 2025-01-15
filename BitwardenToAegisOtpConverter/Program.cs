using System.Text.Json;

using BitwardenToAegisOtpConverter;

Console.WriteLine("Bitwarden Authenticator -> Aegis OTP Json Converter");
var bitwardenJson = "g:\\My Drive\\authenticator_export_20250115115407.json";
var aegisJson = "g:\\My Drive\\aegis.json";

if (!File.Exists(bitwardenJson))
{
    Console.WriteLine("Input file doesn't exist, aborting.");
    Environment.Exit(1);
}

var bitwarden = JsonSerializer.Deserialize<Bitwarden>(File.ReadAllText(bitwardenJson));

if (bitwarden == null)
{
    Console.WriteLine("Input file JSON parsing returned NULL, aborting.");
    Environment.Exit(1);
}

if (bitwarden.items.Count == 0)
{
    Console.WriteLine("Input file contained 0 entries, aborting.");
    Environment.Exit(1);
}

Console.WriteLine($"Found {bitwarden.items.Count} entries in Bitwarden file");

var aegis = new Aegis
{
    db = new DB
    {
        entries = new List<Entry>()
    }
};

foreach (var bitwardenItem in bitwarden.items)
{
    var otpUrl = new OtpAuthUrl(bitwardenItem.login.totp);
    aegis.db.entries.Add(new Entry
    {
        type = "totp",
        uuid = bitwardenItem.id,
        name = otpUrl.user,
        issuer = otpUrl.issuer,
        note = string.Empty,
        icon = null,
        info= new Info
        {
            secret = otpUrl.secret,
            algo = otpUrl.algorithm,
            digits = otpUrl.digits,
            period = otpUrl.period
        }
    });
}

Console.WriteLine($"Generated Aegis JSON now containing {aegis.db.entries.Count} entries");

var aegisJsonString = JsonSerializer.Serialize(aegis);
File.WriteAllText(aegisJson, aegisJsonString);
Console.WriteLine("All done.");