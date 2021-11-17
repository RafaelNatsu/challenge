using getSiteFile.DanMeTornodes;
using getSiteFile.Onionoo;
using System.IO;
// using Newtonsoft.Json;

namespace getSiteFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = @"./temp";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var valuer = new DanMeTornodes.DanMeTornodes();
            valuer.MakeInsertFile();
            var x = new Onionoo.Onionoo();
            x.MakeInsertFile();
        }
    }
}
