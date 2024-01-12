namespace ThemeManager.Cli;

public static class DirectoryExtensions
{
    public static DirectoryInfo CopyTo(this DirectoryInfo directory, string path)
    {
        CopyFilesRecursively(directory.FullName, path);
        return new DirectoryInfo(path);
    }

    public static DirectoryInfo EnsureEmpty(this DirectoryInfo directory)
    {
        directory.EnsureDelete().Create();

        return directory;
    }

    private static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        //Now Create all of the directories
        foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        //Copy all the files & Replaces any files with the same name
        foreach (var newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
    public static DirectoryInfo EnsureDelete(this DirectoryInfo directory)
    {
        try
        {
            RecursiveDeleteDirectory(directory);
        }
        catch (DirectoryNotFoundException) { }

        return directory;
    }
    public static void RecursiveDeleteDirectory(DirectoryInfo directory)
    {
        var files = Directory.GetFiles(directory.FullName)
            .Select(name => new FileInfo(name));

        var dirs = Directory.GetDirectories(directory.FullName)
            .Select(name => new DirectoryInfo(name));

        foreach (var file in files)
        {
            File.SetAttributes(file.FullName, FileAttributes.Normal);
            file.Delete();
        }

        foreach (var dir in dirs)
        {
            RecursiveDeleteDirectory(dir);
        }

        directory.Delete(true);
    }
}
