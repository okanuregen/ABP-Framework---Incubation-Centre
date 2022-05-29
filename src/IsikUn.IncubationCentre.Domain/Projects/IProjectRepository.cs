using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
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

namespace IsikUn.IncubationCentre.Projects
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        Task<List<Project>> GetListAsync(
             ProjectStatus status,
             bool filterByStatus = false,
             string filter = null,
             string name = null,
             string tags = null,
             bool filterByinvesmentReady = false,
             bool invesmentReady = false, 
             bool filterByopenForInvesment = false,
             bool openForInvesment = false,
             List<Guid> founderIds = null,
             List<Guid> investorIds = null,
             List<Guid> mentorIds = null,
             List<Guid> collaboratorIds = null,
             string sorting = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             ProjectStatus status,
             bool filterByStatus = false,
             string filter = null,
             string name = null,
             string tags = null,
             bool filterByinvesmentReady = false,
             bool invesmentReady = false,
             bool filterByopenForInvesment = false,
             bool openForInvesment = false,
             List<Guid> founderIds = null,
             List<Guid> investorIds = null,
             List<Guid> mentorIds = null,
             List<Guid> collaboratorIds = null,
             CancellationToken cancelationToken = default
            );
    }
}
