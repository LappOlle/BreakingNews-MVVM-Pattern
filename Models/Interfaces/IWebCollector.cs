

namespace Models.Interfaces
{
    public interface IWebCollector
    {
        /// <summary>
        /// Html code from last search.
        /// </summary>
        string HTMLCode { get; set; }

        /// <summary>
        /// Sets the downloaded html code into the HTMLCode-property.
        /// Sets it to string.Empty if url is NULL, empty or not containing "http"
        /// </summary>
        /// <param name="url"></param>
        void GetHTMLFromUrl(string url);
    }
}
