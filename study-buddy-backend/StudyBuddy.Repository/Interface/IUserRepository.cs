using StudyBuddy.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<StudyBuddyUser> GetAll();
        StudyBuddyUser Get(string id);
        void Insert(StudyBuddyUser entity);
        void Update(StudyBuddyUser entity);
        void Delete(StudyBuddyUser entity);
    }
}
