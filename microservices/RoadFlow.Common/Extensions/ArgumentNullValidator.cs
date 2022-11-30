using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RoadFlow.Common.Extensions;

public static class ArgumentNullValidator
{
    public static T ThrowIfNullOrReturn<T>([NotNull] T argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        return argument;
    }
}