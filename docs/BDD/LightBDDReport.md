# Results of tests
## TestErrors
### TestNoServicesAdded
| Number| Name|Status|Comments|
| ----------- | ----------- |----------- |----------- |
|1|GIVEN Factory [RemoveServices: "True"] [RemoveFakeUser: "False"]|Passed||
|2|THEN The Application Will Have Error|Passed||
## TestHappyPath
### TestFakeUser
| Number| Name|Status|Comments|
| ----------- | ----------- |----------- |----------- |
|1|GIVEN The Application Starts|Passed|!!!Start application!!!!|
|2|WHEN The User Access The Url "/recordVisitors/AllVisitors5Min"|Passed||
|3|THEN The Response Should Contain [str: "JeanIrvine"]|Passed||
### TestEndpointGetHistoryUser
| Number| Name|Status|Comments|
| ----------- | ----------- |----------- |----------- |
|1|It should have UserId [userName: "JeanIrvine"]|Passed||
|1.1|GIVEN The Application Starts|Passed||
|1.2|WHEN The User Access The Url "/recordVisitors/GetUserId/JeanIrvine"|Passed||
|1.3|THEN The Response Should Be Guid|Passed||
|2|WHEN The User Access The Url "/recordVisitors/UserHistory/fd0467dd-8942-4863-ad35-1d33b818e310/1970-04-16/2021-06-01"|Passed||
|3|THEN The Response Should Be Something|Passed|the response is:[{"additionalData":null,"id":"d5aafd7e-9263-4f1b-80f6-acb7ad9083d9","url":"/recordVisitors/AllVisitors5Min","userRecordedId":"fd0467dd-8942-4863-ad35-1d33b818e310","dateRecorded":"2021-05-31T16:54:38.3656298Z"},{"additionalData":null,"id":"43d865f2-5392-48c0-9110-b38b1e573a34","url":"/recordVisitors/GetUserId/JeanIrvine","userRecordedId":"fd0467dd-8942-4863-ad35-1d33b818e310","dateRecorded":"2021-05-31T16:54:38.4790677Z"},{"additionalData":null,"id":"8c0d1870-76a4-45e7-a402-0c71247e703a","url":"/recordVisitors/UserHistory/fd0467dd-8942-4863-ad35-1d33b818e310/1970-04-16/2021-06-01","userRecordedId":"fd0467dd-8942-4863-ad35-1d33b818e310","dateRecorded":"2021-05-31T16:54:38.4935742Z"}]|
### TestUserEndpoint
| Number| Name|Status|Comments|
| ----------- | ----------- |----------- |----------- |
|1|It should have UserId [userName: "JeanIrvine"]|Passed||
|1.1|GIVEN The Application Starts|Passed||
|1.2|WHEN The User Access The Url "/recordVisitors/GetUserId/JeanIrvine"|Passed||
|1.3|THEN The Response Should Be Guid|Passed||
