using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VectorSemanticSearchPoc.Models.Enums;

namespace VectorSemanticSearchPoc.Services
{
    public class EmbeddingService(IAmazonBedrockRuntime bedrock, ILogger<IEmbeddingService> logger) : IEmbeddingService
    {
        private readonly ILogger<IEmbeddingService> logger = logger;
        private readonly IAmazonBedrockRuntime bedrock = bedrock;
        private const string TITAN_TEXT_EMBEDDING_V2_MODEL_ID = "amazon.titan-embed-text-v2:0";

        public async Task<JsonNode> InvokeTitanTextEmbeddingV2ModelAsync(string input, TitanTextEmbeddingV2Dimensions dimension = TitanTextEmbeddingV2Dimensions.Large)
        {
            string modelId = TITAN_TEXT_EMBEDDING_V2_MODEL_ID;
            string requestJson = JsonSerializer.Serialize(new
            {
                inputText = input.ToLower(), // convert all embeddings to lowercase to make L2Distance queries across embeddings more accurate.
                dimensions = dimension
            });

            var request = new InvokeModelRequest()
            {
                ModelId = modelId,
                Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(requestJson)),
                Accept = "*/*",
                ContentType = "application/json"
            };

            try
            {
                InvokeModelResponse response = await bedrock.InvokeModelAsync(request);
                JsonNode modelResponse = await JsonNode.ParseAsync(response.Body);
                JsonNode responseJson = modelResponse["embedding"] ?? string.Empty;
                return responseJson;
            }
            catch (AmazonBedrockRuntimeException ex)
            {
                this.logger.LogError("ERROR: Can't invoke '{modelId}. Reason: {ex.Message}'", modelId, ex.Message);
                throw;
            }
        }
    }
}
