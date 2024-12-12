# Database Migration Plan

## 1. Schema Diagrams by Subsystem

### 1.1 Core Patient and Order Management
```mermaid
erDiagram
    tbl_customer ||--o{ tbl_customer_insurance : has
    tbl_customer ||--o{ tbl_customer_notes : has
    tbl_customer ||--o{ tbl_order : places
    tbl_order ||--|{ tbl_orderdetails : contains
    tbl_order ||--o{ tbl_invoice : generates
    tbl_orderdetails ||--o{ tbl_serial : uses
    tbl_orderdetails ||--o{ tbl_inventoryitem : references
    tbl_customer_insurance ||--o{ tbl_invoice : bills

    tbl_customer {
        int ID PK
        string FirstName
        string LastName
        datetime DOB
        int CustomerTypeID FK
        int InsuranceCompanyID FK
    }

    tbl_order {
        int ID PK
        int CustomerID FK
        datetime OrderDate
        datetime DeliveryDate
        boolean Approved
        decimal TotalAmount
    }

    tbl_orderdetails {
        int ID PK
        int OrderID FK
        int InventoryItemID FK
        int SerialID FK
        decimal Price
        int Quantity
    }
```

### 1.2 Inventory and Equipment Management
```mermaid
erDiagram
    tbl_inventoryitem ||--|{ tbl_inventory : stocks
    tbl_inventoryitem ||--o{ tbl_serial : tracks
    tbl_inventory ||--|{ tbl_inventory_transaction : records
    tbl_serial ||--|{ tbl_serial_transaction : logs
    tbl_serial ||--o{ tbl_serial_maintenance : maintains
    tbl_warehouse ||--|{ tbl_inventory : stores

    tbl_inventoryitem {
        int ID PK
        string Name
        string ModelNumber
        boolean Serialized
        int ManufacturerID FK
        decimal PurchasePrice
    }

    tbl_inventory {
        int ID PK
        int InventoryItemID FK
        int WarehouseID FK
        int OnHand
        decimal CostPerUnit
    }

    tbl_serial {
        int ID PK
        int InventoryItemID FK
        string SerialNumber
        string Status
        int CurrentCustomerID FK
    }
```

### 1.3 Billing and Insurance
```mermaid
erDiagram
    tbl_invoice ||--|{ tbl_invoicedetails : contains
    tbl_invoicedetails ||--|{ tbl_invoice_transaction : processes
    tbl_invoice ||--|| tbl_order : references
    tbl_customer_insurance ||--o{ tbl_invoice : covers
    tbl_pricecode ||--|{ tbl_pricecode_item : defines

    tbl_invoice {
        int ID PK
        int CustomerID FK
        int OrderID FK
        datetime InvoiceDate
        decimal TotalAmount
        int CustomerInsurance1_ID FK
    }

    tbl_invoicedetails {
        int ID PK
        int InvoiceID FK
        decimal Amount
        decimal PaidAmount
        string Status
    }

    tbl_invoice_transaction {
        int ID PK
        int InvoiceID FK
        int InvoiceDetailsID FK
        decimal Amount
        datetime TransactionDate
    }
```

### 1.4 Medical Documentation
```mermaid
erDiagram
    tbl_cmnform ||--|{ tbl_cmnform_details : contains
    tbl_cmnform ||--|| tbl_order : documents
    tbl_cmnform ||--o{ tbl_cmnform_drorder : requires
    tbl_cmnform_details ||--o{ tbl_inventoryitem : references

    tbl_cmnform {
        int ID PK
        int OrderID FK
        datetime StartDate
        datetime EndDate
        string Status
    }

    tbl_cmnform_details {
        int ID PK
        int CMNFormID FK
        int InventoryItemID FK
        string Answers
    }
```

## 2. Migration Priority List

### 2.1 Priority 1 - Core Customer and Order Tables
High complexity, critical business operations, most relationships

| Table Name | Columns | Rows | Complexity | Dependencies | Priority |
|------------|---------|------|------------|--------------|----------|
| tbl_customer | 104 | 49 | High | Primary | 1.1 |
| tbl_order | 50 | 346 | High | Primary | 1.2 |
| tbl_orderdetails | 67 | 433 | High | Primary | 1.3 |
| tbl_customer_insurance | 29 | 74 | Medium | Primary | 1.4 |

### 2.2 Priority 2 - Inventory Management
High transaction volume, critical for operations

| Table Name | Columns | Rows | Complexity | Dependencies | Priority |
|------------|---------|------|------------|--------------|----------|
| tbl_inventoryitem | 26 | 466 | High | Primary | 2.1 |
| tbl_inventory | 14 | 49 | Medium | Secondary | 2.2 |
| tbl_serial | 32 | 41 | High | Secondary | 2.3 |
| tbl_inventory_transaction | 19 | 342 | High | Secondary | 2.4 |

### 2.3 Priority 3 - Billing and Payments
Financial data, complex calculations

| Table Name | Columns | Rows | Complexity | Dependencies | Priority |
|------------|---------|------|------------|--------------|----------|
| tbl_invoice | 43 | 330 | High | Secondary | 3.1 |
| tbl_invoicedetails | 56 | 364 | High | Secondary | 3.2 |
| tbl_invoice_transaction | 18 | 614 | Medium | Secondary | 3.3 |
| tbl_pricecode_item | 39 | 513 | Medium | Secondary | 3.4 |

### 2.4 Priority 4 - Medical Documentation
Complex forms, regulatory compliance

| Table Name | Columns | Rows | Complexity | Dependencies | Priority |
|------------|---------|------|------------|--------------|----------|
| tbl_cmnform | 25 | 82 | High | Tertiary | 4.1 |
| tbl_cmnform_details | 14 | 74 | Medium | Tertiary | 4.2 |
| Multiple CMN form tables | varies | varies | Medium | Tertiary | 4.3 |

### 2.5 Priority 5 - Supporting Tables
Reference data, lower complexity

| Table Name | Columns | Rows | Complexity | Dependencies | Priority |
|------------|---------|------|------------|--------------|----------|
| tbl_warehouse | 13 | 2 | Low | Support | 5.1 |
| tbl_manufacturer | 14 | 2 | Low | Support | 5.2 |
| tbl_producttype | 4 | 18 | Low | Support | 5.3 |

## 3. Migration Phases

### Phase 1: Foundation (Weeks 1-2)
- Migrate core customer and order tables
- Set up basic PostgreSQL schema
- Implement primary keys and indexes
- Basic data validation

### Phase 2: Inventory (Weeks 3-4)
- Migrate inventory management system
- Implement serial number tracking
- Set up transaction logging
- Test inventory movements

### Phase 3: Financial (Weeks 5-6)
- Migrate billing system
- Implement payment processing
- Set up invoice generation
- Test financial calculations

### Phase 4: Medical Records (Weeks 7-8)
- Migrate CMN forms
- Implement form validation
- Set up medical documentation
- Test compliance requirements

### Phase 5: Support Systems (Weeks 9-10)
- Migrate reference tables
- Implement views
- Set up reporting
- Final testing and validation

## 4. Special Considerations

### 4.1 Data Volume Handling
- Tables over 400 rows require batch processing
- Consider parallel migration for large tables
- Implement validation checkpoints

### 4.2 Business Logic Migration
- 60 stored procedures need conversion
- 16 functions require PostgreSQL equivalents
- Complex calculations need thorough testing

### 4.3 Data Integrity
- Maintain referential integrity during migration
- Validate all foreign key relationships
- Ensure transaction consistency

### 4.4 Performance Optimization
- Index strategy for PostgreSQL
- Query optimization for complex views
- Connection pooling setup
