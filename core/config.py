from typing import List
from pydantic_settings import BaseSettings
from pydantic import EmailStr, PostgresDsn, RedisDsn, field_validator
from functools import lru_cache


class Settings(BaseSettings):
    """
    Application settings using Pydantic for validation.
    """
    # Django
    DEBUG: bool = False
    SECRET_KEY: str
    ALLOWED_HOSTS: List[str] = ["localhost", "127.0.0.1"]
    
    # Database
    DATABASE_URL: PostgresDsn
    
    # Redis/Celery
    REDIS_URL: RedisDsn
    
    # Email
    EMAIL_HOST: str = "smtp.gmail.com"
    EMAIL_PORT: int = 587
    EMAIL_USE_TLS: bool = True
    EMAIL_HOST_USER: EmailStr
    EMAIL_HOST_PASSWORD: str
    
    # AWS
    AWS_ACCESS_KEY_ID: str | None = None
    AWS_SECRET_ACCESS_KEY: str | None = None
    AWS_STORAGE_BUCKET_NAME: str | None = None
    AWS_S3_REGION_NAME: str | None = None
    
    # Security
    CORS_ALLOWED_ORIGINS: List[str] = []
    JWT_SECRET_KEY: str | None = None
    JWT_ALGORITHM: str = "HS256"
    ACCESS_TOKEN_EXPIRE_MINUTES: int = 60
    REFRESH_TOKEN_EXPIRE_DAYS: int = 7
    
    @field_validator("DATABASE_URL")
    def validate_database_url(cls, v: str) -> str:
        """Validate database URL"""
        if not v:
            raise ValueError("DATABASE_URL is required")
        return v
    
    @field_validator("REDIS_URL")
    def validate_redis_url(cls, v: str) -> str:
        """Validate Redis URL"""
        if not v:
            raise ValueError("REDIS_URL is required")
        return v
    
    class Config:
        env_file = ".env"
        case_sensitive = True


@lru_cache()
def get_settings() -> Settings:
    """
    Get cached settings instance.
    """
    return Settings()
