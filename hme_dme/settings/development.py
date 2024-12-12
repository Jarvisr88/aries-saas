"""
Development settings for HME/DME project.
"""

from .base import *
from core.config import get_settings
from datetime import timedelta

settings = get_settings()

# SECURITY WARNING: don't run with debug turned on in production!
DEBUG = True

ALLOWED_HOSTS = ['localhost', '127.0.0.1']

# Database - MySQL for analysis (will be migrated to PostgreSQL)
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.mysql',
        'NAME': 'c01',
        'USER': 'root',
        'PASSWORD': 'dmeworks',
        'HOST': 'localhost',
        'PORT': '3306',
        'OPTIONS': {
            'init_command': "SET sql_mode='STRICT_TRANS_TABLES'",
            'charset': 'utf8mb4',
        }
    }
}

# Add mysqlclient to requirements (temporary for analysis)
INSTALLED_APPS += ['django_mysql']

# Redis/Celery
CELERY_BROKER_URL = str(settings.REDIS_URL)
CELERY_RESULT_BACKEND = str(settings.REDIS_URL)

# Email settings
EMAIL_HOST = settings.EMAIL_HOST
EMAIL_PORT = settings.EMAIL_PORT
EMAIL_USE_TLS = settings.EMAIL_USE_TLS
EMAIL_HOST_USER = settings.EMAIL_HOST_USER
EMAIL_HOST_PASSWORD = settings.EMAIL_HOST_PASSWORD

# PostgreSQL configuration (for reference, will be used after migration)
POSTGRES_CONFIG = {
    'ENGINE': 'django.db.backends.postgresql',
    'NAME': 'hme_dme',
    'USER': 'postgres',
    'PASSWORD': '',  # To be set in environment
    'HOST': 'localhost',
    'PORT': '5432',
}

# CORS settings
CORS_ALLOW_ALL_ORIGINS = True  # Only for development!

# JWT Settings
SIMPLE_JWT = {
    'SIGNING_KEY': settings.JWT_SECRET_KEY or settings.SECRET_KEY,
    'ALGORITHM': settings.JWT_ALGORITHM,
    'ACCESS_TOKEN_LIFETIME': timedelta(minutes=settings.ACCESS_TOKEN_EXPIRE_MINUTES),
    'REFRESH_TOKEN_LIFETIME': timedelta(days=settings.REFRESH_TOKEN_EXPIRE_DAYS),
}
