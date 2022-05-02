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
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IncubationCentreResource>(name);
    }
}
