using System.Collections.Generic;
using VectorSemanticSearchPoc.Data.Models;

namespace VectorSemanticSearchPoc.Models
{
    public class CourseCreateOrUpdateResults
    {
        public int NumCoursesAdded { get; set; } = 0;
        public int NumCourseUpdated { get; set; } = 0;
        public IList<Course> CoursesAdded { get; set; } = new List<Course>();
        public IList<Course> CoursesUpdated { get; set; } = new List<Course>();
    }
}
