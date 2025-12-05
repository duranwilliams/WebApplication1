using System.Text.Json.Serialization;
using Microsoft.Extensions.Azure;

// Filereader designed around public data given at data.gov
// files: https://catalog.data.gov/dataset?q=&sort=metadata_created+desc

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["dbConnectionString:blob"]!, preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["dbConnectionString:queue"]!, preferMsi: true);
});

var app = builder.Build();
var filetxt = DatafileReader.ImportFiles();

var dataBag = new Todo[] {
    new(1, "Create database normalized for most files"),
    new(2, "Login and authentication", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Report and present the data on the front end", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, $"Last File Row Count: {DatafileReader.currentFileRowCounter.ToString()}")
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => dataBag);
todosApi.MapGet("/{id}", (int id) =>
    dataBag.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());



app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
