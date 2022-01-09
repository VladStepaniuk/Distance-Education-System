using DESystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        private BaseRepository<Institution> _institutions;
        private BaseRepository<Course> _courses;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Institution> Institutions
        {
            get
            {
                return _institutions ?? (_institutions = new BaseRepository<Institution>(_dbContext));
            }
        }

        public IRepository<Course> Courses
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
    }
}
