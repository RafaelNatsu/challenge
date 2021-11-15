using getSiteFile.DanMeTornodes;
using getSiteFile.Onionoo;

// using Newtonsoft.Json;

namespace getSiteFile
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var valuer = new DanMeTornodes.DanMeTornodes();
            valuer.MakeInsertFile();
            var x = new Onionoo.Onionoo();
            x.MakeInsertFile();
        }
    }
}
