﻿using System.Runtime.CompilerServices;
using Elsa.Extensions;
using Elsa.Telnyx.Client.Models;
using Elsa.Telnyx.Client.Services;
using Elsa.Telnyx.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using Refit;

namespace Elsa.Telnyx.Activities;

/// <summary>
/// Stop recording the call.
/// </summary>
[Activity(Constants.Namespace, "Stop recording the call.", Kind = ActivityKind.Task)]
[FlowNode("Recording stopped", "Disconnected")]
public class StopRecording : Activity
{
    /// <inheritdoc />
    public StopRecording([CallerFilePath] string? source = null, [CallerLineNumber] int? line = null) : base(source, line)
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
        var request = new StopRecordingRequest(context.CreateCorrelatingClientState());
        var callControlId = CallControlId.Get(context);
        var telnyxClient = context.GetRequiredService<ITelnyxClient>();

        try
        {
            await telnyxClient.Calls.StopRecordingAsync(callControlId, request, context.CancellationToken);
            await context.CompleteActivityWithOutcomesAsync("Recording stopped");
        }
        catch (ApiException e)
        {
            if (!await e.CallIsNoLongerActiveAsync()) throw;
            await context.CompleteActivityWithOutcomesAsync("Disconnected");
        }
    }
}