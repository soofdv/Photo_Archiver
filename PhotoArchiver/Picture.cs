using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PhotoArchiver
{
    public class Picture
    {
        public string id { get; set; }
        public string PictureName { get; set; }
        public string item_type { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string FilePath { get; set; }
        public string folder_id { get; set; }
    
    }
    public class PhotoInformation
    {
        public List<Picture> pictureInfo = new List<Picture>();

        public void getData()
        {
            System.Net.WebClient downloader = new System.Net.WebClient();
            string dataJson = downloader.DownloadString("http://localhost/php/photo_archiver/read.php");//hier moet je zelf ff kijken hoe de link er uitziet bij je.
            pictureInfo = JsonConvert.DeserializeObject<List<Picture>>(dataJson);
        }
    }
}