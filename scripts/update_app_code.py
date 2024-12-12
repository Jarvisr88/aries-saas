import os
import re
import logging
from pathlib import Path
from typing import Dict, List, Set

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

# Patterns to identify MySQL-specific code
MYSQL_PATTERNS = {
    'last_insert_id': r'LAST_INSERT_ID\(\)',
    'limit_offset': r'LIMIT\s+\d+(?:\s*,\s*\d+)?',
    'on_duplicate': r'ON\s+DUPLICATE\s+KEY\s+UPDATE',
    'force_index': r'FORCE\s+INDEX',
    'straight_join': r'STRAIGHT_JOIN',
    'mysql_functions': r'\b(IFNULL|CONCAT_WS|GROUP_CONCAT|FIND_IN_SET)\b',
}

# PostgreSQL replacements
PG_REPLACEMENTS = {
    'LAST_INSERT_ID()': 'lastval()',
    'IFNULL': 'COALESCE',
    'CONCAT_WS': 'array_to_string',
    'GROUP_CONCAT': 'string_agg',
    'FIND_IN_SET': '= ANY(string_to_array',
}

def find_python_files(start_path: Path) -> List[Path]:
    """Find all Python files in the project"""
    python_files = []
    for root, _, files in os.walk(start_path):
        for file in files:
            if file.endswith('.py'):
                python_files.append(Path(root) / file)
    return python_files

def analyze_file(file_path: Path) -> Dict[str, List[int]]:
    """Analyze a file for MySQL-specific code"""
    issues = {}
    
    with open(file_path) as f:
        lines = f.readlines()
        
    for pattern_name, pattern in MYSQL_PATTERNS.items():
        for i, line in enumerate(lines, 1):
            if re.search(pattern, line, re.IGNORECASE):
                if pattern_name not in issues:
                    issues[pattern_name] = []
                issues[pattern_name].append(i)
    
    return issues

def update_connection_strings(file_path: Path) -> None:
    """Update database connection strings to use psycopg2"""
    with open(file_path) as f:
        content = f.read()
    
    # Replace MySQL-python imports
    content = re.sub(
        r'import MySQLdb|import mysql.connector',
        'import psycopg2',
        content
    )
    
    # Replace connection parameters
    content = re.sub(
        r'MySQLdb.connect\((.*?)\)',
        lambda m: update_connect_params(m.group(1)),
        content
    )
    
    with open(file_path, 'w') as f:
        f.write(content)

def update_connect_params(params: str) -> str:
    """Convert MySQL connection parameters to PostgreSQL format"""
    # This is a simplified version - you'll need to handle more cases
    params = params.replace('db=', 'database=')
    params = params.replace('passwd=', 'password=')
    return f'psycopg2.connect({params})'

def update_orm_config(config_path: Path) -> None:
    """Update ORM configuration for PostgreSQL"""
    if not config_path.exists():
        return
    
    with open(config_path) as f:
        content = f.read()
    
    # Update database engine
    content = re.sub(
        r"'ENGINE':\s*'.*mysql.*'",
        "'ENGINE': 'django.db.backends.postgresql'",
        content
    )
    
    # Update other database settings
    content = re.sub(
        r"'OPTIONS':\s*{[^}]*}",
        "'OPTIONS': {'client_encoding': 'UTF8'}",
        content
    )
    
    with open(config_path, 'w') as f:
        f.write(content)

def generate_report(issues_by_file: Dict[Path, Dict[str, List[int]]]) -> str:
    """Generate a report of findings and changes"""
    report = ["# Application Code Update Report\n\n"]
    
    if not issues_by_file:
        report.append("No MySQL-specific code found.\n")
        return '\n'.join(report)
    
    report.append("## Files Requiring Updates\n\n")
    for file_path, issues in issues_by_file.items():
        report.append(f"### {file_path.relative_to(Path.cwd())}\n\n")
        for pattern_name, lines in issues.items():
            report.append(f"- {pattern_name}: Lines {', '.join(map(str, lines))}\n")
            if pattern_name in PG_REPLACEMENTS:
                report.append(f"  - Replace with: {PG_REPLACEMENTS[pattern_name]}\n")
        report.append("\n")
    
    return '\n'.join(report)

def main():
    project_root = Path(__file__).parent.parent
    
    # Find all Python files
    python_files = find_python_files(project_root)
    
    # Analyze files
    issues_by_file = {}
    for file_path in python_files:
        logger.info(f"Analyzing {file_path}")
        issues = analyze_file(file_path)
        if issues:
            issues_by_file[file_path] = issues
    
    # Update connection strings
    for file_path in python_files:
        logger.info(f"Updating connection strings in {file_path}")
        update_connection_strings(file_path)
    
    # Update ORM config
    orm_config = project_root / 'config' / 'settings.py'
    if orm_config.exists():
        logger.info("Updating ORM configuration")
        update_orm_config(orm_config)
    
    # Generate report
    report = generate_report(issues_by_file)
    report_path = project_root / 'docs' / 'migration' / 'app_code_updates.md'
    report_path.parent.mkdir(parents=True, exist_ok=True)
    
    with open(report_path, 'w') as f:
        f.write(report)
    
    logger.info(f"Report written to: {report_path}")

if __name__ == '__main__':
    main()
