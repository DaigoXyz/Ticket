# 🎟️ Ticket Entry App System

Aplikasi **Ticket Entry Management System** berbasis **Blazor Server (.NET 8)** + **SQL Server**.
Digunakan untuk mengelola user, ticket kunjungan, dan proses check-in.

---

## 📦 Tech Stack

* .NET 8 (Blazor Server)
* Entity Framework Core
* SQL Server
* Bootstrap 5 + Bootstrap Icons
* Blazored.SessionStorage
* Docker & Docker Compose (opsional)

---

## 📁 Struktur Project (ringkas)

```
Ticket/
│
├── Components/          # Pages & Layout
├── Data/                # DbContext
├── Models/              # Entity Models
├── Repositories/        # Repository Pattern
├── Services/            # Auth, Hash, State
├── wwwroot/             # Static files
├── Program.cs
├── appsettings.json
├── Dockerfile
├── docker-compose.yml
└── README.md
```

---

# 🚀 Setup TANPA Docker (Local / Development)

## 1️⃣ Prerequisites

Pastikan sudah install:

* **.NET SDK 8.0**
* **SQL Server** (LocalDB / SQL Server Express / Full)
* **SQL Server Management Studio (SSMS)**

---

## 2️⃣ Clone Repository

```bash
git clone https://github.com/USERNAME/ticket-entry-app.git
cd ticket-entry-app
```

---

## 3️⃣ Konfigurasi Database

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=YOUR_SQL_SERVER;Initial Catalog=EntryTicket;Integrated Security=True;TrustServerCertificate=True"
}
```

Contoh:

* `Data Source=INDAH`
* `Data Source=(localdb)\\MSSQLLocalDB`

---

## 4️⃣ Apply Migration (Bikin DB & Table)

```bash
dotnet ef database update
```

> Ini akan otomatis:

* bikin database **EntryTicket**
* bikin semua tabel (`Users`, `EntryTickets`, dll)

---

## 5️⃣ Insert User Admin (WAJIB)

### Generate password hash

Masukin password via aplikasi **(lebih disarankan)**
atau pakai kode C# hash yang sama dengan app.

Contoh query:

```sql
INSERT INTO Users
(Username, Name, PasswordHash, Role, IsActive, CreatedAt)
VALUES
(
  'admin',
  'Administrator',
  'PASTE_HASH_DI_SINI',
  'Admin',
  1,
  GETDATE()
);
```

---

## 6️⃣ Run Project

```bash
dotnet run
```

Buka browser:

```
https://localhost:5182
```

Login:

```
username: admin
password: (password yang kamu hash)
```

---

# 🐳 Setup DENGAN Docker (Production / Deploy Ready)

## 1️⃣ Prerequisites

Install:

* **Docker Desktop**
* **Docker Compose**

---

## 2️⃣ Konfigurasi `docker-compose.yml`

Pastikan file ini **TIDAK pakai password asli** sebelum push ke Git:

```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: ticket-sql
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "YourStrong!Password123"
    ports:
      - "1433:1433"
    volumes:
      - sql_data:/var/opt/mssql

  ticketapp:
    build: .
    container_name: ticket-app
    ports:
      - "5182:8080"
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__DefaultConnection: "Server=sqlserver,1433;Database=EntryTicket;User Id=sa;Password=YourStrong!Password123;TrustServerCertificate=True;Encrypt=False;"
    depends_on:
      - sqlserver

volumes:
  sql_data:
```

---

## 3️⃣ Build & Run Container

```bash
docker compose up -d --build
```

Cek:

```bash
docker ps
```

---

## 4️⃣ Apply Migration ke SQL Server Docker

```bash
dotnet ef database update
```

> Jalankan dari **host**, bukan dari dalam container

---

## 5️⃣ Insert Admin User (Docker SQL)

Masuk ke SQL Server container:

```bash
docker exec -it ticket-sql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Password123" -C
```

Lalu:

```sql
USE EntryTicket;
GO

INSERT INTO Users
(Username, Name, PasswordHash, Role, IsActive, CreatedAt)
VALUES
(
  'admin',
  'Administrator',
  'PASTE_HASH_DI_SINI',
  'Admin',
  1,
  GETDATE()
);
GO
```

---

## 6️⃣ Akses Aplikasi

```
http://localhost:5182
```

---

# ✅ Notes

* `docker compose down -v` ➜ **hapus database**
* Kalau mau data awet ➜ **jangan pakai `-v`**
* Semua password **HARUS di-hash**
* Jangan simpan credential asli di repo
