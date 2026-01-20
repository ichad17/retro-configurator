# Retro Console Store

A modern web application for building and ordering retro game consoles, built with ASP.NET Core and Angular following Domain-Driven Design (DDD) and Clean Architecture principles.

## Architecture

The project follows Clean Architecture with clear separation of concerns:

### Backend Structure

```
src/
├── Domain/                # Enterprise Business Rules
│   ├── Entities/         # Order (Aggregate Root)
│   ├── ValueObjects/     # ConsoleConfig
│   ├── Enums/           # ConsoleType, OrderStatus
│   └── Repositories/    # IOrderRepository interface
│
├── Application/          # Application Business Rules
│   ├── Interfaces/      # IOrderService
│   └── Services/        # OrderService implementation
│
├── Infrastructure/       # External Interfaces
│   ├── Persistence/     # EF Core DbContext
│   ├── Repositories/    # OrderRepository implementation
│   └── Payment/         # Stripe payment integration
│
└── API/                 # Web API Layer
    ├── Controllers/     # OrdersController
    ├── DTOs/           # Data Transfer Objects
    └── Program.cs      # Dependency Injection configuration
```

### Frontend Structure

```
src/frontend/
└── src/app/
    ├── components/      # UI Components
    │   └── configurator/
    ├── services/        # ConfiguratorService
    └── models/         # TypeScript interfaces
```

## Features

### Domain Layer
- **Order Aggregate Root**: Contains business logic and invariants for orders
  - Price calculation based on configuration
  - Order status management (Pending, Processing, Completed, Cancelled)
  - Email validation
  
- **ConsoleConfig Value Object**: Immutable console configuration
  - Console type selection (NES, SNES, Genesis, N64, PlayStation)
  - Number of controllers (1-4)
  - HDMI support option
  - Custom color option with hex validation

### Application Layer
- **OrderService**: Business logic implementation with explicit dependency injection
  - Create orders
  - Retrieve orders by ID or email
  - Complete or cancel orders

### Infrastructure Layer
- **EF Core**: Entity Framework Core with InMemory database for development
- **Stripe Integration**: Payment service integration (placeholder implementation)
- **Repository Pattern**: Data access abstraction

### API Layer
- RESTful API endpoints
- Swagger/OpenAPI documentation
- CORS configuration for frontend integration
- Comprehensive error handling

### Frontend
- **Angular Configurator**: Interactive console configuration form
  - Real-time price calculation
  - Console selection
  - Controller quantity selection
  - HDMI support toggle
  - Custom color picker
  - Order creation with email

## Getting Started

### Prerequisites
- .NET 10 SDK
- Node.js 18+ and npm
- Angular CLI

### Backend Setup

1. Navigate to the root directory:
```bash
cd /home/runner/work/retro-configurator/retro-configurator
```

2. Build the solution:
```bash
dotnet build
```

3. Run the API:
```bash
cd src/API
dotnet run
```

The API will be available at `https://localhost:5001`

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd src/frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The Angular app will be available at `http://localhost:4200`

## API Endpoints

- `POST /api/orders` - Create a new order
- `GET /api/orders/{id}` - Get order by ID
- `GET /api/orders` - Get all orders
- `GET /api/orders/customer/{email}` - Get orders by customer email
- `POST /api/orders/{id}/complete` - Mark order as completed
- `POST /api/orders/{id}/cancel` - Cancel an order

## Technologies Used

### Backend
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- Stripe.net
- Swashbuckle (Swagger)

### Frontend
- Angular 19
- TypeScript
- RxJS
- Angular HttpClient

## Design Patterns

- **Domain-Driven Design (DDD)**
  - Aggregate Roots
  - Value Objects
  - Repository Pattern
  
- **Clean Architecture**
  - Dependency Inversion
  - Separation of Concerns
  - Domain Independence
  
- **SOLID Principles**
  - Single Responsibility
  - Open/Closed
  - Dependency Injection

## License

This project is licensed under the MIT License.
