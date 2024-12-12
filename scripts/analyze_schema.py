import mysql.connector
from mysql.connector import Error

def connect():
    try:
        connection = mysql.connector.connect(
            host='localhost',
            database='c01',
            user='root',
            password='dmeworks'
        )
        return connection
    except Error as e:
        print(f"Error connecting to MySQL Database: {e}")
        return None

def get_table_list(connection):
    cursor = connection.cursor()
    cursor.execute("SHOW TABLES")
    return [table[0] for table in cursor.fetchall()]

def get_table_schema(connection, table_name):
    cursor = connection.cursor()
    cursor.execute(f"""
        SELECT 
            COLUMN_NAME,
            DATA_TYPE,
            IS_NULLABLE,
            COLUMN_KEY,
            COLUMN_DEFAULT,
            EXTRA
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA = 'c01'
        AND TABLE_NAME = '{table_name}'
        ORDER BY ORDINAL_POSITION
    """)
    return cursor.fetchall()

def get_foreign_keys(connection, table_name):
    cursor = connection.cursor()
    cursor.execute(f"""
        SELECT
            COLUMN_NAME,
            REFERENCED_TABLE_NAME,
            REFERENCED_COLUMN_NAME
        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
        WHERE TABLE_SCHEMA = 'c01'
        AND TABLE_NAME = '{table_name}'
        AND REFERENCED_TABLE_NAME IS NOT NULL
    """)
    return cursor.fetchall()

def analyze_schema():
    connection = connect()
    if not connection:
        return
    
    try:
        tables = get_table_list(connection)
        
        print("# Database Schema Analysis")
        print("\n## Tables Overview")
        print(f"Total number of tables: {len(tables)}\n")
        
        for table in sorted(tables):
            print(f"\n### Table: `{table}`")
            
            # Get and print schema
            schema = get_table_schema(connection, table)
            print("\n#### Columns")
            print("| Column | Type | Nullable | Key | Default | Extra |")
            print("|--------|------|----------|-----|---------|--------|")
            for col in schema:
                col = [str(c) if c is not None else 'NULL' for c in col]
                print(f"| {col[0]} | {col[1]} | {col[2]} | {col[3] or ''} | {col[4]} | {col[5]} |")
            
            # Get and print foreign keys
            fks = get_foreign_keys(connection, table)
            if fks:
                print("\n#### Foreign Keys")
                print("| Column | Referenced Table | Referenced Column |")
                print("|---------|-----------------|-------------------|")
                for fk in fks:
                    print(f"| {fk[0]} | {fk[1]} | {fk[2]} |")
    
    finally:
        connection.close()

if __name__ == '__main__':
    analyze_schema()
