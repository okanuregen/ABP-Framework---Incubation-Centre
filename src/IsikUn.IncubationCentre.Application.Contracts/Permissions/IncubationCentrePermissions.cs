namespace IsikUn.IncubationCentre.Permissions;

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

    public static class Mentors
    {
        public const string Default = GroupName + ".Mentors";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    
    public static class Entrepreneurs
    {
        public const string Default = GroupName + ".Entrepreneurs";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
