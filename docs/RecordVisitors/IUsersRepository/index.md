﻿<!--  
  <auto-generated>   
    The contents of this file were generated by a tool.  
    Changes to this file may be list if the file is regenerated  
  </auto-generated>   
-->

# IUsersRepository Interface

**Namespace:** [RecordVisitors](../index.md)  
**Assembly:** RecordVisitors  
**Assembly Version:** 1.0.0+941d3002b81283b9d5c598cff49e5898a83bf6f6

the connection to the storage( database, csv , others)

```csharp
public interface IUsersRepository
```

## Properties

| Name                                               | Description                                                                        |
| -------------------------------------------------- | ---------------------------------------------------------------------------------- |
| [RecordJustLatest](properties/RecordJustLatest.md) | if record just latest user interaction or all ( the database will be inflated ...) |

## Methods

| Name                                                                        | Description                       |
| --------------------------------------------------------------------------- | --------------------------------- |
| [GetUserId(string)](methods/GetUserId.md)                                   | Get user id after the user name   |
| [GetUsers(uint)](methods/GetUsers.md)                                       | obtain latest users               |
| [SaveHistory(IRequestRecorded)](methods/SaveHistory.md)                     | save history for the user         |
| [SaveUser(Claim)](methods/SaveUser.md)                                      | save the user                     |
| [UserRecordedUrls(string, DateTime, DateTime)](methods/UserRecordedUrls.md) | obtain visitors from date to date |

___

*Documentation generated by [MdDocs](https://github.com/ap0llo/mddocs)*
