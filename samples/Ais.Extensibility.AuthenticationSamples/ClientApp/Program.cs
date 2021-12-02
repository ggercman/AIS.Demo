using IdentityModel.Client;

Console.WriteLine("Wait for AIS to start");
Console.WriteLine("Wait for AuthorizedApi to start");
Console.WriteLine("Update server URL if AIS Identity runs on another url (https://localhost:6001/connect/token)");
Console.WriteLine("Press any key to continue!");
Console.ReadKey();


Console.WriteLine("Enter client_id: ");
var clientId = Console.ReadLine(); //  "client_id";

Console.WriteLine("Enter client_secret: ");
var clientSecret = Console.ReadLine(); // "client_secret";

Console.WriteLine("Enter username: ");
var username = Console.ReadLine(); // "username";

Console.WriteLine("Enter password: ");
var password = Console.ReadLine(); // "password"; ;



using var request = new PasswordTokenRequest
{
    Address = "https://localhost:6001/connect/token",

    ClientId = clientId, // < client_id
    ClientSecret = clientSecret, // < client_secret

    UserName = username,
    Password = password,

    Scope = "api1",
};

using var httpClient = new HttpClient();
var tokenResponse = await httpClient.RequestPasswordTokenAsync(request);

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
    return;
}



httpClient.SetBearerToken(tokenResponse.AccessToken);

using var response = await httpClient.GetAsync("https://localhost:7123/UserInfo").ConfigureAwait(false);
//using var response = await httpClient.GetAsync("https://localhost:7123/UserInfo/Admin").ConfigureAwait(false);

Console.WriteLine();
if (response.StatusCode != System.Net.HttpStatusCode.OK)
{
    Console.WriteLine("Failure");
    Console.WriteLine($"Request failed with status={(int)response.StatusCode} ({response.StatusCode})");
}
else
{
    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    Console.WriteLine("Success, results:");
    Console.WriteLine(result);
}
Console.WriteLine();
Console.WriteLine("Press any key to exit");
Console.ReadKey();