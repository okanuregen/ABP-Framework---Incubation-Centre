using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Milestones;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.ProjectsCollaborators;
using IsikUn.IncubationCentre.ProjectsFounders;
using IsikUn.IncubationCentre.ProjectsInvestors;
using IsikUn.IncubationCentre.ProjectsMentors;
using IsikUn.IncubationCentre.Skills;
using IsikUn.IncubationCentre.SystemManagers;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class IncubationCentreDbContext :
    AbpDbContext<IncubationCentreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Mentor> Mentors { get; set; }
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Entrepreneur> Entrepreneurs { get; set; }
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<SystemManager> SystemManagers { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<PersonSkill> PeopleSkills { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectCollaborator> ProjectsCollaborators { get; set; }
    public DbSet<ProjectFounder> ProjectsFounders { get; set; }
    public DbSet<ProjectInvestor> ProjectsInvestors { get; set; }
    public DbSet<ProjectMentor> ProjectsMentors { get; set; }
    public DbSet<Milestone> Milestones { get; set; }

    public IncubationCentreDbContext(DbContextOptions<IncubationCentreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
        builder.Entity<Project>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Project",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<ProjectCollaborator>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "ProjectCollaborator",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<ProjectFounder>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "ProjectFounder",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<ProjectInvestor>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "ProjectInvestor",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<ProjectMentor>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "ProjectMentor",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Person>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "People",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasMany(c => c.FoundedProjects).WithMany(c => c.Founders).UsingEntity<ProjectFounder>(
            j => j.HasOne(pt => pt.Project).WithMany(c => c.ProjectsFounders).HasForeignKey(g => g.ProjectId),
            j => j.HasOne(pt => pt.Person).WithMany(c => c.ProjectsFounders).HasForeignKey(g => g.PersonId),
            j => j.HasKey(t => new { t.PersonId, t.ProjectId }));
        });
        builder.Entity<PersonSkill>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "PersonSkill",
                IncubationCentreConsts.DbSchema);
        });
        builder.Entity<Mentor>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Mentors",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasMany(c => c.MentoringProjects).WithMany(c => c.Mentors).UsingEntity<ProjectMentor>(
            j => j.HasOne(pt => pt.Project).WithMany(c => c.ProjectsMentors).HasForeignKey(g => g.ProjectId),
            j => j.HasOne(pt => pt.Mentor).WithMany(c => c.ProjectsMentors).HasForeignKey(g => g.MentorId),
            j => j.HasKey(t => new { t.MentorId, t.ProjectId }));
        });
        builder.Entity<Investor>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Investors",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasMany(c => c.InvestedProjects).WithMany(c => c.Investors).UsingEntity<ProjectInvestor>(
            j => j.HasOne(pt => pt.Project).WithMany(c => c.ProjectsInvestors).HasForeignKey(g => g.ProjectId),
            j => j.HasOne(pt => pt.Investor).WithMany(c => c.ProjectsInvestors).HasForeignKey(g => g.InvestorId),
            j => j.HasKey(t => new { t.InvestorId, t.ProjectId }));
        });
        builder.Entity<Entrepreneur>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Entrepreneurs",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<Collaborator>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Collaborators",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasMany(c => c.CollaboratoringProjects).WithMany(c => c.Collaborators).UsingEntity<ProjectCollaborator>(
            j => j.HasOne(pt => pt.Project).WithMany(c => c.ProjectsCollaborators).HasForeignKey(g => g.ProjectId),
            j => j.HasOne(pt => pt.Collaborator).WithMany(c => c.ProjectsCollaborators).HasForeignKey(g => g.CollaboratorId),
            j => j.HasKey(t => new { t.CollaboratorId, t.ProjectId }));

        });
        builder.Entity<SystemManager>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "SystemManagers",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<Skill>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Skills",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasMany(c => c.People).WithMany(c => c.Skills).UsingEntity<PersonSkill>(
            j => j.HasOne(pt => pt.Person).WithMany(c => c.PeopleSkills).HasForeignKey(g => g.PersonId),
            j => j.HasOne(pt => pt.Skill).WithMany(c => c.PeopleSkills).HasForeignKey(g => g.SkillId),
            j => j.HasKey(t => new { t.SkillId, t.PersonId }));

        });
        builder.Entity<Document>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Documents",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Milestone>(b =>
        {
            b.ToTable(IncubationCentreConsts.DbTablePrefix + "Milestone",
                IncubationCentreConsts.DbSchema);
            b.ConfigureByConvention();
        });

    }
}
