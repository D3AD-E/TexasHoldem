using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexasHoldem.Server.DAL;
using TexasHoldem.Server.DAL.Models;

namespace TexasHoldem.Server.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUser(string username);

        Task<User> GetOrCreateUser(string username, string password);

        Task CreateNewUser(string username, string password);

        Task UpdateUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly DbContextOptions<UserContext> _options;

        public UserService(DbContextOptions<UserContext> options)
        {
            _options = options;
        }

        public async Task CreateNewUser(string username, string password)
        {
            using var context = new UserContext(_options);

            username = username.ToLower();

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            User user = new User
            {
                Username = username,
                Password = passwordHash
            };

            context.Add(user);

            await context.SaveChangesAsync();
        }

        public async Task<User> GetOrCreateUser(string username, string password)
        {
            using var context = new UserContext(_options);

            username = username.ToLower();

            var profile = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (profile != null)
            {
                return profile;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);


            User user = new User
            {
                Username = username,
                Password = passwordHash
            };

            context.Add(user);

            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUser(string username)
        {
            using var context = new UserContext(_options);

            username = username.ToLower();

            return await context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task UpdateUser(User user)
        {
            using var context = new UserContext(_options);

            context.Update(user);

            await context.SaveChangesAsync();
        }
    }
}
