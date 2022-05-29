using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.ProjectsMentors;
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

namespace IsikUn.IncubationCentre.ProjectsMentors
{
    public interface IProjectMentorRepository : IRepository<ProjectMentor, Guid>
    {
        Task<List<ProjectMentor>> GetListAsync(
             Guid[] MentorIds = null,
             Guid[] ProjectIds = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             Guid[] MentorIds = null,
             Guid[] ProjectIds = null,
             CancellationToken cancelationToken = default
            );
    }
}
