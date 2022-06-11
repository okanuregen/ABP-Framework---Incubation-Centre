using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;
using IsikUn.IncubationCentre.Currencies;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Investors;
using Volo.Abp.Users;
using IsikUn.IncubationCentre.ProjectsInvestors;

namespace IsikUn.IncubationCentre.Web.Pages.Projects
{

    public class DetailModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public Project Project { get; set; }
        public bool IsAllowedEdit { get; set; }
        public bool IsAllowedSeeInvestment { get; set; }
 
        public List<string> InvestorNames = new List<string>();
        
        public List<double> InvestorShares = new List<double>();
        public string EntreprenurNameSurname { get; set; }



        private readonly IProjectRepository _projectRepo;
        private readonly IProjectInvestorRepository _projectInvestorRepo;
        private readonly ICurrentUser _currentUser;

        public DetailModel(IProjectRepository projectRepo, ICurrentUser currentUser, IProjectInvestorRepository projectInvestor)
        {
            _projectRepo = projectRepo;
            _projectInvestorRepo = projectInvestor;
            _currentUser = currentUser;
        }

        public virtual async Task OnGetAsync()
        {
            Project = await _projectRepo.GetWithDetailAsync(Id);
            IsAllowedEdit = Project.Entrepreneurs != null ? Project.Entrepreneurs.Any(a => a.IdentityUserId == _currentUser.Id) : false;
            IsAllowedEdit |= Project.Collaborators != null ? Project.Collaborators.Any(a => a.IdentityUserId == _currentUser.Id) : false;

            try
            {
                EntreprenurNameSurname = Project.Entrepreneurs.FirstOrDefault().IdentityUser.Name + " " + Project.Entrepreneurs.FirstOrDefault().IdentityUser.Surname;
            }
            catch (Exception ex)
            {
                EntreprenurNameSurname = "-";
            }
            var investments = await _projectInvestorRepo.GetAllWithDetailAsync();
            var invesmentChartData = investments.GroupBy(a => a.Investor).Select(
                    g => new
                    {
                        Investor = g.Key.IdentityUser.Name + " " + g.Key.IdentityUser.Surname,
                        TotalShare = g.Sum(s => s.Share),
                    });

            invesmentChartData.ToList().ForEach(x =>
            {
                InvestorNames.Add(x.Investor);
                InvestorShares.Add(x.TotalShare);
            });

            InvestorNames.Add(L["ProjectOwners"].Value);
            InvestorShares.Add(100 - InvestorShares.Sum());

            IsAllowedSeeInvestment = _currentUser.IsInRole("admin") || _currentUser.IsInRole("SystemManager") || IsAllowedEdit || (Project.Mentors != null ? Project.Mentors.Any(a => a.IdentityUserId == _currentUser.Id) : false) || (Project.Investors != null ? Project.Investors.Any(a => a.IdentityUserId == _currentUser.Id) : false);

        }


    }
}