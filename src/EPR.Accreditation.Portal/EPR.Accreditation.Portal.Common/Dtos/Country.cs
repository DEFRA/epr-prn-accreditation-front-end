﻿using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class Country
    {
        public int CountryId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
