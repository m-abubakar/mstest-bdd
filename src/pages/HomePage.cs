using Microsoft.Playwright;

namespace Pages
{
    public class HomePage : BasePage
    {
        /**
         * Web Selectors
         */
        private readonly string _menuButtonSelector = "#menu-toggle";
        private readonly string _loginLinkSelector = "text=Login";

        /**
         * Constructor
         */
        public HomePage(IPage page) : base(page) { }

        /**
         * Page Methods
         */
        // Navigate to Homepage
        public async Task<HomePage> NavigateToHomepageAsync()
        {
            Console.WriteLine($"Opening Website: [{Config.AppUrl()}]");
            await NavigateToAsync(Config.AppUrl());
            return this;
        }

        // Navigate to LoginPage
        public async Task<LoginPage> NavigateToLoginPageAsync()
        {
            Console.WriteLine("Going to Login Page...");

            await ClickAsync(_menuButtonSelector);
            await ClickAsync(_loginLinkSelector);

            return new LoginPage(page_);
        }
    }
}

