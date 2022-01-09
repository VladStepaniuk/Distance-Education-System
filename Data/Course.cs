using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Data
{
    public class Course
    {
        public int Id { get; set; }
        public string NumberOrName { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
