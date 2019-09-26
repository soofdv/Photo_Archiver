using System;

namespace PhotoArchiver
{
    internal class Picture
    {
        public string PictureName { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string FilePath { get; set; }
        public string itemType { get; set; }
        public string parentDirPath { get; set; }
    }
}