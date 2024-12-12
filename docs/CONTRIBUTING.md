# Contributing Guide

## Development Setup

### Prerequisites
- Python 3.12+
- PostgreSQL 15+
- Redis (for Celery)

### Environment Setup
1. Clone the repository
2. Create a virtual environment:
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```
3. Install dependencies:
```bash
pip install -r requirements.txt
```
4. Copy `.env.example` to `.env` and configure your environment variables

### Code Style
- Follow PEP 8 guidelines
- Use type hints
- Write docstrings for all classes and methods
- Keep functions focused and small
- Use meaningful variable names

### Testing
- Write unit tests for all new features
- Maintain minimum 80% code coverage
- Run tests before submitting PR:
```bash
python manage.py test
```

### Git Workflow
1. Create a feature branch from `develop`
2. Make your changes
3. Write/update tests
4. Update documentation
5. Submit PR to `develop`

### Documentation
- Update relevant documentation with code changes
- Include docstrings and type hints
- Document API endpoints using OpenAPI/Swagger

### Code Review Process
1. Self-review checklist
2. Peer review requirements
3. Testing verification
4. Documentation review
