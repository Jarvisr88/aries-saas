"""
PostgreSQL Schema Generator
Converts MySQL schema to PostgreSQL compatible schema
"""

import json
import re
from datetime import datetime
import os

class PostgresSchemaGenerator:
    def __init__(self):
        self.type_mappings = {
            'tinyint(1)': 'boolean',
            'tinyint': 'smallint',
            'smallint': 'smallint',
            'mediumint': 'integer',
            'int': 'integer',
            'bigint': 'bigint',
            'float': 'real',
            'double': 'double precision',
            'decimal': 'decimal',
            'char': 'char',
            'varchar': 'varchar',
            'text': 'text',
            'mediumtext': 'text',
            'longtext': 'text',
            'datetime': 'timestamp',
            'timestamp': 'timestamp',
            'date': 'date',
            'time': 'time',
            'year': 'smallint',
            'binary': 'bytea',
            'varbinary': 'bytea',
            'blob': 'bytea',
            'bit': 'boolean'
        }

    def convert_type(self, mysql_type):
        """Convert MySQL type to PostgreSQL type"""
        # Extract base type and size/precision
        match = re.match(r'(\w+)(?:\((.*?)\))?', mysql_type.lower())
        if not match:
            return mysql_type
            
        base_type, params = match.groups()
        
        # Handle special cases
        if base_type == 'enum':
            # Extract enum values
            enum_values = params.replace("'", "").split(',')
            return f"varchar CHECK (value IN ({','.join(repr(v) for v in enum_values)}))"
        
        if base_type == 'set':
            # Convert to array type
            set_values = params.replace("'", "").split(',')
            return f"varchar[] CHECK (value <@ ARRAY[{','.join(repr(v) for v in set_values)}])"
            
        # Handle standard type mappings
        pg_type = self.type_mappings.get(base_type, base_type)
        
        # Add size/precision if needed
        if params and pg_type in ('varchar', 'char', 'decimal'):
            return f"{pg_type}({params})"
            
        return pg_type

    def convert_column_definition(self, column):
        """Convert MySQL column definition to PostgreSQL"""
        parts = []
        parts.append(f"{column['COLUMN_NAME']} {self.convert_type(column['COLUMN_TYPE'])}")
        
        if column['IS_NULLABLE'] == 'NO':
            parts.append('NOT NULL')
            
        if column['COLUMN_DEFAULT'] is not None:
            if column['COLUMN_DEFAULT'] == 'CURRENT_TIMESTAMP':
                parts.append('DEFAULT CURRENT_TIMESTAMP')
            else:
                parts.append(f"DEFAULT {column['COLUMN_DEFAULT']}")
                
        return ' '.join(parts)

    def convert_table(self, table_name, table_data):
        """Convert MySQL table to PostgreSQL"""
        lines = [f'CREATE TABLE {table_name} (']
        
        # Add columns
        column_defs = []
        for col in table_data['columns']:
            column_defs.append('    ' + self.convert_column_definition(col))
        
        # Add primary key
        pk_columns = [idx['Column_name'] for idx in table_data['indexes'] 
                     if idx.get('Key_name') == 'PRIMARY']
        if pk_columns:
            column_defs.append(f"    PRIMARY KEY ({', '.join(pk_columns)})")
            
        lines.append(',\n'.join(column_defs))
        lines.append(');')
        
        # Add indexes
        for idx in table_data['indexes']:
            if idx.get('Key_name') != 'PRIMARY':
                index_type = 'UNIQUE ' if not idx.get('Non_unique') else ''
                index_name = f"{table_name}_{idx['Column_name']}_idx"
                lines.append(f"\nCREATE {index_type}INDEX {index_name} ON {table_name} ({idx['Column_name']});")
        
        # Add foreign keys
        for fk in table_data['foreign_keys']:
            constraint_name = fk['CONSTRAINT_NAME']
            column = fk['COLUMN_NAME']
            ref_table = fk['REFERENCED_TABLE_NAME']
            ref_column = fk['REFERENCED_COLUMN_NAME']
            lines.append(f"\nALTER TABLE {table_name} ADD CONSTRAINT {constraint_name} "
                        f"FOREIGN KEY ({column}) REFERENCES {ref_table}({ref_column});")
        
        return '\n'.join(lines)

    def convert_schema(self, mysql_schema):
        """Convert entire MySQL schema to PostgreSQL"""
        output = []
        
        # Convert tables
        for table_name, table_data in mysql_schema['tables'].items():
            output.append(self.convert_table(table_name, table_data))
            output.append('\n')
            
        # Convert views
        for view_name, view_data in mysql_schema['views'].items():
            # Simplified view conversion - may need manual review
            output.append(f"CREATE VIEW {view_name} AS")
            output.append(view_data['create_statement']['Create View'])
            output.append(';\n')
            
        return '\n'.join(output)

    def generate_migration_script(self, mysql_schema):
        """Generate complete migration script"""
        timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
        output_dir = os.path.join('sql', 'migration')
        os.makedirs(output_dir, exist_ok=True)
        
        # Generate schema
        schema_sql = self.convert_schema(mysql_schema)
        
        # Write migration script
        filename = os.path.join(output_dir, f'migration_{timestamp}.sql')
        with open(filename, 'w') as f:
            f.write('-- Migration script generated at ' + timestamp + '\n\n')
            f.write('BEGIN;\n\n')
            f.write(schema_sql)
            f.write('\nCOMMIT;\n')
        
        return filename

def main():
    # Load extracted schema
    schema_dir = os.path.join('docs', 'schema')
    latest_schema = max(
        [f for f in os.listdir(schema_dir) if f.startswith('complete_schema_')],
        key=lambda x: os.path.getctime(os.path.join(schema_dir, x))
    )
    
    with open(os.path.join(schema_dir, latest_schema)) as f:
        mysql_schema = json.load(f)
    
    # Generate PostgreSQL schema
    generator = PostgresSchemaGenerator()
    migration_file = generator.generate_migration_script(mysql_schema)
    
    print(f"Migration script generated: {migration_file}")

if __name__ == "__main__":
    main()
