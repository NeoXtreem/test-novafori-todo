namespace NovaFori.Todos.Data;

internal static class DbInitializer
{
    public static void Initialize(TodosContext context)
    {
        context.Database.EnsureCreated();
    }
}