using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace PlaywrightDemo;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestMSNClass : PageTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestMSNSearchBooks()
    {
        await Page.GotoAsync("https://www.msn.com/");
        await Page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter your search term" }).ClickAsync();
        await Page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter your search term" }).FillAsync("books");
        var Page1 = await Page.RunAndWaitForPopupAsync(async () =>
        {
            await Page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter your search term" }).PressAsync("Enter");
        });
        await Expect(Page1.GetByText("Amazon", new() { Exact = true }).First).ToBeVisibleAsync();
        await Expect(Page1.GetByRole(AriaRole.Heading, new() { Name = "Google Books" }).GetByRole(AriaRole.Link)).ToBeVisibleAsync();
        var Page2 = await Page1.RunAndWaitForPopupAsync(async () =>
        {
            await Page1.GetByRole(AriaRole.Link, new() { Name = "Videos" }).ClickAsync();
        });
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "Kids Books", Exact = true })).ToBeVisibleAsync();
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "Books to Read" })).ToBeVisibleAsync();
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "Top 10 Books", Exact = true })).ToBeVisibleAsync();
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "Dr. Seuss Books", Exact = true })).ToBeVisibleAsync();
        await Expect(Page2.GetByLabel("Suggested searches").GetByRole(AriaRole.List)).ToContainTextAsync("2nd Grade");
        await Page2.GetByRole(AriaRole.Link, new() { Name = "Images" }).ClickAsync();
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "Children's Books", Exact = true })).ToBeVisibleAsync();
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "Bookshelves" })).ToBeVisibleAsync();
        await Expect(Page2.GetByRole(AriaRole.Link, new() { Name = "School books", Exact = true })).ToBeVisibleAsync();

        //close the popup
        await Page2.CloseAsync();
        await Page1.CloseAsync();

        //close the main page
        await Page.CloseAsync();

    }

}
