﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using IsikUn.IncubationCentre.EntityFrameworkCore;
using IsikUn.IncubationCentre.Skills;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Mentors
{
    public class EFCoreMentorRepository :
        EfCoreRepository<IncubationCentreDbContext, Mentor, Guid>,
        IMentorRepository
    {
        public EFCoreMentorRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string filter = null, string userName = null, string name = null, string surname = null, string email = null, string phoneNumber = null, string experience = null, Guid[] SkillIds = null, string about = null, bool filterByActiveted = false, bool isActivated = true, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    ((await GetQueryableAsync()).Include(a => a.Skills).Include(a => a.IdentityUser).Include(a => a.MentoringProjects))
                                     ,
                                      filter,
                                      userName,
                                      name,
                                      surname,
                                      email,
                                      phoneNumber,
                                      experience,
                                      SkillIds,
                                      about,
                                      filterByActiveted,
                                      isActivated
                                     );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));

        }

        public async Task<List<Mentor>> GetListAsync(string filter = null, string userName = null, string name = null, string surname = null, string email = null, string phoneNumber = null, string experience = null, Guid[] SkillIds = null, string about = null, bool filterByActiveted = false, bool isActivated = true, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                          ((await GetQueryableAsync()).Include(a => a.Skills).Include(a => a.IdentityUser).Include(a => a.MentoringProjects))
                          ,
                           filter,
                           userName,
                           name,
                           surname,
                           email,
                           phoneNumber,
                           experience,
                           SkillIds,
                           about,
                           filterByActiveted,
                           isActivated
                          );
            //query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? " " : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<Mentor> ApplyFilter(
           IQueryable<Mentor> query,
            string filter,
            string userName = null,
            string name = null,
            string surname = null,
            string email = null,
            string phoneNumber = null,
            string experience = null,
            Guid[] SkillIds = null,
            string about = null,
            bool filterByActiveted = false,
            bool isActivated = true
        )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e =>
                        (e.IdentityUser != null ? e.IdentityUser.UserName.Contains(filter) : false) ||
                        (e.IdentityUser != null ? e.IdentityUser.Name.Contains(filter) : false) ||
                        (e.IdentityUser != null ? e.IdentityUser.Surname.Contains(filter) : false) ||
                        (e.IdentityUser != null ? e.IdentityUser.Email.Contains(filter) : false) ||
                        e.Experience.Contains(filter) ||
                        e.About.Contains(filter)
                        )
                    .WhereIf(!string.IsNullOrWhiteSpace(userName), e => (e.IdentityUser != null ? e.IdentityUser.UserName.Contains(userName) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => (e.IdentityUser != null ? e.IdentityUser.Name.Contains(name) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(surname), e => (e.IdentityUser != null ? e.IdentityUser.Surname.Contains(surname) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => (e.IdentityUser != null ? e.IdentityUser.Email.Contains(email) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => (e.IdentityUser != null ? e.IdentityUser.PhoneNumber.Contains(phoneNumber) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(experience), e => e.Experience.Contains(experience))
                    .WhereIf(!string.IsNullOrWhiteSpace(about), e => e.About.Contains(about))
                    .WhereIf(SkillIds != null && SkillIds.Any(), e => SkillIds.All(a => e.Skills.Select(x => x.Id).Contains(a)))
                    .WhereIf(filterByActiveted, e => e.isActivated == isActivated);
        }

        public async Task<Mentor> GetWithDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbSet = (await GetDbSetAsync())
                .Include(c => c.Skills)
                .Include(c => c.MentoringProjects).ThenInclude(b => b.Milestones)
                .Include(c => c.MentoringProjects).ThenInclude(b => b.Mentors)
                .Include(c => c.MentoringProjects).ThenInclude(b => b.Collaborators)
                .Include(c => c.MentoringProjects).ThenInclude(b => b.Entrepreneurs)
                .Include(c => c.MentoringProjects).ThenInclude(b => b.Investors)
                .Include(a => a.IdentityUser);
            var rs = await dbSet.Where(a => a.Id == id).FirstOrDefaultAsync(cancellationToken);
            return rs;
        }
    }
}

