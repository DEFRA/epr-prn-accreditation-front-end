namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    /// <summary>
    /// DTO class for passing between facade and portal for
    /// waste inputs for a reprocessing accreditation
    /// </summary>
    public class MaterialWasteInputsDto
    {
        /// <summary>
        /// Gets or sets whether waste last year was processed
        /// </summary>
        public bool? WasteLastYear { get; set; }

        /// <summary>
        /// Gets or sets the uk packaging waste property
        /// </summary>
        public decimal? UkPackagingWaste { get; set; }

        /// <summary>
        /// Gets or sets the non uk packaging waste
        /// </summary>
        public decimal? NonUkPackagingWaste { get; set; }

        /// <summary>
        /// Gets or sets the non packaging waste value
        /// </summary>
        public decimal? NonPackagingWaste { get; set; }
    }
}