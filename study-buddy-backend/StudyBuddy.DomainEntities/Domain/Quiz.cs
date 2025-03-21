using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.DomainEntities.Domain
{
    public class Quiz : BaseEntity
    {
        public string Question { get; set; }

        //public string Description { get; set; }

        //public bool MultipleCorrectAnswers { get; set; }

        //public string Explanation { get; set; }

        //public string Tip { get; set; }

        //public string Category { get; set; }

        //public string Difficulty { get; set; }

        //// Navigation Properties
        //public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
        //public virtual ICollection<CorrectAnswer> CorrectAnswers { get; set; } = new List<CorrectAnswer>();
        //public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
