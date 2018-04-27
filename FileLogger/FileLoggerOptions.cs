using System;
using System.Collections.Generic;
using System.Text;

namespace FileLogger
{
    public class FileLoggerOptions
    {
        private string path;
        private string fileExtension;
        public string Path { get => path; set => path = value; }
        public string FileExtension { get => fileExtension; set => fileExtension = value; }
        public FileLoggerOptions()
        {
            path = "Log/";
            fileExtension = ".txt";
        }
    }
}
