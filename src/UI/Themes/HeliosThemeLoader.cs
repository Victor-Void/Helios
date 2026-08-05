using System;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Media;

namespace Helios.UI.Themes;

/// <summary>
/// Reads Material You colours from the same <c>colors.json</c>
/// that the Quickshell dotfiles shell watches, and applies them
/// to the Avalonia application resource dictionary as
/// <c>{DynamicResource key}</c> entries.
/// 
/// Keys use the same snake_case names as the JSON file:
///   background, surface, surface_container, on_surface,
///   primary, on_primary, outline, error, etc.
/// 
/// A FileSystemWatcher reloads the palette when the wallpaper
/// changes (matugen regenerates colors.json).
/// 
/// When <c>colors.json</c> is absent the theme CSS fallback
/// colours defined in HeliosTheme.axaml are used.
/// </summary>
public static class HeliosThemeLoader
{
    private const string ColorsJsonPath =
        ".local/state/quickshell/user/generated/colors.json";

    private static string _resolvedPath = "";
    private static FileSystemWatcher? _watcher;

    /// <summary>
    /// Apply the Material You palette (or fallback defaults) and start
    /// watching for wallpaper changes.
    /// </summary>
    public static void Initialize()
    {
        string homePath = Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile);
        _resolvedPath = Path.GetFullPath(
            Path.Combine(homePath, ColorsJsonPath));

        ApplyColorsFromFile();

        // Watch for wallpaper changes (matugen regenerates colors.json)
        try
        {
            var dir = Path.GetDirectoryName(_resolvedPath);
            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                _watcher = new FileSystemWatcher(dir, "colors.json")
                {
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _watcher.Changed += (_, _) =>
                    Avalonia.Threading.Dispatcher.UIThread.Post(ApplyColorsFromFile);
            }
        }
        catch
        {
            // File watching is a best-effort convenience
        }
    }

    /// <summary>
    /// Read the M3 palette from <c>colors.json</c> and register
    /// every key as a <c>DynamicResource</c> that the theme styles
    /// can reference.
    /// </summary>
    public static void ApplyColorsFromFile()
    {
        if (!File.Exists(_resolvedPath))
            return;

        try
        {
            string json = File.ReadAllText(_resolvedPath);
            using var doc = JsonDocument.Parse(json);
            var resources = Application.Current!.Resources;

            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                string? value = prop.Value.GetString();
                if (string.IsNullOrEmpty(value) || value.Length != 7 || value[0] != '#')
                    continue;

                if (Color.TryParse(value, out var color))
                {
                    resources[prop.Name] = color;
                    resources[$"{prop.Name}_brush"] = new SolidColorBrush(color);
                }
            }
        }
        catch
        {
            // Best effort — fallback to static theme.
        }
    }

    /// <summary>
    /// Stop watching for palette changes.
    /// </summary>
    public static void Shutdown()
    {
        _watcher?.Dispose();
        _watcher = null;
    }
}
