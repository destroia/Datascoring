using Data.Helpers;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class UserData : IUser
    {
        readonly DatascoringDBContext DB;
        public UserData(DatascoringDBContext db)
        {
            DB = db;
        }

        public async Task<bool> ChangePassword(ChangedPassword user)
        {
            var result = await DB.Users.FindAsync(user.UserId);
            if (result != null)
            {
                user.NewPassword = user.NewPassword.Trim().ToLower();
                result.Password = Encrypt.GetSHA256(user.NewPassword);

                DB.Users.Update(result);
                await DB.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Create(User user)
        {
            user.Name = user.Name.Trim();
            user.LastName = user.LastName.Trim();
            user.Password = user.Password.Trim().ToLower();
            user.Email = user.Email.ToLower().Trim();
            user.Password = user.Password.Trim().ToLower();

            user.Password = Encrypt.GetSHA256(user.Password);

            await DB.Users.AddAsync(user);
            await DB.SaveChangesAsync();

            return true;

        }

        public async Task<bool> Delete(int id)
        {
            var result = await DB.Users.FindAsync(id);
            if (result != null)
            {
                DB.Users.Remove(result);
                await DB.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> Exist(string Email)
        {
            var result = await DB.Users.Where( x => x.Email == Email).FirstOrDefaultAsync();

            return result == null ? false : true;
        }

        public async Task<List<User>> Get()
        {
            return await DB.Users.ToListAsync();
        }

        public async Task<User> Login(Login user)
        {
            user.Email = user.Email.Trim().ToLower();
            user.Password = user.Password.Trim().ToLower();

            user.Password = Encrypt.GetSHA256(user.Password);

            return await DB.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(User user)
        {
            var result = await DB.Users.FindAsync(user.Id);

            if (result != null)
            {
                result.Name = user.Name.Trim();
                result.LastName = user.LastName.Trim();
                result.Email = user.Email.ToLower().Trim();

                DB.Users.Update(result);
                await DB.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
