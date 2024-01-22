// -------------------------------------------------------------------------------------------------
// Copyright (c) Integrated Health Information Systems Pte Ltd. All rights reserved.
// -------------------------------------------------------------------------------------------------

using FhirStarter.CustomResources;
using FhirStarter.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.AddHttpClient<PokemonDataFhirHandler>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
    });

// Reads the 'FhirEngine' configuration section to add services
builder.AddFhirEngineServer()
    .AddCustomResource<Pokemon>();

var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DisplayRequestDuration();
    });
}

app.UseHsts()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapFhirEngineHealthChecks("/health");
        endpoints.MapFhirEngine();
    });

await app.RunAsync();
