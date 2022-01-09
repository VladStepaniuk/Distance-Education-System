using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Data
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
