# PearlLibrary

A comprehensive library management system built with ASP.NET Core Minimal APIs, designed to streamline book borrowing and management processes for both library members and staff.

## 🎯 Use Case

PearlLibrary addresses the needs of modern libraries by providing a digital platform for:

- **Members**: Easy book discovery, reservation, and borrowing without physical queues
- **Staff**: Efficient inventory management, reservation processing, and return handling
- **Libraries**: Automated tracking of books, users, and transactions with real-time dashboards

## ✨ Key Features

### For Library Members
- **Book Discovery**: Advanced search and browse functionality across title, author, ISBN, genre, and more
- **Smart Reservations**: Reserve available books online and pick them up at the library
- **Borrow Tracking**: View active loans, due dates, and borrowing history
- **Personal Dashboard**: Overview of reservations, active borrows, and account activity

### For Library Staff
- **Inventory Management**: Add new books with multiple copy support
- **Reservation Processing**: Approve member reservations and issue books
- **Return Processing**: Handle book returns and update availability
- **Administrative Dashboard**: Monitor inventory levels, pending reservations, active loans, and overdue items

### System Features
- **Secure Authentication**: JWT-based login with role-based access control
- **Real-time Availability**: Automatic tracking of book copies and availability
- **Due Date Management**: Configurable loan periods with overdue tracking
- **RESTful API**: Clean, documented API endpoints for all operations

## 🏗️ Architecture Overview

```
┌─────────────────┐       ┌─────────────────┐
│   Web Client    │       │   Admin Panel   │
│                 │       │                 │
└─────────────────┘       └─────────────────┘
         │                       │                       
         └───────────────────────┼
                                 │
                    ┌─────────────────────┐
                    │   ASP.NET Core      │
                    │   Minimal APIs      │
                    │                     │
                    │  • Authentication   │
                    │  • Authorization    │
                    │  • Business Logic   │
                    └─────────────────────┘
                                 │
                    ┌─────────────────────┐
                    │   Entity Framework  │
                    │   Core (EF Core)    │
                    └─────────────────────┘
                                 │
                    ┌─────────────────────┐
                    │   PostgreSQL        │
                    │   Database          │
                    └─────────────────────┘
```

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL database
- Git

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/u3kkasha/PearlLibrary.git
   cd PearlLibrary
   ```

2. Navigate to backend:
   ```bash
   cd backend
   ```

3. Update database connection in `appsettings.json`

4. Run database migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. Start the application:
   ```bash
   dotnet run
   ```

6. Access the API documentation at `https://localhost:7059/swagger`


## 📚 API Documentation

The API provides comprehensive endpoints for:
- User authentication and authorization
- Book catalog management
- Reservation and borrowing workflows
- Dashboard analytics

See the backend README for detailed architecture information.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.