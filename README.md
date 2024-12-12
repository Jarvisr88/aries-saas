# HME/DME Management System

## Project Overview
Modern Django-based application for Home Medical Equipment (HME) and Durable Medical Equipment (DME) management. This system replaces the legacy C# application with a scalable, maintainable solution built on Python/Django and PostgreSQL.

## Business Modules
- Order Management
- Inventory Management
- Billing and Claims Processing
- Delivery and Logistics
- Maintenance and Service Tracking
- Compliance and Audit Management
- User Management
- Customer Relationship Management (CRM)

## Technical Stack
- Backend: Django/Python
- Database: PostgreSQL
- Frontend: Django Templates with Modern JavaScript
- API: Django REST Framework

## Development Setup
1. Create and activate a virtual environment:
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

2. Install dependencies:
```bash
pip install -r requirements.txt
```

3. Configure database settings in `config/.env`

4. Run migrations:
```bash
python manage.py migrate
```

5. Start development server:
```bash
python manage.py runserver
```

## Project Structure
```
hme_dme/
├── apps/
│   ├── order_management/
│   ├── inventory/
│   ├── billing/
│   ├── delivery/
│   ├── maintenance/
│   ├── compliance/
│   ├── users/
│   └── crm/
├── config/
├── core/
├── static/
└── templates/
```

## Contributing
Please refer to CONTRIBUTING.md for development guidelines and coding standards.

## License
Proprietary - All rights reserved
