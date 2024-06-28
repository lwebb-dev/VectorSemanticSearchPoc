using System.Collections.Generic;
using VectorSemanticSearchPoc.Data.Models;

namespace VectorSemanticSearchPoc.Models
{
    public class CourseDeleteResults
    {
        public int NumCoursesDeleted { get; set; } = 0;
        public IList<Course> CoursesDeleted { get; set; }
    }
}
