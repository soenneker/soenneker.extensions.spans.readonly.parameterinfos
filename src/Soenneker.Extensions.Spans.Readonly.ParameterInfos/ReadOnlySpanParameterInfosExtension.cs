using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Soenneker.Extensions.Spans.Readonly.ParameterInfos;

/// <summary>
/// Helpful extension methods surrounding ReadOnlySpan of ParameterInfo
/// </summary>
public static class ReadOnlySpanParameterInfosExtension
{
    /// <summary>
    /// Converts a span of <see cref="ParameterInfo"/> into an array of their corresponding <see cref="Type"/> objects.
    /// </summary>
    [Pure]
    public static Type[] ToTypes(this ReadOnlySpan<ParameterInfo> parameterInfos)
    {
        int length = parameterInfos.Length;

        if (length == 0)
            return [];

        if (length == 1)
            return [parameterInfos[0].ParameterType];

        var result = new Type[length];

        for (var i = 0; i < length; i++)
            result[i] = parameterInfos[i].ParameterType;

        return result;
    }

    /// <summary>
    /// Fills the destination span with the corresponding <see cref="Type"/> for each parameter (no allocations).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void FillTypes(this ReadOnlySpan<ParameterInfo> parameterInfos, Span<Type> destination)
    {
        if (destination.Length < parameterInfos.Length)
            throw new ArgumentException("Destination span is too small.", nameof(destination));

        for (var i = 0; i < parameterInfos.Length; i++)
            destination[i] = parameterInfos[i].ParameterType;
    }
}
