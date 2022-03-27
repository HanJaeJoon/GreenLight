using GreenLight.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenLight.Api;

[Route("api/[controller]")]
[ApiController]
public class MenuController : Controller
{
    private readonly MenuService _menuService;

    public MenuController()
    {
        _menuService = new MenuService();
    }

    //[HttpGet]
    //public ActionResult GetRecentMenu()
    //{
    //    return _menuService.GetRecentMenu();
    //}

    [HttpGet("{date?}")]
    public async Task<ActionResult<string>> GetAsync(DateTime date)
    {
        string? menu = await _menuService.GetAsync(date);

        if (menu == null)
        {
            return NotFound();
        }

        return menu;
    }
}
