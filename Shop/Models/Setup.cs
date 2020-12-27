using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Setup
    {
        [Key]
        public int Id { get; set; }
        public bool SeedExecuted { get; set; }
        public DateTime InstallationDate { get; set; }
    }
}
