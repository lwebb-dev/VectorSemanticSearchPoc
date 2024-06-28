using Pgvector;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Models;
using VectorSemanticSearchPoc.Models.Enums;

namespace VectorSemanticSearchPoc.Components.Pages
{
    public sealed partial class SemanticSearch
    {
        private readonly string title = typeof(SemanticSearch).Name;
        private string prompt;
        private bool searching = false;
        private IList<L2CourseQueryResult> courseViews = [];


        public async Task OnClickedAsync()
        {
            this.courseViews = [];
            this.searching = true;
            JsonNode embeddedJson = await this.embeddingService.InvokeTitanTextEmbeddingV2ModelAsync(prompt.ToLower(), TitanTextEmbeddingV2Dimensions.Small);
            float[] embeddedQuery = JsonSerializer.Deserialize<float[]>(embeddedJson);
            courseViews = await this.courseService.QueryNearestCoursesByVectorAsync(new Vector(embeddedQuery));

            this.searching = false;
        }
    }
}
