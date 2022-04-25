using IsikUn.IncubationCentre.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace IsikUn.IncubationCentre.Permissions;

public class IncubationCentrePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IncubationCentrePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(IncubationCentrePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IncubationCentreResource>(name);
    }
}
