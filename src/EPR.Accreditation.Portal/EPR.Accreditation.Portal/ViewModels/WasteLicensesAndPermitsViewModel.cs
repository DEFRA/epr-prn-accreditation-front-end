﻿namespace EPR.Accreditation.Portal.ViewModels
{
    public class WasteLicensesAndPermitsViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public Guid MaterialId { get; set; }

        public double? RegistrationNumber { get; set; }

        public double? PermitNumber { get; set; }

        public double? ActivityNumber { get; set; }

        public double? ActivityReferenceNumber { get; set; }

        public double? DischargeConstentNumber { get; set; }
    }
}
