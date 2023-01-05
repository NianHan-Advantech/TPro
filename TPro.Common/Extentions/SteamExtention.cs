using System.IO;

namespace TPro.Common.Extentions
{
    public static class SteamExtention
    {
        public static string ReadToString(this Stream stream)
        {
            if (!stream.CanRead) return "";
            if (stream == null || stream.Length == 0) return "";
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}