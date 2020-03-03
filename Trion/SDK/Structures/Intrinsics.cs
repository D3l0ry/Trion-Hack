namespace Trion.SDK.Structures
{
    internal static class Intrinsics
    {
        public static bool IsNull<T>(this T type) where T : unmanaged => typeof(T) == null;
    }
}