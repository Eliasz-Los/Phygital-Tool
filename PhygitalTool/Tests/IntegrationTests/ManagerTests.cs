using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Phygital.BL;
using Phygital.Domain.Themas;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ManagerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void AddTheme_Should_Validate_Name_NotNull_And_Max_Hundred_Chars_Long()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IThemeManager>();

        // Act
        var th1 = new Theme { Title = null, Description = "Dit is de description" };
        var th2 = new Theme { Title = new string('a', 101), Description = "Dit is de description" };

        var actionNull = () => manager.AddSubThema(th1);
        var actionTooLong = () => manager.AddSubThema(th2);

        // Assert
        Assert.Throws<ValidationException>(actionNull);
        Assert.Throws<ValidationException>(actionTooLong);
        
        
    }
}