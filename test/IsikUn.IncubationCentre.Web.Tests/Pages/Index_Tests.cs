using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace IsikUn.IncubationCentre.Pages;

public class Index_Tests : IncubationCentreWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
