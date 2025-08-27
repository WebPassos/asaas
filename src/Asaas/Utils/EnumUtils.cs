﻿namespace AsaasSdk.Utils;

internal static class EnumUtils
{
    public static T Parse<T>(string @enum)
    {
        if (string.IsNullOrEmpty(@enum))
        {
            return default;
        }

        if (typeof(T).IsNullableEnum())
        {
            return (T)Enum.Parse(Nullable.GetUnderlyingType(typeof(T)), @enum);
        }

        return (T)Enum.Parse(typeof(T), @enum);
    }
}