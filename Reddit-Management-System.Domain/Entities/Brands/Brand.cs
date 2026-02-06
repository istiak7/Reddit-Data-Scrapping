using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Domain.Entities.Brands
{
    public class Brand : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;

        public Brand(string name, string description) 
        {
            Name = name;
            Description = description;
        }
        public static Brand Create(string name, string description)
        {
            return new Brand(name, description);
        }
        public void Update(string name, string description) 
        {
            Name = name;
            Description = description;
        }
    }
}
