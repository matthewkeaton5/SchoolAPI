using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Entities.RequestFeatures;
using Repository.Extensions;

namespace Repository
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public PagedList<Organization> GetAllOrganizations(OrganizationParameters orgParameters, bool trackChanges)
        {
            var organization = FindAll(trackChanges)
                .Sort(orgParameters.Order)
                .FilterCity(orgParameters.CityFilter)
                .Search(orgParameters.SearchTerm)
                .ToList();

            return PagedList<Organization>
                .ToPagedList(organization, orgParameters.PageNumber, orgParameters.PageSize);

        }

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