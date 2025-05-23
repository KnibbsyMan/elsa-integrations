﻿using System.Runtime.CompilerServices;
using Elsa.Extensions;
using Elsa.Telnyx.Client.Services;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;

namespace Elsa.Telnyx.Activities;

/// <inheritdoc />
[FlowNode("Alive", "Dead", "Done")]
[Activity(Constants.Namespace, "Get the status of a call.", Kind = ActivityKind.Task)]
public class GetCallStatus : Activity<bool>
{
    /// <inheritdoc />
    public GetCallStatus([CallerFilePath] string? source = null, [CallerLineNumber] int? line = null) : base(source, line)
    {
    }

    /// <summary>
    /// Unique identifier and token for controlling the call.
    /// </summary>
    [Input(
        DisplayName = "Call Control ID",
        Description = "Unique identifier and token for controlling the call.",
        Category = "Advanced"
    )]
    public Input<string> CallControlId { get; set; } = null!;

    /// <inheritdoc />
    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var client = context.GetRequiredService<ITelnyxClient>();
        var callControlId = CallControlId.Get(context);
        var response = await client.Calls.GetStatusAsync(callControlId, context.CancellationToken);
        var isAlive = response.Data.IsAlive;
        var outcome = isAlive ? "Alive" : "Dead";

        Result.Set(context, isAlive);

        await context.CompleteActivityWithOutcomesAsync(outcome, "Done");
    }
}