using System.Text.RegularExpressions;

namespace getSiteFile.common
{
    public class functions
    {
        string patter = @"\b(?:(?:2(?:[0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9])\.){3}(?:(?:2([0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9]))\b";
        
        public Regex regex(){
            return new Regex(this.patter);
        }
    }
}