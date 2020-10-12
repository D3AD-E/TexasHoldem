using Microsoft.EntityFrameworkCore;
using TexasHoldem.Server.DAL.Models;

namespace TexasHoldem.Server.DAL
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}