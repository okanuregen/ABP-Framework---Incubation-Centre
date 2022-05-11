using System.Threading.Tasks;
using IsikUn.IncubationCentre.Localization;
using IsikUn.IncubationCentre.MultiTenancy;
using IsikUn.IncubationCentre.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

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

        var members = new ApplicationMenuItem(IncubationCentreMenus.SystemDescriptions, l["Menu:Members"], icon: "fa fa-user-circle-o", order: 2);
        members.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.SkillManagement,
            l["Mentors"],
            url: "/Mentors",
            icon: "fa fa-question",
            requiredPermissionName: IncubationCentrePermissions.Mentors.Default
        ));
        context.Menu.AddItem(members);



        var systemDescriptions = new ApplicationMenuItem(IncubationCentreMenus.SystemDescriptions, l["Menu:SystemManagement"], icon: "fa fa-cubes", order: 3);
        systemDescriptions.AddItem(new ApplicationMenuItem(
            IncubationCentreMenus.SkillManagement,
            l["Menu:SkillDescriptions"],
            url: "/Skills",
            icon: "fa fa-pencil-square-o",
            requiredPermissionName: IncubationCentrePermissions.Skills.Default
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
        context.Menu.AddItem(administration);
    }
}
