from django.db import models
from django.utils.translation import gettext_lazy as _
from djmoney.models.fields import MoneyField
from core.models import BaseModel


class Equipment(BaseModel):
    """
    Model to track medical equipment inventory
    """
    CONDITION_CHOICES = [
        ('new', _('New')),
        ('excellent', _('Excellent')),
        ('good', _('Good')),
        ('fair', _('Fair')),
        ('poor', _('Poor')),
        ('retired', _('Retired')),
    ]

    name = models.CharField(_('name'), max_length=255)
    model_number = models.CharField(_('model number'), max_length=100)
    serial_number = models.CharField(_('serial number'), max_length=100, unique=True)
    manufacturer = models.ForeignKey(
        'Manufacturer',
        on_delete=models.PROTECT,
        related_name='equipment',
        verbose_name=_('manufacturer')
    )
    category = models.ForeignKey(
        'Category',
        on_delete=models.PROTECT,
        related_name='equipment',
        verbose_name=_('category')
    )
    description = models.TextField(_('description'))
    condition = models.CharField(
        _('condition'),
        max_length=20,
        choices=CONDITION_CHOICES,
        default='new'
    )
    purchase_date = models.DateField(_('purchase date'))
    purchase_price = MoneyField(
        _('purchase price'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    rental_price = MoneyField(
        _('rental price'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    insurance_code = models.CharField(_('insurance code'), max_length=50)
    quantity_available = models.PositiveIntegerField(_('quantity available'))
    quantity_reserved = models.PositiveIntegerField(_('quantity reserved'), default=0)
    reorder_point = models.PositiveIntegerField(_('reorder point'))
    location = models.CharField(_('location'), max_length=100)
    is_active = models.BooleanField(_('active'), default=True)
    last_maintenance = models.DateField(_('last maintenance'), null=True, blank=True)
    next_maintenance = models.DateField(_('next maintenance'), null=True, blank=True)

    class Meta:
        verbose_name = _('equipment')
        verbose_name_plural = _('equipment')
        ordering = ['name']

    def __str__(self):
        return f"{self.name} - {self.model_number}"

    @property
    def quantity_available_for_order(self):
        """Calculate available quantity for new orders"""
        return self.quantity_available - self.quantity_reserved


class Manufacturer(BaseModel):
    """
    Model to track equipment manufacturers
    """
    name = models.CharField(_('name'), max_length=255)
    contact_person = models.CharField(_('contact person'), max_length=255)
    email = models.EmailField(_('email'))
    phone = models.CharField(_('phone'), max_length=20)
    website = models.URLField(_('website'), blank=True)
    address = models.TextField(_('address'))
    notes = models.TextField(_('notes'), blank=True)

    class Meta:
        verbose_name = _('manufacturer')
        verbose_name_plural = _('manufacturers')
        ordering = ['name']

    def __str__(self):
        return self.name


class Category(BaseModel):
    """
    Model to categorize equipment
    """
    name = models.CharField(_('name'), max_length=255)
    description = models.TextField(_('description'), blank=True)
    parent = models.ForeignKey(
        'self',
        on_delete=models.CASCADE,
        null=True,
        blank=True,
        related_name='children',
        verbose_name=_('parent category')
    )

    class Meta:
        verbose_name = _('category')
        verbose_name_plural = _('categories')
        ordering = ['name']

    def __str__(self):
        return self.name


class InventoryTransaction(BaseModel):
    """
    Model to track inventory movements
    """
    TRANSACTION_TYPES = [
        ('receive', _('Receive')),
        ('issue', _('Issue')),
        ('return', _('Return')),
        ('adjust', _('Adjustment')),
        ('transfer', _('Transfer')),
    ]

    equipment = models.ForeignKey(
        Equipment,
        on_delete=models.PROTECT,
        related_name='transactions',
        verbose_name=_('equipment')
    )
    transaction_type = models.CharField(
        _('transaction type'),
        max_length=20,
        choices=TRANSACTION_TYPES
    )
    quantity = models.IntegerField(_('quantity'))
    reference_number = models.CharField(_('reference number'), max_length=100)
    notes = models.TextField(_('notes'), blank=True)
    from_location = models.CharField(_('from location'), max_length=100, blank=True)
    to_location = models.CharField(_('to location'), max_length=100, blank=True)

    class Meta:
        verbose_name = _('inventory transaction')
        verbose_name_plural = _('inventory transactions')
        ordering = ['-created_at']

    def __str__(self):
        return f"{self.transaction_type} - {self.equipment} - {self.quantity}"
