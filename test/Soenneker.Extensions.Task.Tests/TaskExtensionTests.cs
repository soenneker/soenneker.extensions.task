using AwesomeAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Soenneker.Extensions.Task.Tests;

public class TaskExtensionTests
{
    [Fact]
    public void Default()
    {

    }

    [Fact]
    public void AwaitSyncSafe_Task_CompletesSuccessfully()
    {
        System.Threading.Tasks.Task completedTask = System.Threading.Tasks.Task.Delay(100);
        Action act = () => completedTask.AwaitSyncSafe();
        act.Should().NotThrow();
    }

    [Fact]
    public void AwaitSyncSafe_Task_ThrowsException()
    {
        System.Threading.Tasks.Task faultedTask = System.Threading.Tasks.Task.FromException(new InvalidOperationException("Oops!"));
        Action act = () => faultedTask.AwaitSyncSafe();
        act.Should().Throw<InvalidOperationException>().WithMessage("Oops!");
    }

    [Fact]
    public void AwaitSyncSafe_Task_IsCancelled()
    {
        using var cts = new CancellationTokenSource();
        var task = System.Threading.Tasks.Task.Run(async () =>
        {
            await System.Threading.Tasks.Task.Delay(5000, cts.Token);
        }, cts.Token);

        cts.Cancel();

        Action act = () => task.AwaitSyncSafe(cts.Token);
        act.Should().Throw<OperationCanceledException>();
    }

    [Fact]
    public void AwaitSyncSafe_TaskOfT_CompletesSuccessfully()
    {
        Task<int> task = System.Threading.Tasks.Task.FromResult(42);
        int result = task.AwaitSyncSafe();
        result.Should().Be(42);
    }

    [Fact]
    public void AwaitSyncSafe_TaskOfT_ThrowsException()
    {
        Task<int> task = System.Threading.Tasks.Task.FromException<int>(new InvalidOperationException("Broken"));
        Action act = () => task.AwaitSyncSafe();
        act.Should().Throw<InvalidOperationException>().WithMessage("Broken");
    }

    [Fact]
    public void AwaitSyncSafe_TaskOfT_IsCancelled()
    {
        using var cts = new CancellationTokenSource();
        var task = System.Threading.Tasks.Task.Run(async () =>
        {
            await System.Threading.Tasks.Task.Delay(5000, cts.Token);
            return 5;
        }, cts.Token);

        cts.Cancel();

        Action act = () => task.AwaitSyncSafe(cts.Token);
        act.Should().Throw<OperationCanceledException>();
    }
}
