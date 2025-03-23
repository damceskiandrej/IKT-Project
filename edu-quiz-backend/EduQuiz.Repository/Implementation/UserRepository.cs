using Microsoft.EntityFrameworkCore;
using EduQuiz.DomainEntities.Identity;
using EduQuiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<EduQuizUser> _entities;
        string errorMessage = string.Empty;
        public void Delete(EduQuizUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public EduQuizUser Get(string id)
        {
            var strGuid = id.ToString();
            return _entities.SingleOrDefault(s => s.Id == strGuid);
        }

        public IEnumerable<EduQuizUser> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void Insert(EduQuizUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(EduQuizUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Update(entity);
            _context.SaveChanges();
        }
    }
}
