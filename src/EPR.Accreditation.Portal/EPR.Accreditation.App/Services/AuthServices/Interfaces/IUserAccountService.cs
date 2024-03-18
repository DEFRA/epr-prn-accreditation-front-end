using EPR.Accreditation.App.DTOs.UserAccount;

namespace EPR.Accreditation.App.Services.AuthServices.Interfaces;

public interface IUserAccountService
{
    public Task<UserAccountDto?> GetUserAccount();

    public Task<PersonDto?> GetPersonByUserId(Guid userId);
}