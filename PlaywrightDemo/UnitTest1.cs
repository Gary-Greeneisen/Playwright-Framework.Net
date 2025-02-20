namespace PlaywrightDemo
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class UnitTest1 : PageTest
    {
        [Test]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
        {
            /********** comment out ********************
             *** This code doesn't work because this class
             * inherts from PageTest
             * public class NUnitTest1 : PageTest 
             * and the method await Expect(Page) doesn't need
             * the Assertions namespace before the Expect(Page) method
             *****************************************
            // Launch the browser wait (1)-sec
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });
            var page = await browser.NewPageAsync();
            ********** comment out ****************/

            await Page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

            // create a locator
            var getStarted = Page.Locator("text=Get Started");

            // Expect an attribute "to be strictly equal" to the value.
            await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

            // Click the get started link.
            await getStarted.ClickAsync();

            // Expects the URL to contain intro.
            await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
        }
    }
}
