﻿using EPR.Accreditation.App.DTOs.UserAccount;

namespace EPR.Accreditation.App.Services.AuthServices.Interfaces;

public interface IRoleManagementService
{
    public Task<DelegatedPersonNominatorDto> GetDelegatedPersonNominator(Guid enrolmentId, Guid? organisationId);

    public Task<HttpResponseMessage> AcceptNominationToDelegatedPerson(Guid enrolmentId, Guid organisationId, string serviceKey, AcceptNominationRequest acceptNominationRequest);
}
