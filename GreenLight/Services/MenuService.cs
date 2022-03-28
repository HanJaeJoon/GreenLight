using System.Text.Json;
using GreenLight.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GreenLight.Services;

public class MenuService
{
    private readonly HttpClient _httpClient;
    private readonly string _instagramId = "green_food_buffet";
    //private readonly string _instagramId = "green_food_buffet/channel";

    public MenuService()
    {
        _httpClient = new HttpClient();
    }

    //public List<string> GetRecentMenu() 
    //{
    //    return new List<string>();
    //}

    public async Task<InstagramObject> GetAsync(DateTime date)
    {
        string url = $"https://www.instagram.com/{_instagramId}/?__a=1";
        string result = string.Empty;

        //ChromeOptions chromeOptions = new ChromeOptions();
        //chromeOptions.AddArguments("headless");

        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Url = url;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var elements = driver.FindElements(By.CssSelector("body"));

            try
            {
                foreach (var el in elements)
                {
                    result += el.FindElement(By.CssSelector("pre")).Text.Trim();
                }
            }
            catch
            {
                return new InstagramObject();
            }
        }

        return JsonSerializer.Deserialize<InstagramObject>(result);
    }
}