
# Record Latest visitors for .NET Core 

<img src="https://github.com/ignatandrei/RecordVisitors/raw/main/docs/rv.png" width="50px"  height="50px" style="height: 50px; width:50px;" /> [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ignatandrei/RecordVisitors/blob/master/LICENSE)  
[![BuildAndTest](https://github.com/ignatandrei/RecordVisitors/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ignatandrei/RecordVisitors/actions/workflows/dotnet.yml) 
[![codecov](https://codecov.io/gh/ignatandrei/RecordVisitors/branch/main/graph/badge.svg?token=ur3OvnDoGh)](https://codecov.io/gh/ignatandrei/RecordVisitors)
![Nuget](https://img.shields.io/nuget/v/recordvisitors) ![Docs](https://readthedocs.org/projects/record-visitors/badge/?version=latest)

<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->



# What it does

This project helps you to record what visitors have you on the site. It does not enforce [authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-5.0)

You can see the latest 5 minutes visitors by browsing to /recordVisitors/AllVisitors5Min or /recordVisitors/AllVisitors/5 

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
    //code omitted
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.UseVisitors();
    });


}

```

Then browse to /recordVisitors/AllVisitors5Min or /recordVisitors/AllVisitors/5


You can see also the history for the URL that the user have browsed : goto 
/recordVisitors/UserHistory/{userId}/{dateFrom:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}/{dateTo?}

( to find the user id , you can also goto /recordVisitors/GetUserId/{userName})

You can see also the classes documentation at https://ignatandrei.github.io/RecordVisitors/RecordVisitors/


## Customizable uses

There are several interfaces that you can implement via DI


| Name                                                        | Description                                            |
| ----------------------------------------------------------- | ------------------------------------------------------ |
| [IRecordVisitorFunctions](IRecordVisitorFunctions/index.md) | how to indentify the user                              |
| [IRequestRecorded](IRequestRecorded/index.md)               | request of the user                                    |
| [IUserRecorded](IUserRecorded/index.md)                     | the user recorded interface to store data              |
| [IUsersRepository](IUsersRepository/index.md)               | the connection to the storage( database, csv , others) |



The project comes with his implementation - however, you can add yours.


## Contributors ✨

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="http://msprogrammer.serviciipeweb.ro/"><img src="https://avatars.githubusercontent.com/u/153982?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Andrei Ignat</b></sub></a><br /><a href="https://github.com/ignatandrei/RecordVisitors/commits?author=ignatandrei" title="Tests">⚠️</a> <a href="https://github.com/ignatandrei/RecordVisitors/commits?author=ignatandrei" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!


## Links

- Project homepage: https://github.com/ignatandrei/RecordVisitors 
- Repository: https://github.com/ignatandrei/RecordVisitors/
- Issue tracker: https://github.com/ignatandrei/RecordVisitors/issues
- Documentation: Project homepage: https://record-visitors.readthedocs.io/en/latest/ (or simple https://ignatandrei.github.io/RecordVisitors/)
- Blog: http://msprogrammer.serviciipeweb.ro/category/recordvisitors/ 

## Licence

The code in this project is licensed under MIT license.