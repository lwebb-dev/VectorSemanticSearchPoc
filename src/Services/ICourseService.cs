using Pgvector;
using System.Collections.Generic;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Data.Models;
using VectorSemanticSearchPoc.Models;

namespace VectorSemanticSearchPoc.Services
{
    public interface ICourseService
    {
        Task<CourseCreateOrUpdateResults> CreateOrUpdateCoursesAsync(Course[] courses);
        Task<CourseDeleteResults> DeleteCoursesAsync(Course[] courses);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<IList<L2CourseQueryResult>> QueryNearestCoursesByVectorAsync(Vector embeddedQuery, int take = 5);
    }
}