using System;
using System.Collections.Generic;

namespace linqq.Models
{
    public partial class Courses
    {
        public Courses()
        {
            TagCourses = new HashSet<TagCourses>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public float FullPrice { get; set; }
        public int? AuthorId { get; set; }

        public virtual Authors Author { get; set; }
        public virtual ICollection<TagCourses> TagCourses { get; set; }
    }
}
