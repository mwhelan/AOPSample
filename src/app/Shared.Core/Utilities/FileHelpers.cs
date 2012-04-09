using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Shared.Core.Utilities
{
    public static class FileHelpers
    {
        public static string GetProjectPath(string projectFolderName)
        {
            string path = Path.GetDirectoryName(typeof(FileHelpers).Assembly.Location);
            var directory = new DirectoryInfo(path);

            while (directory.GetFiles("*.sln").Length == 0)
            {
                directory = directory.Parent;
            }

            directory = (directory.GetDirectories("*", SearchOption.AllDirectories)
                .Where(folder => folder.Name == projectFolderName))
                .FirstOrDefault();

            if (directory == null)
                throw new DirectoryNotFoundException();

            return directory.FullName;
        }
    }
}
