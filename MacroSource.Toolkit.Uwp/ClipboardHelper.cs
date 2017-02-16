using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace MacroSource.Toolkit.Uwp
{
    public class ClipboardHelper
    {
        /// <summary>
        /// 从剪切板获取文字
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetTextAsync()
        {
            DataPackageView dataPackage = Clipboard.GetContent();
            return dataPackage.Contains(StandardDataFormats.Text) ?
                await dataPackage.GetTextAsync() : "";
        }

        /// <summary>
        /// 将文字存入剪切板
        /// </summary>
        /// <param name="text"></param>
        public static void SetText(string text)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
        }
    }
}
