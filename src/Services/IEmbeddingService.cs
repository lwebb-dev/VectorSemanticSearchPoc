using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Models.Enums;

namespace VectorSemanticSearchPoc.Services
{
    public interface IEmbeddingService
    {
        Task<JsonNode> InvokeTitanTextEmbeddingV2ModelAsync(string input, TitanTextEmbeddingV2Dimensions dimension = TitanTextEmbeddingV2Dimensions.Large);
    }
}