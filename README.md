[![](https://img.shields.io/nuget/v/soenneker.extensions.task.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.task/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.task/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.task/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.task.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.task/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.task/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.task/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Task
Focused helpers for `ConfigureAwait(false)`, `Task`/`ValueTask` adaptation, synchronous bridges, and observed fire-and-forget work.

## Installation

```bash
dotnet add package Soenneker.Extensions.Task
```

## Avoid context capture

```csharp
using Soenneker.Extensions.Task;

HttpResponseMessage response = await httpClient.GetAsync(uri).NoSync();
```

`NoSync()` is shorthand for `ConfigureAwait(false)` for both `Task` and `Task<T>`. It prevents the await from requesting the current synchronization context; it does not guarantee that the continuation runs on a different thread.

## Adapt a Task to ValueTask

```csharp
ValueTask operation = task.ToValueTask();
ValueTask<int> valueOperation = valueTaskSource.ToValueTask();
```

The completed-success fast path avoids wrapping the original task. Faults and cancellation are preserved. Use this at API boundaries only; converting an existing `Task` does not recover an allocation that already occurred, and a `ValueTask` should normally be awaited only once.

## Synchronous bridges

```csharp
Result result = GetResultAsync().AwaitSync();
```

`AwaitSync()` blocks the calling thread and unwraps the original exception through `GetAwaiter().GetResult()`. `AwaitSyncSafe()` moves the wrapper await to `TaskScheduler.Default`, but it still blocks and cannot rescue a task whose completion itself requires the blocked UI or request context. Prefer async all the way.

The cancellation token on `AwaitSyncSafe()` controls scheduling of its wrapper; it does not cancel the supplied task. If the task is already complete, its result, exception, or cancellation wins directly.

## Observe detached work

```csharp
SaveAuditAsync().FireAndForgetSafe(exception => logger.LogError(exception, "Audit save failed"));
```

`FireAndForgetSafe()` observes task faults and optionally invokes a synchronous callback with the base exception. Callback failures are swallowed so the detached continuation cannot create another unobserved fault. This does not keep a process alive, retry failed work, propagate cancellation, or provide delivery guarantees; use a durable background queue for important work.
