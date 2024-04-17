// <copyright file="AccreditationSiteService.cs" company="DEFRA">
// Copyright (c) DEFRA All rights reserved.
// </copyright>

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;

    public class AccreditationSiteService : IAccreditationSiteService
    {
        private readonly IHttpAccreditationSiteService _httpAccreditationSiteService;

        public AccreditationSiteService(IHttpAccreditationSiteService httpAccreditationSiteService)
        {
            _httpAccreditationSiteService = httpAccreditationSiteService ?? throw new ArgumentNullException(nameof(httpAccreditationSiteService));
        }

        public async Task<ExemptionReferencesViewModel> GetExemptionReferencesViewModel(Guid id)
        {
            var exemptionReferences = await _httpAccreditationSiteService.GetExemptionReferences(id);

            var viewModel = new ExemptionReferencesViewModel { Id = id };

            var references = exemptionReferences.ToList();

            if (references.Count >= 1)
                viewModel.Reference1 = references[0];
            if (references.Count >= 2)
                viewModel.Reference2 = references[1];
            if (references.Count >= 3)
                viewModel.Reference3 = references[2];
            if (references.Count >= 4)
                viewModel.Reference4 = references[3];
            if (references.Count >= 5)
                viewModel.Reference5 = references[4];

            return viewModel;
        }

        public async Task UpdateExemptionReferences(ExemptionReferencesViewModel viewModel)
        {
            IEnumerable<string> exemptionReferences = new List<string>
            {
                viewModel.Reference1,
                viewModel.Reference2,
                viewModel.Reference3,
                viewModel.Reference4,
                viewModel.Reference5
            };

            await _httpAccreditationSiteService.UpdateExemptionReferences(
                viewModel.Id,
                exemptionReferences);
        }
    }
}
