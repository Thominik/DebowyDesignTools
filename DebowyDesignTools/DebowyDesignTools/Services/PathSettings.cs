using System.Text.RegularExpressions;

namespace DebowyDesignTools.Services;

public static class PathSettings
{
    public static string DirectoryPath => Environment.CurrentDirectory;

    public static FileInfo? GetLatestUserFile(string pattern)
    {
        var directoryInfo = new DirectoryInfo(DirectoryPath);
        var regex = new Regex(pattern);

        return directoryInfo.GetFiles("*.json")
            .Where(f => regex.IsMatch(f.Name))
            .OrderByDescending(f => f.LastWriteTime)
            .FirstOrDefault();
    }
}