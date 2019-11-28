using System;
using System.Collections.Generic;

namespace linqq.Models
{
    public partial class Tags
    {
        public Tags()
        {
            TagCourses = new HashSet<TagCourses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TagCourses> TagCourses { get; set; }
    }
}
