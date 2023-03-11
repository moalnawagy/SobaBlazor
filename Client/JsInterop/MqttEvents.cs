using Microsoft.JSInterop;

namespace SobaBlazor.Client.JsInterop;

public static class OnMessageReceived
{
    public static Action<string, string> Action { get; set; }
    [JSInvokable("OnMessageReceivedHandler")]
    public static void Handler(string key, string data)
    {
        Action?.Invoke(key, data);
    }
}

public static class OnConnectionChanged
{
    public static Action<string> Action { get; set; }
    [JSInvokable]
    public static void Handler(string value)
    {
        Action?.Invoke(value);
    }
}