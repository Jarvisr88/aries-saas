import json
import logging
from pathlib import Path
import psycopg2
from psycopg2.extensions import ISOLATION_LEVEL_AUTOCOMMIT
import time
from typing import Dict, List, Optional
import os

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

def get_config():
    """Get database configuration"""
    return {
        'host': 'ep-frosty-math-a82fyke1.eastus2.azure.neon.tech',
        'database': 'Aries-Saas',
        'user': 'neondb_owner',
        'password': 'hD1GCdJxU2Io',
        'sslmode': 'require'
    }

def get_connection_params(config: Dict) -> Dict:
    """Get connection parameters with SSL settings"""
    return {
        'host': config['host'],
        'user': config['user'],
        'password': config['password'],
        'dbname': config['database'],
        'sslmode': config['sslmode']
    }

def run_migration_scripts(config: Dict) -> None:
    """Run all migration scripts in order"""
    logger.info("Running migration scripts...")
    
    try:
        conn = psycopg2.connect(
            host=config['host'],
            database=config['database'],
            user=config['user'],
            password=config['password'],
            sslmode=config['sslmode']
        )
        conn.autocommit = True
        cursor = conn.cursor()
        
        # Run schema_v2.sql first
        logger.info("Running migration script: schema_v2.sql")
        schema_path = os.path.join('scripts', 'migration', 'schema_v2.sql')
        with open(schema_path, 'r', encoding='utf-8') as f:
            sql = f.read()
            try:
                cursor.execute(sql)
                logger.info("Successfully executed schema_v2.sql")
            except Exception as e:
                logger.error(f"Error running schema_v2.sql: {str(e)}")
                raise
        
        cursor.close()
        conn.close()
        logger.info("Migration completed successfully")
        
    except Exception as e:
        logger.error(f"Migration failed: {str(e)}")
        raise

def validate_schema(config: Dict) -> List[str]:
    """Validate the migrated schema"""
    issues = []
    
    conn = psycopg2.connect(**get_connection_params(config))
    cursor = conn.cursor()
    
    # Check tables exist
    cursor.execute("""
        SELECT table_name 
        FROM information_schema.tables 
        WHERE table_schema = 'public'
    """)
    tables = [row[0] for row in cursor.fetchall()]
    
    if not tables:
        issues.append("No tables found in the database")
    else:
        logger.info(f"Found {len(tables)} tables")
    
    # For each table, check structure
    for table in tables:
        # Check columns
        cursor.execute("""
            SELECT column_name, data_type, is_nullable
            FROM information_schema.columns
            WHERE table_name = %s
        """, (table,))
        columns = cursor.fetchall()
        
        if not columns:
            issues.append(f"No columns found in table {table}")
        else:
            logger.info(f"Table {table} has {len(columns)} columns")
        
        # Check constraints
        cursor.execute("""
            SELECT constraint_name, constraint_type
            FROM information_schema.table_constraints
            WHERE table_name = %s
        """, (table,))
        constraints = cursor.fetchall()
        
        if not any(c[1] == 'PRIMARY KEY' for c in constraints):
            issues.append(f"No primary key found for table {table}")
    
    cursor.close()
    conn.close()
    
    return issues

def test_stored_procedures(config: Dict) -> List[str]:
    """Test all stored procedures"""
    issues = []
    
    conn = psycopg2.connect(**get_connection_params(config))
    cursor = conn.cursor()
    
    # Get all procedures
    cursor.execute("""
        SELECT routine_name
        FROM information_schema.routines
        WHERE routine_type = 'PROCEDURE'
    """)
    
    procedures = [row[0] for row in cursor.fetchall()]
    
    if procedures:
        logger.info(f"Found {len(procedures)} stored procedures")
    else:
        logger.info("No stored procedures found")
    
    for proc in procedures:
        try:
            # Try to call procedure with NULL parameters
            cursor.execute(f"CALL {proc}()")
            conn.commit()
            logger.info(f"Successfully tested procedure {proc}")
        except Exception as e:
            issues.append(f"Error testing procedure {proc}: {str(e)}")
            conn.rollback()
    
    cursor.close()
    conn.close()
    
    return issues

def main():
    config = get_config()
    logger.info("Starting database migration and validation")
    
    try:
        # Run migration scripts
        logger.info("Running migration scripts...")
        run_migration_scripts(config)
        
        # Validate schema
        logger.info("Validating schema...")
        schema_issues = validate_schema(config)
        
        # Test stored procedures
        logger.info("Testing stored procedures...")
        proc_issues = test_stored_procedures(config)
        
        # Write validation report
        report_path = Path(__file__).parent.parent / 'docs' / 'migration' / 'test_validation.md'
        report_path.parent.mkdir(parents=True, exist_ok=True)
        
        with open(report_path, 'w') as f:
            f.write("# Migration Test Validation Report\n\n")
            
            f.write("## Schema Validation\n\n")
            if schema_issues:
                f.write("Issues found:\n\n")
                for issue in schema_issues:
                    f.write(f"- {issue}\n")
            else:
                f.write("No schema issues found.\n")
            
            f.write("\n## Stored Procedures Testing\n\n")
            if proc_issues:
                f.write("Issues found:\n\n")
                for issue in proc_issues:
                    f.write(f"- {issue}\n")
            else:
                f.write("No stored procedure issues found.\n")
        
        logger.info(f"Validation report written to: {report_path}")
        
    except Exception as e:
        logger.error(f"Migration failed: {str(e)}")
        raise

if __name__ == '__main__':
    main()
