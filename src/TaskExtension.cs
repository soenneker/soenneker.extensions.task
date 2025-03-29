using System;
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

    /// <summary>
    /// Synchronously awaits the specified <see cref="Task"/>.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> to await synchronously.</param>
    /// <remarks>
    /// This method blocks the calling thread until the task completes. This may lead to deadlocks
    /// if called on a context that does not allow synchronous blocking (e.g., UI thread).
    /// </remarks>
    /// <exception cref="OperationCanceledException">The task was canceled.</exception>
    /// <exception cref="Exception">The task faulted and threw an exception.</exception>
    public static void AwaitSync(this System.Threading.Tasks.Task task)
    {
        task.GetAwaiter().GetResult();
    }

    /// <summary>
    /// Synchronously awaits the specified <see cref="Task{TResult}"/> and returns its result.
    /// </summary>
    /// <typeparam name="T">The result type of the <see cref="Task{T}"/>.</typeparam>
    /// <param name="task">The <see cref="Task{T}"/> to await synchronously.</param>
    /// <returns>The result of the completed <see cref="Task{T}"/>.</returns>
    /// <remarks>
    /// This method blocks the calling thread until the task completes. This may lead to deadlocks
    /// if called on a context that does not allow synchronous blocking (e.g., UI thread).
    /// </remarks>
    /// <exception cref="OperationCanceledException">The task was canceled.</exception>
    /// <exception cref="Exception">The task faulted and threw an exception.</exception>
    public static T AwaitSync<T>(this Task<T> task)
    {
        return task.GetAwaiter().GetResult();
    }
}