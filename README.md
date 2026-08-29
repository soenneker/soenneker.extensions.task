[![](https://img.shields.io/nuget/v/soenneker.extensions.task.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.task/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.task/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.task/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.task.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.task/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.task/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.task/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Task
A collection of helpful Task extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.Task
```

## Quick start

```csharp
using Soenneker.Extensions.Task;

// Given an existing System.Threading.Tasks.Task named task:
var result = task.NoSync();
```

## Common operations

- `NoSync()` - Configures an awaiter used to await this `Task` to continue on a different context. Equivalent to `task.ConfigureAwait(false);`.
- `ToValueTask()` - Converts a `Task` to a `ValueTask`. If the task is already completed successfully, returns a completed `ValueTask`. Equivalent to `new ValueTask(task)`.
- `AwaitSync()` - Synchronously awaits the specified `Task`. This method blocks the calling thread until the task completes.
- `AwaitSyncSafe()` - Attempts to synchronously wait in a way that avoids common deadlocks by running the await on the ThreadPool. This is still not a silver bullet; prefer async all the way when possible.
- `FireAndForgetSafe()` - Fires the task without awaiting it, while ensuring any exceptions are observed. Optionally forwards the exception to `onException`.
