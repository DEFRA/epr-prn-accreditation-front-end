using EPR.Accreditation.Portal.DTOs.UserAccount;

namespace EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

public interface IUserAccountService
{
    public Task<UserAccountDto?> GetUserAccount();

    public Task<PersonDto?> GetPersonByUserId(Guid userId);
}