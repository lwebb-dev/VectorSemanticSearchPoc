using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Models.Enums;

namespace VectorSemanticSearchPoc.Components.Pages
{
    public sealed partial class Embedding
    {
        private readonly string title = typeof(Embedding).Name;
        private string embeddingResult;
        private string input;
        private bool embedding = false;

        private async Task OnClickedAsync()
        {
            if (string.IsNullOrEmpty(this.input))
                return;

            this.embedding = true;

            try
            {
                JsonNode resultJson = await this.embeddingService.InvokeTitanTextEmbeddingV2ModelAsync(this.input, TitanTextEmbeddingV2Dimensions.Small);
                this.embeddingResult = resultJson.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured while Invoking the Embedding model: {ex.Message}");
            }

            this.embedding = false;
        }
    }
}
