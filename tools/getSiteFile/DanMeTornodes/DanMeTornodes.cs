using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using getSiteFile.common;

namespace getSiteFile.DanMeTornodes
{
    public class DanMeTornodes
    {
        string url = "https://www.dan.me.uk/tornodes";

        string path =  @"./temp/tornode.html";

        private void GetHtml(){
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(this.url,this.path);
            }
        }
        public void MakeInsertFile(){
            GetHtml();
            try
            {
                StreamReader streamReader = File.OpenText(path);
                FileStream fileStream = File.Create("./temp/tornode.clean.sql");
                string clean = "";
                bool beginTag = false;
                bool endTag = false;
                int count = 0;
                functions rgx = new functions(); //refatorar nome de métodos
                while ((clean = streamReader.ReadLine()) != null && endTag == false)
                {
                    if(clean == "<!-- __BEGIN_TOR_NODE_LIST__ //-->")
                    {
                        beginTag = true;
                        string formated = $"INSERT INTO RegistrationUrl (PathUrl) VALUES('{url}');\n";
                        byte[] info = new UTF8Encoding(true).GetBytes(formated);
                        fileStream.Write(info,0,info.Length);
                    }
                    else if (beginTag == true)
                    {
                        if(clean == "<!-- __END_TOR_NODE_LIST__ //-->")
                        {
                            endTag = true;
                            fileStream.Close();
                        }
                        else
                        {
                            clean = clean.ToString().Replace("<br />","");
                            string[] values = clean.Split('|');
                            string type = (rgx.regex().IsMatch(values[0].ToString()))?"IPV4":"IPV6";
                            //TODO: adicionar remoção de possiveis codigos maliciosos nos campos (js ou sql)
                            string formated = $"INSERT INTO `ListUrl` (`IdRegistrationUrl`, `IpAddress`, `Type`, `Name`, `Fingerprint`, `RouterPort`, `DirectoryPort`, `Flags`, `Uptime`, `Version`, `ContactInfo`) VALUES((SELECT Id FROM RegistrationUrl WHERE PathUrl = \"{url}\"), '{values[0]}', '{type}', '{values[1]}', '', {values[2]} ,{values[3]}, '{values[4]}', '{values[5]}', '{values[6]}', \"{values[7]}\" );\n";
                            byte[] info = new UTF8Encoding(true).GetBytes(formated);
                            fileStream.Write(info,0,info.Length);
                            count++;
                        } 
                    }
                }
                Console.WriteLine(count);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}