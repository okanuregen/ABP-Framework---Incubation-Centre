using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Collaborators;
using Volo.Abp.Users;
using System.Linq;
using System;

namespace IsikUn.IncubationCentre.Web.Pages.Collaborators;

public class DetailModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid id { get; set; }

    public bool IsMyPage = false;

    public Project CurrentProject { get; set; }
    public List<Project> Projects { get; set; }
    public Collaborator Collaborator { get; set; }


    private readonly ICollaboratorRepository _collaboratorRepo;
    private readonly ICurrentUser _currentUser;

    public DetailModel(
        ICollaboratorRepository collaboratorRepo,
        ICurrentUser currentUser
        )
    {
        this._currentUser = currentUser;
        this._collaboratorRepo = collaboratorRepo;
    }
    public async System.Threading.Tasks.Task OnGetAsync()
    {
        Collaborator = await _collaboratorRepo.GetWithDetailAsync(id);
        IsMyPage = _currentUser.Id == Collaborator.IdentityUserId;
        Projects = Collaborator.CollaboratoringProjects != null && Collaborator.CollaboratoringProjects.Any() ? Collaborator.CollaboratoringProjects.ToList() : null;
        CurrentProject = Projects != null ? Projects.Where(a => a.Status ==  ProjectStatus.OnGoing).FirstOrDefault() : null;
    }
}
