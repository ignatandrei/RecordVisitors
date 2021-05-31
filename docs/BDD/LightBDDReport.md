
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
|2|WHEN The User Access The Url "/recordVisitors/UserHistory/9c80d279-797c-49bc-830e-adbde71cbf11/1970-04-16/2021-06-01"|Passed||
|3|THEN The Response Should Be Something|Passed|the response is:[{"additionalData":null,"id":"29938d58-e59f-4cb1-ad24-875cb5249454","url":"/recordVisitors/AllVisitors5Min","userRecordedId":"9c80d279-797c-49bc-830e-adbde71cbf11","dateRecorded":"2021-05-31T17:09:34.8201306Z"},{"additionalData":null,"id":"92612ac2-6311-4fa7-a97e-f58a3901f2e5","url":"/recordVisitors/GetUserId/JeanIrvine","userRecordedId":"9c80d279-797c-49bc-830e-adbde71cbf11","dateRecorded":"2021-05-31T17:09:34.950792Z"},{"additionalData":null,"id":"4b6bf3ed-0c2a-4c03-b27c-4bb39654d473","url":"/recordVisitors/UserHistory/9c80d279-797c-49bc-830e-adbde71cbf11/1970-04-16/2021-06-01","userRecordedId":"9c80d279-797c-49bc-830e-adbde71cbf11","dateRecorded":"2021-05-31T17:09:34.9667858Z"}]|

### TestUserEndpoint

| Number| Name|Status|Comments|
| ----------- | ----------- |----------- |----------- |
|1|It should have UserId [userName: "JeanIrvine"]|Passed||
|1.1|GIVEN The Application Starts|Passed||
|1.2|WHEN The User Access The Url "/recordVisitors/GetUserId/JeanIrvine"|Passed||
|1.3|THEN The Response Should Be Guid|Passed||
