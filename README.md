# FlightPlanApi
Based on [Pluralsight] ASP.NET Core 6 Web API- Using NoSQL Databases [2022, ENG]

## Source code
Includes complete source code of FlightPlanApi project (Pluralsight.com - course by Edward Curren)

### Course content
Course Overview 			
Understanding the Basics of APIs 	
The Data and Database Setup 		
Writing the APIs 			
Putting a Front End on It 		


Link to course here https://www.pluralsight.com/courses/asp-dot-net-core-6-webapi-using-nosql-databases

To Run and Test projects:


1)  Start MongoDB service (by run "START_mongodb_srv.bat") for access to DB data for FlightPlanApi project.

2) FlightPlanApi

   - Open "FlightPlanApi.sln" with Visual Studio 2022, than click Run FlightPlanApi project.
     Browser will be started and opened on "https://localhost:3001/swagger/index.html" page with Swagger interface.
   
3) FlightPlanUi

   - Open folder "...\FlightPlanUi" with Visual Studio Code, than click on flightplanlist.html file on left files list panel.
     Right mouse click and in context menu click on "Open with Live Server".
     Browser will be started and opened on "http://127.0.0.1:5500/flightplanlist.html" page with static Html API interface.
     Click on "Load Flight Plans" button to get all flight plans from storage.