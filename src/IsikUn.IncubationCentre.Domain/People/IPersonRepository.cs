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

namespace IsikUn.IncubationCentre.People
{
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        Task<List<Person>> GetListAsync(
             string filter = null,
             string userName = null,
             string name = null,
             string surname = null,
             string email = null,
             string phoneNumber = null,
             string experience = null,
             Guid[] SkillIds = null,
             string about = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );
        Task<Person> GetWithDetailByIdentityUserIdAsync(Guid IdentityUserId, CancellationToken cancelationToken = default);

        Task<long> GetCountAsync(
             string filter = null,
             string userName = null,
             string name = null,
             string surname = null,
             string email = null,
             string phoneNumber = null,
             string experience = null,
             Guid[] SkillIds = null,
             string about = null,
             CancellationToken cancelationToken = default
            );
    }
}
