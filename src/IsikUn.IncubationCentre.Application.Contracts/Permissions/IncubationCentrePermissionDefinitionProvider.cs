﻿using IsikUn.IncubationCentre.Localization;
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

        var investorsPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.Investors.Default, L("Permission:Investors"));
        investorsPermission.AddChild(IncubationCentrePermissions.Investors.Create, L("Permission:Investors.Create"));
        investorsPermission.AddChild(IncubationCentrePermissions.Investors.Edit, L("Permission:Investors.Edit"));
        investorsPermission.AddChild(IncubationCentrePermissions.Investors.Delete, L("Permission:Investors.Delete"));

        var systemManagersPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.SystemManagers.Default, L("Permission:SystemManagers"));
        systemManagersPermission.AddChild(IncubationCentrePermissions.SystemManagers.Create, L("Permission:SystemManagers.Create"));
        systemManagersPermission.AddChild(IncubationCentrePermissions.SystemManagers.Edit, L("Permission:SystemManagers.Edit"));
        systemManagersPermission.AddChild(IncubationCentrePermissions.SystemManagers.Delete, L("Permission:SystemManagers.Delete"));

        var collaboratorsPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.Collaborators.Default, L("Permission:Collaborators"));
        collaboratorsPermission.AddChild(IncubationCentrePermissions.Collaborators.Create, L("Permission:Collaborators.Create"));
        collaboratorsPermission.AddChild(IncubationCentrePermissions.Collaborators.Edit, L("Permission:Collaborators.Edit"));
        collaboratorsPermission.AddChild(IncubationCentrePermissions.Collaborators.Delete, L("Permission:Collaborators.Delete"));

        var documentsPermission = IncubationCentreGroup.AddPermission(IncubationCentrePermissions.Documents.Default, L("Permission:Documents"));
        documentsPermission.AddChild(IncubationCentrePermissions.Documents.Create, L("Permission:Documents.Create"));
        documentsPermission.AddChild(IncubationCentrePermissions.Documents.Edit, L("Permission:Documents.Edit"));
        documentsPermission.AddChild(IncubationCentrePermissions.Documents.Delete, L("Permission:Documents.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IncubationCentreResource>(name);
    }
}
