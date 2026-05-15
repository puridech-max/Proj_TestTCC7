-- ตารางเก็บรหัสสินค้า (PLU) สำหรับ SQL Server
-- รันครั้งเดียวเมื่อยังไม่มีตาราง (ไม่ลบข้อมูลเดิม)

IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = N'Products' AND s.name = N'dbo'
)
BEGIN
    CREATE TABLE dbo.Products
    (
        Id      INT           IDENTITY (1, 1) NOT NULL,
        PluCode NVARCHAR(35) NOT NULL,
        CONSTRAINT PK_Products PRIMARY KEY CLUSTERED (Id ASC),
        CONSTRAINT UQ_Products_PluCode UNIQUE (PluCode)
    );
END
GO
