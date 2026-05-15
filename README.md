# Proj_TestTCC7 — Product Code Manager

WinForms (.NET 9) + ASP.NET Core Web API + SQL Server

## โครงสร้าง

- `ProductCodeApp` — แอป Windows (จัดการรหัสสินค้า, QR, ลบ)
- `ProductCodeApi` — Web API (`/api/Products`)
- `ProductCode.sln`

## ความต้องการ

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server หรือ LocalDB

## ตั้งค่า

1. คัดลอกไฟล์ตัวอย่าง config (ถ้ายังไม่มี):
   - `ProductCodeApi/appsettings.example.json` → `appsettings.Development.json`
   - `ProductCodeApp/appsettings.example.json` → แก้ `appsettings.json` ตามพอร์ต API
2. แก้ connection string ใน `ProductCodeApi/appsettings.Development.json`
3. (ทางเลือก) รัน `ProductCodeApi/Database/CreateProductsTable.sql` บน SQL Server

## รัน

```powershell
# Terminal 1 — API
dotnet run --project ProductCodeApi

# Terminal 2 — WinForms
dotnet run --project ProductCodeApp
```

API เริ่มต้น: `https://localhost:7111` (ดู `Properties/launchSettings.json`)

## รหัสสินค้า

- A–Z, 0–9 เท่านั้น (พิมพ์ใหญ่)
- 30 หลัก (ไม่นับ `-`)
- รูปแบบ: `XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`
