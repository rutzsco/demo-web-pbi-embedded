# Power BI Embedded Setup Instructions

## Quick Setup

1. **Set the Client Secret** (REQUIRED):
   ```bash
   cd DemoWebApp
   dotnet user-secrets set "PowerBI:ClientSecret" "YOUR_ACTUAL_CLIENT_SECRET"
   ```
   Replace `YOUR_ACTUAL_CLIENT_SECRET` with the actual client secret you mentioned you'll send separately.

2. **Run the Application**:
   ```bash
   dotnet run
   ```

3. **Access the Application**:
   - Open your browser and go to: `https://localhost:7000` or `http://localhost:5000`
   - Click "View Power BI Report" to see the embedded report

## What's Been Configured

✅ **Client ID**: `ee5f0953-a358-4160-a787-5ba77deada2c`  
✅ **Group ID**: `f736563a-b458-4fdd-9b48-4fa2617ec454`  
✅ **Report ID**: `20f26576-f6c1-4ac9-84c2-7f395b6cfbc9`  
✅ **Dataset ID**: `638067e6-3539-47ad-9942-0c57bf96d20a`  
⚠️ **Client Secret**: Needs to be added via user secrets (see step 1 above)

## Troubleshooting

If you encounter issues:
1. Check the browser console for JavaScript errors
2. Check the application logs for authentication errors
3. Verify your Azure AD app permissions include:
   - `Report.Read.All` (Application)
   - `Dataset.Read.All` (Application)
4. Ensure admin consent has been granted for these permissions

The application includes comprehensive error handling and will display helpful error messages if something goes wrong.
