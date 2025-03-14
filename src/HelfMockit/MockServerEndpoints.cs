namespace HelfMockit;

public static class MockServerEndpoints
{
    public static void MapMockServerRoutes(this IEndpointRouteBuilder endpoint)
    {
        var groupMockServer = endpoint.MapGroup("/mock-server")
                          .RequireAuthorization("qa-engineer");

        groupMockServer.MapGet("", () =>
            Results.Ok(GlobalMockServerService.GetRoutes()));

        groupMockServer.MapPost("/", (string route) =>
        {
            GlobalMockServerService.AddRoute(route);
            return Results.Ok(new { message = "Route added", route });
        });

        groupMockServer.MapDelete("/{route}", (string route) =>
        {
            if (!GlobalMockServerService.ContainsRoute(route))
            {
                return Results.NotFound(new { message = "Route not found", route });
            }

            GlobalMockServerService.RemoveRoute(route);
            return Results.Ok(new { message = "Route removed", route });
        });

        groupMockServer.MapDelete("/", () =>
        {
            GlobalMockServerService.ClearRoutes();
            return Results.Ok(new { message = "All routes cleared" });
        });
    }
}
