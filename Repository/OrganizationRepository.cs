using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Repository
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Organization> GetAllOrganizations(bool trackChanges) =>
          FindAll(trackChanges)
          .OrderBy(c => c.OrgName)
          .ToList();

        public Organization GetOrganization(Guid companyId, bool trackChanges) =>
         FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefault();

        public IEnumerable<Organization> GetIds(IEnumerable<Guid> ids, bool trackChanges) =>
        FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();

            public void createOrg(Organization organization)
        {
            Create(organization);
        }

        public void deleteOrg(Organization organization)
        {
            Delete(organization);
        }
    }
}