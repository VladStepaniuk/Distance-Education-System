using DESystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Institution> Institutions { get; }
        IRepository<Course> Courses { get; }
        void Commit();
    }
}
