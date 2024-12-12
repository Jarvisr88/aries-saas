import os
import shutil
import subprocess
from pathlib import Path

def setup_project():
    """
    Set up the development environment for the HME/DME project.
    """
    # Get the project root directory
    root_dir = Path(__file__).parent.parent
    
    print("Setting up HME/DME project environment...")
    
    # Create required directories if they don't exist
    directories = [
        'logs',
        'media',
        'static',
        'docs/analysis/workflows',
        'docs/analysis/migration',
    ]
    
    for directory in directories:
        dir_path = root_dir / directory
        dir_path.mkdir(parents=True, exist_ok=True)
        print(f"Created directory: {directory}")
    
    # Copy env.template to .env if it doesn't exist
    env_template = root_dir / 'env.template'
    env_file = root_dir / '.env'
    
    if not env_file.exists() and env_template.exists():
        shutil.copy(env_template, env_file)
        print("Created .env file from template")
    
    # Create virtual environment if it doesn't exist
    venv_dir = root_dir / 'venv'
    if not venv_dir.exists():
        subprocess.run(['python', '-m', 'venv', 'venv'], cwd=root_dir)
        print("Created virtual environment")
    
    print("\nProject setup completed!")
    print("\nNext steps:")
    print("1. Activate virtual environment:")
    print("   - Windows: .\\venv\\Scripts\\activate")
    print("   - Unix/MacOS: source venv/bin/activate")
    print("2. Install dependencies: pip install -r requirements.txt")
    print("3. Configure your .env file")
    print("4. Run migrations: python manage.py migrate")
    print("5. Create superuser: python manage.py createsuperuser")

if __name__ == '__main__':
    setup_project()
