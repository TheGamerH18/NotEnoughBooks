using ConstructorGenerator.Attributes;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core.UseCases;

[GenerateFullConstructor]
public partial class SetAdminConfigurationUseCase : ISetAdminConfigurationUseCase
{
    private readonly IAdminConfigurationPort _adminConfigurationPort;

    public void SetAdminConfiguration(AdminConfiguration adminConfiguration)
    {
        _adminConfigurationPort.SetAdminConfiguration(adminConfiguration);
    }
}