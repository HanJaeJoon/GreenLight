using GreenLight.Models;
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
    public async Task<ActionResult<InstagramObject>> GetAsync(DateTime date)
    {
        var menu = await _menuService.GetAsync(date);

        if (menu is null)
        {
            return NotFound();
        }

        return menu;
    }
}
