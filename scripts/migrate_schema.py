import mysql.connector
from mysql.connector.connection import MySQLConnection
from mysql.connector.pooling import PooledMySQLConnection
from mysql.connector.abstracts import MySQLConnectionAbstract
import json
import logging
from pathlib import Path
import re
from typing import Dict, List, Tuple, Union, Optional

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

# Data type mapping from MySQL to PostgreSQL
TYPE_MAPPING = {
    'tinyint': 'smallint',
    'smallint': 'smallint',
    'mediumint': 'integer',
    'int': 'integer',
    'bigint': 'bigint',
    'float': 'real',
    'double': 'double precision',
    'decimal': 'decimal',
    'datetime': 'timestamp',
    'timestamp': 'timestamp',
    'time': 'time',
    'date': 'date',
    'char': 'char',
    'varchar': 'varchar',
    'tinytext': 'text',
    'text': 'text',
    'mediumtext': 'text',
    'longtext': 'text',
    'tinyblob': 'bytea',
    'blob': 'bytea',
    'mediumblob': 'bytea',
    'longblob': 'bytea',
    'binary': 'bytea',
    'varbinary': 'bytea',
    'enum': 'varchar',
    'set': 'varchar[]',
    'json': 'jsonb',
    'bool': 'boolean',
    'boolean': 'boolean'
}

def load_config() -> Dict:
    config_path = Path(__file__).parent.parent / 'config' / 'database.json'
    with open(config_path) as f:
        return json.load(f)

def connect_db(config: Dict) -> Union[MySQLConnection, PooledMySQLConnection, MySQLConnectionAbstract]:
    """Connect to MySQL database and return the connection object.
    
    Returns:
        A MySQL connection object which could be one of:
        - MySQLConnection: Standard connection
        - PooledMySQLConnection: Connection from a connection pool
        - MySQLConnectionAbstract: Abstract base connection
    """
    mysql_config = config['mysql']
    return mysql.connector.connect(
        host=mysql_config['host'],
        user=mysql_config['user'],
        password=mysql_config['password'],
        database=mysql_config['database']
    )

def get_all_tables(cursor) -> List[str]:
    cursor.execute("SHOW TABLES")
    return [table[0] for table in cursor.fetchall()]

def get_table_schema(cursor, table: str) -> List[Dict]:
    cursor.execute(f"DESCRIBE {table}")
    return [dict(zip(cursor.column_names, row)) for row in cursor.fetchall()]

def get_table_indexes(cursor, table: str) -> List[Dict]:
    cursor.execute(f"SHOW INDEX FROM {table}")
    return [dict(zip(cursor.column_names, row)) for row in cursor.fetchall()]

def clean_identifier(name: str) -> str:
    """Clean and format identifier names to snake_case"""
    # Convert to lowercase and replace spaces/special chars with underscore
    clean_name = re.sub(r'[^a-zA-Z0-9]', '_', name.lower())
    # Remove consecutive underscores
    clean_name = re.sub(r'_+', '_', clean_name)
    # Remove leading/trailing underscores
    clean_name = clean_name.strip('_')
    return clean_name

def clean_default_value(value: str, column_type: str) -> str:
    """Clean and format default values"""
    if value is None or value.upper() == 'NULL':
        return 'NULL'
    
    # Handle numeric types
    if column_type in ('integer', 'bigint', 'smallint', 'double precision', 'real', 'numeric'):
        return value
    
    # Handle boolean types
    if column_type == 'boolean':
        return 'true' if value.lower() in ('1', 'true', 't', 'yes', 'y') else 'false'
    
    # Handle array types
    if column_type.endswith('[]'):
        return "'{}'"
    
    # Handle timestamp default
    if value.upper() == 'CURRENT_TIMESTAMP':
        return 'CURRENT_TIMESTAMP'
        
    # All other types should be quoted
    return f"'{value}'"

def convert_column_type(mysql_type: str) -> str:
    """Convert MySQL column type to PostgreSQL"""
    # Extract base type and size/precision
    match = re.match(r'(\w+)(?:\((.*?)\))?', mysql_type.lower())
    if not match:
        return mysql_type
    
    base_type, specs = match.groups()
    
    # Handle ENUM types
    if base_type == 'enum':
        values = [v.strip("'") for v in specs.split(',')]
        formatted_values = ", ".join(f"'{v.strip()}'" for v in values)
        return f"varchar CHECK (value IN ({formatted_values}))"
    
    # Handle SET types
    if base_type == 'set':
        return 'varchar[]'
    
    # Map to PostgreSQL type
    pg_type = TYPE_MAPPING.get(base_type, base_type)
    
    # Handle numeric types with precision
    if pg_type == 'numeric' and specs:
        return f'{pg_type}({specs})'
    
    # Handle character types with length
    if pg_type in ('char', 'varchar') and specs:
        return f'{pg_type}({specs})'
    
    return pg_type

