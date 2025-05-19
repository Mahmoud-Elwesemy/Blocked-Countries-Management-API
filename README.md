Blocked Countries Management API
A .NET Core Web API for managing blocked countries and validating IP addresses using third-party geolocation services. The application uses in-memory storage for data management and does not require a database.

Features
Block/Unblock Countries: Add or remove countries from the blocked list.

Temporal Blocking: Block countries for a specific duration (auto-removed after expiration).

IP Lookup: Fetch country details (code, name, ISP) using external geolocation APIs.

IP Block Check: Automatically detect the caller's IP and verify if their country is blocked.

Logging: Track blocked attempts with details like IP, timestamp, and user agent.

Pagination & Filtering: Retrieve paginated lists of blocked countries or logs with search/filter support.

Built With
.NET Core 7/8/9 - Backend framework.

Concurrent Collections (ConcurrentDictionary, ConcurrentBag) - Thread-safe in-memory storage.

HttpClient - Integration with geolocation APIs (e.g., ipapi.co).

Swagger - API documentation.

Background Services - Automated cleanup of expired temporal blocks.

API Endpoints
Endpoint	Method	Description
/api/countries/block	POST	Block a country by its code (e.g., "US").
/api/countries/block/{code}	DELETE	Unblock a country.
/api/countries/blocked	GET	Get all blocked countries (paginated).
/api/countries/temporal-block	POST	Temporarily block a country.
/api/ip/lookup	GET	Get country details by IP address.
/api/ip/check-block	GET	Check if the caller's IP is blocked.
/api/logs/blocked-attempts	GET	Get logs of blocked attempts (paginated).

Architecture
In-Memory Storage: Uses ConcurrentDictionary and ConcurrentBag for thread-safe operations.

Specification Pattern: Dynamic querying with pagination and filtering.

Background Service: Automatically removes expired temporal blocks every 5 minutes.

Dependency Injection: Clean separation of concerns between services, repositories, and controllers.

Contact
For questions or feedback, reach out to:

Your Name - weso2020@icloud.com

GitHub Profile - https://github.com/Mahmoud-Elwesemy
