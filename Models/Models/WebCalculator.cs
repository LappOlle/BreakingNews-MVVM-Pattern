using Models.Interfaces;
using System.Text.RegularExpressions;

namespace Models.Models
{
    public class WebCalculator : IWebCalculator
    {
        public int CalculateNumberOfHits(IWebCollector webColl, string keyword)
        {
            if (webColl != null)
            {
                if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(webColl.HTMLCode))
                {
                    int NumberOfHits = Regex.Matches(webColl.HTMLCode, keyword, RegexOptions.IgnoreCase).Count;
                    return NumberOfHits;
                }
            }
            return -1;
        }
    }
}

