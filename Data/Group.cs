using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ApplicationUser> Students { get; set; }
        public Faculty Faculty { get; set; }
    }
}
