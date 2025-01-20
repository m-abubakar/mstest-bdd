using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Utils;

namespace Pages
{
    public class BasePage
    {
        public BasePage(IPage page)
        {
            page_ = page;
            config_ = new ConfigReader();
        }

        public async Task<IElementHandle> WaitVisibilityAsync(string selector)
        {
            return await page_.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
        }

        public async Task<IElementHandle> ScrollToAsync(string selector)
        {
            var element = await WaitVisibilityAsync(selector);
            await element.ScrollIntoViewIfNeededAsync();
            return element;
        }

        public async Task ClickAsync(string selector)
        {
            var element = await WaitVisibilityAsync(selector);
            await element.ClickAsync();
        }

        public async Task WriteTextAsync(string selector, string text, bool clear = false)
        {
            var element = await WaitVisibilityAsync(selector);
            if (clear)
            {
                await element.FillAsync("");
            }
            await element.TypeAsync(text);
        }

        public async Task<string> ReadTextAsync(string selector)
        {
            var element = await WaitVisibilityAsync(selector);
            return await element.TextContentAsync();
        }

        public async Task SelectOptionByTextAsync(string selector, string text)
        {
            await page_.SelectOptionAsync(selector, new SelectOptionValue { Label = text });
        }

        public async Task SelectOptionByIndexAsync(string selector, int index)
        {
            var options = await page_.QuerySelectorAllAsync(selector + " option");
            if (index >= 0 && index < options.Count)
            {
                await options[index].ClickAsync();
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Option index is out of range.");
            }
        }

        public async Task<string> CurrentUrlAsync()
        {
            return page_.Url;
        }

        public async Task NavigateToAsync(string url)
        {
            await page_.GotoAsync(url);
        }

        public async Task HighlightElementAsync(IElementHandle element)
        {
            await page_.EvaluateAsync(@"element => {
                element.style.border = '3px solid red';
                element.style.backgroundColor = 'yellow';
            }", element);
        }

        public async Task RemoveHighlightAsync(IElementHandle element)
        {
            await page_.EvaluateAsync(@"element => {
                element.style.border = '';
                element.style.backgroundColor = '';
            }", element);
        }

        public async Task<string> AlertReadTextAsync()
        {
            var dialogText = "";
            page_.Dialog += (_, dialog) =>
            {
                dialogText = dialog.Message;
                dialog.AcceptAsync().Wait();
            };
            return dialogText;
        }

        public async Task AcceptAlertAsync()
        {
            page_.Dialog += async (_, dialog) => await dialog.AcceptAsync();
        }

        public async Task DismissAlertAsync()
        {
            page_.Dialog += async (_, dialog) => await dialog.DismissAsync();
        }

        public async Task<bool> BrowserConsoleLogPresentAsync(string message)
        {
            var consoleMessages = new List<string>();

            // Listen to console messages
            page_.Console += (_, consoleMessage) =>
            {
                consoleMessages.Add(consoleMessage.Text);
            };

            // Wait to capture logs (adjust duration as needed)
            await Task.Delay(1000);

            // Check if any log contains the specified message
            return consoleMessages.Any(log => log.Contains(message));
        }


        public ConfigReader Config => config_;
        protected readonly IPage page_;
        protected readonly ConfigReader config_;
    }
}

