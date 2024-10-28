


/*
Directory.GetFiles(path) returns a list with the FULL path of the files in the path, including file type
Directory.GetDirectories(path) returns a list with the FULL path of the directories in the path
Directory.CreateDirectory(path) creates a new directory, if the directory exists it doesn't create a new one
 */



using System;
using System.IO;

namespace fileOrdering
{
    public class Program
    {
        public static void CopyDirectory(string sourceDir, string destinationDir)
        {
            // Get the name of the source directory
            string sourceDirName = Path.GetFileName(sourceDir);
            string newDestinationDir = Path.Combine(destinationDir, sourceDirName);

            // Create the destination directory
            Directory.CreateDirectory(newDestinationDir);

            // Copy files to the destination directory
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(newDestinationDir, fileName);
                File.Copy(file, destFile, true); // Overwrite if the file exists
            }

            // Copy subdirectories and their contents
            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(newDestinationDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }
        public static void Main(string[] args)
        {
            string path = "\\dummy folder";
            string orderedPath = path + "-ordered"; // same as path but with -ordered after
            string[] splited;
            Console.WriteLine("original path: " + path);
            Console.WriteLine("ordered path:  " + orderedPath);

            // the files line returns the full path with the file type in it
            string[] files = Directory.GetFiles(path); // the files in the folder
            string[] folders = Directory.GetDirectories(path); // the folders in the folder
            
            // creating the list with file types(no doubles) and creates the folders
            List<string> fileTypes = new List<string>();
            foreach (string fileName in files)
            {
                splited = fileName.Split(new string[] { "." }, StringSplitOptions.None);
                string fileType = splited[splited.Length - 1];

                // creating the folder
                if (fileTypes.Contains(fileType) == false)
                {
                    fileTypes.Add(fileType);
                    Directory.CreateDirectory(orderedPath + "\\" + fileType);
                }
                splited = fileName.Split(new string[] { "\\" }, StringSplitOptions.None);
                try
                {
                    // adding the file to the folder
                    File.Copy(fileName, orderedPath + "\\" + fileType + "\\" + splited[splited.GetLength(0) - 1]);
                } catch (Exception ex) { }
            }

            if (folders.GetLength(0) != 0)
            {
                Directory.CreateDirectory(orderedPath + "\\folders");
                foreach (string foldName in folders)
                {
                    try
                    {
                        CopyDirectory(foldName, orderedPath + "\\folders");
                    } catch (Exception ex) { }
                }
            }
        }
    }
}
