using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Soenneker.Extensions.Task;

/// <summary>
/// A collection of helpful Task extension methods
/// </summary>
public static class TaskExtension
{
    /// <summary>
    /// Configures an awaiter used to await this <see cref="Task"/> to continue on a different context.
    /// Equivalent to <code>task.ConfigureAwait(false);</code>.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> to configure.</param>
    /// <returns>A configured task awaitable.</returns>
    public static ConfiguredTaskAwaitable NoSync(this System.Threading.Tasks.Task task)
    {
        return task.ConfigureAwait(false);
    }

    /// <summary>
    /// Configures an awaiter used to await this <see cref="Task{TResult}"/> to continue on a different context.
    /// Equivalent to <code>task.ConfigureAwait(false);</code>.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by this <see cref="Task{TResult}"/>.</typeparam>
    /// <param name="task">The <see cref="Task{TResult}"/> to configure.</param>
    /// <returns>A configured task awaitable.</returns>
    public static ConfiguredTaskAwaitable<T> NoSync<T>(this Task<T> task)
    {
        return task.ConfigureAwait(false);
    }

    /// <summary>
    /// Converts a <see cref="Task"/> to a <see cref="ValueTask"/>. 
    /// If the task is already completed successfully, returns a completed <see cref="ValueTask"/>. 
    /// Equivalent to <code>new ValueTask(task)</code>.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> to convert.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    public static ValueTask ToValueTask(this System.Threading.Tasks.Task task)
    {
        if (task.IsCompletedSuccessfully)
            return new ValueTask();

        return new ValueTask(task);
    }

    /// <summary>
    /// Converts a <see cref="Task{TResult}"/> to a <see cref="ValueTask{TResult}"/>. 
    /// If the task is already completed successfully, returns a completed <see cref="ValueTask{TResult}"/> with the result.
    /// Equivalent to <code>new ValueTask(task)</code>.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the task.</typeparam>
    /// <param name="task">The <see cref="Task{TResult}"/> to convert.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.</returns>
    public static ValueTask<T> ToValueTask<T>(this Task<T> task)
    {
        if (task.IsCompletedSuccessfully)
            return new ValueTask<T>(task.Result);

        return new ValueTask<T>(task);
    }
}