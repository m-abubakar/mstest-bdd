using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Pages
{
    public class LoginPage : BasePage
    {
        /**
         * Web Selectors
         */
        private readonly string _menuButtonSelector = "#menu-toggle";
        private readonly string _usernameInputSelector = "#txt-username";
        private readonly string _passwordInputSelector = "#txt-password";
        private readonly string _loginButtonSelector = "#btn-login";
        private readonly string _loginLinkSelector = "text=Login";
        private readonly string _profileLinkSelector = "text=Profile";
        private readonly string _logoutLinkSelector = "text=Logout";
        //private readonly string _loginMessageSelector = "//p[text()='Please login to make appointment.']";

        /**
         * Constructor
         */
        public LoginPage(IPage page) : base(page) { }

        /**
         * Page Methods
         */

        // Perform login
        public async Task<LoginPage> PerformLoginAsync(string username, string password)
        {
            Console.WriteLine("Attempting to login...");
            await WriteTextAsync(_usernameInputSelector, username);
            await WriteTextAsync(_passwordInputSelector, password);
            await ClickAsync(_loginButtonSelector);
            return this;
        }

        // Verify login error
        public async Task<LoginPage> VerifyLoginErrorAsync(string expectedText)
        {
            Console.WriteLine("Verifying login error message...");
            var alertMessage = await ReadTextAsync("//p[@class='lead text-danger']");
            Assert.AreEqual(expectedText, alertMessage);
            return this;
        }

        // Verify password error from browser logs
        public async Task<LoginPage> VerifyLogErrorAsync(string error)
        {
            Console.WriteLine("Verifying login errors in browser console...");
            bool logPresent = await BrowserConsoleLogPresentAsync(error);
            Assert.IsTrue(logPresent);
            return this;
        }

        // Verify if the user is on the profile page
        public async Task<LoginPage> VerifyProfilePageAsync()
        {
            Console.WriteLine("Verifying profile page...");
            await ClickAsync(_menuButtonSelector);
            var profileLinkText = await ReadTextAsync(_profileLinkSelector);
            Assert.AreEqual("Profile", profileLinkText);
            await ClickAsync(_menuButtonSelector);
            return this;
        }

        // Perform logout
        public async Task<LoginPage> PerformLogoutAsync()
        {
            Console.WriteLine("Logging out...");
            await ClickAsync(_menuButtonSelector);
            await ClickAsync(_logoutLinkSelector);
            await ClickAsync(_menuButtonSelector);
            var loginLinkText = await ReadTextAsync(_loginLinkSelector);
            Assert.AreEqual("Login", loginLinkText);
            return this;
        }
    }
}

