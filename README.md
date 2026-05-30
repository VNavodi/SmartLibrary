# 📚 SmartLibrary

SmartLibrary is a state-of-the-art, premium Library Management System built using a robust **ASP.NET Core 8 REST API** backend and an elegant, modern **Blazor WebAssembly UI** frontend. 

It empowers librarians and administrators to manage book catalogs, author indices, categorizations, library memberships, and real-time book circulation workflows (loans, returns, and overdues) with absolute ease and style.

---

## ✨ Features

### 💻 Modern & Elegant UI (Blazor Frontend)
- **Stunning Aesthetics:** Harmonious curated color palettes, elegant cards, full responsiveness, and smooth micro-animations.
- **Books Catalog:** Search by Title/ISBN/Author, filter by availability, add/edit/delete books with clean, validated forms.
- **Borrow Records & Circulation:** Track loans, returns, and overdues in real-time. Shows actual member names, book titles, and intuitive visual badges for loan status (*Active*, *Overdue*, *Returned*).
- **Authors & Categories:** Easily manage your library's authors and genres.

### ⚙️ Robust RESTful Architecture (ASP.NET Core Backend)
- **Dual Endpoints:** Mixes standard REST Controllers (for complex transactions like borrow records) and super-fast Minimal APIs (for authors and categories).
- **Data Persistence:** Integrated Entity Framework Core with SQL Server/LocalDB support.
- **Automatic Demo Seeder:** Automatically cleans and populates the database on startup with a rich, balanced demo dataset (7 Authors, 6 Categories, 9 Books, 5 Members, and 3 active/overdue/returned loans) for immediate demonstration capabilities.
- **Swagger/OpenAPI Support:** Interactive API playground available out-of-the-box.

---

## 🛠️ Technology Stack

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Backend API** | ASP.NET Core 8.0 | Web API with Controllers & Minimal APIs |
| **ORM** | Entity Framework Core | Database migrations & relationship mapping |
| **Database** | SQL Server / LocalDB | Enterprise-ready relational database |
| **Frontend UI** | Blazor WebAssembly | C# in the browser for high performance |
| **Styling** | Vanilla CSS3 | Sleek, custom-tailored layout with glassmorphism |
| **Documentation** | Swagger / OpenAPI | Auto-generated interactive API docs |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or higher
- [SQL Server Express / LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

### Setup & Run

1. **Clone the Repository:**
   ```bash
   git clone <repository-url>
   cd SmartLibrary
   ```

2. **Configure the Connection String:**
   Open [SmartLibrary.API/appsettings.json](file:///c:/Users/videesha/Desktop/.NET/SmartLibrary/SmartLibrary.API/appsettings.json) and ensure the `DefaultConnection` string matches your local SQL Server instance (the default points to LocalDB).

3. **Restore & Build the Solution:**
   ```bash
   dotnet build
   ```

4. **Run the Backend API:**
   ```bash
   cd SmartLibrary.API
   dotnet run
   ```
   *The Swagger interactive documentation page will open automatically at `http://localhost:<port>/swagger` in Development mode.*

5. **Run the Blazor Frontend:**
   ```bash
   cd ../SmartLibrary.UI
   dotnet run
   ```
   *Open your browser and navigate to the printed localhost address (typically `https://localhost:7193` or `http://localhost:5193`) to view the application.*

---

## 📊 Database Schema Relationships

The database context models follow clean relational best practices:
- **Book ⟷ Author:** Many-to-One
- **Book ⟷ Category:** Many-to-One
- **BorrowRecord ⟷ Member:** Many-to-One
- **BorrowRecord ⟷ Book:** Many-to-One

---

## 🧑‍💻 Seeding for Demos

No manual database setup is required. Upon first startup, the database is automatically created, checked, and seeded with:
* **7 Authors** (e.g. J.K. Rowling, Stephen King, Agatha Christie)
* **6 Genres** (e.g. Fiction, Mystery, Science, Biography)
* **9 Books** with balanced stock and availability
* **5 Members** with complete details
* **3 Borrow Records** demonstrating active, overdue, and successfully returned loan states.
