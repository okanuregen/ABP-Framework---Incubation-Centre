using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.ProjectsInvestors
{
    public interface IProjectInvestorRepository : IRepository<ProjectInvestor, Guid>
    {
        Task<List<ProjectInvestor>> GetListAsync(
             Guid? InvestorId = null,
             Guid? ProjectId = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             Guid? InvestorId = null,
             Guid? ProjectId = null,
             CancellationToken cancelationToken = default
            );

        Task<List<ProjectInvestor>> GetAllWithDetailAsync();
    }
}
