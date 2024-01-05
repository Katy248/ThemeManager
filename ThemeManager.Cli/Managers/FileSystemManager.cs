using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Managers;
public class FileSystemManager
{
    public void EnsureDelete(string directoryName)
    {
        try
        {
            RecursiveDeleteDirectory(directoryName);
        }
        catch (DirectoryNotFoundException) { }
    }
    public void RecursiveDeleteDirectory(string directory)
    {
        string[] files = Directory.GetFiles(directory);
        string[] dirs = Directory.GetDirectories(directory);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            RecursiveDeleteDirectory(dir);
        }

        Directory.Delete(directory, false);
    }
    /// <summary>
    /// Not my method!!!
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="targetPath"></param>
    public void CopyFilesRecursively(string sourcePath, string targetPath)
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
