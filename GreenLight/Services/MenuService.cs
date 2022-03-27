using System.Text.Json;

namespace GreenLight.Services;

public class MenuService
{
    private readonly HttpClient _httpClient;
    private readonly string _instagramId = "green_food_buffet";

    public MenuService()
    {
        _httpClient = new HttpClient();
    }

    //public List<string> GetRecentMenu() 
    //{
    //    return new List<string>();
    //}

    public async Task<string?> GetAsync(DateTime date)
    {
        string url = $"https://www.instagram.com/{_instagramId}/?a=1";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response is null)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        string result = await response.Content.ReadAsStringAsync();

        JsonSerializer.Deserialize<RootObject1>(result);

        return result;
    }
}