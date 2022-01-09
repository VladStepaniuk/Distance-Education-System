using DESystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Institution> InstitutionRepository { get; }
        IRepository<Course> CourseRepository { get; }
        void Commit();
    }
}
