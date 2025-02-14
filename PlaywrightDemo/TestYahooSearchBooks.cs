using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;


namespace PlaywrightDemo;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestYahooClass : PageTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestYahooSearchBooks()
    {
        await Page.GotoAsync("https://www.yahoo.com/");
        await Page.GetByRole(AriaRole.Searchbox, new() { Name = "Search query" }).ClickAsync();
        await Page.GetByRole(AriaRole.Searchbox, new() { Name = "Search query" }).FillAsync("books");
        var Page1 = await Page.RunAndWaitForPopupAsync(async () =>
        {
            await Page.GetByRole(AriaRole.Searchbox, new() { Name = "Search query" }).PressAsync("Enter");
        });
        await Expect(Page1.GetByRole(AriaRole.Link, new() { Name = "www.amazon.com Shop Books at" })).ToBeVisibleAsync();
        await Expect(Page1.GetByRole(AriaRole.Link, new() { Name = "Google Books", Exact = true })).ToBeVisibleAsync();
        await Page1.GetByRole(AriaRole.Link, new() { Name = "Images", Exact = true }).ClickAsync();
        await Expect(Page1.GetByRole(AriaRole.Link, new() { Name = "Children's books" })).ToBeVisibleAsync();
        await Expect(Page1.GetByRole(AriaRole.Link, new() { Name = "bookshelves" })).ToBeVisibleAsync();
        await Expect(Page1.GetByRole(AriaRole.Link, new() { Name = "Kids books", Exact = true })).ToBeVisibleAsync();
        await Page1.GetByRole(AriaRole.Link, new() { Name = "Kids books", Exact = true }).ClickAsync();
        await Expect(Page1.Locator("#yui_3_5_1_1_1739550373853_973")).ToContainTextAsync("Kids Books");
        await Expect(Page1.GetByRole(AriaRole.Link, new() { Name = "Kids Story Books" })).ToBeVisibleAsync();

        //close the popup   
        await Page1.CloseAsync();

        //close the main page   
        await Page.CloseAsync();


    }


}
