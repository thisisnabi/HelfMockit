using HelfMockit;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// configure settings
builder.Services.Configure<MockServerSettings>(builder.Configuration.GetSection(MockServerSettings.SectionName));

builder.Services.AddHttpClient("bService",(sp, client) =>
{
    client.BaseAddress = new Uri("external-service-url");
})
.ConfigureAdditionalHttpMessageHandlers((handlers, sp) =>
{
    var hostEnv = sp.GetRequiredService<IHostEnvironment>();
    if (hostEnv.IsStaging())
        handlers.Add(ActivatorUtilities.CreateInstance<MockHttpClientHandler>(sp));
});

var app = builder.Build();

app.MapMockServerRoutes();

app.Run();
