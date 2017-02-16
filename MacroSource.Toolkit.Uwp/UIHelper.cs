using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        
        public static Color GetAccentColor()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                return (Color)Application.Current.Resources["SystemAccentColor"];
            }
            else
            {
                return new UISettings().GetColorValue(UIColorType.Accent);
            }
        }


        public static void SetWindowLaunchSize(int height, int width)
        {
            ApplicationView.PreferredLaunchViewSize = new Size { Height = height, Width = width };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        /// <summary>
        /// 在新窗口中显示页面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async void ShowWindow<T>() where T : Page, new()
        {
            CoreApplicationView newCoreView = CoreApplication.CreateNewView();
            ApplicationView newAppView = null;
            int mainViewId = ApplicationView.GetApplicationViewIdForWindow(
              CoreApplication.MainView.CoreWindow);
            await newCoreView.Dispatcher.RunAsync(
              CoreDispatcherPriority.Normal,
              () =>
              {
                  newAppView = ApplicationView.GetForCurrentView();
                  Window.Current.Content = new T();
                  Window.Current.Activate();
              });
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
              newAppView.Id,
              ViewSizePreference.UseHalf,
              mainViewId,
              ViewSizePreference.UseHalf);
        }
    }
}
