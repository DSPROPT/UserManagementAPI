# UserManagementAPI

A simple ASP.NET Core Web API that manages users with CRUD operations, authentication, error handling, and logging middleware. This project demonstrates how to build and enhance a back-end API using C#, .NET, and token-based security.

## Overview

This API was created for TechHive Solutions to handle basic user management tasks:

- **CRUD Operations:** Create, read, update, and delete user records.
- **Authentication:** Token-based authentication using a Bearer token (for demonstration purposes).
- **Logging:** Logs incoming requests and outgoing responses.
- **Error Handling:** Middleware returns structured JSON responses on exceptions.

## Features

### User Management
- **Create New User:**  
    `POST /api/users`  
    Example:
    ```json
    {
        "fullName": "Jane Doe",
        "email": "jane@domain.com"
    }
    ```

- **Retrieve Users:**  
    - `GET /api/users` — Retrieve all users.
    - `GET /api/users/{id}` — Retrieve a specific user by ID.

- **Update User:**  
    `PUT /api/users/{id}`  
    Example:
    ```json
    {
        "fullName": "New Name",
        "email": "new@domain.com"
    }
    ```

- **Delete User:**  
    `DELETE /api/users/{id}`

### Authentication Middleware
- **Login Endpoint:**  
    `POST /api/auth/login` — Returns a token if credentials are valid.
    ```json
    {
        "username": "admin",
        "password": "password123"
    }
    ```
    - Response:
        ```json
        {
            "token": "mysecrettoken"
        }
        ```
- All other endpoints require a valid Bearer token in the header:
    ```
    Authorization: Bearer mysecrettoken
    ```

### Error-Handling Middleware
- Catches unhandled exceptions and returns a JSON error response with status code 500.

### Logging Middleware
- Logs key details such as HTTP method, request path, and response status code for every request.

## Technologies Used

- .NET 6+ (ASP.NET Core)
- C#
- In-Memory Data (for simplicity; no external database)
- Insomnia or Postman for API testing
- Visual Studio / VS Code (recommended for development)

## Getting Started

### Prerequisites
- .NET 6 SDK (or later) installed.
- A code editor or IDE such as Visual Studio, Rider, or VS Code.

### Installation

1. **Clone the repository:**
     ```bash
     git clone https://github.com/your-username/UserManagementAPI.git
     cd UserManagementAPI
     ```

2. **Restore and build the project:**
     ```bash
     dotnet restore
     dotnet build
     ```

3. **Run the application:**
     ```bash
     dotnet run
     ```
     By default, the API will run on [http://localhost:5000](http://localhost:5000) and [https://localhost:7000](https://localhost:7000) (or similar ports).

## API Usage

### 1. Log In to Get a Token
- **Endpoint:** `POST /api/auth/login`
- **Example Request:**
    ```json
    {
        "username": "admin",
        "password": "password123"
    }
    ```
- **Example Response:**
    ```json
    {
        "token": "mysecrettoken"
    }
    ```

### 2. Use the Token for Authentication
Include the token in the Authorization header for subsequent requests:
```
Authorization: Bearer mysecrettoken
```

### 3. User Endpoints

| Endpoint                   | Description                                     | Example Body / Response                                                                          |
|----------------------------|-------------------------------------------------|--------------------------------------------------------------------------------------------------|
| `GET /api/users`           | Retrieve all users                              | Returns a list of all users                                                                      |
| `GET /api/users/{id}`      | Retrieve a specific user by ID                  | Returns the user object or 404 if not found                                                      |
| `POST /api/users`          | Create a new user                              | `{ "fullName": "Jane Doe", "email": "jane@domain.com" }`                                         |
| `PUT /api/users/{id}`      | Update an existing user’s data                 | `{ "fullName": "New Name", "email": "new@domain.com" }`                                            |
| `DELETE /api/users/{id}`   | Delete a user by ID                             | Returns 204 No Content or 404 if not found                                                       |

#### Example POST Request (Creating a User)
```bash
POST /api/users
Authorization: Bearer mysecrettoken
Content-Type: application/json

{
    "fullName": "Jane Doe",
    "email": "jane@domain.com"
}
```

#### Example GET Request (Fetching All Users)
```bash
GET /api/users
Authorization: Bearer mysecrettoken
```
**Response:**
```json
[
    {
        "id": 1,
        "fullName": "Jane Doe",
        "email": "jane@domain.com",
        "createdAt": "2025-04-05T12:00:00Z"
    }
]
```

## Middleware Details

- **ErrorHandlingMiddleware:**  
    Wraps requests in a try-catch block and returns a structured JSON error response with a 500 status code upon unhandled exceptions.

- **AuthenticationMiddleware:**  
    Validates the `Authorization: Bearer <token>` header. If the token (`mysecrettoken`) is valid, the request proceeds. Otherwise, returns a 401 Unauthorized status.

- **LoggingMiddleware:**  
    Logs HTTP method, request path, and response status code for every request.

## Testing with Insomnia or Postman

1. **Login:**
     - Send a `POST /api/auth/login` request with valid JSON credentials.
     - Copy the token from the response.

2. **Make Authenticated Calls:**
     - Include `Authorization: Bearer mysecrettoken` in the request header.
     - Call any `/api/users` endpoint to confirm success.

3. **Unauthorized Tests:**
     - Omit or provide an incorrect token and expect a 401 Unauthorized response.

4. **Edge Cases:**
     - Requesting a non-existent user ID should return 404.
     - Causing an unhandled exception in a controller should return a 500 error with a JSON response.

## Contributing

Feel free to fork this repository and send a pull request with improvements such as:
- Real database integration (using EF Core)
- JWT-based authentication and role-based access control
- Enhanced input validation
- Pagination or advanced searching for users

## License

This project is open source under the [MIT License](LICENSE). You’re free to use, modify, and distribute it as you see fit.