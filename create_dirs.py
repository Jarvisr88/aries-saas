import os

def create_dirs():
    base_dirs = [
        'apps',
        'apps/order_management',
        'apps/inventory',
        'apps/billing',
        'apps/delivery',
        'apps/maintenance',
        'apps/compliance',
        'apps/users',
        'apps/crm',
        'core',
        'static',
        'templates'
    ]
    
    for dir_path in base_dirs:
        os.makedirs(dir_path, exist_ok=True)
        # Create __init__.py in each app directory
        if dir_path.startswith('apps/'):
            open(os.path.join(dir_path, '__init__.py'), 'a').close()

if __name__ == '__main__':
    create_dirs()
