MedReminder
MedReminder is a medication management system that enables users to track their medications and schedules. 
The application uses secure authentication and authorization.

Features
User authentication with JWT and refresh tokens.
Secure login/logout functionality with token storage in cookies.
CRUD operations for medications.
Daily medication schedule tracking.
REST endpoints.

Technology Stack
Backend: ASP.NET Core + REST API
Authentication using JWT with refresh token support.
Frontend: Razor Views (MVC)
Database: SQL Server
Client Library: RestSharp consuming the MedReminder API.

Architecture
MedReminder.API:
- Contains the REST API for user authentication, medication management, and scheduling.
- Authentication using JWT and refresh tokens.
- Controllers like AuthController, UserController, MedicationController.
MedReminder.Web:
- ASP.NET MVC application.
- Interacts with the API using the ApiClient library.
- Manages user sessions with secure cookies.
MedReminder.Shared:
- DTOs for data transfer between API and Web.
MedReminder.ApiClient:
- Handles communication between the web app and the API using RestSharp.
