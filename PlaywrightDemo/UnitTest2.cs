using Microsoft.Playwright;

namespace PlaywrightDemo;

public class UnitTest2
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        /********** comment out ********************
         *** This code works because this class
         * does not inhert from PageTest
         * public class NUnitTest2
         * but the method await Expect(Page) DOES need
         * the Assertions namespace before the Expect(Page) method
         ********** comment out ********************/

        // Launch the browser wait (1)-sec
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });
        //var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });
        //var browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });

        var Page = await browser.NewPageAsync();

        // Navigate to a website
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        //await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
        await Assertions.Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.Locator("text=Get Started");

        // Expect an attribute "to be strictly equal" to the value.
        //await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");
        await Assertions.Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Assertions.Expect(Page).ToHaveURLAsync(new Regex(".*intro"));

    }
}
