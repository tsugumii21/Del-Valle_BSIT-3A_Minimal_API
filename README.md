# 🛒 Product Management System API

## Lab Activity: Multi-Model ASP.NET Core Web API using Entity Framework Core, SQL Server, and CRUD Operations

**Submitted By:** Allen P. Del Valle  
**Course/Section:** BSIT-3A  
**Subject:** IT Elective 2  

---

## 📋 Table of Contents
1. [Project Overview](#-project-overview)
2. [Features](#-features)
3. [EF Core Concepts Used](#-ef-core-concepts-used)
4. [Project Structure](#-project-structure)
5. [Relationships Diagram](#-relationships-diagram)
6. [API Endpoints](#-api-endpoints)
7. [How to Run](#-how-to-run)
8. [Sample Request/Response](#-sample-requestresponse)
9. [Technologies Used](#️-technologies-used)
10. [Important Notes](#-important-notes)

---

## 🎯 Project Overview

The **Product Management System API** is a robust and scalable ASP.NET Core Web API designed to handle complex relational data. It serves as a backend foundation for an inventory and customer management system, utilizing Entity Framework Core as the Object-Relational Mapper (ORM) to interact with a SQL Server database. The API provides comprehensive CRUD (Create, Read, Update, Delete) operations across multiple interconnected models, including Products, Categories, Suppliers, and an independent Customer model. 

**Key Highlights:**
- ✅ **Multi-Model Architecture:** Manages relational entities (Category & Supplier) alongside independent entities (Customer).
- ✅ **Entity Framework Core Integration:** Utilizes Code-First migration for seamless database schema generation and updates.
- ✅ **Data Validation:** Implements Data Annotations to enforce strict data integrity rules directly at the model level.
- ✅ **Eager Loading:** Uses `.Include()` to retrieve related data efficiently, preventing N+1 query performance issues.
- ✅ **Interactive Documentation:** Fully integrated Swagger UI for visual endpoint testing and API exploration.

---

## ✨ Features

### Category Management
| Endpoint | Method | Description |
| :--- | :---: | :--- |
| `/api/categories` | GET | Retrieves a list of all categories. |
| `/api/categories/{id}` | GET | Retrieves details of a specific category by ID. |
| `/api/categories` | POST | Creates a new category record. |
| `/api/categories/{id}` | PUT | Updates an existing category by ID. |
| `/api/categories/{id}` | DELETE | Deletes a specific category by ID. |

### Supplier Management
| Endpoint | Method | Description |
| :--- | :---: | :--- |
| `/api/suppliers` | GET | Retrieves a list of all suppliers. |
| `/api/suppliers/{id}` | GET | Retrieves details of a specific supplier by ID. |
| `/api/suppliers` | POST | Creates a new supplier record. |
| `/api/suppliers/{id}` | PUT | Updates an existing supplier by ID. |
| `/api/suppliers/{id}` | DELETE | Deletes a specific supplier by ID. |

### Customer Management 
| Endpoint | Method | Description |
| :--- | :---: | :--- |
| `/api/customers` | GET | Retrieves a list of all independent customers. |
| `/api/customers/{id}` | GET | Retrieves details of a specific customer by ID. |
| `/api/customers` | POST | Creates a new customer record. |
| `/api/customers/{id}` | PUT | Updates an existing customer by ID. |
| `/api/customers/{id}` | DELETE | Deletes a specific customer by ID. |

### Product Management
| Endpoint | Method | Description |
| :--- | :---: | :--- |
| `/api/products` | GET | Retrieves all products, including their associated Category and Supplier data. |
| `/api/products/{id}` | GET | Retrieves a specific product by ID, including relational data. |
| `/api/products` | POST | Creates a new product, optionally linking to a Category/Supplier. |
| `/api/products/{id}` | PUT | Updates a product, including its relational Foreign Keys. |
| `/api/products/{id}` | DELETE | Deletes a specific product by ID. |

---

## 🔧 EF Core Concepts Used

### 1. DbContext & DbSet
The `ApplicationDBContext` inherits from `DbContext` and acts as the bridge between the application and the database. `DbSet<T>` represents collections of the specified entities.
```csharp
public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    // ...
}
```

### 2. Relationships (Foreign Keys & Navigation Properties)
Relationships are defined using Foreign Key column definitions and Navigation Properties, further configured explicitly inside `OnModelCreating`.
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>()
        .HasOne(p => p.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryID);
}
```

### 3. Data Annotations
Attributes are applied to model properties to enforce validation rules before data reaches the database.
```csharp
[Required]
public string Name { get; set; }

[Range(1, 1000000)]
public decimal Price { get; set; }

[EmailAddress]
public string Email { get; set; }
```

### 4. Migrations
EF Core Migrations are used to build the database schema directly from the C# model definitions using commands like `dotnet ef migrations add`.

---

## 📁 Project Structure

```text
ProductAPIDemo/
├── 📁 Controllers/
│   ├── 📄 CategoryController.cs
│   ├── 📄 SupplierController.cs
│   ├── 📄 CustomerController.cs
│   └── 📄 ProductController.cs
├── 📁 Data/
│   └── 📄 ApplicationDBContext.cs
├── 📁 Migrations/
│   └── 📄 (auto-generated EF Core files)
├── 📁 Models/
│   ├── 📄 Category.cs
│   ├── 📄 Supplier.cs
│   ├── 📄 Customer.cs
│   └── 📄 Product.cs
├── 📄 appsettings.json
└── 📄 Program.cs
```

---

## 🔄 Relationships Diagram

```text
       [Category]                          [Supplier]
       (One-to-Many)                       (One-to-Many)
            │                                   │
            └───────────> [Product] <───────────┘
            
                        [Customer]
                       (Independent)
```

---

## 📊 API Endpoints

### Category (`/api/categories`)
| Method | Endpoint | Description | Status Code |
| :---: | :--- | :--- | :---: |
| GET | `/api/categories` | Get all categories | 200 OK |
| GET | `/api/categories/{id}` | Get category by ID | 200 OK / 404 Not Found |
| POST | `/api/categories` | Create new category | 201 Created / 400 Bad Request |
| PUT | `/api/categories/{id}` | Update category | 200 OK / 404 Not Found |
| DELETE | `/api/categories/{id}` | Delete category | 200 OK / 404 Not Found |

### Supplier (`/api/suppliers`)
| Method | Endpoint | Description | Status Code |
| :---: | :--- | :--- | :---: |
| GET | `/api/suppliers` | Get all suppliers | 200 OK |
| GET | `/api/suppliers/{id}` | Get supplier by ID | 200 OK / 404 Not Found |
| POST | `/api/suppliers` | Create new supplier | 201 Created / 400 Bad Request |
| PUT | `/api/suppliers/{id}` | Update supplier | 200 OK / 404 Not Found |
| DELETE | `/api/suppliers/{id}` | Delete supplier | 200 OK / 404 Not Found |

### Customer (`/api/customers`)
| Method | Endpoint | Description | Status Code |
| :---: | :--- | :--- | :---: |
| GET | `/api/customers` | Get all customers | 200 OK |
| GET | `/api/customers/{id}` | Get customer by ID | 200 OK / 404 Not Found |
| POST | `/api/customers` | Create new customer | 201 Created / 400 Bad Request |
| PUT | `/api/customers/{id}` | Update customer | 200 OK / 404 Not Found |
| DELETE | `/api/customers/{id}` | Delete customer | 200 OK / 404 Not Found |

### Product (`/api/products`)
| Method | Endpoint | Description | Status Code |
| :---: | :--- | :--- | :---: |
| GET | `/api/products` | Get all products | 200 OK |
| GET | `/api/products/{id}` | Get product by ID | 200 OK / 404 Not Found |
| POST | `/api/products` | Create new product | 201 Created / 400 Bad Request |
| PUT | `/api/products/{id}` | Update product | 200 OK / 404 Not Found |
| DELETE | `/api/products/{id}` | Delete product | 200 OK / 404 Not Found |

---

## 🚀 How to Run

### Prerequisites
- .NET 8.0 SDK (or later)
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code
- Entity Framework Core Tools (`dotnet tool install --global dotnet-ef`)

### Steps to Run

1. **Clone the repository (or open the local folder):**
```bash
git clone https://github.com/tsugumii21/Del-Valle_BSIT-3A_Minimal_API.git
cd Del-Valle_BSIT-3A_Minimal_API/ProductAPIDemo
```

2. **Restore dependencies:**
```bash
dotnet restore
```

3. **Update the Database:** 
Apply the migrations to build the SQL Server schema.
```bash
dotnet ef database update
```

4. **Run the Application:**
```bash
dotnet run
```
5. **Open Swagger UI:** Navigate to `http://localhost:<port>/swagger` in your browser.

---

## 📌 Sample Request/Response

### 1. Create a Category (`POST /api/categories`)
**Request Body (JSON):**
```json
{
  "name": "Electronics",
  "description": "Gadgets, devices, and accessories"
}
```
**Response (201 Created):**
```json
{
  "categoryID": 1,
  "name": "Electronics",
  "description": "Gadgets, devices, and accessories",
  "products": null
}
```

### 2. Create a Product (`POST /api/products`)
**Request Body (JSON):**
```json
{
  "name": "Wireless Mouse",
  "description": "Ergonomic Bluetooth Mouse",
  "price": 29.99,
  "stock": 150,
  "categoryID": 1,
  "supplierID": 1
}
```
**Response (201 Created):**
```json
{
  "productID": 1,
  "name": "Wireless Mouse",
  "description": "Ergonomic Bluetooth Mouse",
  "price": 29.99,
  "stock": 150,
  "categoryID": 1,
  "category": null,
  "supplierID": 1,
  "supplier": null
}
```

---

## 🛠️ Technologies Used

| Technology | Purpose |
| :--- | :--- |
| **C# (.NET)** | Core programming language |
| **ASP.NET Core Web API** | Framework for building HTTP services |
| **Entity Framework Core** | Data access layer (Object-Relational Mapping) |
| **SQL Server** | Relational Database Management System (RDBMS) |
| **Swagger / OpenAPI** | Interactive API documentation and testing |
| **Git & GitHub** | Version control and source code management |
| **Visual Studio 2022** | Primary Integrated Development Environment |

---

## ⚠️ Important Notes

1. **Required Fields:** Ensure all fields decorated with the `[Required]` Data Annotation are included in your POST/PUT payloads to avoid `400 Bad Request` errors.
2. **CORS:** The API is configured to allow Cross-Origin requests (`AllowAll` policy).
3. **Database Connection:** The default connection string targets localdb. Ensure `appsettings.json` points to your active database engine.
4. **Relational Integrity:** When creating a Product with a `CategoryID` or `SupplierID`, those specific ID records must already exist in the database, otherwise SQL Server will throw a Foreign Key constraint error.

---

## 👤 Author
**Allen P. Del Valle**  
Section: BSIT-3A  
Subject: IT Elective 2  

*Last Updated: April 2026*