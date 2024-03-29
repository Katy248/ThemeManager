﻿namespace ThemeManager.Cli.FileSystem;
public static class FileSystemExtensions
{
    public static DirectoryInfo CopyTo(this DirectoryInfo directory, string path)
    {
        CopyFilesRecursively(directory.FullName, path);

        return new DirectoryInfo(path);
    }
    public static DirectoryInfo EnsureCreated(this DirectoryInfo directory)
    {
        if (!directory.Exists)
        {
            directory.Create();
        }
        return directory;
    }
    public static FileInfo EnsureCreated(this FileInfo file)
    {
        if (!file.Exists)
        {
            file.Create();
        }
        return file;
    }
    public static DirectoryInfo EnsureEmpty(this DirectoryInfo directory)
    {
        directory.EnsureDeleted();
        directory.Create();

        return directory;
    }
    public static void EnsureDeleted(this DirectoryInfo directory)
    {
        try
        {
            DeleteDirectoryRecursively(directory);
        }
        catch (DirectoryNotFoundException) { }
    }
    private static void DeleteDirectoryRecursively(DirectoryInfo directory)
    {
        var files = directory.GetFiles();
        var dirs = directory.GetDirectories();

        foreach (var file in files)
        {
            File.SetAttributes(file.FullName, FileAttributes.Normal);
            file.Delete();
        }

        foreach (var dir in dirs)
        {
            DeleteDirectoryRecursively(dir);
        }

        directory.Delete(false);
    }
    public static FileInfo Child(this DirectoryInfo directory, string childName)
    {
        return new FileInfo(Path.Combine(directory.FullName, childName));
    }
    /// <summary>
    /// Not my method!!!
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="targetPath"></param>
    private static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        //Now Create all of the directories
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        //Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}
