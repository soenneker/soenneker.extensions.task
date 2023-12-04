using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Soenneker.Extensions.Task;

/// <summary>
/// A collection of helpful Task extension methods
/// </summary>
public static class TaskExtension
{
    /// <summary>
    /// Equivalent to <code>task.ConfigureAwait(false);</code>
    /// </summary>
    public static ConfiguredTaskAwaitable NoSync(this System.Threading.Tasks.Task task)
    {
        return task.ConfigureAwait(false);
    }

    /// <summary>
    /// Equivalent to <code>task.ConfigureAwait(false);</code>
    /// </summary>
    public static ConfiguredTaskAwaitable<T> NoSync<T>(this Task<T> task)
    {
        return task.ConfigureAwait(false);
    }

    /// <summary>
    /// Equivalent to <code>new ValueTask(task)</code>
    /// </summary>
    public static ValueTask ToValueTask(this System.Threading.Tasks.Task task)
    {
        return new ValueTask(task);
    }

    /// <summary>
    /// Equivalent to <code>new ValueTask(task)</code>
    /// </summary>
    public static ValueTask<T> ToValueTask<T>(this Task<T> task)
    {
        return new ValueTask<T>(task);
    }
}