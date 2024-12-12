# Business Logic Migration Plan: MySQL to Python/Django

## 1. Function Conversions

### 1.1 Billing Calculation Functions
```python
class BillingCalculator:
    @staticmethod
    def get_billable_amount(sale_type: str, billing_month: date, *args) -> Decimal:
        """
        Converts GetBillableAmount function
        Original: GetBillableAmount(sale_type, billing_month)
        """
        pass

    @staticmethod
    def get_allowable_amount(price: Decimal, insurance_type: str) -> Decimal:
        """
        Converts GetAllowableAmount function
        Original: GetAllowableAmount(price, insurance_type)
        """
        pass

    @staticmethod
    def get_multiplier(rental_type: str, period: str) -> Decimal:
        """
        Combines GetAmountMultiplier and GetQuantityMultiplier
        Original: GetAmountMultiplier(rental_type, period)
        """
        pass
```

### 1.2 Date Handling Functions
```python
class ServicePeriodCalculator:
    @staticmethod
    def get_next_dos_from(current_date: date, frequency: str) -> date:
        """
        Converts GetNextDosFrom function
        Original: GetNextDosFrom(current_date, frequency)
        """
        pass

    @staticmethod
    def get_next_dos_to(start_date: date, frequency: str) -> date:
        """
        Converts GetNextDosTo function
        Original: GetNextDosTo(start_date, frequency)
        """
        pass

    @staticmethod
    def get_period_end(start_date: date, frequency: str) -> date:
        """
        Combines GetPeriodEnd and GetPeriodEnd2
        Original: GetPeriodEnd(start_date, frequency)
        """
        pass
```

## 2. Stored Procedure Conversions

### 2.1 Order Processing Classes
```python
class OrderProcessor:
    """Converts order_process and related procedures"""
    
    def __init__(self, order_id: int):
        self.order = Order.objects.get(id=order_id)
    
    def process_order(self) -> None:
        """
        Converts order_process procedure
        Handles order workflow and status updates
        """
        pass

    def update_dos(self, new_date: date) -> None:
        """
        Converts order_update_dos procedure
        Updates delivery dates and related records
        """
        pass

    def convert_deposits_to_payments(self) -> None:
        """
        Converts Order_ConvertDepositsIntoPayments procedure
        """
        pass
```

### 2.2 Inventory Management Classes
```python
class InventoryManager:
    """Converts inventory management procedures"""
    
    def adjust_inventory(self, item_id: int, quantity: int) -> None:
        """
        Converts inventory_adjust_2 procedure
        Handles complex inventory adjustments
        """
        pass

    def refresh_inventory(self, warehouse_id: int) -> None:
        """
        Converts inventory_refresh procedure
        Updates inventory levels
        """
        pass

    def transfer_inventory(self, source_id: int, dest_id: int, items: list) -> None:
        """
        Converts inventory_transfer procedure
        Handles stock transfers between warehouses
        """
        pass
```

### 2.3 Invoice Processing Classes
```python
class InvoiceProcessor:
    """Converts invoice processing procedures"""
    
    def __init__(self, invoice_id: int):
        self.invoice = Invoice.objects.get(id=invoice_id)

    def add_payment(self, amount: Decimal) -> None:
        """
        Converts InvoiceDetails_AddPayment procedure
        """
        pass

    def recalculate_internals(self) -> None:
        """
        Converts InvoiceDetails_RecalculateInternals procedure
        """
        pass

    def update_balance(self) -> None:
        """
        Converts Invoice_UpdateBalance procedure
        """
        pass
```

## 3. Migration Strategy

### 3.1 Class Organization
- Group related functions into service classes
- Use Django models for data access
- Implement business logic in model methods where appropriate
- Create separate service layer for complex operations

### 3.2 Data Access Patterns
```python
class BaseService:
    """Base class for all service classes"""
    
    def __init__(self):
        self.db = transaction.atomic()  # Use Django's transaction management

    def execute_in_transaction(self, func):
        """Decorator for transactional operations"""
        with transaction.atomic():
            return func()
```

### 3.3 Error Handling
```python
class BusinessLogicException(Exception):
    """Custom exception for business logic errors"""
    pass

class ValidationError(BusinessLogicException):
    """Custom exception for validation errors"""
    pass
```

## 4. Implementation Priority

### Phase 1: Core Business Logic
1. BillingCalculator
2. ServicePeriodCalculator
3. OrderProcessor
   - Critical for daily operations
   - High complexity in business rules

### Phase 2: Inventory Management
1. InventoryManager
2. SerialNumberTracker
3. StockTransferService
   - Essential for stock control
   - Complex transaction handling

### Phase 3: Financial Processing
1. InvoiceProcessor
2. PaymentHandler
3. AccountingService
   - Critical for business operations
   - Requires careful testing

### Phase 4: Supporting Functions
1. ReportGenerator
2. DocumentationHandler
3. ComplianceChecker
   - Lower priority
   - Can be migrated gradually

## 5. Testing Strategy

### 5.1 Unit Tests
```python
class TestBillingCalculator(TestCase):
    def test_get_billable_amount(self):
        calculator = BillingCalculator()
        result = calculator.get_billable_amount(
            sale_type="RENTAL",
            billing_month=date(2024, 1, 1)
        )
        self.assertEqual(result, expected_amount)
```

### 5.2 Integration Tests
```python
class TestOrderProcessing(TransactionTestCase):
    def test_complete_order_workflow(self):
        processor = OrderProcessor(order_id=1)
        processor.process_order()
        # Verify all related records are updated
```

## 6. Performance Considerations

### 6.1 Batch Processing
```python
class BatchProcessor:
    """Handle large data operations"""
    
    @classmethod
    def process_in_chunks(cls, queryset, chunk_size=1000):
        """Process large querysets in chunks"""
        for chunk in queryset.iterator(chunk_size=chunk_size):
            with transaction.atomic():
                cls.process_chunk(chunk)
```

### 6.2 Caching Strategy
```python
class CachedCalculator:
    """Base class for calculators with caching"""
    
    @cached_property
    def get_cached_result(self):
        """Cache complex calculations"""
        return self.perform_calculation()
```

## 7. Migration Timeline

1. Week 1-2: Core Calculation Functions
   - BillingCalculator
   - ServicePeriodCalculator

2. Week 3-4: Order Processing
   - OrderProcessor
   - Basic workflow

3. Week 5-6: Inventory Management
   - InventoryManager
   - Stock control

4. Week 7-8: Financial Processing
   - InvoiceProcessor
   - Payment handling

5. Week 9-10: Testing & Optimization
   - Performance testing
   - Integration testing
