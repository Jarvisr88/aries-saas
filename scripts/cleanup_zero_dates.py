"""
Script to clean up zero dates in MySQL database before PostgreSQL migration.
Replaces zero dates (0000-00-00) with NULL or appropriate default values.
"""

import json
import logging
import mysql.connector
from mysql.connector.cursor import MySQLCursor
from mysql.connector.connection import MySQLConnection
from mysql.connector.pooling import PooledMySQLConnection
from mysql.connector.abstracts import MySQLConnectionAbstract, MySQLCursorAbstract
from datetime import datetime
from typing import Dict, List, Tuple, Optional, Any, cast, Union

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# SQL to find zero dates
FIND_ZERO_DATES_SQL = """
SELECT t.TABLE_NAME, t.COLUMN_NAME, COUNT(*) as zero_count
FROM INFORMATION_SCHEMA.COLUMNS t
JOIN information_schema.tables bt 
    ON t.TABLE_NAME = bt.table_name 
    AND t.TABLE_SCHEMA = bt.table_schema
WHERE t.TABLE_SCHEMA = %s
AND t.DATA_TYPE IN ('date', 'datetime', 'timestamp')
AND bt.table_type = 'BASE TABLE'
AND bt.table_name NOT LIKE 'bak_%'
AND EXISTS (
    SELECT 1 
    FROM information_schema.tables it
    WHERE it.table_schema = t.TABLE_SCHEMA
    AND it.table_name = t.TABLE_NAME
    AND it.table_type = 'BASE TABLE'
)
GROUP BY t.TABLE_NAME, t.COLUMN_NAME
HAVING COUNT(CASE 
    WHEN t.COLUMN_DEFAULT = '0000-00-00' 
    OR t.COLUMN_DEFAULT = '0000-00-00 00:00:00' 
    THEN 1 
END) > 0;
"""

