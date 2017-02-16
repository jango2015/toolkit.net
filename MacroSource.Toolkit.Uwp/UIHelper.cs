using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace MacroSource.Toolkit.Uwp
{
    public class UIHelper
    {
        public static async Task ShowDialogAsync(string contents, string title = null)
        {
            var dialog = title == null ?
                new MessageDialog(contents) { CancelCommandIndex = 0 } :
                new MessageDialog(contents, title) { CancelCommandIndex = 0 };
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, async () => await dialog.ShowAsync());
        }

        public static async Task ShowActionDialogAsync(string contents, Action callback,
                string title = null, string okButtonText = "OK", string cancelButtonText = "Cancel")
        {
            var dialog = title == null ?
                new MessageDialog(contents) { CancelCommandIndex = 1 } :
                new MessageDialog(contents, title) { CancelCommandIndex = 1 };
            dialog.Commands.Add(new UICommand(okButtonText, command => callback()));
            dialog.Commands.Add(new UICommand(cancelButtonText));
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, async () => await dialog.ShowAsync());
        }

        public static async Task ShowStoreRatingDialogAsync(string message,
                string okButtonText = "OK", string cancelButtonText = "Cancel")
        {
            Action handler = async () => await Launcher.LaunchUriAsync(new Uri(
                $"ms-windows-store:REVIEW?PFN={Package.Current.Id.FamilyName}"));
            var messageDialog = new MessageDialog(message) { CancelCommandIndex = 1 };
            messageDialog.Commands.Add(new UICommand(okButtonText, command => handler()));
            messageDialog.Commands.Add(new UICommand(cancelButtonText));
            await messageDialog.ShowAsync();
        }
    }
}
