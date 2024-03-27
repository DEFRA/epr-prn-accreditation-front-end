﻿namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class Site
    {
        public int Id { get; set; }

        public Guid ExternalId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public Guid OrganisationId { get; set; }

        public IEnumerable<SiteAuthority> SiteAuthorties { get; set;}
    }
}
