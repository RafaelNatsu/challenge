using System.Collections.Generic;

namespace getSiteFile.Onionoo
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Relay
    {
        public string n { get; set; }
        public string f { get; set; }
        public List<string> a { get; set; }
        public bool r { get; set; }
    }

    public class Root
    {
        public string version { get; set; }
        public string build_revision { get; set; }
        public string relays_published { get; set; }
        public List<Relay> relays { get; set; }
        public int relays_truncated { get; set; }
        public string bridges_published { get; set; }
        public List<object> bridges { get; set; }
        public int bridges_truncated { get; set; }
    }


}