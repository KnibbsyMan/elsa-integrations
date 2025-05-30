﻿namespace Elsa.Telnyx.Options;

public class TelnyxOptions
{
    public Uri ApiUrl { get; set; } = new("https://api.telnyx.com");
    public string ApiKey { get; set; } = null!;
    public string? CallControlAppId { get; set; }
}