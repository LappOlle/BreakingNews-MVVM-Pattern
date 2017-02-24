using Models.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Models.Models
{
    public class WebCollector : IWebCollector
    {
        public string HTMLCode { get; set; }

        public void GetHTMLFromUrl(string url)
        {
            if (String.IsNullOrEmpty(url) == false && url.Contains("http"))
            {
                using (WebClient client = new WebClient())
                {
                    Task<string> workingTask = Task.Run(async () => await client.DownloadStringTaskAsync(url));
                    HTMLCode = workingTask.Result;
                    return;
                }
            }
            HTMLCode = string.Empty;
        }
    }
}
