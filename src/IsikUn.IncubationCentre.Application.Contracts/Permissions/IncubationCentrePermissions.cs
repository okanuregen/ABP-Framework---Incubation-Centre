﻿namespace IsikUn.IncubationCentre.Permissions;

public static class IncubationCentrePermissions
{
    public const string GroupName = "IncubationCentre";

    public static class Skills
    {
        public const string Default = GroupName + ".Skills";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
