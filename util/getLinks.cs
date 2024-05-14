using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDG.util
{
    internal class U
    {
        internal static Dictionary<string, string[]> getLinks(string url)
        {
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(currentDir);
            var dir = fileInfo.Directory!.Parent!.Parent!.Parent!.FullName;
            var file = System.IO.Path.Combine(dir, url);
            var path = Path.GetFullPath(file);
            using (StreamReader sr = File.OpenText(path))
            {
                var txt = sr.ReadToEnd();
                var lines = txt.Split("\n");
                var links = lines.Select(v =>
                {
                    var pair = v.Split(":");
                    return new
                    {
                        Key = pair[0].Trim(),
                        Value = pair[1].Split(",")
                        .Select(child => child.Trim())
                        .ToArray()
                    };
                }).ToDictionary(o => o.Key, o => o.Value);
                return links;

            }

        }
    }
}
