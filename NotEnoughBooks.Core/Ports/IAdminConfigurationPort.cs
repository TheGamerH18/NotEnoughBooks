using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.Ports;

public interface IAdminConfigurationPort
{
    public AdminConfiguration GetAdminConfiguration();
    public void SetAdminConfiguration(AdminConfiguration adminConfiguration);
}