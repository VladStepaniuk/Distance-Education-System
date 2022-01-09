using DESystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _dbContext;
        private BaseRepository<Institution> _institutions;
        private BaseRepository<Course> _courses;

        public IRepository<Institution> InstitutionRepository
        {
            get
            {
                return _institutions ?? (_institutions = new BaseRepository<Institution>(_dbContext));
            }
        }

        public IRepository<Course> CourseRepository
        {
            get
            {
                return _courses ?? (_courses = new BaseRepository<Course>(_dbContext));
            }
        }

        public async void Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
