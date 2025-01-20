using Microsoft.Playwright;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using Pages;

[TestClass]
public class BaseTest
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        Console.WriteLine("Tests are starting!");

        string browserType = new ConfigReader().Browser().ToLower(CultureInfo.InvariantCulture);
        var playwright = await Playwright.CreateAsync();

        IBrowserType browserTypeInstance = browserType switch
        {
            "firefox" => playwright.Firefox,
            "chrome" => playwright.Chromium,
            "edge" => playwright.Chromium, // Edge is Chromium-based; you can configure it with a channel
            _ => throw new Exception("Browser unsupported")
        };

        browser_ = await browserTypeInstance.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // Set to `true` if you want headless mode
        });

        var context = await browser_.NewContextAsync();
        page = await context.NewPageAsync();

        await page.SetViewportSizeAsync(1920, 1080); // Simulating window maximize
        homePage = new HomePage(page);
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        Console.WriteLine("Tests are ending!");
        await browser_.CloseAsync();
    }

    private IBrowser browser_;
    protected IPage page;
    protected HomePage homePage;
}
