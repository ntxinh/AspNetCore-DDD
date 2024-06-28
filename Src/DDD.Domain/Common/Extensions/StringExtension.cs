using System;

namespace DDD.Domain.Common.Extensions;

public static class StringExtension
{
    public static int WordCount(this string str) =>
        str.Split(
            new char[] { ' ', '.', '?' },
            StringSplitOptions.RemoveEmptyEntries).Length;
}
