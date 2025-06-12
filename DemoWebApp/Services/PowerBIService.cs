using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using Microsoft.Identity.Client;
using DemoWebApp.Models;
using Microsoft.Extensions.Options;

namespace DemoWebApp.Services
{
    public class PowerBIService
    {
        private readonly PowerBIConfig _config;
        private readonly ILogger<PowerBIService> _logger;

        public PowerBIService(IOptions<PowerBIConfig> config, ILogger<PowerBIService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task<EmbedConfig> GetEmbedConfigAsync()
        {
            try
            {
                // Get access token
                var accessToken = await GetAccessTokenAsync();
                
                if (string.IsNullOrEmpty(accessToken))
                {
                    return new EmbedConfig { ErrorMessage = "Failed to get access token" };
                }

                // Create Power BI client
                var tokenCredentials = new TokenCredentials(accessToken, "Bearer");
                using var pbiClient = new PowerBIClient(new Uri(_config.ApiUrl), tokenCredentials);

                // Get report
                var report = await pbiClient.Reports.GetReportInGroupAsync(Guid.Parse(_config.GroupId), Guid.Parse(_config.ReportId));

                // Generate embed token
                var generateTokenRequestParameters = new GenerateTokenRequest(
                    accessLevel: TokenAccessLevel.View,
                    datasetId: _config.DatasetId
                );

                var tokenResponse = await pbiClient.Reports.GenerateTokenInGroupAsync(
                    Guid.Parse(_config.GroupId),
                    Guid.Parse(_config.ReportId),
                    generateTokenRequestParameters
                );

                return new EmbedConfig
                {
                    EmbedToken = tokenResponse.Token,
                    EmbedUrl = report.EmbedUrl,
                    ReportId = _config.ReportId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting embed config");
                return new EmbedConfig { ErrorMessage = ex.Message };
            }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var app = ConfidentialClientApplicationBuilder
                    .Create(_config.ClientId)
                    .WithClientSecret(_config.ClientSecret)
                    .WithAuthority(new Uri($"{_config.AuthorityUri}{_config.TenantId}"))
                    .Build();

                var scopes = new[] { $"{_config.ResourceUri}/.default" };

                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                return result.AccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting access token");
                return string.Empty;
            }
        }
    }
}
