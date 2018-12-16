[![Website](https://img.shields.io/badge/website-triggerme.io-lightgrey.svg)](https://www.triggerme.io)
[![Build Status](https://dev.azure.com/TriggerHelper/CSharpRequestLibrary/_apis/build/status/TriggerMe.CSharpRequestLibrary)](https://dev.azure.com/TriggerHelper/CSharpRequestLibrary/_build/latest?definitionId=1)
[![NuGet](https://img.shields.io/nuget/v/TriggerMe.Request.svg)](https://www.nuget.org/packages/TriggerMe.Request)

# TriggerMe C# Request Library
C# Library to make requests to the TriggerMe Forwarder/Router. TriggerMe provides an API proxy to Test, Forward and Route requests.

TriggerMe provides a serverless proxy to help diagnose API request issues, manage retries and mock responses. 

# Usage
To forward a HTTP Post request:

```csharp
var content = new { message = "Hello World" };
var strContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

Options.ApiKey = "[[Your API Key]]";
var client = new ForwardRequestClient();
var response = await client.PostAsync("[[TargetUrl]]", strContent);
Console.WriteLine(response.RequestId);
```

You can check the status of the request using the `RequestId` returned from the response object.

```csharp
var requestStatus = new ForwardRequestStatus();
var update = await requestStatus.CheckRequestAsync(response.RequestId);
Console.WriteLine(update.Result);
```

You can download the Request or Response body by accessing the `BlobUri` property of either `RequestLog.BlobUri` or `RequestLog.RetryRecords[].BlobUri`.

***Note:** These Blob URIs are timed so should not be cached*

```csharp
// Downloading the request blob
var req = await update.Request.DownloadBlobAsStringAsync();
Console.WriteLine(req);

// Downloading the final response blob
var response = await update.RetryRecords.Last().DownloadBlobAsStringAsync();
Console.WriteLine(response);
```

# Building
Prerequisites
- Microsoft .NET Core 2.2 SDK

To build the solution and the tests
```
dotnet build
```

To build the NuGet package
```
cd src/TriggerMe/Request
dotnet pack -c Release
```

# License
Copyright © 2018 TriggerMe (Stellar Tech Limited)

This program is free software: you can redistribute it and/or modify it under the terms of the Apache 2.0 license.

# Legal
By submitting a Pull Request, you disavow any rights or claims to any changes submitted to the TriggerMe project and assign the copyright of those changes to Stellar Tech Limited.

If you cannot or do not want to reassign those rights (your employment contract for your employer may not allow this), you should not submit a PR. Open an issue and someone else can do the work.

This is a legal way of saying "If you submit a PR to us, that code becomes ours". 99.9% of the time that's what you intend anyways; we hope it doesn't scare you away from contributing.