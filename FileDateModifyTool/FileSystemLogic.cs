using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDateModifyTool
{
    class FileSystemLogic
    {
        public static string SelectFileOrDirectory()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "Folder";
            openFileDialog.CheckFileExists = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Regex.IsMatch(openFileDialog.FileName, @"Folder$"))
                {
                    return Path.GetDirectoryName(openFileDialog.FileName);
                }
                else
                {
                    return openFileDialog.FileName;
                }
            }
            else
            {
                return "";
            }
        }

        public static string[] ReadFileSystemInfo(string path)
        {
            FileAttributes attributes = File.GetAttributes(path);
            FileSystemInfo fileSystemInfo;

            if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                fileSystemInfo = new DirectoryInfo(path);
            }
            else
            {
                fileSystemInfo = new FileInfo(path);
            }

            return new string[] {
                fileSystemInfo.FullName,
                fileSystemInfo.LastWriteTime.ToString(),
                fileSystemInfo.CreationTime.ToString(),
                fileSystemInfo.LastAccessTime.ToString(),
            };

        }

        public static void SetTimestamp(string path, DateTime dateTime, string targetTimestamp)
        {
            FileAttributes attributes = File.GetAttributes(path);
            FileSystemInfo fileSystemInfo;

            if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                fileSystemInfo = new DirectoryInfo(path);
            }
            else
            {
                fileSystemInfo = new FileInfo(path);
            }

            if (targetTimestamp == "LastWriteTime")
            {
                fileSystemInfo.LastWriteTime = dateTime;
            }
            else if (targetTimestamp == "LastAccessTime")
            {
                fileSystemInfo.LastAccessTime = dateTime;
            }
            else if (targetTimestamp == "CreationTime")
            {
                fileSystemInfo.CreationTime = dateTime;
            }
            else
            {
                throw new Exception("Invalid timestamp type");
            }
        }
    }
}
