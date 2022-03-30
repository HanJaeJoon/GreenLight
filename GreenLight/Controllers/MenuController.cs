using GreenLight.Models;
using GreenLight.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenLight.Api;

[Route("api/[controller]")]
[ApiController]
public class MenuController : Controller
{
    private readonly MenuService _menuService;

    public MenuController(IWebHostEnvironment env)
    {
        _menuService = new MenuService(env);
    }

    [HttpGet("{date?}")]
    public async Task<ActionResult<TodayMenu>> GetAsync(DateTime date)
    {
        var menu = await _menuService.GetTodayMenuAsync(date);

        return menu;
    }
}
