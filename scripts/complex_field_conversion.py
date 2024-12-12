"""
Complex Field Conversion Script
Handles conversion of ENUM, SET, and other complex MySQL fields to PostgreSQL
"""

import mysql.connector
from typing import Dict, List, Tuple
import json
import logging
from datetime import datetime
import os

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

def load_config() -> Dict:
    """Load database configuration from JSON file"""
    config_path = os.path.join('config', 'database.json')
    with open(config_path) as f:
        return json.load(f)

class ComplexFieldConverter:
    def __init__(self, mysql_config: Dict):
        self.mysql_conn = mysql.connector.connect(**mysql_config)
        self.mysql_cursor = self.mysql_conn.cursor(dictionary=True)

    def get_enum_fields(self) -> List[Tuple]:
        """Get all ENUM fields from MySQL database"""
        query = """
        SELECT 
            TABLE_NAME,
            COLUMN_NAME,
            COLUMN_TYPE,
            IS_NULLABLE,
            COLUMN_DEFAULT
        FROM information_schema.COLUMNS 
        WHERE TABLE_SCHEMA = 'c01'
        AND COLUMN_TYPE LIKE 'enum%'
        """
        self.mysql_cursor.execute(query)
        return self.mysql_cursor.fetchall()

    def get_set_fields(self) -> List[Tuple]:
        """Get all SET fields from MySQL database"""
        query = """
        SELECT 
            TABLE_NAME,
            COLUMN_NAME,
            COLUMN_TYPE,
            IS_NULLABLE,
            COLUMN_DEFAULT
        FROM information_schema.COLUMNS 
        WHERE TABLE_SCHEMA = 'c01'
        AND COLUMN_TYPE LIKE 'set%'
        """
        self.mysql_cursor.execute(query)
        return self.mysql_cursor.fetchall()

    def parse_enum_values(self, enum_type: str) -> List[str]:
        """Parse ENUM values from MySQL column type"""
        # Extract values between parentheses and split
        values_str = enum_type[enum_type.find('(')+1:enum_type.rfind(')')]
        return [v.strip("'") for v in values_str.split(',')]

    def parse_set_values(self, set_type: str) -> List[str]:
        """Parse SET values from MySQL column type"""
        values_str = set_type[set_type.find('(')+1:set_type.rfind(')')]
        return [v.strip("'") for v in values_str.split(',')]

    def generate_enum_conversion(self, field: Dict) -> Tuple[str, str]:
        """Generate ALTER TABLE statements for ENUM conversion"""
        values = self.parse_enum_values(field['COLUMN_TYPE'])
        table = field['TABLE_NAME']
        column = field['COLUMN_NAME']
        
        # Create temporary column
        temp_col = f"{column}_new"
        
        alter_statements = [
            f"-- Convert {table}.{column} from ENUM to VARCHAR with CHECK constraint",
            f"ALTER TABLE {table} ADD COLUMN {temp_col} VARCHAR({max(len(v) for v in values)});",
            f"UPDATE {table} SET {temp_col} = {column}::text;",
            f"ALTER TABLE {table} DROP COLUMN {column};",
            f"ALTER TABLE {table} RENAME COLUMN {temp_col} TO {column};",
            f"ALTER TABLE {table} ADD CONSTRAINT {table}_{column}_check CHECK ({column} IN ({', '.join(repr(v) for v in values)}));"
        ]
        
        # Generate rollback statements
        rollback = [
            f"-- Rollback {table}.{column} conversion",
            f"ALTER TABLE {table} DROP CONSTRAINT IF EXISTS {table}_{column}_check;",
            f"ALTER TABLE {table} ALTER COLUMN {column} TYPE text;"
        ]
        
        return '\n'.join(alter_statements), '\n'.join(rollback)

    def generate_set_conversion(self, field: Dict) -> Tuple[str, str]:
        """Generate ALTER TABLE statements for SET conversion"""
        values = self.parse_set_values(field['COLUMN_TYPE'])
        table = field['TABLE_NAME']
        column = field['COLUMN_NAME']
        
        # Create temporary column
        temp_col = f"{column}_new"
        
        alter_statements = [
            f"-- Convert {table}.{column} from SET to VARCHAR[] with CHECK constraint",
            f"ALTER TABLE {table} ADD COLUMN {temp_col} VARCHAR[];",
            f"UPDATE {table} SET {temp_col} = string_to_array({column}, ',');",
            f"ALTER TABLE {table} DROP COLUMN {column};",
            f"ALTER TABLE {table} RENAME COLUMN {temp_col} TO {column};",
            f"ALTER TABLE {table} ADD CONSTRAINT {table}_{column}_check CHECK ({column} <@ ARRAY[{', '.join(repr(v) for v in values)}]);"
        ]
        
        # Generate rollback statements
        rollback = [
            f"-- Rollback {table}.{column} conversion",
            f"ALTER TABLE {table} DROP CONSTRAINT IF EXISTS {table}_{column}_check;",
            f"ALTER TABLE {table} ALTER COLUMN {column} TYPE text;"
        ]
        
        return '\n'.join(alter_statements), '\n'.join(rollback)

    def generate_conversion_scripts(self) -> None:
        """Generate all conversion scripts"""
        timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
        
        # Get all complex fields
        enum_fields = self.get_enum_fields()
        set_fields = self.get_set_fields()
        
        # Create output directory
        os.makedirs('sql/migration', exist_ok=True)
        
        # Generate migration scripts
        with open(f'sql/migration/complex_fields_{timestamp}.sql', 'w') as f:
            f.write('-- Complex field conversion script\n')
            f.write(f'-- Generated at {timestamp}\n\n')
            f.write('BEGIN;\n\n')
            
            # Handle ENUMs
            f.write('-- ENUM Conversions\n')
            for field in enum_fields:
                migration, _ = self.generate_enum_conversion(field)
                f.write(f"\n-- Converting {field['TABLE_NAME']}.{field['COLUMN_NAME']}\n")
                f.write(migration + '\n')
            
            # Handle SETs
            f.write('\n-- SET Conversions\n')
            for field in set_fields:
                migration, _ = self.generate_set_conversion(field)
                f.write(f"\n-- Converting {field['TABLE_NAME']}.{field['COLUMN_NAME']}\n")
                f.write(migration + '\n')
            
            f.write('\nCOMMIT;\n')
        
        # Generate rollback scripts
        with open(f'sql/migration/complex_fields_rollback_{timestamp}.sql', 'w') as f:
            f.write('-- Complex field rollback script\n')
            f.write(f'-- Generated at {timestamp}\n\n')
            f.write('BEGIN;\n\n')
            
            # Handle ENUMs
            f.write('-- ENUM Rollbacks\n')
            for field in enum_fields:
                _, rollback = self.generate_enum_conversion(field)
                f.write(f"\n-- Rolling back {field['TABLE_NAME']}.{field['COLUMN_NAME']}\n")
                f.write(rollback + '\n')
            
            # Handle SETs
            f.write('\n-- SET Rollbacks\n')
            for field in set_fields:
                _, rollback = self.generate_set_conversion(field)
                f.write(f"\n-- Rolling back {field['TABLE_NAME']}.{field['COLUMN_NAME']}\n")
                f.write(rollback + '\n')
            
            f.write('\nCOMMIT;\n')

        logger.info(f"Generated migration script: sql/migration/complex_fields_{timestamp}.sql")
        logger.info(f"Generated rollback script: sql/migration/complex_fields_rollback_{timestamp}.sql")

    def close(self):
        """Close database connections"""
        self.mysql_cursor.close()
        self.mysql_conn.close()

def main():
    # Load configuration
    config = load_config()
    
    converter = ComplexFieldConverter(config['mysql'])
    try:
        converter.generate_conversion_scripts()
        logger.info("Conversion scripts generated successfully")
    finally:
        converter.close()

if __name__ == "__main__":
    main()
