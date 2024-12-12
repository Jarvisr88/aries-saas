"""
Migration Readiness Verification Script
Checks if we have all necessary information for a successful MySQL to PostgreSQL migration
"""

import mysql.connector
import json
import logging
from datetime import datetime
from typing import Dict, List, Set
import os

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class MigrationVerifier:
    def __init__(self, mysql_config: Dict):
        self.mysql_conn = mysql.connector.connect(**mysql_config)
        self.mysql_cursor = self.mysql_conn.cursor(dictionary=True)
        self.issues = []
        self.warnings = []

    def check_schema_objects(self) -> None:
        """Check all schema objects"""
        logger.info("Checking schema objects...")
        
        # Get tables
        self.mysql_cursor.execute("""
            SELECT TABLE_NAME, TABLE_TYPE, ENGINE, TABLE_COLLATION
            FROM information_schema.TABLES 
            WHERE TABLE_SCHEMA = 'c01'
        """)
        tables = self.mysql_cursor.fetchall()
        logger.info(f"Found {len(tables)} tables/views")
        
        # Check for unsupported engines
        unsupported_engines = set()
        for table in tables:
            if table['ENGINE'] not in (None, 'InnoDB', 'MyISAM'):
                unsupported_engines.add(table['ENGINE'])
                self.issues.append(f"Table {table['TABLE_NAME']} uses unsupported engine: {table['ENGINE']}")

    def check_data_types(self) -> None:
        """Check for problematic data types"""
        logger.info("Checking data types...")
        
        self.mysql_cursor.execute("""
            SELECT TABLE_NAME, COLUMN_NAME, COLUMN_TYPE, DATA_TYPE
            FROM information_schema.COLUMNS 
            WHERE TABLE_SCHEMA = 'c01'
        """)
        columns = self.mysql_cursor.fetchall()
        
        problematic_types = set()
        for col in columns:
            dtype = col['DATA_TYPE'].lower()
            if dtype in ('geometry', 'point', 'linestring', 'polygon'):
                self.issues.append(f"Spatial data type in {col['TABLE_NAME']}.{col['COLUMN_NAME']}: {col['COLUMN_TYPE']}")
            elif dtype == 'bit' and 'bit(1)' not in col['COLUMN_TYPE'].lower():
                self.issues.append(f"Multi-bit field in {col['TABLE_NAME']}.{col['COLUMN_NAME']}: {col['COLUMN_TYPE']}")
            elif dtype == 'year':
                self.warnings.append(f"YEAR type in {col['TABLE_NAME']}.{col['COLUMN_NAME']} will be converted to INTEGER")

    def check_constraints(self) -> None:
        """Check constraints and keys"""
        logger.info("Checking constraints...")
        
        # Check foreign keys
        self.mysql_cursor.execute("""
            SELECT TABLE_NAME, CONSTRAINT_NAME, REFERENCED_TABLE_NAME
            FROM information_schema.KEY_COLUMN_USAGE
            WHERE TABLE_SCHEMA = 'c01'
            AND REFERENCED_TABLE_NAME IS NOT NULL
        """)
        fks = self.mysql_cursor.fetchall()
        logger.info(f"Found {len(fks)} foreign key constraints")
        
        # Check for circular references
        for fk in fks:
            if fk['TABLE_NAME'] == fk['REFERENCED_TABLE_NAME']:
                self.warnings.append(f"Self-referential foreign key in {fk['TABLE_NAME']}: {fk['CONSTRAINT_NAME']}")

    def check_stored_procedures(self) -> None:
        """Check stored procedures and functions"""
        logger.info("Checking stored procedures...")
        
        self.mysql_cursor.execute("""
            SELECT ROUTINE_NAME, ROUTINE_TYPE, SQL_DATA_ACCESS
            FROM information_schema.ROUTINES
            WHERE ROUTINE_SCHEMA = 'c01'
        """)
        routines = self.mysql_cursor.fetchall()
        logger.info(f"Found {len(routines)} stored routines")
        
        for routine in routines:
            # Get routine body
            self.mysql_cursor.execute(f"SHOW CREATE {routine['ROUTINE_TYPE']} {routine['ROUTINE_NAME']}")
            body = self.mysql_cursor.fetchone()
            if body:
                if 'DETERMINISTIC' not in str(body).upper():
                    self.warnings.append(f"Non-deterministic routine: {routine['ROUTINE_NAME']}")

    def check_triggers(self) -> None:
        """Check triggers"""
        logger.info("Checking triggers...")
        
        self.mysql_cursor.execute("""
            SELECT TRIGGER_NAME, EVENT_MANIPULATION, ACTION_STATEMENT
            FROM information_schema.TRIGGERS
            WHERE TRIGGER_SCHEMA = 'c01'
        """)
        triggers = self.mysql_cursor.fetchall()
        logger.info(f"Found {len(triggers)} triggers")
        
        for trigger in triggers:
            if 'NEW.' in trigger['ACTION_STATEMENT'] or 'OLD.' in trigger['ACTION_STATEMENT']:
                self.warnings.append(f"Trigger {trigger['TRIGGER_NAME']} uses row-level references")

    def check_views(self) -> None:
        """Check views"""
        logger.info("Checking views...")
        
        self.mysql_cursor.execute("""
            SELECT TABLE_NAME, VIEW_DEFINITION
            FROM information_schema.VIEWS
            WHERE TABLE_SCHEMA = 'c01'
        """)
        views = self.mysql_cursor.fetchall()
        logger.info(f"Found {len(views)} views")
        
        for view in views:
            if 'DEFINER' in view['VIEW_DEFINITION']:
                self.warnings.append(f"View {view['TABLE_NAME']} has DEFINER clause")

    def check_data_values(self) -> None:
        """Check for problematic data values"""
        logger.info("Checking data values...")
        
        # Check for zero dates
        self.mysql_cursor.execute("""
            SELECT TABLE_NAME, COLUMN_NAME
            FROM information_schema.COLUMNS 
            WHERE TABLE_SCHEMA = 'c01'
            AND DATA_TYPE IN ('date', 'datetime', 'timestamp')
        """)
        date_columns = self.mysql_cursor.fetchall()
        
        for col in date_columns:
            query = f"""
                SELECT COUNT(*) as count
                FROM {col['TABLE_NAME']}
                WHERE {col['COLUMN_NAME']} = '0000-00-00'
                   OR {col['COLUMN_NAME']} = '0000-00-00 00:00:00'
            """
            self.mysql_cursor.execute(query)
            result = self.mysql_cursor.fetchone()
            if result and result['count'] > 0:
                self.issues.append(f"Found {result['count']} zero dates in {col['TABLE_NAME']}.{col['COLUMN_NAME']}")

    def generate_report(self) -> None:
        """Generate migration readiness report"""
        timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
        
        # Create output directory
        os.makedirs('docs/migration', exist_ok=True)
        
        with open(f'docs/migration/migration_readiness_{timestamp}.md', 'w', encoding='utf-8') as f:
            f.write('# Migration Readiness Report\n\n')
            f.write(f'Generated at: {timestamp}\n\n')
            
            if self.issues:
                f.write('## Critical Issues\n\n')
                for issue in self.issues:
                    f.write(f'- [CRITICAL] {issue}\n')
                f.write('\n')
            
            if self.warnings:
                f.write('## Warnings\n\n')
                for warning in self.warnings:
                    f.write(f'- [WARNING] {warning}\n')
                f.write('\n')
            
            if not self.issues and not self.warnings:
                f.write('## Status\n\n')
                f.write('[SUCCESS] No issues or warnings found. Ready for migration!\n')

    def verify_all(self) -> None:
        """Run all verification checks"""
        try:
            self.check_schema_objects()
            self.check_data_types()
            self.check_constraints()
            self.check_stored_procedures()
            self.check_triggers()
            self.check_views()
            self.check_data_values()
            
            # Print results to console
            print("\n=== Migration Readiness Report ===\n")
            
            if self.issues:
                print("Critical Issues:")
                for issue in self.issues:
                    print(f"  - [CRITICAL] {issue}")
                print()
            
            if self.warnings:
                print("Warnings:")
                for warning in self.warnings:
                    print(f"  - [WARNING] {warning}")
                print()
            
            if not self.issues and not self.warnings:
                print("[SUCCESS] No issues or warnings found. Ready for migration!")
            
            # Also generate the markdown report
            self.generate_report()
            
            if self.issues:
                logger.error(f"Found {len(self.issues)} critical issues")
            if self.warnings:
                logger.warning(f"Found {len(self.warnings)} warnings")
            if not self.issues and not self.warnings:
                logger.info("No issues found - ready for migration!")
                
        except Exception as e:
            logger.error(f"Error during verification: {str(e)}")
            raise

    def close(self):
        """Close database connections"""
        self.mysql_cursor.close()
        self.mysql_conn.close()

def main():
    # Load configuration
    config_path = os.path.join('config', 'database.json')
    with open(config_path) as f:
        config = json.load(f)
    
    verifier = MigrationVerifier(config['mysql'])
    try:
        verifier.verify_all()
    finally:
        verifier.close()

if __name__ == "__main__":
    main()
