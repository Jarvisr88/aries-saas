# Database and Data Access Layer Analysis

## 1. Data Access Architecture

### 1.1 Implementation Pattern
- Uses Data Adapter pattern for database operations
- Custom exception handling for common database scenarios:
  - DeadlockException
  - DuplicateKeyException
  - ObjectIsModifiedException
  - ObjectIsNotFoundException
  - ValidationException
- Centralized query management (Queries.cs)
- Type handlers for custom data type conversions

### 1.2 Code Organization
```
DMEWorks.Data/
├── Adapters/          # Data access adapters
├── Common/            # Shared utilities
├── Entities/          # Data models
│   └── Address.cs     # Address entity
├── TypeHandlers/      # Custom type conversions
└── Queries.cs         # SQL queries
```

### 1.3 Error Handling
Specialized exceptions for different scenarios:
- Deadlock detection and handling
- Duplicate key violations
- Optimistic concurrency conflicts
- Record not found scenarios
- Data validation errors

## 2. Data Access Patterns

### 2.1 Key Features
1. Separation of Concerns:
   - Entities for data models
   - Adapters for data access
   - Type handlers for data conversion
   - Centralized query management

2. Error Management:
   - Specialized exceptions
   - Validation handling
   - Concurrency control

3. Data Integrity:
   - Duplicate detection
   - Deadlock handling
   - Object modification tracking

## 3. Migration Considerations

### 3.1 Django Implementation
1. Model Mapping:
   - Convert entities to Django models
   - Implement validation using Django's validation system
   - Use Django's built-in field types

2. Error Handling:
   - Map custom exceptions to Django exceptions
   - Implement custom exception middleware
   - Use Django's transaction management

3. Data Access:
   - Replace adapters with Django ORM
   - Convert SQL queries to Django ORM queries
   - Implement custom model managers where needed

### 3.2 Data Migration
1. Schema Migration:
   - Create Django migrations
   - Handle data type conversions
   - Preserve relationships and constraints

2. Data Transfer:
   - Plan incremental data migration
   - Validate data integrity
   - Handle legacy data formats

### 3.3 Performance Considerations
1. Indexing Strategy:
   - Review current indexes
   - Plan Django model indexes
   - Optimize for common queries

2. Query Optimization:
   - Analyze complex queries
   - Plan for Django ORM optimization
   - Consider raw SQL for complex operations

## 4. Database Schema Analysis

### Overview
The database schema represents a comprehensive Home Medical Equipment (HME) and Durable Medical Equipment (DME) management system. The schema contains 119 tables that handle various aspects of medical equipment management, billing, customer data, and medical documentation.

### Core Tables and Their Relationships

#### Customer and Order Management
- `tbl_customer`: (Inferred) Stores customer information
- `tbl_order`: (Inferred) Main order table
- `tbl_orderdetails`: Detailed order information including billing codes, quantities, and pricing
- `view_orderdetails`: Comprehensive view of order details with additional calculated fields
- `tbl_warehouse`: Manages warehouse locations and inventory storage

#### Medical Documentation
- `tbl_cmnform`: Certificate of Medical Necessity (CMN) forms with various types
- Multiple CMN-related tables (e.g., `tbl_cmnform_0102a`, `tbl_cmnform_0102b`, etc.): Store specific answers for different CMN form types
- `tbl_cmnform_details`: Links CMN forms with inventory items and billing codes
- `tbl_cmnform_drorder`: Doctor's orders associated with CMN forms

#### Billing and Insurance
- `tbl_batchpayment`: Tracks insurance company payments
- `tbl_billingtype`: Defines different billing types
- `tbl_insurancecompany`: (Inferred) Insurance company information
- `tbl_submitter`: Electronic claim submission configuration
- `view_billinglist`: Manages billing cycles and flags
- `view_invoicetransaction_statistics`: Comprehensive billing and payment statistics

#### Inventory Management
- `tbl_inventoryitem`: (Inferred) Product catalog
- `tbl_vendor`: Supplier information
- `tbl_warehouse`: Physical storage locations

#### User Management
- `tbl_user`: User authentication and basic info
- `tbl_user_location`: Links users to locations (multi-location support)
- `tbl_user_notifications`: User notification system

### Key Design Patterns

1. **Audit Trail**
   - Most tables include `LastUpdateUserID` and `LastUpdateDatetime` fields
   - `tbl_changes` tracks table modifications with session tracking

2. **Multi-tenant Support**
   - Location-based access control through `tbl_user_location`
   - Warehouse management for multiple locations

3. **Billing Flexibility**
   - Multiple billing types and payment tracking
   - Support for various insurance companies and payment methods
   - Comprehensive pricing and tax handling

