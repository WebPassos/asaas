﻿using AsaasSdk.Extensions;
using AsaasSdk.Utils;

namespace AsaasSdk.Models.Common;

public class RequestParameters : Dictionary<string, string>
{
    public new string this[string key]
    {
        get
        {
            if (TryGetValue(key, out string? value))
            {
                return value;
            }

            return null!;
        }
        set
        {
            if (ContainsKey(key))
            {
                Remove(key);
            }

            base.Add(key, value);
        }
    }

    public void Add(string key, List<string> valueList)
    {
        foreach (string value in valueList)
        {
            Add(key, value);
        }
    }

    public void AddRange(RequestParameters map)
    {
        map.Keys.ToList().ForEach(key => Add(key, map[key]));
    }

    public new void Add(string key, string value)
    {
        if (value != null)
        {
            this[key] = value;
            return;
        }

        Remove(key);
    }

    public void Add(string key, DateTime? value)
    {
        if (value.HasValue)
        {
            Add(key, value.Value.ToApiRequest());
            return;
        }

        Remove(key);
    }

    public void Add(string key, Enum @enum)
    {
        if (@enum != null!)
        {
            Add(key, @enum.ToString());
            return;
        }

        Remove(key);
    }

    public void Add(string key, bool? value)
    {
        if (value != null)
        {
            Add(key, value.ToString()!);
            return;
        }

        Remove(key);
    }

    public void Add(string key, decimal? value)
    {
        if (value != null)
        {
            Add(key, value.ToString()!);
            return;
        }

        Remove(key);
    }

    public T Get<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(this[key]))
        {
            return default!;
        }

        if (typeof(T).IsEnum || typeof(T).IsNullableEnum())
        {
            return EnumUtils.Parse<T>(this[key]);
        }

        if (typeof(T) == typeof(DateTime?) || typeof(T) == typeof(DateTime))
        {
            return DateTimeUtils.Parse(this[key])!.Cast<T>();
        }

        if (typeof(T) == typeof(bool?) || typeof(T) == typeof(bool))
        {
            return Convert.ToBoolean(this[key]).Cast<T>();
        }

        return (T)(object)this[key];
    }

    public string Build()
    {
        if (Count == 0)
        {
            return string.Empty;
        }

        string queryString = "?";

        foreach (string key in Keys)
        {
            queryString += $"{key}={Uri.EscapeDataString(this[key])}";

            if (key != Keys.Last())
            {
                queryString += "&";
            }
        }

        return queryString;
    }
}