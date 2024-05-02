namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.FileService;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.Options;
    using System.Net;

    /// <summary>
    /// This controller contains methods to handle file upload management
    /// </summary>
    [Route("Accreditation/{id}/[controller]")]
    public class FilesController : BaseController
    {
        private readonly IFileService _fileService;
        private readonly IOptions<AppSettingsConfigOptions> _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor.</param>
        /// <param name="fileService">File service.</param>
        /// <param name="urlHelper">Url helper.</param>
        /// <param name="backPageViewModel">Back page view model.</param>
        /// <param name="appSettings">App settings.</param>
        /// <exception cref="ArgumentNullException">Possible exception type.</exception>
        public FilesController(
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel,
            IOptions<AppSettingsConfigOptions> appSettings)
            : base(
                  httpContextAccessor,
                  urlHelper,
                  backPageViewModel)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        /// <summary>
        /// Gets a list of uploaded file records.
        /// </summary>
        /// <param name="id">Accreditation Id.</param>
        /// <returns>A list of file uploaded view models</returns>
        [HttpGet]
        public async Task<IActionResult> Files(Guid id)
        {
            var vm = await _fileService.GetFileRecords(id);
            ViewData["Id"] = id;
            return View(vm);
        }

        /// <summary>
        /// Gets an uploaded file and opens it in a new tab.
        /// </summary>
        /// <param name="id">Accreditation Id.</param>
        /// <param name="fileId">File Id.</param>
        /// <returns>A list of file uploaded view models</returns>
        [HttpGet("/{fileId}")]
        public async Task<IActionResult> File(Guid id, Guid fileId)
        {
            //var filePath = @"c:\temp\Uploaded_Files\" + fileId + ".txt";
            var filePath = @"c:\temp\Uploaded_Files\" + fileId + ".csv";
            //var filePath = @"c:\temp\Uploaded_Files\" + "Alex 1.png";

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));

            //return RedirectToAction("Files", new { id });
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(Guid id, IFormFile file)
        {
            var filePath = @"c:\temp\Uploaded_Files\" + file.FileName;
            var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return RedirectToAction("Files", new { id });
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid id, Guid fileId)
        {
            await _fileService.DeleteFile(id, fileId);
            return RedirectToAction("Files", new { id });
        }
    }
}
