using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace MacroSource.Toolkit.Uwp
{
    public static class FileHelper
    {
        public static async Task<bool> FileExistsInFolderAsync(StorageFolder folder, string filename)
        {
            var item = await folder.TryGetItemAsync(filename);
            return (item != null) && item.IsOfType(StorageItemTypes.File);
        }

        public static async Task<bool> FileExistsInSubtreeAsync(StorageFolder rootFolder, string filename)
        {
            if (filename.IndexOf('"') >= 0) throw new ArgumentException("filename");
            var options = new QueryOptions
            {
                FolderDepth = FolderDepth.Deep,
                UserSearchFilter = $"filename:=\"{filename}\"" // “:=” is the exact-match operator
            };
            var files = await rootFolder.CreateFileQueryWithOptions(options).GetFilesAsync();
            return files.Count > 0;
        }

        /// <summary>
        /// 指定文件名是否存在指定文件夹中
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="filename">文件名</param>
        /// <param name="isRecursive">递归查询</param>
        /// <returns></returns>
        public static async Task<bool> FileExistsAsync(StorageFolder folder, string filename,
            bool isRecursive = false) => isRecursive
                ? await FileExistsInSubtreeAsync(folder, filename)
                : await FileExistsInFolderAsync(folder, filename);
    }
}
