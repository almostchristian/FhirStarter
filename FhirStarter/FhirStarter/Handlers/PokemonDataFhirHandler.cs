using FhirStarter.CustomResources;
using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core;
using Ihis.FhirEngine.Core.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace FhirStarter.Handlers;

public class PokemonDataFhirHandler
{
    private readonly HttpClient httpClient;
    private readonly ILogger<PokemonDataFhirHandler> logger;

    public PokemonDataFhirHandler(HttpClient httpClient, ILogger<PokemonDataFhirHandler> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    [FhirHandler("ValidatePokemonSpeciesName", HandlerCategory.PreCRUD, FhirInteractionType.Create)]
    public async Task ValidatePokemonSpeciesNameAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"pokemon/{pokemon.Name.ToLowerInvariant()}"));
        if (!result.IsSuccessStatusCode)
        {
            var message = await result.Content.ReadAsStringAsync();
            logger.LogError(message);
            throw new ResourceNotValidException($"Pokemon species name '{pokemon.Name}' is invalid", OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.NotFound);
        }
    }
}
