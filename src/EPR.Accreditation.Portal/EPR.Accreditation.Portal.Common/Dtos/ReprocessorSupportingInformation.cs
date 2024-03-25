﻿using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class ReprocessorSupportingInformation
    {
        public int Id { get; set; }

        public Enums.ReprocessorSupportingInformationType ReprocessorSupportingInformationTypeId { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        public decimal Tonnes { get; set; }
    }
}
