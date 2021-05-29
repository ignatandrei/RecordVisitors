# Record Visitors

Record Latest visitors for .NET Core 

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ignatandrei/RecordVisitors/blob/master/LICENSE)  
[![BuildAndTest](https://github.com/ignatandrei/RecordVisitors/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ignatandrei/RecordVisitors/actions/workflows/dotnet.yml) 
[![codecov](https://codecov.io/gh/ignatandrei/RecordVisitors/branch/main/graph/badge.svg?token=ur3OvnDoGh)](https://codecov.io/gh/ignatandrei/RecordVisitors)
![Nuget](https://img.shields.io/nuget/v/recordvisitors)


# What it does

This project helps you to record what visitors have you on the site. It does not enforce [authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-5.0)

You can see the latest 5 minutes visitors by browsing to /recordVisitors/AllVisitors5Min

# How to use it

## Simple use

In Startup, 
```csharp
public void ConfigureServices(IServiceCollection services)
{
    //code omitted
    services.AddRecordVisitorsDefault();
    
}
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //code omitted 
    app.UseAuthentication();
    //put AFTER authentication
    app.UseRecordVisitors();
    //not necessary
    app.UseAuthorization();

}

```

Then browse to /recordVisitors/AllVisitors5Min

## Customizable uses

There are several classes that you can 