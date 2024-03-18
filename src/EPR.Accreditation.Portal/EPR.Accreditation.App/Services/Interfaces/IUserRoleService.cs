using EPR.Accreditation.App.Enums;

namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IUserRoleService
    {
        bool HasRole(UserRole roleToTestFor);

        void SetRole(UserRole userRole);

        void RemoveRole(UserRole userRole);
    }
}
