### Documentation for Online Bookstore Backend API

#### Overview

This project is a backend system for an online bookstore. It provides endpoints to manage books, handle user authentication, manage shopping carts, simulate a checkout process, and track purchase history. The backend is built with C# .NET and uses Dapper with PostgreSQL for data access. This documentation will guide you through setting up, running, and understanding the codebase.

#### Table of Contents

1. [Prerequisites](#prerequisites)
2. [Project Structure](#project-structure)
3. [Setting Up the Development Environment](#setting-up-the-development-environment)
4. [Configuration](#configuration)
5. [Database Setup](#database-setup)
6. [Running the Application](#running-the-application)
7. [API Endpoints](#api-endpoints)
8. [Unit Testing](#unit-testing)
9. [High-Level Design](#high-level-design)
10. [Troubleshooting](#troubleshooting)

#### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Dapper](https://github.com/DapperLib/Dapper)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- Git

#### Project Structure

```
OnlineBookstore/
├── Controllers/
│   ├── AuthController.cs
│   ├── BooksController.cs
│   ├── CartController.cs
│   ├── CheckoutController.cs
│   ├── PurchaseHistoryController.cs
│   └── SearchController.cs
├── Models/
│   ├── Book.cs
│   ├── CartItem.cs
│   ├── CheckoutRequest.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── User.cs
|   ├── DatabaseContext.cs
│   └── Enums/
│       └── PaymentMethod.cs
├── Repositories/
│   ├── BookRepository.cs
│   ├── CartRepository.cs
│   ├── OrderRepository.cs
│   ├── UserRepository.cs
│   ├── IBookRepository.cs
│   ├── ICartRepository.cs
│   ├── IOrderRepository.cs
│   └── IUserRepository.cs
├── Services/
│   ├── TokenService.cs
│   ├── IPaymentService.cs
│   └── PaymentService.cs
├── UnitTests/
│   ├── UserRepositoryTests.cs
├── appsettings.json
├── OnlineBookstore.csproj
├── Program.cs
└── Startup.cs
```

#### Setting Up the Development Environment

1. **Clone the Repository**

    ```sh
    git clone <repository-url>
    cd OnlineBookstore
    ```

2. **Install Dependencies**

    Ensure you have .NET 7.0 SDK and PostgreSQL installed. You can check your .NET installation with:

    ```sh
    dotnet --version
    ```

3. **Install Dapper**

    Add Dapper to your project via NuGet:

    ```sh
    dotnet add package Dapper
    dotnet add package Npgsql
    ```

#### Configuration

1. **Configure Database Connection**

    Update `appsettings.json` with your PostgreSQL connection string:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=OnlineBookstore;Username=yourusername;Password=yourpassword"
      },
      "Jwt": {
        "Key": "YourSecretKey",
        "Issuer": "YourIssuer"
        "Audience": "your_audience"
      }
    }
    ```

2. **JWT Configuration**

    Ensure your `appsettings.json` contains the correct JWT configuration:

    ```json
    {
      "Jwt": {
        "Key": "YourSecretKey",
        "Issuer": "YourIssuer"
        "Audience": "your_audience"
      }
    }
    ```

#### Database Setup

1. **Create Database**

    Create a PostgreSQL database for the project:

    ```sh
    createdb OnlineBookstore
    ```

2. **Run Migrations**

    Initialize the database schema. For this example, use manual SQL scripts or Entity Framework migrations if you prefer.

#### Running the Application

1. **Build and Run the Application**

    Navigate to the project directory and run:

    ```sh
    dotnet build
    dotnet run
    ```

    The application should now be running at `[https://localhost:7170]`.

#### API Endpoints

1. **User Authentication**

    - **Register**: `POST /api/Auth/register`
    - **Login**: `POST /api/Auth/login`

2. **Books**

    - **Create Book**: `POST /api/Books`
    - **Get All Books**: `GET /api/Books`
    - **Get Book by ID**: `GET /api/Books/{id}`
    - **Update Book**: `PUT /api/Books/{id}`
    - **Delete Book**: `DELETE /api/Books/{id}`

3. **Cart**

    - **Add to Cart**: `POST /api/Cart`
    - **Update Cart**: `PUT /api/Cart`
    - **Get Cart Items**: `GET /api/Cart/{userId}`
    - **Remove from Cart**: `DELETE /api/Cart/{userId}/{bookId}`
    - **Clear Cart**: `POST /api/Cart/{userId}/{clear}`

4. **Checkout**

    - **Checkout**: `POST /api/Checkout`

5. **SearchBooks**

    - **Search Books**: `GET /api/Search`

6. **PurchaseHistory**

    - **PurchaseHistory**: `GET /api/PurchaseHistory/{userId}`
    - **PurchaseHistory**: `GET /api/PurchaseHistory/{userId}/{orderId}`

#### Unit Testing

1. **Add Unit Tests**

    Write unit tests in the `UnitTests` directory using xUnit or NUnit.

2. **Run Unit Tests**

    Run tests with:

    ```sh
    dotnet test
    ```

#### High-Level Design

The backend system is designed using a layered architecture:

- **Controllers**: Handle HTTP requests and responses.
- **Services**: Contain business logic.
- **Repositories**: Handle data access using Dapper.
- **Models**: Define data structures.
- **Unit Tests**: Ensure code functionality.


#### Troubleshooting

1. **Common Issues**

    - **Database Connection**: Ensure PostgreSQL is running and the connection string is correct.
    - **JWT Errors**: Verify the JWT key and issuer in `appsettings.json`.
    - **Dependencies**: Ensure all necessary NuGet packages are installed.

2. **Logs**

    Check application logs for detailed error messages.
