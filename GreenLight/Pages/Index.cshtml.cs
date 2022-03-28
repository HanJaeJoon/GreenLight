using GreenLight.Models;
using GreenLight.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreenLight.Pages
{
    public class IndexModel : PageModel
    {
        private MenuService _menuService;
        private readonly ILogger<IndexModel> _logger;

        public InstagramObject menuInfo = new();

        public IndexModel(ILogger<IndexModel> logger, MenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        public async Task OnGet()
        {
            menuInfo = await _menuService.GetAsync(DateTime.Today);
        }
    }
}