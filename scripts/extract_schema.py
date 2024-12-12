"""
MySQL Schema Extraction Script
This script extracts complete schema information from MySQL database
"""

import mysql.connector
from mysql.connector import Error
import json
from datetime import datetime
import os

class SchemaExtractor:
    def __init__(self, host='localhost', user='root', password='dmeworks', database='c01'):
        self.connection = mysql.connector.connect(
            host=host,
            user=user,
            password=password,
            database=database
        )
        self.cursor = self.connection.cursor(dictionary=True)
        
    def get_table_list(self):
        """Get list of all tables and views"""
        query = """
        SELECT TABLE_NAME, TABLE_TYPE 
        FROM information_schema.TABLES 
        WHERE TABLE_SCHEMA = 'c01'
        ORDER BY TABLE_TYPE, TABLE_NAME
        """
        self.cursor.execute(query)
        return self.cursor.fetchall()

    def get_table_schema(self, table_name):
        """Get complete schema for a table"""
        # Get CREATE TABLE statement
        self.cursor.execute(f"SHOW CREATE TABLE {table_name}")
        create_stmt = self.cursor.fetchone()

        # Get columns
        self.cursor.execute(f"""
        SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE, COLUMN_DEFAULT, EXTRA,
               CHARACTER_SET_NAME, COLLATION_NAME
        FROM information_schema.COLUMNS 
        WHERE TABLE_SCHEMA = 'c01' AND TABLE_NAME = '{table_name}'
        ORDER BY ORDINAL_POSITION
        """)
        columns = self.cursor.fetchall()

        # Get indexes
        self.cursor.execute(f"SHOW INDEX FROM {table_name}")
        indexes = self.cursor.fetchall()

        # Get foreign keys
        self.cursor.execute(f"""
        SELECT 
            COLUMN_NAME, REFERENCED_TABLE_NAME, REFERENCED_COLUMN_NAME,
            CONSTRAINT_NAME
        FROM information_schema.KEY_COLUMN_USAGE
        WHERE TABLE_SCHEMA = 'c01' 
        AND TABLE_NAME = '{table_name}'
        AND REFERENCED_TABLE_NAME IS NOT NULL
        """)
        foreign_keys = self.cursor.fetchall()

        return {
            'create_statement': create_stmt,
            'columns': columns,
            'indexes': indexes,
            'foreign_keys': foreign_keys
        }

    def get_routines(self):
        """Get all stored procedures and functions"""
        query = """
        SELECT ROUTINE_NAME, ROUTINE_TYPE, ROUTINE_DEFINITION,
               DATA_TYPE, SECURITY_TYPE, SQL_DATA_ACCESS
        FROM information_schema.ROUTINES
        WHERE ROUTINE_SCHEMA = 'c01'
        """
        self.cursor.execute(query)
        return self.cursor.fetchall()

    def get_triggers(self):
        """Get all triggers"""
        query = """
        SELECT TRIGGER_NAME, EVENT_MANIPULATION, EVENT_OBJECT_TABLE,
               ACTION_STATEMENT, ACTION_TIMING
        FROM information_schema.TRIGGERS
        WHERE TRIGGER_SCHEMA = 'c01'
        """
        self.cursor.execute(query)
        return self.cursor.fetchall()

    def get_views(self):
        """Get all view definitions"""
        query = """
        SELECT TABLE_NAME, VIEW_DEFINITION
        FROM information_schema.VIEWS
        WHERE TABLE_SCHEMA = 'c01'
        """
        self.cursor.execute(query)
        return self.cursor.fetchall()

    def extract_all(self):
        """Extract all schema information"""
        output = {
            'metadata': {
                'extraction_date': datetime.now().isoformat(),
                'database': 'c01'
            },
            'tables': {},
            'views': {},
            'routines': {},
            'triggers': {}
        }

        # Extract tables and views
        tables = self.get_table_list()
        for table in tables:
            if table['TABLE_TYPE'] == 'BASE TABLE':
                output['tables'][table['TABLE_NAME']] = self.get_table_schema(table['TABLE_NAME'])
            else:
                output['views'][table['TABLE_NAME']] = self.get_table_schema(table['TABLE_NAME'])

        # Extract routines
        output['routines'] = self.get_routines()

        # Extract triggers
        output['triggers'] = self.get_triggers()

        return output

    def save_to_file(self, data, filename):
        """Save extracted schema to file"""
        with open(filename, 'w') as f:
            json.dump(data, f, indent=2, default=str)

    def close(self):
        """Close database connection"""
        self.cursor.close()
        self.connection.close()

def main():
    # Create output directory if it doesn't exist
    output_dir = os.path.join('docs', 'schema')
    os.makedirs(output_dir, exist_ok=True)

    extractor = SchemaExtractor()
    
    try:
        # Extract schema
        schema_data = extractor.extract_all()
        
        # Save to files
        timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
        
        # Save complete schema
        extractor.save_to_file(
            schema_data, 
            os.path.join(output_dir, f'complete_schema_{timestamp}.json')
        )
        
        # Save individual components
        for component in ['tables', 'views', 'routines', 'triggers']:
            extractor.save_to_file(
                schema_data[component],
                os.path.join(output_dir, f'{component}_{timestamp}.json')
            )
            
        print(f"Schema extraction complete. Files saved in {output_dir}")
        
    finally:
        extractor.close()

if __name__ == "__main__":
    main()
