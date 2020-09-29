using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TexasHoldemServer.DAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
