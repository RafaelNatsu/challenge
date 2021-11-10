using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using getSiteFile.common;

namespace getSiteFile.Onionoo
{
    public class Onionoo
    {
        string url = "https://onionoo.torproject.org/summary?limit=5000";
        string path = @"./temp/onionoo.json";

        public void MakeInsertFile(){
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(this.url,this.path);
            }
            try
            {
                string file = File.ReadAllText(this.path);
                // Console.WriteLine(valor);
                Root root = JsonSerializer.Deserialize<Root>(file);
                
                FileStream fileStream = File.Create("./temp/onionoo.clean.sql");
                string formated = $"INSERT INTO registration_url (path_url) VALUES('{this.url}');\n";
                byte[] info = new UTF8Encoding(true).GetBytes(formated);
                fileStream.Write(info,0,info.Length);

                root.relays.ForEach(x => {
                    for (int i = 0; i < x.a.Count; i++)
                    {
                        functions func = new functions();
                        string ip = x.a.ToArray()[i];
                        string type = (func.regex().IsMatch(ip))?"IPV4":"IPV6";
                        string query = $"INSERT INTO `list`(`id_registration_url`, `ip_address`, `type`, `name`, `fingerprint`,`flags`) VALUES ((SELECT id FROM registration_url WHERE path_url = \"{this.url}\"),'{ip}','{type}','{x.n}','{x.f}','{x.r}');\n";
                        info = new UTF8Encoding(true).GetBytes(query);
                        fileStream.Write(info,0,info.Length);
                    }
                });
                fileStream.Close();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}