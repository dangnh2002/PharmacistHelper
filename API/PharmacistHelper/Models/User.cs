using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacistHelper.Models
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
