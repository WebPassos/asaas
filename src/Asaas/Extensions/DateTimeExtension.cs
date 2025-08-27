﻿namespace Asaas.Extensions;

internal static class DateTimeExtension
{
    public static string ToApiRequest(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd");
    }
}