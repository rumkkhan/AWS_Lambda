using System;
using System.Collections.Generic;

namespace linqq.Models
{
    public partial class TagCourses
    {
        public int TagId { get; set; }
        public int CourseId { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
