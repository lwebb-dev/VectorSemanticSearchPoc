using System.Collections.Generic;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Data.Models;

namespace VectorSemanticSearchPoc.Components.Pages
{
    public sealed partial class DatabaseView
    {
        private readonly string title = typeof(DatabaseView).ToString();
        private IEnumerable<Course> Courses { get; set; } = new List<Course>();

        protected override async Task OnInitializedAsync()
        {
            this.Courses = await this.courseService.GetAllCoursesAsync();
        }
    }
}
