using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BDownloaderRe
{
    internal class MaterialDesignFonts
    {
        public static FontFamily STSONG { get; }

        static MaterialDesignFonts()
        {
            var fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Fonts\STSONG\");
            STSONG = new FontFamily(new Uri($"file:///{fontPath}"), "./#STSONG");
        }
    }
}
