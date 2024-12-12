from django.contrib.auth.models import AbstractUser
from django.db import models
from django.utils.translation import gettext_lazy as _
from phonenumber_field.modelfields import PhoneNumberField


class User(AbstractUser):
    """
    Custom user model for HME/DME application.
    """
    email = models.EmailField(_('email address'), unique=True)
    phone_number = PhoneNumberField(_('phone number'), blank=True)
    department = models.CharField(_('department'), max_length=100, blank=True)
    position = models.CharField(_('position'), max_length=100, blank=True)
    is_active = models.BooleanField(_('active'), default=True)
    date_joined = models.DateTimeField(_('date joined'), auto_now_add=True)
    
    # Additional fields for audit
    created_by = models.ForeignKey(
        'self', on_delete=models.SET_NULL, null=True, 
        related_name='created_users'
    )
    modified_by = models.ForeignKey(
        'self', on_delete=models.SET_NULL, null=True, 
        related_name='modified_users'
    )
    last_modified = models.DateTimeField(_('last modified'), auto_now=True)

    class Meta:
        verbose_name = _('user')
        verbose_name_plural = _('users')
        ordering = ['-date_joined']

    def __str__(self):
        return f"{self.get_full_name()} ({self.username})"

    def get_full_name(self):
        """
        Return the first_name plus the last_name, with a space in between.
        """
        full_name = f"{self.first_name} {self.last_name}"
        return full_name.strip()

    def get_short_name(self):
        """Return the short name for the user."""
        return self.first_name
