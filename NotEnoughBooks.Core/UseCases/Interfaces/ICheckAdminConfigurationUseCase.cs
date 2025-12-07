using NotEnoughBooks.Core.Models;

namespace NotEnoughBooks.Core.UseCases.Interfaces;

public interface ICheckAdminConfigurationUseCase
{
    bool CheckUserRegistrationAllowed();
    AdminConfiguration GetAdminConfiguration();
}