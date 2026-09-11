namespace Helios.I18n;

/// <summary>
/// Label lookup. Helios ships English only: every key resolves against the
/// English dictionary, falling back to the key name if missing.
/// </summary>
public static class Labels
{
    private static readonly Dictionary<string, string> English = Languages.English.Translations;

    /// <summary>
    /// Retained for the window label-refresh subscriptions. Never fired in an
    /// English-only build, but keeping the event avoids touching every window.
    /// </summary>
#pragma warning disable CS0067
    public static event Action? LanguageChanged;
#pragma warning restore CS0067

    public static string CurrentLanguage => "en";

    /// <summary>No-op; the English dictionary is loaded at static init.</summary>
    public static void Initialize() { }

    /// <summary>Get the string for a key, or the key itself if unknown.</summary>
    public static string Get(string key)
        => English.TryGetValue(key, out var val) ? val : key;

    /// <summary>Get a format string and apply arguments.</summary>
    public static string Format(string key, params object[] args)
        => string.Format(Get(key), args);
}
