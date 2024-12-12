# PostgreSQL Schema Migration Documentation

## Overview
This document details the schema changes made during the migration from MySQL to PostgreSQL for the DME/HME application database.

## Major Changes

### 1. Data Type Conversions

#### ENUM Fields
| Table | Column | Original Type | New Type | Constraints |
|-------|---------|--------------|-----------|------------|
| tbl_customer | Courtesy | ENUM('Dr.','Miss','Mr.','Mrs.','Rev.') | VARCHAR(4) | CHECK IN ('Dr.','Miss','Mr.','Mrs.','Rev.') |
| tbl_customer | Gender | ENUM('Male','Female') | VARCHAR(6) | CHECK IN ('Male','Female') |
| tbl_customer | MaritalStatus | ENUM | VARCHAR(20) | CHECK with allowed values |
| tbl_customer | EmploymentStatus | ENUM | VARCHAR(20) | CHECK with allowed values |
| tbl_customer | MilitaryBranch | ENUM | VARCHAR(20) | CHECK with allowed values |
| tbl_customer | StudentStatus | ENUM | VARCHAR(20) | CHECK with allowed values |

#### SET Fields
| Table | Column | Original Type | New Type | Constraints |
|-------|---------|--------------|-----------|------------|
| tbl_customer | MIR | SET | VARCHAR[] | Array with CHECK constraint |
| tbl_cmnform | MIR | SET | VARCHAR[] | Array with CHECK constraint |
| tbl_insurancecompany | MIR | SET | VARCHAR[] | Array with CHECK constraint |

#### Boolean Fields
| Table | Column | Original Type | New Type |
|-------|---------|--------------|-----------|
| tbl_customer | Collections | BIT(1) | BOOLEAN |
| tbl_customer | Emergency | TINYINT(1) | BOOLEAN |
| tbl_customer | HIPPANote | TINYINT(1) | BOOLEAN |

### 2. Index Changes

#### B-Tree Indexes
- All MySQL B-TREE indexes converted to PostgreSQL B-Tree indexes
- Index names standardized to `{table_name}_{column_name}_idx`

#### Full-Text Indexes
- MySQL FULLTEXT indexes replaced with PostgreSQL text search capabilities
- Added GiST indexes for text search columns

### 3. Default Values

#### Timestamp Defaults
- `CURRENT_TIMESTAMP` defaults preserved
- `ON UPDATE CURRENT_TIMESTAMP` replaced with triggers
- Zero dates (`0000-00-00`) converted to NULL

#### Auto-increment Fields
- MySQL AUTO_INCREMENT replaced with PostgreSQL SERIAL
- Sequence names standardized to `{table_name}_{column_name}_seq`

### 4. Constraint Changes

#### Foreign Keys
- All foreign key relationships preserved
- Constraint names standardized to `fk_{table_name}_{referenced_table}`
- ON DELETE/UPDATE actions maintained

#### Check Constraints
- Added for ENUM field replacements
- Added for SET field array validation
- Added for business logic validation

### 5. View Changes

#### Complex Views
The following views required significant modifications:
- `view_mir`
- `view_orderdetails`
- `view_invoicetransaction_statistics`

Changes include:
- MySQL-specific functions replaced with PostgreSQL equivalents
- JOIN syntax updated
- Aggregate functions adjusted
- Window functions optimized

## Performance Considerations

### Indexes
1. B-Tree indexes on frequently queried columns
2. GiST indexes for text search
3. Partial indexes for filtered queries
4. Combined indexes for common query patterns

### Partitioning
Large tables considered for partitioning:
- `tbl_invoice`
- `tbl_order`
- `tbl_inventory_transaction`

### Query Optimization
1. Materialized views for complex queries
2. Optimized JOIN operations
3. Efficient array operations for SET replacements

## Security Changes

### Role-Based Access
- Implemented PostgreSQL roles
- Schema-level permissions
- Object-level grants

### Row-Level Security
- Added RLS policies for multi-tenant data
- Implemented security context

## Monitoring and Maintenance

### Statistics Collection
- Automated ANALYZE scheduling
- Custom statistics for complex columns

### Performance Monitoring
- Query performance tracking
- Index usage statistics
- Table bloat monitoring

## Backup and Recovery

### Backup Strategy
1. Daily full backups
2. Continuous WAL archiving
3. Point-in-time recovery capability

### High Availability
1. Streaming replication setup
2. Failover configuration
3. Connection pooling

## Migration Verification

### Data Integrity Checks
1. Row count validation
2. Checksum verification
3. Business logic validation
4. Constraint enforcement

### Performance Validation
1. Query execution time comparison
2. Index usage verification
3. Resource utilization monitoring

## Post-Migration Tasks

### Optimization
1. Table statistics update
2. Index optimization
3. Vacuum analysis
4. Configuration tuning

### Monitoring
1. Query performance tracking
2. Resource usage monitoring
3. Error logging
4. Connection management

### Documentation
1. Schema documentation
2. Procedure documentation
3. Security documentation
4. Backup/recovery procedures

## Rollback Plan

### Triggers
1. Data integrity issues
2. Performance degradation
3. Application incompatibility
4. Security concerns

### Procedure
1. Stop application services
2. Restore MySQL database
3. Revert application configuration
4. Validate system state
5. Resume services

## Appendix

### A. Complex Field Mappings
Detailed mapping of complex fields including:
- ENUM values
- SET members
- Custom types
- Computed columns

### B. Index Strategy
Comprehensive index design including:
- Primary keys
- Foreign keys
- Performance indexes
- Full-text search indexes

### C. Trigger Implementations
PostgreSQL trigger equivalents for:
- Audit logging
- Timestamp updates
- Computed values
- Business rules
