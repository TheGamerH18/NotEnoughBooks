using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface ISetAdminConfigurationUseCase
{
    void SetAdminConfiguration(AdminConfiguration adminConfiguration);
}