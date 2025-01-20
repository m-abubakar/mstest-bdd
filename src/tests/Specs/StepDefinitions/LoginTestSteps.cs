using System.Threading.Tasks;
using Pages;
using Reqnroll;

namespace Specs.StepDeinitions;

[Binding]
public class LoginTestStepDefinitions : BaseTest
{
    [Given("the home page is open")]
    public async Task GivenTheClientStartedShopping()
    {
        await homePage.NavigateToHomepageAsync();
    }

    [Given("the user navigates to the login page")]
    public async Task navigateToLoginPage()
    {
        loginPage = await homePage.NavigateToLoginPageAsync();
    }

    //[Given("the user enters the username {int} pcs of {string} to the basket")]
    [Given("the user enters the username {string} and password {string} into the fields")]
    public async Task enterUserName(string username, string password)
    {
        await loginPage.PerformLoginAsync(username, password);
    }


    [Then("the user is logged in")]
    public async Task verifyUserLogin(decimal expectedPrice)
    {
        await loginPage.VerifyLogErrorAsync("");
    }

    LoginPage loginPage;
}
