using GreenLight.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Json;

namespace GreenLight.Services;

public class MenuService
{
    private readonly IWebHostEnvironment _env;
    private readonly string _instagramId = "jaejoon_h";


    public MenuService(IWebHostEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(env.WebRootPath))
        {
            env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        _env = env;
    }

    public async Task<TodayMenu> GetTodayMenuAsync(DateTime date)
    {
        string instagramData = await LoadDataFromJson(date);

        if (string.IsNullOrWhiteSpace(instagramData))
        {
            string url1 = $"https://www.instagram.com/{_instagramId}/?__a=1";
            instagramData = GetInstagramData(url1);

            if (string.IsNullOrWhiteSpace(instagramData))
            {
                string url2 = $"https://www.instagram.com/{_instagramId}/channel/?__a=1";
                instagramData = GetInstagramData(url2);
            }

            if (!string.IsNullOrWhiteSpace(instagramData))
            {
                SaveDataAsFile(date, instagramData);
            }
            else
            {
                return new TodayMenu
                {
                    Menu = instagramData,
                    MenuPhotoUrl = "#",
                };
            }
        }

        InstagramObject? instagramObject = JsonSerializer.Deserialize<InstagramObject>(instagramData);

        return new TodayMenu
        {
            Menu = instagramObject?.graphql.user.edge_owner_to_timeline_media.edges[0].node.accessibility_caption,
            MenuPhotoUrl = instagramObject?.graphql.user.edge_owner_to_timeline_media.edges[0].node.display_url,
        };
    }

    //private static async Task<string> GetInstagramDataAsync(string url)
    //{
    //    string result = string.Empty;

    //    try
    //    {
    //        var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
    //        var context = BrowsingContext.New(config);
    //        var document = await context.OpenAsync(url);

    //        await document.WaitForReadyAsync();

    //        var preList = document.QuerySelectorAll("pre");

    //        foreach (var item in preList)
    //        {
    //            result += item.InnerHtml;
    //        }
    //    }
    //    catch
    //    {
    //        // ignored
    //    }

    //    return result;
    //}

    private static string GetInstagramData(string url)
    {
        string result = string.Empty;

        try
        {
            ChromeOptions chromeOptions = new();
            chromeOptions.AddArguments("headless");

            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                driver.Url = url;

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var elements = driver.FindElements(By.CssSelector("body"));

                result = elements.Aggregate(result, (current, el) => current + el.FindElement(By.CssSelector("pre")).Text.Trim());
            }
        }
        catch
        {
            // ignored
        }

        return result;
    }

    private void SaveDataAsFile(DateTime date, string result)
    {
        string path = Path.Combine(_env.WebRootPath, "data");

        File.WriteAllTextAsync(Path.Combine(path, $"{date:yyyy-MM-dd}.json"), result);
    }

    private async Task<string> LoadDataFromJson(DateTime date)
    {
        string path = Path.Combine(_env.WebRootPath, $"data/{date:yyyy-MM-dd}.json");

        if (!File.Exists(path))
        {
            return string.Empty;
        }

        return await File.ReadAllTextAsync(path);
    }

}