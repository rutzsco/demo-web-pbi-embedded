using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DemoWebApp.Services;
using DemoWebApp.Models;

namespace DemoWebApp.Pages
{
    public class PowerBIModel : PageModel
    {
        private readonly PowerBIService _powerBIService;
        private readonly ILogger<PowerBIModel> _logger;

        public PowerBIModel(PowerBIService powerBIService, ILogger<PowerBIModel> logger)
        {
            _powerBIService = powerBIService;
            _logger = logger;
        }

        public EmbedConfig EmbedConfig { get; set; } = new EmbedConfig();

        public async Task OnGetAsync()
        {
            try
            {
                EmbedConfig = await _powerBIService.GetEmbedConfigAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Power BI report");
                EmbedConfig.ErrorMessage = "Failed to load Power BI report. Please check your configuration.";
            }
        }
    }
}
