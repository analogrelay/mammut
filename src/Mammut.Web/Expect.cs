using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Mammut.Web;

public static class Expect
{
    public static T NotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string expr = null)
        where T : class
    {
        if (value is null)
        {
            throw new UnreachableException($"Expected '{expr}' to not be null.");
        }

        return value;
    }
    
    public static T NotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string expr = null)
        where T : struct
    {
        if (value is null)
        {
            throw new UnreachableException($"Expected '{expr}' to not be null.");
        }

        return value.Value;
    }
}

public static class ExpectExtensions
{
    public static T Require<T>([NotNull] this T? value, [CallerArgumentExpression(nameof(value))] string expr = null)
        where T: class => Expect.NotNull(value, expr);
    
    public static T Require<T>([NotNull] this T? value, [CallerArgumentExpression(nameof(value))] string expr = null)
        where T: struct => Expect.NotNull(value, expr);
}