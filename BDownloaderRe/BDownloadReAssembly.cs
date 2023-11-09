using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BDownloaderRe
{
    internal class BDownloadReAssembly
    {
        public bool IsUrl(string url)
        {
            string pattern = @"^(https?|ftp|file|ws)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(url);
        }
    }
}
