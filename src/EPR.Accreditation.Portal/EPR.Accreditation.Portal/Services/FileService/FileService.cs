namespace EPR.Accreditation.Portal.Services.FileService
{
    using AutoMapper;
    using EPR.Accreditation.Portal.ViewModels.Files;

    /// <summary>
    /// A class containing methods to handle uploaded file management
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly RESTservices.Interfaces.IHttpAccreditationService _httpAccreditationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// Class constructor for File management.
        /// </summary>
        /// <param name="httpAccreditationService">Accreditation HTTP service.</param>
        /// <param name="mapper">Automapper.</param>
        public FileService(
            RESTservices.Interfaces.IHttpAccreditationService httpAccreditationService,
            IMapper mapper)
        {
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a list of file records with a status of [UploadComplete].
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>A list of file uploaded view models</returns>
        public async Task<FileUploadRecordsViewModel> GetFileRecords(Guid accreditationId)
        {
            var fileRecords = await _httpAccreditationService.GetFiles(accreditationId);
            var vm = new FileUploadRecordsViewModel { AccreditationId = accreditationId };
            vm.FlowCharts = _mapper.Map<List<FileUploadRecordViewModel>>(fileRecords.Where(x => x.FileUploadTypeId == Common.Enums.FileUploadType.FlowDiagram && x.Status == Common.Enums.FileUploadStatus.UploadComplete));
            vm.PlantsAndEquipments = _mapper.Map<List<FileUploadRecordViewModel>>(fileRecords.Where(x => x.FileUploadTypeId == Common.Enums.FileUploadType.PlantsAndEquipment && x.Status == Common.Enums.FileUploadStatus.UploadComplete));
            vm.RecordingSystems = _mapper.Map<List<FileUploadRecordViewModel>>(fileRecords.Where(x => x.FileUploadTypeId == Common.Enums.FileUploadType.RecordingSystem && x.Status == Common.Enums.FileUploadStatus.UploadComplete));
            vm.SamplingAndInspection = _mapper.Map<List<FileUploadRecordViewModel>>(fileRecords.Where(x => x.FileUploadTypeId == Common.Enums.FileUploadType.SamplingPlan && x.Status == Common.Enums.FileUploadStatus.UploadComplete));
            return vm;
        }

        /// <summary>
        /// Adds a new uploaded file.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>Completed task</returns>
        public Task AddFile(Guid accreditationId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a previously uploaded file.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <param name="fileId">File Id.</param>
        /// <returns>Completed task</returns>
        public async Task DeleteFile(Guid accreditationId, Guid fileId)
        {
            await _httpAccreditationService.DeleteFile(accreditationId, fileId);
        }
    }
}
