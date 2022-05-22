using IsikUn.IncubationCentre.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace IsikUn.IncubationCentre.Permissions;

public class IncubationCentrePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var IncubationCentreGroup = context.AddGroup(IncubationCentrePermissions.GroupName, L("Permission:IncubationCentre"));

        var skillsPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.Skills.Default, L("Permission:Skills"));
        skillsPermission.AddChild(IncubationCentrePermissions.Skills.Create, L("Permission:Skills.Create"));
        skillsPermission.AddChild(IncubationCentrePermissions.Skills.Edit, L("Permission:Skills.Edit"));
        skillsPermission.AddChild(IncubationCentrePermissions.Skills.Delete, L("Permission:Skills.Delete"));

        var mentorsPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.Mentors.Default, L("Permission:Mentors"));
        mentorsPermission.AddChild(IncubationCentrePermissions.Mentors.Create, L("Permission:Mentors.Create"));
        mentorsPermission.AddChild(IncubationCentrePermissions.Mentors.Edit, L("Permission:Mentors.Edit"));
        mentorsPermission.AddChild(IncubationCentrePermissions.Mentors.Delete, L("Permission:Mentors.Delete"));

        var entrepreneursPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.Entrepreneurs.Default, L("Permission:Entrepreneurs"));
        entrepreneursPermission.AddChild(IncubationCentrePermissions.Entrepreneurs.Create, L("Permission:Entrepreneurs.Create"));
        entrepreneursPermission.AddChild(IncubationCentrePermissions.Entrepreneurs.Edit, L("Permission:Entrepreneurs.Edit"));
        entrepreneursPermission.AddChild(IncubationCentrePermissions.Entrepreneurs.Delete, L("Permission:Entrepreneurs.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IncubationCentreResource>(name);
    }
}
