using System.Threading;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.Localization;
using IsikUn.IncubationCentre.Permissions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace IsikUn.IncubationCentre.Web.Menus;

public class IncubationCentreMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        if(context == null)
        {
            await ConfigureMainMenuAsync(context);
        }
        var l = context.GetLocalizer<IncubationCentreResource>();

        context.Menu.AddItem(
            new ApplicationMenuItem(
                IncubationCentreMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 1
            )
        );

        var members = new ApplicationMenuItem(IncubationCentreMenus.Members, l["Menu:Members"], icon: "fa fa-user-circle-o", order: 2);
        members.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Mentors,
            l["Mentors"],
            url: "/Mentors",
            icon: "fa fa-question",
            requiredPermissionName: IncubationCentrePermissions.Mentors.Default,
            order: 3
        ));
        members.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Collaborators,
            l["Collaborators"],
            url: "/Collaborators",
            icon: "fa fa-user-plus",
            requiredPermissionName: IncubationCentrePermissions.Collaborators.Default,
            order: 2
        ));
        members.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Entrepreneurs,
            l["Entrepreneurs"],
            url: "/Entrepreneurs",
            icon: "fa fa-lightbulb-o",
            requiredPermissionName: IncubationCentrePermissions.Entrepreneurs.Default,
            order:1
        ));
        members.AddItem(new ApplicationMenuItem(
           IncubationCentreMenus.Investors,
           l["Investors"],
           url: "/Investors",
           icon: "fa fa-money",
           requiredPermissionName: IncubationCentrePermissions.Investors.Default,
           order: 4
       ));
        members.AddItem(new ApplicationMenuItem(
           IncubationCentreMenus.SystemManagers,
           l["SystemManagers"],
           url: "/SystemManagers",
           icon: "fa fa-key",
           requiredPermissionName: IncubationCentrePermissions.SystemManagers.Default,
           order: 5
       ));
        context.Menu.AddItem(members);



        var systemDescriptions = new ApplicationMenuItem(IncubationCentreMenus.SystemDescriptions, l["Menu:SystemManagement"], icon: "fa fa-cubes", order: 3, requiredPermissionName: IncubationCentrePermissions.SystemManagers.Default);
        systemDescriptions.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Applications,
            l["Menu:Applications"],
            url: "/Applications",
            icon: "fa fa-plus-circle",
            requiredPermissionName: IncubationCentrePermissions.Applications.Default
        ));
        systemDescriptions.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Projects,
            l["Menu:Projects"],
            url: "/Projects",
            icon: "fa fa-file-code-o",
            requiredPermissionName: IncubationCentrePermissions.Projects.Default
        ));
        systemDescriptions.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.SkillManagement,
            l["Menu:SkillDescriptions"],
            url: "/Skills",
            icon: "fa fa-pencil-square-o",
            requiredPermissionName: IncubationCentrePermissions.Skills.Default
        ));
        systemDescriptions.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Currencies,
            l["Menu:Currencies"],
            url: "/Currencies",
            icon: "fa fa-try",
            requiredPermissionName: IncubationCentrePermissions.Currencies.Default
        ));
        context.Menu.AddItem(systemDescriptions);


        context.Menu.TryRemoveMenuItem("Abp.Application.Main.Administration");
        var administration = new ApplicationMenuItem(IncubationCentreMenus.Administration, l["Menu:Administration"], icon: "fa fa-wrench", order: 4);

        //if (MultiTenancyConsts.IsEnabled)
        //{
        //    //administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        //}

        administration.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Settings,
            l["Menu:Settings"],
            "/SettingManagement",
            "fa fa-cog"
            ));

        administration.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.Roles,
            l["Menu:Roles"],
            "/Identity/Roles",
            "fa fa-user-shield"
            ));

        administration.AddItem(new ApplicationMenuItem(
           IncubationCentreMenus.Users,
           l["Menu:Users"],
           "/Identity/Users",
           "fa fa-users"
           ));

        var currentUser = context.ServiceProvider.GetService<ICurrentUser>();
        if (currentUser.IsInRole("admin") || currentUser.IsInRole("SystemManager") || currentUser.IsInRole("System Manager"))
        {
            context.Menu.AddItem(administration);
        }

    }
}
