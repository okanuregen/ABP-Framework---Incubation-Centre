namespace IsikUn.IncubationCentre;

using IsikUn.IncubationCentre.Applications;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Tasks;
using Shouldly;
using Xunit;

public abstract class IncubationCentreDomainTestBase : IncubationCentreTestBase<IncubationCentreDomainTestModule>
{
    [Fact]
    public void CollaboratorTest()
    {
        Collaborator c = new Collaborator();
        c.isActivated.ShouldBeFalse(); 
        c.CollaboratoringProjects.ShouldBeNull();
        c.ProjectsCollaborators.ShouldBeNull();
    }

    [Fact]
    public void EntrepreneurTest()
    {
        Entrepreneur e = new Entrepreneur();
        e.isActivated.ShouldBeFalse();
        e.MyProjects.ShouldBeNull();
        e.ProjectsEntrepreneurs.ShouldBeNull();
    }

    [Fact]
    public void ApplicationTest()
    {
        Application a = new Application();
        a.SenderName = "Mertcan";
        a.SenderSurname = "Özdemir";
        a.SenderMail = "mertcan.ozdemir@hotmail.com";
        a.Explanation = "This is a test request...";


        a.SenderName.ShouldNotBeNull();
        a.SenderSurname.ShouldNotBeNull();
        a.SenderMail.ShouldNotBeNull();
        a.Explanation.ShouldNotBeNull();

    }

    [Fact]
    public void DocumentProjectTest()
    {
        Document d = new Document();
        d.Name = "RAD Document";

        Project p = new Project();
        d.Project = p;
        p.Name = "Incubation Center";
        

        d.Name.ShouldNotBeNull();
        d.Project.ShouldNotBeNull();

        p.Name.ShouldNotBeNull();
        p.InvesmentReady.ShouldBeFalse();
        p.Mentors.ShouldBeNull();
        p.Collaborators.ShouldBeNull();

        
    }

    [Fact]
    public void TaskTest()
    {
        Task t = new Task();
        Entrepreneur p = new Entrepreneur();
        t.AssignedTo= p;
        t.Title = "About Project";

        t.AssignedTo.ShouldNotBeNull();
        t.Title.ShouldNotBeNull();
        t.isDone.ShouldBeFalse();
        t.CompletionDate.ShouldBeNull();

        
    }

}