4. **Medical Documentation**
   - Extensive support for different CMN form types
   - Structured storage of medical necessity documentation
   - Support for both ICD-9 and ICD-10 codes

### Migration Considerations

#### Data Type Mapping
1. **Numeric Types**
   - MySQL `int` → PostgreSQL `integer`
   - MySQL `smallint` → PostgreSQL `smallint`
   - MySQL `decimal` → PostgreSQL `numeric`
   - MySQL `double` → PostgreSQL `double precision`

2. **String Types**
   - MySQL `varchar` → PostgreSQL `varchar`
   - MySQL `text`/`mediumtext` → PostgreSQL `text`
   - MySQL `char` → PostgreSQL `char`

3. **Date/Time Types**
   - MySQL `timestamp` → PostgreSQL `timestamp`
   - MySQL `date` → PostgreSQL `date`

4. **Special Types**
   - MySQL `enum` → PostgreSQL `varchar` with CHECK constraints
   - MySQL `set` → PostgreSQL `varchar[]` or custom type

#### Indexing Strategy
1. Primary Keys: Most tables use auto-incrementing IDs
2. Foreign Keys: Need to be explicitly defined in PostgreSQL
3. Unique Constraints: Present in user logins and other business keys

#### Data Migration Challenges
1. **Enum/Set Conversion**
   - Many tables use MySQL ENUM type (e.g., SaleRentType, BillItemOn)
   - Need to create check constraints or lookup tables in PostgreSQL

2. **Default Values**
   - CURRENT_TIMESTAMP behavior differs between MySQL and PostgreSQL
   - Need to adjust trigger/default value behavior

3. **Text Encoding**
   - Ensure proper handling of character encodings during migration
   - Consider using UTF-8 encoding

4. **Zero Dates**
   - MySQL allows '0000-00-00' dates
   - Need to handle these during migration (convert to NULL or minimum date)

#### Performance Considerations
1. **Partitioning**
   - Consider partitioning large tables (orders, invoices) by date
   - Evaluate PostgreSQL partition pruning capabilities

2. **Views**
   - Complex views need performance testing
   - Consider materializing frequently accessed views

3. **Indexing**
   - Review and optimize existing indexes
   - Add new indexes based on query patterns

## 5. Detailed Database Object Analysis

### 5.1 Tables (95 Base Tables)

#### Core Business Tables
1. **Customer Management**
   - `tbl_customer`: Primary customer information
   - `tbl_customerclass`: Customer classification
   - `tbl_customertype`: Customer types
   - `tbl_customer_insurance`: Insurance policies per customer
   - `tbl_customer_notes`: Customer-specific notes

2. **Order Management**
   - `tbl_order`: Primary order table
   - `tbl_orderdetails`: Line items in orders
   - `tbl_orderdeposits`: Deposits against orders
   - `tbl_order_survey`: Order-related surveys

3. **Inventory Management**
   - `tbl_inventory`: Current inventory levels
   - `tbl_inventoryitem`: Product catalog
   - `tbl_inventory_transaction`: Inventory movements
   - `tbl_serial`: Serialized inventory tracking
   - `tbl_serial_maintenance`: Maintenance records
   - `tbl_warehouse`: Storage locations

4. **Medical Documentation**
   - `tbl_cmnform`: Base CMN form table
   - Multiple CMN form type tables (21 tables like `tbl_cmnform_0102a`)
   - `tbl_cmnform_details`: CMN form line items
   - `tbl_cmnform_drorder`: Doctor's orders

5. **Billing and Finance**
   - `tbl_invoice`: Primary invoice table
   - `tbl_invoicedetails`: Invoice line items
   - `tbl_invoice_transaction`: Payment transactions
   - `tbl_batchpayment`: Batch payment processing
   - `tbl_deposits`: Customer deposits
   - `tbl_paymentplan`: Payment plans

6. **Reference Tables**
   - `tbl_manufacturer`: Equipment manufacturers
   - `tbl_producttype`: Product categories
   - `tbl_taxrate`: Tax rates by region
   - `tbl_pricecode`: Pricing structures
   - `tbl_vendor`: Supplier information

### 5.2 Views (15 Views)

1. **Medical Reference Views**
   - `tbl_doctor`: Doctor information
   - `tbl_doctortype`: Doctor specialties
   - `tbl_icd9`: ICD-9 codes
   - `tbl_icd10`: ICD-10 codes

2. **Business Operation Views**
   - `view_billinglist`: Billing cycle management
   - `view_invoicetransaction_statistics`: Payment analytics
   - `view_orderdetails`: Enhanced order information
   - `view_orderdetails_core`: Core order details
   - `view_pricecode`: Pricing information
   - `view_reoccuringlist`: Recurring orders

