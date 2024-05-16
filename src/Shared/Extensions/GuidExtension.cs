namespace WaveChat.Common.Extensions;

using System;

public static class GuidExtension
{
    public static string Shrink(this Guid guid)
    {
        return guid.ToString().Replace("-", "").Replace(" ", "");
    }
    public static bool Equals(this Guid guid, string stringGuid)
    {
        return guid.ToString() == stringGuid;
    }
}