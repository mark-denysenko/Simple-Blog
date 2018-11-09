using BusinessLayer.BusinessModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        bool Login(string nickname, string password);
        bool Register(string nickname, string password, string email);
        UserProfile GetUserProfile(string nickname);
        IEnumerable<string> GetAllNicknames();
    }
}
