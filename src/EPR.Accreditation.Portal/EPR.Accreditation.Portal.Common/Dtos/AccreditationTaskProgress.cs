﻿namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class AccreditationTaskProgress
    {
        public int Id { get; set; }

        public Enums.TaskStatus TaskStatusId { get; set; }
        
        public Enums.TaskName TaskNameId { get; set; }
    }
}