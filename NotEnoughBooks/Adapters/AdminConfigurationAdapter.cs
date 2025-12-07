using System.Text.Json;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;

namespace NotEnoughBooks.Adapters;

public class AdminConfigurationAdapter : IAdminConfigurationPort
{
    private AdminConfiguration _adminConfiguration;
    
    public AdminConfiguration GetAdminConfiguration()
    {
        if (_adminConfiguration != null) 
            return _adminConfiguration;
        
        if (File.Exists(PathProvider.AdminConfigurationFile))
            _adminConfiguration = JsonSerializer.Deserialize<AdminConfiguration>(File.ReadAllText(PathProvider.AdminConfigurationFile));
        else
            _adminConfiguration = new AdminConfiguration();
        return _adminConfiguration;
    }

    public void SetAdminConfiguration(AdminConfiguration adminConfiguration)
    {
        File.WriteAllText(PathProvider.AdminConfigurationFile, JsonSerializer.Serialize(adminConfiguration, new JsonSerializerOptions { WriteIndented = true }));
        _adminConfiguration = adminConfiguration;
    }
}