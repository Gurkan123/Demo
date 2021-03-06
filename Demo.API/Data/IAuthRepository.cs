using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.API.Models;

namespace Demo.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
         Task<User> GetUser(int id);
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();


    }
}