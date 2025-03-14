namespace HelfMockit;

public static class GlobalMockServerService
{
    private readonly static HashSet<string> _routes = new HashSet<string>();

    public static void AddRoute(string route)
    {
        _routes.Add(route);
    }

    public static void RemoveRoute(string route)
    {
        _routes.Remove(route);
    }

    public static void ClearRoutes()
    {
        _routes.Clear();
    }

    public static IEnumerable<string> GetRoutes()
    {
        return _routes;
    }

    public static bool ContainsRoute(string route)
    {
        return _routes.Contains(route);
    }

}
