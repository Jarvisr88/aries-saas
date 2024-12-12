"""
Script to update stored procedures for PostgreSQL compatibility.
Converts non-deterministic procedures and updates syntax.
"""

import json
from typing import Dict, List, Optional, Tuple, Any
import logging
import psycopg2
from psycopg2.extensions import connection, cursor
from datetime import datetime
import os

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# SQL to get stored procedures
GET_PROCEDURES_SQL = """
SELECT 
    ROUTINE_NAME,
    ROUTINE_DEFINITION,
    IS_DETERMINISTIC,
    DATA_TYPE,
    SECURITY_TYPE,
    SQL_DATA_ACCESS
FROM information_schema.routines
WHERE ROUTINE_SCHEMA = %s
AND ROUTINE_TYPE = 'PROCEDURE'
ORDER BY ROUTINE_NAME;
"""

class StoredProcedureConverter:
    def __init__(self, config_path: str = 'config/database.json'):
        """Initialize the converter with database configuration."""
        self.config: Dict[str, Any] = self._load_config(config_path)
        self.conn: Optional[connection] = None
        self.cursor: Optional[cursor] = None
        self.procedures: List[Tuple[Any, ...]] = []
        
    def _load_config(self, config_path: str) -> Dict:
        """Load database configuration from JSON file."""
        with open(config_path, 'r') as f:
            return json.load(f)['mysql']
            
    def connect(self) -> None:
        """Establish database connection."""
        try:
            self.conn = psycopg2.connect(
                host=self.config['host'],
                user=self.config['user'],
                password=self.config['password'],
                database=self.config['database']
            )
            self.cursor = self.conn.cursor()
            logger.info("Connected to PostgreSQL database")
        except Exception as e:
            logger.error(f"Failed to connect to database: {str(e)}")
            raise
            
    def get_procedures(self) -> None:
        """Get all stored procedures from the database."""
        try:
            if self.cursor is None:
                raise RuntimeError("Database cursor is not initialized")
            self.cursor.execute(GET_PROCEDURES_SQL, (self.config['database'],))
            result = self.cursor.fetchall()
            if result is not None:
                self.procedures = list(result)  # Convert to List[Tuple]
            else:
                self.procedures = []
            logger.info(f"Found {len(self.procedures)} stored procedures")
        except Exception as e:
            logger.error(f"Error getting stored procedures: {str(e)}")
            raise
            
    def _convert_mysql_function(self, func_name: str) -> str:
        """Convert MySQL function to PostgreSQL equivalent."""
        conversions = {
            'IFNULL': 'COALESCE',
            'CONCAT_WS': 'CONCAT_WS',  # Same in both
            'DATE_FORMAT': 'TO_CHAR',
            'STR_TO_DATE': 'TO_DATE',
            'NOW()': 'CURRENT_TIMESTAMP',
            'CURDATE()': 'CURRENT_DATE',
            'LAST_INSERT_ID()': 'lastval()',
            'FOUND_ROWS()': '(SELECT count(*) FROM _found_rows)',
            'GROUP_CONCAT': 'STRING_AGG',
        }
        return conversions.get(func_name, func_name)
            
    def _convert_data_type(self, mysql_type: str) -> str:
        """Convert MySQL data type to PostgreSQL equivalent."""
        type_map = {
            'TINYINT': 'SMALLINT',
            'SMALLINT': 'SMALLINT',
            'MEDIUMINT': 'INTEGER',
            'INT': 'INTEGER',
            'BIGINT': 'BIGINT',
            'FLOAT': 'REAL',
            'DOUBLE': 'DOUBLE PRECISION',
            'DECIMAL': 'DECIMAL',
            'DATETIME': 'TIMESTAMP',
            'TIMESTAMP': 'TIMESTAMP',
            'DATE': 'DATE',
            'TIME': 'TIME',
            'YEAR': 'INTEGER',
            'CHAR': 'CHAR',
            'VARCHAR': 'VARCHAR',
            'TINYTEXT': 'TEXT',
            'TEXT': 'TEXT',
            'MEDIUMTEXT': 'TEXT',
            'LONGTEXT': 'TEXT',
            'ENUM': 'VARCHAR',
            'SET': 'VARCHAR[]',
            'BINARY': 'BYTEA',
            'VARBINARY': 'BYTEA',
            'TINYBLOB': 'BYTEA',
            'BLOB': 'BYTEA',
            'MEDIUMBLOB': 'BYTEA',
            'LONGBLOB': 'BYTEA',
            'BIT': 'BIT',
        }
        return type_map.get(mysql_type.upper(), mysql_type)
            
    def _convert_procedure_body(self, body: str) -> str:
        """Convert MySQL procedure body to PostgreSQL syntax."""
        # Replace MySQL-specific syntax
        replacements = [
            ('DECLARE', 'DECLARE'),  # Same in both
            ('SET @', 'SET'),  # Remove @ for variables
            ('SELECT @', 'SELECT'),  # Remove @ for variables
            ('DELIMITER ;;', ''),  # Remove DELIMITER
            ('END;;', 'END;'),  # Fix end delimiter
            ('BEGIN', 'BEGIN'),  # Same in both
            ('END', 'END'),  # Same in both
            ('IF NOT EXISTS', 'IF NOT EXISTS'),  # Same in both
            ('IF EXISTS', 'IF EXISTS'),  # Same in both
            ('LIMIT 1', 'LIMIT 1'),  # Same in both
            ('AUTO_INCREMENT', 'SERIAL'),  # Replace auto_increment
            ('UNSIGNED', ''),  # Remove unsigned
            ('TRUE', 'true'),  # Lowercase boolean
            ('FALSE', 'false'),  # Lowercase boolean
        ]
        
        result = body
        for old, new in replacements:
            result = result.replace(old, new)
            
        # Convert function calls
        for func in ['IFNULL', 'CONCAT_WS', 'DATE_FORMAT', 'STR_TO_DATE', 'NOW()', 'CURDATE()', 'LAST_INSERT_ID()', 'FOUND_ROWS()', 'GROUP_CONCAT']:
            if func in result:
                result = result.replace(func, self._convert_mysql_function(func))
                
        return result
            
    def convert_procedures(self) -> None:
        """Convert all stored procedures to PostgreSQL syntax."""
        try:
            converted = []
            for name, body, is_deterministic, return_type, security, data_access in self.procedures:
                logger.info(f"Converting procedure: {name}")
                
                # Convert the procedure body
                pg_body = self._convert_procedure_body(body)
                
                # Store the conversion
                converted.append({
                    'name': name,
                    'original': body,
                    'converted': pg_body,
                    'is_deterministic': is_deterministic,
                    'return_type': return_type,
                    'security': security,
                    'data_access': data_access
                })
                
            # Generate conversion report
            with open('docs/migration/stored_procedures_conversion.md', 'w') as f:
                f.write('# Stored Procedures Conversion Report\n\n')
                f.write(f"Generated at: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n\n")
                
                for proc in converted:
                    f.write(f"## {proc['name']}\n\n")
                    f.write("### Original MySQL Procedure\n")
                    f.write("```sql\n")
                    f.write(proc['original'])
                    f.write("\n```\n\n")
                    f.write("### Converted PostgreSQL Procedure\n")
                    f.write("```sql\n")
                    f.write(proc['converted'])
                    f.write("\n```\n\n")
                    f.write("### Metadata\n")
                    f.write(f"- Deterministic: {proc['is_deterministic']}\n")
                    f.write(f"- Return Type: {proc['return_type']}\n")
                    f.write(f"- Security Type: {proc['security']}\n")
                    f.write(f"- Data Access: {proc['data_access']}\n\n")
                    
            # Generate PostgreSQL migration script
            with open('scripts/migration/convert_procedures.sql', 'w') as f:
                for proc in converted:
                    f.write(f"-- Drop existing procedure if it exists\n")
                    f.write(f"DROP PROCEDURE IF EXISTS {proc['name']};\n\n")
                    f.write(f"-- Create converted procedure\n")
                    f.write(proc['converted'])
                    f.write("\n\n")
                    
            logger.info(f"Successfully converted {len(converted)} procedures")
            logger.info("Generated conversion report: docs/migration/stored_procedures_conversion.md")
            logger.info("Generated migration script: scripts/migration/convert_procedures.sql")
            
        except Exception as e:
            logger.error(f"Error converting procedures: {str(e)}")
            raise
            
    def close(self) -> None:
        """Close database connection."""
        if self.cursor:
            self.cursor.close()
        if self.conn:
            self.conn.close()
            logger.info("Closed database connection")
            
def main():
    converter = StoredProcedureConverter()
    try:
        converter.connect()
        converter.get_procedures()
        converter.convert_procedures()
    finally:
        converter.close()

if __name__ == '__main__':
    main()
