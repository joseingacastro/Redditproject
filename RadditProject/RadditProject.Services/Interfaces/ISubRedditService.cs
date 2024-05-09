using RadditProject.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadditProject.Services.Interfaces
{
    public interface ISubRedditService
    {
        Task<Root> GetSubRedditData(TokenResponse tokenResponse, string api);
    }
}
