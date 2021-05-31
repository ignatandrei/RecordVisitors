# Rec

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
|2|WHEN The User Access The Url "/recordVisitors/UserHistory/b1cb47d2-01ec-4b02-9855-6a00c6f8b82e/1970-04-16/2021-06-01"|Passed||
|3|THEN The Response Should Be Something|Passed|the response is:[{"additionalData":null,"id":"f2f44105-1109-45bb-b1e2-854c6092efe1","url":"/recordVisitors/AllVisitors5Min","userRecordedId":"b1cb47d2-01ec-4b02-9855-6a00c6f8b82e","dateRecorded":"2021-05-31T16:38:16.355897Z"},{"additionalData":null,"id":"20b02d1b-e1df-4a82-b2c8-0c6b9ffe9050","url":"/recordVisitors/GetUserId/JeanIrvine","userRecordedId":"b1cb47d2-01ec-4b02-9855-6a00c6f8b82e","dateRecorded":"2021-05-31T16:38:16.4697633Z"},{"additionalData":null,"id":"31b8af1d-4c29-44ef-8c7a-f0a0037cad72","url":"/recordVisitors/UserHistory/b1cb47d2-01ec-4b02-9855-6a00c6f8b82e/1970-04-16/2021-06-01","userRecordedId":"b1cb47d2-01ec-4b02-9855-6a00c6f8b82e","dateRecorded":"2021-05-31T16:38:16.4911534Z"}]|
### TestUserEndpoint
| Number| Name|Status|Comments|
| ----------- | ----------- |----------- |----------- |
|1|It should have UserId [userName: "JeanIrvine"]|Passed||
|1.1|GIVEN The Application Starts|Passed||
|1.2|WHEN The User Access The Url "/recordVisitors/GetUserId/JeanIrvine"|Passed||
|1.3|THEN The Response Should Be Guid|Passed||
