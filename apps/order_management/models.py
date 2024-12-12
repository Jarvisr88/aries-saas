from django.db import models
from django.utils.translation import gettext_lazy as _
from djmoney.models.fields import MoneyField
from core.models import BaseModel


class Order(BaseModel):
    """
    Model to track medical equipment orders
    """
    STATUS_CHOICES = [
        ('draft', _('Draft')),
        ('submitted', _('Submitted')),
        ('approved', _('Approved')),
        ('processing', _('Processing')),
        ('ready', _('Ready for Delivery')),
        ('delivered', _('Delivered')),
        ('cancelled', _('Cancelled')),
    ]

    order_number = models.CharField(_('order number'), max_length=50, unique=True)
    patient = models.ForeignKey(
        'crm.Patient',
        on_delete=models.PROTECT,
        related_name='orders',
        verbose_name=_('patient')
    )
    prescriber = models.ForeignKey(
        'crm.Prescriber',
        on_delete=models.PROTECT,
        related_name='orders',
        verbose_name=_('prescriber')
    )
    status = models.CharField(
        _('status'),
        max_length=20,
        choices=STATUS_CHOICES,
        default='draft'
    )
    order_date = models.DateTimeField(_('order date'), auto_now_add=True)
    delivery_date = models.DateField(_('delivery date'), null=True, blank=True)
    total_amount = MoneyField(
        _('total amount'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    insurance_coverage = MoneyField(
        _('insurance coverage'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    patient_responsibility = MoneyField(
        _('patient responsibility'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    notes = models.TextField(_('notes'), blank=True)

    class Meta:
        verbose_name = _('order')
        verbose_name_plural = _('orders')
        ordering = ['-order_date']

    def __str__(self):
        return f"Order {self.order_number}"


class OrderItem(BaseModel):
    """
    Model to track individual items within an order
    """
    order = models.ForeignKey(
        Order,
        on_delete=models.CASCADE,
        related_name='items',
        verbose_name=_('order')
    )
    equipment = models.ForeignKey(
        'inventory.Equipment',
        on_delete=models.PROTECT,
        related_name='order_items',
        verbose_name=_('equipment')
    )
    quantity = models.PositiveIntegerField(_('quantity'))
    unit_price = MoneyField(
        _('unit price'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    total_price = MoneyField(
        _('total price'),
        max_digits=10,
        decimal_places=2,
        default_currency='USD'
    )
    insurance_code = models.CharField(_('insurance code'), max_length=50)
    status = models.CharField(
        _('status'),
        max_length=20,
        choices=Order.STATUS_CHOICES,
        default='draft'
    )

    class Meta:
        verbose_name = _('order item')
        verbose_name_plural = _('order items')

    def __str__(self):
        return f"{self.equipment} - {self.quantity} units"

    def save(self, *args, **kwargs):
        """Calculate total price before saving"""
        self.total_price = self.quantity * self.unit_price
        super().save(*args, **kwargs)
