"""
Core Pydantic schemas for data validation and serialization.
"""
from datetime import datetime
from typing import Optional
from pydantic import BaseModel, EmailStr, Field  # type: ignore

class BaseSchema(BaseModel):
    """Base schema with audit fields."""
    id: Optional[int] = None
    created_at: Optional[datetime] = None
    updated_at: Optional[datetime] = None
    created_by_id: Optional[int] = None
    updated_by_id: Optional[int] = None

    class Config:
        from_attributes = True


class UserBase(BaseSchema):
    """Base schema for User model."""
    email: EmailStr
    username: str = Field(min_length=3, max_length=150)
    first_name: Optional[str] = Field(None, max_length=150)
    last_name: Optional[str] = Field(None, max_length=150)
    phone_number: Optional[str] = None
    department: Optional[str] = Field(None, max_length=100)
    position: Optional[str] = Field(None, max_length=100)
    is_active: bool = True


class UserCreate(UserBase):
    """Schema for creating a new user."""
    password: str = Field(min_length=8)


class UserUpdate(UserBase):
    """Schema for updating a user."""
    password: Optional[str] = Field(None, min_length=8)


class UserResponse(UserBase):
    """Schema for user response."""
    pass


class Token(BaseModel):
    """Schema for authentication tokens."""
    access_token: str
    refresh_token: str
    token_type: str = "bearer"


class TokenPayload(BaseModel):
    """Schema for token payload."""
    sub: int
    exp: datetime
    type: str
