using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class CheckAdminConfigurationUseCase : ICheckAdminConfigurationUseCase
{
    private readonly IAdminConfigurationPort _adminConfigurationPort;
    
    public bool CheckUserRegistrationAllowed()
    {
        return _adminConfigurationPort.GetAdminConfiguration().AllowRegistration;
    }

    public AdminConfiguration GetAdminConfiguration()
    {
        return _adminConfigurationPort.GetAdminConfiguration();
    }
}