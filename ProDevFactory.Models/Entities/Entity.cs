using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProDevFactory.Models.Entities
{
    public abstract class Entity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
