using System;
using System.IO;

namespace MapsTestApp
{
    public class Constants
    {
        public static readonly string DbFilePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "moneyback.db"
            );
    }
}
