﻿namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class MaterialReprocessorDetails
    {
        public int Id { get; set; }

        public bool WasteLastYear { get; set; }

        public decimal? UkPackagingWaste { get; set; }

        public decimal? NonUkPackagingWaste { get; set; }

        public decimal? NonPackagingWaste { get; set; }

        public decimal? MaterialsNotProcessedOnSite { get; set; }

        public decimal? Contaminents { get; set; }

        public decimal? ProcessLoss { get; set; }

        public IEnumerable<ReprocessorSupportingInformation> ReprocessorSupportingInformation { get; set; }
    }
}
