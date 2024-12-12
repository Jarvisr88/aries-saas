# MySQL to PostgreSQL Data Type Conversion Plan

## Basic Data Types

### Numeric Types
| MySQL Type | PostgreSQL Type | Notes |
|------------|----------------|--------|
| TINYINT(1) | BOOLEAN | For boolean flags |
| TINYINT | SMALLINT | For other cases |
| SMALLINT | SMALLINT | Direct mapping |
| MEDIUMINT | INTEGER | Upgrade to INTEGER |
| INT/INTEGER | INTEGER | Direct mapping |
| BIGINT | BIGINT | Direct mapping |
| FLOAT | REAL | Direct mapping |
| DOUBLE | DOUBLE PRECISION | Direct mapping |
| DECIMAL(p,s) | DECIMAL(p,s) | Direct mapping |

### String Types
| MySQL Type | PostgreSQL Type | Notes |
|------------|----------------|--------|
| CHAR(n) | CHAR(n) | Direct mapping |
| VARCHAR(n) | VARCHAR(n) | Direct mapping |
| TEXT | TEXT | Direct mapping |
| MEDIUMTEXT | TEXT | Consolidate to TEXT |
| LONGTEXT | TEXT | Consolidate to TEXT |
| ENUM | VARCHAR + CHECK | Convert to VARCHAR with CHECK constraint |
| SET | VARCHAR[] | Convert to array type |

### Date/Time Types
| MySQL Type | PostgreSQL Type | Notes |
|------------|----------------|--------|
| DATE | DATE | Handle '0000-00-00' dates |
| DATETIME | TIMESTAMP | Convert to TIMESTAMP |
| TIMESTAMP | TIMESTAMP | Direct mapping |
| TIME | TIME | Direct mapping |
| YEAR | SMALLINT | Convert to numeric |

### Binary Types
| MySQL Type | PostgreSQL Type | Notes |
|------------|----------------|--------|
| BINARY | BYTEA | Convert to BYTEA |
| VARBINARY | BYTEA | Convert to BYTEA |
| BLOB | BYTEA | Convert to BYTEA |
| BIT | BOOLEAN or BIT | Based on length |

## Special Handling Cases

### ENUM Fields
Convert all ENUM fields to VARCHAR with CHECK constraints. Example:

```sql
-- MySQL
CREATE TABLE example (
    status ENUM('active', 'inactive', 'pending')
);

-- PostgreSQL
CREATE TABLE example (
    status VARCHAR(8) CHECK (status IN ('active', 'inactive', 'pending'))
);
```

### SET Fields
Convert SET fields to PostgreSQL arrays. Example:

```sql
-- MySQL
CREATE TABLE example (
    permissions SET('read', 'write', 'execute')
);

-- PostgreSQL
CREATE TABLE example (
    permissions VARCHAR[] CHECK (
        permissions <@ ARRAY['read', 'write', 'execute']
    )
);
```

### Zero Dates
Handle '0000-00-00' dates with NULL or a specific default date:

```sql
-- During migration
UPDATE table_name 
SET date_column = NULL 
WHERE date_column = '0000-00-00';
```

### Default Values
1. CURRENT_TIMESTAMP - Direct mapping
2. ON UPDATE CURRENT_TIMESTAMP - Implement using triggers
3. Auto-increment - Use SERIAL or BIGSERIAL

## Indexes and Constraints

### Primary Keys
Direct mapping using PRIMARY KEY constraint

### Foreign Keys
Direct mapping with ON DELETE/UPDATE actions preserved

### Unique Constraints
Direct mapping using UNIQUE constraint

### Indexes
1. BTREE - Use standard PostgreSQL index
2. HASH - Convert to BTREE (PostgreSQL optimizes automatically)
3. FULLTEXT - Use pg_trgm and GiST/GIN indexes

## Custom Data Type Mappings

### Application-Specific Types
| Table | Column | Current Type | New Type | Notes |
|-------|---------|--------------|-----------|-------|
| tbl_customer | Courtesy | ENUM | VARCHAR(4) | Add CHECK constraint |
| tbl_customer | Gender | ENUM | VARCHAR(6) | Add CHECK constraint |
| tbl_customer | MaritalStatus | ENUM | VARCHAR(20) | Add CHECK constraint |
| tbl_customer | MIR | SET | VARCHAR[] | Convert to array |
| tbl_customer | Collections | BIT(1) | BOOLEAN | Convert to boolean |

## Migration Scripts
Conversion scripts will be generated for each special case, including:
1. ENUM/SET conversion
2. Zero date handling
3. Timestamp conversion
4. Default value adjustments
5. Index recreation
