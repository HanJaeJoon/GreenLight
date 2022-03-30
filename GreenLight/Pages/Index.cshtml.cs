using GreenLight.Models;
using GreenLight.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreenLight.Pages
{
    public class IndexModel : PageModel
    {
        private MenuService _menuService;
        private readonly ILogger<IndexModel> _logger;

        public TodayMenu todayMenu = new();

        public IndexModel(ILogger<IndexModel> logger, MenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        public async Task OnGet()
        {
            todayMenu = await _menuService.GetTodayMenuAsync(DateTime.Today);
        }
    }
}