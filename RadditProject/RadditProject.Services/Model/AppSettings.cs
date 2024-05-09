using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadditProject.Services.Model
{
    public class AppSettings
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthAPI { get; set; }
        public string SubRedittAPI { get; set;}
        public string SubRedditPost { get; set; }
        public string SubRedditUserFlair { get; set; }
    }
}
