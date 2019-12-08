using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Introduction
{
  class Program
  {
    static void Main()
    {
      //string path = @"C:\windows";
      string path = @"C:\temp";
      var tempFiles = GetAllLargeFilesWithLinq(path);
      Console.WriteLine($"There are {tempFiles.Count()} files in the C:\\temp directory.");
      ShowLargeFilesWithoutLinq(path);
      Console.WriteLine("***");
      ShowLargeFilesWithLinq(path);
      Console.WriteLine("");
      Console.WriteLine("Press any key to exit:");
      Console.ReadKey();
    }

    public static void ShowLargeFilesWithLinq(string path, int numberOfFiles = 5)
    {
      var query = new DirectoryInfo(path).GetFiles()
                      .OrderByDescending(file => file.Length)
                      .Take(numberOfFiles);

      foreach (var file in query)
      {
        Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
      }
    }

    public static IEnumerable<FileInfo> GetAllLargeFilesWithLinq(string path)
    {
      var query = new DirectoryInfo(path).GetFiles()
                      .OrderByDescending(file => file.Length);
      return query;
    }

    private static void ShowLargeFilesWithoutLinq(string path)
    {
      DirectoryInfo directory = new DirectoryInfo(path);
      FileInfo[] files = directory.GetFiles();
      Array.Sort(files, new FileInfoComparer());
      for (int i = 0; i < 5; i++)
      {
        FileInfo file = files[i];
        Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
      }
    }
  }

  public class FileInfoComparer : IComparer<FileInfo>
  {
    public int Compare(FileInfo x, FileInfo y)
    {
      return y.Length.CompareTo(x.Length);
    }
  }
}
