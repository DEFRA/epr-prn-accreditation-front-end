using EPR.Accreditation.Portal.Enums;

namespace EPR.Accreditation.Portal.Services.Interfaces
{
    public interface IUserRoleService
    {
        bool HasRole(UserRole roleToTestFor);

        void SetRole(UserRole userRole);

        void RemoveRole(UserRole userRole);
    }
}
