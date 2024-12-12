from django.db import models
from django.conf import settings
from simple_history.models import HistoricalRecords


class BaseModel(models.Model):
    """
    Base model for all models in the application.
    Includes audit fields and historical records.
    """
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    created_by = models.ForeignKey(
        settings.AUTH_USER_MODEL,
        on_delete=models.SET_NULL,
        null=True,
        related_name='%(class)s_created'
    )
    updated_by = models.ForeignKey(
        settings.AUTH_USER_MODEL,
        on_delete=models.SET_NULL,
        null=True,
        related_name='%(class)s_updated'
    )
    history = HistoricalRecords(inherit=True)

    class Meta:
        abstract = True
        ordering = ['-created_at']

    def save(self, *args, **kwargs):
        """
        Override save method to handle audit fields
        """
        user = kwargs.pop('user', None)
        if user:
            if not self.pk:
                self.created_by = user
            self.updated_by = user
        super().save(*args, **kwargs)
