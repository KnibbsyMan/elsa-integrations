﻿using System.Runtime.CompilerServices;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;

namespace Elsa.Telnyx.Activities;

/// <inheritdoc />
public class AnswerCall : AnswerCallBase
{
    /// <inheritdoc />
    public AnswerCall([CallerFilePath] string? source = null, [CallerLineNumber] int? line = null) : base(source, line)
    {
    }

    /// <summary>
    /// The activity to schedule when the call was successfully answered.
    /// </summary>
    [Port] public IActivity? Connected { get; set; }

    /// <summary>
    /// The activity to schedule when the call was no longer active.
    /// </summary>
    [Port] public IActivity? Disconnected { get; set; }

    /// <inheritdoc />
    protected override async ValueTask HandleConnectedAsync(ActivityExecutionContext context) => await context.ScheduleActivityAsync(Connected);

    /// <inheritdoc />
    protected override async ValueTask HandleDisconnectedAsync(ActivityExecutionContext context) => await context.ScheduleActivityAsync(Disconnected);
}