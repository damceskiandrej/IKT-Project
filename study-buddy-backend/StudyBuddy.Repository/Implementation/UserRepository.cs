using Microsoft.EntityFrameworkCore;
using StudyBuddy.DomainEntities.Identity;
using StudyBuddy.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<StudyBuddyUser> _entities;
        string errorMessage = string.Empty;
        public void Delete(StudyBuddyUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public StudyBuddyUser Get(string id)
        {
            var strGuid = id.ToString();
            return _entities.SingleOrDefault(s => s.Id == strGuid);
        }

        public IEnumerable<StudyBuddyUser> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void Insert(StudyBuddyUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(StudyBuddyUser entity)
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