def generate_create_table_sql(table: str, columns: List[Dict], indexes: List[Dict]) -> str:
    """Generate PostgreSQL CREATE TABLE statement"""
    table_name = clean_identifier(table)
    lines = [f'CREATE TABLE {table_name} (']
    
    # Add columns
    column_defs = []
    constraints = []
    
    for col in columns:
        col_name = clean_identifier(col['Field'])
        pg_type = convert_column_type(col['Type'])
        nullable = 'NULL' if col['Null'] == 'YES' else 'NOT NULL'
        
        # Handle default value
        default = ''
        if col['Default'] is not None:
            default = f"DEFAULT {clean_default_value(col['Default'], pg_type)}"
        
        # Handle auto_increment
        if col['Extra'] == 'auto_increment':
            if pg_type == 'integer':
                pg_type = 'SERIAL'
            elif pg_type == 'bigint':
                pg_type = 'BIGSERIAL'
        
        # Check for ENUM constraint
        if 'CHECK' in pg_type:
            base_type, check_constraint = pg_type.split('CHECK', 1)
            pg_type = base_type.strip()
            check_constraint = check_constraint.replace('value', col_name)
            constraints.append(f"    {check_constraint}")
        
        column_defs.append(
            f"    {col_name} {pg_type} {nullable} {default}".strip()
        )
    
    # Join column definitions with commas
    if column_defs:
        lines.extend([f"{col_def}," for col_def in column_defs[:-1]])
        lines.append(column_defs[-1])  # Last column without comma
    
    # Add constraints with commas
    if constraints:
        lines.extend([f"{constraint}," for constraint in constraints[:-1]])
        if constraints:
            lines.append(constraints[-1])
    
    # Add primary key
    pk_columns = [clean_identifier(col['Field']) for col in columns if col['Key'] == 'PRI']
    if pk_columns:
        pk_cols = ', '.join(pk_columns)
        if constraints or len(column_defs) > 0:
            lines.append(f",    PRIMARY KEY ({pk_cols})")
        else:
            lines.append(f"    PRIMARY KEY ({pk_cols})")
    
    lines.append(');')
    
    # Add indexes
    index_sql = []
    unique_indexes = {}
    
    for idx in indexes:
        if idx['Key_name'] == 'PRIMARY':
            continue
            
        if idx['Key_name'] not in unique_indexes:
            unique_indexes[idx['Key_name']] = {
                'columns': [],
                'unique': idx['Non_unique'] == 0
            }
        unique_indexes[idx['Key_name']]['columns'].append(clean_identifier(idx['Column_name']))
    
    for idx_name, idx_info in unique_indexes.items():
        unique_str = 'UNIQUE ' if idx_info['unique'] else ''
        columns_str = ', '.join(idx_info['columns'])
        index_name = clean_identifier(f"{table_name}_{idx_name}")
        index_sql.append(
            f"CREATE {unique_str}INDEX {index_name} ON {table_name} ({columns_str});"
        )
    
    return '\n'.join(lines + [''] + index_sql)

def main():
    config = load_config()
    conn = connect_db(config)
    cursor = conn.cursor()
    
    output_dir = Path(__file__).parent.parent / 'scripts' / 'migration'
    output_dir.mkdir(parents=True, exist_ok=True)
    
    schema_sql_path = output_dir / 'schema.sql'
    validation_report_path = output_dir / 'data_type_validation.md'
    
    tables = get_all_tables(cursor)
    schema_sql = []
    validation_issues = []
    
    for table in tables:
        logger.info(f"Processing table: {table}")
        columns = get_table_schema(cursor, table)
        indexes = get_table_indexes(cursor, table)
        
        # Generate PostgreSQL schema
        create_table_sql = generate_create_table_sql(table, columns, indexes)
        schema_sql.append(create_table_sql)
        
        # Validate data types
        for col in columns:
            mysql_type = col['Type'].lower()
            type_match: Optional[re.Match[str]] = re.match(r'(\w+)', mysql_type)
            
            if type_match is None:
                validation_issues.append(
                    f"- Table `{table}`, Column `{col['Field']}`: Invalid type format '{mysql_type}'"
                )
                continue
                
            base_type = type_match.group(1)
            
            if base_type not in TYPE_MAPPING:
                validation_issues.append(
                    f"- Table `{table}`, Column `{col['Field']}`: "
                    f"Unknown MySQL type '{mysql_type}'"
                )
            elif base_type in ['enum', 'set']:
                validation_issues.append(
                    f"- Table `{table}`, Column `{col['Field']}`: "
                    f"Needs manual review - Converting {base_type} to {TYPE_MAPPING[base_type]}"
                )
    
    # Write schema SQL
    with open(schema_sql_path, 'w') as f:
        f.write('\n\n'.join(schema_sql))
    
    # Write validation report
    with open(validation_report_path, 'w') as f:
        f.write("# Data Type Validation Report\n\n")
        if validation_issues:
            f.write("## Issues Found\n\n")
            f.write('\n'.join(validation_issues))
        else:
            f.write("No data type validation issues found.")
    
    cursor.close()
    conn.close()
    
    logger.info(f"Schema SQL written to: {schema_sql_path}")
    logger.info(f"Validation report written to: {validation_report_path}")

if __name__ == '__main__':
    main()
