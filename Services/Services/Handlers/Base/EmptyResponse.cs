namespace Service.Services.Handlers.Base;

public sealed class EmptyResponse
{
    private EmptyResponse() { }

    public static EmptyResponse Instance { get; } = new();
} 