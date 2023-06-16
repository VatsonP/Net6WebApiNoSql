# FlightPlanApi
Based on [Pluralsight] ASP.NET Core 6 Web API- Using NoSQL Databases [2022, ENG]

## Source code
Includes complete source code of FlightPlanApi project (Pluralsight.com - course by Edward Curren)

### Course details
RESTful Web APIs are an ideal solution to many of todays distrubution and data efficiency needs within browser applications. 
In this course, Using ASP.NET Core 6 Web API and NoSQL Databases, you'll learn to how to implement a REST API using C# and ASP.NET 
using MongoDB for the backend database. 
First, you'll explore how to store and retrieve data from a NoSQL database. 
Next, you'll discover the details of how to implement a REST API using C# and ASP.NET. 
Finally, you'll learn how to integrate a web page with the API. When you're finished with this course, 
you'll have the skills and knowledge of the different moving parts needed to design 
and implement a REST API using ASP.NET and a NoSQL database.

### Course content
Course Overview 			1m 13s
Understanding the Basics of APIs 	34m 32s
The Data and Database Setup 		21m 22s
Writing the APIs 			45m 38s
Putting a Front End on It 		14m 47s


Link to course here https://www.pluralsight.com/courses/asp-dot-net-core-6-webapi-using-nosql-databases

To Run and Test projects:


1)  Start MongoDB service (by run "START_mongodb_srv.bat") for access to DB data scheme for FlightPlanApi project.

2) FlightPlanApi

   - Open "FlightPlanApi.sln" with Visual Studio 2022, than click Run FlightPlanApi project.
     Browser will be started and opened on "https://localhost:3001/swagger/index.html" page with Swagger interface.
   
3) FlightPlanUi

   - Open folder "...\FlightPlanUi" with Visual Studio Code, than click on flightplanlist.html file on left files list panel.
     Right mouse click and in context menu click on "Open with Live Server".
     Browser will be started and opened on "http://127.0.0.1:5500/flightplanlist.html" page with static Html API interface.
     Click on "Load Flight Plans" button to get all flight plans from storage.