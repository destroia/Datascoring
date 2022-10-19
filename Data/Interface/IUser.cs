using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IUser
    {
        Task<bool> Exist(string Email);
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> ChangePassword(ChangedPassword user);
        Task<User> Login(Login user);
        Task<List<User>> Get();
        Task<bool> Delete(int id);

    }
}
