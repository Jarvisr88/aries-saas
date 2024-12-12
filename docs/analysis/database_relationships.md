# Database Relationships Analysis

## 1. Database Scale
- Total Tables: 101 base tables
- Total Views: 18 views
- Total Functions: 16
- Total Stored Procedures: 60
- Total Triggers: 1

## 2. Core Subsystems and Their Relationships

### 2.1 Order Management Subsystem
Primary Tables:
- `tbl_order` (50 columns) - Central order table
- `tbl_orderdetails` (67 columns) - Order line items
- `tbl_deposits` - Order deposits
- `tbl_orderdeposits` - Links deposits to orders

Relationships:
```
tbl_orderdetails -> tbl_order (CustomerID, OrderID)
tbl_orderdeposits -> tbl_order (CustomerID, OrderID)
tbl_orderdetails -> tbl_order (NextOrderID) [Reoccurring orders]
```

### 2.2 Billing Subsystem
Primary Tables:
- `tbl_invoice` (43 columns) - Main invoice table
- `tbl_invoicedetails` (56 columns) - Invoice line items
- `tbl_invoice_transaction` (18 columns) - Payment transactions

Relationships:
```
tbl_invoice -> tbl_order (CustomerID, OrderID)
tbl_invoicedetails -> tbl_invoice (CustomerID, InvoiceID)
tbl_invoice_transaction -> tbl_invoicedetails (CustomerID, InvoiceID, InvoiceDetailsID)
```

### 2.3 Inventory Management Subsystem
Primary Tables:
- `tbl_inventoryitem` (26 columns) - Product catalog
- `tbl_inventory` (14 columns) - Stock levels
- `tbl_inventory_transaction` (19 columns) - Stock movements
- `tbl_serial` (32 columns) - Serialized items
- `tbl_warehouse` (13 columns) - Storage locations

### 2.4 Medical Documentation Subsystem
Primary Tables:
- `tbl_cmnform` (25 columns) - Base CMN form
- 20 specialized CMN form tables
- `tbl_cmnform_details` (14 columns) - Form line items
- `tbl_cmnform_drorder` (3 columns) - Doctor's orders

### 2.5 Customer Management Subsystem
Primary Tables:
- `tbl_customer` (104 columns) - Customer information
- `tbl_customer_insurance` (29 columns) - Insurance policies
- `tbl_customer_notes` (10 columns) - Customer notes

## 3. Subsystem Schema Diagrams

### 3.1 Order Processing Flow
```
[tbl_order] <-- [tbl_orderdetails]
       ^            |
       |            v
[tbl_deposits] <-- [tbl_orderdeposits]
       |
       v
[tbl_invoice] <-- [tbl_invoicedetails] <-- [tbl_invoice_transaction]
```

### 3.2 Inventory Flow
```
[tbl_inventoryitem] --> [tbl_inventory]
         |                     |
         v                     v
    [tbl_serial]    [tbl_inventory_transaction]
         |
         v
[tbl_serial_transaction]
```

### 3.3 Medical Documentation Flow
```
[tbl_cmnform] --> [tbl_cmnform_details]
       |
       v
[Specific CMN Forms] --> [tbl_cmnform_drorder]
```

## 4. Migration Priority List

### High Priority (Critical Business Operations)
1. Customer Management
   - `tbl_customer` (104 columns, complex relationships)
   - `tbl_customer_insurance` (29 columns)

2. Order Processing
   - `tbl_order` (50 columns)
   - `tbl_orderdetails` (67 columns)
   - Related stored procedures (order_process*)

3. Inventory Management
   - `tbl_inventoryitem` (466 rows)
   - `tbl_inventory` (high transaction volume)
   - `tbl_serial` (critical for equipment tracking)

### Medium Priority
1. Billing System
   - `tbl_invoice`
   - `tbl_invoicedetails`
   - `tbl_invoice_transaction`

2. Medical Documentation
   - `tbl_cmnform` and related tables
   - Supporting stored procedures

### Lower Priority
1. Reference Data
   - Product types
   - Manufacturers
   - Shipping methods

2. Reporting Views
   - `view_orderdetails`
   - `view_invoicetransaction_statistics`

### Special Considerations
1. Complex Stored Procedures
   - 60 procedures need careful migration
   - Many handle critical business logic

2. Functions
   - 16 functions, mostly for calculations
   - Need equivalent PostgreSQL implementations

3. Data Volume
   - Largest tables first for testing
   - Consider parallel migration strategies

## 5. Migration Challenges

### 5.1 Complex Relationships
- Multiple foreign key relationships in order/invoice system
- Circular references need careful handling
- Transaction integrity across related tables

### 5.2 Business Logic
- Heavy use of stored procedures
- Complex calculations in functions
- Need to maintain exact business rules

### 5.3 Data Types
- ENUM fields in multiple tables
- DATE/DATETIME handling differences
- Decimal precision in financial calculations

### 5.4 Performance
- Large tables (400+ rows in some)
- Complex views need optimization
- Transaction volume in inventory system
