using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Data;
using VectorSemanticSearchPoc.Data.Models;
using VectorSemanticSearchPoc.Models;

namespace VectorSemanticSearchPoc.Services
{
    public class CourseService(SemanticSearchPocContext dbContext) : ICourseService
    {
        private readonly SemanticSearchPocContext dbContext = dbContext;

        public async Task<CourseCreateOrUpdateResults> CreateOrUpdateCoursesAsync(Course[] courses)
        {
            CourseCreateOrUpdateResults result = new();
            IEnumerable<Guid> existingCourseIds = await this.dbContext.Courses.Select(x => x.Id).ToListAsync();
            IEnumerable<Course> coursesToAdd = courses.Where(x => !existingCourseIds.Contains(x.Id));
            IEnumerable<Course> coursesToUpdate = courses.Where(x => existingCourseIds.Contains(x.Id));

            foreach (Course course in coursesToUpdate)
            {
                Course existingCourse = await this.dbContext.Courses.FirstOrDefaultAsync(x => x.Id == course.Id);
                existingCourse = course;
                result.NumCourseUpdated++;
                result.CoursesUpdated.Add(existingCourse);
            }

            this.dbContext.Courses.AddRange(coursesToAdd);
            result.NumCoursesAdded = coursesToAdd.Count();
            result.CoursesAdded = coursesToAdd.ToList();


            this.dbContext.SaveChanges();
            return result;
        }

        public async Task<CourseDeleteResults> DeleteCoursesAsync(Course[] courses)
        {
            this.dbContext.RemoveRange(courses);
            int numDeletedCourses = await this.dbContext.SaveChangesAsync();
            return new CourseDeleteResults()
            {
                NumCoursesDeleted = numDeletedCourses,
                CoursesDeleted = courses
            };
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await this.dbContext.Courses.ToListAsync();
        }

        public async Task<IList<L2CourseQueryResult>> QueryNearestCoursesByVectorAsync(Vector embeddedQuery, int take = 5)
        {
            return await this.dbContext.Courses
                .OrderBy(x => x.Embedding!.L2Distance(embeddedQuery))
                .Take(take)
                .Select(x => new L2CourseQueryResult
                {
                    Name = x.Name,
                    Sku = x.Sku,
                    L2Distance = x.Embedding!.L2Distance(embeddedQuery),
                })
                .ToListAsync();
        }
    }
}
