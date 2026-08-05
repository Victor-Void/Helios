// Minimal stubs for types that NvidiaProcessScanner.cs and GPUModeControl.cs
// reference but that are too heavy (Avalonia, I18n, etc.) for the test build.

using Helios.Platform;

namespace Helios.Install
{
    internal static class Installer
    {
        public static bool StartupWillPrompt => false;
        public static void WaitForStartupDecision() { }
    }
}

namespace Helios.Platform.Linux
{
    partial class LinuxSystemIntegration
    {
        public sealed class PrivilegedSelf : IDisposable
        {
            public string Path { get; }
            internal PrivilegedSelf(string path, string? tempCopy) => Path = path;
            public void Dispose() { }
        }

        public static PrivilegedSelf ResolvePrivilegedSelf()
            => new(Environment.ProcessPath ?? "/proc/self/exe", null);
    }
}

namespace Helios
{
    internal static class App
    {
        public static IHardwareControl? Wmi => null;

        // LinuxNvidiaGpuControl uses App.System?.ShowNotification(); null = no-op.
        public static ISystemIntegration? System => null;

        // GPUModeControl calls App.Mode?.WaitForApply(); null = no-op.
        internal sealed class ModeControlStub
        {
            public void WaitForApply() { }
        }

        public static ModeControlStub? Mode => null;
    }
}
