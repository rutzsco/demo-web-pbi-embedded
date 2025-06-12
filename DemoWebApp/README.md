# Power BI Embedded Demo Web Application

This ASP.NET Core web application demonstrates how to embed Power BI reports using the Power BI Embedded service.

## Features

- Embedded Power BI report with authentication
- Secure client secret management using ASP.NET Core User Secrets
- Responsive design with Bootstrap
- Error handling and loading states

## Prerequisites

- .NET 8.0 SDK
- Power BI Pro or Premium license
- Azure Active Directory application registration

## Configuration

### 1. Power BI Settings

The application is pre-configured with the following Power BI settings in `appsettings.json`:

- **Client ID**: `ee5f0953-a358-4160-a787-5ba77deada2c`
- **Group ID**: `f736563a-b458-4fdd-9b48-4fa2617ec454`
- **Report ID**: `20f26576-f6c1-4ac9-84c2-7f395b6cfbc9`
- **Dataset ID**: `638067e6-3539-47ad-9942-0c57bf96d20a`

### 2. Client Secret Configuration

The client secret is stored securely using ASP.NET Core User Secrets. To set your client secret:

```bash
cd DemoWebApp
dotnet user-secrets set "PowerBI:ClientSecret" "YOUR_ACTUAL_CLIENT_SECRET"
```

### 3. Azure AD App Registration Requirements

Make sure your Azure AD application has the following API permissions:
- **Power BI Service API**: 
  - `Report.Read.All` (Application permission)
  - `Dataset.Read.All` (Application permission)

## Installation

1. Clone the repository
2. Navigate to the DemoWebApp directory
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Set the client secret (see configuration above)
5. Run the application:
   ```bash
   dotnet run
   ```

## Usage

1. Start the application
2. Navigate to the home page
3. Click "View Power BI Report" to access the embedded report
4. The report will load with the configured Power BI content

## Project Structure

- `Models/`: Configuration and data models
  - `PowerBIConfig.cs`: Power BI configuration settings
  - `EmbedConfig.cs`: Embed token and URL configuration
- `Services/`: Business logic services
  - `PowerBIService.cs`: Handles Power BI authentication and embedding
- `Pages/`: Razor Pages
  - `PowerBI.cshtml`: Main Power BI report page
  - `Index.cshtml`: Home page with navigation

## Security Notes

- Client secrets are stored using ASP.NET Core User Secrets for development
- For production, use Azure Key Vault or other secure secret management
- The application uses service principal authentication (client credentials flow)

## Troubleshooting

### Common Issues

1. **"Failed to get access token"**
   - Verify your client ID and secret are correct
   - Check that your Azure AD app has the required permissions
   - Ensure the permissions are granted admin consent

2. **"Report not found"**
   - Verify the Group ID and Report ID are correct
   - Ensure the service principal has access to the workspace

3. **"Embed token generation failed"**
   - Check that the Dataset ID is correct
   - Verify the service principal has read access to the dataset

### Logs

Check the application logs for detailed error messages. The application logs authentication and embedding errors with full exception details.

## Power BI Report URL

Original report URL: https://app.powerbi.com/groups/f736563a-b458-4fdd-9b48-4fa2617ec454/reports/20f26576-f6c1-4ac9-84c2-7f395b6cfbc9/ReportSectiondecccb8482fa6cf4f797?experience=power-bi
