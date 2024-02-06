using Persistence;
using Presentation;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddApplication()
    .AddPresentation();


builder
    .Build()
    .ConfigurateRequestPipelines()
    .Run();

