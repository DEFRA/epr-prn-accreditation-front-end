namespace EPR.Accreditation.Portal.Configuration
{
    public class ServicesConfiguration
    {
        public static string SectionName => "Services";

        public Service AccreditationFacade { get; set; }
    }

    public class Service
    {
        public string Url { get; set; }
    }
}