3. **Insurance Views**
   - `tbl_insurancecompany`: Insurance provider details
   - `tbl_insurancecompanygroup`: Insurance company groupings
   - `tbl_insurancecompanytype`: Insurance company categories

### 5.3 Functions (16 Functions)

1. **Billing Functions**
   - `GetAllowableAmount`: Calculates insurance allowable amount
   - `GetBillableAmount`: Determines billable amount
   - `GetInvoiceModifier`: Applies invoice modifiers
   - `GetAmountMultiplier`, `GetMultiplier`: Price calculations

2. **Date/Period Functions**
   - `GetNewDosTo`: Calculates service period end
   - `GetNextDosFrom`, `GetNextDosTo`: Service period calculations
   - `GetPeriodEnd`, `GetPeriodEnd2`: Period calculations

3. **Order Processing Functions**
   - `OrderedQty2BilledQty`: Converts order to billing quantities
   - `OrderedQty2DeliveryQty`: Converts order to delivery quantities
   - `OrderMustBeClosed`, `OrderMustBeSkipped`: Order status logic

### 5.4 Stored Procedures (60 Procedures)

1. **Inventory Management**
   - `inventory_adjust_2`: Complex inventory adjustments
   - `inventory_refresh`: Updates inventory levels
   - `inventory_transfer`: Handles stock transfers
   - `serial_transfer`: Manages serialized item transfers

2. **Order Processing**
   - `order_process`, `order_process_2`: Order workflow
   - `order_update_dos`: Updates delivery dates
   - `Order_ConvertDepositsIntoPayments`: Payment processing

3. **Invoice Management**
   - `Invoice_UpdateBalance`: Updates invoice totals
   - `InvoiceDetails_AddPayment`: Processes payments
   - `InvoiceDetails_RecalculateInternals`: Recalculates totals

4. **Medical Information Record (MIR) Updates**
   - Multiple `mir_update_*` procedures for different entities
   - Handles updates to customer, insurance, doctor records

### 5.5 Triggers (1 Trigger)

- `tbl_invoice_transaction_beforeinsert`: Pre-insert validation on invoice transactions

### 5.6 Key Database Features

1. **Audit Trail**
   - Most tables include:
     - `LastUpdateUserID`
     - `LastUpdateDatetime`
   - `tbl_changes` tracks modifications

2. **Complex Business Rules**
   - Extensive stored procedures for business logic
   - Multiple validation layers
   - Sophisticated billing calculations

3. **Medical Equipment Specifics**
   - CMN form management
   - Equipment rental tracking
   - Maintenance records
   - Serial number tracking

4. **Financial Controls**
   - Multi-level pricing
   - Payment plan support
   - Insurance billing
   - Deposit management

### 5.7 Migration Challenges

1. **Stored Procedures**
   - Complex business logic in MySQL syntax
   - Heavy use of cursors and temporary tables
   - MySQL-specific transaction handling

2. **Views**
   - Complex joins and calculations
   - MySQL-specific date functions
   - Performance optimization needed

3. **Data Types**
   - ENUM fields used extensively
   - SET data type conversions
   - Zero date handling

4. **Triggers**
   - Different syntax in PostgreSQL
   - Transaction management differences
   - Before/After trigger behavior changes

## 6. Migration Considerations

### Function Migration
1. **Date Functions**
   - PostgreSQL has different date handling functions
   - Need to rewrite using PostgreSQL's interval and date arithmetic

2. **Numeric Calculations**
   - Review decimal precision handling
   - Adjust rounding behavior to match MySQL

3. **Control Flow**
   - Replace MySQL's IF/ELSEIF with PostgreSQL's CASE expressions
   - Handle NULL differently in PostgreSQL

### View Migration
1. **Complex Views**
   - Rewrite using PostgreSQL's CTE (WITH clauses)
   - Optimize join performance
   - Review materialized view opportunities

2. **Date/Time Handling**
   - Adjust INTERVAL syntax
   - Review timezone handling

3. **String Operations**
   - Update CONCAT operations
   - Review LIKE patterns

### Performance Optimization
1. **Indexing Strategy**
   - Review and optimize indexes for complex views
   - Consider partial indexes in PostgreSQL
   - Plan for materialized views

2. **Query Optimization**
   - Rewrite complex joins for PostgreSQL
   - Review execution plans
   - Consider partitioning for large tables

## 7. Next Steps

1. Create PostgreSQL function equivalents
2. Test complex business logic in PostgreSQL
3. Validate view performance
4. Create migration test cases