class ZeroDateCleaner:
    def __init__(self, config_path: str = 'config/database.json'):
        """Initialize the cleaner with database configuration."""
        self.config = self._load_config(config_path)
        self.conn: Optional[Union[MySQLConnection, PooledMySQLConnection, MySQLConnectionAbstract]] = None
        self.cursor: Optional[Union[MySQLCursor, MySQLCursorAbstract]] = None
        self.zero_dates: List[Tuple[str, str, int]] = []
        
    def _load_config(self, config_path: str) -> Dict[str, Any]:
        """Load database configuration from JSON file."""
        with open(config_path, 'r') as f:
            return json.load(f)['mysql']
            
    def connect(self) -> None:
        """Establish database connection."""
        try:
            self.conn = mysql.connector.connect(
                host=self.config['host'],
                user=self.config['user'],
                password=self.config['password'],
                database=self.config['database']
            )
            if self.conn is None:
                raise RuntimeError("Failed to establish database connection")
                
            self.cursor = self.conn.cursor()
            if self.cursor is None:
                raise RuntimeError("Failed to create database cursor")
            
            if self.cursor is None:
                raise RuntimeError("Cursor is not initialized")
            
            # Allow zero dates temporarily
            self.cursor.execute("SET SESSION sql_mode = ''")
            
            logger.info("Connected to MySQL database")
        except Exception as e:
            logger.error(f"Failed to connect to database: {str(e)}")
            raise
            
    def find_zero_dates(self) -> None:
        """Find all columns containing zero dates."""
        if self.cursor is None:
            raise RuntimeError("Database cursor is not initialized")
            
        try:
            self.cursor.execute(FIND_ZERO_DATES_SQL, (self.config['database'],))
            result = self.cursor.fetchall()
            self.zero_dates = cast(List[Tuple[str, str, int]], result)
            logger.info(f"Found {len(self.zero_dates)} columns with zero dates")
        except Exception as e:
            logger.error(f"Error finding zero dates: {str(e)}")
            raise
            
    def _get_default_value(self, table: str, column: str) -> str:
        """
        Get appropriate default value based on column context.
        Returns either 'NULL' or a specific date string.
        """
        # Map of columns to their default values
        defaults = {
            'DateofBirth': 'NULL',  # Birth dates should be NULL if unknown
            'SignatureOnFile': 'NULL',  # Signature dates should be NULL if not signed
            'SetupDate': "'1970-01-01'",  # Use Unix epoch for setup dates
            'LicenseExpired': "'9999-12-31'",  # Use far future for non-expired licenses
            'LastUpdateDatetime': 'CURRENT_TIMESTAMP',  # Use current time for last updates
            'LastContacted': 'NULL'  # NULL for no contact
        }
        return defaults.get(column, 'NULL')
            
    def _get_backup_table_name(self, table: str, column: str) -> str:
        """Generate a unique but shorter backup table name."""
        # Use first 3 chars of table name and column name
        table_prefix = table[:3]
        col_prefix = column[:3]
        # Add a hash of the full names to ensure uniqueness
        hash_suffix = abs(hash(f"{table}_{column}")) % 1000
        return f"bak_{table_prefix}_{col_prefix}_{hash_suffix}"

    def cleanup_zero_dates(self) -> None:
        """Clean up zero dates in all affected columns."""
        if self.cursor is None:
            raise RuntimeError("Database cursor is not initialized")
        if self.conn is None:
            raise RuntimeError("Database connection is not initialized")

        try:
            # First, create a mapping of backup tables
            backup_mapping = {}
            for table, column, count in self.zero_dates:
                backup_table = self._get_backup_table_name(table, column)
                backup_mapping[f"{table}.{column}"] = backup_table
            
            # Create a record of backup table names
            with open('docs/migration/backup_tables_mapping.txt', 'w') as f:
                for original, backup in backup_mapping.items():
                    f.write(f"{backup} -> {original}\n")
            
            for table, column, _ in self.zero_dates:
                default_value = self._get_default_value(table, column)
                backup_table = backup_mapping[f"{table}.{column}"]
                logger.info(f"Processing {table}.{column}")
                
                # Drop backup table if it exists
                self.cursor.execute(f"DROP TABLE IF EXISTS {backup_table}")
                
                # Create backup table with same structure as original
                backup_sql = f"""
                CREATE TABLE {backup_table} 
                SELECT * FROM {table} WHERE 1=0;
                """
                self.cursor.execute(backup_sql)
                
                # Add metadata columns
                alter_sql = f"""
                ALTER TABLE {backup_table}
                ADD COLUMN backup_id INT AUTO_INCREMENT PRIMARY KEY FIRST,
                ADD COLUMN backup_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP;
                """
                self.cursor.execute(alter_sql)
                
                # Copy data to backup
                insert_sql = f"""
                INSERT INTO {backup_table} 
                SELECT NULL, CURRENT_TIMESTAMP, t.* 
                FROM {table} t
                WHERE {column} = '0000-00-00' OR {column} = '0000-00-00 00:00:00';
                """
                self.cursor.execute(insert_sql)
                
                # Get count of backed up rows
                self.cursor.execute(f"SELECT COUNT(*) FROM {backup_table}")
                result = self.cursor.fetchone()
                if result is None:
                    backup_count = 0
                else:
                    backup_count = result[0] if isinstance(result, tuple) else 0
                logger.info(f"Backed up {backup_count} rows to {backup_table}")
                
                # Update the zero dates
                update_sql = f"""
                UPDATE {table} 
                SET {column} = {default_value}
                WHERE {column} = '0000-00-00' OR {column} = '0000-00-00 00:00:00';
                """
                self.cursor.execute(update_sql)
                logger.info(f"Updated zero dates in {table}.{column}")
            
            # Now add constraints to all affected tables
            constraint_failures = []
            for table, column, _ in self.zero_dates:
                constraint_name = f"chk_{table}_{column}_no_zero_dates"
                
                # Drop constraint if it exists (ignore errors)
                try:
                    self.cursor.execute(f"ALTER TABLE {table} DROP CONSTRAINT {constraint_name}")
                except:
                    pass
                
                # Add new constraint
                alter_sql = f"""
                ALTER TABLE {table} 
                ADD CONSTRAINT {constraint_name}
                CHECK ({column} IS NULL OR {column} > '1000-01-01');
                """
                try:
                    self.cursor.execute(alter_sql)
                    logger.info(f"Added constraint to {table}.{column}")
                except Exception as e:
                    constraint_failures.append(f"{table}.{column}: {str(e)}")
                    logger.warning(f"Could not add constraint to {table}.{column}: {str(e)}")
            
            self.conn.commit()
            
            if constraint_failures:
                logger.warning("Some constraints could not be added:")
                for failure in constraint_failures:
                    logger.warning(f"  - {failure}")
            
            logger.info("Successfully cleaned up all zero dates")
            
        except Exception as e:
            if self.conn is not None:
                self.conn.rollback()
            logger.error(f"Error cleaning up zero dates: {str(e)}")
            raise
            
    def generate_report(self) -> None:
        """Generate a report of changes made."""
        report = []
        report.append("# Zero Dates Cleanup Report")
        report.append(f"\nGenerated at: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
        
        for table, column, count in self.zero_dates:
            default_value = self._get_default_value(table, column)
            report.append(f"\n## {table}.{column}")
            report.append(f"- Found {count} zero dates")
            report.append(f"- Replaced with: {default_value}")
            report.append(f"- Backup table created: {self._get_backup_table_name(table, column)}")
            
        with open('docs/migration/zero_dates_cleanup_report.md', 'w') as f:
            f.write('\n'.join(report))
            
    def close(self) -> None:
        """Close database connection."""
        if self.cursor:
            self.cursor.close()
        if self.conn:
            self.conn.close()
            logger.info("Closed database connection")
            
def main():
    cleaner = ZeroDateCleaner()
    try:
        cleaner.connect()
        cleaner.find_zero_dates()
        cleaner.cleanup_zero_dates()
        cleaner.generate_report()
    finally:
        cleaner.close()

if __name__ == '__main__':
    main()
