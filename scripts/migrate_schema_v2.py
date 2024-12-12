import psycopg2
import json
import logging
from pathlib import Path

# Set up logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

def load_config() -> dict:
    """
    Load database configuration from JSON file.
    Returns:
        dict: Configuration data.
    """
    config_path = Path(__file__).parent.parent / 'config' / 'database.json'
    if not config_path.exists():
        raise FileNotFoundError(f"Configuration file not found: {config_path}")
    with open(config_path, 'r') as f:
        return json.load(f)

def execute_schema_file(connection, schema_file: Path):
    """
    Executes a given SQL schema file.
    Args:
        connection: Active PostgreSQL connection.
        schema_file (Path): Path to the SQL schema file.
    """
    logger.info(f"Executing schema file: {schema_file}")
    if not schema_file.exists():
        logger.error(f"Schema file {schema_file} does not exist.")
        return
    
    cursor = connection.cursor()
    try:
        # Read and execute the schema file
        with open(schema_file, 'r') as f:
            sql = f.read()
            statements = sql.split(';')  # Split into individual statements
            total_statements = len([s for s in statements if s.strip()])
            logger.info(f"Found {total_statements} SQL statements to execute")
            
            for i, statement in enumerate(statements, 1):
                if statement.strip():  # Skip empty statements
                    try:
                        logger.info(f"[{i}/{total_statements}] Executing: {statement.strip()[:100]}...")
                        cursor.execute(statement)
                        logger.info(f"Statement {i} executed successfully")
                    except Exception as e:
                        logger.error(f"Error in statement {i}: {str(e)}")
                        logger.error(f"Problematic SQL: {statement.strip()}")
                        raise
        
        connection.commit()
        logger.info("Schema execution completed successfully")
    except Exception as e:
        connection.rollback()
        logger.error(f"Error executing schema: {str(e)}")
        raise
    finally:
        cursor.close()

def main():
    """
    Main function to load configuration, connect to PostgreSQL, and execute the schema file.
    """
    try:
        # Load database configuration
        config = load_config()
        pg_config = config.get('postgresql', {})
        required_keys = ['host', 'dbname', 'user', 'password']  # Changed 'database' to 'dbname'
        
        # Validate configuration
        for key in required_keys:
            if key not in pg_config:
                raise KeyError(f"Missing required PostgreSQL config key: {key}")

        # Connect to PostgreSQL
        logger.info("Connecting to PostgreSQL database...")
        connection_params = {
            'host': pg_config['host'],
            'dbname': pg_config['dbname'],  # Using dbname instead of database
            'user': pg_config['user'],
            'password': pg_config['password'],
            'sslmode': pg_config.get('sslmode', 'require')
        }
        
        logger.info(f"Connecting to {connection_params['host']} as {connection_params['user']}")
        with psycopg2.connect(**connection_params) as connection:
            # Execute schema file
            schema_file = Path(__file__).parent / 'migration' / 'schema_v2.sql'
            execute_schema_file(connection, schema_file)

    except FileNotFoundError as e:
        logger.error(f"Configuration error: {str(e)}")
    except KeyError as e:
        logger.error(f"Configuration error: {str(e)}")
    except psycopg2.Error as e:
        logger.error(f"Database error: {str(e)}")
    except Exception as e:
        logger.error(f"Unexpected error: {str(e)}")
    finally:
        logger.info("Script execution completed.")

if __name__ == '__main__':
    main()
