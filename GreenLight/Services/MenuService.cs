using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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

        //_httpClient.DefaultRequestHeaders.Add("Accept",
        //    "application/json");

        //HttpResponseMessage response = await _httpClient.GetAsync(url);

        //if (response is null)
        //{
        //    return null;
        //}

        //response.EnsureSuccessStatusCode();
        //string result = await response.Content.ReadAsStringAsync();

        string result = string.Empty;

        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Url = url;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var elements = driver.FindElements(By.CssSelector("body"));

            foreach (var el in elements)
            {
                result += el.FindElement(By.CssSelector("pre")).Text.Trim();
            }
        }

        return result;
    }
}