using Pgvector;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Data.Models;
using VectorSemanticSearchPoc.Models;
using VectorSemanticSearchPoc.Models.Enums;

namespace VectorSemanticSearchPoc.Components.Pages
{
    public sealed partial class CourseCreate
    {
        private readonly string title = typeof(CourseCreate).Name;
        private bool creating = false;
#pragma warning disable IDE0044 // Add readonly modifier
        private Course formData = new();
#pragma warning restore IDE0044 // Add readonly modifier
        private CourseCreateOrUpdateResults result = null;
        private bool CourseCreated => this.result != null;

        private async Task OnClickedAsync()
        {
            this.creating = true;

            string embeddingInput = $"{formData.Name} {formData.Description}";
            JsonNode embeddedJson = await this.embeddingService
                .InvokeTitanTextEmbeddingV2ModelAsync(embeddingInput.ToLower(), TitanTextEmbeddingV2Dimensions.Small);
            float[] embeddedFloatArray = JsonSerializer.Deserialize<float[]>(embeddedJson);
            formData.Embedding = new Vector(embeddedFloatArray);
            formData.Id = Guid.NewGuid();

            this.result = await this.courseService.CreateOrUpdateCoursesAsync([formData]);
            this.creating = false;
        }
    }
}