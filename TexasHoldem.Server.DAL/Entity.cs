using System.ComponentModel.DataAnnotations;

namespace TexasHoldem.Server.DAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}