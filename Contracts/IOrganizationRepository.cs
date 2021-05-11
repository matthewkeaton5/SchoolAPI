using Entities.Models;
using System;
using System.Collections.Generic;
using Entities.RequestFeatures;


namespace Contracts
{
    public interface IOrganizationRepository
    {
        PagedList<Organization> GetAllOrganizations(OrganizationParameters organizationParameters, bool trackChanges);
        Organization GetOrganization(Guid companyId, bool trackChanges);
        void createOrg(Organization organization);

        void deleteOrg(Organization organization);
    }
}
