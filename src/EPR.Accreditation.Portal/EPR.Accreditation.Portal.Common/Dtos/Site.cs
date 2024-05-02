namespace EPR.Accreditation.Portal.Common.Dtos
{
    using EPR.Accreditation.Portal.Common.Dtos.Portal;

    public class Site : AddressDto
    {
        public Guid ExternalId { get; set; }

        public Guid OrganisationId { get; set; }
    }
}
