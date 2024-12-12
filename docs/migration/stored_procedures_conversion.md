# Stored Procedures Conversion Report

Generated at: 2024-12-12 14:38:27

## customer_insurance_fixrank

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_ID, V_Rank INT; --
  DECLARE cur CURSOR FOR
    SELECT ID
    FROM tbl_customer_insurance
    WHERE (CustomerID = P_CustomerID)
    ORDER BY IF(InactiveDate <= Current_Date(), 1, 0), `Rank`, ID; --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_Rank = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_ID; --

    IF NOT done THEN
      UPDATE tbl_customer_insurance SET `Rank` = IF(InactiveDate <= Current_Date(), 99999, V_Rank) WHERE (ID = V_ID) AND (CustomerID = P_CustomerID); --
      SET V_Rank = V_Rank + 1; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_ID, V_Rank INT; --
  DECLARE cur CURSOR FOR
    SELECT ID
    FROM tbl_customer_insurance
    WHERE (CustomerID = P_CustomerID)
    ORDER BY IF(InactiveDate <= Current_Date(), 1, 0), `Rank`, ID; --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_Rank = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_ID; --

    IF NOT done THEN
      UPDATE tbl_customer_insurance SET `Rank` = IF(InactiveDate <= Current_Date(), 99999, V_Rank) WHERE (ID = V_ID) AND (CustomerID = P_CustomerID); --
      SET V_Rank = V_Rank + 1; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## fixInvoicePolicies

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance1_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance1_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance2_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance2_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance3_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance3_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance4_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance4_ID = tbl_customer_insurance.ID; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance1_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance1_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance2_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance2_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance3_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance3_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_invoice
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_invoice.CustomerInsurance4_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_invoice.CustomerID
  SET tbl_invoice.CustomerInsurance4_ID = tbl_customer_insurance.ID; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## fixOrderPolicies

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance1_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance1_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance2_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance2_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance3_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance3_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance4_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance4_ID = tbl_customer_insurance.ID; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance1_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance1_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance2_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance2_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance3_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance3_ID = tbl_customer_insurance.ID; --

  UPDATE tbl_order
        INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.InsuranceCompanyID = tbl_order.CustomerInsurance4_ID
                                          AND tbl_customer_insurance.CustomerID = tbl_order.CustomerID
  SET tbl_order.CustomerInsurance4_ID = tbl_customer_insurance.ID; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## fix_serial_transactions

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_Priority         int(11); --
  DECLARE cur_CustomerID       int(11); --
  DECLARE cur_OrderID          int(11); --
  DECLARE cur_OrderDetailsID   int(11); --
  DECLARE cur_SerialID         int(11); --
  DECLARE cur_WarehouseID      int(11); --
  DECLARE cur_TranType         varchar(50); --
  DECLARE cur_TranTime         datetime; --

  DECLARE cur CURSOR FOR
  SELECT
    Priority
  , CustomerID
  , OrderID
  , OrderDetailsID
  , SerialID
  , WarehouseID
  , TranType
  , TranTime
  FROM `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`
  ORDER BY DateReserved, OrderDetailsID, SerialId, Priority; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  DROP TABLE IF EXISTS `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`; --

  CREATE TEMPORARY TABLE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` AS
  SELECT DISTINCT
    od.SerialID
  , od.CustomerID
  , od.OrderID
  , od.ID as OrderDetailsID
  , od.WarehouseID
  , o.DeliveryDate as DateReserved
  , trf.Time as DateTransferred
  , CASE WHEN (o.Approved = 1) AND (od.IsRented = 1)
         THEN o.DeliveryDate
         ELSE NULL END as DateRented
  , CASE WHEN (o.Approved = 1) AND (od.IsRented = 1) AND (od.IsActive = 0) AND (od.IsCanceled = 0) AND (od.IsPickedup = 0)
         THEN od.EndDate
         ELSE NULL END as DateRentSold
  , CASE WHEN (o.Approved = 1) AND (od.IsRented = 1) AND (od.IsActive = 0) AND (od.IsCanceled = 0) AND (od.IsPickedup = 1)
         THEN od.EndDate
         ELSE NULL END as DatePickedup
  , CASE WHEN (o.Approved = 1) AND (od.IsSold = 1)
         THEN o.DeliveryDate
         ELSE NULL END as DateSold
  FROM tbl_order AS o
       INNER JOIN view_orderdetails AS od ON od.CustomerID = o.CustomerID
                                         AND od.OrderID    = o.ID
       INNER JOIN tbl_serial AS s ON od.SerialID        = s.ID -- serial exists
                                 AND od.InventoryItemID = s.InventoryItemID
       LEFT JOIN (SELECT st.SerialID, st.WarehouseID, MIN(TransactionDatetime) as Time
                  FROM tbl_serial_transaction as st
                       INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID
                  WHERE stt.Name = 'Transferred In'
                  GROUP BY st.SerialID, st.WarehouseID) as trf ON trf.SerialID    = od.SerialID
                                                              AND trf.WarehouseID = od.WarehouseID
  WHERE (o.DeliveryDate IS NOT NULL)
    AND (P_SerialID IS NULL OR s.ID = P_SerialID)
  ORDER BY od.SerialID, o.DeliveryDate, od.ID; --

  ALTER TABLE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` ADD COLUMN Number INT NOT NULL AUTO_INCREMENT PRIMARY KEY; --
  ALTER TABLE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` ADD COLUMN IsFirst BOOL NOT NULL DEFAULT 0; --

  DROP TABLE IF EXISTS `{F591E13A-9C30-445B-A812-BDE8F9A4566F}`; --

  CREATE TEMPORARY TABLE `{F591E13A-9C30-445B-A812-BDE8F9A4566F}` AS
  SELECT SerialID, Min(Number) as Number
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`
  GROUP BY SerialID; --

  UPDATE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` AS a
         INNER JOIN `{F591E13A-9C30-445B-A812-BDE8F9A4566F}` AS b ON a.SerialID = b.SerialID
  SET a.IsFirst = CASE WHEN a.Number = b.Number THEN 1 ELSE 0 END; --

  DROP TABLE IF EXISTS `{F591E13A-9C30-445B-A812-BDE8F9A4566F}`; --

  -- delete bad entries

  DROP TABLE IF EXISTS `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`; --

  CREATE TEMPORARY TABLE `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}` AS
  SELECT SerialID
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`
  GROUP BY SerialID
  HAVING 2 <= SUM(CASE WHEN DateRentSold IS NULL AND DatePickedup IS NULL THEN 1 ELSE 0 END); --

  -- OUTPUT bad entries for investigations

  SELECT SerialID
  FROM `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`; --

  DELETE FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`
  WHERE SerialID IN (SELECT SerialID FROM `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`); --

  DROP TABLE IF EXISTS `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`; --

  -- OUTPUT bad entries for investigations
  SELECT DISTINCT tmp.SerialID, stt.Name
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` as tmp
       INNER JOIN tbl_serial_transaction as st ON st.SerialID = tmp.SerialID
       INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID
  WHERE stt.Name NOT IN ('Reserved', 'Reserve Cancelled', 'Rented', 'Sold', 'Returned', 'In from Maintenance', 'Transferred In')
  ORDER BY tmp.SerialID, stt.Name; --

  DELETE tmp
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` as tmp
       INNER JOIN tbl_serial_transaction as st ON st.SerialID = tmp.SerialID
       INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID
  WHERE stt.Name NOT IN ('Reserved', 'Reserve Cancelled', 'Rented', 'Sold', 'Returned', 'In from Maintenance', 'Transferred In'); --

  DROP TABLE IF EXISTS `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`; --

  CREATE TEMPORARY TABLE `{E9A96545-F98D-4318-836E-A10EA2CD78B7}` AS
  SELECT
    CASE WHEN s.IsFirst = 1              AND t.Name = 'Transferred In'      THEN 0
         WHEN s.IsFirst = 0              AND t.Name = 'In from Maintenance' THEN 0
         WHEN s.DateReserved IS NOT NULL AND t.Name = 'Reserved'            THEN 1
         WHEN s.DateSold     IS NOT NULL AND t.Name = 'Sold'                THEN 2
         WHEN s.DateRented   IS NOT NULL AND t.Name = 'Rented'              THEN 2
         WHEN s.DateRentSold IS NOT NULL AND t.Name = 'Sold'                THEN 3
         WHEN s.DatePickedup IS NOT NULL AND t.Name = 'Returned'            THEN 3
         END as Priority
  , s.DateReserved
  , s.CustomerID
  , s.OrderID
  , s.OrderDetailsID
  , s.SerialID
  , s.WarehouseID
  , t.Name as TranType
  , CASE WHEN s.IsFirst = 1              AND t.Name = 'Transferred In'      THEN IFNULL(s.DateTransferred, s.DateReserved)
         WHEN s.IsFirst = 0              AND t.Name = 'In from Maintenance' THEN s.DateReserved
         WHEN s.DateReserved IS NOT NULL AND t.Name = 'Reserved'            THEN s.DateReserved
         WHEN s.DateSold     IS NOT NULL AND t.Name = 'Sold'                THEN s.DateSold
         WHEN s.DateRented   IS NOT NULL AND t.Name = 'Rented'              THEN s.DateRented
         WHEN s.DateRentSold IS NOT NULL AND t.Name = 'Sold'                THEN s.DateRentSold
         WHEN s.DatePickedup IS NOT NULL AND t.Name = 'Returned'            THEN s.DatePickedup
         END as TranTime
  FROM ( SELECT Name
         FROM tbl_serial_transaction_type
         WHERE Name IN ('Reserved', 'Reserve Cancelled', 'Rented', 'Sold', 'Returned', 'In from Maintenance', 'Transferred In')
       ) as t
       INNER JOIN `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` as s
               ON (s.IsFirst = 1              AND t.Name = 'Transferred In')
               OR (s.IsFirst = 0              AND t.Name = 'In from Maintenance')
               OR (s.DateReserved IS NOT NULL AND t.Name = 'Reserved')
               OR (s.DateSold     IS NOT NULL AND t.Name = 'Sold')
               OR (s.DateRented   IS NOT NULL AND t.Name = 'Rented')
               OR (s.DateRentSold IS NOT NULL AND t.Name = 'Sold')
               OR (s.DatePickedup IS NOT NULL AND t.Name = 'Returned')
  ORDER BY SerialId, DateReserved, OrderDetailsID, Priority; --

  DROP TABLE IF EXISTS `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`; --

  DELETE
  FROM tbl_serial_transaction
  WHERE SerialID IN (SELECT SerialID FROM `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
     cur_Priority
    ,cur_CustomerID
    ,cur_OrderID
    ,cur_OrderDetailsID
    ,cur_SerialID
    ,cur_WarehouseID
    ,cur_TranType
    ,cur_TranTime; --

    IF (done = 0) THEN
      CALL serial_add_transaction(
          cur_TranType       -- P_TranType         VARCHAR(50)
        , cur_TranTime       -- P_TranTime         DATETIME
        , cur_SerialID       -- P_SerialID         INT,
        , cur_WarehouseID    -- P_WarehouseID      INT,
        , null               -- P_VendorID         INT,
        , cur_CustomerID     -- P_CustomerID       INT,
        , cur_OrderID        -- P_OrderID          INT,
        , cur_OrderDetailsID -- P_OrderDetailsID   INT,
        , null               -- P_LotNumber        VARCHAR(50),
        , 1                  -- P_LastUpdateUserID INT
      ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --

  DROP TABLE IF EXISTS `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_Priority         int(11); --
  DECLARE cur_CustomerID       int(11); --
  DECLARE cur_OrderID          int(11); --
  DECLARE cur_OrderDetailsID   int(11); --
  DECLARE cur_SerialID         int(11); --
  DECLARE cur_WarehouseID      int(11); --
  DECLARE cur_TranType         varchar(50); --
  DECLARE cur_TranTime         datetime; --

  DECLARE cur CURSOR FOR
  SELECT
    Priority
  , CustomerID
  , OrderID
  , OrderDetailsID
  , SerialID
  , WarehouseID
  , TranType
  , TranTime
  FROM `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`
  ORDER BY DateReserved, OrderDetailsID, SerialId, Priority; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  DROP TABLE IF EXISTS `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`; --

  CREATE TEMPORARY TABLE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` AS
  SELECT DISTINCT
    od.SerialID
  , od.CustomerID
  , od.OrderID
  , od.ID as OrderDetailsID
  , od.WarehouseID
  , o.DeliveryDate as DateReserved
  , trf.Time as DateTransferred
  , CASE WHEN (o.Approved = 1) AND (od.IsRented = 1)
         THEN o.DeliveryDate
         ELSE NULL END as DateRented
  , CASE WHEN (o.Approved = 1) AND (od.IsRented = 1) AND (od.IsActive = 0) AND (od.IsCanceled = 0) AND (od.IsPickedup = 0)
         THEN od.EndDate
         ELSE NULL END as DateRentSold
  , CASE WHEN (o.Approved = 1) AND (od.IsRented = 1) AND (od.IsActive = 0) AND (od.IsCanceled = 0) AND (od.IsPickedup = 1)
         THEN od.EndDate
         ELSE NULL END as DatePickedup
  , CASE WHEN (o.Approved = 1) AND (od.IsSold = 1)
         THEN o.DeliveryDate
         ELSE NULL END as DateSold
  FROM tbl_order AS o
       INNER JOIN view_orderdetails AS od ON od.CustomerID = o.CustomerID
                                         AND od.OrderID    = o.ID
       INNER JOIN tbl_serial AS s ON od.SerialID        = s.ID -- serial exists
                                 AND od.InventoryItemID = s.InventoryItemID
       LEFT JOIN (SELECT st.SerialID, st.WarehouseID, MIN(TransactionDatetime) as Time
                  FROM tbl_serial_transaction as st
                       INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID
                  WHERE stt.Name = 'Transferred In'
                  GROUP BY st.SerialID, st.WarehouseID) as trf ON trf.SerialID    = od.SerialID
                                                              AND trf.WarehouseID = od.WarehouseID
  WHERE (o.DeliveryDate IS NOT NULL)
    AND (P_SerialID IS NULL OR s.ID = P_SerialID)
  ORDER BY od.SerialID, o.DeliveryDate, od.ID; --

  ALTER TABLE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` ADD COLUMN Number INT NOT NULL SERIAL PRIMARY KEY; --
  ALTER TABLE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` ADD COLUMN IsFirst BOOL NOT NULL DEFAULT 0; --

  DROP TABLE IF EXISTS `{F591E13A-9C30-445B-A812-BDE8F9A4566F}`; --

  CREATE TEMPORARY TABLE `{F591E13A-9C30-445B-A812-BDE8F9A4566F}` AS
  SELECT SerialID, Min(Number) as Number
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`
  GROUP BY SerialID; --

  UPDATE `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` AS a
         INNER JOIN `{F591E13A-9C30-445B-A812-BDE8F9A4566F}` AS b ON a.SerialID = b.SerialID
  SET a.IsFirst = CASE WHEN a.Number = b.Number THEN 1 ELSE 0 END; --

  DROP TABLE IF EXISTS `{F591E13A-9C30-445B-A812-BDE8F9A4566F}`; --

  -- delete bad entries

  DROP TABLE IF EXISTS `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`; --

  CREATE TEMPORARY TABLE `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}` AS
  SELECT SerialID
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`
  GROUP BY SerialID
  HAVING 2 <= SUM(CASE WHEN DateRentSold IS NULL AND DatePickedup IS NULL THEN 1 ELSE 0 END); --

  -- OUTPUT bad entries for investigations

  SELECT SerialID
  FROM `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`; --

  DELETE FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`
  WHERE SerialID IN (SELECT SerialID FROM `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`); --

  DROP TABLE IF EXISTS `{B3F09F5E-8C0F-41BD-B652-25386EAAEAC4}`; --

  -- OUTPUT bad entries for investigations
  SELECT DISTINCT tmp.SerialID, stt.Name
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` as tmp
       INNER JOIN tbl_serial_transaction as st ON st.SerialID = tmp.SerialID
       INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID
  WHERE stt.Name NOT IN ('Reserved', 'Reserve Cancelled', 'Rented', 'Sold', 'Returned', 'In from Maintenance', 'Transferred In')
  ORDER BY tmp.SerialID, stt.Name; --

  DELETE tmp
  FROM `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` as tmp
       INNER JOIN tbl_serial_transaction as st ON st.SerialID = tmp.SerialID
       INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID
  WHERE stt.Name NOT IN ('Reserved', 'Reserve Cancelled', 'Rented', 'Sold', 'Returned', 'In from Maintenance', 'Transferred In'); --

  DROP TABLE IF EXISTS `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`; --

  CREATE TEMPORARY TABLE `{E9A96545-F98D-4318-836E-A10EA2CD78B7}` AS
  SELECT
    CASE WHEN s.IsFirst = 1              AND t.Name = 'Transferred In'      THEN 0
         WHEN s.IsFirst = 0              AND t.Name = 'In from Maintenance' THEN 0
         WHEN s.DateReserved IS NOT NULL AND t.Name = 'Reserved'            THEN 1
         WHEN s.DateSold     IS NOT NULL AND t.Name = 'Sold'                THEN 2
         WHEN s.DateRented   IS NOT NULL AND t.Name = 'Rented'              THEN 2
         WHEN s.DateRentSold IS NOT NULL AND t.Name = 'Sold'                THEN 3
         WHEN s.DatePickedup IS NOT NULL AND t.Name = 'Returned'            THEN 3
         END as Priority
  , s.DateReserved
  , s.CustomerID
  , s.OrderID
  , s.OrderDetailsID
  , s.SerialID
  , s.WarehouseID
  , t.Name as TranType
  , CASE WHEN s.IsFirst = 1              AND t.Name = 'Transferred In'      THEN COALESCE(s.DateTransferred, s.DateReserved)
         WHEN s.IsFirst = 0              AND t.Name = 'In from Maintenance' THEN s.DateReserved
         WHEN s.DateReserved IS NOT NULL AND t.Name = 'Reserved'            THEN s.DateReserved
         WHEN s.DateSold     IS NOT NULL AND t.Name = 'Sold'                THEN s.DateSold
         WHEN s.DateRented   IS NOT NULL AND t.Name = 'Rented'              THEN s.DateRented
         WHEN s.DateRentSold IS NOT NULL AND t.Name = 'Sold'                THEN s.DateRentSold
         WHEN s.DatePickedup IS NOT NULL AND t.Name = 'Returned'            THEN s.DatePickedup
         END as TranTime
  FROM ( SELECT Name
         FROM tbl_serial_transaction_type
         WHERE Name IN ('Reserved', 'Reserve Cancelled', 'Rented', 'Sold', 'Returned', 'In from Maintenance', 'Transferred In')
       ) as t
       INNER JOIN `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}` as s
               ON (s.IsFirst = 1              AND t.Name = 'Transferred In')
               OR (s.IsFirst = 0              AND t.Name = 'In from Maintenance')
               OR (s.DateReserved IS NOT NULL AND t.Name = 'Reserved')
               OR (s.DateSold     IS NOT NULL AND t.Name = 'Sold')
               OR (s.DateRented   IS NOT NULL AND t.Name = 'Rented')
               OR (s.DateRentSold IS NOT NULL AND t.Name = 'Sold')
               OR (s.DatePickedup IS NOT NULL AND t.Name = 'Returned')
  ORDER BY SerialId, DateReserved, OrderDetailsID, Priority; --

  DROP TABLE IF EXISTS `{B19B3C36-B432-4F3C-86F9-F4AF004EE8AF}`; --

  DELETE
  FROM tbl_serial_transaction
  WHERE SerialID IN (SELECT SerialID FROM `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
     cur_Priority
    ,cur_CustomerID
    ,cur_OrderID
    ,cur_OrderDetailsID
    ,cur_SerialID
    ,cur_WarehouseID
    ,cur_TranType
    ,cur_TranTime; --

    IF (done = 0) THEN
      CALL serial_add_transaction(
          cur_TranType       -- P_TranType         VARCHAR(50)
        , cur_TranTime       -- P_TranTime         DATETIME
        , cur_SerialID       -- P_SerialID         INT,
        , cur_WarehouseID    -- P_WarehouseID      INT,
        , null               -- P_VendorID         INT,
        , cur_CustomerID     -- P_CustomerID       INT,
        , cur_OrderID        -- P_OrderID          INT,
        , cur_OrderDetailsID -- P_OrderDetailsID   INT,
        , null               -- P_LotNumber        VARCHAR(50),
        , 1                  -- P_LastUpdateUserID INT
      ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --

  DROP TABLE IF EXISTS `{E9A96545-F98D-4318-836E-A10EA2CD78B7}`; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## internal_inventory_transfer

### Original MySQL Procedure
```sql
BEGIN
  DECLARE
    V_OnHand INT; --
  DECLARE
    V_CostPerUnit DECIMAL(18, 2); --

  IF (P_InventoryItemID IS NOT NULL)
  AND (P_SrcWarehouseID IS NOT NULL)
  AND (P_DstWarehouseID IS NOT NULL)
  AND (0 < IFNULL(P_Quantity, 0)) THEN
    CALL inventory_refresh(P_SrcWarehouseID, P_InventoryItemID); --

    SELECT
      IFNULL(OnHand     , 0) as OnHand
    , IFNULL(CostPerUnit, 0) as CostPerUnit
    INTO
      V_OnHand
    , V_CostPerUnit
    FROM tbl_inventory
    WHERE (InventoryItemID = P_InventoryItemID)
      AND (WarehouseID     = P_SrcWarehouseID); --

    IF (P_Quantity <= V_OnHand) THEN
      CALL inventory_transaction_add_adjustment(
        P_SrcWarehouseID,
        P_InventoryItemID,
        'Transferred Out',
        P_Description,
        P_Quantity,
        V_CostPerUnit,
        P_LastUpdateUserID); --

      CALL inventory_transaction_add_adjustment(
        P_DstWarehouseID,
        P_InventoryItemID,
        'Transferred In',
        P_Description,
        P_Quantity,
        V_CostPerUnit,
        P_LastUpdateUserID); --

      CALL inventory_refresh(P_SrcWarehouseID, P_InventoryItemID); --
      CALL inventory_refresh(P_DstWarehouseID, P_InventoryItemID); --
    END IF; --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE
    V_OnHand INT; --
  DECLARE
    V_CostPerUnit DECIMAL(18, 2); --

  IF (P_InventoryItemID IS NOT NULL)
  AND (P_SrcWarehouseID IS NOT NULL)
  AND (P_DstWarehouseID IS NOT NULL)
  AND (0 < COALESCE(P_Quantity, 0)) THEN
    CALL inventory_refresh(P_SrcWarehouseID, P_InventoryItemID); --

    SELECT
      COALESCE(OnHand     , 0) as OnHand
    , COALESCE(CostPerUnit, 0) as CostPerUnit
    INTO
      V_OnHand
    , V_CostPerUnit
    FROM tbl_inventory
    WHERE (InventoryItemID = P_InventoryItemID)
      AND (WarehouseID     = P_SrcWarehouseID); --

    IF (P_Quantity <= V_OnHand) THEN
      CALL inventory_transaction_add_adjustment(
        P_SrcWarehouseID,
        P_InventoryItemID,
        'Transferred Out',
        P_Description,
        P_Quantity,
        V_CostPerUnit,
        P_LastUpdateUserID); --

      CALL inventory_transaction_add_adjustment(
        P_DstWarehouseID,
        P_InventoryItemID,
        'Transferred In',
        P_Description,
        P_Quantity,
        V_CostPerUnit,
        P_LastUpdateUserID); --

      CALL inventory_refresh(P_SrcWarehouseID, P_InventoryItemID); --
      CALL inventory_refresh(P_DstWarehouseID, P_InventoryItemID); --
    END IF; --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InventoryItem_Clone

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_RowCount INT; --

  INSERT INTO tbl_inventoryitem (
    Barcode
  , BarcodeType
  , Basis
  , CommissionPaidAt
  , VendorID
  , FlatRate
  , FlatRateAmount
  , Frequency
  , InventoryCode
  , ModelNumber
  , Name
  , O2Tank
  , Percentage
  , PercentageAmount
  , PredefinedTextID
  , ProductTypeID
  , Serialized
  , Service
  , LastUpdateUserID
  , LastUpdateDatetime
  , Inactive
  , ManufacturerID
  , PurchasePrice
  ) SELECT
    Barcode
  , BarcodeType
  , Basis
  , CommissionPaidAt
  , VendorID
  , FlatRate
  , FlatRateAmount
  , Frequency
  , InventoryCode
  , ModelNumber
  , IFNULL(P_NewName, Name) as Name
  , O2Tank
  , Percentage
  , PercentageAmount
  , PredefinedTextID
  , ProductTypeID
  , Serialized
  , Service
  , LastUpdateUserID
  , LastUpdateDatetime
  , Inactive
  , ManufacturerID
  , PurchasePrice
  FROM tbl_inventoryitem
  WHERE (ID = P_OldInventoryItemID); --

  SELECT ROW_COUNT(), LAST_INSERT_ID() INTO V_RowCount, P_NewInventoryItemID; --

  IF (V_RowCount = 0) THEN
    SET P_NewInventoryItemID = NULL; --
  ELSE
    INSERT INTO `tbl_pricecode_item` (
      AcceptAssignment
    , OrderedQuantity
    , OrderedUnits
    , OrderedWhen
    , OrderedConverter
    , BilledUnits
    , BilledWhen
    , BilledConverter
    , DeliveryUnits
    , DeliveryConverter
    , BillingCode
    , BillItemOn
    , DefaultCMNType
    , DefaultOrderType
    , AuthorizationTypeID
    , FlatRate
    , InventoryItemID
    , Modifier1
    , Modifier2
    , Modifier3
    , Modifier4
    , PriceCodeID
    , PredefinedTextID
    , Rent_AllowablePrice
    , Rent_BillablePrice
    , Sale_AllowablePrice
    , Sale_BillablePrice
    , RentalType
    , ReoccuringSale
    , ShowSpanDates
    , Taxable
    , LastUpdateUserID
    , LastUpdateDatetime
    , BillInsurance
    , DrugNoteField
    , DrugControlNumber
    ) SELECT
      AcceptAssignment
    , OrderedQuantity
    , OrderedUnits
    , OrderedWhen
    , OrderedConverter
    , BilledUnits
    , BilledWhen
    , BilledConverter
    , DeliveryUnits
    , DeliveryConverter
    , BillingCode
    , BillItemOn
    , DefaultCMNType
    , DefaultOrderType
    , AuthorizationTypeID
    , FlatRate
    , P_NewInventoryItemID as InventoryItemID
    , Modifier1
    , Modifier2
    , Modifier3
    , Modifier4
    , PriceCodeID
    , PredefinedTextID
    , Rent_AllowablePrice
    , Rent_BillablePrice
    , Sale_AllowablePrice
    , Sale_BillablePrice
    , RentalType
    , ReoccuringSale
    , ShowSpanDates
    , Taxable
    , LastUpdateUserID
    , LastUpdateDatetime
    , BillInsurance
    , DrugNoteField
    , DrugControlNumber
    FROM `tbl_pricecode_item`
    WHERE (InventoryItemID = P_OldInventoryItemID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_RowCount INT; --

  INSERT INTO tbl_inventoryitem (
    Barcode
  , BarcodeType
  , Basis
  , CommissionPaidAt
  , VendorID
  , FlatRate
  , FlatRateAmount
  , Frequency
  , InventoryCode
  , ModelNumber
  , Name
  , O2Tank
  , Percentage
  , PercentageAmount
  , PredefinedTextID
  , ProductTypeID
  , Serialized
  , Service
  , LastUpdateUserID
  , LastUpdateDatetime
  , Inactive
  , ManufacturerID
  , PurchasePrice
  ) SELECT
    Barcode
  , BarcodeType
  , Basis
  , CommissionPaidAt
  , VendorID
  , FlatRate
  , FlatRateAmount
  , Frequency
  , InventoryCode
  , ModelNumber
  , COALESCE(P_NewName, Name) as Name
  , O2Tank
  , Percentage
  , PercentageAmount
  , PredefinedTextID
  , ProductTypeID
  , Serialized
  , Service
  , LastUpdateUserID
  , LastUpdateDatetime
  , Inactive
  , ManufacturerID
  , PurchasePrice
  FROM tbl_inventoryitem
  WHERE (ID = P_OldInventoryItemID); --

  SELECT ROW_COUNT(), lastval() INTO V_RowCount, P_NewInventoryItemID; --

  IF (V_RowCount = 0) THEN
    SET P_NewInventoryItemID = NULL; --
  ELSE
    INSERT INTO `tbl_pricecode_item` (
      AcceptAssignment
    , OrderedQuantity
    , OrderedUnits
    , OrderedWhen
    , OrderedConverter
    , BilledUnits
    , BilledWhen
    , BilledConverter
    , DeliveryUnits
    , DeliveryConverter
    , BillingCode
    , BillItemOn
    , DefaultCMNType
    , DefaultOrderType
    , AuthorizationTypeID
    , FlatRate
    , InventoryItemID
    , Modifier1
    , Modifier2
    , Modifier3
    , Modifier4
    , PriceCodeID
    , PredefinedTextID
    , Rent_AllowablePrice
    , Rent_BillablePrice
    , Sale_AllowablePrice
    , Sale_BillablePrice
    , RentalType
    , ReoccuringSale
    , ShowSpanDates
    , Taxable
    , LastUpdateUserID
    , LastUpdateDatetime
    , BillInsurance
    , DrugNoteField
    , DrugControlNumber
    ) SELECT
      AcceptAssignment
    , OrderedQuantity
    , OrderedUnits
    , OrderedWhen
    , OrderedConverter
    , BilledUnits
    , BilledWhen
    , BilledConverter
    , DeliveryUnits
    , DeliveryConverter
    , BillingCode
    , BillItemOn
    , DefaultCMNType
    , DefaultOrderType
    , AuthorizationTypeID
    , FlatRate
    , P_NewInventoryItemID as InventoryItemID
    , Modifier1
    , Modifier2
    , Modifier3
    , Modifier4
    , PriceCodeID
    , PredefinedTextID
    , Rent_AllowablePrice
    , Rent_BillablePrice
    , Sale_AllowablePrice
    , Sale_BillablePrice
    , RentalType
    , ReoccuringSale
    , ShowSpanDates
    , Taxable
    , LastUpdateUserID
    , LastUpdateDatetime
    , BillInsurance
    , DrugNoteField
    , DrugControlNumber
    FROM `tbl_pricecode_item`
    WHERE (InventoryItemID = P_OldInventoryItemID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_adjust_2

### Original MySQL Procedure
```sql
BEGIN
  DECLARE
    V_DeltaOnHand
  , V_DeltaRented
  , V_DeltaSold
  , V_DeltaUnavailable
  , V_DeltaCommitted
  , V_DeltaOnOrder
  , V_DeltaBackOrdered INT; --
  DECLARE
    V_CostPerUnit DECIMAL(18, 2); --

  IF (P_WarehouseID IS NOT NULL) AND (P_InventoryItemID IS NOT NULL) THEN
    CALL inventory_refresh(P_WarehouseID, P_InventoryItemID); --

    /* in case when this entry does not have any transactions */
    SET V_DeltaOnHand        = IF(0 <= IFNULL(P_OnHand     , -1), P_OnHand      - 0, NULL); --
    SET V_DeltaRented        = IF(0 <= IFNULL(P_Rented     , -1), P_Rented      - 0, NULL); --
    SET V_DeltaSold          = IF(0 <= IFNULL(P_Sold       , -1), P_Sold        - 0, NULL); --
    SET V_DeltaUnavailable   = IF(0 <= IFNULL(P_Unavailable, -1), P_Unavailable - 0, NULL); --
    SET V_DeltaCommitted     = IF(0 <= IFNULL(P_Committed  , -1), P_Committed   - 0, NULL); --
    SET V_DeltaOnOrder       = IF(0 <= IFNULL(P_OnOrder    , -1), P_OnOrder     - 0, NULL); --
    SET V_DeltaBackOrdered   = IF(0 <= IFNULL(P_BackOrdered, -1), P_BackOrdered - 0, NULL); --
    SET V_CostPerUnit        = P_CostPerUnit; --

    SELECT
      IF(0 <= IFNULL(P_OnHand     , -1), P_OnHand      - IFNULL(OnHand     , 0), NULL) as DeltaOnHand
    , IF(0 <= IFNULL(P_Rented     , -1), P_Rented      - IFNULL(Rented     , 0), NULL) as DeltaRented
    , IF(0 <= IFNULL(P_Sold       , -1), P_Sold        - IFNULL(Sold       , 0), NULL) as DeltaSold
    , IF(0 <= IFNULL(P_Unavailable, -1), P_Unavailable - IFNULL(Unavailable, 0), NULL) as DeltaUnavailable
    , IF(0 <= IFNULL(P_Committed  , -1), P_Committed   - IFNULL(Committed  , 0), NULL) as DeltaCommitted
    , IF(0 <= IFNULL(P_OnOrder    , -1), P_OnOrder     - IFNULL(OnOrder    , 0), NULL) as DeltaOnOrder
    , IF(0 <= IFNULL(P_BackOrdered, -1), P_BackOrdered - IFNULL(BackOrdered, 0), NULL) as DeltaBackOrdered
    , CostPerUnit
    INTO
      V_DeltaOnHand
    , V_DeltaRented
    , V_DeltaSold
    , V_DeltaUnavailable
    , V_DeltaCommitted
    , V_DeltaOnOrder
    , V_DeltaBackOrdered
    , V_CostPerUnit
    FROM tbl_inventory
    WHERE (WarehouseID     = P_WarehouseID)
      AND (InventoryItemID = P_InventoryItemID); --

    SET V_CostPerUnit = IFNULL(P_CostPerUnit, IFNULL(V_CostPerUnit, 0)); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'OnHand Adjustment',
      'Manual Adjustment',
      IFNULL(V_DeltaOnHand, 0) + IFNULL(V_DeltaCommitted, 0) + IF(V_DeltaOnOrder < 0, V_DeltaOnOrder, 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Rented Adjustment',
      'Manual Adjustment',
      V_DeltaRented,
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Sold Adjustment',
      'Manual Adjustment',
      V_DeltaSold,
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Unavailable Adj',
      'Manual Adjustment',
      0 - V_DeltaUnavailable,
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Committed',
      'Manual Adjustment',
      IF(0 < V_DeltaCommitted, ABS(V_DeltaCommitted), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Commit Cancelled',
      'Manual Adjustment',
      IF(V_DeltaCommitted < 0, ABS(V_DeltaCommitted), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Ordered',
      'Manual Adjustment',
      IF(0 < V_DeltaOnOrder, ABS(V_DeltaOnOrder), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Received',
      'Manual Adjustment',
      IF(V_DeltaOnOrder < 0, ABS(V_DeltaOnOrder), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'BackOrdered',
      'Manual Adjustment',
      IF(0 < V_DeltaBackOrdered, ABS(V_DeltaBackOrdered), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Fill Back Order',
      'Manual Adjustment',
      IF(V_DeltaBackOrdered < 0, ABS(V_DeltaBackOrdered), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    IF (0 < IFNULL(P_CostPerUnit, -1)) THEN
      CALL inventory_transaction_add_adjustment(
        P_WarehouseID,
        P_InventoryItemID,
        'CostPerUnit Adj',
        'Manual Adjustment',
        1, -- to satisfy quantity check
        P_CostPerUnit,
        P_LastUpdateUserID); --
    END IF; --

    IF (0 <= IFNULL(P_ReOrderPoint, -1)) THEN
      UPDATE tbl_inventory
      SET ReOrderPoint = P_ReOrderPoint
      WHERE (WarehouseID     = P_WarehouseID)
        AND (InventoryItemID = P_InventoryItemID); --
    END IF; --

    CALL inventory_refresh(P_WarehouseID, P_InventoryItemID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE
    V_DeltaOnHand
  , V_DeltaRented
  , V_DeltaSold
  , V_DeltaUnavailable
  , V_DeltaCommitted
  , V_DeltaOnOrder
  , V_DeltaBackOrdered INT; --
  DECLARE
    V_CostPerUnit DECIMAL(18, 2); --

  IF (P_WarehouseID IS NOT NULL) AND (P_InventoryItemID IS NOT NULL) THEN
    CALL inventory_refresh(P_WarehouseID, P_InventoryItemID); --

    /* in case when this entry does not have any transactions */
    SET V_DeltaOnHand        = IF(0 <= COALESCE(P_OnHand     , -1), P_OnHand      - 0, NULL); --
    SET V_DeltaRented        = IF(0 <= COALESCE(P_Rented     , -1), P_Rented      - 0, NULL); --
    SET V_DeltaSold          = IF(0 <= COALESCE(P_Sold       , -1), P_Sold        - 0, NULL); --
    SET V_DeltaUnavailable   = IF(0 <= COALESCE(P_Unavailable, -1), P_Unavailable - 0, NULL); --
    SET V_DeltaCommitted     = IF(0 <= COALESCE(P_Committed  , -1), P_Committed   - 0, NULL); --
    SET V_DeltaOnOrder       = IF(0 <= COALESCE(P_OnOrder    , -1), P_OnOrder     - 0, NULL); --
    SET V_DeltaBackOrdered   = IF(0 <= COALESCE(P_BackOrdered, -1), P_BackOrdered - 0, NULL); --
    SET V_CostPerUnit        = P_CostPerUnit; --

    SELECT
      IF(0 <= COALESCE(P_OnHand     , -1), P_OnHand      - COALESCE(OnHand     , 0), NULL) as DeltaOnHand
    , IF(0 <= COALESCE(P_Rented     , -1), P_Rented      - COALESCE(Rented     , 0), NULL) as DeltaRented
    , IF(0 <= COALESCE(P_Sold       , -1), P_Sold        - COALESCE(Sold       , 0), NULL) as DeltaSold
    , IF(0 <= COALESCE(P_Unavailable, -1), P_Unavailable - COALESCE(Unavailable, 0), NULL) as DeltaUnavailable
    , IF(0 <= COALESCE(P_Committed  , -1), P_Committed   - COALESCE(Committed  , 0), NULL) as DeltaCommitted
    , IF(0 <= COALESCE(P_OnOrder    , -1), P_OnOrder     - COALESCE(OnOrder    , 0), NULL) as DeltaOnOrder
    , IF(0 <= COALESCE(P_BackOrdered, -1), P_BackOrdered - COALESCE(BackOrdered, 0), NULL) as DeltaBackOrdered
    , CostPerUnit
    INTO
      V_DeltaOnHand
    , V_DeltaRented
    , V_DeltaSold
    , V_DeltaUnavailable
    , V_DeltaCommitted
    , V_DeltaOnOrder
    , V_DeltaBackOrdered
    , V_CostPerUnit
    FROM tbl_inventory
    WHERE (WarehouseID     = P_WarehouseID)
      AND (InventoryItemID = P_InventoryItemID); --

    SET V_CostPerUnit = COALESCE(P_CostPerUnit, COALESCE(V_CostPerUnit, 0)); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'OnHand Adjustment',
      'Manual Adjustment',
      COALESCE(V_DeltaOnHand, 0) + COALESCE(V_DeltaCommitted, 0) + IF(V_DeltaOnOrder < 0, V_DeltaOnOrder, 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Rented Adjustment',
      'Manual Adjustment',
      V_DeltaRented,
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Sold Adjustment',
      'Manual Adjustment',
      V_DeltaSold,
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Unavailable Adj',
      'Manual Adjustment',
      0 - V_DeltaUnavailable,
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Committed',
      'Manual Adjustment',
      IF(0 < V_DeltaCommitted, ABS(V_DeltaCommitted), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Commit Cancelled',
      'Manual Adjustment',
      IF(V_DeltaCommitted < 0, ABS(V_DeltaCommitted), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Ordered',
      'Manual Adjustment',
      IF(0 < V_DeltaOnOrder, ABS(V_DeltaOnOrder), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Received',
      'Manual Adjustment',
      IF(V_DeltaOnOrder < 0, ABS(V_DeltaOnOrder), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'BackOrdered',
      'Manual Adjustment',
      IF(0 < V_DeltaBackOrdered, ABS(V_DeltaBackOrdered), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    CALL inventory_transaction_add_adjustment(
      P_WarehouseID,
      P_InventoryItemID,
      'Fill Back Order',
      'Manual Adjustment',
      IF(V_DeltaBackOrdered < 0, ABS(V_DeltaBackOrdered), 0),
      V_CostPerUnit,
      P_LastUpdateUserID); --

    IF (0 < COALESCE(P_CostPerUnit, -1)) THEN
      CALL inventory_transaction_add_adjustment(
        P_WarehouseID,
        P_InventoryItemID,
        'CostPerUnit Adj',
        'Manual Adjustment',
        1, -- to satisfy quantity check
        P_CostPerUnit,
        P_LastUpdateUserID); --
    END IF; --

    IF (0 <= COALESCE(P_ReOrderPoint, -1)) THEN
      UPDATE tbl_inventory
      SET ReOrderPoint = P_ReOrderPoint
      WHERE (WarehouseID     = P_WarehouseID)
        AND (InventoryItemID = P_InventoryItemID); --
    END IF; --

    CALL inventory_refresh(P_WarehouseID, P_InventoryItemID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_order_refresh

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_WarehouseID, V_InventoryItemID INT; --
  DECLARE cur CURSOR FOR SELECT WarehouseID, InventoryItemID FROM tbl_orderdetails WHERE (OrderID = P_OrderID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_WarehouseID, V_InventoryItemID; --

    IF NOT done THEN
      CALL inventory_refresh(V_WarehouseID, V_InventoryItemID); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_WarehouseID, V_InventoryItemID INT; --
  DECLARE cur CURSOR FOR SELECT WarehouseID, InventoryItemID FROM tbl_orderdetails WHERE (OrderID = P_OrderID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_WarehouseID, V_InventoryItemID; --

    IF NOT done THEN
      CALL inventory_refresh(V_WarehouseID, V_InventoryItemID); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_po_refresh

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_WarehouseID, V_InventoryItemID INT; --
  DECLARE cur CURSOR FOR SELECT WarehouseID, InventoryItemID FROM tbl_purchaseorderdetails WHERE (PurchaseOrderID = P_PurchaseOrderID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_WarehouseID, V_InventoryItemID; --

    IF NOT done THEN
      CALL inventory_refresh(V_WarehouseID, V_InventoryItemID); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_WarehouseID, V_InventoryItemID INT; --
  DECLARE cur CURSOR FOR SELECT WarehouseID, InventoryItemID FROM tbl_purchaseorderdetails WHERE (PurchaseOrderID = P_PurchaseOrderID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_WarehouseID, V_InventoryItemID; --

    IF NOT done THEN
      CALL inventory_refresh(V_WarehouseID, V_InventoryItemID); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_refresh

### Original MySQL Procedure
```sql
BEGIN
  DECLARE
    V_WarehouseID,
    V_InventoryItemID INT; --
  DECLARE
    V_OnHand,
    V_Committed,
    V_OnOrder,
    V_UnAvailable,
    V_Rented,
    V_Sold,
    V_BackOrdered INT; --
  DECLARE
    V_UnitPrice DECIMAL(18, 2); --

  DECLARE done INT DEFAULT 0; --
  DECLARE cur CURSOR FOR
      SELECT
        summary.WarehouseID
      , summary.InventoryItemID
      , IF(item.Service = 0, IFNULL(summary.OnHand      , 0), 0) as OnHand
      , IF(item.Service = 0, IFNULL(summary.Committed   , 0), 0) as Committed
      , IF(item.Service = 0, IFNULL(summary.OnOrder     , 0), 0) as OnOrder
      , IF(item.Service = 0, IFNULL(summary.UnAvailable , 0), 0) as UnAvailable
      , IF(item.Service = 0, IFNULL(summary.Rented      , 0), 0) as Rented
      , IF(item.Service = 0, IFNULL(summary.Sold        , 0), 0) as Sold
      , IF(item.Service = 0, IFNULL(summary.BackOrdered , 0), 0) as BackOrdered
      , IF(item.Service = 0, IFNULL(tran.Cost / tran.Quantity, IFNULL(summary.TotalCost / summary.TotalQuantity, 0)), 0) as UnitPrice
      FROM (SELECT
              tran.WarehouseID
            , tran.InventoryItemID
            , SUM(CASE WHEN tran_type.OnHand       > 0 THEN tran.Quantity WHEN tran_type.OnHand       < 0 THEN -tran.Quantity ELSE null END) as OnHand
            , SUM(CASE WHEN tran_type.Committed    > 0 THEN tran.Quantity WHEN tran_type.Committed    < 0 THEN -tran.Quantity ELSE null END) as Committed
            , SUM(CASE WHEN tran_type.OnOrder      > 0 THEN tran.Quantity WHEN tran_type.OnOrder      < 0 THEN -tran.Quantity ELSE null END) as OnOrder
            , SUM(CASE WHEN tran_type.UnAvailable  > 0 THEN tran.Quantity WHEN tran_type.UnAvailable  < 0 THEN -tran.Quantity ELSE null END) as UnAvailable
            , SUM(CASE WHEN tran_type.Rented       > 0 THEN tran.Quantity WHEN tran_type.Rented       < 0 THEN -tran.Quantity ELSE null END) as Rented
            , SUM(CASE WHEN tran_type.Sold         > 0 THEN tran.Quantity WHEN tran_type.Sold         < 0 THEN -tran.Quantity ELSE null END) as Sold
            , SUM(CASE WHEN tran_type.BackOrdered  > 0 THEN tran.Quantity WHEN tran_type.BackOrdered  < 0 THEN -tran.Quantity ELSE null END) as BackOrdered
            , SUM(IF(tran_type.AdjTotalCost = 1, tran.Cost    , null)) as TotalCost
            , SUM(IF(tran_type.AdjTotalCost = 1, tran.Quantity, null)) as TotalQuantity
            , MAX(IF(tran_type.Name = 'CostPerUnit Adj', tran.ID, null)) as LastAdjustID
            FROM tbl_inventory_transaction as tran
                 INNER JOIN tbl_inventory_transaction_type as tran_type ON tran.TypeID = tran_type.ID
            WHERE ((P_WarehouseID     IS NULL) OR (tran.WarehouseID     = P_WarehouseID    ))
              AND ((P_InventoryItemID IS NULL) OR (tran.InventoryItemID = P_InventoryItemID))
            GROUP BY tran.WarehouseID, tran.InventoryItemID) as summary
      LEFT JOIN tbl_inventory_transaction as tran ON tran.ID              = summary.LastAdjustID
                                                 AND tran.WarehouseID     = summary.WarehouseID
                                                 AND tran.InventoryItemID = summary.InventoryItemID
      INNER JOIN tbl_inventoryitem as item ON item.ID = summary.InventoryItemID
      WHERE (1 = 1); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      V_WarehouseID
    , V_InventoryItemID
    , V_OnHand
    , V_Committed
    , V_OnOrder
    , V_UnAvailable
    , V_Rented
    , V_Sold
    , V_BackOrdered
    , V_UnitPrice; --

    IF NOT done THEN
      UPDATE tbl_inventory SET
       OnHand           = V_OnHand
      ,Committed        = V_Committed
      ,OnOrder          = V_OnOrder
      ,UnAvailable      = V_UnAvailable
      ,Rented           = V_Rented
      ,Sold             = V_Sold
      ,BackOrdered      = V_BackOrdered
      ,CostPerUnit      = V_UnitPrice
      ,TotalCost        = V_UnitPrice * (V_OnHand + V_Rented + V_UnAvailable)
      ,LastUpdateUserID = 1
      WHERE (WarehouseID     = V_WarehouseID)
        AND (InventoryItemID = V_InventoryItemID); --

      IF (ROW_COUNT() = 0) THEN
        INSERT IGNORE INTO tbl_inventory SET
         OnHand           = V_OnHand
        ,Committed        = V_Committed
        ,OnOrder          = V_OnOrder
        ,UnAvailable      = V_UnAvailable
        ,Rented           = V_Rented
        ,Sold             = V_Sold
        ,BackOrdered      = V_BackOrdered
        ,CostPerUnit      = V_UnitPrice
        ,TotalCost        = V_UnitPrice * (V_OnHand + V_Rented + V_UnAvailable)
        ,LastUpdateUserID = 1
        ,ReOrderPoint     = 0
        ,WarehouseID      = V_WarehouseID
        ,InventoryItemID  = V_InventoryItemID; --
      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE
    V_WarehouseID,
    V_InventoryItemID INT; --
  DECLARE
    V_OnHand,
    V_Committed,
    V_OnOrder,
    V_UnAvailable,
    V_Rented,
    V_Sold,
    V_BackOrdered INT; --
  DECLARE
    V_UnitPrice DECIMAL(18, 2); --

  DECLARE done INT DEFAULT 0; --
  DECLARE cur CURSOR FOR
      SELECT
        summary.WarehouseID
      , summary.InventoryItemID
      , IF(item.Service = 0, COALESCE(summary.OnHand      , 0), 0) as OnHand
      , IF(item.Service = 0, COALESCE(summary.Committed   , 0), 0) as Committed
      , IF(item.Service = 0, COALESCE(summary.OnOrder     , 0), 0) as OnOrder
      , IF(item.Service = 0, COALESCE(summary.UnAvailable , 0), 0) as UnAvailable
      , IF(item.Service = 0, COALESCE(summary.Rented      , 0), 0) as Rented
      , IF(item.Service = 0, COALESCE(summary.Sold        , 0), 0) as Sold
      , IF(item.Service = 0, COALESCE(summary.BackOrdered , 0), 0) as BackOrdered
      , IF(item.Service = 0, COALESCE(tran.Cost / tran.Quantity, COALESCE(summary.TotalCost / summary.TotalQuantity, 0)), 0) as UnitPrice
      FROM (SELECT
              tran.WarehouseID
            , tran.InventoryItemID
            , SUM(CASE WHEN tran_type.OnHand       > 0 THEN tran.Quantity WHEN tran_type.OnHand       < 0 THEN -tran.Quantity ELSE null END) as OnHand
            , SUM(CASE WHEN tran_type.Committed    > 0 THEN tran.Quantity WHEN tran_type.Committed    < 0 THEN -tran.Quantity ELSE null END) as Committed
            , SUM(CASE WHEN tran_type.OnOrder      > 0 THEN tran.Quantity WHEN tran_type.OnOrder      < 0 THEN -tran.Quantity ELSE null END) as OnOrder
            , SUM(CASE WHEN tran_type.UnAvailable  > 0 THEN tran.Quantity WHEN tran_type.UnAvailable  < 0 THEN -tran.Quantity ELSE null END) as UnAvailable
            , SUM(CASE WHEN tran_type.Rented       > 0 THEN tran.Quantity WHEN tran_type.Rented       < 0 THEN -tran.Quantity ELSE null END) as Rented
            , SUM(CASE WHEN tran_type.Sold         > 0 THEN tran.Quantity WHEN tran_type.Sold         < 0 THEN -tran.Quantity ELSE null END) as Sold
            , SUM(CASE WHEN tran_type.BackOrdered  > 0 THEN tran.Quantity WHEN tran_type.BackOrdered  < 0 THEN -tran.Quantity ELSE null END) as BackOrdered
            , SUM(IF(tran_type.AdjTotalCost = 1, tran.Cost    , null)) as TotalCost
            , SUM(IF(tran_type.AdjTotalCost = 1, tran.Quantity, null)) as TotalQuantity
            , MAX(IF(tran_type.Name = 'CostPerUnit Adj', tran.ID, null)) as LastAdjustID
            FROM tbl_inventory_transaction as tran
                 INNER JOIN tbl_inventory_transaction_type as tran_type ON tran.TypeID = tran_type.ID
            WHERE ((P_WarehouseID     IS NULL) OR (tran.WarehouseID     = P_WarehouseID    ))
              AND ((P_InventoryItemID IS NULL) OR (tran.InventoryItemID = P_InventoryItemID))
            GROUP BY tran.WarehouseID, tran.InventoryItemID) as summary
      LEFT JOIN tbl_inventory_transaction as tran ON tran.ID              = summary.LastAdjustID
                                                 AND tran.WarehouseID     = summary.WarehouseID
                                                 AND tran.InventoryItemID = summary.InventoryItemID
      INNER JOIN tbl_inventoryitem as item ON item.ID = summary.InventoryItemID
      WHERE (1 = 1); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      V_WarehouseID
    , V_InventoryItemID
    , V_OnHand
    , V_Committed
    , V_OnOrder
    , V_UnAvailable
    , V_Rented
    , V_Sold
    , V_BackOrdered
    , V_UnitPrice; --

    IF NOT done THEN
      UPDATE tbl_inventory SET
       OnHand           = V_OnHand
      ,Committed        = V_Committed
      ,OnOrder          = V_OnOrder
      ,UnAvailable      = V_UnAvailable
      ,Rented           = V_Rented
      ,Sold             = V_Sold
      ,BackOrdered      = V_BackOrdered
      ,CostPerUnit      = V_UnitPrice
      ,TotalCost        = V_UnitPrice * (V_OnHand + V_Rented + V_UnAvailable)
      ,LastUpdateUserID = 1
      WHERE (WarehouseID     = V_WarehouseID)
        AND (InventoryItemID = V_InventoryItemID); --

      IF (ROW_COUNT() = 0) THEN
        INSERT IGNORE INTO tbl_inventory SET
         OnHand           = V_OnHand
        ,Committed        = V_Committed
        ,OnOrder          = V_OnOrder
        ,UnAvailable      = V_UnAvailable
        ,Rented           = V_Rented
        ,Sold             = V_Sold
        ,BackOrdered      = V_BackOrdered
        ,CostPerUnit      = V_UnitPrice
        ,TotalCost        = V_UnitPrice * (V_OnHand + V_Rented + V_UnAvailable)
        ,LastUpdateUserID = 1
        ,ReOrderPoint     = 0
        ,WarehouseID      = V_WarehouseID
        ,InventoryItemID  = V_InventoryItemID; --
      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_transaction_add_adjustment

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_TranTypeID INT; --

  IF (P_WarehouseID IS NOT NULL) AND (P_InventoryItemID IS NOT NULL) AND (IFNULL(P_Quantity, 0) != 0) THEN
    SELECT ID
    INTO V_TranTypeID
    FROM tbl_inventory_transaction_type
    WHERE (Name = P_Type); --

    IF (V_TranTypeID IS NOT NULL) THEN
      INSERT INTO tbl_inventory_transaction SET
       WarehouseID            = P_WarehouseID
      ,InventoryItemID        = P_InventoryItemID
      ,TypeID                 = V_TranTypeID
      ,Date                   = Now()
      ,Quantity               = P_Quantity
      ,Cost                   = P_Quantity * P_CostPerUnit
      ,Description            = IFNULL(P_Description, 'No Description')
      ,SerialID               = NULL
      ,VendorID               = NULL
      ,CustomerID             = NULL
      ,LastUpdateUserID       = P_LastUpdateUserID
      ,LastUpdateDatetime     = Now()
      ,PurchaseOrderID        = NULL
      ,PurchaseOrderDetailsID = NULL
      ,InvoiceID              = NULL
      ,ManufacturerID         = NULL
      ,OrderID                = NULL
      ,OrderDetailsID         = NULL; --
    END IF; --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_TranTypeID INT; --

  IF (P_WarehouseID IS NOT NULL) AND (P_InventoryItemID IS NOT NULL) AND (COALESCE(P_Quantity, 0) != 0) THEN
    SELECT ID
    INTO V_TranTypeID
    FROM tbl_inventory_transaction_type
    WHERE (Name = P_Type); --

    IF (V_TranTypeID IS NOT NULL) THEN
      INSERT INTO tbl_inventory_transaction SET
       WarehouseID            = P_WarehouseID
      ,InventoryItemID        = P_InventoryItemID
      ,TypeID                 = V_TranTypeID
      ,Date                   = Now()
      ,Quantity               = P_Quantity
      ,Cost                   = P_Quantity * P_CostPerUnit
      ,Description            = COALESCE(P_Description, 'No Description')
      ,SerialID               = NULL
      ,VendorID               = NULL
      ,CustomerID             = NULL
      ,LastUpdateUserID       = P_LastUpdateUserID
      ,LastUpdateDatetime     = Now()
      ,PurchaseOrderID        = NULL
      ,PurchaseOrderDetailsID = NULL
      ,InvoiceID              = NULL
      ,ManufacturerID         = NULL
      ,OrderID                = NULL
      ,OrderDetailsID         = NULL; --
    END IF; --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_transaction_order_cleanup

### Original MySQL Procedure
```sql
BEGIN
  -- delete transactions if corresponding orders or line items do not exist
  DELETE tran
  FROM tbl_inventory_transaction as tran
       LEFT JOIN view_orderdetails as d ON tran.InventoryItemID = d.InventoryItemID
                                       AND tran.WarehouseID     = d.WarehouseID
                                       AND tran.CustomerID      = d.CustomerID
                                       AND tran.OrderID         = d.OrderID
                                       AND tran.OrderDetailsID  = d.ID
       LEFT JOIN tbl_order as o ON d.OrderID    = o.ID
                               AND d.CustomerID = o.CustomerID
  WHERE (tran.OrderID IS NOT NULL)
    AND (o.ID IS NULL); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  -- delete transactions if corresponding orders or line items do not exist
  DELETE tran
  FROM tbl_inventory_transaction as tran
       LEFT JOIN view_orderdetails as d ON tran.InventoryItemID = d.InventoryItemID
                                       AND tran.WarehouseID     = d.WarehouseID
                                       AND tran.CustomerID      = d.CustomerID
                                       AND tran.OrderID         = d.OrderID
                                       AND tran.OrderDetailsID  = d.ID
       LEFT JOIN tbl_order as o ON d.OrderID    = o.ID
                               AND d.CustomerID = o.CustomerID
  WHERE (tran.OrderID IS NOT NULL)
    AND (o.ID IS NULL); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_transaction_order_refresh

### Original MySQL Procedure
```sql
BEGIN
  -- type:
  -- Committed
  -- Sold
  -- Rented
  -- Rental Returned
  -- since user with necessary permissions have ability to set Approved = False
  -- we need to delete all transactions for not approved order
  -- and check all warehouses for it.

  -- if something was severily changed we need to delete
  DELETE tran
  FROM tbl_inventory_transaction as tran
       LEFT JOIN view_orderdetails as d ON tran.InventoryItemID = d.InventoryItemID
                                       AND tran.WarehouseID     = d.WarehouseID
                                       AND tran.CustomerID      = d.CustomerID
                                       AND tran.OrderID         = d.OrderID
                                       AND tran.OrderDetailsID  = d.ID
       LEFT JOIN tbl_order as o ON d.OrderID    = o.ID
                               AND d.CustomerID = o.CustomerID
  WHERE (tran.OrderID = P_OrderID)
    AND (o.ID IS NULL); --

  -- if transaction type does not correspond to the state of order we need to delete
  DELETE tran
  FROM tbl_inventory_transaction as tran
       INNER JOIN view_orderdetails as d ON tran.InventoryItemID = d.InventoryItemID
                                        AND tran.WarehouseID     = d.WarehouseID
                                        AND tran.CustomerID      = d.CustomerID
                                        AND tran.OrderID         = d.OrderID
                                        AND tran.OrderDetailsID  = d.ID
       INNER JOIN tbl_order as o ON d.OrderID    = o.ID
                                AND d.CustomerID = o.CustomerID
       INNER JOIN tbl_inventory_transaction_type as tt ON tt.ID = tran.TypeID
  WHERE (tran.OrderID = P_OrderID)
    AND NOT CASE tt.Name WHEN 'Committed'       THEN 1
                         WHEN 'Sold'            THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsSold
                         WHEN 'Rented'          THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented
                         WHEN 'Rental Returned' THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented AND d.IsPickedup
                         ELSE 0 END; --

  -- if transaction were not removed we have to update it to sync with order
  UPDATE tbl_inventory_transaction as tran
         INNER JOIN view_orderdetails as d ON tran.WarehouseID     = d.WarehouseID
                                          AND tran.InventoryItemID = d.InventoryItemID
                                          AND tran.CustomerID      = d.CustomerID
                                          AND tran.OrderID         = d.OrderID
                                          AND tran.OrderDetailsID  = d.ID
         INNER JOIN tbl_order as o ON d.OrderID = o.ID
                                  AND d.CustomerID = o.CustomerID
         INNER JOIN tbl_inventory_transaction_type as tt ON tt.ID = tran.TypeID
  SET tran.Date        = IFNULL(o.OrderDate, CURRENT_DATE()),
      tran.Quantity    = d.DeliveryQuantity,
      tran.Description = CONCAT('Order #', d.OrderID),
      tran.Cost        = 0,
      tran.SerialID    = NULL,
      tran.VendorID    = NULL,
      tran.InvoiceID   = NULL,
      tran.ManufacturerID = NULL,
      tran.LastUpdateUserID = o.LastUpdateUserID
  WHERE (o.ID = P_OrderID)
    AND CASE tt.Name WHEN 'Committed'       THEN 1
                     WHEN 'Sold'            THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsSold
                     WHEN 'Rented'          THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented
                     WHEN 'Rental Returned' THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented AND d.IsPickedup
                     ELSE 0 END; --

  INSERT INTO tbl_inventory_transaction
    (WarehouseID
    ,InventoryItemID
    ,TypeID
    ,Date
    ,Quantity
    ,Cost
    ,Description
    ,CustomerID
    ,OrderID
    ,OrderDetailsID
    ,LastUpdateUserID
    ,SerialID
    ,VendorID
    ,PurchaseOrderID
    ,PurchaseOrderDetailsID
    ,InvoiceID
    ,ManufacturerID)
  SELECT d.WarehouseID,
         d.InventoryItemID,
         tt.ID as TypeID,
         IFNULL(o.OrderDate, CURRENT_DATE()) as Date,
         d.DeliveryQuantity as Quantity,
         0 as Cost,
         CONCAT('Order #', d.OrderID) as Description,
         d.CustomerID,
         d.OrderID,
         d.ID as OrderDetailsID,
         o.LastUpdateUserID,
         NULL as SerialID,
         NULL as VendorID,
         NULL as PurchaseOrderID,
         NULL as PurchaseOrderDetailsID,
         NULL as InvoiceID,
         NULL as ManufacturerID
  FROM view_orderdetails as d
       INNER JOIN tbl_order as o ON d.OrderID    = o.ID
                                AND d.CustomerID = o.CustomerID
       INNER JOIN tbl_inventory_transaction_type as tt ON tt.Name IN ('Committed', 'Sold', 'Rented', 'Rental Returned')
       LEFT JOIN tbl_inventory_transaction as tran ON tran.WarehouseID     = d.WarehouseID
                                                  AND tran.InventoryItemID = d.InventoryItemID
                                                  AND tran.CustomerID      = d.CustomerID
                                                  AND tran.OrderID         = d.OrderID
                                                  AND tran.OrderDetailsID  = d.ID
                                                  AND tran.TypeID          = tt.ID
  WHERE (o.ID = P_OrderID)
    AND (tran.ID IS NULL)
    AND CASE tt.Name WHEN 'Committed'       THEN 1
                     WHEN 'Sold'            THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsSold
                     WHEN 'Rented'          THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented
                     WHEN 'Rental Returned' THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented AND d.IsPickedup
                     ELSE 0 END; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  -- type:
  -- Committed
  -- Sold
  -- Rented
  -- Rental Returned
  -- since user with necessary permissions have ability to set Approved = False
  -- we need to delete all transactions for not approved order
  -- and check all warehouses for it.

  -- if something was severily changed we need to delete
  DELETE tran
  FROM tbl_inventory_transaction as tran
       LEFT JOIN view_orderdetails as d ON tran.InventoryItemID = d.InventoryItemID
                                       AND tran.WarehouseID     = d.WarehouseID
                                       AND tran.CustomerID      = d.CustomerID
                                       AND tran.OrderID         = d.OrderID
                                       AND tran.OrderDetailsID  = d.ID
       LEFT JOIN tbl_order as o ON d.OrderID    = o.ID
                               AND d.CustomerID = o.CustomerID
  WHERE (tran.OrderID = P_OrderID)
    AND (o.ID IS NULL); --

  -- if transaction type does not correspond to the state of order we need to delete
  DELETE tran
  FROM tbl_inventory_transaction as tran
       INNER JOIN view_orderdetails as d ON tran.InventoryItemID = d.InventoryItemID
                                        AND tran.WarehouseID     = d.WarehouseID
                                        AND tran.CustomerID      = d.CustomerID
                                        AND tran.OrderID         = d.OrderID
                                        AND tran.OrderDetailsID  = d.ID
       INNER JOIN tbl_order as o ON d.OrderID    = o.ID
                                AND d.CustomerID = o.CustomerID
       INNER JOIN tbl_inventory_transaction_type as tt ON tt.ID = tran.TypeID
  WHERE (tran.OrderID = P_OrderID)
    AND NOT CASE tt.Name WHEN 'Committed'       THEN 1
                         WHEN 'Sold'            THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsSold
                         WHEN 'Rented'          THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented
                         WHEN 'Rental Returned' THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented AND d.IsPickedup
                         ELSE 0 END; --

  -- if transaction were not removed we have to update it to sync with order
  UPDATE tbl_inventory_transaction as tran
         INNER JOIN view_orderdetails as d ON tran.WarehouseID     = d.WarehouseID
                                          AND tran.InventoryItemID = d.InventoryItemID
                                          AND tran.CustomerID      = d.CustomerID
                                          AND tran.OrderID         = d.OrderID
                                          AND tran.OrderDetailsID  = d.ID
         INNER JOIN tbl_order as o ON d.OrderID = o.ID
                                  AND d.CustomerID = o.CustomerID
         INNER JOIN tbl_inventory_transaction_type as tt ON tt.ID = tran.TypeID
  SET tran.Date        = COALESCE(o.OrderDate, CURRENT_DATE()),
      tran.Quantity    = d.DeliveryQuantity,
      tran.Description = CONCAT('Order #', d.OrderID),
      tran.Cost        = 0,
      tran.SerialID    = NULL,
      tran.VendorID    = NULL,
      tran.InvoiceID   = NULL,
      tran.ManufacturerID = NULL,
      tran.LastUpdateUserID = o.LastUpdateUserID
  WHERE (o.ID = P_OrderID)
    AND CASE tt.Name WHEN 'Committed'       THEN 1
                     WHEN 'Sold'            THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsSold
                     WHEN 'Rented'          THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented
                     WHEN 'Rental Returned' THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented AND d.IsPickedup
                     ELSE 0 END; --

  INSERT INTO tbl_inventory_transaction
    (WarehouseID
    ,InventoryItemID
    ,TypeID
    ,Date
    ,Quantity
    ,Cost
    ,Description
    ,CustomerID
    ,OrderID
    ,OrderDetailsID
    ,LastUpdateUserID
    ,SerialID
    ,VendorID
    ,PurchaseOrderID
    ,PurchaseOrderDetailsID
    ,InvoiceID
    ,ManufacturerID)
  SELECT d.WarehouseID,
         d.InventoryItemID,
         tt.ID as TypeID,
         COALESCE(o.OrderDate, CURRENT_DATE()) as Date,
         d.DeliveryQuantity as Quantity,
         0 as Cost,
         CONCAT('Order #', d.OrderID) as Description,
         d.CustomerID,
         d.OrderID,
         d.ID as OrderDetailsID,
         o.LastUpdateUserID,
         NULL as SerialID,
         NULL as VendorID,
         NULL as PurchaseOrderID,
         NULL as PurchaseOrderDetailsID,
         NULL as InvoiceID,
         NULL as ManufacturerID
  FROM view_orderdetails as d
       INNER JOIN tbl_order as o ON d.OrderID    = o.ID
                                AND d.CustomerID = o.CustomerID
       INNER JOIN tbl_inventory_transaction_type as tt ON tt.Name IN ('Committed', 'Sold', 'Rented', 'Rental Returned')
       LEFT JOIN tbl_inventory_transaction as tran ON tran.WarehouseID     = d.WarehouseID
                                                  AND tran.InventoryItemID = d.InventoryItemID
                                                  AND tran.CustomerID      = d.CustomerID
                                                  AND tran.OrderID         = d.OrderID
                                                  AND tran.OrderDetailsID  = d.ID
                                                  AND tran.TypeID          = tt.ID
  WHERE (o.ID = P_OrderID)
    AND (tran.ID IS NULL)
    AND CASE tt.Name WHEN 'Committed'       THEN 1
                     WHEN 'Sold'            THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsSold
                     WHEN 'Rented'          THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented
                     WHEN 'Rental Returned' THEN (o.Approved = 1) AND (o.DeliveryDate IS NOT NULL) AND d.IsRented AND d.IsPickedup
                     ELSE 0 END; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_transaction_po_refresh

### Original MySQL Procedure
```sql
BEGIN
  -- 'Ordered'
  -- 'Received'
  -- 'BackOrdered'
  UPDATE tbl_inventory_transaction as tran
         INNER JOIN tbl_purchaseorderdetails as podetails ON tran.WarehouseID = podetails.WarehouseID
                                                         AND tran.InventoryItemID = podetails.InventoryItemID
                                                         AND tran.PurchaseOrderID = podetails.PurchaseOrderID
                                                         AND tran.PurchaseOrderDetailsID = podetails.ID
         INNER JOIN tbl_purchaseorder as po ON podetails.PurchaseOrderID = po.ID
         INNER JOIN tbl_inventory_transaction_type ON tbl_inventory_transaction_type.ID   = tran.TypeID
                                                  AND tbl_inventory_transaction_type.Name = P_Type
  SET tran.Date = CASE P_Type WHEN 'Ordered'     THEN IFNULL(po.OrderDate   , CURRENT_DATE())
                              WHEN 'Received'    THEN IFNULL(podetails.DateReceived, CURRENT_DATE())
                              WHEN 'BackOrdered' THEN IFNULL(podetails.DateReceived, CURRENT_DATE())
                              ELSE 0 END,
      tran.Quantity = CASE P_Type WHEN 'Ordered'     THEN podetails.Ordered
                                  WHEN 'Received'    THEN podetails.Received
                                  WHEN 'BackOrdered' THEN podetails.Ordered - podetails.Received
                                  ELSE 0 END,
      tran.Cost = CASE P_Type WHEN 'Ordered'     THEN 0
                              WHEN 'Received'    THEN IFNULL(podetails.Received, 0) * IFNULL(podetails.Price, 0)
                              WHEN 'BackOrdered' THEN 0
                              ELSE 0 END,
      tran.Description = CONCAT('PO #', podetails.PurchaseOrderID),
      tran.SerialID = NULL,
      tran.VendorID = po.VendorID,
      tran.CustomerID = NULL,
      tran.InvoiceID  = NULL,
      tran.ManufacturerID = NULL,
      tran.OrderDetailsID = NULL,
      tran.LastUpdateUserID = po.LastUpdateUserID
  WHERE (po.ID = P_PurchaseOrderID)
    AND (po.Approved = 1)
    AND CASE P_Type WHEN 'Ordered'     THEN (0 < podetails.Ordered)
                    WHEN 'Received'    THEN (0 < podetails.Ordered) AND (0 < podetails.Received)
                    WHEN 'BackOrdered' THEN (0 < podetails.Ordered) AND (0 < podetails.Received) -- AND (podetails.Received < podetails.Ordered)
                    ELSE 0 END; --

  INSERT INTO tbl_inventory_transaction
  (WarehouseID,
   InventoryItemID,
   TypeID,
   Date,
   Quantity,
   Cost,
   Description,
   SerialID,
   VendorID,
   CustomerID,
   LastUpdateUserID,
   PurchaseOrderID,
   PurchaseOrderDetailsID,
   InvoiceID,
   ManufacturerID,
   OrderDetailsID)
  SELECT podetails.WarehouseID,
         podetails.InventoryItemID,
         tran_type.ID as TypeID,
         CASE P_Type WHEN 'Ordered'     THEN IFNULL(po.OrderDate   , CURRENT_DATE())
                     WHEN 'Received'    THEN IFNULL(podetails.DateReceived, CURRENT_DATE())
                     WHEN 'BackOrdered' THEN IFNULL(podetails.DateReceived, CURRENT_DATE())
                     ELSE 0 END as Date,
         CASE P_Type WHEN 'Ordered'     THEN podetails.Ordered
                     WHEN 'Received'    THEN podetails.Received
                     WHEN 'BackOrdered' THEN podetails.Ordered - podetails.Received
                     ELSE 0 END as Quantity,
         CASE P_Type WHEN 'Ordered'     THEN 0
                     WHEN 'Received'    THEN IFNULL(podetails.Received, 0) * IFNULL(podetails.Price, 0)
                     WHEN 'BackOrdered' THEN 0
                     ELSE 0 END as Cost,
         CONCAT('PO #', podetails.PurchaseOrderID) as Description,
         NULL as SerialID,
         po.VendorID,
         NULL as CustomerID,
         po.LastUpdateUserID,
         podetails.PurchaseOrderID,
         podetails.ID as PurchaseOrderDetailsID,
         NULL as InvoiceID,
         NULL as ManufacturerID,
         NULL as OrderDetailsID
  FROM tbl_purchaseorderdetails as podetails
       INNER JOIN tbl_purchaseorder as po ON podetails.PurchaseOrderID = po.ID
       INNER JOIN tbl_inventory_transaction_type as tran_type ON tran_type.Name = P_Type
       LEFT JOIN tbl_inventory_transaction as tran ON tran.WarehouseID = podetails.WarehouseID
                                                  AND tran.InventoryItemID = podetails.InventoryItemID
                                                  AND tran.PurchaseOrderID = podetails.PurchaseOrderID
                                                  AND tran.PurchaseOrderDetailsID = podetails.ID
                                                  AND tran.TypeID = tran_type.ID
  WHERE (po.ID = P_PurchaseOrderID)
    AND (po.Approved = 1)
    AND (tran.ID IS NULL)
    AND CASE P_Type WHEN 'Ordered'     THEN (0 < podetails.Ordered)
                    WHEN 'Received'    THEN (0 < podetails.Ordered) AND (0 < podetails.Received)
                    WHEN 'BackOrdered' THEN (0 < podetails.Ordered) AND (0 < podetails.Received) -- AND (podetails.Received < podetails.Ordered)
                    ELSE 0 END; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  -- 'Ordered'
  -- 'Received'
  -- 'BackOrdered'
  UPDATE tbl_inventory_transaction as tran
         INNER JOIN tbl_purchaseorderdetails as podetails ON tran.WarehouseID = podetails.WarehouseID
                                                         AND tran.InventoryItemID = podetails.InventoryItemID
                                                         AND tran.PurchaseOrderID = podetails.PurchaseOrderID
                                                         AND tran.PurchaseOrderDetailsID = podetails.ID
         INNER JOIN tbl_purchaseorder as po ON podetails.PurchaseOrderID = po.ID
         INNER JOIN tbl_inventory_transaction_type ON tbl_inventory_transaction_type.ID   = tran.TypeID
                                                  AND tbl_inventory_transaction_type.Name = P_Type
  SET tran.Date = CASE P_Type WHEN 'Ordered'     THEN COALESCE(po.OrderDate   , CURRENT_DATE())
                              WHEN 'Received'    THEN COALESCE(podetails.DateReceived, CURRENT_DATE())
                              WHEN 'BackOrdered' THEN COALESCE(podetails.DateReceived, CURRENT_DATE())
                              ELSE 0 END,
      tran.Quantity = CASE P_Type WHEN 'Ordered'     THEN podetails.Ordered
                                  WHEN 'Received'    THEN podetails.Received
                                  WHEN 'BackOrdered' THEN podetails.Ordered - podetails.Received
                                  ELSE 0 END,
      tran.Cost = CASE P_Type WHEN 'Ordered'     THEN 0
                              WHEN 'Received'    THEN COALESCE(podetails.Received, 0) * COALESCE(podetails.Price, 0)
                              WHEN 'BackOrdered' THEN 0
                              ELSE 0 END,
      tran.Description = CONCAT('PO #', podetails.PurchaseOrderID),
      tran.SerialID = NULL,
      tran.VendorID = po.VendorID,
      tran.CustomerID = NULL,
      tran.InvoiceID  = NULL,
      tran.ManufacturerID = NULL,
      tran.OrderDetailsID = NULL,
      tran.LastUpdateUserID = po.LastUpdateUserID
  WHERE (po.ID = P_PurchaseOrderID)
    AND (po.Approved = 1)
    AND CASE P_Type WHEN 'Ordered'     THEN (0 < podetails.Ordered)
                    WHEN 'Received'    THEN (0 < podetails.Ordered) AND (0 < podetails.Received)
                    WHEN 'BackOrdered' THEN (0 < podetails.Ordered) AND (0 < podetails.Received) -- AND (podetails.Received < podetails.Ordered)
                    ELSE 0 END; --

  INSERT INTO tbl_inventory_transaction
  (WarehouseID,
   InventoryItemID,
   TypeID,
   Date,
   Quantity,
   Cost,
   Description,
   SerialID,
   VendorID,
   CustomerID,
   LastUpdateUserID,
   PurchaseOrderID,
   PurchaseOrderDetailsID,
   InvoiceID,
   ManufacturerID,
   OrderDetailsID)
  SELECT podetails.WarehouseID,
         podetails.InventoryItemID,
         tran_type.ID as TypeID,
         CASE P_Type WHEN 'Ordered'     THEN COALESCE(po.OrderDate   , CURRENT_DATE())
                     WHEN 'Received'    THEN COALESCE(podetails.DateReceived, CURRENT_DATE())
                     WHEN 'BackOrdered' THEN COALESCE(podetails.DateReceived, CURRENT_DATE())
                     ELSE 0 END as Date,
         CASE P_Type WHEN 'Ordered'     THEN podetails.Ordered
                     WHEN 'Received'    THEN podetails.Received
                     WHEN 'BackOrdered' THEN podetails.Ordered - podetails.Received
                     ELSE 0 END as Quantity,
         CASE P_Type WHEN 'Ordered'     THEN 0
                     WHEN 'Received'    THEN COALESCE(podetails.Received, 0) * COALESCE(podetails.Price, 0)
                     WHEN 'BackOrdered' THEN 0
                     ELSE 0 END as Cost,
         CONCAT('PO #', podetails.PurchaseOrderID) as Description,
         NULL as SerialID,
         po.VendorID,
         NULL as CustomerID,
         po.LastUpdateUserID,
         podetails.PurchaseOrderID,
         podetails.ID as PurchaseOrderDetailsID,
         NULL as InvoiceID,
         NULL as ManufacturerID,
         NULL as OrderDetailsID
  FROM tbl_purchaseorderdetails as podetails
       INNER JOIN tbl_purchaseorder as po ON podetails.PurchaseOrderID = po.ID
       INNER JOIN tbl_inventory_transaction_type as tran_type ON tran_type.Name = P_Type
       LEFT JOIN tbl_inventory_transaction as tran ON tran.WarehouseID = podetails.WarehouseID
                                                  AND tran.InventoryItemID = podetails.InventoryItemID
                                                  AND tran.PurchaseOrderID = podetails.PurchaseOrderID
                                                  AND tran.PurchaseOrderDetailsID = podetails.ID
                                                  AND tran.TypeID = tran_type.ID
  WHERE (po.ID = P_PurchaseOrderID)
    AND (po.Approved = 1)
    AND (tran.ID IS NULL)
    AND CASE P_Type WHEN 'Ordered'     THEN (0 < podetails.Ordered)
                    WHEN 'Received'    THEN (0 < podetails.Ordered) AND (0 < podetails.Received)
                    WHEN 'BackOrdered' THEN (0 < podetails.Ordered) AND (0 < podetails.Received) -- AND (podetails.Received < podetails.Ordered)
                    ELSE 0 END; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## inventory_transfer

### Original MySQL Procedure
```sql
BEGIN
  CALL internal_inventory_transfer(
    P_InventoryItemID
  , P_SrcWarehouseID
  , P_DstWarehouseID
  , P_Quantity
  , 'Inventory Transfer'
  , P_LastUpdateUserID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL internal_inventory_transfer(
    P_InventoryItemID
  , P_SrcWarehouseID
  , P_DstWarehouseID
  , P_Quantity
  , 'Inventory Transfer'
  , P_LastUpdateUserID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_AddAutoSubmit

### Original MySQL Procedure
```sql
PROC: BEGIN
  DECLARE V_CustomerID, V_InvoiceID, V_InvoiceDetailsID INT; --
  DECLARE V_TransmittedCustomerInsuranceID, V_TransmittedInsuranceCompanyID INT; --
  DECLARE V_Billable DECIMAL(18, 2); --
  DECLARE V_Quantity, V_Count INT; --

  SELECT
   `detail`.CustomerID,
   `detail`.InvoiceID,
   `detail`.ID as InvoiceDetailsID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.ID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.ID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.ID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.ID
        ELSE NULL END AS TransmittedCustomerInsuranceID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.InsuranceCompanyID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.InsuranceCompanyID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.InsuranceCompanyID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.InsuranceCompanyID
        ELSE NULL END AS TransmittedInsuranceCompanyID,
   `detail`.BillableAmount,
   `detail`.Quantity
  INTO
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_TransmittedCustomerInsuranceID,
    V_TransmittedInsuranceCompanyID,
    V_Billable,
    V_Quantity
  FROM tbl_invoicedetails as `detail`
       INNER JOIN tbl_invoice as `invoice` ON `detail`.InvoiceID  = `invoice`.ID
                                          AND `detail`.CustomerID = `invoice`.CustomerID
       LEFT JOIN `tbl_customer_insurance` as `ins1` ON `ins1`.ID         = `invoice`.CustomerInsurance1_ID
                                                   AND `ins1`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns1 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins2` ON `ins2`.ID         = `invoice`.CustomerInsurance2_ID
                                                   AND `ins2`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns2 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins3` ON `ins3`.ID         = `invoice`.CustomerInsurance3_ID
                                                   AND `ins3`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns3 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins4` ON `ins4`.ID         = `invoice`.CustomerInsurance4_ID
                                                   AND `ins4`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns4 = 1
  WHERE (`detail`.ID = P_InvoiceDetailsID); --

  IF (V_CustomerID IS NULL) OR (V_InvoiceID IS NULL) OR (V_InvoiceDetailsID IS NULL) THEN
    SET P_Result = 'InvoiceDetailsID is wrong'; --
    LEAVE PROC; --
  END IF; --

  IF (V_TransmittedCustomerInsuranceID IS NULL) OR (V_TransmittedInsuranceCompanyID IS NULL) THEN
    SET P_Result = 'Autosubmitted Company ID is wrong'; --
    LEAVE PROC; --
  END IF; --

  SELECT COUNT(*)
  INTO V_Count
  FROM tbl_invoice_transaction as it
       INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
  WHERE (tt.Name               = 'Auto Submit'                  )
    AND (it.CustomerID         = V_CustomerID                   )
    AND (it.InvoiceID          = V_InvoiceID                    )
    AND (it.InvoiceDetailsID   = V_InvoiceDetailsID             )
    AND (it.InsuranceCompanyID = V_TransmittedInsuranceCompanyID); --

  IF 0 < V_Count THEN
    SET P_Result = 'Transaction already exists'; --
    LEAVE PROC; --
  END IF; --

  INSERT INTO tbl_invoice_transaction (
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , TransactionDate
  , Amount
  , Quantity
  , Taxes
  , BatchNumber
  , Comments
  , Extra
  , Approved
  , LastUpdateUserID)
  SELECT
    V_InvoiceDetailsID
  , V_InvoiceID
  , V_CustomerID
  , V_TransmittedInsuranceCompanyID
  , V_TransmittedCustomerInsuranceID
  , ID as TransactionTypeID
  , P_TransactionDate
  , V_Billable as Amount
  , V_Quantity as Quantity
  , 0.00       as Taxes
  , ''         as BatchNumber
  , 'EDI'      as Comments
  , null       as Extra
  , 1          as Approved
  , P_LastUpdateUserID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Auto Submit'); --

  SET P_Result = 'Success'; --
END PROC
```

### Converted PostgreSQL Procedure
```sql
PROC: BEGIN
  DECLARE V_CustomerID, V_InvoiceID, V_InvoiceDetailsID INT; --
  DECLARE V_TransmittedCustomerInsuranceID, V_TransmittedInsuranceCompanyID INT; --
  DECLARE V_Billable DECIMAL(18, 2); --
  DECLARE V_Quantity, V_Count INT; --

  SELECT
   `detail`.CustomerID,
   `detail`.InvoiceID,
   `detail`.ID as InvoiceDetailsID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.ID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.ID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.ID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.ID
        ELSE NULL END AS TransmittedCustomerInsuranceID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.InsuranceCompanyID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.InsuranceCompanyID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.InsuranceCompanyID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.InsuranceCompanyID
        ELSE NULL END AS TransmittedInsuranceCompanyID,
   `detail`.BillableAmount,
   `detail`.Quantity
  INTO
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_TransmittedCustomerInsuranceID,
    V_TransmittedInsuranceCompanyID,
    V_Billable,
    V_Quantity
  FROM tbl_invoicedetails as `detail`
       INNER JOIN tbl_invoice as `invoice` ON `detail`.InvoiceID  = `invoice`.ID
                                          AND `detail`.CustomerID = `invoice`.CustomerID
       LEFT JOIN `tbl_customer_insurance` as `ins1` ON `ins1`.ID         = `invoice`.CustomerInsurance1_ID
                                                   AND `ins1`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns1 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins2` ON `ins2`.ID         = `invoice`.CustomerInsurance2_ID
                                                   AND `ins2`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns2 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins3` ON `ins3`.ID         = `invoice`.CustomerInsurance3_ID
                                                   AND `ins3`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns3 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins4` ON `ins4`.ID         = `invoice`.CustomerInsurance4_ID
                                                   AND `ins4`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns4 = 1
  WHERE (`detail`.ID = P_InvoiceDetailsID); --

  IF (V_CustomerID IS NULL) OR (V_InvoiceID IS NULL) OR (V_InvoiceDetailsID IS NULL) THEN
    SET P_Result = 'InvoiceDetailsID is wrong'; --
    LEAVE PROC; --
  END IF; --

  IF (V_TransmittedCustomerInsuranceID IS NULL) OR (V_TransmittedInsuranceCompanyID IS NULL) THEN
    SET P_Result = 'Autosubmitted Company ID is wrong'; --
    LEAVE PROC; --
  END IF; --

  SELECT COUNT(*)
  INTO V_Count
  FROM tbl_invoice_transaction as it
       INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
  WHERE (tt.Name               = 'Auto Submit'                  )
    AND (it.CustomerID         = V_CustomerID                   )
    AND (it.InvoiceID          = V_InvoiceID                    )
    AND (it.InvoiceDetailsID   = V_InvoiceDetailsID             )
    AND (it.InsuranceCompanyID = V_TransmittedInsuranceCompanyID); --

  IF 0 < V_Count THEN
    SET P_Result = 'Transaction already exists'; --
    LEAVE PROC; --
  END IF; --

  INSERT INTO tbl_invoice_transaction (
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , TransactionDate
  , Amount
  , Quantity
  , Taxes
  , BatchNumber
  , Comments
  , Extra
  , Approved
  , LastUpdateUserID)
  SELECT
    V_InvoiceDetailsID
  , V_InvoiceID
  , V_CustomerID
  , V_TransmittedInsuranceCompanyID
  , V_TransmittedCustomerInsuranceID
  , ID as TransactionTypeID
  , P_TransactionDate
  , V_Billable as Amount
  , V_Quantity as Quantity
  , 0.00       as Taxes
  , ''         as BatchNumber
  , 'EDI'      as Comments
  , null       as Extra
  , 1          as Approved
  , P_LastUpdateUserID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Auto Submit'); --

  SET P_Result = 'Success'; --
END PROC
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: MODIFIES SQL DATA

## InvoiceDetails_AddPayment

### Original MySQL Procedure
```sql
PROC: BEGIN
  DECLARE V_CustomerID, V_InvoiceID, V_InvoiceDetailsID, V_CustomerInsuranceID, V_InsuranceCompanyID INT; --
  DECLARE V_BasisAllowable, V_FirstInsurance BOOL; --
  DECLARE V_AllowableAmount, V_BillableAmount DECIMAL(18, 2); --
  DECLARE V_Quantity, V_Count INT; --
  DECLARE V_ExtraPaid, V_ExtraAllowable, V_ExtraDeductible, V_ExtraCheckNumber, V_ExtraPostingGuid, V_ExtraSequestration, V_ExtraContractualWriteoff VARCHAR(18); --
  DECLARE V_NumericRegexp VARCHAR(50); --
  DECLARE V_PaymentPaidAmount, V_PaymentAllowableAmount, V_PaymentDeductibleAmount, V_PaymentSequestrationAmount, V_PaymentContractualWriteoffAmount DECIMAL(18, 2); --

  SET V_ExtraPaid                = ExtractValue(P_Extra, 'values/v[@n="Paid"]/text()'); --
  SET V_ExtraAllowable           = ExtractValue(P_Extra, 'values/v[@n="Allowable"]/text()'); --
  SET V_ExtraCheckNumber         = ExtractValue(P_Extra, 'values/v[@n="CheckNumber"]/text()'); --
  SET V_ExtraPostingGuid         = ExtractValue(P_Extra, 'values/v[@n="PostingGuid"]/text()'); --
  SET V_ExtraDeductible          = ExtractValue(P_Extra, 'values/v[@n="Deductible"]/text()'); --
  SET V_ExtraSequestration       = ExtractValue(P_Extra, 'values/v[@n="Sequestration"]/text()'); --
  SET V_ExtraContractualWriteoff = ExtractValue(P_Extra, 'values/v[@n="ContractualWriteoff"]/text()'); --

  SET V_NumericRegexp = '^(-|\+)?([0-9]+\.[0-9]*|[0-9]*\.[0-9]+|[0-9]+)$'; --

  SET V_PaymentPaidAmount                = CASE WHEN V_ExtraPaid                REGEXP V_NumericRegexp THEN V_ExtraPaid                ELSE NULL END; --
  SET V_PaymentAllowableAmount           = CASE WHEN V_ExtraAllowable           REGEXP V_NumericRegexp THEN V_ExtraAllowable           ELSE NULL END; --
  SET V_PaymentDeductibleAmount          = CASE WHEN V_ExtraDeductible          REGEXP V_NumericRegexp THEN V_ExtraDeductible          ELSE NULL END; --
  SET V_PaymentSequestrationAmount       = CASE WHEN V_ExtraSequestration       REGEXP V_NumericRegexp THEN V_ExtraSequestration       ELSE NULL END; --
  SET V_PaymentContractualWriteoffAmount = CASE WHEN V_ExtraContractualWriteoff REGEXP V_NumericRegexp THEN V_ExtraContractualWriteoff ELSE NULL END; --

  IF (V_PaymentPaidAmount IS NULL) THEN
    SET P_Result = 'Paid amount is not specified'; --
    LEAVE PROC; --
  END IF; --

  SELECT
   `detail`.CustomerID,
   `detail`.InvoiceID,
   `detail`.ID as InvoiceDetailsID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.ID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.ID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.ID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.ID
        ELSE NULL END AS CustomerInsuranceID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.InsuranceCompanyID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.InsuranceCompanyID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.InsuranceCompanyID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.InsuranceCompanyID
        ELSE NULL END AS InsuranceCompanyID,
   CASE WHEN ins1.ID IS NOT NULL THEN ins1.InsuranceCompanyID = P_InsuranceCompanyID
        WHEN ins2.ID IS NOT NULL THEN ins2.InsuranceCompanyID = P_InsuranceCompanyID
        WHEN ins3.ID IS NOT NULL THEN ins3.InsuranceCompanyID = P_InsuranceCompanyID
        WHEN ins4.ID IS NOT NULL THEN ins4.InsuranceCompanyID = P_InsuranceCompanyID
        ELSE 0 END AS FirstInsurance,
   CASE WHEN ins1.ID IS NOT NULL THEN ins1.Basis = 'Allowed'
        WHEN ins2.ID IS NOT NULL THEN ins2.Basis = 'Allowed'
        WHEN ins3.ID IS NOT NULL THEN ins3.Basis = 'Allowed'
        WHEN ins4.ID IS NOT NULL THEN ins4.Basis = 'Allowed'
        ELSE 0 END AS BasisAllowable,
   `detail`.AllowableAmount,
   `detail`.BillableAmount,
   `detail`.Quantity
  INTO
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_CustomerInsuranceID,
    V_InsuranceCompanyID,
    V_FirstInsurance,
    V_BasisAllowable,
    V_AllowableAmount,
    V_BillableAmount,
    V_Quantity
  FROM tbl_invoicedetails as `detail`
       INNER JOIN tbl_invoice as `invoice` ON `detail`.InvoiceID  = `invoice`.ID
                                          AND `detail`.CustomerID = `invoice`.CustomerID
       LEFT JOIN `tbl_customer_insurance` as ins1 ON ins1.ID         = `invoice`.CustomerInsurance1_ID
                                                 AND ins1.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns1 = 1
       LEFT JOIN `tbl_customer_insurance` as ins2 ON ins2.ID         = `invoice`.CustomerInsurance2_ID
                                                 AND ins2.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns2 = 1
       LEFT JOIN `tbl_customer_insurance` as ins3 ON ins3.ID         = `invoice`.CustomerInsurance3_ID
                                                 AND ins3.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns3 = 1
       LEFT JOIN `tbl_customer_insurance` as ins4 ON ins4.ID         = `invoice`.CustomerInsurance4_ID
                                                 AND ins4.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns4 = 1
  WHERE (`detail`.ID = P_InvoiceDetailsID); --

  IF (V_CustomerID IS NULL)
  OR (V_InvoiceID IS NULL)
  OR (V_InvoiceDetailsID IS NULL) THEN
    SET P_Result = 'InvoiceDetailsID is wrong'; --
    LEAVE PROC; --
  END IF; --

  IF ((V_InsuranceCompanyID IS NULL) != (P_InsuranceCompanyID IS NULL)) THEN
    SET P_Result = 'InsuranceCompanyID is wrong'; --
    LEAVE PROC; --
  END IF; --

  IF (V_ExtraCheckNumber != '')
  AND (V_ExtraPostingGuid != '') THEN
    -- if we got both check number and posting guid we have to check that
    -- there are no other payment / denied transactions with same check number but different PostingGuid
    -- that way we allow posting multiple transaction for same checknumber
    -- but prevent autoposting from posting same check (and 835) twice since PostingGuid is used only by auto posting
    SELECT SUM(CASE WHEN ExtractValue(it.Extra, 'values/v[@n="CheckNumber"]/text()') = V_ExtraCheckNumber
                     AND ExtractValue(it.Extra, 'values/v[@n="PostingGuid"]/text()') != V_ExtraPostingGuid
                    THEN 1 ELSE 0 END)
    INTO V_Count
    FROM tbl_invoice_transaction as it
         INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
    WHERE (tt.Name IN ('Denied', 'Payment'))
      AND (it.CustomerID         = V_CustomerID        )
      AND (it.InvoiceID          = V_InvoiceID         )
      AND (it.InvoiceDetailsID   = V_InvoiceDetailsID  )
      AND (it.InsuranceCompanyID = V_InsuranceCompanyID OR (it.InsuranceCompanyID IS NULL AND V_InsuranceCompanyID IS NULL)); --

    IF V_Count != 0 THEN
      SET P_Result = CONCAT('Payment for check# ', V_ExtraCheckNumber, ' does already exist'); --
      LEAVE PROC; --
    END IF; --
  END IF; --

  -- 'Adjust Allowable' - optional
  -- 'Denied' IF Amount = 0 - optional
  -- 'Payment' OTHERWISE
  -- 'Contractual Writeoff'
  -- 'Deductible'
  -- 'Auto Submit'
  -- 'Sequestration Writeoff'
  -- 'Hardship Writeoff'
  -- 'Balance Writeoff' - optional

  IF (0 < FIND_IN_SET('Adjust Allowable', P_Options))
  AND (V_CustomerInsuranceID IS NOT NULL)
  AND (V_InsuranceCompanyID IS NOT NULL)
  AND (V_FirstInsurance = 1)
  AND (0.01 <= ABS(V_PaymentAllowableAmount - V_AllowableAmount)) THEN
    -- we should add transaction only once
    SELECT COUNT(*)
    INTO V_Count
    FROM tbl_invoice_transaction as it
         INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
    WHERE (tt.Name = 'Adjust Allowable')
      AND (it.CustomerID         = V_CustomerID        )
      AND (it.InvoiceID          = V_InvoiceID         )
      AND (it.InvoiceDetailsID   = V_InvoiceDetailsID  )
      AND (it.InsuranceCompanyID = V_InsuranceCompanyID); --

    IF V_Count = 0 THEN
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_InsuranceCompanyID
      , V_CustomerInsuranceID
      , ID   as TransactionTypeID
      , P_TransactionDate
      , V_PaymentAllowableAmount
      , V_Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , P_Comments as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Adjust Allowable'); --

      SET V_AllowableAmount = V_PaymentAllowableAmount; --
    END IF; --
  END IF; --

  IF (0 < FIND_IN_SET('Post Denied', P_Options))
  AND (ABS(V_PaymentPaidAmount) < 0.01) THEN
    -- we allow adding 'denied' transaction many times since they will not affect anything
    INSERT INTO tbl_invoice_transaction (
      InvoiceDetailsID
    , InvoiceID
    , CustomerID
    , InsuranceCompanyID
    , CustomerInsuranceID
    , TransactionTypeID
    , TransactionDate
    , Amount
    , Quantity
    , Taxes
    , BatchNumber
    , Comments
    , Extra
    , Approved
    , LastUpdateUserID)
    SELECT
      V_InvoiceDetailsID
    , V_InvoiceID
    , V_CustomerID
    , V_InsuranceCompanyID
    , V_CustomerInsuranceID
    , ID as TransactionTypeID
    , P_TransactionDate
    , 0.00       as Amount
    , V_Quantity
    , 0.00       as Taxes
    , ''         as BatchNumber
    , P_Comments as Comments
    , P_Extra    as Extra
    , 1          as Approved
    , P_LastUpdateUserID
    FROM tbl_invoice_transactiontype
    WHERE (Name = 'Denied'); --
  ELSE
    INSERT INTO tbl_invoice_transaction (
      InvoiceDetailsID
    , InvoiceID
    , CustomerID
    , InsuranceCompanyID
    , CustomerInsuranceID
    , TransactionTypeID
    , TransactionDate
    , Amount
    , Quantity
    , Taxes
    , BatchNumber
    , Comments
    , Extra
    , Approved
    , LastUpdateUserID)
    SELECT
      V_InvoiceDetailsID
    , V_InvoiceID
    , V_CustomerID
    , V_InsuranceCompanyID
    , V_CustomerInsuranceID
    , ID as TransactionTypeID
    , P_TransactionDate
    , V_PaymentPaidAmount
    , V_Quantity
    , 0.00       as Taxes
    , ''         as BatchNumber
    , P_Comments as Comments
    , P_Extra    as Extra
    , 1          as Approved
    , P_LastUpdateUserID
    FROM tbl_invoice_transactiontype
    WHERE (Name = 'Payment'); --
  END IF; --

  IF (V_CustomerInsuranceID IS NOT NULL)
  AND (V_InsuranceCompanyID IS NOT NULL) THEN
    IF (0.01 <= ABS(V_PaymentSequestrationAmount)) THEN
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_InsuranceCompanyID
      , V_CustomerInsuranceID
      , ID as TransactionTypeID
      , P_TransactionDate
      , V_PaymentSequestrationAmount
      , V_Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , 'Sequestration Writeoff' as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Writeoff'); --
    END IF; --

    IF (V_FirstInsurance = 1)
    AND (0.01 <= ABS(V_PaymentContractualWriteoffAmount)) THEN
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_InsuranceCompanyID
      , V_CustomerInsuranceID
      , ID as TransactionTypeID
      , P_TransactionDate
      , V_PaymentContractualWriteoffAmount
      , V_Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , P_Comments as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Contractual Writeoff'); --
    ELSEIF (V_FirstInsurance = 1)
    AND (V_BasisAllowable = 1)
    AND (0.01 <= V_BillableAmount - V_AllowableAmount) THEN
      SELECT COUNT(*)
      INTO V_Count
      FROM tbl_invoice_transaction as it
           INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
      WHERE (tt.Name               = 'Contractual Writeoff')
        AND (it.CustomerID         = V_CustomerID          )
        AND (it.InvoiceID          = V_InvoiceID           )
        AND (it.InvoiceDetailsID   = V_InvoiceDetailsID    )
        AND (it.InsuranceCompanyID = V_InsuranceCompanyID  ); --

      IF V_Count = 0 THEN
        INSERT INTO tbl_invoice_transaction (
          InvoiceDetailsID
        , InvoiceID
        , CustomerID
        , InsuranceCompanyID
        , CustomerInsuranceID
        , TransactionTypeID
        , TransactionDate
        , Amount
        , Quantity
        , Taxes
        , BatchNumber
        , Comments
        , Extra
        , Approved
        , LastUpdateUserID)
        SELECT
          V_InvoiceDetailsID
        , V_InvoiceID
        , V_CustomerID
        , V_InsuranceCompanyID
        , V_CustomerInsuranceID
        , ID as TransactionTypeID
        , P_TransactionDate
        , V_BillableAmount - V_AllowableAmount
        , V_Quantity
        , 0.00       as Taxes
        , ''         as BatchNumber
        , P_Comments as Comments
        , null       as Extra
        , 1          as Approved
        , P_LastUpdateUserID
        FROM tbl_invoice_transactiontype
        WHERE (Name = 'Contractual Writeoff'); --
      END IF; --
    END IF; --

    IF (V_FirstInsurance = 1)
    AND (0.01 <= V_PaymentDeductibleAmount) THEN
      SELECT COUNT(*)
      INTO V_Count
      FROM tbl_invoice_transaction as it
           INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
      WHERE (tt.Name               = 'Deductible'        )
        AND (it.CustomerID         = V_CustomerID        )
        AND (it.InvoiceID          = V_InvoiceID         )
        AND (it.InvoiceDetailsID   = V_InvoiceDetailsID  )
        AND (it.InsuranceCompanyID = V_InsuranceCompanyID); --

      IF V_Count = 0 THEN
        INSERT INTO tbl_invoice_transaction (
          InvoiceDetailsID
        , InvoiceID
        , CustomerID
        , InsuranceCompanyID
        , CustomerInsuranceID
        , TransactionTypeID
        , TransactionDate
        , Amount
        , Quantity
        , Taxes
        , BatchNumber
        , Comments
        , Extra
        , Approved
        , LastUpdateUserID)
        SELECT
          V_InvoiceDetailsID
        , V_InvoiceID
        , V_CustomerID
        , V_InsuranceCompanyID
        , V_CustomerInsuranceID
        , ID as TransactionTypeID
        , P_TransactionDate
        , V_PaymentDeductibleAmount
        , V_Quantity
        , 0.00       as Taxes
        , ''         as BatchNumber
        , P_Comments as Comments
        , null       as Extra
        , 1          as Approved
        , P_LastUpdateUserID
        FROM tbl_invoice_transactiontype
        WHERE (Name = 'Deductible'); --
      END IF; --
    END IF; --
  END IF; --

  CALL InvoiceDetails_RecalculateInternals_Single(null, P_InvoiceDetailsID); --
  -- for the following operations we need updated balance so we need to recalculate it

  INSERT INTO tbl_invoice_transaction (
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , TransactionDate
  , Amount
  , Quantity
  , Comments
  , Taxes
  , BatchNumber
  , Extra
  , Approved
  , LastUpdateUserID)
  SELECT
    det.ID as InvoiceDetailsID
  , det.InvoiceID
  , det.CustomerID
  , det.CurrentInsuranceCompanyID
  , det.CurrentCustomerInsuranceID
  , itt.ID as TransactionTypeID
  , NOW() as TransactionDate
  , det.Balance
  , det.Quantity
  , CASE WHEN det.Hardship = 1 THEN 'Hardship Writeoff' ELSE CONCAT('Wrote off by ', IFNULL(usr.Login, '?')) END AS Comments
  , 0.00 as Taxes
  , ''   as BatchNumber
  , null as Extra
  , 1    as Approved
  , P_LastUpdateUserID
  FROM tbl_invoicedetails as det
       INNER JOIN tbl_invoice_transactiontype as itt ON itt.Name = 'Writeoff'
       LEFT JOIN tbl_user as usr ON usr.ID = P_LastUpdateUserID
  WHERE (det.ID = P_InvoiceDetailsID)
    AND ((det.Hardship = 1 AND det.CurrentPayer = 'Patient') OR (0 < FIND_IN_SET('Writeoff Balance', P_Options)))
    AND (0.01 <= det.Balance); --

  IF (ROW_COUNT() != 0) THEN
    CALL InvoiceDetails_RecalculateInternals_Single(null, P_InvoiceDetailsID); --
  END IF; --

  SET P_Result = 'Success'; --
END PROC
```

### Converted PostgreSQL Procedure
```sql
PROC: BEGIN
  DECLARE V_CustomerID, V_InvoiceID, V_InvoiceDetailsID, V_CustomerInsuranceID, V_InsuranceCompanyID INT; --
  DECLARE V_BasisAllowable, V_FirstInsurance BOOL; --
  DECLARE V_AllowableAmount, V_BillableAmount DECIMAL(18, 2); --
  DECLARE V_Quantity, V_Count INT; --
  DECLARE V_ExtraPaid, V_ExtraAllowable, V_ExtraDeductible, V_ExtraCheckNumber, V_ExtraPostingGuid, V_ExtraSequestration, V_ExtraContractualWriteoff VARCHAR(18); --
  DECLARE V_NumericRegexp VARCHAR(50); --
  DECLARE V_PaymentPaidAmount, V_PaymentAllowableAmount, V_PaymentDeductibleAmount, V_PaymentSequestrationAmount, V_PaymentContractualWriteoffAmount DECIMAL(18, 2); --

  SET V_ExtraPaid                = ExtractValue(P_Extra, 'values/v[@n="Paid"]/text()'); --
  SET V_ExtraAllowable           = ExtractValue(P_Extra, 'values/v[@n="Allowable"]/text()'); --
  SET V_ExtraCheckNumber         = ExtractValue(P_Extra, 'values/v[@n="CheckNumber"]/text()'); --
  SET V_ExtraPostingGuid         = ExtractValue(P_Extra, 'values/v[@n="PostingGuid"]/text()'); --
  SET V_ExtraDeductible          = ExtractValue(P_Extra, 'values/v[@n="Deductible"]/text()'); --
  SET V_ExtraSequestration       = ExtractValue(P_Extra, 'values/v[@n="Sequestration"]/text()'); --
  SET V_ExtraContractualWriteoff = ExtractValue(P_Extra, 'values/v[@n="ContractualWriteoff"]/text()'); --

  SET V_NumericRegexp = '^(-|\+)?([0-9]+\.[0-9]*|[0-9]*\.[0-9]+|[0-9]+)$'; --

  SET V_PaymentPaidAmount                = CASE WHEN V_ExtraPaid                REGEXP V_NumericRegexp THEN V_ExtraPaid                ELSE NULL END; --
  SET V_PaymentAllowableAmount           = CASE WHEN V_ExtraAllowable           REGEXP V_NumericRegexp THEN V_ExtraAllowable           ELSE NULL END; --
  SET V_PaymentDeductibleAmount          = CASE WHEN V_ExtraDeductible          REGEXP V_NumericRegexp THEN V_ExtraDeductible          ELSE NULL END; --
  SET V_PaymentSequestrationAmount       = CASE WHEN V_ExtraSequestration       REGEXP V_NumericRegexp THEN V_ExtraSequestration       ELSE NULL END; --
  SET V_PaymentContractualWriteoffAmount = CASE WHEN V_ExtraContractualWriteoff REGEXP V_NumericRegexp THEN V_ExtraContractualWriteoff ELSE NULL END; --

  IF (V_PaymentPaidAmount IS NULL) THEN
    SET P_Result = 'Paid amount is not specified'; --
    LEAVE PROC; --
  END IF; --

  SELECT
   `detail`.CustomerID,
   `detail`.InvoiceID,
   `detail`.ID as InvoiceDetailsID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.ID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.ID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.ID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.ID
        ELSE NULL END AS CustomerInsuranceID,
   CASE WHEN ins1.InsuranceCompanyID = P_InsuranceCompanyID THEN ins1.InsuranceCompanyID
        WHEN ins2.InsuranceCompanyID = P_InsuranceCompanyID THEN ins2.InsuranceCompanyID
        WHEN ins3.InsuranceCompanyID = P_InsuranceCompanyID THEN ins3.InsuranceCompanyID
        WHEN ins4.InsuranceCompanyID = P_InsuranceCompanyID THEN ins4.InsuranceCompanyID
        ELSE NULL END AS InsuranceCompanyID,
   CASE WHEN ins1.ID IS NOT NULL THEN ins1.InsuranceCompanyID = P_InsuranceCompanyID
        WHEN ins2.ID IS NOT NULL THEN ins2.InsuranceCompanyID = P_InsuranceCompanyID
        WHEN ins3.ID IS NOT NULL THEN ins3.InsuranceCompanyID = P_InsuranceCompanyID
        WHEN ins4.ID IS NOT NULL THEN ins4.InsuranceCompanyID = P_InsuranceCompanyID
        ELSE 0 END AS FirstInsurance,
   CASE WHEN ins1.ID IS NOT NULL THEN ins1.Basis = 'Allowed'
        WHEN ins2.ID IS NOT NULL THEN ins2.Basis = 'Allowed'
        WHEN ins3.ID IS NOT NULL THEN ins3.Basis = 'Allowed'
        WHEN ins4.ID IS NOT NULL THEN ins4.Basis = 'Allowed'
        ELSE 0 END AS BasisAllowable,
   `detail`.AllowableAmount,
   `detail`.BillableAmount,
   `detail`.Quantity
  INTO
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_CustomerInsuranceID,
    V_InsuranceCompanyID,
    V_FirstInsurance,
    V_BasisAllowable,
    V_AllowableAmount,
    V_BillableAmount,
    V_Quantity
  FROM tbl_invoicedetails as `detail`
       INNER JOIN tbl_invoice as `invoice` ON `detail`.InvoiceID  = `invoice`.ID
                                          AND `detail`.CustomerID = `invoice`.CustomerID
       LEFT JOIN `tbl_customer_insurance` as ins1 ON ins1.ID         = `invoice`.CustomerInsurance1_ID
                                                 AND ins1.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns1 = 1
       LEFT JOIN `tbl_customer_insurance` as ins2 ON ins2.ID         = `invoice`.CustomerInsurance2_ID
                                                 AND ins2.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns2 = 1
       LEFT JOIN `tbl_customer_insurance` as ins3 ON ins3.ID         = `invoice`.CustomerInsurance3_ID
                                                 AND ins3.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns3 = 1
       LEFT JOIN `tbl_customer_insurance` as ins4 ON ins4.ID         = `invoice`.CustomerInsurance4_ID
                                                 AND ins4.CustomerID = `invoice`.CustomerID
                                                 AND `detail`.BillIns4 = 1
  WHERE (`detail`.ID = P_InvoiceDetailsID); --

  IF (V_CustomerID IS NULL)
  OR (V_InvoiceID IS NULL)
  OR (V_InvoiceDetailsID IS NULL) THEN
    SET P_Result = 'InvoiceDetailsID is wrong'; --
    LEAVE PROC; --
  END IF; --

  IF ((V_InsuranceCompanyID IS NULL) != (P_InsuranceCompanyID IS NULL)) THEN
    SET P_Result = 'InsuranceCompanyID is wrong'; --
    LEAVE PROC; --
  END IF; --

  IF (V_ExtraCheckNumber != '')
  AND (V_ExtraPostingGuid != '') THEN
    -- if we got both check number and posting guid we have to check that
    -- there are no other payment / denied transactions with same check number but different PostingGuid
    -- that way we allow posting multiple transaction for same checknumber
    -- but prevent autoposting from posting same check (and 835) twice since PostingGuid is used only by auto posting
    SELECT SUM(CASE WHEN ExtractValue(it.Extra, 'values/v[@n="CheckNumber"]/text()') = V_ExtraCheckNumber
                     AND ExtractValue(it.Extra, 'values/v[@n="PostingGuid"]/text()') != V_ExtraPostingGuid
                    THEN 1 ELSE 0 END)
    INTO V_Count
    FROM tbl_invoice_transaction as it
         INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
    WHERE (tt.Name IN ('Denied', 'Payment'))
      AND (it.CustomerID         = V_CustomerID        )
      AND (it.InvoiceID          = V_InvoiceID         )
      AND (it.InvoiceDetailsID   = V_InvoiceDetailsID  )
      AND (it.InsuranceCompanyID = V_InsuranceCompanyID OR (it.InsuranceCompanyID IS NULL AND V_InsuranceCompanyID IS NULL)); --

    IF V_Count != 0 THEN
      SET P_Result = CONCAT('Payment for check# ', V_ExtraCheckNumber, ' does already exist'); --
      LEAVE PROC; --
    END IF; --
  END IF; --

  -- 'Adjust Allowable' - optional
  -- 'Denied' IF Amount = 0 - optional
  -- 'Payment' OTHERWISE
  -- 'Contractual Writeoff'
  -- 'Deductible'
  -- 'Auto Submit'
  -- 'Sequestration Writeoff'
  -- 'Hardship Writeoff'
  -- 'Balance Writeoff' - optional

  IF (0 < FIND_IN_SET('Adjust Allowable', P_Options))
  AND (V_CustomerInsuranceID IS NOT NULL)
  AND (V_InsuranceCompanyID IS NOT NULL)
  AND (V_FirstInsurance = 1)
  AND (0.01 <= ABS(V_PaymentAllowableAmount - V_AllowableAmount)) THEN
    -- we should add transaction only once
    SELECT COUNT(*)
    INTO V_Count
    FROM tbl_invoice_transaction as it
         INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
    WHERE (tt.Name = 'Adjust Allowable')
      AND (it.CustomerID         = V_CustomerID        )
      AND (it.InvoiceID          = V_InvoiceID         )
      AND (it.InvoiceDetailsID   = V_InvoiceDetailsID  )
      AND (it.InsuranceCompanyID = V_InsuranceCompanyID); --

    IF V_Count = 0 THEN
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_InsuranceCompanyID
      , V_CustomerInsuranceID
      , ID   as TransactionTypeID
      , P_TransactionDate
      , V_PaymentAllowableAmount
      , V_Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , P_Comments as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Adjust Allowable'); --

      SET V_AllowableAmount = V_PaymentAllowableAmount; --
    END IF; --
  END IF; --

  IF (0 < FIND_IN_SET('Post Denied', P_Options))
  AND (ABS(V_PaymentPaidAmount) < 0.01) THEN
    -- we allow adding 'denied' transaction many times since they will not affect anything
    INSERT INTO tbl_invoice_transaction (
      InvoiceDetailsID
    , InvoiceID
    , CustomerID
    , InsuranceCompanyID
    , CustomerInsuranceID
    , TransactionTypeID
    , TransactionDate
    , Amount
    , Quantity
    , Taxes
    , BatchNumber
    , Comments
    , Extra
    , Approved
    , LastUpdateUserID)
    SELECT
      V_InvoiceDetailsID
    , V_InvoiceID
    , V_CustomerID
    , V_InsuranceCompanyID
    , V_CustomerInsuranceID
    , ID as TransactionTypeID
    , P_TransactionDate
    , 0.00       as Amount
    , V_Quantity
    , 0.00       as Taxes
    , ''         as BatchNumber
    , P_Comments as Comments
    , P_Extra    as Extra
    , 1          as Approved
    , P_LastUpdateUserID
    FROM tbl_invoice_transactiontype
    WHERE (Name = 'Denied'); --
  ELSE
    INSERT INTO tbl_invoice_transaction (
      InvoiceDetailsID
    , InvoiceID
    , CustomerID
    , InsuranceCompanyID
    , CustomerInsuranceID
    , TransactionTypeID
    , TransactionDate
    , Amount
    , Quantity
    , Taxes
    , BatchNumber
    , Comments
    , Extra
    , Approved
    , LastUpdateUserID)
    SELECT
      V_InvoiceDetailsID
    , V_InvoiceID
    , V_CustomerID
    , V_InsuranceCompanyID
    , V_CustomerInsuranceID
    , ID as TransactionTypeID
    , P_TransactionDate
    , V_PaymentPaidAmount
    , V_Quantity
    , 0.00       as Taxes
    , ''         as BatchNumber
    , P_Comments as Comments
    , P_Extra    as Extra
    , 1          as Approved
    , P_LastUpdateUserID
    FROM tbl_invoice_transactiontype
    WHERE (Name = 'Payment'); --
  END IF; --

  IF (V_CustomerInsuranceID IS NOT NULL)
  AND (V_InsuranceCompanyID IS NOT NULL) THEN
    IF (0.01 <= ABS(V_PaymentSequestrationAmount)) THEN
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_InsuranceCompanyID
      , V_CustomerInsuranceID
      , ID as TransactionTypeID
      , P_TransactionDate
      , V_PaymentSequestrationAmount
      , V_Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , 'Sequestration Writeoff' as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Writeoff'); --
    END IF; --

    IF (V_FirstInsurance = 1)
    AND (0.01 <= ABS(V_PaymentContractualWriteoffAmount)) THEN
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_InsuranceCompanyID
      , V_CustomerInsuranceID
      , ID as TransactionTypeID
      , P_TransactionDate
      , V_PaymentContractualWriteoffAmount
      , V_Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , P_Comments as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Contractual Writeoff'); --
    ELSEIF (V_FirstInsurance = 1)
    AND (V_BasisAllowable = 1)
    AND (0.01 <= V_BillableAmount - V_AllowableAmount) THEN
      SELECT COUNT(*)
      INTO V_Count
      FROM tbl_invoice_transaction as it
           INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
      WHERE (tt.Name               = 'Contractual Writeoff')
        AND (it.CustomerID         = V_CustomerID          )
        AND (it.InvoiceID          = V_InvoiceID           )
        AND (it.InvoiceDetailsID   = V_InvoiceDetailsID    )
        AND (it.InsuranceCompanyID = V_InsuranceCompanyID  ); --

      IF V_Count = 0 THEN
        INSERT INTO tbl_invoice_transaction (
          InvoiceDetailsID
        , InvoiceID
        , CustomerID
        , InsuranceCompanyID
        , CustomerInsuranceID
        , TransactionTypeID
        , TransactionDate
        , Amount
        , Quantity
        , Taxes
        , BatchNumber
        , Comments
        , Extra
        , Approved
        , LastUpdateUserID)
        SELECT
          V_InvoiceDetailsID
        , V_InvoiceID
        , V_CustomerID
        , V_InsuranceCompanyID
        , V_CustomerInsuranceID
        , ID as TransactionTypeID
        , P_TransactionDate
        , V_BillableAmount - V_AllowableAmount
        , V_Quantity
        , 0.00       as Taxes
        , ''         as BatchNumber
        , P_Comments as Comments
        , null       as Extra
        , 1          as Approved
        , P_LastUpdateUserID
        FROM tbl_invoice_transactiontype
        WHERE (Name = 'Contractual Writeoff'); --
      END IF; --
    END IF; --

    IF (V_FirstInsurance = 1)
    AND (0.01 <= V_PaymentDeductibleAmount) THEN
      SELECT COUNT(*)
      INTO V_Count
      FROM tbl_invoice_transaction as it
           INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
      WHERE (tt.Name               = 'Deductible'        )
        AND (it.CustomerID         = V_CustomerID        )
        AND (it.InvoiceID          = V_InvoiceID         )
        AND (it.InvoiceDetailsID   = V_InvoiceDetailsID  )
        AND (it.InsuranceCompanyID = V_InsuranceCompanyID); --

      IF V_Count = 0 THEN
        INSERT INTO tbl_invoice_transaction (
          InvoiceDetailsID
        , InvoiceID
        , CustomerID
        , InsuranceCompanyID
        , CustomerInsuranceID
        , TransactionTypeID
        , TransactionDate
        , Amount
        , Quantity
        , Taxes
        , BatchNumber
        , Comments
        , Extra
        , Approved
        , LastUpdateUserID)
        SELECT
          V_InvoiceDetailsID
        , V_InvoiceID
        , V_CustomerID
        , V_InsuranceCompanyID
        , V_CustomerInsuranceID
        , ID as TransactionTypeID
        , P_TransactionDate
        , V_PaymentDeductibleAmount
        , V_Quantity
        , 0.00       as Taxes
        , ''         as BatchNumber
        , P_Comments as Comments
        , null       as Extra
        , 1          as Approved
        , P_LastUpdateUserID
        FROM tbl_invoice_transactiontype
        WHERE (Name = 'Deductible'); --
      END IF; --
    END IF; --
  END IF; --

  CALL InvoiceDetails_RecalculateInternals_Single(null, P_InvoiceDetailsID); --
  -- for the following operations we need updated balance so we need to recalculate it

  INSERT INTO tbl_invoice_transaction (
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , TransactionDate
  , Amount
  , Quantity
  , Comments
  , Taxes
  , BatchNumber
  , Extra
  , Approved
  , LastUpdateUserID)
  SELECT
    det.ID as InvoiceDetailsID
  , det.InvoiceID
  , det.CustomerID
  , det.CurrentInsuranceCompanyID
  , det.CurrentCustomerInsuranceID
  , itt.ID as TransactionTypeID
  , CURRENT_TIMESTAMP as TransactionDate
  , det.Balance
  , det.Quantity
  , CASE WHEN det.Hardship = 1 THEN 'Hardship Writeoff' ELSE CONCAT('Wrote off by ', COALESCE(usr.Login, '?')) END AS Comments
  , 0.00 as Taxes
  , ''   as BatchNumber
  , null as Extra
  , 1    as Approved
  , P_LastUpdateUserID
  FROM tbl_invoicedetails as det
       INNER JOIN tbl_invoice_transactiontype as itt ON itt.Name = 'Writeoff'
       LEFT JOIN tbl_user as usr ON usr.ID = P_LastUpdateUserID
  WHERE (det.ID = P_InvoiceDetailsID)
    AND ((det.Hardship = 1 AND det.CurrentPayer = 'Patient') OR (0 < FIND_IN_SET('Writeoff Balance', P_Options)))
    AND (0.01 <= det.Balance); --

  IF (ROW_COUNT() != 0) THEN
    CALL InvoiceDetails_RecalculateInternals_Single(null, P_InvoiceDetailsID); --
  END IF; --

  SET P_Result = 'Success'; --
END PROC
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: MODIFIES SQL DATA

## InvoiceDetails_AddSubmitted

### Original MySQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_InternalAddSubmitted(P_InvoiceDetailsID, P_Amount, P_SubmittedTo, P_SubmittedBy, P_SubmittedBatch, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals_Single(null, P_InvoiceDetailsID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_InternalAddSubmitted(P_InvoiceDetailsID, P_Amount, P_SubmittedTo, P_SubmittedBy, P_SubmittedBatch, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals_Single(null, P_InvoiceDetailsID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_InternalAddAutoSubmit

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_CustomerID, V_InvoiceID, V_InvoiceDetailsID INT; --
  DECLARE V_TransmittedCustomerInsuranceID, V_TransmittedInsuranceCompanyID INT; --
  DECLARE V_Billable DECIMAL(18, 2); --
  DECLARE V_Quantity, V_Count INT; --

  SELECT
   `detail`.CustomerID,
   `detail`.InvoiceID,
   `detail`.ID as InvoiceDetailsID,
   CASE WHEN P_AutoSubmittedTo = 'Ins1' THEN `ins1`.ID
        WHEN P_AutoSubmittedTo = 'Ins2' THEN `ins2`.ID
        WHEN P_AutoSubmittedTo = 'Ins3' THEN `ins3`.ID
        WHEN P_AutoSubmittedTo = 'Ins4' THEN `ins4`.ID
        ELSE NULL END AS TransmittedCustomerInsuranceID,
   CASE WHEN P_AutoSubmittedTo = 'Ins1' THEN `ins1`.InsuranceCompanyID
        WHEN P_AutoSubmittedTo = 'Ins2' THEN `ins2`.InsuranceCompanyID
        WHEN P_AutoSubmittedTo = 'Ins3' THEN `ins3`.InsuranceCompanyID
        WHEN P_AutoSubmittedTo = 'Ins4' THEN `ins4`.InsuranceCompanyID
        ELSE NULL END AS TransmittedInsuranceCompanyID,
   `detail`.BillableAmount,
   `detail`.Quantity
  INTO
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_TransmittedCustomerInsuranceID,
    V_TransmittedInsuranceCompanyID,
    V_Billable,
    V_Quantity
  FROM tbl_invoicedetails as `detail`
       INNER JOIN tbl_invoice as `invoice` ON `detail`.InvoiceID  = `invoice`.ID
                                          AND `detail`.CustomerID = `invoice`.CustomerID
       LEFT JOIN `tbl_customer_insurance` as `ins1` ON `ins1`.ID         = `invoice`.CustomerInsurance1_ID
                                                   AND `ins1`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns1 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins2` ON `ins2`.ID         = `invoice`.CustomerInsurance2_ID
                                                   AND `ins2`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns2 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins3` ON `ins3`.ID         = `invoice`.CustomerInsurance3_ID
                                                   AND `ins3`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns3 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins4` ON `ins4`.ID         = `invoice`.CustomerInsurance4_ID
                                                   AND `ins4`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns4 = 1
  WHERE (`detail`.ID = P_InvoiceDetailsID); --

  IF (V_CustomerID IS NULL) OR (V_InvoiceID IS NULL) OR (V_InvoiceDetailsID IS NULL) THEN
    SET P_Result = 'InvoiceDetailsID is wrong'; --
  ELSEIF (V_TransmittedCustomerInsuranceID IS NULL) OR (V_TransmittedInsuranceCompanyID IS NULL) THEN
    SET P_Result = 'Autosubmitted Payer is wrong'; --
  ELSE
    SELECT COUNT(*)
    INTO V_Count
    FROM tbl_invoice_transaction as it
         INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
    WHERE (tt.Name               = 'Auto Submit'                  )
      AND (it.CustomerID         = V_CustomerID                   )
      AND (it.InvoiceID          = V_InvoiceID                    )
      AND (it.InvoiceDetailsID   = V_InvoiceDetailsID             )
      AND (it.InsuranceCompanyID = V_TransmittedInsuranceCompanyID); --

    IF 0 < V_Count THEN
      SET P_Result = 'Transaction already exists'; --
    ELSE
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_TransmittedInsuranceCompanyID
      , V_TransmittedCustomerInsuranceID
      , ID as TransactionTypeID
      , CURRENT_DATE() as TransactionDate
      , V_Billable as Amount
      , V_Quantity as Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , 'Manual'   as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Auto Submit'); --
    END IF; --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_CustomerID, V_InvoiceID, V_InvoiceDetailsID INT; --
  DECLARE V_TransmittedCustomerInsuranceID, V_TransmittedInsuranceCompanyID INT; --
  DECLARE V_Billable DECIMAL(18, 2); --
  DECLARE V_Quantity, V_Count INT; --

  SELECT
   `detail`.CustomerID,
   `detail`.InvoiceID,
   `detail`.ID as InvoiceDetailsID,
   CASE WHEN P_AutoSubmittedTo = 'Ins1' THEN `ins1`.ID
        WHEN P_AutoSubmittedTo = 'Ins2' THEN `ins2`.ID
        WHEN P_AutoSubmittedTo = 'Ins3' THEN `ins3`.ID
        WHEN P_AutoSubmittedTo = 'Ins4' THEN `ins4`.ID
        ELSE NULL END AS TransmittedCustomerInsuranceID,
   CASE WHEN P_AutoSubmittedTo = 'Ins1' THEN `ins1`.InsuranceCompanyID
        WHEN P_AutoSubmittedTo = 'Ins2' THEN `ins2`.InsuranceCompanyID
        WHEN P_AutoSubmittedTo = 'Ins3' THEN `ins3`.InsuranceCompanyID
        WHEN P_AutoSubmittedTo = 'Ins4' THEN `ins4`.InsuranceCompanyID
        ELSE NULL END AS TransmittedInsuranceCompanyID,
   `detail`.BillableAmount,
   `detail`.Quantity
  INTO
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_TransmittedCustomerInsuranceID,
    V_TransmittedInsuranceCompanyID,
    V_Billable,
    V_Quantity
  FROM tbl_invoicedetails as `detail`
       INNER JOIN tbl_invoice as `invoice` ON `detail`.InvoiceID  = `invoice`.ID
                                          AND `detail`.CustomerID = `invoice`.CustomerID
       LEFT JOIN `tbl_customer_insurance` as `ins1` ON `ins1`.ID         = `invoice`.CustomerInsurance1_ID
                                                   AND `ins1`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns1 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins2` ON `ins2`.ID         = `invoice`.CustomerInsurance2_ID
                                                   AND `ins2`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns2 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins3` ON `ins3`.ID         = `invoice`.CustomerInsurance3_ID
                                                   AND `ins3`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns3 = 1
       LEFT JOIN `tbl_customer_insurance` as `ins4` ON `ins4`.ID         = `invoice`.CustomerInsurance4_ID
                                                   AND `ins4`.CustomerID = `invoice`.CustomerID
                                                   AND `detail`.BillIns4 = 1
  WHERE (`detail`.ID = P_InvoiceDetailsID); --

  IF (V_CustomerID IS NULL) OR (V_InvoiceID IS NULL) OR (V_InvoiceDetailsID IS NULL) THEN
    SET P_Result = 'InvoiceDetailsID is wrong'; --
  ELSEIF (V_TransmittedCustomerInsuranceID IS NULL) OR (V_TransmittedInsuranceCompanyID IS NULL) THEN
    SET P_Result = 'Autosubmitted Payer is wrong'; --
  ELSE
    SELECT COUNT(*)
    INTO V_Count
    FROM tbl_invoice_transaction as it
         INNER JOIN tbl_invoice_transactiontype as tt ON it.TransactionTypeID = tt.ID
    WHERE (tt.Name               = 'Auto Submit'                  )
      AND (it.CustomerID         = V_CustomerID                   )
      AND (it.InvoiceID          = V_InvoiceID                    )
      AND (it.InvoiceDetailsID   = V_InvoiceDetailsID             )
      AND (it.InsuranceCompanyID = V_TransmittedInsuranceCompanyID); --

    IF 0 < V_Count THEN
      SET P_Result = 'Transaction already exists'; --
    ELSE
      INSERT INTO tbl_invoice_transaction (
        InvoiceDetailsID
      , InvoiceID
      , CustomerID
      , InsuranceCompanyID
      , CustomerInsuranceID
      , TransactionTypeID
      , TransactionDate
      , Amount
      , Quantity
      , Taxes
      , BatchNumber
      , Comments
      , Extra
      , Approved
      , LastUpdateUserID)
      SELECT
        V_InvoiceDetailsID
      , V_InvoiceID
      , V_CustomerID
      , V_TransmittedInsuranceCompanyID
      , V_TransmittedCustomerInsuranceID
      , ID as TransactionTypeID
      , CURRENT_DATE() as TransactionDate
      , V_Billable as Amount
      , V_Quantity as Quantity
      , 0.00       as Taxes
      , ''         as BatchNumber
      , 'Manual'   as Comments
      , null       as Extra
      , 1          as Approved
      , P_LastUpdateUserID
      FROM tbl_invoice_transactiontype
      WHERE (Name = 'Auto Submit'); --
    END IF; --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: MODIFIES SQL DATA

## InvoiceDetails_InternalAddSubmitted

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_TransactionTypeID INT DEFAULT (0); --

  SELECT ID
  INTO V_TransactionTypeID
  FROM tbl_invoice_transactiontype
  WHERE Name = 'Submit'; --

  IF P_SubmittedTo = 'Patient' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           NULL                  as `InsuranceCompanyID`,
           NULL                  as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM tbl_invoicedetails
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins4' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance4_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins3' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance3_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins2' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance2_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins1' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance1_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_TransactionTypeID INT DEFAULT (0); --

  SELECT ID
  INTO V_TransactionTypeID
  FROM tbl_invoice_transactiontype
  WHERE Name = 'Submit'; --

  IF P_SubmittedTo = 'Patient' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           NULL                  as `InsuranceCompanyID`,
           NULL                  as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM tbl_invoicedetails
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins4' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance4_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins3' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance3_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins2' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance2_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  ELSEIF P_SubmittedTo = 'Ins1' THEN
    INSERT INTO `tbl_invoice_transaction` (
      `InvoiceDetailsID`,
      `InvoiceID`,
      `CustomerID`,
      `InsuranceCompanyID`,
      `CustomerInsuranceID`,
      `TransactionTypeID`,
      `Amount`,
      `Quantity`,
      `TransactionDate`,
      `BatchNumber`,
      `Comments`,
      `LastUpdateUserID`)
    SELECT tbl_invoicedetails.ID as `InvoiceDetailsID`,
           tbl_invoicedetails.InvoiceID,
           tbl_invoicedetails.CustomerID,
           tbl_customer_insurance.InsuranceCompanyID,
           tbl_customer_insurance.ID as `CustomerInsuranceID`,
           V_TransactionTypeID,
           P_Amount,
           tbl_invoicedetails.Quantity,
           CURRENT_DATE() as `TransactionDate`,
           P_SubmittedBatch,
           Concat('Submitted by ', P_SubmittedBy) as `Comments`,
           P_LastUpdateUserID
    FROM ((tbl_invoicedetails
           INNER JOIN tbl_invoice ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                 AND tbl_invoicedetails.InvoiceID = tbl_invoice.ID)
          INNER JOIN tbl_customer_insurance ON tbl_invoice.CustomerID = tbl_customer_insurance.CustomerID
                                           AND tbl_invoice.CustomerInsurance1_ID = tbl_customer_insurance.ID)
    WHERE (tbl_invoicedetails.ID = P_InvoiceDetailsID); --

  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_InternalReflag

### Original MySQL Procedure
```sql
BEGIN
  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE V_TransactionTypeID int DEFAULT 0; --
  DECLARE V_Username VARCHAR(50); --

  SET V_TransactionTypeID = NULL; --
  SELECT ID
  INTO V_TransactionTypeID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Voided Submission'); --

  SET V_Username = ''; --
  SELECT Login
  INTO V_Username
  FROM tbl_user
  WHERE (ID = P_LastUpdateUserID); --

  INSERT INTO tbl_invoice_transaction
    (InvoiceDetailsID
    ,InvoiceID
    ,CustomerID
    ,InsuranceCompanyID
    ,CustomerInsuranceID
    ,TransactionTypeID
    ,Amount
    ,Quantity
    ,TransactionDate
    ,BatchNumber
    ,Comments
    ,LastUpdateUserID)
  SELECT
     InvoiceDetailsID
    ,InvoiceID
    ,CustomerID
    ,CASE CurrentPayer WHEN 'Patient' THEN null
                       WHEN 'Ins4'    THEN InsuranceCompany4_ID
                       WHEN 'Ins3'    THEN InsuranceCompany3_ID
                       WHEN 'Ins2'    THEN InsuranceCompany2_ID
                       WHEN 'Ins1'    THEN InsuranceCompany1_ID
                       ELSE null END as InsuranceCompanyID
    ,CASE CurrentPayer WHEN 'Patient' THEN null
                       WHEN 'Ins4'    THEN Insurance4_ID
                       WHEN 'Ins3'    THEN Insurance3_ID
                       WHEN 'Ins2'    THEN Insurance2_ID
                       WHEN 'Ins1'    THEN Insurance1_ID
                       ELSE null END as CustomerInsuranceID
    ,V_TransactionTypeID as TransactionTypeID
    ,BillableAmount
    ,Quantity
    ,CURRENT_DATE()
    ,null as BatchNumber
    ,Concat('Reflagged by ', V_Username) as Comments
    ,P_LastUpdateUserID as LastUpdateUserID
  FROM view_invoicetransaction_statistics
  WHERE ((0 < FIND_IN_SET(InvoiceID, P_InvoiceID)) OR (P_InvoiceID IS NULL) OR (P_InvoiceID = ''))
    AND ((0 < FIND_IN_SET(InvoiceDetailsID, P_InvoiceDetailsID)) OR (P_InvoiceDetailsID IS NULL) OR (P_InvoiceDetailsID = ''))
    AND ((CurrentPayer = 'Patient' AND Submits & F_Patient != 0) OR
         (CurrentPayer = 'Ins4'    AND Submits & F_Insco_4 != 0) OR
         (CurrentPayer = 'Ins3'    AND Submits & F_Insco_3 != 0) OR
         (CurrentPayer = 'Ins2'    AND Submits & F_Insco_2 != 0) OR
         (CurrentPayer = 'Ins1'    AND Submits & F_Insco_1 != 0)); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE V_TransactionTypeID int DEFAULT 0; --
  DECLARE V_Username VARCHAR(50); --

  SET V_TransactionTypeID = NULL; --
  SELECT ID
  INTO V_TransactionTypeID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Voided Submission'); --

  SET V_Username = ''; --
  SELECT Login
  INTO V_Username
  FROM tbl_user
  WHERE (ID = P_LastUpdateUserID); --

  INSERT INTO tbl_invoice_transaction
    (InvoiceDetailsID
    ,InvoiceID
    ,CustomerID
    ,InsuranceCompanyID
    ,CustomerInsuranceID
    ,TransactionTypeID
    ,Amount
    ,Quantity
    ,TransactionDate
    ,BatchNumber
    ,Comments
    ,LastUpdateUserID)
  SELECT
     InvoiceDetailsID
    ,InvoiceID
    ,CustomerID
    ,CASE CurrentPayer WHEN 'Patient' THEN null
                       WHEN 'Ins4'    THEN InsuranceCompany4_ID
                       WHEN 'Ins3'    THEN InsuranceCompany3_ID
                       WHEN 'Ins2'    THEN InsuranceCompany2_ID
                       WHEN 'Ins1'    THEN InsuranceCompany1_ID
                       ELSE null END as InsuranceCompanyID
    ,CASE CurrentPayer WHEN 'Patient' THEN null
                       WHEN 'Ins4'    THEN Insurance4_ID
                       WHEN 'Ins3'    THEN Insurance3_ID
                       WHEN 'Ins2'    THEN Insurance2_ID
                       WHEN 'Ins1'    THEN Insurance1_ID
                       ELSE null END as CustomerInsuranceID
    ,V_TransactionTypeID as TransactionTypeID
    ,BillableAmount
    ,Quantity
    ,CURRENT_DATE()
    ,null as BatchNumber
    ,Concat('Reflagged by ', V_Username) as Comments
    ,P_LastUpdateUserID as LastUpdateUserID
  FROM view_invoicetransaction_statistics
  WHERE ((0 < FIND_IN_SET(InvoiceID, P_InvoiceID)) OR (P_InvoiceID IS NULL) OR (P_InvoiceID = ''))
    AND ((0 < FIND_IN_SET(InvoiceDetailsID, P_InvoiceDetailsID)) OR (P_InvoiceDetailsID IS NULL) OR (P_InvoiceDetailsID = ''))
    AND ((CurrentPayer = 'Patient' AND Submits & F_Patient != 0) OR
         (CurrentPayer = 'Ins4'    AND Submits & F_Insco_4 != 0) OR
         (CurrentPayer = 'Ins3'    AND Submits & F_Insco_3 != 0) OR
         (CurrentPayer = 'Ins2'    AND Submits & F_Insco_2 != 0) OR
         (CurrentPayer = 'Ins1'    AND Submits & F_Insco_1 != 0)); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_InternalWriteoffBalance

### Original MySQL Procedure
```sql
BEGIN
  INSERT INTO tbl_invoice_transaction (
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , TransactionDate
  , Amount
  , Quantity
  , Comments
  , Taxes
  , BatchNumber
  , Extra
  , Approved
  , LastUpdateUserID)
  SELECT
    det.ID as InvoiceDetailsID
  , det.InvoiceID
  , det.CustomerID
  , det.CurrentInsuranceCompanyID
  , det.CurrentCustomerInsuranceID
  , itt.ID as TransactionTypeID
  , NOW() as TransactionDate
  , det.Balance
  , det.Quantity
  , CONCAT('Wrote off by ', usr.Login) as Comments
  , 0.00 as Taxes
  , ''   as BatchNumber
  , null as Extra
  , 1    as Approved
  , P_LastUpdateUserID
  FROM tbl_invoicedetails as det
       INNER JOIN tbl_invoice_transactiontype as itt ON itt.Name = 'Writeoff'
       LEFT JOIN tbl_user as usr ON usr.ID = P_LastUpdateUserID
  WHERE ((0 < FIND_IN_SET(det.InvoiceID, P_InvoiceID)) OR (P_InvoiceID IS NULL) OR (P_InvoiceID = ''))
    AND ((0 < FIND_IN_SET(det.ID, P_InvoiceDetailsID)) OR (P_InvoiceDetailsID IS NULL) OR (P_InvoiceDetailsID = ''))
    AND (0.01 <= det.Balance); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  INSERT INTO tbl_invoice_transaction (
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , TransactionDate
  , Amount
  , Quantity
  , Comments
  , Taxes
  , BatchNumber
  , Extra
  , Approved
  , LastUpdateUserID)
  SELECT
    det.ID as InvoiceDetailsID
  , det.InvoiceID
  , det.CustomerID
  , det.CurrentInsuranceCompanyID
  , det.CurrentCustomerInsuranceID
  , itt.ID as TransactionTypeID
  , CURRENT_TIMESTAMP as TransactionDate
  , det.Balance
  , det.Quantity
  , CONCAT('Wrote off by ', usr.Login) as Comments
  , 0.00 as Taxes
  , ''   as BatchNumber
  , null as Extra
  , 1    as Approved
  , P_LastUpdateUserID
  FROM tbl_invoicedetails as det
       INNER JOIN tbl_invoice_transactiontype as itt ON itt.Name = 'Writeoff'
       LEFT JOIN tbl_user as usr ON usr.ID = P_LastUpdateUserID
  WHERE ((0 < FIND_IN_SET(det.InvoiceID, P_InvoiceID)) OR (P_InvoiceID IS NULL) OR (P_InvoiceID = ''))
    AND ((0 < FIND_IN_SET(det.ID, P_InvoiceDetailsID)) OR (P_InvoiceDetailsID IS NULL) OR (P_InvoiceDetailsID = ''))
    AND (0.01 <= det.Balance); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: MODIFIES SQL DATA

## InvoiceDetails_RecalculateInternals

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE
    V_PrevCustomerID,
    V_PrevInvoiceID,
    V_PrevDetailsID,
    cur_CustomerID,
    cur_InvoiceID,
    cur_DetailsID,
    cur_TranID INT; --
  -- cursor variables
  DECLARE
    cur_CustomerInsuranceID_1,
    cur_CustomerInsuranceID_2,
    cur_CustomerInsuranceID_3,
    cur_CustomerInsuranceID_4,
    cur_InsuranceCompanyID_1,
    cur_InsuranceCompanyID_2,
    cur_InsuranceCompanyID_3,
    cur_InsuranceCompanyID_4 INT; --
  DECLARE
    V_CustomerInsuranceID_1,
    V_CustomerInsuranceID_2,
    V_CustomerInsuranceID_3,
    V_CustomerInsuranceID_4,
    V_InsuranceCompanyID_1,
    V_InsuranceCompanyID_2,
    V_InsuranceCompanyID_3,
    V_InsuranceCompanyID_4 INT; --
  DECLARE
    cur_TranAmount,
    V_PaymentAmount_Insco_1,
    V_PaymentAmount_Insco_2,
    V_PaymentAmount_Insco_3,
    V_PaymentAmount_Insco_4,
    V_PaymentAmount_Patient,
    V_PaymentAmount,
    V_WriteoffAmount,
    V_DeductibleAmount decimal(18,2); --
  DECLARE
    cur_Percent int; --
  DECLARE
    cur_Basis VARCHAR(7); --
  DECLARE
    cur_TranType VARCHAR(50); --
  DECLARE
    cur_TranOwner,
    cur_Insurances,
    V_ProposedPayer, -- modified by 'Change Current Payee' transaction
    V_CurrentPayer,  -- used only to simplify evaluations
    V_Insurances,    -- insurances available for current line
    V_Pendings,
    V_Submits,
    V_ZeroPayments tinyint; --
  DECLARE
    cur_TranDate,
    V_SubmitDate_1,
    V_SubmitDate_2,
    V_SubmitDate_3,
    V_SubmitDate_4,
    V_SubmitDate_P DATE; --

  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE cur CURSOR FOR
    SELECT
      `detail`.CustomerID,
      `detail`.InvoiceID,
      `detail`.ID as InvoiceDetailsID,
      `tran`.`ID` as TranID,
      `trantype`.`Name` as TranType,
      `tran`.`Amount` as TranAmount,
      `tran`.`TransactionDate` as TranDate,
      CASE WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance1_ID THEN F_Insco_1
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance2_ID THEN F_Insco_2
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance3_ID THEN F_Insco_3
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance4_ID THEN F_Insco_4
           WHEN `tran`.CustomerInsuranceID IS NULL                           THEN F_Patient
           ELSE 0 END AS TranOwner,
      IF((`insurance1`.ID IS NOT NULL) AND (`detail`.BillIns1 = 1) AND (`detail`.NopayIns1 = 0), F_Insco_1, 0) +
      IF((`insurance2`.ID IS NOT NULL) AND (`detail`.BillIns2 = 1), F_Insco_2, 0) +
      IF((`insurance3`.ID IS NOT NULL) AND (`detail`.BillIns3 = 1), F_Insco_3, 0) +
      IF((`insurance4`.ID IS NOT NULL) AND (`detail`.BillIns4 = 1), F_Insco_4, 0) as Insurances,
      `insurance1`.ID as CustomerInsuranceID_1,
      `insurance2`.ID as CustomerInsuranceID_2,
      `insurance3`.ID as CustomerInsuranceID_3,
      `insurance4`.ID as CustomerInsuranceID_4,
      `insurance1`.InsuranceCompanyID as InsuranceCompanyID_1,
      `insurance2`.InsuranceCompanyID as InsuranceCompanyID_2,
      `insurance3`.InsuranceCompanyID as InsuranceCompanyID_3,
      `insurance4`.InsuranceCompanyID as InsuranceCompanyID_4,
       CASE WHEN IFNULL(`insurance1`.PaymentPercent, 0) < 000 THEN 000
            WHEN 100 < IFNULL(`insurance1`.PaymentPercent, 0) THEN 100
            ELSE IFNULL(`insurance1`.PaymentPercent, 0) END as Percent,
       IFNULL(`insurance1`.Basis, 'Bill') as Basis
    FROM tbl_invoicedetails as `detail`
         INNER JOIN tbl_invoice as `invoice` ON `invoice`.CustomerID = `detail`.CustomerID
                                            AND `invoice`.ID         = `detail`.InvoiceID
         LEFT JOIN `tbl_customer_insurance` as `insurance1` ON `insurance1`.ID         = `invoice`.CustomerInsurance1_ID
                                                           AND `insurance1`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance2` ON `insurance2`.ID         = `invoice`.CustomerInsurance2_ID
                                                           AND `insurance2`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance3` ON `insurance3`.ID         = `invoice`.CustomerInsurance3_ID
                                                           AND `insurance3`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance4` ON `insurance4`.ID         = `invoice`.CustomerInsurance4_ID
                                                           AND `insurance4`.CustomerID = `invoice`.CustomerID
         LEFT JOIN tbl_invoice_transaction as `tran` ON `tran`.InvoiceDetailsID = `detail`.ID
                                                    AND `tran`.InvoiceID        = `detail`.InvoiceID
                                                    AND `tran`.CustomerID       = `detail`.CustomerID
         LEFT JOIN tbl_invoice_transactiontype as `trantype` ON `trantype`.ID = `tran`.TransactionTypeID
  WHERE CASE
        WHEN (P_InvoiceID        IS NOT NULL) THEN 0 < FIND_IN_SET(`invoice`.ID, P_InvoiceID)
        WHEN (P_InvoiceDetailsID IS NOT NULL) THEN 0 < FIND_IN_SET(`detail`.ID, P_InvoiceDetailsID)
        ELSE 1 END
  ORDER BY `detail`.CustomerID, `detail`.InvoiceID, `detail`.ID, `tran`.`ID`; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_PrevCustomerID = null; --
  SET V_PrevInvoiceID  = null; --
  SET V_PrevDetailsID  = null; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      cur_CustomerID,
      cur_InvoiceID,
      cur_DetailsID,
      cur_TranID,
      cur_TranType,
      cur_TranAmount,
      cur_TranDate,
      cur_TranOwner,
      cur_Insurances,
      cur_CustomerInsuranceID_1,
      cur_CustomerInsuranceID_2,
      cur_CustomerInsuranceID_3,
      cur_CustomerInsuranceID_4,
      cur_InsuranceCompanyID_1,
      cur_InsuranceCompanyID_2,
      cur_InsuranceCompanyID_3,
      cur_InsuranceCompanyID_4,
      cur_Percent,
      cur_Basis; --

    IF (done != 0)
    OR (V_PrevCustomerID IS NULL) OR (cur_CustomerID != V_PrevCustomerID)
    OR (V_PrevInvoiceID  IS NULL) OR (cur_InvoiceID  != V_PrevInvoiceID)
    OR (V_PrevDetailsID  IS NULL) OR (cur_DetailsID  != V_PrevDetailsID)
    THEN
      IF  (V_PrevCustomerID IS NOT NULL)
      AND (V_PrevInvoiceID  IS NOT NULL)
      AND (V_PrevDetailsID  IS NOT NULL)
      THEN
        -- we must allow changing payer regardless of the payments (zero payments and total amount paid)
        SET V_CurrentPayer
          = CASE WHEN (V_ProposedPayer = F_Insco_1) THEN F_Insco_1
                 WHEN (V_ProposedPayer = F_Insco_2) THEN F_Insco_2
                 WHEN (V_ProposedPayer = F_Insco_3) THEN F_Insco_3
                 WHEN (V_ProposedPayer = F_Insco_4) THEN F_Insco_4
                 WHEN (V_ProposedPayer = F_Patient) THEN F_Patient
                 WHEN (V_Insurances & F_Insco_1 != 0) AND (V_PaymentAmount_Insco_1 < 0.01) AND (V_ZeroPayments & F_Insco_1 = 0) THEN F_Insco_1
                 WHEN (V_Insurances & F_Insco_2 != 0) AND (V_PaymentAmount_Insco_2 < 0.01) AND (V_ZeroPayments & F_Insco_2 = 0) THEN F_Insco_2
                 WHEN (V_Insurances & F_Insco_3 != 0) AND (V_PaymentAmount_Insco_3 < 0.01) AND (V_ZeroPayments & F_Insco_3 = 0) THEN F_Insco_3
                 WHEN (V_Insurances & F_Insco_4 != 0) AND (V_PaymentAmount_Insco_4 < 0.01) AND (V_ZeroPayments & F_Insco_4 = 0) THEN F_Insco_4
                 ELSE F_Patient END; -- we should never switch from patient - somebody must pay --

        -- save into db
        UPDATE tbl_invoicedetails
        SET Balance = BillableAmount - V_PaymentAmount - V_WriteoffAmount,
            PaymentAmount  = V_PaymentAmount,
            WriteoffAmount = V_WriteoffAmount,
            DeductibleAmount = V_DeductibleAmount,
            CurrentPayer
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 'None'
                     WHEN V_CurrentPayer = F_Insco_1 THEN 'Ins1'
                     WHEN V_CurrentPayer = F_Insco_2 THEN 'Ins2'
                     WHEN V_CurrentPayer = F_Insco_3 THEN 'Ins3'
                     WHEN V_CurrentPayer = F_Insco_4 THEN 'Ins4'
                     WHEN V_CurrentPayer = F_Patient THEN 'Patient'
                     ELSE 'None' END,
            SubmittedDate
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_SubmitDate_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_SubmitDate_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_SubmitDate_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_SubmitDate_4
                     WHEN V_CurrentPayer = F_Patient THEN V_SubmitDate_P
                     ELSE null END,
            Submitted
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 1
                     WHEN V_CurrentPayer = F_Insco_1 THEN IF(V_SubmitDate_1 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_2 THEN IF(V_SubmitDate_2 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_3 THEN IF(V_SubmitDate_3 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_4 THEN IF(V_SubmitDate_4 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Patient THEN IF(V_SubmitDate_P IS NOT NULL, 1, 0)
                     ELSE 1 END,
            CurrentInsuranceCompanyID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_InsuranceCompanyID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_InsuranceCompanyID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_InsuranceCompanyID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_InsuranceCompanyID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            CurrentCustomerInsuranceID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_CustomerInsuranceID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_CustomerInsuranceID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_CustomerInsuranceID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_CustomerInsuranceID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            -- for debugging
            Pendings    = V_Pendings,
            Submits     = V_Submits,
            Payments
              = CASE WHEN 0.01 <= V_PaymentAmount_Insco_1 OR V_ZeroPayments & F_Insco_1 != 0 THEN F_Insco_1 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_2 OR V_ZeroPayments & F_Insco_2 != 0 THEN F_Insco_2 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_3 OR V_ZeroPayments & F_Insco_3 != 0 THEN F_Insco_3 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_4 OR V_ZeroPayments & F_Insco_4 != 0 THEN F_Insco_4 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Patient THEN F_Patient ELSE 0 END
        WHERE (CustomerID = V_PrevCustomerID) AND (InvoiceID = V_PrevInvoiceID) AND (ID = V_PrevDetailsID); --
      END IF; --

      -- init / reinit
      SET V_PrevCustomerID = cur_CustomerID; --
      SET V_PrevInvoiceID  = cur_InvoiceID; --
      SET V_PrevDetailsID  = cur_DetailsID; --

      SET V_PaymentAmount_Insco_1 = 0.0; --
      SET V_PaymentAmount_Insco_2 = 0.0; --
      SET V_PaymentAmount_Insco_3 = 0.0; --
      SET V_PaymentAmount_Insco_4 = 0.0; --
      SET V_PaymentAmount_Patient = 0.0; --
      SET V_PaymentAmount  = 0.0; --
      SET V_WriteoffAmount = 0.0; --
      SET V_DeductibleAmount = 0.0; --
      SET V_ProposedPayer = null; --
      SET V_Insurances = cur_Insurances; -- snapshot of insurances available for current line
      SET V_Pendings = 0; --
      SET V_Submits  = 0; --
      SET V_ZeroPayments = 0; --
      SET V_SubmitDate_1 = null; --
      SET V_SubmitDate_2 = null; --
      SET V_SubmitDate_3 = null; --
      SET V_SubmitDate_4 = null; --
      SET V_SubmitDate_P = null; --
      SET V_InsuranceCompanyID_1 = cur_InsuranceCompanyID_1; --
      SET V_InsuranceCompanyID_2 = cur_InsuranceCompanyID_2; --
      SET V_InsuranceCompanyID_3 = cur_InsuranceCompanyID_3; --
      SET V_InsuranceCompanyID_4 = cur_InsuranceCompanyID_4; --
      SET V_CustomerInsuranceID_1 = cur_CustomerInsuranceID_1; --
      SET V_CustomerInsuranceID_2 = cur_CustomerInsuranceID_2; --
      SET V_CustomerInsuranceID_3 = cur_CustomerInsuranceID_3; --
      SET V_CustomerInsuranceID_4 = cur_CustomerInsuranceID_4; --
    END IF; --

    IF (done = 0)
    AND (cur_TranID IS NOT NULL)
    THEN
      -- Only 'Payment' and 'Change Current Payee' changes current payer
      IF (cur_TranType = 'Contractual Writeoff') OR (cur_TranType = 'Writeoff') THEN
        SET V_WriteoffAmount = V_WriteoffAmount + IFNULL(cur_TranAmount, 0); --

      ELSEIF (cur_TranType = 'Submit') OR (cur_TranType = 'Auto Submit') THEN
        SET V_Submits = V_Submits | cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = cur_TranDate; --
        END IF; --

      ELSEIF (cur_TranType = 'Voided Submission') THEN
        SET V_Submits = V_Submits & ~cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = null; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = null; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = null; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = null; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Pending Submission') THEN
        SET V_Pendings = V_Pendings | cur_TranOwner; --

      ELSEIF (cur_TranType = 'Change Current Payee') THEN
        IF (cur_TranOwner = F_Insco_1 and V_Insurances & F_Insco_1 != 0)
        OR (cur_TranOwner = F_Insco_2 and V_Insurances & F_Insco_2 != 0)
        OR (cur_TranOwner = F_Insco_3 and V_Insurances & F_Insco_3 != 0)
        OR (cur_TranOwner = F_Insco_4 and V_Insurances & F_Insco_4 != 0)
        OR (cur_TranOwner = F_Patient)
        THEN
          -- "Change Current Payee" transaction changes responsibility unconditionally
          SET V_ProposedPayer = cur_TranOwner; --
        END IF; --

      ELSEIF (cur_TranType = 'Payment') THEN
        IF (ABS(cur_TranAmount) < 0.01) THEN
          SET V_ZeroPayments = V_ZeroPayments | cur_TranOwner; --
        ELSE
          SET V_ZeroPayments = V_ZeroPayments & ~cur_TranOwner; --
        END IF; --

        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_PaymentAmount_Insco_1 = V_PaymentAmount_Insco_1 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_PaymentAmount_Insco_2 = V_PaymentAmount_Insco_2 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_PaymentAmount_Insco_3 = V_PaymentAmount_Insco_3 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_PaymentAmount_Insco_4 = V_PaymentAmount_Insco_4 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_PaymentAmount_Patient = V_PaymentAmount_Patient + cur_TranAmount; --
        END IF; --

        SET V_PaymentAmount = V_PaymentAmount + cur_TranAmount; --

        IF (cur_TranOwner = V_ProposedPayer)
        AND (0.00 <= cur_TranAmount) THEN
          -- "Payment" transaction (with positive or zero amount) advances responsibility to next payer
          SET V_ProposedPayer = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Deductible') THEN
        -- I guess we should use deductible amount of first insurance only
        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_DeductibleAmount = IFNULL(cur_TranAmount, 0.0); --
        END IF; --

      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE
    V_PrevCustomerID,
    V_PrevInvoiceID,
    V_PrevDetailsID,
    cur_CustomerID,
    cur_InvoiceID,
    cur_DetailsID,
    cur_TranID INT; --
  -- cursor variables
  DECLARE
    cur_CustomerInsuranceID_1,
    cur_CustomerInsuranceID_2,
    cur_CustomerInsuranceID_3,
    cur_CustomerInsuranceID_4,
    cur_InsuranceCompanyID_1,
    cur_InsuranceCompanyID_2,
    cur_InsuranceCompanyID_3,
    cur_InsuranceCompanyID_4 INT; --
  DECLARE
    V_CustomerInsuranceID_1,
    V_CustomerInsuranceID_2,
    V_CustomerInsuranceID_3,
    V_CustomerInsuranceID_4,
    V_InsuranceCompanyID_1,
    V_InsuranceCompanyID_2,
    V_InsuranceCompanyID_3,
    V_InsuranceCompanyID_4 INT; --
  DECLARE
    cur_TranAmount,
    V_PaymentAmount_Insco_1,
    V_PaymentAmount_Insco_2,
    V_PaymentAmount_Insco_3,
    V_PaymentAmount_Insco_4,
    V_PaymentAmount_Patient,
    V_PaymentAmount,
    V_WriteoffAmount,
    V_DeductibleAmount decimal(18,2); --
  DECLARE
    cur_Percent int; --
  DECLARE
    cur_Basis VARCHAR(7); --
  DECLARE
    cur_TranType VARCHAR(50); --
  DECLARE
    cur_TranOwner,
    cur_Insurances,
    V_ProposedPayer, -- modified by 'Change Current Payee' transaction
    V_CurrentPayer,  -- used only to simplify evaluations
    V_Insurances,    -- insurances available for current line
    V_Pendings,
    V_Submits,
    V_ZeroPayments tinyint; --
  DECLARE
    cur_TranDate,
    V_SubmitDate_1,
    V_SubmitDate_2,
    V_SubmitDate_3,
    V_SubmitDate_4,
    V_SubmitDate_P DATE; --

  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE cur CURSOR FOR
    SELECT
      `detail`.CustomerID,
      `detail`.InvoiceID,
      `detail`.ID as InvoiceDetailsID,
      `tran`.`ID` as TranID,
      `trantype`.`Name` as TranType,
      `tran`.`Amount` as TranAmount,
      `tran`.`TransactionDate` as TranDate,
      CASE WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance1_ID THEN F_Insco_1
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance2_ID THEN F_Insco_2
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance3_ID THEN F_Insco_3
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance4_ID THEN F_Insco_4
           WHEN `tran`.CustomerInsuranceID IS NULL                           THEN F_Patient
           ELSE 0 END AS TranOwner,
      IF((`insurance1`.ID IS NOT NULL) AND (`detail`.BillIns1 = 1) AND (`detail`.NopayIns1 = 0), F_Insco_1, 0) +
      IF((`insurance2`.ID IS NOT NULL) AND (`detail`.BillIns2 = 1), F_Insco_2, 0) +
      IF((`insurance3`.ID IS NOT NULL) AND (`detail`.BillIns3 = 1), F_Insco_3, 0) +
      IF((`insurance4`.ID IS NOT NULL) AND (`detail`.BillIns4 = 1), F_Insco_4, 0) as Insurances,
      `insurance1`.ID as CustomerInsuranceID_1,
      `insurance2`.ID as CustomerInsuranceID_2,
      `insurance3`.ID as CustomerInsuranceID_3,
      `insurance4`.ID as CustomerInsuranceID_4,
      `insurance1`.InsuranceCompanyID as InsuranceCompanyID_1,
      `insurance2`.InsuranceCompanyID as InsuranceCompanyID_2,
      `insurance3`.InsuranceCompanyID as InsuranceCompanyID_3,
      `insurance4`.InsuranceCompanyID as InsuranceCompanyID_4,
       CASE WHEN COALESCE(`insurance1`.PaymentPercent, 0) < 000 THEN 000
            WHEN 100 < COALESCE(`insurance1`.PaymentPercent, 0) THEN 100
            ELSE COALESCE(`insurance1`.PaymentPercent, 0) END as Percent,
       COALESCE(`insurance1`.Basis, 'Bill') as Basis
    FROM tbl_invoicedetails as `detail`
         INNER JOIN tbl_invoice as `invoice` ON `invoice`.CustomerID = `detail`.CustomerID
                                            AND `invoice`.ID         = `detail`.InvoiceID
         LEFT JOIN `tbl_customer_insurance` as `insurance1` ON `insurance1`.ID         = `invoice`.CustomerInsurance1_ID
                                                           AND `insurance1`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance2` ON `insurance2`.ID         = `invoice`.CustomerInsurance2_ID
                                                           AND `insurance2`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance3` ON `insurance3`.ID         = `invoice`.CustomerInsurance3_ID
                                                           AND `insurance3`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance4` ON `insurance4`.ID         = `invoice`.CustomerInsurance4_ID
                                                           AND `insurance4`.CustomerID = `invoice`.CustomerID
         LEFT JOIN tbl_invoice_transaction as `tran` ON `tran`.InvoiceDetailsID = `detail`.ID
                                                    AND `tran`.InvoiceID        = `detail`.InvoiceID
                                                    AND `tran`.CustomerID       = `detail`.CustomerID
         LEFT JOIN tbl_invoice_transactiontype as `trantype` ON `trantype`.ID = `tran`.TransactionTypeID
  WHERE CASE
        WHEN (P_InvoiceID        IS NOT NULL) THEN 0 < FIND_IN_SET(`invoice`.ID, P_InvoiceID)
        WHEN (P_InvoiceDetailsID IS NOT NULL) THEN 0 < FIND_IN_SET(`detail`.ID, P_InvoiceDetailsID)
        ELSE 1 END
  ORDER BY `detail`.CustomerID, `detail`.InvoiceID, `detail`.ID, `tran`.`ID`; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_PrevCustomerID = null; --
  SET V_PrevInvoiceID  = null; --
  SET V_PrevDetailsID  = null; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      cur_CustomerID,
      cur_InvoiceID,
      cur_DetailsID,
      cur_TranID,
      cur_TranType,
      cur_TranAmount,
      cur_TranDate,
      cur_TranOwner,
      cur_Insurances,
      cur_CustomerInsuranceID_1,
      cur_CustomerInsuranceID_2,
      cur_CustomerInsuranceID_3,
      cur_CustomerInsuranceID_4,
      cur_InsuranceCompanyID_1,
      cur_InsuranceCompanyID_2,
      cur_InsuranceCompanyID_3,
      cur_InsuranceCompanyID_4,
      cur_Percent,
      cur_Basis; --

    IF (done != 0)
    OR (V_PrevCustomerID IS NULL) OR (cur_CustomerID != V_PrevCustomerID)
    OR (V_PrevInvoiceID  IS NULL) OR (cur_InvoiceID  != V_PrevInvoiceID)
    OR (V_PrevDetailsID  IS NULL) OR (cur_DetailsID  != V_PrevDetailsID)
    THEN
      IF  (V_PrevCustomerID IS NOT NULL)
      AND (V_PrevInvoiceID  IS NOT NULL)
      AND (V_PrevDetailsID  IS NOT NULL)
      THEN
        -- we must allow changing payer regardless of the payments (zero payments and total amount paid)
        SET V_CurrentPayer
          = CASE WHEN (V_ProposedPayer = F_Insco_1) THEN F_Insco_1
                 WHEN (V_ProposedPayer = F_Insco_2) THEN F_Insco_2
                 WHEN (V_ProposedPayer = F_Insco_3) THEN F_Insco_3
                 WHEN (V_ProposedPayer = F_Insco_4) THEN F_Insco_4
                 WHEN (V_ProposedPayer = F_Patient) THEN F_Patient
                 WHEN (V_Insurances & F_Insco_1 != 0) AND (V_PaymentAmount_Insco_1 < 0.01) AND (V_ZeroPayments & F_Insco_1 = 0) THEN F_Insco_1
                 WHEN (V_Insurances & F_Insco_2 != 0) AND (V_PaymentAmount_Insco_2 < 0.01) AND (V_ZeroPayments & F_Insco_2 = 0) THEN F_Insco_2
                 WHEN (V_Insurances & F_Insco_3 != 0) AND (V_PaymentAmount_Insco_3 < 0.01) AND (V_ZeroPayments & F_Insco_3 = 0) THEN F_Insco_3
                 WHEN (V_Insurances & F_Insco_4 != 0) AND (V_PaymentAmount_Insco_4 < 0.01) AND (V_ZeroPayments & F_Insco_4 = 0) THEN F_Insco_4
                 ELSE F_Patient END; -- we should never switch from patient - somebody must pay --

        -- save into db
        UPDATE tbl_invoicedetails
        SET Balance = BillableAmount - V_PaymentAmount - V_WriteoffAmount,
            PaymentAmount  = V_PaymentAmount,
            WriteoffAmount = V_WriteoffAmount,
            DeductibleAmount = V_DeductibleAmount,
            CurrentPayer
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 'None'
                     WHEN V_CurrentPayer = F_Insco_1 THEN 'Ins1'
                     WHEN V_CurrentPayer = F_Insco_2 THEN 'Ins2'
                     WHEN V_CurrentPayer = F_Insco_3 THEN 'Ins3'
                     WHEN V_CurrentPayer = F_Insco_4 THEN 'Ins4'
                     WHEN V_CurrentPayer = F_Patient THEN 'Patient'
                     ELSE 'None' END,
            SubmittedDate
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_SubmitDate_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_SubmitDate_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_SubmitDate_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_SubmitDate_4
                     WHEN V_CurrentPayer = F_Patient THEN V_SubmitDate_P
                     ELSE null END,
            Submitted
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 1
                     WHEN V_CurrentPayer = F_Insco_1 THEN IF(V_SubmitDate_1 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_2 THEN IF(V_SubmitDate_2 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_3 THEN IF(V_SubmitDate_3 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_4 THEN IF(V_SubmitDate_4 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Patient THEN IF(V_SubmitDate_P IS NOT NULL, 1, 0)
                     ELSE 1 END,
            CurrentInsuranceCompanyID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_InsuranceCompanyID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_InsuranceCompanyID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_InsuranceCompanyID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_InsuranceCompanyID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            CurrentCustomerInsuranceID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_CustomerInsuranceID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_CustomerInsuranceID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_CustomerInsuranceID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_CustomerInsuranceID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            -- for debugging
            Pendings    = V_Pendings,
            Submits     = V_Submits,
            Payments
              = CASE WHEN 0.01 <= V_PaymentAmount_Insco_1 OR V_ZeroPayments & F_Insco_1 != 0 THEN F_Insco_1 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_2 OR V_ZeroPayments & F_Insco_2 != 0 THEN F_Insco_2 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_3 OR V_ZeroPayments & F_Insco_3 != 0 THEN F_Insco_3 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_4 OR V_ZeroPayments & F_Insco_4 != 0 THEN F_Insco_4 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Patient THEN F_Patient ELSE 0 END
        WHERE (CustomerID = V_PrevCustomerID) AND (InvoiceID = V_PrevInvoiceID) AND (ID = V_PrevDetailsID); --
      END IF; --

      -- init / reinit
      SET V_PrevCustomerID = cur_CustomerID; --
      SET V_PrevInvoiceID  = cur_InvoiceID; --
      SET V_PrevDetailsID  = cur_DetailsID; --

      SET V_PaymentAmount_Insco_1 = 0.0; --
      SET V_PaymentAmount_Insco_2 = 0.0; --
      SET V_PaymentAmount_Insco_3 = 0.0; --
      SET V_PaymentAmount_Insco_4 = 0.0; --
      SET V_PaymentAmount_Patient = 0.0; --
      SET V_PaymentAmount  = 0.0; --
      SET V_WriteoffAmount = 0.0; --
      SET V_DeductibleAmount = 0.0; --
      SET V_ProposedPayer = null; --
      SET V_Insurances = cur_Insurances; -- snapshot of insurances available for current line
      SET V_Pendings = 0; --
      SET V_Submits  = 0; --
      SET V_ZeroPayments = 0; --
      SET V_SubmitDate_1 = null; --
      SET V_SubmitDate_2 = null; --
      SET V_SubmitDate_3 = null; --
      SET V_SubmitDate_4 = null; --
      SET V_SubmitDate_P = null; --
      SET V_InsuranceCompanyID_1 = cur_InsuranceCompanyID_1; --
      SET V_InsuranceCompanyID_2 = cur_InsuranceCompanyID_2; --
      SET V_InsuranceCompanyID_3 = cur_InsuranceCompanyID_3; --
      SET V_InsuranceCompanyID_4 = cur_InsuranceCompanyID_4; --
      SET V_CustomerInsuranceID_1 = cur_CustomerInsuranceID_1; --
      SET V_CustomerInsuranceID_2 = cur_CustomerInsuranceID_2; --
      SET V_CustomerInsuranceID_3 = cur_CustomerInsuranceID_3; --
      SET V_CustomerInsuranceID_4 = cur_CustomerInsuranceID_4; --
    END IF; --

    IF (done = 0)
    AND (cur_TranID IS NOT NULL)
    THEN
      -- Only 'Payment' and 'Change Current Payee' changes current payer
      IF (cur_TranType = 'Contractual Writeoff') OR (cur_TranType = 'Writeoff') THEN
        SET V_WriteoffAmount = V_WriteoffAmount + COALESCE(cur_TranAmount, 0); --

      ELSEIF (cur_TranType = 'Submit') OR (cur_TranType = 'Auto Submit') THEN
        SET V_Submits = V_Submits | cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = cur_TranDate; --
        END IF; --

      ELSEIF (cur_TranType = 'Voided Submission') THEN
        SET V_Submits = V_Submits & ~cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = null; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = null; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = null; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = null; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Pending Submission') THEN
        SET V_Pendings = V_Pendings | cur_TranOwner; --

      ELSEIF (cur_TranType = 'Change Current Payee') THEN
        IF (cur_TranOwner = F_Insco_1 and V_Insurances & F_Insco_1 != 0)
        OR (cur_TranOwner = F_Insco_2 and V_Insurances & F_Insco_2 != 0)
        OR (cur_TranOwner = F_Insco_3 and V_Insurances & F_Insco_3 != 0)
        OR (cur_TranOwner = F_Insco_4 and V_Insurances & F_Insco_4 != 0)
        OR (cur_TranOwner = F_Patient)
        THEN
          -- "Change Current Payee" transaction changes responsibility unconditionally
          SET V_ProposedPayer = cur_TranOwner; --
        END IF; --

      ELSEIF (cur_TranType = 'Payment') THEN
        IF (ABS(cur_TranAmount) < 0.01) THEN
          SET V_ZeroPayments = V_ZeroPayments | cur_TranOwner; --
        ELSE
          SET V_ZeroPayments = V_ZeroPayments & ~cur_TranOwner; --
        END IF; --

        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_PaymentAmount_Insco_1 = V_PaymentAmount_Insco_1 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_PaymentAmount_Insco_2 = V_PaymentAmount_Insco_2 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_PaymentAmount_Insco_3 = V_PaymentAmount_Insco_3 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_PaymentAmount_Insco_4 = V_PaymentAmount_Insco_4 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_PaymentAmount_Patient = V_PaymentAmount_Patient + cur_TranAmount; --
        END IF; --

        SET V_PaymentAmount = V_PaymentAmount + cur_TranAmount; --

        IF (cur_TranOwner = V_ProposedPayer)
        AND (0.00 <= cur_TranAmount) THEN
          -- "Payment" transaction (with positive or zero amount) advances responsibility to next payer
          SET V_ProposedPayer = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Deductible') THEN
        -- I guess we should use deductible amount of first insurance only
        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_DeductibleAmount = COALESCE(cur_TranAmount, 0.0); --
        END IF; --

      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_RecalculateInternals_Single

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE
    V_PrevCustomerID,
    V_PrevInvoiceID,
    V_PrevDetailsID,
    cur_CustomerID,
    cur_InvoiceID,
    cur_DetailsID,
    cur_TranID INT; --
  -- cursor variables
  DECLARE
    cur_CustomerInsuranceID_1,
    cur_CustomerInsuranceID_2,
    cur_CustomerInsuranceID_3,
    cur_CustomerInsuranceID_4,
    cur_InsuranceCompanyID_1,
    cur_InsuranceCompanyID_2,
    cur_InsuranceCompanyID_3,
    cur_InsuranceCompanyID_4 INT; --
  DECLARE
    V_CustomerInsuranceID_1,
    V_CustomerInsuranceID_2,
    V_CustomerInsuranceID_3,
    V_CustomerInsuranceID_4,
    V_InsuranceCompanyID_1,
    V_InsuranceCompanyID_2,
    V_InsuranceCompanyID_3,
    V_InsuranceCompanyID_4 INT; --
  DECLARE
    cur_TranAmount,
    V_PaymentAmount_Insco_1,
    V_PaymentAmount_Insco_2,
    V_PaymentAmount_Insco_3,
    V_PaymentAmount_Insco_4,
    V_PaymentAmount_Patient,
    V_PaymentAmount,
    V_WriteoffAmount,
    V_DeductibleAmount decimal(18,2); --
  DECLARE
    cur_Percent int; --
  DECLARE
    cur_Basis VARCHAR(7); --
  DECLARE
    cur_TranType VARCHAR(50); --
  DECLARE
    cur_TranOwner,
    cur_Insurances,
    V_ProposedPayer, -- modified by 'Change Current Payee' transaction
    V_CurrentPayer,  -- used only to simplify evaluations
    V_Insurances,    -- insurances available for current line
    V_Pendings,
    V_Submits,
    V_ZeroPayments tinyint; --
  DECLARE
    cur_TranDate,
    V_SubmitDate_1,
    V_SubmitDate_2,
    V_SubmitDate_3,
    V_SubmitDate_4,
    V_SubmitDate_P DATE; --

  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE cur CURSOR FOR
    SELECT
      `detail`.CustomerID,
      `detail`.InvoiceID,
      `detail`.ID as InvoiceDetailsID,
      `tran`.`ID` as TranID,
      `trantype`.`Name` as TranType,
      `tran`.`Amount` as TranAmount,
      `tran`.`TransactionDate` as TranDate,
      CASE WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance1_ID THEN F_Insco_1
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance2_ID THEN F_Insco_2
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance3_ID THEN F_Insco_3
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance4_ID THEN F_Insco_4
           WHEN `tran`.CustomerInsuranceID IS NULL                           THEN F_Patient
           ELSE 0 END AS TranOwner,
      IF((`insurance1`.ID IS NOT NULL) AND (`detail`.BillIns1 = 1) AND (`detail`.NopayIns1 = 0), F_Insco_1, 0) +
      IF((`insurance2`.ID IS NOT NULL) AND (`detail`.BillIns2 = 1), F_Insco_2, 0) +
      IF((`insurance3`.ID IS NOT NULL) AND (`detail`.BillIns3 = 1), F_Insco_3, 0) +
      IF((`insurance4`.ID IS NOT NULL) AND (`detail`.BillIns4 = 1), F_Insco_4, 0) as Insurances,
      `insurance1`.ID as CustomerInsuranceID_1,
      `insurance2`.ID as CustomerInsuranceID_2,
      `insurance3`.ID as CustomerInsuranceID_3,
      `insurance4`.ID as CustomerInsuranceID_4,
      `insurance1`.InsuranceCompanyID as InsuranceCompanyID_1,
      `insurance2`.InsuranceCompanyID as InsuranceCompanyID_2,
      `insurance3`.InsuranceCompanyID as InsuranceCompanyID_3,
      `insurance4`.InsuranceCompanyID as InsuranceCompanyID_4,
       CASE WHEN IFNULL(`insurance1`.PaymentPercent, 0) < 000 THEN 000
            WHEN 100 < IFNULL(`insurance1`.PaymentPercent, 0) THEN 100
            ELSE IFNULL(`insurance1`.PaymentPercent, 0) END as Percent,
       IFNULL(`insurance1`.Basis, 'Bill') as Basis
    FROM tbl_invoicedetails as `detail`
         INNER JOIN tbl_invoice as `invoice` ON `invoice`.CustomerID = `detail`.CustomerID
                                            AND `invoice`.ID         = `detail`.InvoiceID
         LEFT JOIN `tbl_customer_insurance` as `insurance1` ON `insurance1`.ID         = `invoice`.CustomerInsurance1_ID
                                                           AND `insurance1`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance2` ON `insurance2`.ID         = `invoice`.CustomerInsurance2_ID
                                                           AND `insurance2`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance3` ON `insurance3`.ID         = `invoice`.CustomerInsurance3_ID
                                                           AND `insurance3`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance4` ON `insurance4`.ID         = `invoice`.CustomerInsurance4_ID
                                                           AND `insurance4`.CustomerID = `invoice`.CustomerID
         LEFT JOIN tbl_invoice_transaction as `tran` ON `tran`.InvoiceDetailsID = `detail`.ID
                                                    AND `tran`.InvoiceID        = `detail`.InvoiceID
                                                    AND `tran`.CustomerID       = `detail`.CustomerID
         LEFT JOIN tbl_invoice_transactiontype as `trantype` ON `trantype`.ID = `tran`.TransactionTypeID
  WHERE ((P_InvoiceID        IS NULL) OR (`invoice`.ID = P_InvoiceID       ))
    AND ((P_InvoiceDetailsID IS NULL) OR (`detail`.ID  = P_InvoiceDetailsID))
  ORDER BY `detail`.CustomerID, `detail`.InvoiceID, `detail`.ID, `tran`.`ID`; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_PrevCustomerID = null; --
  SET V_PrevInvoiceID  = null; --
  SET V_PrevDetailsID  = null; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      cur_CustomerID,
      cur_InvoiceID,
      cur_DetailsID,
      cur_TranID,
      cur_TranType,
      cur_TranAmount,
      cur_TranDate,
      cur_TranOwner,
      cur_Insurances,
      cur_CustomerInsuranceID_1,
      cur_CustomerInsuranceID_2,
      cur_CustomerInsuranceID_3,
      cur_CustomerInsuranceID_4,
      cur_InsuranceCompanyID_1,
      cur_InsuranceCompanyID_2,
      cur_InsuranceCompanyID_3,
      cur_InsuranceCompanyID_4,
      cur_Percent,
      cur_Basis; --

    IF (done != 0)
    OR (V_PrevCustomerID IS NULL) OR (cur_CustomerID != V_PrevCustomerID)
    OR (V_PrevInvoiceID  IS NULL) OR (cur_InvoiceID  != V_PrevInvoiceID)
    OR (V_PrevDetailsID  IS NULL) OR (cur_DetailsID  != V_PrevDetailsID)
    THEN
      IF  (V_PrevCustomerID IS NOT NULL)
      AND (V_PrevInvoiceID  IS NOT NULL)
      AND (V_PrevDetailsID  IS NOT NULL)
      THEN
        -- we must allow changing payer regardless of the payments (zero payments and total amount paid)
        SET V_CurrentPayer
          = CASE WHEN (V_ProposedPayer = F_Insco_1) THEN F_Insco_1
                 WHEN (V_ProposedPayer = F_Insco_2) THEN F_Insco_2
                 WHEN (V_ProposedPayer = F_Insco_3) THEN F_Insco_3
                 WHEN (V_ProposedPayer = F_Insco_4) THEN F_Insco_4
                 WHEN (V_ProposedPayer = F_Patient) THEN F_Patient
                 WHEN (V_Insurances & F_Insco_1 != 0) AND (V_PaymentAmount_Insco_1 < 0.01) AND (V_ZeroPayments & F_Insco_1 = 0) THEN F_Insco_1
                 WHEN (V_Insurances & F_Insco_2 != 0) AND (V_PaymentAmount_Insco_2 < 0.01) AND (V_ZeroPayments & F_Insco_2 = 0) THEN F_Insco_2
                 WHEN (V_Insurances & F_Insco_3 != 0) AND (V_PaymentAmount_Insco_3 < 0.01) AND (V_ZeroPayments & F_Insco_3 = 0) THEN F_Insco_3
                 WHEN (V_Insurances & F_Insco_4 != 0) AND (V_PaymentAmount_Insco_4 < 0.01) AND (V_ZeroPayments & F_Insco_4 = 0) THEN F_Insco_4
                 ELSE F_Patient END; -- we should never switch from patient - somebody must pay --

        -- save into db
        UPDATE tbl_invoicedetails
        SET Balance = BillableAmount - V_PaymentAmount - V_WriteoffAmount,
            PaymentAmount  = V_PaymentAmount,
            WriteoffAmount = V_WriteoffAmount,
            DeductibleAmount = V_DeductibleAmount,
            CurrentPayer
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 'None'
                     WHEN V_CurrentPayer = F_Insco_1 THEN 'Ins1'
                     WHEN V_CurrentPayer = F_Insco_2 THEN 'Ins2'
                     WHEN V_CurrentPayer = F_Insco_3 THEN 'Ins3'
                     WHEN V_CurrentPayer = F_Insco_4 THEN 'Ins4'
                     WHEN V_CurrentPayer = F_Patient THEN 'Patient'
                     ELSE 'None' END,
            SubmittedDate
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_SubmitDate_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_SubmitDate_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_SubmitDate_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_SubmitDate_4
                     WHEN V_CurrentPayer = F_Patient THEN V_SubmitDate_P
                     ELSE null END,
            Submitted
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 1
                     WHEN V_CurrentPayer = F_Insco_1 THEN IF(V_SubmitDate_1 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_2 THEN IF(V_SubmitDate_2 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_3 THEN IF(V_SubmitDate_3 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_4 THEN IF(V_SubmitDate_4 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Patient THEN IF(V_SubmitDate_P IS NOT NULL, 1, 0)
                     ELSE 1 END,
            CurrentInsuranceCompanyID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_InsuranceCompanyID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_InsuranceCompanyID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_InsuranceCompanyID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_InsuranceCompanyID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            CurrentCustomerInsuranceID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_CustomerInsuranceID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_CustomerInsuranceID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_CustomerInsuranceID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_CustomerInsuranceID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            -- for debugging
            Pendings    = V_Pendings,
            Submits     = V_Submits,
            Payments
              = CASE WHEN 0.01 <= V_PaymentAmount_Insco_1 OR V_ZeroPayments & F_Insco_1 != 0 THEN F_Insco_1 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_2 OR V_ZeroPayments & F_Insco_2 != 0 THEN F_Insco_2 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_3 OR V_ZeroPayments & F_Insco_3 != 0 THEN F_Insco_3 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_4 OR V_ZeroPayments & F_Insco_4 != 0 THEN F_Insco_4 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Patient THEN F_Patient ELSE 0 END
        WHERE (CustomerID = V_PrevCustomerID) AND (InvoiceID = V_PrevInvoiceID) AND (ID = V_PrevDetailsID); --
      END IF; --

      -- init / reinit
      SET V_PrevCustomerID = cur_CustomerID; --
      SET V_PrevInvoiceID  = cur_InvoiceID; --
      SET V_PrevDetailsID  = cur_DetailsID; --

      SET V_PaymentAmount_Insco_1 = 0.0; --
      SET V_PaymentAmount_Insco_2 = 0.0; --
      SET V_PaymentAmount_Insco_3 = 0.0; --
      SET V_PaymentAmount_Insco_4 = 0.0; --
      SET V_PaymentAmount_Patient = 0.0; --
      SET V_PaymentAmount  = 0.0; --
      SET V_WriteoffAmount = 0.0; --
      SET V_DeductibleAmount = 0.0; --
      SET V_ProposedPayer = null; --
      SET V_Insurances = cur_Insurances; -- snapshot of insurances available for current line
      SET V_Pendings = 0; --
      SET V_Submits  = 0; --
      SET V_ZeroPayments = 0; --
      SET V_SubmitDate_1 = null; --
      SET V_SubmitDate_2 = null; --
      SET V_SubmitDate_3 = null; --
      SET V_SubmitDate_4 = null; --
      SET V_SubmitDate_P = null; --
      SET V_InsuranceCompanyID_1 = cur_InsuranceCompanyID_1; --
      SET V_InsuranceCompanyID_2 = cur_InsuranceCompanyID_2; --
      SET V_InsuranceCompanyID_3 = cur_InsuranceCompanyID_3; --
      SET V_InsuranceCompanyID_4 = cur_InsuranceCompanyID_4; --
      SET V_CustomerInsuranceID_1 = cur_CustomerInsuranceID_1; --
      SET V_CustomerInsuranceID_2 = cur_CustomerInsuranceID_2; --
      SET V_CustomerInsuranceID_3 = cur_CustomerInsuranceID_3; --
      SET V_CustomerInsuranceID_4 = cur_CustomerInsuranceID_4; --
    END IF; --

    IF (done = 0)
    AND (cur_TranID IS NOT NULL)
    THEN
      -- Only 'Payment' and 'Change Current Payee' changes current payer
      IF (cur_TranType = 'Contractual Writeoff') OR (cur_TranType = 'Writeoff') THEN
        SET V_WriteoffAmount = V_WriteoffAmount + IFNULL(cur_TranAmount, 0); --

      ELSEIF (cur_TranType = 'Submit') OR (cur_TranType = 'Auto Submit') THEN
        SET V_Submits = V_Submits | cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = cur_TranDate; --
        END IF; --

      ELSEIF (cur_TranType = 'Voided Submission') THEN
        SET V_Submits = V_Submits & ~cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = null; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = null; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = null; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = null; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Pending Submission') THEN
        SET V_Pendings = V_Pendings | cur_TranOwner; --

      ELSEIF (cur_TranType = 'Change Current Payee') THEN
        IF (cur_TranOwner = F_Insco_1 and V_Insurances & F_Insco_1 != 0)
        OR (cur_TranOwner = F_Insco_2 and V_Insurances & F_Insco_2 != 0)
        OR (cur_TranOwner = F_Insco_3 and V_Insurances & F_Insco_3 != 0)
        OR (cur_TranOwner = F_Insco_4 and V_Insurances & F_Insco_4 != 0)
        OR (cur_TranOwner = F_Patient)
        THEN
          -- "Change Current Payee" transaction changes responsibility unconditionally
          SET V_ProposedPayer = cur_TranOwner; --
        END IF; --

      ELSEIF (cur_TranType = 'Payment') THEN
        IF (ABS(cur_TranAmount) < 0.01) THEN
          SET V_ZeroPayments = V_ZeroPayments | cur_TranOwner; --
        ELSE
          SET V_ZeroPayments = V_ZeroPayments & ~cur_TranOwner; --
        END IF; --

        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_PaymentAmount_Insco_1 = V_PaymentAmount_Insco_1 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_PaymentAmount_Insco_2 = V_PaymentAmount_Insco_2 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_PaymentAmount_Insco_3 = V_PaymentAmount_Insco_3 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_PaymentAmount_Insco_4 = V_PaymentAmount_Insco_4 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_PaymentAmount_Patient = V_PaymentAmount_Patient + cur_TranAmount; --
        END IF; --

        SET V_PaymentAmount = V_PaymentAmount + cur_TranAmount; --

        IF (cur_TranOwner = V_ProposedPayer)
        AND (0.00 <= cur_TranAmount) THEN
          -- "Payment" transaction (with positive or zero amount) advances responsibility to next payer
          SET V_ProposedPayer = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Deductible') THEN
        -- I guess we should use deductible amount of first insurance only
        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_DeductibleAmount = IFNULL(cur_TranAmount, 0.0); --
        END IF; --

      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE
    V_PrevCustomerID,
    V_PrevInvoiceID,
    V_PrevDetailsID,
    cur_CustomerID,
    cur_InvoiceID,
    cur_DetailsID,
    cur_TranID INT; --
  -- cursor variables
  DECLARE
    cur_CustomerInsuranceID_1,
    cur_CustomerInsuranceID_2,
    cur_CustomerInsuranceID_3,
    cur_CustomerInsuranceID_4,
    cur_InsuranceCompanyID_1,
    cur_InsuranceCompanyID_2,
    cur_InsuranceCompanyID_3,
    cur_InsuranceCompanyID_4 INT; --
  DECLARE
    V_CustomerInsuranceID_1,
    V_CustomerInsuranceID_2,
    V_CustomerInsuranceID_3,
    V_CustomerInsuranceID_4,
    V_InsuranceCompanyID_1,
    V_InsuranceCompanyID_2,
    V_InsuranceCompanyID_3,
    V_InsuranceCompanyID_4 INT; --
  DECLARE
    cur_TranAmount,
    V_PaymentAmount_Insco_1,
    V_PaymentAmount_Insco_2,
    V_PaymentAmount_Insco_3,
    V_PaymentAmount_Insco_4,
    V_PaymentAmount_Patient,
    V_PaymentAmount,
    V_WriteoffAmount,
    V_DeductibleAmount decimal(18,2); --
  DECLARE
    cur_Percent int; --
  DECLARE
    cur_Basis VARCHAR(7); --
  DECLARE
    cur_TranType VARCHAR(50); --
  DECLARE
    cur_TranOwner,
    cur_Insurances,
    V_ProposedPayer, -- modified by 'Change Current Payee' transaction
    V_CurrentPayer,  -- used only to simplify evaluations
    V_Insurances,    -- insurances available for current line
    V_Pendings,
    V_Submits,
    V_ZeroPayments tinyint; --
  DECLARE
    cur_TranDate,
    V_SubmitDate_1,
    V_SubmitDate_2,
    V_SubmitDate_3,
    V_SubmitDate_4,
    V_SubmitDate_P DATE; --

  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE cur CURSOR FOR
    SELECT
      `detail`.CustomerID,
      `detail`.InvoiceID,
      `detail`.ID as InvoiceDetailsID,
      `tran`.`ID` as TranID,
      `trantype`.`Name` as TranType,
      `tran`.`Amount` as TranAmount,
      `tran`.`TransactionDate` as TranDate,
      CASE WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance1_ID THEN F_Insco_1
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance2_ID THEN F_Insco_2
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance3_ID THEN F_Insco_3
           WHEN `tran`.CustomerInsuranceID = `invoice`.CustomerInsurance4_ID THEN F_Insco_4
           WHEN `tran`.CustomerInsuranceID IS NULL                           THEN F_Patient
           ELSE 0 END AS TranOwner,
      IF((`insurance1`.ID IS NOT NULL) AND (`detail`.BillIns1 = 1) AND (`detail`.NopayIns1 = 0), F_Insco_1, 0) +
      IF((`insurance2`.ID IS NOT NULL) AND (`detail`.BillIns2 = 1), F_Insco_2, 0) +
      IF((`insurance3`.ID IS NOT NULL) AND (`detail`.BillIns3 = 1), F_Insco_3, 0) +
      IF((`insurance4`.ID IS NOT NULL) AND (`detail`.BillIns4 = 1), F_Insco_4, 0) as Insurances,
      `insurance1`.ID as CustomerInsuranceID_1,
      `insurance2`.ID as CustomerInsuranceID_2,
      `insurance3`.ID as CustomerInsuranceID_3,
      `insurance4`.ID as CustomerInsuranceID_4,
      `insurance1`.InsuranceCompanyID as InsuranceCompanyID_1,
      `insurance2`.InsuranceCompanyID as InsuranceCompanyID_2,
      `insurance3`.InsuranceCompanyID as InsuranceCompanyID_3,
      `insurance4`.InsuranceCompanyID as InsuranceCompanyID_4,
       CASE WHEN COALESCE(`insurance1`.PaymentPercent, 0) < 000 THEN 000
            WHEN 100 < COALESCE(`insurance1`.PaymentPercent, 0) THEN 100
            ELSE COALESCE(`insurance1`.PaymentPercent, 0) END as Percent,
       COALESCE(`insurance1`.Basis, 'Bill') as Basis
    FROM tbl_invoicedetails as `detail`
         INNER JOIN tbl_invoice as `invoice` ON `invoice`.CustomerID = `detail`.CustomerID
                                            AND `invoice`.ID         = `detail`.InvoiceID
         LEFT JOIN `tbl_customer_insurance` as `insurance1` ON `insurance1`.ID         = `invoice`.CustomerInsurance1_ID
                                                           AND `insurance1`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance2` ON `insurance2`.ID         = `invoice`.CustomerInsurance2_ID
                                                           AND `insurance2`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance3` ON `insurance3`.ID         = `invoice`.CustomerInsurance3_ID
                                                           AND `insurance3`.CustomerID = `invoice`.CustomerID
         LEFT JOIN `tbl_customer_insurance` as `insurance4` ON `insurance4`.ID         = `invoice`.CustomerInsurance4_ID
                                                           AND `insurance4`.CustomerID = `invoice`.CustomerID
         LEFT JOIN tbl_invoice_transaction as `tran` ON `tran`.InvoiceDetailsID = `detail`.ID
                                                    AND `tran`.InvoiceID        = `detail`.InvoiceID
                                                    AND `tran`.CustomerID       = `detail`.CustomerID
         LEFT JOIN tbl_invoice_transactiontype as `trantype` ON `trantype`.ID = `tran`.TransactionTypeID
  WHERE ((P_InvoiceID        IS NULL) OR (`invoice`.ID = P_InvoiceID       ))
    AND ((P_InvoiceDetailsID IS NULL) OR (`detail`.ID  = P_InvoiceDetailsID))
  ORDER BY `detail`.CustomerID, `detail`.InvoiceID, `detail`.ID, `tran`.`ID`; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_PrevCustomerID = null; --
  SET V_PrevInvoiceID  = null; --
  SET V_PrevDetailsID  = null; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      cur_CustomerID,
      cur_InvoiceID,
      cur_DetailsID,
      cur_TranID,
      cur_TranType,
      cur_TranAmount,
      cur_TranDate,
      cur_TranOwner,
      cur_Insurances,
      cur_CustomerInsuranceID_1,
      cur_CustomerInsuranceID_2,
      cur_CustomerInsuranceID_3,
      cur_CustomerInsuranceID_4,
      cur_InsuranceCompanyID_1,
      cur_InsuranceCompanyID_2,
      cur_InsuranceCompanyID_3,
      cur_InsuranceCompanyID_4,
      cur_Percent,
      cur_Basis; --

    IF (done != 0)
    OR (V_PrevCustomerID IS NULL) OR (cur_CustomerID != V_PrevCustomerID)
    OR (V_PrevInvoiceID  IS NULL) OR (cur_InvoiceID  != V_PrevInvoiceID)
    OR (V_PrevDetailsID  IS NULL) OR (cur_DetailsID  != V_PrevDetailsID)
    THEN
      IF  (V_PrevCustomerID IS NOT NULL)
      AND (V_PrevInvoiceID  IS NOT NULL)
      AND (V_PrevDetailsID  IS NOT NULL)
      THEN
        -- we must allow changing payer regardless of the payments (zero payments and total amount paid)
        SET V_CurrentPayer
          = CASE WHEN (V_ProposedPayer = F_Insco_1) THEN F_Insco_1
                 WHEN (V_ProposedPayer = F_Insco_2) THEN F_Insco_2
                 WHEN (V_ProposedPayer = F_Insco_3) THEN F_Insco_3
                 WHEN (V_ProposedPayer = F_Insco_4) THEN F_Insco_4
                 WHEN (V_ProposedPayer = F_Patient) THEN F_Patient
                 WHEN (V_Insurances & F_Insco_1 != 0) AND (V_PaymentAmount_Insco_1 < 0.01) AND (V_ZeroPayments & F_Insco_1 = 0) THEN F_Insco_1
                 WHEN (V_Insurances & F_Insco_2 != 0) AND (V_PaymentAmount_Insco_2 < 0.01) AND (V_ZeroPayments & F_Insco_2 = 0) THEN F_Insco_2
                 WHEN (V_Insurances & F_Insco_3 != 0) AND (V_PaymentAmount_Insco_3 < 0.01) AND (V_ZeroPayments & F_Insco_3 = 0) THEN F_Insco_3
                 WHEN (V_Insurances & F_Insco_4 != 0) AND (V_PaymentAmount_Insco_4 < 0.01) AND (V_ZeroPayments & F_Insco_4 = 0) THEN F_Insco_4
                 ELSE F_Patient END; -- we should never switch from patient - somebody must pay --

        -- save into db
        UPDATE tbl_invoicedetails
        SET Balance = BillableAmount - V_PaymentAmount - V_WriteoffAmount,
            PaymentAmount  = V_PaymentAmount,
            WriteoffAmount = V_WriteoffAmount,
            DeductibleAmount = V_DeductibleAmount,
            CurrentPayer
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 'None'
                     WHEN V_CurrentPayer = F_Insco_1 THEN 'Ins1'
                     WHEN V_CurrentPayer = F_Insco_2 THEN 'Ins2'
                     WHEN V_CurrentPayer = F_Insco_3 THEN 'Ins3'
                     WHEN V_CurrentPayer = F_Insco_4 THEN 'Ins4'
                     WHEN V_CurrentPayer = F_Patient THEN 'Patient'
                     ELSE 'None' END,
            SubmittedDate
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_SubmitDate_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_SubmitDate_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_SubmitDate_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_SubmitDate_4
                     WHEN V_CurrentPayer = F_Patient THEN V_SubmitDate_P
                     ELSE null END,
            Submitted
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN 1
                     WHEN V_CurrentPayer = F_Insco_1 THEN IF(V_SubmitDate_1 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_2 THEN IF(V_SubmitDate_2 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_3 THEN IF(V_SubmitDate_3 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Insco_4 THEN IF(V_SubmitDate_4 IS NOT NULL, 1, 0)
                     WHEN V_CurrentPayer = F_Patient THEN IF(V_SubmitDate_P IS NOT NULL, 1, 0)
                     ELSE 1 END,
            CurrentInsuranceCompanyID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_InsuranceCompanyID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_InsuranceCompanyID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_InsuranceCompanyID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_InsuranceCompanyID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            CurrentCustomerInsuranceID
              = CASE WHEN BillableAmount - V_PaymentAmount - V_WriteoffAmount < 0.01 THEN null
                     WHEN V_CurrentPayer = F_Insco_1 THEN V_CustomerInsuranceID_1
                     WHEN V_CurrentPayer = F_Insco_2 THEN V_CustomerInsuranceID_2
                     WHEN V_CurrentPayer = F_Insco_3 THEN V_CustomerInsuranceID_3
                     WHEN V_CurrentPayer = F_Insco_4 THEN V_CustomerInsuranceID_4
                     WHEN V_CurrentPayer = F_Patient THEN null
                     ELSE null END,
            -- for debugging
            Pendings    = V_Pendings,
            Submits     = V_Submits,
            Payments
              = CASE WHEN 0.01 <= V_PaymentAmount_Insco_1 OR V_ZeroPayments & F_Insco_1 != 0 THEN F_Insco_1 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_2 OR V_ZeroPayments & F_Insco_2 != 0 THEN F_Insco_2 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_3 OR V_ZeroPayments & F_Insco_3 != 0 THEN F_Insco_3 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Insco_4 OR V_ZeroPayments & F_Insco_4 != 0 THEN F_Insco_4 ELSE 0 END
              + CASE WHEN 0.01 <= V_PaymentAmount_Patient THEN F_Patient ELSE 0 END
        WHERE (CustomerID = V_PrevCustomerID) AND (InvoiceID = V_PrevInvoiceID) AND (ID = V_PrevDetailsID); --
      END IF; --

      -- init / reinit
      SET V_PrevCustomerID = cur_CustomerID; --
      SET V_PrevInvoiceID  = cur_InvoiceID; --
      SET V_PrevDetailsID  = cur_DetailsID; --

      SET V_PaymentAmount_Insco_1 = 0.0; --
      SET V_PaymentAmount_Insco_2 = 0.0; --
      SET V_PaymentAmount_Insco_3 = 0.0; --
      SET V_PaymentAmount_Insco_4 = 0.0; --
      SET V_PaymentAmount_Patient = 0.0; --
      SET V_PaymentAmount  = 0.0; --
      SET V_WriteoffAmount = 0.0; --
      SET V_DeductibleAmount = 0.0; --
      SET V_ProposedPayer = null; --
      SET V_Insurances = cur_Insurances; -- snapshot of insurances available for current line
      SET V_Pendings = 0; --
      SET V_Submits  = 0; --
      SET V_ZeroPayments = 0; --
      SET V_SubmitDate_1 = null; --
      SET V_SubmitDate_2 = null; --
      SET V_SubmitDate_3 = null; --
      SET V_SubmitDate_4 = null; --
      SET V_SubmitDate_P = null; --
      SET V_InsuranceCompanyID_1 = cur_InsuranceCompanyID_1; --
      SET V_InsuranceCompanyID_2 = cur_InsuranceCompanyID_2; --
      SET V_InsuranceCompanyID_3 = cur_InsuranceCompanyID_3; --
      SET V_InsuranceCompanyID_4 = cur_InsuranceCompanyID_4; --
      SET V_CustomerInsuranceID_1 = cur_CustomerInsuranceID_1; --
      SET V_CustomerInsuranceID_2 = cur_CustomerInsuranceID_2; --
      SET V_CustomerInsuranceID_3 = cur_CustomerInsuranceID_3; --
      SET V_CustomerInsuranceID_4 = cur_CustomerInsuranceID_4; --
    END IF; --

    IF (done = 0)
    AND (cur_TranID IS NOT NULL)
    THEN
      -- Only 'Payment' and 'Change Current Payee' changes current payer
      IF (cur_TranType = 'Contractual Writeoff') OR (cur_TranType = 'Writeoff') THEN
        SET V_WriteoffAmount = V_WriteoffAmount + COALESCE(cur_TranAmount, 0); --

      ELSEIF (cur_TranType = 'Submit') OR (cur_TranType = 'Auto Submit') THEN
        SET V_Submits = V_Submits | cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = cur_TranDate; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = cur_TranDate; --
        END IF; --

      ELSEIF (cur_TranType = 'Voided Submission') THEN
        SET V_Submits = V_Submits & ~cur_TranOwner; --
        IF     (cur_TranOwner = F_Insco_1) THEN
          SET V_SubmitDate_1 = null; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_SubmitDate_2 = null; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_SubmitDate_3 = null; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_SubmitDate_4 = null; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_SubmitDate_P = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Pending Submission') THEN
        SET V_Pendings = V_Pendings | cur_TranOwner; --

      ELSEIF (cur_TranType = 'Change Current Payee') THEN
        IF (cur_TranOwner = F_Insco_1 and V_Insurances & F_Insco_1 != 0)
        OR (cur_TranOwner = F_Insco_2 and V_Insurances & F_Insco_2 != 0)
        OR (cur_TranOwner = F_Insco_3 and V_Insurances & F_Insco_3 != 0)
        OR (cur_TranOwner = F_Insco_4 and V_Insurances & F_Insco_4 != 0)
        OR (cur_TranOwner = F_Patient)
        THEN
          -- "Change Current Payee" transaction changes responsibility unconditionally
          SET V_ProposedPayer = cur_TranOwner; --
        END IF; --

      ELSEIF (cur_TranType = 'Payment') THEN
        IF (ABS(cur_TranAmount) < 0.01) THEN
          SET V_ZeroPayments = V_ZeroPayments | cur_TranOwner; --
        ELSE
          SET V_ZeroPayments = V_ZeroPayments & ~cur_TranOwner; --
        END IF; --

        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_PaymentAmount_Insco_1 = V_PaymentAmount_Insco_1 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_2) THEN
          SET V_PaymentAmount_Insco_2 = V_PaymentAmount_Insco_2 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_3) THEN
          SET V_PaymentAmount_Insco_3 = V_PaymentAmount_Insco_3 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Insco_4) THEN
          SET V_PaymentAmount_Insco_4 = V_PaymentAmount_Insco_4 + cur_TranAmount; --
        ELSEIF (cur_TranOwner = F_Patient) THEN
          SET V_PaymentAmount_Patient = V_PaymentAmount_Patient + cur_TranAmount; --
        END IF; --

        SET V_PaymentAmount = V_PaymentAmount + cur_TranAmount; --

        IF (cur_TranOwner = V_ProposedPayer)
        AND (0.00 <= cur_TranAmount) THEN
          -- "Payment" transaction (with positive or zero amount) advances responsibility to next payer
          SET V_ProposedPayer = null; --
        END IF; --

      ELSEIF (cur_TranType = 'Deductible') THEN
        -- I guess we should use deductible amount of first insurance only
        IF (cur_TranOwner = F_Insco_1) THEN
          SET V_DeductibleAmount = COALESCE(cur_TranAmount, 0.0); --
        END IF; --

      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_Reflag

### Original MySQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals(P_InvoiceID, P_InvoiceDetailsID); --
  CALL InvoiceDetails_InternalReflag      (P_InvoiceID, P_InvoiceDetailsID, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals(P_InvoiceID, P_InvoiceDetailsID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals(P_InvoiceID, P_InvoiceDetailsID); --
  CALL InvoiceDetails_InternalReflag      (P_InvoiceID, P_InvoiceDetailsID, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals(P_InvoiceID, P_InvoiceDetailsID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## InvoiceDetails_WriteoffBalance

### Original MySQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals   (P_InvoiceID, P_InvoiceDetailsID); --
  CALL InvoiceDetails_InternalWriteoffBalance(P_InvoiceID, P_InvoiceDetailsID, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals   (P_InvoiceID, P_InvoiceDetailsID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals   (P_InvoiceID, P_InvoiceDetailsID); --
  CALL InvoiceDetails_InternalWriteoffBalance(P_InvoiceID, P_InvoiceDetailsID, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals   (P_InvoiceID, P_InvoiceDetailsID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_AddAutoSubmit

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE V_Result VARCHAR(50); --
  DECLARE cur CURSOR FOR SELECT ID FROM tbl_invoicedetails WHERE (InvoiceID = P_InvoiceID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_InvoiceDetailsID; --
    IF NOT done THEN
      CALL InvoiceDetails_InternalAddAutoSubmit(V_InvoiceDetailsID, P_AutoSubmittedTo, P_LastUpdateUserID, V_Result); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE V_Result VARCHAR(50); --
  DECLARE cur CURSOR FOR SELECT ID FROM tbl_invoicedetails WHERE (InvoiceID = P_InvoiceID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_InvoiceDetailsID; --
    IF NOT done THEN
      CALL InvoiceDetails_InternalAddAutoSubmit(V_InvoiceDetailsID, P_AutoSubmittedTo, P_LastUpdateUserID, V_Result); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_AddSubmitted

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE cur CURSOR FOR SELECT ID FROM tbl_invoicedetails WHERE (InvoiceID = P_InvoiceID) AND (CurrentPayer = P_SubmittedTo); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_InvoiceDetailsID; --
    IF NOT done THEN
      CALL InvoiceDetails_AddSubmitted(V_InvoiceDetailsID, 0.00, P_SubmittedTo, P_SubmittedBy, P_SubmittedBatch, P_LastUpdateUserID); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE cur CURSOR FOR SELECT ID FROM tbl_invoicedetails WHERE (InvoiceID = P_InvoiceID) AND (CurrentPayer = P_SubmittedTo); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO V_InvoiceDetailsID; --
    IF NOT done THEN
      CALL InvoiceDetails_AddSubmitted(V_InvoiceDetailsID, 0.00, P_SubmittedTo, P_SubmittedBy, P_SubmittedBatch, P_LastUpdateUserID); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --

  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_InternalReflag

### Original MySQL Procedure
```sql
BEGIN
  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE V_TransactionTypeID int DEFAULT 0; --
  DECLARE V_Username VARCHAR(50); --

  SET V_TransactionTypeID = NULL; --
  SELECT ID
  INTO V_TransactionTypeID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Voided Submission'); --

  SET V_Username = ''; --
  SELECT Login
  INTO V_Username
  FROM tbl_user
  WHERE (ID = P_LastUpdateUserID); --

  IF P_Extra NOT LIKE '<values>%</values>'
  AND P_Extra NOT LIKE '<values%/>'
  THEN
    SET P_Extra = NULL; --
  END IF; --

  INSERT INTO tbl_invoice_transaction
  ( InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , Amount
  , Quantity
  , TransactionDate
  , BatchNumber
  , Comments
  , Extra
  , LastUpdateUserID)
  SELECT
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , CASE CurrentPayer WHEN 'Patient' THEN null
                      WHEN 'Ins4'    THEN InsuranceCompany4_ID
                      WHEN 'Ins3'    THEN InsuranceCompany3_ID
                      WHEN 'Ins2'    THEN InsuranceCompany2_ID
                      WHEN 'Ins1'    THEN InsuranceCompany1_ID
                      ELSE null END as InsuranceCompanyID
  , CASE CurrentPayer WHEN 'Patient' THEN null
                      WHEN 'Ins4'    THEN Insurance4_ID
                      WHEN 'Ins3'    THEN Insurance3_ID
                      WHEN 'Ins2'    THEN Insurance2_ID
                      WHEN 'Ins1'    THEN Insurance1_ID
                      ELSE null END as CustomerInsuranceID
  , V_TransactionTypeID as TransactionTypeID
  , BillableAmount
  , Quantity
  , CURRENT_DATE()
  , null as BatchNumber
  , Concat('Reflagged by ', V_Username) as Comments
  , P_Extra
  , P_LastUpdateUserID as LastUpdateUserID
  FROM view_invoicetransaction_statistics
  WHERE (InvoiceID = P_InvoiceID)
    AND ((CurrentPayer = 'Patient' AND Submits & F_Patient != 0) OR
         (CurrentPayer = 'Ins4'    AND Submits & F_Insco_4 != 0) OR
         (CurrentPayer = 'Ins3'    AND Submits & F_Insco_3 != 0) OR
         (CurrentPayer = 'Ins2'    AND Submits & F_Insco_2 != 0) OR
         (CurrentPayer = 'Ins1'    AND Submits & F_Insco_1 != 0)); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE F_Insco_1 tinyint DEFAULT 01; --
  DECLARE F_Insco_2 tinyint DEFAULT 02; --
  DECLARE F_Insco_3 tinyint DEFAULT 04; --
  DECLARE F_Insco_4 tinyint DEFAULT 08; --
  DECLARE F_Patient tinyint DEFAULT 16; --

  DECLARE V_TransactionTypeID int DEFAULT 0; --
  DECLARE V_Username VARCHAR(50); --

  SET V_TransactionTypeID = NULL; --
  SELECT ID
  INTO V_TransactionTypeID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Voided Submission'); --

  SET V_Username = ''; --
  SELECT Login
  INTO V_Username
  FROM tbl_user
  WHERE (ID = P_LastUpdateUserID); --

  IF P_Extra NOT LIKE '<values>%</values>'
  AND P_Extra NOT LIKE '<values%/>'
  THEN
    SET P_Extra = NULL; --
  END IF; --

  INSERT INTO tbl_invoice_transaction
  ( InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , InsuranceCompanyID
  , CustomerInsuranceID
  , TransactionTypeID
  , Amount
  , Quantity
  , TransactionDate
  , BatchNumber
  , Comments
  , Extra
  , LastUpdateUserID)
  SELECT
    InvoiceDetailsID
  , InvoiceID
  , CustomerID
  , CASE CurrentPayer WHEN 'Patient' THEN null
                      WHEN 'Ins4'    THEN InsuranceCompany4_ID
                      WHEN 'Ins3'    THEN InsuranceCompany3_ID
                      WHEN 'Ins2'    THEN InsuranceCompany2_ID
                      WHEN 'Ins1'    THEN InsuranceCompany1_ID
                      ELSE null END as InsuranceCompanyID
  , CASE CurrentPayer WHEN 'Patient' THEN null
                      WHEN 'Ins4'    THEN Insurance4_ID
                      WHEN 'Ins3'    THEN Insurance3_ID
                      WHEN 'Ins2'    THEN Insurance2_ID
                      WHEN 'Ins1'    THEN Insurance1_ID
                      ELSE null END as CustomerInsuranceID
  , V_TransactionTypeID as TransactionTypeID
  , BillableAmount
  , Quantity
  , CURRENT_DATE()
  , null as BatchNumber
  , Concat('Reflagged by ', V_Username) as Comments
  , P_Extra
  , P_LastUpdateUserID as LastUpdateUserID
  FROM view_invoicetransaction_statistics
  WHERE (InvoiceID = P_InvoiceID)
    AND ((CurrentPayer = 'Patient' AND Submits & F_Patient != 0) OR
         (CurrentPayer = 'Ins4'    AND Submits & F_Insco_4 != 0) OR
         (CurrentPayer = 'Ins3'    AND Submits & F_Insco_3 != 0) OR
         (CurrentPayer = 'Ins2'    AND Submits & F_Insco_2 != 0) OR
         (CurrentPayer = 'Ins1'    AND Submits & F_Insco_1 != 0)); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_InternalUpdateBalance

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_invoice as i
  LEFT JOIN (SELECT tbl_invoicedetails.InvoiceID, Sum(tbl_invoicedetails.Balance) as Balance
             FROM tbl_invoice
                  INNER JOIN tbl_invoicedetails ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                               AND tbl_invoicedetails.InvoiceID  = tbl_invoice.ID
             WHERE (tbl_invoice.ID = P_InvoiceID)
             GROUP BY tbl_invoicedetails.InvoiceID) as b
         ON b.InvoiceID = i.ID
  SET i.InvoiceBalance = IFNULL(b.Balance, 0)
  WHERE (i.ID = P_InvoiceID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_invoice as i
  LEFT JOIN (SELECT tbl_invoicedetails.InvoiceID, Sum(tbl_invoicedetails.Balance) as Balance
             FROM tbl_invoice
                  INNER JOIN tbl_invoicedetails ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                               AND tbl_invoicedetails.InvoiceID  = tbl_invoice.ID
             WHERE (tbl_invoice.ID = P_InvoiceID)
             GROUP BY tbl_invoicedetails.InvoiceID) as b
         ON b.InvoiceID = i.ID
  SET i.InvoiceBalance = COALESCE(b.Balance, 0)
  WHERE (i.ID = P_InvoiceID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_InternalUpdatePendingSubmissions

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_Insurance1_ID,
    V_Insurance2_ID,
    V_Insurance3_ID,
    V_Insurance4_ID,
    V_Company1_ID,
    V_Company2_ID,
    V_Company3_ID,
    V_Company4_ID,
    V_Insurances,
    V_PendingSubmissions,
    V_Payments INT; --
  DECLARE
    V_CurrentPayer VARCHAR(10); --
  DECLARE
    V_PendingSubmissionID,
    V_WriteoffID INT; --
  DECLARE
    V_PaymentAmount,
    V_WriteoffAmount,
    V_BillableAmount DECIMAL(18, 2); --
  DECLARE
    V_Quantity DOUBLE; --
  DECLARE
    V_Hardship TINYINT(1); --
  DECLARE cur CURSOR FOR
    SELECT
      CustomerID,
      InvoiceID,
      InvoiceDetailsID,
      PaymentAmount,
      WriteoffAmount,
      BillableAmount,
      IFNULL(Quantity, 0.0) as Quantity,
      Insurance1_ID,
      Insurance2_ID,
      Insurance3_ID,
      Insurance4_ID,
      InsuranceCompany1_ID,
      InsuranceCompany2_ID,
      InsuranceCompany3_ID,
      InsuranceCompany4_ID,
      Insurances,
      PendingSubmissions,
      Payments,
      CurrentPayer
  FROM view_invoicetransaction_statistics
  WHERE (InvoiceID = P_InvoiceID) OR (P_InvoiceID IS NULL); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_PendingSubmissionID = NULL; --

  SELECT ID
  INTO V_PendingSubmissionID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Pending Submission'); --

  SET V_WriteoffID = NULL; --

  SELECT ID
  INTO V_WriteoffID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Writeoff'); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      V_CustomerID,
      V_InvoiceID,
      V_InvoiceDetailsID,
      V_PaymentAmount,
      V_WriteoffAmount,
      V_BillableAmount,
      V_Quantity,
      V_Insurance1_ID,
      V_Insurance2_ID,
      V_Insurance3_ID,
      V_Insurance4_ID,
      V_Company1_ID,
      V_Company2_ID,
      V_Company3_ID,
      V_Company4_ID,
      V_Insurances,
      V_PendingSubmissions,
      V_Payments,
      V_CurrentPayer; --

    IF NOT done THEN
      IF (V_CurrentPayer = 'Ins1') AND (V_Insurance1_ID IS NOT NULL) AND (V_PendingSubmissions & 01 = 00) THEN -- first insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company1_ID,       V_Insurance1_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins1',
           V_BillableAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Ins2') AND (V_Insurance2_ID IS NOT NULL) AND (V_PendingSubmissions & 02 = 00) THEN -- second insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company2_ID,       V_Insurance2_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins2',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Ins3') AND (V_Insurance3_ID IS NOT NULL) AND (V_PendingSubmissions & 04 = 00) THEN -- third insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company3_ID,       V_Insurance3_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins3',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Ins4') AND (V_Insurance4_ID IS NOT NULL) AND (V_PendingSubmissions & 08 = 00) THEN -- fourth insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company4_ID,       V_Insurance4_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins4',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Patient') AND (V_PendingSubmissions & 16 = 00) THEN -- patient requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,                 null,                  null, V_PendingSubmissionID,    CURRENT_DATE(), 'Patient',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE
    V_CustomerID,
    V_InvoiceID,
    V_InvoiceDetailsID,
    V_Insurance1_ID,
    V_Insurance2_ID,
    V_Insurance3_ID,
    V_Insurance4_ID,
    V_Company1_ID,
    V_Company2_ID,
    V_Company3_ID,
    V_Company4_ID,
    V_Insurances,
    V_PendingSubmissions,
    V_Payments INT; --
  DECLARE
    V_CurrentPayer VARCHAR(10); --
  DECLARE
    V_PendingSubmissionID,
    V_WriteoffID INT; --
  DECLARE
    V_PaymentAmount,
    V_WriteoffAmount,
    V_BillableAmount DECIMAL(18, 2); --
  DECLARE
    V_Quantity DOUBLE; --
  DECLARE
    V_Hardship TINYINT(1); --
  DECLARE cur CURSOR FOR
    SELECT
      CustomerID,
      InvoiceID,
      InvoiceDetailsID,
      PaymentAmount,
      WriteoffAmount,
      BillableAmount,
      COALESCE(Quantity, 0.0) as Quantity,
      Insurance1_ID,
      Insurance2_ID,
      Insurance3_ID,
      Insurance4_ID,
      InsuranceCompany1_ID,
      InsuranceCompany2_ID,
      InsuranceCompany3_ID,
      InsuranceCompany4_ID,
      Insurances,
      PendingSubmissions,
      Payments,
      CurrentPayer
  FROM view_invoicetransaction_statistics
  WHERE (InvoiceID = P_InvoiceID) OR (P_InvoiceID IS NULL); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_PendingSubmissionID = NULL; --

  SELECT ID
  INTO V_PendingSubmissionID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Pending Submission'); --

  SET V_WriteoffID = NULL; --

  SELECT ID
  INTO V_WriteoffID
  FROM tbl_invoice_transactiontype
  WHERE (Name = 'Writeoff'); --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      V_CustomerID,
      V_InvoiceID,
      V_InvoiceDetailsID,
      V_PaymentAmount,
      V_WriteoffAmount,
      V_BillableAmount,
      V_Quantity,
      V_Insurance1_ID,
      V_Insurance2_ID,
      V_Insurance3_ID,
      V_Insurance4_ID,
      V_Company1_ID,
      V_Company2_ID,
      V_Company3_ID,
      V_Company4_ID,
      V_Insurances,
      V_PendingSubmissions,
      V_Payments,
      V_CurrentPayer; --

    IF NOT done THEN
      IF (V_CurrentPayer = 'Ins1') AND (V_Insurance1_ID IS NOT NULL) AND (V_PendingSubmissions & 01 = 00) THEN -- first insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company1_ID,       V_Insurance1_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins1',
           V_BillableAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Ins2') AND (V_Insurance2_ID IS NOT NULL) AND (V_PendingSubmissions & 02 = 00) THEN -- second insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company2_ID,       V_Insurance2_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins2',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Ins3') AND (V_Insurance3_ID IS NOT NULL) AND (V_PendingSubmissions & 04 = 00) THEN -- third insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company3_ID,       V_Insurance3_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins3',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Ins4') AND (V_Insurance4_ID IS NOT NULL) AND (V_PendingSubmissions & 08 = 00) THEN -- fourth insurance requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,        V_Company4_ID,       V_Insurance4_ID, V_PendingSubmissionID,    CURRENT_DATE(), 'Ins4',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      ELSEIF (V_CurrentPayer = 'Patient') AND (V_PendingSubmissions & 16 = 00) THEN -- patient requires billing but do not have 'pending submission'
        INSERT INTO `tbl_invoice_transaction`
          (`InvoiceDetailsID`, `InvoiceID`, `CustomerID`, `InsuranceCompanyID`, `CustomerInsuranceID`, `TransactionTypeID`, `TransactionDate`, `Comments`, `Amount`, `Quantity`)
        VALUES
          (V_InvoiceDetailsID, V_InvoiceID, V_CustomerID,                 null,                  null, V_PendingSubmissionID,    CURRENT_DATE(), 'Patient',
           V_BillableAmount - V_PaymentAmount - V_WriteoffAmount, V_Quantity); --

      END IF; --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_Reflag

### Original MySQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, NULL); --
  CALL Invoice_InternalReflag                    (P_InvoiceID, P_Extra, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, NULL); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, NULL); --
  CALL Invoice_InternalReflag                    (P_InvoiceID, P_Extra, P_LastUpdateUserID); --
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, NULL); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_UpdateBalance

### Original MySQL Procedure
```sql
BEGIN
  IF P_Recursive THEN
    CALL `InvoiceDetails_RecalculateInternals_Single`(P_InvoiceID, null); --
  END IF; --

  CALL `Invoice_InternalUpdateBalance`(P_InvoiceID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  IF P_Recursive THEN
    CALL `InvoiceDetails_RecalculateInternals_Single`(P_InvoiceID, null); --
  END IF; --

  CALL `Invoice_InternalUpdateBalance`(P_InvoiceID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Invoice_UpdatePendingSubmissions

### Original MySQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
  CALL Invoice_InternalUpdatePendingSubmissions  (P_InvoiceID); --
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
  CALL Invoice_InternalUpdatePendingSubmissions  (P_InvoiceID); --
  CALL InvoiceDetails_RecalculateInternals_Single(P_InvoiceID, null); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update

### Original MySQL Procedure
```sql
BEGIN
  CALL mir_update_facility(null); --
  CALL mir_update_insurancecompany(null); --
  CALL mir_update_customer_insurance(null); --
  CALL mir_update_doctor(null); --
  CALL mir_update_customer(null); --
  CALL mir_update_cmnform(null); --
  CALL mir_update_orderdetails('ActiveOnly'); --
  CALL mir_update_order('ActiveOnly'); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL mir_update_facility(null); --
  CALL mir_update_insurancecompany(null); --
  CALL mir_update_customer_insurance(null); --
  CALL mir_update_doctor(null); --
  CALL mir_update_customer(null); --
  CALL mir_update_cmnform(null); --
  CALL mir_update_orderdetails('ActiveOnly'); --
  CALL mir_update_order('ActiveOnly'); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_cmnform

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_cmnform
         LEFT JOIN tbl_customer as tbl_customer ON tbl_cmnform.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_doctor   as tbl_doctor   ON tbl_cmnform.DoctorID   = tbl_doctor  .ID
         LEFT JOIN tbl_facility as tbl_facility ON tbl_cmnform.FacilityID = tbl_facility.ID
  SET tbl_cmnform.`MIR` =
      CONCAT_WS(',',
          IF(IFNULL(tbl_cmnform.CMNType              , '') = '', 'CMNType'              , null),
          IF(IFNULL(tbl_cmnform.Signature_Name       , '') = '', 'Signature_Name'       , null),
          IF(tbl_cmnform.InitialDate    is null, 'InitialDate'   , null),
          IF(tbl_cmnform.POSTypeID      is null, 'POSTypeID'     , null),
          IF(tbl_cmnform.Signature_Date is null, 'Signature_Date', null),
          CASE WHEN tbl_cmnform.EstimatedLengthOfNeed is null THEN 'EstimatedLengthOfNeed'
               WHEN tbl_cmnform.EstimatedLengthOfNeed <= 0    THEN 'EstimatedLengthOfNeed'
               ELSE null END,
          IF(tbl_customer.ID IS NULL, 'CustomerID', null),
          IF(tbl_customer.MIR != '' , 'Customer'  , null),
          IF(tbl_doctor  .ID IS NULL, 'DoctorID'  , null),
          IF(tbl_doctor  .MIR != '' , 'Doctor'    , null))
  WHERE (tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_icd9 as icd_1 ON tbl_cmnform.Customer_ICD9_1 = icd_1.Code
         LEFT JOIN tbl_icd9 as icd_2 ON tbl_cmnform.Customer_ICD9_2 = icd_2.Code
         LEFT JOIN tbl_icd9 as icd_3 ON tbl_cmnform.Customer_ICD9_3 = icd_3.Code
         LEFT JOIN tbl_icd9 as icd_4 ON tbl_cmnform.Customer_ICD9_4 = icd_4.Code
  SET tbl_cmnform.`MIR` =
      CONCAT_WS(',',
          tbl_cmnform.`MIR`,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_1, '') = ''  THEN 'ICD9_1.Required'
               WHEN icd_1.Code is null                            THEN 'ICD9_1.Unknown'
               WHEN icd_1.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_1.Inactive'
               ELSE null END,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_2, '') = ''  THEN null
               WHEN icd_2.Code is null                            THEN 'ICD9_2.Unknown'
               WHEN icd_2.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_2.Inactive'
               ELSE null END,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_3, '') = ''  THEN null
               WHEN icd_3.Code is null                            THEN 'ICD9_3.Unknown'
               WHEN icd_3.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_3.Inactive'
               ELSE null END,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_4, '') = ''  THEN null
               WHEN icd_4.Code is null                            THEN 'ICD9_4.Unknown'
               WHEN icd_4.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_4.Inactive'
               ELSE null END)
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.Customer_UsingICD10 != 1 OR tbl_cmnform.Customer_UsingICD10 IS NULL); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_icd10 as icd_1 ON tbl_cmnform.Customer_ICD9_1 = icd_1.Code
         LEFT JOIN tbl_icd10 as icd_2 ON tbl_cmnform.Customer_ICD9_2 = icd_2.Code
         LEFT JOIN tbl_icd10 as icd_3 ON tbl_cmnform.Customer_ICD9_3 = icd_3.Code
         LEFT JOIN tbl_icd10 as icd_4 ON tbl_cmnform.Customer_ICD9_4 = icd_4.Code
  SET tbl_cmnform.`MIR` =
      CONCAT_WS(',',
          tbl_cmnform.`MIR`,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_1, '') = ''  THEN 'ICD9_1.Required'
               WHEN icd_1.Code is null                            THEN 'ICD9_1.Unknown'
               WHEN icd_1.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_1.Inactive'
               ELSE null END,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_2, '') = ''  THEN null
               WHEN icd_2.Code is null                            THEN 'ICD9_2.Unknown'
               WHEN icd_2.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_2.Inactive'
               ELSE null END,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_3, '') = ''  THEN null
               WHEN icd_3.Code is null                            THEN 'ICD9_3.Unknown'
               WHEN icd_3.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_3.Inactive'
               ELSE null END,
          CASE WHEN IFNULL(tbl_cmnform.Customer_ICD9_4, '') = ''  THEN null
               WHEN icd_4.Code is null                            THEN 'ICD9_4.Unknown'
               WHEN icd_4.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_4.Inactive'
               ELSE null END)
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.Customer_UsingICD10 = 1); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0102a ON tbl_cmnform.ID = tbl_cmnform_0102a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 01.02A')
    AND (IFNULL(tbl_cmnform_0102a.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102a.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102a.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102a.Answer5, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102a.Answer6, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102a.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0102b ON tbl_cmnform.ID = tbl_cmnform_0102b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 01.02B')
    AND (IFNULL(tbl_cmnform_0102b.Answer12, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer13, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer14, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer15, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer16, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer19, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer20, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0102b.Answer22, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0203a ON tbl_cmnform.ID = tbl_cmnform_0203a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 02.03A')
    AND (IFNULL(tbl_cmnform_0203a.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203a.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203a.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203a.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203a.Answer6, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203a.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0203b ON tbl_cmnform.ID = tbl_cmnform_0203b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 02.03B')
    AND (IFNULL(tbl_cmnform_0203b.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203b.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203b.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203b.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203b.Answer8, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0203b.Answer9, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0302 ON tbl_cmnform.ID = tbl_cmnform_0302.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 03.02')
    AND (IFNULL(tbl_cmnform_0302.Answer14, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0403b ON tbl_cmnform.ID = tbl_cmnform_0403b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 04.03B')
    AND (IFNULL(tbl_cmnform_0403b.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403b.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403b.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403b.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403b.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0403c ON tbl_cmnform.ID = tbl_cmnform_0403c.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 04.03C')
    AND (IFNULL(tbl_cmnform_0403c.Answer6a , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403c.Answer7a , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403c.Answer8  , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403c.Answer9a , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403c.Answer10a, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0403c.Answer11a, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0602b ON tbl_cmnform.ID = tbl_cmnform_0602b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 06.02B')
    AND (IFNULL(tbl_cmnform_0602b.Answer1 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0602b.Answer3 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0602b.Answer6 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0602b.Answer7 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0602b.Answer11, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0702a ON tbl_cmnform.ID = tbl_cmnform_0702a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 07.02A')
    AND (IFNULL(tbl_cmnform_0702a.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702a.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702a.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702a.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702a.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0702b ON tbl_cmnform.ID = tbl_cmnform_0702b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 07.02B')
    AND (IFNULL(tbl_cmnform_0702b.Answer6 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702b.Answer7 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702b.Answer8 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702b.Answer12, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702b.Answer13, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0702b.Answer14, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0902 ON tbl_cmnform.ID = tbl_cmnform_0902.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 09.02')
    AND (IFNULL(tbl_cmnform_0902.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_1002a ON tbl_cmnform.ID = tbl_cmnform_1002a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 10.02A')
    AND (IFNULL(tbl_cmnform_1002a.Answer1, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_1002b ON tbl_cmnform.ID = tbl_cmnform_1002b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 10.02B')
    AND (IFNULL(tbl_cmnform_1002b.Answer7 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_1002b.Answer8 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_1002b.Answer14, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_4842 ON tbl_cmnform.ID = tbl_cmnform_4842.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 484.2')
    AND (IFNULL(tbl_cmnform_4842.Answer2 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_4842.Answer5 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_4842.Answer8 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_4842.Answer9 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_4842.Answer10, '') != 'Y'); --

  -- new forms

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0404b ON tbl_cmnform.ID = tbl_cmnform_0404b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 04.04B')
    AND (IFNULL(tbl_cmnform_0404b.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404b.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404b.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404b.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404b.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0404c ON tbl_cmnform.ID = tbl_cmnform_0404c.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 04.04C')
    AND (IFNULL(tbl_cmnform_0404c.Answer6  , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404c.Answer7a , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404c.Answer8  , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404c.Answer9a , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404c.Answer10a, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404c.Answer11 , '') != 'Y')
    AND (IFNULL(tbl_cmnform_0404c.Answer12 , '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0603b ON tbl_cmnform.ID = tbl_cmnform_0603b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 06.03B')
    AND (IFNULL(tbl_cmnform_0603b.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0603b.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0603b.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0703a ON tbl_cmnform.ID = tbl_cmnform_0703a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 07.03A')
    AND (IFNULL(tbl_cmnform_0703a.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0703a.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0703a.Answer3, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0703a.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_0703a.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_1003 ON tbl_cmnform.ID = tbl_cmnform_1003.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 10.03')
    AND (IFNULL(tbl_cmnform_1003.Answer1, '') != 'Y')
    AND (IFNULL(tbl_cmnform_1003.Answer2, '') != 'Y')
    AND (IFNULL(tbl_cmnform_1003.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_48403 ON tbl_cmnform.ID = tbl_cmnform_48403.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 484.03')
    AND ((tbl_cmnform_48403.Answer1a is null) OR
         (tbl_cmnform_48403.Answer1b is null) OR
         (IFNULL(tbl_cmnform_48403.Answer1c, '0000-00-00') = '0000-00-00'))
    AND (IFNULL(tbl_cmnform_48403.Answer2, '')  = '')
    AND (IFNULL(tbl_cmnform_48403.Answer3, '')  = '')
    AND (IFNULL(tbl_cmnform_48403.Answer4, '') != 'Y')
    AND (IFNULL(tbl_cmnform_48403.Answer7, '') != 'Y')
    AND (IFNULL(tbl_cmnform_48403.Answer8, '') != 'Y')
    AND (IFNULL(tbl_cmnform_48403.Answer9, '') != 'Y'); --

--  `Answer1a` int(11) default NULL,
--  `Answer1b` int(11) default NULL,
--  `Answer1c` date default NULL,
--  `Answer2` enum('1','2','3') NOT NULL default '1',
--  `Answer3` enum('1','2','3') NOT NULL default '1',
--  `Answer4` enum('Y','N','D') NOT NULL default 'D',
--  `Answer5` varchar(10) default NULL,
--  `Answer6a` int(11) default NULL,
--  `Answer6b` int(11) default NULL,
--  `Answer6c` date default NULL,
--  `Answer7` enum('Y','N') NOT NULL default 'Y',
--  `Answer8` enum('Y','N') NOT NULL default 'Y',
--  `Answer9` enum('Y','N') NOT NULL default 'Y',

--  UPDATE tbl_cmnform
--         LEFT JOIN tbl_cmnform_drorder ON tbl_cmnform.ID = tbl_cmnform_drorder.CMNFormID
--  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
--  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
--    AND (tbl_cmnform.CMNType = 'DMERC DRORDER')
--    AND  (1 != 1); --

--  UPDATE tbl_cmnform
--         LEFT JOIN tbl_cmnform_uro ON tbl_cmnform.ID = tbl_cmnform_uro.CMNFormID
--  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
--  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
--    AND (tbl_cmnform.CMNType = 'DMERC URO')
--    AND  (1 != 1); --

--  UPDATE tbl_cmnform
--         LEFT JOIN tbl_cmnform_0903 ON tbl_cmnform.ID = tbl_cmnform_0903.CMNFormID
--  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
--  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
--    AND (tbl_cmnform.CMNType = 'DME 09.03')
--    AND (1 != 1); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_cmnform
         LEFT JOIN tbl_customer as tbl_customer ON tbl_cmnform.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_doctor   as tbl_doctor   ON tbl_cmnform.DoctorID   = tbl_doctor  .ID
         LEFT JOIN tbl_facility as tbl_facility ON tbl_cmnform.FacilityID = tbl_facility.ID
  SET tbl_cmnform.`MIR` =
      CONCAT_WS(',',
          IF(COALESCE(tbl_cmnform.CMNType              , '') = '', 'CMNType'              , null),
          IF(COALESCE(tbl_cmnform.Signature_Name       , '') = '', 'Signature_Name'       , null),
          IF(tbl_cmnform.InitialDate    is null, 'InitialDate'   , null),
          IF(tbl_cmnform.POSTypeID      is null, 'POSTypeID'     , null),
          IF(tbl_cmnform.Signature_Date is null, 'Signature_Date', null),
          CASE WHEN tbl_cmnform.EstimatedLengthOfNeed is null THEN 'EstimatedLengthOfNeed'
               WHEN tbl_cmnform.EstimatedLengthOfNeed <= 0    THEN 'EstimatedLengthOfNeed'
               ELSE null END,
          IF(tbl_customer.ID IS NULL, 'CustomerID', null),
          IF(tbl_customer.MIR != '' , 'Customer'  , null),
          IF(tbl_doctor  .ID IS NULL, 'DoctorID'  , null),
          IF(tbl_doctor  .MIR != '' , 'Doctor'    , null))
  WHERE (tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_icd9 as icd_1 ON tbl_cmnform.Customer_ICD9_1 = icd_1.Code
         LEFT JOIN tbl_icd9 as icd_2 ON tbl_cmnform.Customer_ICD9_2 = icd_2.Code
         LEFT JOIN tbl_icd9 as icd_3 ON tbl_cmnform.Customer_ICD9_3 = icd_3.Code
         LEFT JOIN tbl_icd9 as icd_4 ON tbl_cmnform.Customer_ICD9_4 = icd_4.Code
  SET tbl_cmnform.`MIR` =
      CONCAT_WS(',',
          tbl_cmnform.`MIR`,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_1, '') = ''  THEN 'ICD9_1.Required'
               WHEN icd_1.Code is null                            THEN 'ICD9_1.Unknown'
               WHEN icd_1.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_1.Inactive'
               ELSE null END,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_2, '') = ''  THEN null
               WHEN icd_2.Code is null                            THEN 'ICD9_2.Unknown'
               WHEN icd_2.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_2.Inactive'
               ELSE null END,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_3, '') = ''  THEN null
               WHEN icd_3.Code is null                            THEN 'ICD9_3.Unknown'
               WHEN icd_3.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_3.Inactive'
               ELSE null END,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_4, '') = ''  THEN null
               WHEN icd_4.Code is null                            THEN 'ICD9_4.Unknown'
               WHEN icd_4.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_4.Inactive'
               ELSE null END)
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.Customer_UsingICD10 != 1 OR tbl_cmnform.Customer_UsingICD10 IS NULL); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_icd10 as icd_1 ON tbl_cmnform.Customer_ICD9_1 = icd_1.Code
         LEFT JOIN tbl_icd10 as icd_2 ON tbl_cmnform.Customer_ICD9_2 = icd_2.Code
         LEFT JOIN tbl_icd10 as icd_3 ON tbl_cmnform.Customer_ICD9_3 = icd_3.Code
         LEFT JOIN tbl_icd10 as icd_4 ON tbl_cmnform.Customer_ICD9_4 = icd_4.Code
  SET tbl_cmnform.`MIR` =
      CONCAT_WS(',',
          tbl_cmnform.`MIR`,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_1, '') = ''  THEN 'ICD9_1.Required'
               WHEN icd_1.Code is null                            THEN 'ICD9_1.Unknown'
               WHEN icd_1.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_1.Inactive'
               ELSE null END,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_2, '') = ''  THEN null
               WHEN icd_2.Code is null                            THEN 'ICD9_2.Unknown'
               WHEN icd_2.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_2.Inactive'
               ELSE null END,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_3, '') = ''  THEN null
               WHEN icd_3.Code is null                            THEN 'ICD9_3.Unknown'
               WHEN icd_3.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_3.Inactive'
               ELSE null END,
          CASE WHEN COALESCE(tbl_cmnform.Customer_ICD9_4, '') = ''  THEN null
               WHEN icd_4.Code is null                            THEN 'ICD9_4.Unknown'
               WHEN icd_4.InactiveDate <= tbl_cmnform.InitialDate THEN 'ICD9_4.Inactive'
               ELSE null END)
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.Customer_UsingICD10 = 1); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0102a ON tbl_cmnform.ID = tbl_cmnform_0102a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 01.02A')
    AND (COALESCE(tbl_cmnform_0102a.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102a.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102a.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102a.Answer5, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102a.Answer6, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102a.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0102b ON tbl_cmnform.ID = tbl_cmnform_0102b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 01.02B')
    AND (COALESCE(tbl_cmnform_0102b.Answer12, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer13, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer14, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer15, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer16, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer19, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer20, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0102b.Answer22, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0203a ON tbl_cmnform.ID = tbl_cmnform_0203a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 02.03A')
    AND (COALESCE(tbl_cmnform_0203a.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203a.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203a.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203a.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203a.Answer6, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203a.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0203b ON tbl_cmnform.ID = tbl_cmnform_0203b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 02.03B')
    AND (COALESCE(tbl_cmnform_0203b.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203b.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203b.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203b.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203b.Answer8, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0203b.Answer9, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0302 ON tbl_cmnform.ID = tbl_cmnform_0302.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 03.02')
    AND (COALESCE(tbl_cmnform_0302.Answer14, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0403b ON tbl_cmnform.ID = tbl_cmnform_0403b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 04.03B')
    AND (COALESCE(tbl_cmnform_0403b.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403b.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403b.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403b.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403b.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0403c ON tbl_cmnform.ID = tbl_cmnform_0403c.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 04.03C')
    AND (COALESCE(tbl_cmnform_0403c.Answer6a , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403c.Answer7a , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403c.Answer8  , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403c.Answer9a , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403c.Answer10a, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0403c.Answer11a, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0602b ON tbl_cmnform.ID = tbl_cmnform_0602b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 06.02B')
    AND (COALESCE(tbl_cmnform_0602b.Answer1 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0602b.Answer3 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0602b.Answer6 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0602b.Answer7 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0602b.Answer11, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0702a ON tbl_cmnform.ID = tbl_cmnform_0702a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 07.02A')
    AND (COALESCE(tbl_cmnform_0702a.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702a.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702a.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702a.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702a.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0702b ON tbl_cmnform.ID = tbl_cmnform_0702b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 07.02B')
    AND (COALESCE(tbl_cmnform_0702b.Answer6 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702b.Answer7 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702b.Answer8 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702b.Answer12, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702b.Answer13, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0702b.Answer14, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0902 ON tbl_cmnform.ID = tbl_cmnform_0902.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 09.02')
    AND (COALESCE(tbl_cmnform_0902.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_1002a ON tbl_cmnform.ID = tbl_cmnform_1002a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 10.02A')
    AND (COALESCE(tbl_cmnform_1002a.Answer1, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_1002b ON tbl_cmnform.ID = tbl_cmnform_1002b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 10.02B')
    AND (COALESCE(tbl_cmnform_1002b.Answer7 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_1002b.Answer8 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_1002b.Answer14, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_4842 ON tbl_cmnform.ID = tbl_cmnform_4842.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DMERC 484.2')
    AND (COALESCE(tbl_cmnform_4842.Answer2 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_4842.Answer5 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_4842.Answer8 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_4842.Answer9 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_4842.Answer10, '') != 'Y'); --

  -- new forms

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0404b ON tbl_cmnform.ID = tbl_cmnform_0404b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 04.04B')
    AND (COALESCE(tbl_cmnform_0404b.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404b.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404b.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404b.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404b.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0404c ON tbl_cmnform.ID = tbl_cmnform_0404c.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 04.04C')
    AND (COALESCE(tbl_cmnform_0404c.Answer6  , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404c.Answer7a , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404c.Answer8  , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404c.Answer9a , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404c.Answer10a, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404c.Answer11 , '') != 'Y')
    AND (COALESCE(tbl_cmnform_0404c.Answer12 , '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0603b ON tbl_cmnform.ID = tbl_cmnform_0603b.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 06.03B')
    AND (COALESCE(tbl_cmnform_0603b.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0603b.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0603b.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_0703a ON tbl_cmnform.ID = tbl_cmnform_0703a.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 07.03A')
    AND (COALESCE(tbl_cmnform_0703a.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0703a.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0703a.Answer3, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0703a.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_0703a.Answer5, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_1003 ON tbl_cmnform.ID = tbl_cmnform_1003.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 10.03')
    AND (COALESCE(tbl_cmnform_1003.Answer1, '') != 'Y')
    AND (COALESCE(tbl_cmnform_1003.Answer2, '') != 'Y')
    AND (COALESCE(tbl_cmnform_1003.Answer7, '') != 'Y'); --

  UPDATE tbl_cmnform
         LEFT JOIN tbl_cmnform_48403 ON tbl_cmnform.ID = tbl_cmnform_48403.CMNFormID
  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
    AND (tbl_cmnform.CMNType = 'DME 484.03')
    AND ((tbl_cmnform_48403.Answer1a is null) OR
         (tbl_cmnform_48403.Answer1b is null) OR
         (COALESCE(tbl_cmnform_48403.Answer1c, '0000-00-00') = '0000-00-00'))
    AND (COALESCE(tbl_cmnform_48403.Answer2, '')  = '')
    AND (COALESCE(tbl_cmnform_48403.Answer3, '')  = '')
    AND (COALESCE(tbl_cmnform_48403.Answer4, '') != 'Y')
    AND (COALESCE(tbl_cmnform_48403.Answer7, '') != 'Y')
    AND (COALESCE(tbl_cmnform_48403.Answer8, '') != 'Y')
    AND (COALESCE(tbl_cmnform_48403.Answer9, '') != 'Y'); --

--  `Answer1a` int(11) default NULL,
--  `Answer1b` int(11) default NULL,
--  `Answer1c` date default NULL,
--  `Answer2` enum('1','2','3') NOT NULL default '1',
--  `Answer3` enum('1','2','3') NOT NULL default '1',
--  `Answer4` enum('Y','N','D') NOT NULL default 'D',
--  `Answer5` varchar(10) default NULL,
--  `Answer6a` int(11) default NULL,
--  `Answer6b` int(11) default NULL,
--  `Answer6c` date default NULL,
--  `Answer7` enum('Y','N') NOT NULL default 'Y',
--  `Answer8` enum('Y','N') NOT NULL default 'Y',
--  `Answer9` enum('Y','N') NOT NULL default 'Y',

--  UPDATE tbl_cmnform
--         LEFT JOIN tbl_cmnform_drorder ON tbl_cmnform.ID = tbl_cmnform_drorder.CMNFormID
--  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
--  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
--    AND (tbl_cmnform.CMNType = 'DMERC DRORDER')
--    AND  (1 != 1); --

--  UPDATE tbl_cmnform
--         LEFT JOIN tbl_cmnform_uro ON tbl_cmnform.ID = tbl_cmnform_uro.CMNFormID
--  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
--  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
--    AND (tbl_cmnform.CMNType = 'DMERC URO')
--    AND  (1 != 1); --

--  UPDATE tbl_cmnform
--         LEFT JOIN tbl_cmnform_0903 ON tbl_cmnform.ID = tbl_cmnform_0903.CMNFormID
--  SET tbl_cmnform.MIR = CONCAT_WS(',', 'Answers', IF(tbl_cmnform.MIR != '', tbl_cmnform.MIR, null))
--  WHERE ((tbl_cmnform.ID = P_CMNFormID) OR (P_CMNFormID IS NULL))
--    AND (tbl_cmnform.CMNType = 'DME 09.03')
--    AND (1 != 1); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_customer

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_customer
         LEFT JOIN tbl_doctor ON tbl_customer.Doctor1_ID = tbl_doctor.ID
  SET tbl_customer.`MIR` =
      IF(tbl_customer.CommercialAccount = 0
        ,CONCAT_WS(','
                  ,IF(IFNULL(tbl_customer.AccountNumber   , '') = '', 'AccountNumber'   , null)
                  ,IF(IFNULL(tbl_customer.FirstName       , '') = '', 'FirstName'       , null)
                  ,IF(IFNULL(tbl_customer.LastName        , '') = '', 'LastName'        , null)
                  ,IF(IFNULL(tbl_customer.Address1        , '') = '', 'Address1'        , null)
                  ,IF(IFNULL(tbl_customer.City            , '') = '', 'City'            , null)
                  ,IF(IFNULL(tbl_customer.State           , '') = '', 'State'           , null)
                  ,IF(IFNULL(tbl_customer.Zip             , '') = '', 'Zip'             , null)
                  ,IF(IFNULL(tbl_customer.EmploymentStatus, '') = '', 'EmploymentStatus', null)
                  ,IF(IFNULL(tbl_customer.Gender          , '') = '', 'Gender'          , null)
                  ,IF(IFNULL(tbl_customer.MaritalStatus   , '') = '', 'MaritalStatus'   , null)
                  ,IF(IFNULL(tbl_customer.MilitaryBranch  , '') = '', 'MilitaryBranch'  , null)
                  ,IF(IFNULL(tbl_customer.MilitaryStatus  , '') = '', 'MilitaryStatus'  , null)
                  ,IF(IFNULL(tbl_customer.StudentStatus   , '') = '', 'StudentStatus'   , null)
                  ,IF(IFNULL(tbl_customer.MonthsValid     ,  0) =  0, 'MonthsValid'     , null)
                  ,IF(tbl_customer.DateofBirth     IS NULL, 'DateofBirth'    , null)
                  ,IF(tbl_customer.SignatureOnFile IS NULL, 'SignatureOnFile', null)
                  ,IF(tbl_doctor.ID IS NULL, 'Doctor1_ID', null)
                  ,IF(tbl_doctor.MIR != '' , 'Doctor1'   , null)
                  )
        ,'')
  WHERE (tbl_customer.ID = P_CustomerID) OR (P_CustomerID IS NULL); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_customer
         LEFT JOIN tbl_doctor ON tbl_customer.Doctor1_ID = tbl_doctor.ID
  SET tbl_customer.`MIR` =
      IF(tbl_customer.CommercialAccount = 0
        ,CONCAT_WS(','
                  ,IF(COALESCE(tbl_customer.AccountNumber   , '') = '', 'AccountNumber'   , null)
                  ,IF(COALESCE(tbl_customer.FirstName       , '') = '', 'FirstName'       , null)
                  ,IF(COALESCE(tbl_customer.LastName        , '') = '', 'LastName'        , null)
                  ,IF(COALESCE(tbl_customer.Address1        , '') = '', 'Address1'        , null)
                  ,IF(COALESCE(tbl_customer.City            , '') = '', 'City'            , null)
                  ,IF(COALESCE(tbl_customer.State           , '') = '', 'State'           , null)
                  ,IF(COALESCE(tbl_customer.Zip             , '') = '', 'Zip'             , null)
                  ,IF(COALESCE(tbl_customer.EmploymentStatus, '') = '', 'EmploymentStatus', null)
                  ,IF(COALESCE(tbl_customer.Gender          , '') = '', 'Gender'          , null)
                  ,IF(COALESCE(tbl_customer.MaritalStatus   , '') = '', 'MaritalStatus'   , null)
                  ,IF(COALESCE(tbl_customer.MilitaryBranch  , '') = '', 'MilitaryBranch'  , null)
                  ,IF(COALESCE(tbl_customer.MilitaryStatus  , '') = '', 'MilitaryStatus'  , null)
                  ,IF(COALESCE(tbl_customer.StudentStatus   , '') = '', 'StudentStatus'   , null)
                  ,IF(COALESCE(tbl_customer.MonthsValid     ,  0) =  0, 'MonthsValid'     , null)
                  ,IF(tbl_customer.DateofBirth     IS NULL, 'DateofBirth'    , null)
                  ,IF(tbl_customer.SignatureOnFile IS NULL, 'SignatureOnFile', null)
                  ,IF(tbl_doctor.ID IS NULL, 'Doctor1_ID', null)
                  ,IF(tbl_doctor.MIR != '' , 'Doctor1'   , null)
                  )
        ,'')
  WHERE (tbl_customer.ID = P_CustomerID) OR (P_CustomerID IS NULL); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_customer_insurance

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_customer_insurance as policy
         LEFT JOIN tbl_customer ON policy.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_insurancecompany ON policy.InsuranceCompanyID = tbl_insurancecompany.ID
  SET policy.`MIR` =
      IF(tbl_customer.CommercialAccount = 0
        ,CONCAT_WS(','
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.FirstName, '') = ''), 'FirstName'  , null)
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.LastName , '') = ''), 'LastName'   , null)
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.Address1 , '') = ''), 'Address1'   , null)
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.City     , '') = ''), 'City'       , null)
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.State    , '') = ''), 'State'      , null)
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.Zip      , '') = ''), 'Zip'        , null)
                  ,IF((policy.RelationshipCode != '18') AND (IFNULL(policy.Gender   , '') = ''), 'Gender'     , null)
                  ,IF((policy.RelationshipCode != '18') AND (policy.DateofBirth IS NULL       ), 'DateofBirth', null)
                  ,IF(IFNULL(policy.InsuranceType   , '') = '', 'InsuranceType'   , null)
                  ,IF(IFNULL(policy.PolicyNumber    , '') = '', 'PolicyNumber'    , null)
                  ,IF(IFNULL(policy.RelationshipCode, '') = '', 'RelationshipCode', null)
                  ,IF(tbl_insurancecompany.ID IS NULL, 'InsuranceCompanyID', null)
                  ,IF(tbl_insurancecompany.MIR != '' , 'InsuranceCompany'  , null)
                  )
        ,'')
  WHERE (policy.CustomerID = P_CustomerID) OR (P_CustomerID IS NULL); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_customer_insurance as policy
         LEFT JOIN tbl_customer ON policy.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_insurancecompany ON policy.InsuranceCompanyID = tbl_insurancecompany.ID
  SET policy.`MIR` =
      IF(tbl_customer.CommercialAccount = 0
        ,CONCAT_WS(','
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.FirstName, '') = ''), 'FirstName'  , null)
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.LastName , '') = ''), 'LastName'   , null)
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.Address1 , '') = ''), 'Address1'   , null)
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.City     , '') = ''), 'City'       , null)
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.State    , '') = ''), 'State'      , null)
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.Zip      , '') = ''), 'Zip'        , null)
                  ,IF((policy.RelationshipCode != '18') AND (COALESCE(policy.Gender   , '') = ''), 'Gender'     , null)
                  ,IF((policy.RelationshipCode != '18') AND (policy.DateofBirth IS NULL       ), 'DateofBirth', null)
                  ,IF(COALESCE(policy.InsuranceType   , '') = '', 'InsuranceType'   , null)
                  ,IF(COALESCE(policy.PolicyNumber    , '') = '', 'PolicyNumber'    , null)
                  ,IF(COALESCE(policy.RelationshipCode, '') = '', 'RelationshipCode', null)
                  ,IF(tbl_insurancecompany.ID IS NULL, 'InsuranceCompanyID', null)
                  ,IF(tbl_insurancecompany.MIR != '' , 'InsuranceCompany'  , null)
                  )
        ,'')
  WHERE (policy.CustomerID = P_CustomerID) OR (P_CustomerID IS NULL); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_doctor

### Original MySQL Procedure
```sql
BEGIN
  CALL `dmeworks`.`mir_update_doctor`(P_DoctorID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL `dmeworks`.`mir_update_doctor`(P_DoctorID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_facility

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_facility
  SET `MIR` =
      CONCAT_WS(',',
          IF(IFNULL(Name      , '') = '', 'Name'      , null),
          IF(IFNULL(Address1  , '') = '', 'Address1'  , null),
          IF(IFNULL(City      , '') = '', 'City'      , null),
          IF(IFNULL(State     , '') = '', 'State'     , null),
          IF(IFNULL(Zip       , '') = '', 'Zip'       , null),
          IF(IFNULL(POSTypeID , '') = '', 'POSTypeID' , null),
          IF(NPI REGEXP '[[:digit:]]{10}[[:blank:]]*', null, 'NPI')
               )
  WHERE (ID = P_FacilityID) OR (P_FacilityID IS NULL); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_facility
  SET `MIR` =
      CONCAT_WS(',',
          IF(COALESCE(Name      , '') = '', 'Name'      , null),
          IF(COALESCE(Address1  , '') = '', 'Address1'  , null),
          IF(COALESCE(City      , '') = '', 'City'      , null),
          IF(COALESCE(State     , '') = '', 'State'     , null),
          IF(COALESCE(Zip       , '') = '', 'Zip'       , null),
          IF(COALESCE(POSTypeID , '') = '', 'POSTypeID' , null),
          IF(NPI REGEXP '[[:digit:]]{10}[[:blank:]]*', null, 'NPI')
               )
  WHERE (ID = P_FacilityID) OR (P_FacilityID IS NULL); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_insurancecompany

### Original MySQL Procedure
```sql
BEGIN
  CALL `dmeworks`.`mir_update_insurancecompany`(P_InsuranceCompanyID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL `dmeworks`.`mir_update_insurancecompany`(P_InsuranceCompanyID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_order

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_OrderID INT; --
  DECLARE V_ActiveOnly BIT; --

  -- P_OrderID
  -- 'ActiveOnly' - all orders that have details with State != 'Closed'
  -- number - just one
  -- all details regardless of state

  IF (P_OrderID = 'ActiveOnly') THEN
    SET V_OrderID = null; --
    SET V_ActiveOnly = 1; --
  ELSEIF (P_OrderID REGEXP '^(\-|\+){0,1}([0-9]+)$') THEN
    SET V_OrderID = CAST(P_OrderID as signed); --
    SET V_ActiveOnly = 0; --
  ELSE
    SET V_OrderID = null; --
    SET V_ActiveOnly = 0; --
  END IF; --

  IF (V_OrderID IS NOT NULL) THEN
    UPDATE tbl_order
    SET `MIR` =
      CONCAT_WS(','
      ,IF(CustomerID   IS NULL, 'CustomerID'  , null)
      ,IF(DeliveryDate IS NULL, 'DeliveryDate', null)
      ,IF(BillDate     IS NULL, 'BillDate'    , null)
      )
    WHERE (ID = V_OrderID); --
  ELSEIF (V_ActiveOnly != 1) THEN
    UPDATE tbl_order
    SET `MIR` =
      CONCAT_WS(','
      ,IF(CustomerID   IS NULL, 'CustomerID'  , null)
      ,IF(DeliveryDate IS NULL, 'DeliveryDate', null)
      ,IF(BillDate     IS NULL, 'BillDate'    , null)
      ); --
  ELSE
    UPDATE tbl_order as o
    INNER JOIN
           (SELECT DISTINCT CustomerID, OrderID
            FROM view_orderdetails
            WHERE (IsActive = 1)
           ) as d on d.CustomerID = o.CustomerID and d.OrderID = o.ID
    SET o.`MIR` =
      CONCAT_WS(','
      ,IF(o.CustomerID   IS NULL, 'CustomerID'  , null)
      ,IF(o.DeliveryDate IS NULL, 'DeliveryDate', null)
      ,IF(o.BillDate     IS NULL, 'BillDate'    , null)
      ); --
  END IF; --

  UPDATE tbl_order as o
  INNER JOIN
         (SELECT CustomerID, OrderID
          , SUM(0 < FIND_IN_SET('Customer.Inactive', `MIR.ORDER`)) AS `Customer.Inactive`
          , SUM(0 < FIND_IN_SET('Customer.MIR'     , `MIR.ORDER`)) AS `Customer.MIR`
          , SUM(0 < FIND_IN_SET('Policy1.Required' , `MIR.ORDER`)) AS `Policy1.Required`
          , SUM(0 < FIND_IN_SET('Policy1.MIR'      , `MIR.ORDER`)) AS `Policy1.MIR`
          , SUM(0 < FIND_IN_SET('Policy2.Required' , `MIR.ORDER`)) AS `Policy2.Required`
          , SUM(0 < FIND_IN_SET('Policy2.MIR'      , `MIR.ORDER`)) AS `Policy2.MIR`
          , SUM(0 < FIND_IN_SET('Facility.MIR'     , `MIR.ORDER`)) AS `Facility.MIR`
          , SUM(0 < FIND_IN_SET('PosType.Required' , `MIR.ORDER`)) AS `PosType.Required`
          , SUM(0 < FIND_IN_SET('ICD9.Required'    , `MIR.ORDER`)) AS `ICD9.Required`
          , SUM(0 < FIND_IN_SET('ICD9.1.Unknown'   , `MIR.ORDER`)) AS `ICD9.1.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.1.Inactive'  , `MIR.ORDER`)) AS `ICD9.1.Inactive`
          , SUM(0 < FIND_IN_SET('ICD9.2.Unknown'   , `MIR.ORDER`)) AS `ICD9.2.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.2.Inactive'  , `MIR.ORDER`)) AS `ICD9.2.Inactive`
          , SUM(0 < FIND_IN_SET('ICD9.3.Unknown'   , `MIR.ORDER`)) AS `ICD9.3.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.3.Inactive'  , `MIR.ORDER`)) AS `ICD9.3.Inactive`
          , SUM(0 < FIND_IN_SET('ICD9.4.Unknown'   , `MIR.ORDER`)) AS `ICD9.4.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.4.Inactive'  , `MIR.ORDER`)) AS `ICD9.4.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.Required'   , `MIR.ORDER`)) AS `ICD10.Required`
          , SUM(0 < FIND_IN_SET('ICD10.01.Unknown' , `MIR.ORDER`)) AS `ICD10.01.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.01.Inactive', `MIR.ORDER`)) AS `ICD10.01.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.02.Unknown' , `MIR.ORDER`)) AS `ICD10.02.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.02.Inactive', `MIR.ORDER`)) AS `ICD10.02.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.03.Unknown' , `MIR.ORDER`)) AS `ICD10.03.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.03.Inactive', `MIR.ORDER`)) AS `ICD10.03.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.04.Unknown' , `MIR.ORDER`)) AS `ICD10.04.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.04.Inactive', `MIR.ORDER`)) AS `ICD10.04.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.05.Unknown' , `MIR.ORDER`)) AS `ICD10.05.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.05.Inactive', `MIR.ORDER`)) AS `ICD10.05.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.06.Unknown' , `MIR.ORDER`)) AS `ICD10.06.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.06.Inactive', `MIR.ORDER`)) AS `ICD10.06.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.07.Unknown' , `MIR.ORDER`)) AS `ICD10.07.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.07.Inactive', `MIR.ORDER`)) AS `ICD10.07.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.08.Unknown' , `MIR.ORDER`)) AS `ICD10.08.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.08.Inactive', `MIR.ORDER`)) AS `ICD10.08.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.09.Unknown' , `MIR.ORDER`)) AS `ICD10.09.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.09.Inactive', `MIR.ORDER`)) AS `ICD10.09.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.10.Unknown' , `MIR.ORDER`)) AS `ICD10.10.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.10.Inactive', `MIR.ORDER`)) AS `ICD10.10.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.11.Unknown' , `MIR.ORDER`)) AS `ICD10.11.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.11.Inactive', `MIR.ORDER`)) AS `ICD10.11.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.12.Unknown' , `MIR.ORDER`)) AS `ICD10.12.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.12.Inactive', `MIR.ORDER`)) AS `ICD10.12.Inactive`
          FROM view_orderdetails
          WHERE IF(V_OrderID IS NOT NULL, OrderID = V_OrderID, V_ActiveOnly != 1 or IsActive = 1)
            AND (0 < FIND_IN_SET('Customer.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Customer.MIR'     , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy1.Required' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy1.MIR'      , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy2.Required' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy2.MIR'      , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Facility.MIR'     , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('PosType.Required' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.Required'    , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.1.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.1.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.2.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.2.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.3.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.3.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.4.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.4.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.Required'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.01.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.01.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.02.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.02.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.03.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.03.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.04.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.04.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.05.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.05.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.06.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.06.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.07.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.07.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.08.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.08.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.09.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.09.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.10.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.10.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.11.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.11.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.12.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.12.Inactive', `MIR.ORDER`))
          GROUP BY CustomerID, OrderID
         ) as d on d.CustomerID = o.CustomerID and d.OrderID = o.ID
  SET o.`MIR` = CONCAT_WS(','
    ,o.MIR
    ,IF(0 < d.`Customer.Inactive`, 'Customer.Inactive', NULL)
    ,IF(0 < d.`Customer.MIR`     , 'Customer.MIR'     , NULL)
    ,IF(0 < d.`Policy1.Required` , 'Policy1.Required' , NULL)
    ,IF(0 < d.`Policy1.MIR`      , 'Policy1.MIR'      , NULL)
    ,IF(0 < d.`Policy2.Required` , 'Policy2.Required' , NULL)
    ,IF(0 < d.`Policy2.MIR`      , 'Policy2.MIR'      , NULL)
    ,IF(0 < d.`Facility.MIR`     , 'Facility.MIR'     , NULL)
    ,IF(0 < d.`PosType.Required` , 'PosType.Required' , NULL)
    ,IF(0 < d.`ICD9.Required`    , 'ICD9.Required'    , NULL)
    ,IF(0 < d.`ICD9.1.Unknown`   , 'ICD9.1.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.1.Inactive`  , 'ICD9.1.Inactive'  , NULL)
    ,IF(0 < d.`ICD9.2.Unknown`   , 'ICD9.2.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.2.Inactive`  , 'ICD9.2.Inactive'  , NULL)
    ,IF(0 < d.`ICD9.3.Unknown`   , 'ICD9.3.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.3.Inactive`  , 'ICD9.3.Inactive'  , NULL)
    ,IF(0 < d.`ICD9.4.Unknown`   , 'ICD9.4.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.4.Inactive`  , 'ICD9.4.Inactive'  , NULL)
    ,IF(0 < d.`ICD10.Required`   , 'ICD10.Required'   , NULL)
    ,IF(0 < d.`ICD10.01.Unknown` , 'ICD10.01.Unknown' , NULL)
    ,IF(0 < d.`ICD10.01.Inactive`, 'ICD10.01.Inactive', NULL)
    ,IF(0 < d.`ICD10.02.Unknown` , 'ICD10.02.Unknown' , NULL)
    ,IF(0 < d.`ICD10.02.Inactive`, 'ICD10.02.Inactive', NULL)
    ,IF(0 < d.`ICD10.03.Unknown` , 'ICD10.03.Unknown' , NULL)
    ,IF(0 < d.`ICD10.03.Inactive`, 'ICD10.03.Inactive', NULL)
    ,IF(0 < d.`ICD10.04.Unknown` , 'ICD10.04.Unknown' , NULL)
    ,IF(0 < d.`ICD10.04.Inactive`, 'ICD10.04.Inactive', NULL)
    ,IF(0 < d.`ICD10.05.Unknown` , 'ICD10.05.Unknown' , NULL)
    ,IF(0 < d.`ICD10.05.Inactive`, 'ICD10.05.Inactive', NULL)
    ,IF(0 < d.`ICD10.06.Unknown` , 'ICD10.06.Unknown' , NULL)
    ,IF(0 < d.`ICD10.06.Inactive`, 'ICD10.06.Inactive', NULL)
    ,IF(0 < d.`ICD10.07.Unknown` , 'ICD10.07.Unknown' , NULL)
    ,IF(0 < d.`ICD10.07.Inactive`, 'ICD10.07.Inactive', NULL)
    ,IF(0 < d.`ICD10.08.Unknown` , 'ICD10.08.Unknown' , NULL)
    ,IF(0 < d.`ICD10.08.Inactive`, 'ICD10.08.Inactive', NULL)
    ,IF(0 < d.`ICD10.09.Unknown` , 'ICD10.09.Unknown' , NULL)
    ,IF(0 < d.`ICD10.09.Inactive`, 'ICD10.09.Inactive', NULL)
    ,IF(0 < d.`ICD10.10.Unknown` , 'ICD10.10.Unknown' , NULL)
    ,IF(0 < d.`ICD10.10.Inactive`, 'ICD10.10.Inactive', NULL)
    ,IF(0 < d.`ICD10.11.Unknown` , 'ICD10.11.Unknown' , NULL)
    ,IF(0 < d.`ICD10.11.Inactive`, 'ICD10.11.Inactive', NULL)
    ,IF(0 < d.`ICD10.12.Unknown` , 'ICD10.12.Unknown' , NULL)
    ,IF(0 < d.`ICD10.12.Inactive`, 'ICD10.12.Inactive', NULL)
    ); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_OrderID INT; --
  DECLARE V_ActiveOnly BIT; --

  -- P_OrderID
  -- 'ActiveOnly' - all orders that have details with State != 'Closed'
  -- number - just one
  -- all details regardless of state

  IF (P_OrderID = 'ActiveOnly') THEN
    SET V_OrderID = null; --
    SET V_ActiveOnly = 1; --
  ELSEIF (P_OrderID REGEXP '^(\-|\+){0,1}([0-9]+)$') THEN
    SET V_OrderID = CAST(P_OrderID as signed); --
    SET V_ActiveOnly = 0; --
  ELSE
    SET V_OrderID = null; --
    SET V_ActiveOnly = 0; --
  END IF; --

  IF (V_OrderID IS NOT NULL) THEN
    UPDATE tbl_order
    SET `MIR` =
      CONCAT_WS(','
      ,IF(CustomerID   IS NULL, 'CustomerID'  , null)
      ,IF(DeliveryDate IS NULL, 'DeliveryDate', null)
      ,IF(BillDate     IS NULL, 'BillDate'    , null)
      )
    WHERE (ID = V_OrderID); --
  ELSEIF (V_ActiveOnly != 1) THEN
    UPDATE tbl_order
    SET `MIR` =
      CONCAT_WS(','
      ,IF(CustomerID   IS NULL, 'CustomerID'  , null)
      ,IF(DeliveryDate IS NULL, 'DeliveryDate', null)
      ,IF(BillDate     IS NULL, 'BillDate'    , null)
      ); --
  ELSE
    UPDATE tbl_order as o
    INNER JOIN
           (SELECT DISTINCT CustomerID, OrderID
            FROM view_orderdetails
            WHERE (IsActive = 1)
           ) as d on d.CustomerID = o.CustomerID and d.OrderID = o.ID
    SET o.`MIR` =
      CONCAT_WS(','
      ,IF(o.CustomerID   IS NULL, 'CustomerID'  , null)
      ,IF(o.DeliveryDate IS NULL, 'DeliveryDate', null)
      ,IF(o.BillDate     IS NULL, 'BillDate'    , null)
      ); --
  END IF; --

  UPDATE tbl_order as o
  INNER JOIN
         (SELECT CustomerID, OrderID
          , SUM(0 < FIND_IN_SET('Customer.Inactive', `MIR.ORDER`)) AS `Customer.Inactive`
          , SUM(0 < FIND_IN_SET('Customer.MIR'     , `MIR.ORDER`)) AS `Customer.MIR`
          , SUM(0 < FIND_IN_SET('Policy1.Required' , `MIR.ORDER`)) AS `Policy1.Required`
          , SUM(0 < FIND_IN_SET('Policy1.MIR'      , `MIR.ORDER`)) AS `Policy1.MIR`
          , SUM(0 < FIND_IN_SET('Policy2.Required' , `MIR.ORDER`)) AS `Policy2.Required`
          , SUM(0 < FIND_IN_SET('Policy2.MIR'      , `MIR.ORDER`)) AS `Policy2.MIR`
          , SUM(0 < FIND_IN_SET('Facility.MIR'     , `MIR.ORDER`)) AS `Facility.MIR`
          , SUM(0 < FIND_IN_SET('PosType.Required' , `MIR.ORDER`)) AS `PosType.Required`
          , SUM(0 < FIND_IN_SET('ICD9.Required'    , `MIR.ORDER`)) AS `ICD9.Required`
          , SUM(0 < FIND_IN_SET('ICD9.1.Unknown'   , `MIR.ORDER`)) AS `ICD9.1.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.1.Inactive'  , `MIR.ORDER`)) AS `ICD9.1.Inactive`
          , SUM(0 < FIND_IN_SET('ICD9.2.Unknown'   , `MIR.ORDER`)) AS `ICD9.2.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.2.Inactive'  , `MIR.ORDER`)) AS `ICD9.2.Inactive`
          , SUM(0 < FIND_IN_SET('ICD9.3.Unknown'   , `MIR.ORDER`)) AS `ICD9.3.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.3.Inactive'  , `MIR.ORDER`)) AS `ICD9.3.Inactive`
          , SUM(0 < FIND_IN_SET('ICD9.4.Unknown'   , `MIR.ORDER`)) AS `ICD9.4.Unknown`
          , SUM(0 < FIND_IN_SET('ICD9.4.Inactive'  , `MIR.ORDER`)) AS `ICD9.4.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.Required'   , `MIR.ORDER`)) AS `ICD10.Required`
          , SUM(0 < FIND_IN_SET('ICD10.01.Unknown' , `MIR.ORDER`)) AS `ICD10.01.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.01.Inactive', `MIR.ORDER`)) AS `ICD10.01.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.02.Unknown' , `MIR.ORDER`)) AS `ICD10.02.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.02.Inactive', `MIR.ORDER`)) AS `ICD10.02.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.03.Unknown' , `MIR.ORDER`)) AS `ICD10.03.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.03.Inactive', `MIR.ORDER`)) AS `ICD10.03.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.04.Unknown' , `MIR.ORDER`)) AS `ICD10.04.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.04.Inactive', `MIR.ORDER`)) AS `ICD10.04.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.05.Unknown' , `MIR.ORDER`)) AS `ICD10.05.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.05.Inactive', `MIR.ORDER`)) AS `ICD10.05.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.06.Unknown' , `MIR.ORDER`)) AS `ICD10.06.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.06.Inactive', `MIR.ORDER`)) AS `ICD10.06.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.07.Unknown' , `MIR.ORDER`)) AS `ICD10.07.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.07.Inactive', `MIR.ORDER`)) AS `ICD10.07.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.08.Unknown' , `MIR.ORDER`)) AS `ICD10.08.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.08.Inactive', `MIR.ORDER`)) AS `ICD10.08.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.09.Unknown' , `MIR.ORDER`)) AS `ICD10.09.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.09.Inactive', `MIR.ORDER`)) AS `ICD10.09.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.10.Unknown' , `MIR.ORDER`)) AS `ICD10.10.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.10.Inactive', `MIR.ORDER`)) AS `ICD10.10.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.11.Unknown' , `MIR.ORDER`)) AS `ICD10.11.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.11.Inactive', `MIR.ORDER`)) AS `ICD10.11.Inactive`
          , SUM(0 < FIND_IN_SET('ICD10.12.Unknown' , `MIR.ORDER`)) AS `ICD10.12.Unknown`
          , SUM(0 < FIND_IN_SET('ICD10.12.Inactive', `MIR.ORDER`)) AS `ICD10.12.Inactive`
          FROM view_orderdetails
          WHERE IF(V_OrderID IS NOT NULL, OrderID = V_OrderID, V_ActiveOnly != 1 or IsActive = 1)
            AND (0 < FIND_IN_SET('Customer.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Customer.MIR'     , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy1.Required' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy1.MIR'      , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy2.Required' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Policy2.MIR'      , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('Facility.MIR'     , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('PosType.Required' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.Required'    , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.1.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.1.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.2.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.2.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.3.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.3.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.4.Unknown'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD9.4.Inactive'  , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.Required'   , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.01.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.01.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.02.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.02.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.03.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.03.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.04.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.04.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.05.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.05.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.06.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.06.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.07.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.07.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.08.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.08.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.09.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.09.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.10.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.10.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.11.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.11.Inactive', `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.12.Unknown' , `MIR.ORDER`)
              OR 0 < FIND_IN_SET('ICD10.12.Inactive', `MIR.ORDER`))
          GROUP BY CustomerID, OrderID
         ) as d on d.CustomerID = o.CustomerID and d.OrderID = o.ID
  SET o.`MIR` = CONCAT_WS(','
    ,o.MIR
    ,IF(0 < d.`Customer.Inactive`, 'Customer.Inactive', NULL)
    ,IF(0 < d.`Customer.MIR`     , 'Customer.MIR'     , NULL)
    ,IF(0 < d.`Policy1.Required` , 'Policy1.Required' , NULL)
    ,IF(0 < d.`Policy1.MIR`      , 'Policy1.MIR'      , NULL)
    ,IF(0 < d.`Policy2.Required` , 'Policy2.Required' , NULL)
    ,IF(0 < d.`Policy2.MIR`      , 'Policy2.MIR'      , NULL)
    ,IF(0 < d.`Facility.MIR`     , 'Facility.MIR'     , NULL)
    ,IF(0 < d.`PosType.Required` , 'PosType.Required' , NULL)
    ,IF(0 < d.`ICD9.Required`    , 'ICD9.Required'    , NULL)
    ,IF(0 < d.`ICD9.1.Unknown`   , 'ICD9.1.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.1.Inactive`  , 'ICD9.1.Inactive'  , NULL)
    ,IF(0 < d.`ICD9.2.Unknown`   , 'ICD9.2.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.2.Inactive`  , 'ICD9.2.Inactive'  , NULL)
    ,IF(0 < d.`ICD9.3.Unknown`   , 'ICD9.3.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.3.Inactive`  , 'ICD9.3.Inactive'  , NULL)
    ,IF(0 < d.`ICD9.4.Unknown`   , 'ICD9.4.Unknown'   , NULL)
    ,IF(0 < d.`ICD9.4.Inactive`  , 'ICD9.4.Inactive'  , NULL)
    ,IF(0 < d.`ICD10.Required`   , 'ICD10.Required'   , NULL)
    ,IF(0 < d.`ICD10.01.Unknown` , 'ICD10.01.Unknown' , NULL)
    ,IF(0 < d.`ICD10.01.Inactive`, 'ICD10.01.Inactive', NULL)
    ,IF(0 < d.`ICD10.02.Unknown` , 'ICD10.02.Unknown' , NULL)
    ,IF(0 < d.`ICD10.02.Inactive`, 'ICD10.02.Inactive', NULL)
    ,IF(0 < d.`ICD10.03.Unknown` , 'ICD10.03.Unknown' , NULL)
    ,IF(0 < d.`ICD10.03.Inactive`, 'ICD10.03.Inactive', NULL)
    ,IF(0 < d.`ICD10.04.Unknown` , 'ICD10.04.Unknown' , NULL)
    ,IF(0 < d.`ICD10.04.Inactive`, 'ICD10.04.Inactive', NULL)
    ,IF(0 < d.`ICD10.05.Unknown` , 'ICD10.05.Unknown' , NULL)
    ,IF(0 < d.`ICD10.05.Inactive`, 'ICD10.05.Inactive', NULL)
    ,IF(0 < d.`ICD10.06.Unknown` , 'ICD10.06.Unknown' , NULL)
    ,IF(0 < d.`ICD10.06.Inactive`, 'ICD10.06.Inactive', NULL)
    ,IF(0 < d.`ICD10.07.Unknown` , 'ICD10.07.Unknown' , NULL)
    ,IF(0 < d.`ICD10.07.Inactive`, 'ICD10.07.Inactive', NULL)
    ,IF(0 < d.`ICD10.08.Unknown` , 'ICD10.08.Unknown' , NULL)
    ,IF(0 < d.`ICD10.08.Inactive`, 'ICD10.08.Inactive', NULL)
    ,IF(0 < d.`ICD10.09.Unknown` , 'ICD10.09.Unknown' , NULL)
    ,IF(0 < d.`ICD10.09.Inactive`, 'ICD10.09.Inactive', NULL)
    ,IF(0 < d.`ICD10.10.Unknown` , 'ICD10.10.Unknown' , NULL)
    ,IF(0 < d.`ICD10.10.Inactive`, 'ICD10.10.Inactive', NULL)
    ,IF(0 < d.`ICD10.11.Unknown` , 'ICD10.11.Unknown' , NULL)
    ,IF(0 < d.`ICD10.11.Inactive`, 'ICD10.11.Inactive', NULL)
    ,IF(0 < d.`ICD10.12.Unknown` , 'ICD10.12.Unknown' , NULL)
    ,IF(0 < d.`ICD10.12.Inactive`, 'ICD10.12.Inactive', NULL)
    ); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## mir_update_orderdetails

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_OrderID INT; --
  DECLARE V_ActiveOnly BIT; --

  --  now we make field tbl_order.SaleType informative only
  --  now we make field view_orderdetails.IsRetail informative only -
  --  user should use BillIns1 .. BillIns4 for the same purpose

  -- P_OrderID
  -- 'ActiveOnly' - all details with State != 'Closed'
  -- number - just one
  -- all details regardless of state

  IF (P_OrderID = 'ActiveOnly') THEN
    SET V_OrderID = null; --
    SET V_ActiveOnly = 1; --
  ELSEIF (P_OrderID REGEXP '^(\-|\+){0,1}([0-9]+)$') THEN
    SET V_OrderID = CAST(P_OrderID as signed); --
    SET V_ActiveOnly = 0; --
  ELSE
    SET V_OrderID = null; --
    SET V_ActiveOnly = 0; --
  END IF; --

  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         LEFT JOIN tbl_pricecode_item as pricing ON pricing.InventoryItemID = details.InventoryItemID
                                                AND pricing.PriceCodeID     = details.PriceCodeID
         LEFT JOIN tbl_inventoryitem as item ON details.InventoryItemID = item.ID
  SET details.`MIR` = CONCAT_WS(','
    , IF(item.ID IS NULL, 'InventoryItemID', null)
    , IF(pricing.ID IS NULL, 'PriceCodeID', null)
    , CASE WHEN details.SaleRentType = 'Medicare Oxygen Rental' AND details.IsOxygen != 1
           THEN 'SaleRentType'
           WHEN details.ActualSaleRentType = '' THEN 'SaleRentType' ELSE null END
    , CASE WHEN details.ActualBillItemOn   = '' THEN 'BillItemOn'   ELSE null END
    , CASE WHEN details.ActualBilledWhen   = '' THEN 'BilledWhen'   ELSE null END
    , CASE WHEN details.ActualOrderedWhen  = '' THEN 'OrderedWhen'  ELSE null END
    , IF((details.IsActive = 1) AND (details.EndDate < _order.BillDate), 'EndDate.Invalid', null)
    , IF((details.State = 'Pickup') AND (details.EndDate IS NULL), 'EndDate.Unconfirmed', null)
    , IF((details.SaleRentType IN ('Capped Rental', 'Parental Capped Rental')) AND (IFNULL(details.Modifier1, '') = ''), 'Modifier1', null)
    , IF((details.SaleRentType IN ('Capped Rental', 'Parental Capped Rental')) AND (_order.DeliveryDate < '2006-01-01') AND (details.BillingMonth BETWEEN 12 AND 13) AND (details.Modifier3 NOT IN ('BP', 'BR', 'BU')), 'Modifier3', null)
    , IF((details.SaleRentType IN ('Capped Rental', 'Parental Capped Rental')) AND (_order.DeliveryDate < '2006-01-01') AND (details.BillingMonth BETWEEN 14 AND 15) AND (details.Modifier3 NOT IN ('BR', 'BU')), 'Modifier3', null)
    , null)
  , details.`MIR.ORDER` = ''
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1); --

  -- common part, no ICD9 or ICD10
  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         INNER JOIN tbl_customer as customer ON customer.ID = _order.CustomerID
         INNER JOIN tbl_pricecode_item as pricing ON pricing.InventoryItemID = details.InventoryItemID
                                                 AND pricing.PriceCodeID     = details.PriceCodeID
         LEFT JOIN tbl_customer_insurance as policy1 ON _order.CustomerInsurance1_ID = policy1.ID
                                                    AND _order.CustomerID            = policy1.CustomerID
         LEFT JOIN tbl_customer_insurance as policy2 ON _order.CustomerInsurance2_ID = policy2.ID
                                                    AND _order.CustomerID            = policy2.CustomerID
         LEFT JOIN tbl_cmnform as cmnform ON cmnform.ID         = details.CMNFormID
                                         AND cmnform.CustomerID = details.CustomerID
         LEFT JOIN tbl_facility as facility ON _order.FacilityID = facility.ID
         LEFT JOIN tbl_postype as postype ON _order.POSTypeID = postype.ID
  SET details.`MIR` = CONCAT_WS(','
    , details.`MIR`
    , IF(IFNULL(details.OrderedQuantity  ,  0) =  0, 'OrderedQuantity'  , null)
    , IF(IFNULL(details.OrderedUnits     , '') = '', 'OrderedUnits'     , null)
    , IF(IFNULL(details.OrderedConverter ,  0) =  0, 'OrderedConverter' , null)
    , IF(IFNULL(details.BilledQuantity   ,  0) =  0, 'BilledQuantity'   , null)
    , IF(IFNULL(details.BilledUnits      , '') = '', 'BilledUnits'      , null)
    , IF(IFNULL(details.BilledConverter  ,  0) =  0, 'BilledConverter'  , null)
    , IF(IFNULL(details.DeliveryQuantity ,  0) =  0, 'DeliveryQuantity' , null)
    , IF(IFNULL(details.DeliveryUnits    , '') = '', 'DeliveryUnits'    , null)
    , IF(IFNULL(details.DeliveryConverter,  0) =  0, 'DeliveryConverter', null)
    , IF(IFNULL(details.BillingCode      , '') = '', 'BillingCode'      , null)
    , CASE WHEN '2015-10-01' <= details.DOSFrom THEN null
           WHEN IFNULL(details.DXPointer  , '') REGEXP '[1-4](,[1-4])*' THEN null
           ELSE 'DXPointer9' END
    , CASE WHEN details.DOSFrom < '2015-10-01' THEN null
           WHEN IFNULL(details.DXPointer10, '') REGEXP '([1-9]|1[0-2])(,([1-9]|1[0-2]))*' THEN null
           ELSE 'DXPointer10' END
    , IF((IFNULL(pricing.DefaultCMNType, '') != '') AND (cmnform.ID IS NULL), 'CMNForm.Required', null)
    , IF((IFNULL(pricing.DefaultCMNType, '') != '') AND (cmnform.MIR != '' ), 'CMNForm.MIR'     , null)
    , IF((cmnform.CmnType = 'DMERC DRORDER') AND (cmnform.MIR != ''), 'CMNForm.MIR', null)
    , CASE WHEN cmnform.InitialDate           is null THEN null
           WHEN cmnform.EstimatedLengthOfNeed is null THEN null
           WHEN cmnform.EstimatedLengthOfNeed < 0     THEN null
           WHEN 99 <= cmnform.EstimatedLengthOfNeed   THEN null -- 99 = LIFETIME
           WHEN (cmnform.CMNType     IN ('DMERC 484.2', 'DME 484.03'))
            AND (DATE_ADD(cmnform.InitialDate, INTERVAL 12 MONTH) <= details.DOSFrom)
            AND (cmnform.RecertificationDate is null)
           THEN 'CMNForm.RecertificationDate'
           WHEN (cmnform.CMNType     IN ('DMERC 484.2', 'DME 484.03'))
            AND (DATE_ADD(cmnform.InitialDate, INTERVAL 12 MONTH) <= details.DOSFrom)
            AND (DATE_ADD(cmnform.RecertificationDate, INTERVAL 12 MONTH) <= details.DOSFrom)
           THEN 'CMNForm.FormExpired'
           WHEN (cmnform.CMNType NOT IN ('DMERC 484.2', 'DME 484.03'))
            AND (DATE_ADD(cmnform.InitialDate, INTERVAL cmnform.EstimatedLengthOfNeed MONTH) <= details.DOSFrom)
           THEN 'CMNForm.FormExpired'
           ELSE null END
    , CASE WHEN details.AuthorizationNumber is null THEN null
           WHEN details.AuthorizationNumber = ''    THEN null
           WHEN details.AuthorizationExpirationDate < details.DOSFrom THEN 'AuthorizationNumber.Expired'
           WHEN details.AuthorizationExpirationDate <= details.DOSTo  THEN 'AuthorizationNumber.Expires'
           ELSE null END
    , null) -- MIR
  , details.`MIR.ORDER` = CONCAT_WS(','
    , IF((details.BillIns1 = 1) AND (policy1.ID IS NULL), 'Policy1.Required', null)
    , IF((details.BillIns1 = 1) AND (policy1.MIR != '' ), 'Policy1.MIR'     , null)
    , IF((details.BillIns1 = 2) AND (policy2.MIR != '' ), 'Policy2.MIR'     , null)
    , IF(customer.InactiveDate < Now(), 'Customer.Inactive', null)
    , IF(customer.MIR != '', 'Customer.MIR', null)
    , IF(facility.MIR != '', 'Facility.MIR', null)
    , IF(postype.ID IS NULL, 'PosType.Required', null)
    , null)
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1)
    AND (customer.CommercialAccount = 0)
    AND (details.IsZeroAmount = 0)
    AND ((details.BillIns1 = 1) OR (details.BillIns2 = 1) OR (details.BillIns3 = 1) OR (details.BillIns4 = 1)); --

  -- ICD9 is only for orders before 2015-10-01
  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         INNER JOIN tbl_customer as customer ON customer.ID = _order.CustomerID
         LEFT JOIN tbl_icd9 as icd9_1 ON _order.ICD9_1 = icd9_1.Code
         LEFT JOIN tbl_icd9 as icd9_2 ON _order.ICD9_2 = icd9_2.Code
         LEFT JOIN tbl_icd9 as icd9_3 ON _order.ICD9_3 = icd9_3.Code
         LEFT JOIN tbl_icd9 as icd9_4 ON _order.ICD9_4 = icd9_4.Code
  SET details.`MIR.ORDER` = CONCAT_WS(','
    , details.`MIR.ORDER`
    , CASE WHEN _order.ICD9_1 != ''                THEN null
           WHEN _order.ICD9_2 != ''                THEN null
           WHEN _order.ICD9_3 != ''                THEN null
           WHEN _order.ICD9_4 != ''                THEN null
           ELSE 'ICD9.Required' END
    , CASE WHEN IFNULL(_order.ICD9_1, '') = ''          THEN null
           WHEN icd9_1.Code IS NULL                     THEN 'ICD9.1.Unknown'
           WHEN icd9_1.InactiveDate <= _order.OrderDate THEN 'ICD9.1.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD9_2, '') = ''          THEN null
           WHEN icd9_2.Code IS NULL                     THEN 'ICD9.2.Unknown'
           WHEN icd9_2.InactiveDate <= _order.OrderDate THEN 'ICD9.2.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD9_3, '') = ''          THEN null
           WHEN icd9_3.Code IS NULL                     THEN 'ICD9.3.Unknown'
           WHEN icd9_3.InactiveDate <= _order.OrderDate THEN 'ICD9.3.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD9_4, '') = ''          THEN null
           WHEN icd9_4.Code IS NULL                     THEN 'ICD9.4.Unknown'
           WHEN icd9_4.InactiveDate <= _order.OrderDate THEN 'ICD9.4.Inactive'
           ELSE null END
    , null)
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1)
    AND (customer.CommercialAccount = 0)
    AND (details.IsZeroAmount = 0)
    AND (details.DOSFrom < '2015-10-01')
    AND (details.DXPointer != '')
    AND ((details.BillIns1 = 1) OR (details.BillIns2 = 1) OR (details.BillIns3 = 1) OR (details.BillIns4 = 1)); --

  -- ICD10 is only for orders after 2015-10-01
  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         INNER JOIN tbl_customer as customer ON customer.ID = _order.CustomerID
         LEFT JOIN tbl_icd10 as icd10_01 ON _order.ICD10_01 = icd10_01.Code
         LEFT JOIN tbl_icd10 as icd10_02 ON _order.ICD10_02 = icd10_02.Code
         LEFT JOIN tbl_icd10 as icd10_03 ON _order.ICD10_03 = icd10_03.Code
         LEFT JOIN tbl_icd10 as icd10_04 ON _order.ICD10_04 = icd10_04.Code
         LEFT JOIN tbl_icd10 as icd10_05 ON _order.ICD10_05 = icd10_05.Code
         LEFT JOIN tbl_icd10 as icd10_06 ON _order.ICD10_06 = icd10_06.Code
         LEFT JOIN tbl_icd10 as icd10_07 ON _order.ICD10_07 = icd10_07.Code
         LEFT JOIN tbl_icd10 as icd10_08 ON _order.ICD10_08 = icd10_08.Code
         LEFT JOIN tbl_icd10 as icd10_09 ON _order.ICD10_09 = icd10_09.Code
         LEFT JOIN tbl_icd10 as icd10_10 ON _order.ICD10_10 = icd10_10.Code
         LEFT JOIN tbl_icd10 as icd10_11 ON _order.ICD10_11 = icd10_11.Code
         LEFT JOIN tbl_icd10 as icd10_12 ON _order.ICD10_12 = icd10_12.Code
  SET details.`MIR.ORDER` = CONCAT_WS(','
    , details.`MIR.ORDER`
    , CASE WHEN _order.ICD10_01 != '' THEN null
           WHEN _order.ICD10_02 != '' THEN null
           WHEN _order.ICD10_03 != '' THEN null
           WHEN _order.ICD10_04 != '' THEN null
           WHEN _order.ICD10_05 != '' THEN null
           WHEN _order.ICD10_06 != '' THEN null
           WHEN _order.ICD10_07 != '' THEN null
           WHEN _order.ICD10_08 != '' THEN null
           WHEN _order.ICD10_09 != '' THEN null
           WHEN _order.ICD10_10 != '' THEN null
           WHEN _order.ICD10_11 != '' THEN null
           WHEN _order.ICD10_12 != '' THEN null
           ELSE 'ICD10.Required' END
    , CASE WHEN IFNULL(_order.ICD10_01, '') = ''          THEN null
           WHEN icd10_01.Code IS NULL                     THEN 'ICD10.01.Unknown'
           WHEN icd10_01.InactiveDate <= _order.OrderDate THEN 'ICD10.01.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_02, '') = ''          THEN null
           WHEN icd10_02.Code IS NULL                     THEN 'ICD10.02.Unknown'
           WHEN icd10_02.InactiveDate <= _order.OrderDate THEN 'ICD10.02.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_03, '') = ''          THEN null
           WHEN icd10_03.Code IS NULL                     THEN 'ICD10.03.Unknown'
           WHEN icd10_03.InactiveDate <= _order.OrderDate THEN 'ICD10.03.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_04, '') = ''          THEN null
           WHEN icd10_04.Code IS NULL                     THEN 'ICD10.04.Unknown'
           WHEN icd10_04.InactiveDate <= _order.OrderDate THEN 'ICD10.04.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_05, '') = ''          THEN null
           WHEN icd10_05.Code IS NULL                     THEN 'ICD10.05.Unknown'
           WHEN icd10_05.InactiveDate <= _order.OrderDate THEN 'ICD10.05.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_06, '') = ''          THEN null
           WHEN icd10_06.Code IS NULL                     THEN 'ICD10.06.Unknown'
           WHEN icd10_06.InactiveDate <= _order.OrderDate THEN 'ICD10.06.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_07, '') = ''          THEN null
           WHEN icd10_07.Code IS NULL                     THEN 'ICD10.07.Unknown'
           WHEN icd10_07.InactiveDate <= _order.OrderDate THEN 'ICD10.07.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_08, '') = ''          THEN null
           WHEN icd10_08.Code IS NULL                     THEN 'ICD10.08.Unknown'
           WHEN icd10_08.InactiveDate <= _order.OrderDate THEN 'ICD10.08.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_09, '') = ''          THEN null
           WHEN icd10_09.Code IS NULL                     THEN 'ICD10.09.Unknown'
           WHEN icd10_09.InactiveDate <= _order.OrderDate THEN 'ICD10.09.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_10, '') = ''          THEN null
           WHEN icd10_10.Code IS NULL                     THEN 'ICD10.10.Unknown'
           WHEN icd10_10.InactiveDate <= _order.OrderDate THEN 'ICD10.10.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_11, '') = ''          THEN null
           WHEN icd10_11.Code IS NULL                     THEN 'ICD10.11.Unknown'
           WHEN icd10_11.InactiveDate <= _order.OrderDate THEN 'ICD10.11.Inactive'
           ELSE null END
    , CASE WHEN IFNULL(_order.ICD10_12, '') = ''          THEN null
           WHEN icd10_12.Code IS NULL                     THEN 'ICD10.12.Unknown'
           WHEN icd10_12.InactiveDate <= _order.OrderDate THEN 'ICD10.12.Inactive'
           ELSE null END
    , null)
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1)
    AND (customer.CommercialAccount = 0)
    AND (details.IsZeroAmount = 0)
    AND ('2015-10-01' <= details.DOSFrom)
    AND (details.DXPointer10 != '')
    AND ((details.BillIns1 = 1) OR (details.BillIns2 = 1) OR (details.BillIns3 = 1) OR (details.BillIns4 = 1)); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_OrderID INT; --
  DECLARE V_ActiveOnly BIT; --

  --  now we make field tbl_order.SaleType informative only
  --  now we make field view_orderdetails.IsRetail informative only -
  --  user should use BillIns1 .. BillIns4 for the same purpose

  -- P_OrderID
  -- 'ActiveOnly' - all details with State != 'Closed'
  -- number - just one
  -- all details regardless of state

  IF (P_OrderID = 'ActiveOnly') THEN
    SET V_OrderID = null; --
    SET V_ActiveOnly = 1; --
  ELSEIF (P_OrderID REGEXP '^(\-|\+){0,1}([0-9]+)$') THEN
    SET V_OrderID = CAST(P_OrderID as signed); --
    SET V_ActiveOnly = 0; --
  ELSE
    SET V_OrderID = null; --
    SET V_ActiveOnly = 0; --
  END IF; --

  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         LEFT JOIN tbl_pricecode_item as pricing ON pricing.InventoryItemID = details.InventoryItemID
                                                AND pricing.PriceCodeID     = details.PriceCodeID
         LEFT JOIN tbl_inventoryitem as item ON details.InventoryItemID = item.ID
  SET details.`MIR` = CONCAT_WS(','
    , IF(item.ID IS NULL, 'InventoryItemID', null)
    , IF(pricing.ID IS NULL, 'PriceCodeID', null)
    , CASE WHEN details.SaleRentType = 'Medicare Oxygen Rental' AND details.IsOxygen != 1
           THEN 'SaleRentType'
           WHEN details.ActualSaleRentType = '' THEN 'SaleRentType' ELSE null END
    , CASE WHEN details.ActualBillItemOn   = '' THEN 'BillItemOn'   ELSE null END
    , CASE WHEN details.ActualBilledWhen   = '' THEN 'BilledWhen'   ELSE null END
    , CASE WHEN details.ActualOrderedWhen  = '' THEN 'OrderedWhen'  ELSE null END
    , IF((details.IsActive = 1) AND (details.EndDate < _order.BillDate), 'EndDate.Invalid', null)
    , IF((details.State = 'Pickup') AND (details.EndDate IS NULL), 'EndDate.Unconfirmed', null)
    , IF((details.SaleRentType IN ('Capped Rental', 'Parental Capped Rental')) AND (COALESCE(details.Modifier1, '') = ''), 'Modifier1', null)
    , IF((details.SaleRentType IN ('Capped Rental', 'Parental Capped Rental')) AND (_order.DeliveryDate < '2006-01-01') AND (details.BillingMonth BETWEEN 12 AND 13) AND (details.Modifier3 NOT IN ('BP', 'BR', 'BU')), 'Modifier3', null)
    , IF((details.SaleRentType IN ('Capped Rental', 'Parental Capped Rental')) AND (_order.DeliveryDate < '2006-01-01') AND (details.BillingMonth BETWEEN 14 AND 15) AND (details.Modifier3 NOT IN ('BR', 'BU')), 'Modifier3', null)
    , null)
  , details.`MIR.ORDER` = ''
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1); --

  -- common part, no ICD9 or ICD10
  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         INNER JOIN tbl_customer as customer ON customer.ID = _order.CustomerID
         INNER JOIN tbl_pricecode_item as pricing ON pricing.InventoryItemID = details.InventoryItemID
                                                 AND pricing.PriceCodeID     = details.PriceCodeID
         LEFT JOIN tbl_customer_insurance as policy1 ON _order.CustomerInsurance1_ID = policy1.ID
                                                    AND _order.CustomerID            = policy1.CustomerID
         LEFT JOIN tbl_customer_insurance as policy2 ON _order.CustomerInsurance2_ID = policy2.ID
                                                    AND _order.CustomerID            = policy2.CustomerID
         LEFT JOIN tbl_cmnform as cmnform ON cmnform.ID         = details.CMNFormID
                                         AND cmnform.CustomerID = details.CustomerID
         LEFT JOIN tbl_facility as facility ON _order.FacilityID = facility.ID
         LEFT JOIN tbl_postype as postype ON _order.POSTypeID = postype.ID
  SET details.`MIR` = CONCAT_WS(','
    , details.`MIR`
    , IF(COALESCE(details.OrderedQuantity  ,  0) =  0, 'OrderedQuantity'  , null)
    , IF(COALESCE(details.OrderedUnits     , '') = '', 'OrderedUnits'     , null)
    , IF(COALESCE(details.OrderedConverter ,  0) =  0, 'OrderedConverter' , null)
    , IF(COALESCE(details.BilledQuantity   ,  0) =  0, 'BilledQuantity'   , null)
    , IF(COALESCE(details.BilledUnits      , '') = '', 'BilledUnits'      , null)
    , IF(COALESCE(details.BilledConverter  ,  0) =  0, 'BilledConverter'  , null)
    , IF(COALESCE(details.DeliveryQuantity ,  0) =  0, 'DeliveryQuantity' , null)
    , IF(COALESCE(details.DeliveryUnits    , '') = '', 'DeliveryUnits'    , null)
    , IF(COALESCE(details.DeliveryConverter,  0) =  0, 'DeliveryConverter', null)
    , IF(COALESCE(details.BillingCode      , '') = '', 'BillingCode'      , null)
    , CASE WHEN '2015-10-01' <= details.DOSFrom THEN null
           WHEN COALESCE(details.DXPointer  , '') REGEXP '[1-4](,[1-4])*' THEN null
           ELSE 'DXPointer9' END
    , CASE WHEN details.DOSFrom < '2015-10-01' THEN null
           WHEN COALESCE(details.DXPointer10, '') REGEXP '([1-9]|1[0-2])(,([1-9]|1[0-2]))*' THEN null
           ELSE 'DXPointer10' END
    , IF((COALESCE(pricing.DefaultCMNType, '') != '') AND (cmnform.ID IS NULL), 'CMNForm.Required', null)
    , IF((COALESCE(pricing.DefaultCMNType, '') != '') AND (cmnform.MIR != '' ), 'CMNForm.MIR'     , null)
    , IF((cmnform.CmnType = 'DMERC DRORDER') AND (cmnform.MIR != ''), 'CMNForm.MIR', null)
    , CASE WHEN cmnform.InitialDate           is null THEN null
           WHEN cmnform.EstimatedLengthOfNeed is null THEN null
           WHEN cmnform.EstimatedLengthOfNeed < 0     THEN null
           WHEN 99 <= cmnform.EstimatedLengthOfNeed   THEN null -- 99 = LIFETIME
           WHEN (cmnform.CMNType     IN ('DMERC 484.2', 'DME 484.03'))
            AND (DATE_ADD(cmnform.InitialDate, INTERVAL 12 MONTH) <= details.DOSFrom)
            AND (cmnform.RecertificationDate is null)
           THEN 'CMNForm.RecertificationDate'
           WHEN (cmnform.CMNType     IN ('DMERC 484.2', 'DME 484.03'))
            AND (DATE_ADD(cmnform.InitialDate, INTERVAL 12 MONTH) <= details.DOSFrom)
            AND (DATE_ADD(cmnform.RecertificationDate, INTERVAL 12 MONTH) <= details.DOSFrom)
           THEN 'CMNForm.FormExpired'
           WHEN (cmnform.CMNType NOT IN ('DMERC 484.2', 'DME 484.03'))
            AND (DATE_ADD(cmnform.InitialDate, INTERVAL cmnform.EstimatedLengthOfNeed MONTH) <= details.DOSFrom)
           THEN 'CMNForm.FormExpired'
           ELSE null END
    , CASE WHEN details.AuthorizationNumber is null THEN null
           WHEN details.AuthorizationNumber = ''    THEN null
           WHEN details.AuthorizationExpirationDate < details.DOSFrom THEN 'AuthorizationNumber.Expired'
           WHEN details.AuthorizationExpirationDate <= details.DOSTo  THEN 'AuthorizationNumber.Expires'
           ELSE null END
    , null) -- MIR
  , details.`MIR.ORDER` = CONCAT_WS(','
    , IF((details.BillIns1 = 1) AND (policy1.ID IS NULL), 'Policy1.Required', null)
    , IF((details.BillIns1 = 1) AND (policy1.MIR != '' ), 'Policy1.MIR'     , null)
    , IF((details.BillIns1 = 2) AND (policy2.MIR != '' ), 'Policy2.MIR'     , null)
    , IF(customer.InactiveDate < Now(), 'Customer.Inactive', null)
    , IF(customer.MIR != '', 'Customer.MIR', null)
    , IF(facility.MIR != '', 'Facility.MIR', null)
    , IF(postype.ID IS NULL, 'PosType.Required', null)
    , null)
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1)
    AND (customer.CommercialAccount = 0)
    AND (details.IsZeroAmount = 0)
    AND ((details.BillIns1 = 1) OR (details.BillIns2 = 1) OR (details.BillIns3 = 1) OR (details.BillIns4 = 1)); --

  -- ICD9 is only for orders before 2015-10-01
  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         INNER JOIN tbl_customer as customer ON customer.ID = _order.CustomerID
         LEFT JOIN tbl_icd9 as icd9_1 ON _order.ICD9_1 = icd9_1.Code
         LEFT JOIN tbl_icd9 as icd9_2 ON _order.ICD9_2 = icd9_2.Code
         LEFT JOIN tbl_icd9 as icd9_3 ON _order.ICD9_3 = icd9_3.Code
         LEFT JOIN tbl_icd9 as icd9_4 ON _order.ICD9_4 = icd9_4.Code
  SET details.`MIR.ORDER` = CONCAT_WS(','
    , details.`MIR.ORDER`
    , CASE WHEN _order.ICD9_1 != ''                THEN null
           WHEN _order.ICD9_2 != ''                THEN null
           WHEN _order.ICD9_3 != ''                THEN null
           WHEN _order.ICD9_4 != ''                THEN null
           ELSE 'ICD9.Required' END
    , CASE WHEN COALESCE(_order.ICD9_1, '') = ''          THEN null
           WHEN icd9_1.Code IS NULL                     THEN 'ICD9.1.Unknown'
           WHEN icd9_1.InactiveDate <= _order.OrderDate THEN 'ICD9.1.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD9_2, '') = ''          THEN null
           WHEN icd9_2.Code IS NULL                     THEN 'ICD9.2.Unknown'
           WHEN icd9_2.InactiveDate <= _order.OrderDate THEN 'ICD9.2.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD9_3, '') = ''          THEN null
           WHEN icd9_3.Code IS NULL                     THEN 'ICD9.3.Unknown'
           WHEN icd9_3.InactiveDate <= _order.OrderDate THEN 'ICD9.3.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD9_4, '') = ''          THEN null
           WHEN icd9_4.Code IS NULL                     THEN 'ICD9.4.Unknown'
           WHEN icd9_4.InactiveDate <= _order.OrderDate THEN 'ICD9.4.Inactive'
           ELSE null END
    , null)
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1)
    AND (customer.CommercialAccount = 0)
    AND (details.IsZeroAmount = 0)
    AND (details.DOSFrom < '2015-10-01')
    AND (details.DXPointer != '')
    AND ((details.BillIns1 = 1) OR (details.BillIns2 = 1) OR (details.BillIns3 = 1) OR (details.BillIns4 = 1)); --

  -- ICD10 is only for orders after 2015-10-01
  UPDATE view_orderdetails_core as details
         INNER JOIN tbl_order as _order ON details.OrderID    = _order.ID
                                       AND details.CustomerID = _order.CustomerID
         INNER JOIN tbl_customer as customer ON customer.ID = _order.CustomerID
         LEFT JOIN tbl_icd10 as icd10_01 ON _order.ICD10_01 = icd10_01.Code
         LEFT JOIN tbl_icd10 as icd10_02 ON _order.ICD10_02 = icd10_02.Code
         LEFT JOIN tbl_icd10 as icd10_03 ON _order.ICD10_03 = icd10_03.Code
         LEFT JOIN tbl_icd10 as icd10_04 ON _order.ICD10_04 = icd10_04.Code
         LEFT JOIN tbl_icd10 as icd10_05 ON _order.ICD10_05 = icd10_05.Code
         LEFT JOIN tbl_icd10 as icd10_06 ON _order.ICD10_06 = icd10_06.Code
         LEFT JOIN tbl_icd10 as icd10_07 ON _order.ICD10_07 = icd10_07.Code
         LEFT JOIN tbl_icd10 as icd10_08 ON _order.ICD10_08 = icd10_08.Code
         LEFT JOIN tbl_icd10 as icd10_09 ON _order.ICD10_09 = icd10_09.Code
         LEFT JOIN tbl_icd10 as icd10_10 ON _order.ICD10_10 = icd10_10.Code
         LEFT JOIN tbl_icd10 as icd10_11 ON _order.ICD10_11 = icd10_11.Code
         LEFT JOIN tbl_icd10 as icd10_12 ON _order.ICD10_12 = icd10_12.Code
  SET details.`MIR.ORDER` = CONCAT_WS(','
    , details.`MIR.ORDER`
    , CASE WHEN _order.ICD10_01 != '' THEN null
           WHEN _order.ICD10_02 != '' THEN null
           WHEN _order.ICD10_03 != '' THEN null
           WHEN _order.ICD10_04 != '' THEN null
           WHEN _order.ICD10_05 != '' THEN null
           WHEN _order.ICD10_06 != '' THEN null
           WHEN _order.ICD10_07 != '' THEN null
           WHEN _order.ICD10_08 != '' THEN null
           WHEN _order.ICD10_09 != '' THEN null
           WHEN _order.ICD10_10 != '' THEN null
           WHEN _order.ICD10_11 != '' THEN null
           WHEN _order.ICD10_12 != '' THEN null
           ELSE 'ICD10.Required' END
    , CASE WHEN COALESCE(_order.ICD10_01, '') = ''          THEN null
           WHEN icd10_01.Code IS NULL                     THEN 'ICD10.01.Unknown'
           WHEN icd10_01.InactiveDate <= _order.OrderDate THEN 'ICD10.01.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_02, '') = ''          THEN null
           WHEN icd10_02.Code IS NULL                     THEN 'ICD10.02.Unknown'
           WHEN icd10_02.InactiveDate <= _order.OrderDate THEN 'ICD10.02.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_03, '') = ''          THEN null
           WHEN icd10_03.Code IS NULL                     THEN 'ICD10.03.Unknown'
           WHEN icd10_03.InactiveDate <= _order.OrderDate THEN 'ICD10.03.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_04, '') = ''          THEN null
           WHEN icd10_04.Code IS NULL                     THEN 'ICD10.04.Unknown'
           WHEN icd10_04.InactiveDate <= _order.OrderDate THEN 'ICD10.04.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_05, '') = ''          THEN null
           WHEN icd10_05.Code IS NULL                     THEN 'ICD10.05.Unknown'
           WHEN icd10_05.InactiveDate <= _order.OrderDate THEN 'ICD10.05.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_06, '') = ''          THEN null
           WHEN icd10_06.Code IS NULL                     THEN 'ICD10.06.Unknown'
           WHEN icd10_06.InactiveDate <= _order.OrderDate THEN 'ICD10.06.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_07, '') = ''          THEN null
           WHEN icd10_07.Code IS NULL                     THEN 'ICD10.07.Unknown'
           WHEN icd10_07.InactiveDate <= _order.OrderDate THEN 'ICD10.07.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_08, '') = ''          THEN null
           WHEN icd10_08.Code IS NULL                     THEN 'ICD10.08.Unknown'
           WHEN icd10_08.InactiveDate <= _order.OrderDate THEN 'ICD10.08.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_09, '') = ''          THEN null
           WHEN icd10_09.Code IS NULL                     THEN 'ICD10.09.Unknown'
           WHEN icd10_09.InactiveDate <= _order.OrderDate THEN 'ICD10.09.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_10, '') = ''          THEN null
           WHEN icd10_10.Code IS NULL                     THEN 'ICD10.10.Unknown'
           WHEN icd10_10.InactiveDate <= _order.OrderDate THEN 'ICD10.10.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_11, '') = ''          THEN null
           WHEN icd10_11.Code IS NULL                     THEN 'ICD10.11.Unknown'
           WHEN icd10_11.InactiveDate <= _order.OrderDate THEN 'ICD10.11.Inactive'
           ELSE null END
    , CASE WHEN COALESCE(_order.ICD10_12, '') = ''          THEN null
           WHEN icd10_12.Code IS NULL                     THEN 'ICD10.12.Unknown'
           WHEN icd10_12.InactiveDate <= _order.OrderDate THEN 'ICD10.12.Inactive'
           ELSE null END
    , null)
  WHERE IF(V_OrderID IS NOT NULL, _order.ID = V_OrderID, V_ActiveOnly != 1 or details.IsActive = 1)
    AND (customer.CommercialAccount = 0)
    AND (details.IsZeroAmount = 0)
    AND ('2015-10-01' <= details.DOSFrom)
    AND (details.DXPointer10 != '')
    AND ((details.BillIns1 = 1) OR (details.BillIns2 = 1) OR (details.BillIns3 = 1) OR (details.BillIns4 = 1)); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## move_serial_on_hand

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_SerialID    INT(11); --
  DECLARE cur_WarehouseID INT(11); --
  DECLARE cur_TranType    VARCHAR(50); --

  DECLARE cur CURSOR FOR
  SELECT
    st2.SerialID
  , st2.WarehouseId
  , stt2.Name as TranType
  FROM (SELECT *
        FROM tbl_serial_transaction
        WHERE ID IN (SELECT Max(ID) FROM tbl_serial_transaction GROUP BY SerialID)) AS st
  INNER JOIN tbl_serial_transaction_type as stt ON stt.ID   = st.TypeID
                                               AND stt.Name = 'Returned'
  INNER JOIN (SELECT *
              FROM tbl_serial_transaction
              WHERE ID IN (SELECT Max(ID) FROM tbl_serial_transaction WHERE WarehouseId IS NOT NULL GROUP BY SerialID)) AS st2
          ON st2.SerialID = st.SerialID
  INNER JOIN tbl_serial_transaction_type AS stt2 ON stt2.Name = 'In from Maintenance'
  WHERE (P_SerialID IS NULL OR st2.SerialID = P_SerialID); --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      cur_SerialID
    , cur_WarehouseID
    , cur_TranType; --

    IF (done = 0) THEN
      CALL serial_add_transaction(
        cur_TranType       -- P_TranType         VARCHAR(50)
      , NOW()              -- P_TranTime         DATETIME
      , cur_SerialID       -- P_SerialID         INT,
      , cur_WarehouseID    -- P_WarehouseID      INT,
      , null               -- P_VendorID         INT,
      , null               -- P_CustomerID       INT,
      , null               -- P_OrderID          INT,
      , null               -- P_OrderDetailsID   INT,
      , null               -- P_LotNumber        VARCHAR(50),
      , 1                  -- P_LastUpdateUserID INT
      ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_SerialID    INT(11); --
  DECLARE cur_WarehouseID INT(11); --
  DECLARE cur_TranType    VARCHAR(50); --

  DECLARE cur CURSOR FOR
  SELECT
    st2.SerialID
  , st2.WarehouseId
  , stt2.Name as TranType
  FROM (SELECT *
        FROM tbl_serial_transaction
        WHERE ID IN (SELECT Max(ID) FROM tbl_serial_transaction GROUP BY SerialID)) AS st
  INNER JOIN tbl_serial_transaction_type as stt ON stt.ID   = st.TypeID
                                               AND stt.Name = 'Returned'
  INNER JOIN (SELECT *
              FROM tbl_serial_transaction
              WHERE ID IN (SELECT Max(ID) FROM tbl_serial_transaction WHERE WarehouseId IS NOT NULL GROUP BY SerialID)) AS st2
          ON st2.SerialID = st.SerialID
  INNER JOIN tbl_serial_transaction_type AS stt2 ON stt2.Name = 'In from Maintenance'
  WHERE (P_SerialID IS NULL OR st2.SerialID = P_SerialID); --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      cur_SerialID
    , cur_WarehouseID
    , cur_TranType; --

    IF (done = 0) THEN
      CALL serial_add_transaction(
        cur_TranType       -- P_TranType         VARCHAR(50)
      , CURRENT_TIMESTAMP              -- P_TranTime         DATETIME
      , cur_SerialID       -- P_SerialID         INT,
      , cur_WarehouseID    -- P_WarehouseID      INT,
      , null               -- P_VendorID         INT,
      , null               -- P_CustomerID       INT,
      , null               -- P_OrderID          INT,
      , null               -- P_OrderDetailsID   INT,
      , null               -- P_LotNumber        VARCHAR(50),
      , 1                  -- P_LastUpdateUserID INT
      ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Order_ConvertDepositsIntoPayments

### Original MySQL Procedure
```sql
BEGIN
  -- for given OrderId we select all order lines with deposits that have invoice lines without "deposit" payments
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE V_Amount, V_Billable DECIMAL(18, 2); --
  DECLARE V_Date DATE; --
  DECLARE V_PaymentMethod VARCHAR(20); --
  DECLARE V_Template, V_Extra TEXT; --
  DECLARE V_Element VARCHAR(100); --
  DECLARE V_Result VARCHAR(50); --
  DECLARE done INT DEFAULT 0; --
  DECLARE cur CURSOR FOR
    SELECT il.ID, dd.Amount, d.Date, d.PaymentMethod, il.BillableAmount
    FROM tbl_order AS o
         INNER JOIN tbl_orderdetails AS od ON od.CustomerID = o.CustomerID
                                          AND od.OrderID    = o.ID
         INNER JOIN tbl_deposits AS d ON d.CustomerID = od.CustomerID
                                     AND d.OrderID    = od.OrderID
         INNER JOIN tbl_depositdetails AS dd ON dd.CustomerID     = od.CustomerID
                                            AND dd.OrderID        = od.OrderID
                                            AND dd.OrderDetailsID = od.ID
         INNER JOIN tbl_invoice AS i ON i.CustomerID = o.CustomerID
                                    AND i.OrderID    = o.ID
         INNER JOIN tbl_invoicedetails AS il ON il.CustomerID     = i.CustomerID
                                            AND il.InvoiceID      = i.ID
                                            AND il.BillingMonth   = 1 -- only first billing month
                                            AND il.OrderID        = od.OrderID
                                            AND il.OrderDetailsID = od.ID
         INNER JOIN tbl_invoice_transactiontype as tt ON tt.Name = 'Payment'
         LEFT JOIN tbl_invoice_transaction as p ON p.CustomerID       = il.CustomerID
                                               AND p.InvoiceID        = il.InvoiceID
                                               AND p.InvoiceDetailsID = il.ID
                                               AND p.InsuranceCompanyID IS NULL
                                               AND p.CustomerInsuranceID IS NULL
                                               AND p.TransactionTypeID = tt.ID
                                               AND p.TransactionDate   = d.Date
                                               AND p.Amount            = dd.Amount
    WHERE (o.ID = P_OrderID)
      AND (p.ID is null); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_Template = '<values>
  <v n="Billable">0.00</v>
  <v n="CheckDate">00/00/0000</v>
  <v n="Paid">0.00</v>
  <v n="PaymentMethod">Check</v>
</values>'; --

  OPEN cur; --

  DEPOSITS_LOOP: LOOP
    FETCH cur INTO V_InvoiceDetailsID, V_Amount, V_Date, V_PaymentMethod, V_Billable; --

    IF done THEN
      LEAVE DEPOSITS_LOOP; --
    END IF; --

    SET V_Extra = V_Template; --
    SET V_Element = CONCAT('<v n="Billable">', IFNULL(CAST(V_Billable as CHAR), ''), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="Billable"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --
    SET V_Element = CONCAT('<v n="CheckDate">', IFNULL(DATE_FORMAT(V_Date, '%m/%d/%Y'), ''), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="CheckDate"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --
    SET V_Element = CONCAT('<v n="Paid">', IFNULL(CAST(V_Amount as CHAR), ''), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="Paid"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --
    SET V_Element = CONCAT('<v n="PaymentMethod">', IFNULL(CAST(V_PaymentMethod as CHAR), 'Check'), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="PaymentMethod"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --

    CALL `InvoiceDetails_AddPayment`
    ( V_InvoiceDetailsID
    , NULL -- P_InsuranceCompanyID
    , V_Date
    , V_Extra
    , 'Deposit' -- P_Comments
    , '' -- P_Options
    , IFNULL(@UserId, 1)
    , V_Result); --
  END LOOP DEPOSITS_LOOP; --

  CLOSE cur; --

  CALL `Order_InternalUpdateBalance`(P_OrderID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  -- for given OrderId we select all order lines with deposits that have invoice lines without "deposit" payments
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE V_Amount, V_Billable DECIMAL(18, 2); --
  DECLARE V_Date DATE; --
  DECLARE V_PaymentMethod VARCHAR(20); --
  DECLARE V_Template, V_Extra TEXT; --
  DECLARE V_Element VARCHAR(100); --
  DECLARE V_Result VARCHAR(50); --
  DECLARE done INT DEFAULT 0; --
  DECLARE cur CURSOR FOR
    SELECT il.ID, dd.Amount, d.Date, d.PaymentMethod, il.BillableAmount
    FROM tbl_order AS o
         INNER JOIN tbl_orderdetails AS od ON od.CustomerID = o.CustomerID
                                          AND od.OrderID    = o.ID
         INNER JOIN tbl_deposits AS d ON d.CustomerID = od.CustomerID
                                     AND d.OrderID    = od.OrderID
         INNER JOIN tbl_depositdetails AS dd ON dd.CustomerID     = od.CustomerID
                                            AND dd.OrderID        = od.OrderID
                                            AND dd.OrderDetailsID = od.ID
         INNER JOIN tbl_invoice AS i ON i.CustomerID = o.CustomerID
                                    AND i.OrderID    = o.ID
         INNER JOIN tbl_invoicedetails AS il ON il.CustomerID     = i.CustomerID
                                            AND il.InvoiceID      = i.ID
                                            AND il.BillingMonth   = 1 -- only first billing month
                                            AND il.OrderID        = od.OrderID
                                            AND il.OrderDetailsID = od.ID
         INNER JOIN tbl_invoice_transactiontype as tt ON tt.Name = 'Payment'
         LEFT JOIN tbl_invoice_transaction as p ON p.CustomerID       = il.CustomerID
                                               AND p.InvoiceID        = il.InvoiceID
                                               AND p.InvoiceDetailsID = il.ID
                                               AND p.InsuranceCompanyID IS NULL
                                               AND p.CustomerInsuranceID IS NULL
                                               AND p.TransactionTypeID = tt.ID
                                               AND p.TransactionDate   = d.Date
                                               AND p.Amount            = dd.Amount
    WHERE (o.ID = P_OrderID)
      AND (p.ID is null); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SET V_Template = '<values>
  <v n="Billable">0.00</v>
  <v n="CheckDate">00/00/0000</v>
  <v n="Paid">0.00</v>
  <v n="PaymentMethod">Check</v>
</values>'; --

  OPEN cur; --

  DEPOSITS_LOOP: LOOP
    FETCH cur INTO V_InvoiceDetailsID, V_Amount, V_Date, V_PaymentMethod, V_Billable; --

    IF done THEN
      LEAVE DEPOSITS_LOOP; --
    END IF; --

    SET V_Extra = V_Template; --
    SET V_Element = CONCAT('<v n="Billable">', COALESCE(CAST(V_Billable as CHAR), ''), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="Billable"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --
    SET V_Element = CONCAT('<v n="CheckDate">', COALESCE(TO_CHAR(V_Date, '%m/%d/%Y'), ''), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="CheckDate"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --
    SET V_Element = CONCAT('<v n="Paid">', COALESCE(CAST(V_Amount as CHAR), ''), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="Paid"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --
    SET V_Element = CONCAT('<v n="PaymentMethod">', COALESCE(CAST(V_PaymentMethod as CHAR), 'Check'), '</v>'); --
    SET V_Extra = UpdateXML(V_Extra, 'values/v[@n="PaymentMethod"]' COLLATE latin1_general_ci, V_Element COLLATE latin1_general_ci); --

    CALL `InvoiceDetails_AddPayment`
    ( V_InvoiceDetailsID
    , NULL -- P_InsuranceCompanyID
    , V_Date
    , V_Extra
    , 'Deposit' -- P_Comments
    , '' -- P_Options
    , COALESCE(@UserId, 1)
    , V_Result); --
  END LOOP DEPOSITS_LOOP; --

  CLOSE cur; --

  CALL `Order_InternalUpdateBalance`(P_OrderID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: MODIFIES SQL DATA

## Order_InternalProcess

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_DetailsCount, V_ICD10Count INT; --

  SET P_InvoiceID = NULL; --

  SELECT COUNT(*), SUM(CASE WHEN '2015-10-01' <= details.DosFrom THEN 1 ELSE 0 END)
  INTO V_DetailsCount, V_ICD10Count
  FROM tbl_order
       INNER JOIN view_orderdetails_core as details ON tbl_order.ID = details.OrderID
                                                   AND tbl_order.CustomerID = details.CustomerID
       INNER JOIN tbl_pricecode_item as pricecode ON details.PriceCodeID = pricecode.PriceCodeID
                                                 AND details.InventoryItemID = pricecode.InventoryItemID
  WHERE (details.OrderID = P_OrderID)
    AND (details.IsActive = 1)
    -- we should generate invoices before end date and should not generate invoices after end date
    AND ((details.EndDate IS NULL) OR (details.DosFrom <= details.EndDate))
    AND (IF(details.BillingMonth <= 0, 1, details.BillingMonth) = P_BillingMonth)
    AND ((IF((tbl_order.CustomerInsurance1_ID IS NOT NULL) AND (details.BillIns1 = 1), 1, 0) +
          IF((tbl_order.CustomerInsurance2_ID IS NOT NULL) AND (details.BillIns2 = 1), 2, 0) +
          IF((tbl_order.CustomerInsurance3_ID IS NOT NULL) AND (details.BillIns3 = 1), 4, 0) +
          IF((tbl_order.CustomerInsurance4_ID IS NOT NULL) AND (details.BillIns4 = 1), 8, 0) +
          IF((details.EndDate IS NOT NULL), 32, 0) +
          IF((details.AcceptAssignment = 1), 16, 0)) = P_BillingFlags)
    AND (IFNULL(details.MIR, '') = '')
    AND (OrderMustBeSkipped  (tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
    AND (InvoiceMustBeSkipped(tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
    -- check for zero amount was moved out of function InvoiceMustBeSkipped
    AND (details.IsZeroAmount = 0); --

  IF 0 < V_DetailsCount THEN
    -- create invoice
    INSERT INTO tbl_invoice
    ( CustomerID
    , OrderID
    , Approved
    , AcceptAssignment
    , ClaimNote
    , InvoiceDate
    , DoctorID
    , POSTypeID
    , FacilityID
    , ReferralID
    , SalesrepID
    , CustomerInsurance1_ID
    , CustomerInsurance2_ID
    , CustomerInsurance3_ID
    , CustomerInsurance4_ID
    , ICD9_1
    , ICD9_2
    , ICD9_3
    , ICD9_4
    , ICD10_01
    , ICD10_02
    , ICD10_03
    , ICD10_04
    , ICD10_05
    , ICD10_06
    , ICD10_07
    , ICD10_08
    , ICD10_09
    , ICD10_10
    , ICD10_11
    , ICD10_12
    , TaxRateID
    , TaxRatePercent
    , Discount
    , LastUpdateUserID)
    SELECT
      tbl_order.CustomerID
    , tbl_order.ID
    , tbl_order.Approved
    , IF(P_BillingFlags & 16 = 16, 1, 0) as AcceptAssignment
    , ClaimNote
    , P_InvoiceDate as InvoiceDate
    , tbl_order.DoctorID
    , tbl_order.POSTypeID
    , tbl_order.FacilityID
    , tbl_order.ReferralID
    , tbl_order.SalesrepID
    , tbl_order.CustomerInsurance1_ID
    , tbl_order.CustomerInsurance2_ID
    , tbl_order.CustomerInsurance3_ID
    , tbl_order.CustomerInsurance4_ID
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_1) as ICD9_1
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_2) as ICD9_2
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_3) as ICD9_3
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_4) as ICD9_4
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_01) as ICD10_01
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_02) as ICD10_02
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_03) as ICD10_03
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_04) as ICD10_04
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_05) as ICD10_05
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_06) as ICD10_06
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_07) as ICD10_07
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_08) as ICD10_08
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_09) as ICD10_09
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_10) as ICD10_10
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_11) as ICD10_11
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_12) as ICD10_12
    , view_taxrate.ID
    , view_taxrate.TotalTax
    , tbl_order.Discount
    , IFNULL(@UserId, 1) as LastUpdateUserID
    FROM tbl_order
         LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_company ON tbl_company.ID = 1
         LEFT JOIN view_taxrate ON IFNULL(tbl_customer.TaxRateID, tbl_company.TaxRateID) = view_taxrate.ID
    WHERE (tbl_order.ID = P_OrderID); --

    SELECT LAST_INSERT_ID() INTO P_InvoiceID; --

    -- add line items to invoice
    INSERT INTO tbl_invoicedetails
    ( CustomerID
    , InvoiceID
    , InventoryItemID
    , PriceCodeID
    , OrderID
    , OrderDetailsID
    , Balance
    , BillableAmount
    , AllowableAmount
    , Taxes
    , Quantity
    , Hardship
    , AcceptAssignment
    , InvoiceDate
    , DOSFrom
    , DOSTo
    , ShowSpanDates
    , BillingCode
    , Modifier1
    , Modifier2
    , Modifier3
    , Modifier4
    , DXPointer
    , DXPointer10
    , DrugNoteField
    , DrugControlNumber
    , BillingMonth
    , SendCMN_RX_w_invoice
    , SpecialCode
    , ReviewCode
    , MedicallyUnnecessary
    , BillIns1
    , BillIns2
    , BillIns3
    , BillIns4
    , NopayIns1
    , CMNFormID
    , AuthorizationTypeID
    , AuthorizationNumber
    , HaoDescription)
    SELECT
      details.CustomerID
    , P_InvoiceID
    , details.InventoryItemID
    , details.PriceCodeID
    , details.OrderID
    , details.ID
    , (1 - IFNULL(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      IF((details.Taxable = 1) AND (view_taxrate.ID IS NOT NULL)
        ,GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate) * (1 + IFNULL(view_taxrate.TotalTax, 0) / 100)
        ,GetBillableAmount (details.ActualSaleRentType, details.BillingMonth, details.BillablePrice , details.BilledQuantity, pricecode.Sale_BillablePrice, details.FlatRate))
      as Balance
    , (1 - IFNULL(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      IF((details.Taxable = 1) AND (view_taxrate.ID IS NOT NULL)
        ,GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate) * (1 + IFNULL(view_taxrate.TotalTax, 0) / 100)
        ,GetBillableAmount (details.ActualSaleRentType, details.BillingMonth, details.BillablePrice , details.BilledQuantity, pricecode.Sale_BillablePrice, details.FlatRate))
      as BillableAmount
    , (1 - IFNULL(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate)
      as AllowableAmount
    , (1 - IFNULL(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      IF((details.Taxable = 1) AND (view_taxrate.ID IS NOT NULL)
        ,GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate) * (0 + IFNULL(view_taxrate.TotalTax, 0) / 100)
        ,0.00) as Taxes
    , details.BilledQuantity *
      GetQuantityMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen)
      as Quantity
    , IFNULL(tbl_customer.Hardship, 0)
    , IFNULL(details.AcceptAssignment, 0) as AcceptAssignment
    , P_InvoiceDate
    , details.DOSFrom
    , details.ActualDosTo as DOSTo
    , details.ShowSpanDates
    , details.BillingCode
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 1, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 2, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 3, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 4, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , details.DXPointer
    , details.DXPointer10
    , details.DrugNoteField
    , details.DrugControlNumber
    , IF(details.BillingMonth <= 0, 1, details.BillingMonth)
    , IF(details.BillingMonth <= 1, details.SendCMN_RX_w_invoice, 0)
    , details.SpecialCode
    , details.ReviewCode
    , details.MedicallyUnnecessary
    , details.BillIns1
    , details.BillIns2
    , details.BillIns3
    , details.BillIns4
    , details.NopayIns1
    , details.CMNFormID
    , details.AuthorizationTypeID
    , details.AuthorizationNumber
    , details.HaoDescription
    FROM tbl_order
         INNER JOIN view_orderdetails_core as details ON tbl_order.ID = details.OrderID
                                                     AND tbl_order.CustomerID = details.CustomerID
         INNER JOIN tbl_pricecode_item as pricecode ON details.PriceCodeID = pricecode.PriceCodeID
                                                   AND details.InventoryItemID = pricecode.InventoryItemID
         LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_company ON tbl_company.ID = 1
         LEFT JOIN view_taxrate ON IFNULL(tbl_customer.TaxRateID, tbl_company.TaxRateID) = view_taxrate.ID
    WHERE (details.OrderID = P_OrderID)
      AND (details.IsActive = 1)
      -- we should generate invoices before end date and should not generate invoices after end date
      AND ((details.EndDate IS NULL) OR (details.DosFrom <= details.EndDate))
      AND (IF(details.BillingMonth <= 0, 1, details.BillingMonth) = P_BillingMonth)
      AND ((IF((tbl_order.CustomerInsurance1_ID IS NOT NULL) AND (details.BillIns1 = 1), 1, 0) +
            IF((tbl_order.CustomerInsurance2_ID IS NOT NULL) AND (details.BillIns2 = 1), 2, 0) +
            IF((tbl_order.CustomerInsurance3_ID IS NOT NULL) AND (details.BillIns3 = 1), 4, 0) +
            IF((tbl_order.CustomerInsurance4_ID IS NOT NULL) AND (details.BillIns4 = 1), 8, 0) +
            IF((details.EndDate IS NOT NULL), 32, 0) +
            IF((details.AcceptAssignment = 1), 16, 0)) = P_BillingFlags)
      AND (IFNULL(details.MIR, '') = '')
      AND (OrderMustBeSkipped  (tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
      AND (InvoiceMustBeSkipped(tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
      -- check for zero amount was moved out of function InvoiceMustBeSkipped
      AND (details.IsZeroAmount = 0); --
  END IF; --

  -- update order line items
  UPDATE tbl_order
         INNER JOIN view_orderdetails_core as details ON tbl_order.ID = details.OrderID
                                                     AND tbl_order.CustomerID = details.CustomerID
  SET
    tbl_order.BillDate = details.DOSFrom
  , details.DOSTo   = GetNextDosTo  (details.DOSFrom, details.DOSTo, details.ActualBilledWhen)
  , details.DOSFrom = GetNextDosFrom(details.DOSFrom, details.DOSTo, details.ActualBilledWhen)
  , details.Modifier1 = GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 1, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
  , details.Modifier2 = GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 2, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
  , details.State   = CASE WHEN (details.EndDate IS NOT NULL) AND (details.EndDate < details.InvoiceDate)
                           THEN 'Closed'
                           WHEN OrderMustBeClosed(tbl_order.DeliveryDate, details.DOSFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 1
                           THEN 'Closed'
                           ELSE details.State END
  , details.EndDate = CASE WHEN details.EndDate IS NOT NULL
                           THEN details.EndDate
                           WHEN OrderMustBeClosed(tbl_order.DeliveryDate, details.DOSFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 1
                           THEN P_InvoiceDate
                           ELSE details.EndDate END
  , details.BillingMonth = IF(details.BillingMonth <= 0, 1, details.BillingMonth) + 1
  WHERE (details.OrderID = P_OrderID)
    AND (details.IsActive = 1)
    AND (IF(details.BillingMonth <= 0, 1, details.BillingMonth) = P_BillingMonth)
    AND ((IF((tbl_order.CustomerInsurance1_ID IS NOT NULL) AND (details.BillIns1 = 1), 1, 0) +
          IF((tbl_order.CustomerInsurance2_ID IS NOT NULL) AND (details.BillIns2 = 1), 2, 0) +
          IF((tbl_order.CustomerInsurance3_ID IS NOT NULL) AND (details.BillIns3 = 1), 4, 0) +
          IF((tbl_order.CustomerInsurance4_ID IS NOT NULL) AND (details.BillIns4 = 1), 8, 0) +
          IF((details.EndDate IS NOT NULL), 32, 0) +
          IF((details.AcceptAssignment = 1), 16, 0)) = P_BillingFlags)
    AND (IFNULL(details.MIR, '') = '')
    AND (OrderMustBeSkipped(tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0); --

  IF P_BillingMonth = 1 THEN
    CALL Order_ConvertDepositsIntoPayments(P_OrderID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_DetailsCount, V_ICD10Count INT; --

  SET P_InvoiceID = NULL; --

  SELECT COUNT(*), SUM(CASE WHEN '2015-10-01' <= details.DosFrom THEN 1 ELSE 0 END)
  INTO V_DetailsCount, V_ICD10Count
  FROM tbl_order
       INNER JOIN view_orderdetails_core as details ON tbl_order.ID = details.OrderID
                                                   AND tbl_order.CustomerID = details.CustomerID
       INNER JOIN tbl_pricecode_item as pricecode ON details.PriceCodeID = pricecode.PriceCodeID
                                                 AND details.InventoryItemID = pricecode.InventoryItemID
  WHERE (details.OrderID = P_OrderID)
    AND (details.IsActive = 1)
    -- we should generate invoices before end date and should not generate invoices after end date
    AND ((details.EndDate IS NULL) OR (details.DosFrom <= details.EndDate))
    AND (IF(details.BillingMonth <= 0, 1, details.BillingMonth) = P_BillingMonth)
    AND ((IF((tbl_order.CustomerInsurance1_ID IS NOT NULL) AND (details.BillIns1 = 1), 1, 0) +
          IF((tbl_order.CustomerInsurance2_ID IS NOT NULL) AND (details.BillIns2 = 1), 2, 0) +
          IF((tbl_order.CustomerInsurance3_ID IS NOT NULL) AND (details.BillIns3 = 1), 4, 0) +
          IF((tbl_order.CustomerInsurance4_ID IS NOT NULL) AND (details.BillIns4 = 1), 8, 0) +
          IF((details.EndDate IS NOT NULL), 32, 0) +
          IF((details.AcceptAssignment = 1), 16, 0)) = P_BillingFlags)
    AND (COALESCE(details.MIR, '') = '')
    AND (OrderMustBeSkipped  (tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
    AND (InvoiceMustBeSkipped(tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
    -- check for zero amount was moved out of function InvoiceMustBeSkipped
    AND (details.IsZeroAmount = 0); --

  IF 0 < V_DetailsCount THEN
    -- create invoice
    INSERT INTO tbl_invoice
    ( CustomerID
    , OrderID
    , Approved
    , AcceptAssignment
    , ClaimNote
    , InvoiceDate
    , DoctorID
    , POSTypeID
    , FacilityID
    , ReferralID
    , SalesrepID
    , CustomerInsurance1_ID
    , CustomerInsurance2_ID
    , CustomerInsurance3_ID
    , CustomerInsurance4_ID
    , ICD9_1
    , ICD9_2
    , ICD9_3
    , ICD9_4
    , ICD10_01
    , ICD10_02
    , ICD10_03
    , ICD10_04
    , ICD10_05
    , ICD10_06
    , ICD10_07
    , ICD10_08
    , ICD10_09
    , ICD10_10
    , ICD10_11
    , ICD10_12
    , TaxRateID
    , TaxRatePercent
    , Discount
    , LastUpdateUserID)
    SELECT
      tbl_order.CustomerID
    , tbl_order.ID
    , tbl_order.Approved
    , IF(P_BillingFlags & 16 = 16, 1, 0) as AcceptAssignment
    , ClaimNote
    , P_InvoiceDate as InvoiceDate
    , tbl_order.DoctorID
    , tbl_order.POSTypeID
    , tbl_order.FacilityID
    , tbl_order.ReferralID
    , tbl_order.SalesrepID
    , tbl_order.CustomerInsurance1_ID
    , tbl_order.CustomerInsurance2_ID
    , tbl_order.CustomerInsurance3_ID
    , tbl_order.CustomerInsurance4_ID
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_1) as ICD9_1
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_2) as ICD9_2
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_3) as ICD9_3
    , IF(V_ICD10Count = V_DetailsCount, '', tbl_order.ICD9_4) as ICD9_4
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_01) as ICD10_01
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_02) as ICD10_02
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_03) as ICD10_03
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_04) as ICD10_04
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_05) as ICD10_05
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_06) as ICD10_06
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_07) as ICD10_07
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_08) as ICD10_08
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_09) as ICD10_09
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_10) as ICD10_10
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_11) as ICD10_11
    , IF(V_ICD10Count = 0, '', tbl_order.ICD10_12) as ICD10_12
    , view_taxrate.ID
    , view_taxrate.TotalTax
    , tbl_order.Discount
    , COALESCE(@UserId, 1) as LastUpdateUserID
    FROM tbl_order
         LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_company ON tbl_company.ID = 1
         LEFT JOIN view_taxrate ON COALESCE(tbl_customer.TaxRateID, tbl_company.TaxRateID) = view_taxrate.ID
    WHERE (tbl_order.ID = P_OrderID); --

    SELECT lastval() INTO P_InvoiceID; --

    -- add line items to invoice
    INSERT INTO tbl_invoicedetails
    ( CustomerID
    , InvoiceID
    , InventoryItemID
    , PriceCodeID
    , OrderID
    , OrderDetailsID
    , Balance
    , BillableAmount
    , AllowableAmount
    , Taxes
    , Quantity
    , Hardship
    , AcceptAssignment
    , InvoiceDate
    , DOSFrom
    , DOSTo
    , ShowSpanDates
    , BillingCode
    , Modifier1
    , Modifier2
    , Modifier3
    , Modifier4
    , DXPointer
    , DXPointer10
    , DrugNoteField
    , DrugControlNumber
    , BillingMonth
    , SendCMN_RX_w_invoice
    , SpecialCode
    , ReviewCode
    , MedicallyUnnecessary
    , BillIns1
    , BillIns2
    , BillIns3
    , BillIns4
    , NopayIns1
    , CMNFormID
    , AuthorizationTypeID
    , AuthorizationNumber
    , HaoDescription)
    SELECT
      details.CustomerID
    , P_InvoiceID
    , details.InventoryItemID
    , details.PriceCodeID
    , details.OrderID
    , details.ID
    , (1 - COALESCE(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      IF((details.Taxable = 1) AND (view_taxrate.ID IS NOT NULL)
        ,GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate) * (1 + COALESCE(view_taxrate.TotalTax, 0) / 100)
        ,GetBillableAmount (details.ActualSaleRentType, details.BillingMonth, details.BillablePrice , details.BilledQuantity, pricecode.Sale_BillablePrice, details.FlatRate))
      as Balance
    , (1 - COALESCE(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      IF((details.Taxable = 1) AND (view_taxrate.ID IS NOT NULL)
        ,GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate) * (1 + COALESCE(view_taxrate.TotalTax, 0) / 100)
        ,GetBillableAmount (details.ActualSaleRentType, details.BillingMonth, details.BillablePrice , details.BilledQuantity, pricecode.Sale_BillablePrice, details.FlatRate))
      as BillableAmount
    , (1 - COALESCE(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate)
      as AllowableAmount
    , (1 - COALESCE(tbl_order.Discount, 0) / 100) *
      GetAmountMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen) *
      IF((details.Taxable = 1) AND (view_taxrate.ID IS NOT NULL)
        ,GetAllowableAmount(details.ActualSaleRentType, details.BillingMonth, details.AllowablePrice, details.BilledQuantity, pricecode.Sale_AllowablePrice, details.FlatRate) * (0 + COALESCE(view_taxrate.TotalTax, 0) / 100)
        ,0.00) as Taxes
    , details.BilledQuantity *
      GetQuantityMultiplier(details.DOSFrom, details.DOSTo, details.EndDate, details.ActualSaleRentType, details.ActualOrderedWhen, details.ActualBilledWhen)
      as Quantity
    , COALESCE(tbl_customer.Hardship, 0)
    , COALESCE(details.AcceptAssignment, 0) as AcceptAssignment
    , P_InvoiceDate
    , details.DOSFrom
    , details.ActualDosTo as DOSTo
    , details.ShowSpanDates
    , details.BillingCode
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 1, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 2, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 3, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 4, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
    , details.DXPointer
    , details.DXPointer10
    , details.DrugNoteField
    , details.DrugControlNumber
    , IF(details.BillingMonth <= 0, 1, details.BillingMonth)
    , IF(details.BillingMonth <= 1, details.SendCMN_RX_w_invoice, 0)
    , details.SpecialCode
    , details.ReviewCode
    , details.MedicallyUnnecessary
    , details.BillIns1
    , details.BillIns2
    , details.BillIns3
    , details.BillIns4
    , details.NopayIns1
    , details.CMNFormID
    , details.AuthorizationTypeID
    , details.AuthorizationNumber
    , details.HaoDescription
    FROM tbl_order
         INNER JOIN view_orderdetails_core as details ON tbl_order.ID = details.OrderID
                                                     AND tbl_order.CustomerID = details.CustomerID
         INNER JOIN tbl_pricecode_item as pricecode ON details.PriceCodeID = pricecode.PriceCodeID
                                                   AND details.InventoryItemID = pricecode.InventoryItemID
         LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID
         LEFT JOIN tbl_company ON tbl_company.ID = 1
         LEFT JOIN view_taxrate ON COALESCE(tbl_customer.TaxRateID, tbl_company.TaxRateID) = view_taxrate.ID
    WHERE (details.OrderID = P_OrderID)
      AND (details.IsActive = 1)
      -- we should generate invoices before end date and should not generate invoices after end date
      AND ((details.EndDate IS NULL) OR (details.DosFrom <= details.EndDate))
      AND (IF(details.BillingMonth <= 0, 1, details.BillingMonth) = P_BillingMonth)
      AND ((IF((tbl_order.CustomerInsurance1_ID IS NOT NULL) AND (details.BillIns1 = 1), 1, 0) +
            IF((tbl_order.CustomerInsurance2_ID IS NOT NULL) AND (details.BillIns2 = 1), 2, 0) +
            IF((tbl_order.CustomerInsurance3_ID IS NOT NULL) AND (details.BillIns3 = 1), 4, 0) +
            IF((tbl_order.CustomerInsurance4_ID IS NOT NULL) AND (details.BillIns4 = 1), 8, 0) +
            IF((details.EndDate IS NOT NULL), 32, 0) +
            IF((details.AcceptAssignment = 1), 16, 0)) = P_BillingFlags)
      AND (COALESCE(details.MIR, '') = '')
      AND (OrderMustBeSkipped  (tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
      AND (InvoiceMustBeSkipped(tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0)
      -- check for zero amount was moved out of function InvoiceMustBeSkipped
      AND (details.IsZeroAmount = 0); --
  END IF; --

  -- update order line items
  UPDATE tbl_order
         INNER JOIN view_orderdetails_core as details ON tbl_order.ID = details.OrderID
                                                     AND tbl_order.CustomerID = details.CustomerID
  SET
    tbl_order.BillDate = details.DOSFrom
  , details.DOSTo   = GetNextDosTo  (details.DOSFrom, details.DOSTo, details.ActualBilledWhen)
  , details.DOSFrom = GetNextDosFrom(details.DOSFrom, details.DOSTo, details.ActualBilledWhen)
  , details.Modifier1 = GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 1, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
  , details.Modifier2 = GetInvoiceModifier(tbl_order.DeliveryDate, details.ActualSaleRentType, details.BillingMonth, 2, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4)
  , details.State   = CASE WHEN (details.EndDate IS NOT NULL) AND (details.EndDate < details.InvoiceDate)
                           THEN 'Closed'
                           WHEN OrderMustBeClosed(tbl_order.DeliveryDate, details.DOSFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 1
                           THEN 'Closed'
                           ELSE details.State END
  , details.EndDate = CASE WHEN details.EndDate IS NOT NULL
                           THEN details.EndDate
                           WHEN OrderMustBeClosed(tbl_order.DeliveryDate, details.DOSFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 1
                           THEN P_InvoiceDate
                           ELSE details.EndDate END
  , details.BillingMonth = IF(details.BillingMonth <= 0, 1, details.BillingMonth) + 1
  WHERE (details.OrderID = P_OrderID)
    AND (details.IsActive = 1)
    AND (IF(details.BillingMonth <= 0, 1, details.BillingMonth) = P_BillingMonth)
    AND ((IF((tbl_order.CustomerInsurance1_ID IS NOT NULL) AND (details.BillIns1 = 1), 1, 0) +
          IF((tbl_order.CustomerInsurance2_ID IS NOT NULL) AND (details.BillIns2 = 1), 2, 0) +
          IF((tbl_order.CustomerInsurance3_ID IS NOT NULL) AND (details.BillIns3 = 1), 4, 0) +
          IF((tbl_order.CustomerInsurance4_ID IS NOT NULL) AND (details.BillIns4 = 1), 8, 0) +
          IF((details.EndDate IS NOT NULL), 32, 0) +
          IF((details.AcceptAssignment = 1), 16, 0)) = P_BillingFlags)
    AND (COALESCE(details.MIR, '') = '')
    AND (OrderMustBeSkipped(tbl_order.DeliveryDate, details.DosFrom, details.ActualSaleRentType, details.BillingMonth, details.Modifier1, details.Modifier2, details.Modifier3, details.Modifier4) = 0); --

  IF P_BillingMonth = 1 THEN
    CALL Order_ConvertDepositsIntoPayments(P_OrderID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## Order_InternalUpdateBalance

### Original MySQL Procedure
```sql
BEGIN
  UPDATE tbl_invoice as i
  INNER JOIN tbl_order as o ON i.CustomerID = o.CustomerID
                           AND i.OrderID    = o.ID
  LEFT JOIN (SELECT tbl_invoicedetails.InvoiceID, Sum(tbl_invoicedetails.Balance) as Balance
             FROM tbl_order
                  INNER JOIN tbl_invoice ON tbl_invoice.CustomerID = tbl_order.CustomerID
                                        AND tbl_invoice.OrderID    = tbl_order.ID
                  INNER JOIN tbl_invoicedetails ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                               AND tbl_invoicedetails.InvoiceID  = tbl_invoice.ID
             WHERE (tbl_order.ID = P_OrderID)
             GROUP BY tbl_invoicedetails.InvoiceID) as b
         ON b.InvoiceID = i.ID
  SET i.InvoiceBalance = IFNULL(b.Balance, 0)
  WHERE (o.ID = P_OrderID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  UPDATE tbl_invoice as i
  INNER JOIN tbl_order as o ON i.CustomerID = o.CustomerID
                           AND i.OrderID    = o.ID
  LEFT JOIN (SELECT tbl_invoicedetails.InvoiceID, Sum(tbl_invoicedetails.Balance) as Balance
             FROM tbl_order
                  INNER JOIN tbl_invoice ON tbl_invoice.CustomerID = tbl_order.CustomerID
                                        AND tbl_invoice.OrderID    = tbl_order.ID
                  INNER JOIN tbl_invoicedetails ON tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID
                                               AND tbl_invoicedetails.InvoiceID  = tbl_invoice.ID
             WHERE (tbl_order.ID = P_OrderID)
             GROUP BY tbl_invoicedetails.InvoiceID) as b
         ON b.InvoiceID = i.ID
  SET i.InvoiceBalance = COALESCE(b.Balance, 0)
  WHERE (o.ID = P_OrderID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## order_process

### Original MySQL Procedure
```sql
BEGIN
  CALL `Order_InternalProcess`(P_OrderID, P_BillingMonth, P_BillingFlags, P_InvoiceDate, P_InvoiceID); --
  IF (P_InvoiceID IS NOT NULL) THEN
    CALL `InvoiceDetails_RecalculateInternals_Single`(P_InvoiceID, null); --
    CALL `Invoice_InternalUpdatePendingSubmissions`  (P_InvoiceID); --
    CALL `InvoiceDetails_RecalculateInternals_Single`(P_InvoiceID, null); --
    CALL `Invoice_InternalUpdateBalance`             (P_InvoiceID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  CALL `Order_InternalProcess`(P_OrderID, P_BillingMonth, P_BillingFlags, P_InvoiceDate, P_InvoiceID); --
  IF (P_InvoiceID IS NOT NULL) THEN
    CALL `InvoiceDetails_RecalculateInternals_Single`(P_InvoiceID, null); --
    CALL `Invoice_InternalUpdatePendingSubmissions`  (P_InvoiceID); --
    CALL `InvoiceDetails_RecalculateInternals_Single`(P_InvoiceID, null); --
    CALL `Invoice_InternalUpdateBalance`             (P_InvoiceID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## order_process_2

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_InvoiceID INT; --
  SET V_InvoiceID = null; --

  CALL `Order_InternalProcess`(P_OrderID, P_BillingMonth, P_BillingFlags, P_InvoiceDate, V_InvoiceID); --
  IF (V_InvoiceID IS NOT NULL) THEN
    CALL `InvoiceDetails_RecalculateInternals_Single`(V_InvoiceID, null); --
    CALL `Invoice_InternalUpdatePendingSubmissions`  (V_InvoiceID); --
    CALL `InvoiceDetails_RecalculateInternals_Single`(V_InvoiceID, null); --
    CALL `Invoice_InternalUpdateBalance`             (V_InvoiceID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_InvoiceID INT; --
  SET V_InvoiceID = null; --

  CALL `Order_InternalProcess`(P_OrderID, P_BillingMonth, P_BillingFlags, P_InvoiceDate, V_InvoiceID); --
  IF (V_InvoiceID IS NOT NULL) THEN
    CALL `InvoiceDetails_RecalculateInternals_Single`(V_InvoiceID, null); --
    CALL `Invoice_InternalUpdatePendingSubmissions`  (V_InvoiceID); --
    CALL `InvoiceDetails_RecalculateInternals_Single`(V_InvoiceID, null); --
    CALL `Invoice_InternalUpdateBalance`             (V_InvoiceID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## order_update_dos

### Original MySQL Procedure
```sql
BEGIN
  IF P_DOSFrom IS NOT NULL THEN
    UPDATE view_orderdetails as details
           INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                               AND details.OrderID    = tbl_order.ID
    SET
      details.DosFrom = P_DOSFrom
    , details.DosTo   = GetNewDosTo(P_DOSFrom, details.DosFrom, details.DosTo, details.ActualBilledWhen)
    -- ordered quantity will not change
    , details.BilledQuantity = OrderedQty2BilledQty(P_DOSFrom, GetNewDosTo(P_DOSFrom, details.DosFrom, details.DosTo, details.ActualBilledWhen),
        details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
        details.OrderedConverter, details.DeliveryConverter, details.BilledConverter)
    , details.DeliveryQuantity = OrderedQty2DeliveryQty(P_DOSFrom, GetNewDosTo(P_DOSFrom, details.DosFrom, details.DosTo, details.ActualBilledWhen),
        details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
        details.OrderedConverter, details.DeliveryConverter, details.BilledConverter)
    WHERE (tbl_order.ID = P_OrderID)
      AND (tbl_order.Approved = 0); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  IF P_DOSFrom IS NOT NULL THEN
    UPDATE view_orderdetails as details
           INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                               AND details.OrderID    = tbl_order.ID
    SET
      details.DosFrom = P_DOSFrom
    , details.DosTo   = GetNewDosTo(P_DOSFrom, details.DosFrom, details.DosTo, details.ActualBilledWhen)
    -- ordered quantity will not change
    , details.BilledQuantity = OrderedQty2BilledQty(P_DOSFrom, GetNewDosTo(P_DOSFrom, details.DosFrom, details.DosTo, details.ActualBilledWhen),
        details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
        details.OrderedConverter, details.DeliveryConverter, details.BilledConverter)
    , details.DeliveryQuantity = OrderedQty2DeliveryQty(P_DOSFrom, GetNewDosTo(P_DOSFrom, details.DosFrom, details.DosTo, details.ActualBilledWhen),
        details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
        details.OrderedConverter, details.DeliveryConverter, details.BilledConverter)
    WHERE (tbl_order.ID = P_OrderID)
      AND (tbl_order.Approved = 0); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## process_reoccuring_order

### Original MySQL Procedure
```sql
BEGIN
  -- reoccuring sales support
  -- tbl_orderdetails.ReoccuringID - source line item
  DECLARE V_DetailsCount INT; --
  DECLARE V_NewOrderID INT; --
  DECLARE V_NewOrderDate DATETIME; --

  SELECT
     Count(*) as `Count`
    ,MAX(IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom)) as NewOrderDate
  INTO
     V_DetailsCount
    ,V_NewOrderDate
  FROM view_orderdetails AS details
       INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                           AND details.OrderID    = tbl_order.ID
  WHERE (details.OrderID = P_OrderID)
    AND (details.BilledWhen = P_BilledWhen)
    AND (details.BilledWhen != 'One Time')
    AND (details.ActualBillItemOn = P_BillItemOn)
    AND (details.SaleRentType = 'Re-occurring Sale'); --

  IF 0 < V_DetailsCount THEN
    -- create order
    INSERT INTO tbl_order
    (CustomerID
    ,Approved
    ,OrderDate
    ,DeliveryDate
    ,BillDate
    ,EndDate
    ,TakenBy
    ,ShippingMethodID
    ,SpecialInstructions
    ,CustomerInsurance1_ID
    ,CustomerInsurance2_ID
    ,CustomerInsurance3_ID
    ,CustomerInsurance4_ID
    ,ICD9_1
    ,ICD9_2
    ,ICD9_3
    ,ICD9_4
    ,ICD10_01
    ,ICD10_02
    ,ICD10_03
    ,ICD10_04
    ,ICD10_05
    ,ICD10_06
    ,ICD10_07
    ,ICD10_08
    ,ICD10_09
    ,ICD10_10
    ,ICD10_11
    ,ICD10_12
    ,DoctorID
    ,POSTypeID
    ,FacilityID
    ,ReferralID
    ,SalesrepID
    ,LocationID
    ,ClaimNote
    ,UserField1
    ,UserField2
    ,LastUpdateUserID)
    SELECT
     CustomerID
    ,0 as Approved
    ,V_NewOrderDate
    ,null as DeliveryDate
    ,null as BillDate
    ,null as EndDate
    ,'AutoGenerated' as TakenBy
    ,ShippingMethodID
    ,SpecialInstructions
    ,CustomerInsurance1_ID
    ,CustomerInsurance2_ID
    ,CustomerInsurance3_ID
    ,CustomerInsurance4_ID
    ,ICD9_1
    ,ICD9_2
    ,ICD9_3
    ,ICD9_4
    ,ICD10_01
    ,ICD10_02
    ,ICD10_03
    ,ICD10_04
    ,ICD10_05
    ,ICD10_06
    ,ICD10_07
    ,ICD10_08
    ,ICD10_09
    ,ICD10_10
    ,ICD10_11
    ,ICD10_12
    ,DoctorID
    ,POSTypeID
    ,FacilityID
    ,ReferralID
    ,SalesrepID
    ,LocationID
    ,ClaimNote
    ,UserField1
    ,UserField2
    ,1 as LastUpdateUserID
    FROM tbl_order
    WHERE (ID = P_OrderID); --

    SELECT LAST_INSERT_ID() INTO V_NewOrderID; --

    -- add line items to order
    INSERT INTO tbl_orderdetails
    (CustomerID
    ,OrderID
    ,InventoryItemID
    ,PriceCodeID
    ,SaleRentType
    ,BillablePrice
    ,AllowablePrice
    ,Taxable
    ,FlatRate
    ,DOSFrom
    ,DOSTo
    ,SerialNumber
    ,PickupDate
    ,ShowSpanDates
    -- ordered
    ,OrderedQuantity
    ,OrderedUnits
    ,OrderedWhen
    ,OrderedConverter
    -- billed
    ,BilledQuantity
    ,BilledUnits
    ,BilledWhen
    ,BilledConverter
    -- delivery
    ,DeliveryQuantity
    ,DeliveryUnits
    ,DeliveryConverter
    -- other
    ,BillingMonth
    ,BillingCode
    ,Modifier1
    ,Modifier2
    ,Modifier3
    ,Modifier4
    ,DXPointer
    ,DXPointer10
    ,DrugNoteField
    ,DrugControlNumber
    ,BillItemOn
    ,AuthorizationNumber
    ,AuthorizationTypeID
    ,AuthorizationExpirationDate
    ,ReasonForPickup
    ,SendCMN_RX_w_invoice
    ,MedicallyUnnecessary
    ,SpecialCode
    ,ReviewCode
    ,ReoccuringID
    ,HaoDescription
    ,CMNFormID
    ,WarehouseID
    ,BillIns1
    ,BillIns2
    ,BillIns3
    ,BillIns4
    ,NopayIns1
    ,AcceptAssignment
    ,UserField1
    ,UserField2)
    SELECT
     CustomerID
    ,OrderID
    ,InventoryItemID
    ,PriceCodeID
    ,SaleRentType
    ,BillablePrice
    ,AllowablePrice
    ,Taxable
    ,FlatRate
    ,DOSFrom
    ,DOSTo
    ,SerialNumber
    ,PickupDate
    ,ShowSpanDates
    -- ordered
    ,OrderedQuantity
    ,OrderedUnits
    ,OrderedWhen
    ,OrderedConverter
    -- billed
    ,BilledQuantity
    ,BilledUnits
    ,BilledWhen
    ,BilledConverter
    -- delivery
    ,DeliveryQuantity
    ,DeliveryUnits
    ,DeliveryConverter
    -- other
    ,BillingMonth
    ,BillingCode
    ,Modifier1
    ,Modifier2
    ,Modifier3
    ,Modifier4
    ,DXPointer
    ,DXPointer10
    ,DrugNoteField
    ,DrugControlNumber
    ,BillItemOn
    ,AuthorizationNumber
    ,AuthorizationTypeID
    ,AuthorizationExpirationDate
    ,ReasonForPickup
    ,SendCMN_RX_w_invoice
    ,MedicallyUnnecessary
    ,SpecialCode
    ,ReviewCode
    ,ReoccuringID
    ,HaoDescription
    ,CMNFormID
    ,WarehouseID
    ,BillIns1
    ,BillIns2
    ,BillIns3
    ,BillIns4
    ,NopayIns1
    ,AcceptAssignment
    ,UserField1
    ,UserField2
    FROM (
        SELECT
         details.CustomerID
        ,V_NewOrderID as OrderID
        ,details.InventoryItemID
        ,details.PriceCodeID
        ,'Re-occurring Sale' as SaleRentType
        ,details.BillablePrice
        ,details.AllowablePrice
        ,details.Taxable
        ,details.FlatRate
        ,IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom) as DOSFrom
        ,IF(details.BillingMonth <= 1, GetNextDosTo  (details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosTo  ) as DOSTo
        ,null as SerialNumber
        ,null as PickupDate
        ,details.ShowSpanDates
        -- ordered
        ,details.OrderedQuantity
        ,details.OrderedUnits
        ,details.OrderedWhen
        ,details.OrderedConverter
        -- billed
        -- ,details.BilledQuantity
        ,OrderedQty2BilledQty(
            IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom),
            IF(details.BillingMonth <= 1, GetNextDosTo  (details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosTo  ),
            details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
            details.OrderedConverter, details.DeliveryConverter, details.BilledConverter) as BilledQuantity
        ,details.BilledUnits
        ,details.BilledWhen
        ,details.BilledConverter
        -- delivery
        -- ,details.DeliveryQuantity
        ,OrderedQty2DeliveryQty(
            IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom),
            IF(details.BillingMonth <= 1, GetNextDosTo  (details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosTo  ),
            details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
            details.OrderedConverter, details.DeliveryConverter, details.BilledConverter) as DeliveryQuantity
        ,details.DeliveryUnits
        ,details.DeliveryConverter
        -- other
        ,1 as BillingMonth
        ,details.BillingCode
        ,details.Modifier1
        ,details.Modifier2
        ,details.Modifier3
        ,details.Modifier4
        ,details.DXPointer
        ,details.DXPointer10
        ,details.DrugNoteField
        ,details.DrugControlNumber
        ,details.BillItemOn
        ,details.AuthorizationNumber
        ,details.AuthorizationTypeID
        ,details.AuthorizationExpirationDate
        ,null as ReasonForPickup
        ,details.SendCMN_RX_w_invoice
        ,details.MedicallyUnnecessary
        ,details.SpecialCode
        ,details.ReviewCode
        ,details.ID as ReoccuringID
        ,details.HaoDescription
        ,details.CMNFormID
        ,details.WarehouseID
        ,details.BillIns1
        ,details.BillIns2
        ,details.BillIns3
        ,details.BillIns4
        ,details.NopayIns1
        ,details.AcceptAssignment
        ,details.UserField1
        ,details.UserField2
        FROM view_orderdetails as details
             INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                                 AND details.OrderID    = tbl_order.ID
        WHERE (details.OrderID = P_OrderID)
          AND (details.BilledWhen = P_BilledWhen)
          AND (details.BilledWhen != 'One Time')
          AND (details.ActualBillItemOn = P_BillItemOn)
          AND (details.SaleRentType = 'Re-occurring Sale')
    ) as `tmp`; --
  END IF; --

  -- update source line items -- mark them as one time sales
  UPDATE view_orderdetails as details
         INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                             AND details.OrderID    = tbl_order.ID
  SET details.SaleRentType = 'One Time Sale'
  WHERE (details.OrderID = P_OrderID)
    AND (details.BilledWhen = P_BilledWhen)
    AND (details.ActualBillItemOn = P_BillItemOn)
    AND (details.SaleRentType = 'Re-occurring Sale'); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  -- reoccuring sales support
  -- tbl_orderdetails.ReoccuringID - source line item
  DECLARE V_DetailsCount INT; --
  DECLARE V_NewOrderID INT; --
  DECLARE V_NewOrderDate DATETIME; --

  SELECT
     Count(*) as `Count`
    ,MAX(IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom)) as NewOrderDate
  INTO
     V_DetailsCount
    ,V_NewOrderDate
  FROM view_orderdetails AS details
       INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                           AND details.OrderID    = tbl_order.ID
  WHERE (details.OrderID = P_OrderID)
    AND (details.BilledWhen = P_BilledWhen)
    AND (details.BilledWhen != 'One Time')
    AND (details.ActualBillItemOn = P_BillItemOn)
    AND (details.SaleRentType = 'Re-occurring Sale'); --

  IF 0 < V_DetailsCount THEN
    -- create order
    INSERT INTO tbl_order
    (CustomerID
    ,Approved
    ,OrderDate
    ,DeliveryDate
    ,BillDate
    ,EndDate
    ,TakenBy
    ,ShippingMethodID
    ,SpecialInstructions
    ,CustomerInsurance1_ID
    ,CustomerInsurance2_ID
    ,CustomerInsurance3_ID
    ,CustomerInsurance4_ID
    ,ICD9_1
    ,ICD9_2
    ,ICD9_3
    ,ICD9_4
    ,ICD10_01
    ,ICD10_02
    ,ICD10_03
    ,ICD10_04
    ,ICD10_05
    ,ICD10_06
    ,ICD10_07
    ,ICD10_08
    ,ICD10_09
    ,ICD10_10
    ,ICD10_11
    ,ICD10_12
    ,DoctorID
    ,POSTypeID
    ,FacilityID
    ,ReferralID
    ,SalesrepID
    ,LocationID
    ,ClaimNote
    ,UserField1
    ,UserField2
    ,LastUpdateUserID)
    SELECT
     CustomerID
    ,0 as Approved
    ,V_NewOrderDate
    ,null as DeliveryDate
    ,null as BillDate
    ,null as EndDate
    ,'AutoGenerated' as TakenBy
    ,ShippingMethodID
    ,SpecialInstructions
    ,CustomerInsurance1_ID
    ,CustomerInsurance2_ID
    ,CustomerInsurance3_ID
    ,CustomerInsurance4_ID
    ,ICD9_1
    ,ICD9_2
    ,ICD9_3
    ,ICD9_4
    ,ICD10_01
    ,ICD10_02
    ,ICD10_03
    ,ICD10_04
    ,ICD10_05
    ,ICD10_06
    ,ICD10_07
    ,ICD10_08
    ,ICD10_09
    ,ICD10_10
    ,ICD10_11
    ,ICD10_12
    ,DoctorID
    ,POSTypeID
    ,FacilityID
    ,ReferralID
    ,SalesrepID
    ,LocationID
    ,ClaimNote
    ,UserField1
    ,UserField2
    ,1 as LastUpdateUserID
    FROM tbl_order
    WHERE (ID = P_OrderID); --

    SELECT lastval() INTO V_NewOrderID; --

    -- add line items to order
    INSERT INTO tbl_orderdetails
    (CustomerID
    ,OrderID
    ,InventoryItemID
    ,PriceCodeID
    ,SaleRentType
    ,BillablePrice
    ,AllowablePrice
    ,Taxable
    ,FlatRate
    ,DOSFrom
    ,DOSTo
    ,SerialNumber
    ,PickupDate
    ,ShowSpanDates
    -- ordered
    ,OrderedQuantity
    ,OrderedUnits
    ,OrderedWhen
    ,OrderedConverter
    -- billed
    ,BilledQuantity
    ,BilledUnits
    ,BilledWhen
    ,BilledConverter
    -- delivery
    ,DeliveryQuantity
    ,DeliveryUnits
    ,DeliveryConverter
    -- other
    ,BillingMonth
    ,BillingCode
    ,Modifier1
    ,Modifier2
    ,Modifier3
    ,Modifier4
    ,DXPointer
    ,DXPointer10
    ,DrugNoteField
    ,DrugControlNumber
    ,BillItemOn
    ,AuthorizationNumber
    ,AuthorizationTypeID
    ,AuthorizationExpirationDate
    ,ReasonForPickup
    ,SendCMN_RX_w_invoice
    ,MedicallyUnnecessary
    ,SpecialCode
    ,ReviewCode
    ,ReoccuringID
    ,HaoDescription
    ,CMNFormID
    ,WarehouseID
    ,BillIns1
    ,BillIns2
    ,BillIns3
    ,BillIns4
    ,NopayIns1
    ,AcceptAssignment
    ,UserField1
    ,UserField2)
    SELECT
     CustomerID
    ,OrderID
    ,InventoryItemID
    ,PriceCodeID
    ,SaleRentType
    ,BillablePrice
    ,AllowablePrice
    ,Taxable
    ,FlatRate
    ,DOSFrom
    ,DOSTo
    ,SerialNumber
    ,PickupDate
    ,ShowSpanDates
    -- ordered
    ,OrderedQuantity
    ,OrderedUnits
    ,OrderedWhen
    ,OrderedConverter
    -- billed
    ,BilledQuantity
    ,BilledUnits
    ,BilledWhen
    ,BilledConverter
    -- delivery
    ,DeliveryQuantity
    ,DeliveryUnits
    ,DeliveryConverter
    -- other
    ,BillingMonth
    ,BillingCode
    ,Modifier1
    ,Modifier2
    ,Modifier3
    ,Modifier4
    ,DXPointer
    ,DXPointer10
    ,DrugNoteField
    ,DrugControlNumber
    ,BillItemOn
    ,AuthorizationNumber
    ,AuthorizationTypeID
    ,AuthorizationExpirationDate
    ,ReasonForPickup
    ,SendCMN_RX_w_invoice
    ,MedicallyUnnecessary
    ,SpecialCode
    ,ReviewCode
    ,ReoccuringID
    ,HaoDescription
    ,CMNFormID
    ,WarehouseID
    ,BillIns1
    ,BillIns2
    ,BillIns3
    ,BillIns4
    ,NopayIns1
    ,AcceptAssignment
    ,UserField1
    ,UserField2
    FROM (
        SELECT
         details.CustomerID
        ,V_NewOrderID as OrderID
        ,details.InventoryItemID
        ,details.PriceCodeID
        ,'Re-occurring Sale' as SaleRentType
        ,details.BillablePrice
        ,details.AllowablePrice
        ,details.Taxable
        ,details.FlatRate
        ,IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom) as DOSFrom
        ,IF(details.BillingMonth <= 1, GetNextDosTo  (details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosTo  ) as DOSTo
        ,null as SerialNumber
        ,null as PickupDate
        ,details.ShowSpanDates
        -- ordered
        ,details.OrderedQuantity
        ,details.OrderedUnits
        ,details.OrderedWhen
        ,details.OrderedConverter
        -- billed
        -- ,details.BilledQuantity
        ,OrderedQty2BilledQty(
            IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom),
            IF(details.BillingMonth <= 1, GetNextDosTo  (details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosTo  ),
            details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
            details.OrderedConverter, details.DeliveryConverter, details.BilledConverter) as BilledQuantity
        ,details.BilledUnits
        ,details.BilledWhen
        ,details.BilledConverter
        -- delivery
        -- ,details.DeliveryQuantity
        ,OrderedQty2DeliveryQty(
            IF(details.BillingMonth <= 1, GetNextDosFrom(details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosFrom),
            IF(details.BillingMonth <= 1, GetNextDosTo  (details.DosFrom, details.DosTo, details.ActualBilledWhen), details.DosTo  ),
            details.OrderedQuantity, details.OrderedWhen, details.BilledWhen,
            details.OrderedConverter, details.DeliveryConverter, details.BilledConverter) as DeliveryQuantity
        ,details.DeliveryUnits
        ,details.DeliveryConverter
        -- other
        ,1 as BillingMonth
        ,details.BillingCode
        ,details.Modifier1
        ,details.Modifier2
        ,details.Modifier3
        ,details.Modifier4
        ,details.DXPointer
        ,details.DXPointer10
        ,details.DrugNoteField
        ,details.DrugControlNumber
        ,details.BillItemOn
        ,details.AuthorizationNumber
        ,details.AuthorizationTypeID
        ,details.AuthorizationExpirationDate
        ,null as ReasonForPickup
        ,details.SendCMN_RX_w_invoice
        ,details.MedicallyUnnecessary
        ,details.SpecialCode
        ,details.ReviewCode
        ,details.ID as ReoccuringID
        ,details.HaoDescription
        ,details.CMNFormID
        ,details.WarehouseID
        ,details.BillIns1
        ,details.BillIns2
        ,details.BillIns3
        ,details.BillIns4
        ,details.NopayIns1
        ,details.AcceptAssignment
        ,details.UserField1
        ,details.UserField2
        FROM view_orderdetails as details
             INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                                 AND details.OrderID    = tbl_order.ID
        WHERE (details.OrderID = P_OrderID)
          AND (details.BilledWhen = P_BilledWhen)
          AND (details.BilledWhen != 'One Time')
          AND (details.ActualBillItemOn = P_BillItemOn)
          AND (details.SaleRentType = 'Re-occurring Sale')
    ) as `tmp`; --
  END IF; --

  -- update source line items -- mark them as one time sales
  UPDATE view_orderdetails as details
         INNER JOIN tbl_order ON details.CustomerID = tbl_order.CustomerID
                             AND details.OrderID    = tbl_order.ID
  SET details.SaleRentType = 'One Time Sale'
  WHERE (details.OrderID = P_OrderID)
    AND (details.BilledWhen = P_BilledWhen)
    AND (details.ActualBillItemOn = P_BillItemOn)
    AND (details.SaleRentType = 'Re-occurring Sale'); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## process_reoccuring_purchaseorder

### Original MySQL Procedure
```sql
BEGIN
    -- reoccuring purchase order support
    DECLARE V_NewOrderID INT; --

    -- create order
    INSERT INTO tbl_purchaseorder
      (Approved
      ,Reoccuring
      ,Cost
      ,Freight
      ,Tax
      ,TotalDue
      ,VendorID
      ,ShipToName
      ,ShipToAddress1
      ,ShipToAddress2
      ,ShipToCity
      ,ShipToState
      ,ShipToZip
      ,ShipToPhone
      ,OrderDate
      ,CompanyName
      ,CompanyAddress1
      ,CompanyAddress2
      ,CompanyCity
      ,CompanyState
      ,CompanyZip
      ,ShipVia
      ,FOB
      ,VendorSalesRep
      ,Terms
      ,CompanyPhone
      ,TaxRateID
      ,LastUpdateUserID)
    SELECT
       Approved
      ,Reoccuring
      ,Cost
      ,Freight
      ,Tax
      ,TotalDue
      ,VendorID
      ,ShipToName
      ,ShipToAddress1
      ,ShipToAddress2
      ,ShipToCity
      ,ShipToState
      ,ShipToZip
      ,ShipToPhone
      ,OrderDate
      ,CompanyName
      ,CompanyAddress1
      ,CompanyAddress2
      ,CompanyCity
      ,CompanyState
      ,CompanyZip
      ,ShipVia
      ,FOB
      ,VendorSalesRep
      ,Terms
      ,CompanyPhone
      ,TaxRateID
      ,LastUpdateUserID
    FROM
    (
        SELECT
             0 as Approved
            ,1 as Reoccuring
            ,Cost
            ,Freight
            ,Tax
            ,TotalDue
            ,VendorID
            ,ShipToName
            ,ShipToAddress1
            ,ShipToAddress2
            ,ShipToCity
            ,ShipToState
            ,ShipToZip
            ,ShipToPhone
            ,DATE_ADD(OrderDate, INTERVAL 1 MONTH) as OrderDate
            ,CompanyName
            ,CompanyAddress1
            ,CompanyAddress2
            ,CompanyCity
            ,CompanyState
            ,CompanyZip
            ,ShipVia
            ,FOB
            ,VendorSalesRep
            ,Terms
            ,CompanyPhone
            ,TaxRateID
            ,0 as LastUpdateUserID
        FROM tbl_purchaseorder
        WHERE (Reoccuring = 1)
          AND (ID = P_PurchaseOrderID)
    ) as `tmp`; --

    SELECT LAST_INSERT_ID() INTO V_NewOrderID; --

  IF V_NewOrderID <> 0 THEN
    -- add line items to order
    INSERT INTO tbl_purchaseorderdetails
      (BackOrder
      ,Ordered
      ,Received
      ,Price
      ,Customer
      ,DatePromised
      ,DateReceived
      ,DropShipToCustomer
      ,InventoryItemID
      ,PurchaseOrderID
      ,WarehouseID
      ,LastUpdateUserID
      ,LastUpdateDatetime
      ,VendorSTKNumber
      ,ReferenceNumber)
    SELECT
       BackOrder
      ,Ordered
      ,Received
      ,Price
      ,Customer
      ,DatePromised
      ,DateReceived
      ,DropShipToCustomer
      ,InventoryItemID
      ,PurchaseOrderID
      ,WarehouseID
      ,LastUpdateUserID
      ,LastUpdateDatetime
      ,VendorSTKNumber
      ,ReferenceNumber
    FROM (
        SELECT
           0 as BackOrder
          ,Ordered as Ordered
          ,0 as Received
          ,Price
          ,Customer
          ,DATE_ADD(DatePromised, INTERVAL 1 MONTH) as DatePromised
          ,null as DateReceived
          ,DropShipToCustomer
          ,InventoryItemID
          ,V_NewOrderID as PurchaseOrderID
          ,WarehouseID
          ,LastUpdateUserID
          ,CURRENT_DATE as LastUpdateDatetime
          ,VendorSTKNumber
          ,ReferenceNumber
        FROM tbl_purchaseorderdetails
        WHERE (PurchaseOrderID = P_PurchaseOrderID)
    ) as `tmp`; --

    -- update source -- mark them as one time sales
    UPDATE tbl_purchaseorder
    SET Reoccuring = 0
    WHERE (Reoccuring = 1)
      AND (ID = P_PurchaseOrderID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
    -- reoccuring purchase order support
    DECLARE V_NewOrderID INT; --

    -- create order
    INSERT INTO tbl_purchaseorder
      (Approved
      ,Reoccuring
      ,Cost
      ,Freight
      ,Tax
      ,TotalDue
      ,VendorID
      ,ShipToName
      ,ShipToAddress1
      ,ShipToAddress2
      ,ShipToCity
      ,ShipToState
      ,ShipToZip
      ,ShipToPhone
      ,OrderDate
      ,CompanyName
      ,CompanyAddress1
      ,CompanyAddress2
      ,CompanyCity
      ,CompanyState
      ,CompanyZip
      ,ShipVia
      ,FOB
      ,VendorSalesRep
      ,Terms
      ,CompanyPhone
      ,TaxRateID
      ,LastUpdateUserID)
    SELECT
       Approved
      ,Reoccuring
      ,Cost
      ,Freight
      ,Tax
      ,TotalDue
      ,VendorID
      ,ShipToName
      ,ShipToAddress1
      ,ShipToAddress2
      ,ShipToCity
      ,ShipToState
      ,ShipToZip
      ,ShipToPhone
      ,OrderDate
      ,CompanyName
      ,CompanyAddress1
      ,CompanyAddress2
      ,CompanyCity
      ,CompanyState
      ,CompanyZip
      ,ShipVia
      ,FOB
      ,VendorSalesRep
      ,Terms
      ,CompanyPhone
      ,TaxRateID
      ,LastUpdateUserID
    FROM
    (
        SELECT
             0 as Approved
            ,1 as Reoccuring
            ,Cost
            ,Freight
            ,Tax
            ,TotalDue
            ,VendorID
            ,ShipToName
            ,ShipToAddress1
            ,ShipToAddress2
            ,ShipToCity
            ,ShipToState
            ,ShipToZip
            ,ShipToPhone
            ,DATE_ADD(OrderDate, INTERVAL 1 MONTH) as OrderDate
            ,CompanyName
            ,CompanyAddress1
            ,CompanyAddress2
            ,CompanyCity
            ,CompanyState
            ,CompanyZip
            ,ShipVia
            ,FOB
            ,VendorSalesRep
            ,Terms
            ,CompanyPhone
            ,TaxRateID
            ,0 as LastUpdateUserID
        FROM tbl_purchaseorder
        WHERE (Reoccuring = 1)
          AND (ID = P_PurchaseOrderID)
    ) as `tmp`; --

    SELECT lastval() INTO V_NewOrderID; --

  IF V_NewOrderID <> 0 THEN
    -- add line items to order
    INSERT INTO tbl_purchaseorderdetails
      (BackOrder
      ,Ordered
      ,Received
      ,Price
      ,Customer
      ,DatePromised
      ,DateReceived
      ,DropShipToCustomer
      ,InventoryItemID
      ,PurchaseOrderID
      ,WarehouseID
      ,LastUpdateUserID
      ,LastUpdateDatetime
      ,VendorSTKNumber
      ,ReferenceNumber)
    SELECT
       BackOrder
      ,Ordered
      ,Received
      ,Price
      ,Customer
      ,DatePromised
      ,DateReceived
      ,DropShipToCustomer
      ,InventoryItemID
      ,PurchaseOrderID
      ,WarehouseID
      ,LastUpdateUserID
      ,LastUpdateDatetime
      ,VendorSTKNumber
      ,ReferenceNumber
    FROM (
        SELECT
           0 as BackOrder
          ,Ordered as Ordered
          ,0 as Received
          ,Price
          ,Customer
          ,DATE_ADD(DatePromised, INTERVAL 1 MONTH) as DatePromised
          ,null as DateReceived
          ,DropShipToCustomer
          ,InventoryItemID
          ,V_NewOrderID as PurchaseOrderID
          ,WarehouseID
          ,LastUpdateUserID
          ,CURRENT_DATE as LastUpdateDatetime
          ,VendorSTKNumber
          ,ReferenceNumber
        FROM tbl_purchaseorderdetails
        WHERE (PurchaseOrderID = P_PurchaseOrderID)
    ) as `tmp`; --

    -- update source -- mark them as one time sales
    UPDATE tbl_purchaseorder
    SET Reoccuring = 0
    WHERE (Reoccuring = 1)
      AND (ID = P_PurchaseOrderID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## PurchaseOrder_UpdateTotals

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_Cost double; --

  SELECT Sum(Price * Ordered)
  INTO V_Cost
  FROM tbl_purchaseorderdetails
  WHERE (PurchaseOrderID = P_PurchaseOrderID); --

  UPDATE tbl_purchaseorder
  SET Cost = IfNull(V_Cost, 0),
      TotalDue = IfNull(V_Cost, 0) + IfNull(Freight, 0) + IfNull(Tax, 0)
  WHERE (ID = P_PurchaseOrderID); --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_Cost double; --

  SELECT Sum(Price * Ordered)
  INTO V_Cost
  FROM tbl_purchaseorderdetails
  WHERE (PurchaseOrderID = P_PurchaseOrderID); --

  UPDATE tbl_purchaseorder
  SET Cost = IfNull(V_Cost, 0),
      TotalDue = IfNull(V_Cost, 0) + IfNull(Freight, 0) + IfNull(Tax, 0)
  WHERE (ID = P_PurchaseOrderID); --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## retailinvoice_addpayments

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE V_Amount DECIMAL(18, 2); --
  DECLARE V_Extra TEXT; --
  DECLARE V_NewXml VARCHAR(50); --
  DECLARE V_Result VARCHAR(50); --
  DECLARE done INT DEFAULT 0; --
  DECLARE cur CURSOR FOR
    SELECT ID, BillableAmount FROM tbl_invoicedetails WHERE (InvoiceID = P_InvoiceID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  DETAILS_LOOP: LOOP
    FETCH cur INTO V_InvoiceDetailsID, V_Amount; --

    IF done THEN
      LEAVE DETAILS_LOOP; --
    END IF; --

    SET V_NewXml = CONCAT('<v n="Paid">', CAST(V_Amount as CHAR), '</v>'); --
    SET V_Extra = UpdateXML(P_Extra, 'values/v[@n="Paid"]' COLLATE latin1_general_ci, V_NewXml COLLATE latin1_general_ci); --

    CALL `InvoiceDetails_AddPayment`
    ( V_InvoiceDetailsID
    , NULL -- P_InsuranceCompanyID
    , P_TransactionDate
    , V_Extra
    , '' -- P_Comments
    , '' -- P_Options
    , P_LastUpdateUserID
    , V_Result); --
  END LOOP DETAILS_LOOP; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_InvoiceDetailsID INT; --
  DECLARE V_Amount DECIMAL(18, 2); --
  DECLARE V_Extra TEXT; --
  DECLARE V_NewXml VARCHAR(50); --
  DECLARE V_Result VARCHAR(50); --
  DECLARE done INT DEFAULT 0; --
  DECLARE cur CURSOR FOR
    SELECT ID, BillableAmount FROM tbl_invoicedetails WHERE (InvoiceID = P_InvoiceID); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  DETAILS_LOOP: LOOP
    FETCH cur INTO V_InvoiceDetailsID, V_Amount; --

    IF done THEN
      LEAVE DETAILS_LOOP; --
    END IF; --

    SET V_NewXml = CONCAT('<v n="Paid">', CAST(V_Amount as CHAR), '</v>'); --
    SET V_Extra = UpdateXML(P_Extra, 'values/v[@n="Paid"]' COLLATE latin1_general_ci, V_NewXml COLLATE latin1_general_ci); --

    CALL `InvoiceDetails_AddPayment`
    ( V_InvoiceDetailsID
    , NULL -- P_InsuranceCompanyID
    , P_TransactionDate
    , V_Extra
    , '' -- P_Comments
    , '' -- P_Options
    , P_LastUpdateUserID
    , V_Result); --
  END LOOP DETAILS_LOOP; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## serials_fix

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_Count, V_WarehouseID INT; --
  DECLARE cur_ID, cur_WarehouseID INT; --
  DECLARE done INT DEFAULT 0; --

  DECLARE cur CURSOR FOR
    SELECT
      tbl_serial.ID
    , tbl_warehouse.ID as WarehouseID
    FROM tbl_serial
         LEFT JOIN tbl_warehouse ON tbl_serial.WarehouseID = tbl_warehouse.ID
    WHERE tbl_serial.ID NOT IN (SELECT SerialID FROM tbl_serial_transaction); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SELECT Count(*), Min(ID)
  INTO V_Count, V_WarehouseID
  FROM tbl_warehouse; --

  IF V_Count = 0 THEN
    INSERT INTO tbl_warehouse SET
      `Address1` = '',
      `Address2` = '',
      `City`     = '',
      `Contact`  = '',
      `Fax`      = '',
      `Name`     = 'Default warehouse',
      `Phone`    = '',
      `Phone2`   = '',
      `State`    = '',
      `Zip`      = '',
      `LastUpdateUserID` = 1; --

    SELECT LAST_INSERT_ID()
    INTO V_WarehouseID; --
  END IF; --

  OPEN cur; --

  REPEAT
    FETCH cur
    INTO cur_ID, cur_WarehouseID; --

    IF NOT done THEN
      SET cur_WarehouseID = IFNULL(cur_WarehouseID, V_WarehouseID); --

      CALL serial_add_transaction(
         'Transferred In' -- P_TranType         VARCHAR(50),
        ,Now()            -- P_TranTime         DATETIME,
        ,cur_ID           -- P_SerialID         INT,
        ,cur_WarehouseID  -- P_WarehouseID      INT,
        ,null             -- P_VendorID         INT,
        ,null             -- P_CustomerID       INT,
        ,null             -- P_OrderID          INT,
        ,null             -- P_OrderDetailsID   INT,
        ,null             -- P_LotNumber        VARCHAR(50),
        ,null             -- P_LastUpdateUserID INT
        ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_Count, V_WarehouseID INT; --
  DECLARE cur_ID, cur_WarehouseID INT; --
  DECLARE done INT DEFAULT 0; --

  DECLARE cur CURSOR FOR
    SELECT
      tbl_serial.ID
    , tbl_warehouse.ID as WarehouseID
    FROM tbl_serial
         LEFT JOIN tbl_warehouse ON tbl_serial.WarehouseID = tbl_warehouse.ID
    WHERE tbl_serial.ID NOT IN (SELECT SerialID FROM tbl_serial_transaction); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  SELECT Count(*), Min(ID)
  INTO V_Count, V_WarehouseID
  FROM tbl_warehouse; --

  IF V_Count = 0 THEN
    INSERT INTO tbl_warehouse SET
      `Address1` = '',
      `Address2` = '',
      `City`     = '',
      `Contact`  = '',
      `Fax`      = '',
      `Name`     = 'Default warehouse',
      `Phone`    = '',
      `Phone2`   = '',
      `State`    = '',
      `Zip`      = '',
      `LastUpdateUserID` = 1; --

    SELECT lastval()
    INTO V_WarehouseID; --
  END IF; --

  OPEN cur; --

  REPEAT
    FETCH cur
    INTO cur_ID, cur_WarehouseID; --

    IF NOT done THEN
      SET cur_WarehouseID = COALESCE(cur_WarehouseID, V_WarehouseID); --

      CALL serial_add_transaction(
         'Transferred In' -- P_TranType         VARCHAR(50),
        ,Now()            -- P_TranTime         DATETIME,
        ,cur_ID           -- P_SerialID         INT,
        ,cur_WarehouseID  -- P_WarehouseID      INT,
        ,null             -- P_VendorID         INT,
        ,null             -- P_CustomerID       INT,
        ,null             -- P_OrderID          INT,
        ,null             -- P_OrderDetailsID   INT,
        ,null             -- P_LotNumber        VARCHAR(50),
        ,null             -- P_LastUpdateUserID INT
        ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## serials_po_refresh

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_SerialID, V_VendorID, V_InventoryItemID, V_WarehouseID INT; --
  DECLARE V_ReceivedDate DATETIME; --
  DECLARE V_ReceivedQuantity, V_SerialCount INT; --
  DECLARE V_PurchasePrice decimal(18, 2); --
  DECLARE cur CURSOR FOR
      SELECT
        tbl_purchaseorder.VendorID,
        tbl_purchaseorderdetails.InventoryItemID,
        tbl_purchaseorderdetails.WarehouseID,
        MAX(tbl_purchaseorderdetails.DateReceived) as ReceivedDate,
        SUM(tbl_purchaseorderdetails.Received) as ReceivedQuantity,
        tbl_purchaseorderdetails.Price as PurchasePrice
      FROM tbl_purchaseorder
           INNER JOIN tbl_purchaseorderdetails ON tbl_purchaseorder.ID = tbl_purchaseorderdetails.PurchaseOrderID
           INNER JOIN tbl_inventoryitem ON tbl_purchaseorderdetails.InventoryItemID = tbl_inventoryitem.ID
      WHERE (tbl_purchaseorder.ID = P_PurchaseOrderID)
        AND (tbl_inventoryitem.Serialized = 1)
      GROUP BY tbl_purchaseorder.VendorID, tbl_purchaseorderdetails.InventoryItemID, tbl_purchaseorderdetails.WarehouseID; --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  DROP TABLE IF EXISTS `{A890A925-A355-44AA-AA99-D28A52F7DF0D}`; --

  CREATE TEMPORARY TABLE `{A890A925-A355-44AA-AA99-D28A52F7DF0D}` (SerialID INT); --

  IF EXISTS (SELECT * FROM tbl_purchaseorder WHERE Approved = 1 AND ID = P_PurchaseOrderID) THEN
    OPEN cur; --

    REPEAT
      FETCH cur INTO
        V_VendorID,
        V_InventoryItemID,
        V_WarehouseID,
        V_ReceivedDate,
        V_ReceivedQuantity,
        V_PurchasePrice; --

      IF NOT done THEN
        SET V_SerialCount = V_ReceivedQuantity; --

        SELECT Count(*)
        INTO V_SerialCount
        FROM tbl_serial
        WHERE (WarehouseID = V_WarehouseID)
          AND (InventoryItemID = V_InventoryItemID)
          AND (VendorID = V_VendorID)
          AND (PurchaseOrderID = P_PurchaseOrderID); --

        WHILE (V_SerialCount < V_ReceivedQuantity) DO
          INSERT INTO tbl_serial (WarehouseID, InventoryItemID, VendorID, PurchaseOrderID, PurchaseDate, PurchaseAmount, Status)
          VALUES (V_WarehouseID, V_InventoryItemID, V_VendorID, P_PurchaseOrderID, V_ReceivedDate, V_PurchasePrice, 'On Hand'); --

          SELECT LAST_INSERT_ID() INTO V_SerialID; --

          INSERT INTO `{A890A925-A355-44AA-AA99-D28A52F7DF0D}` (SerialID) VALUES (V_SerialID); --

          SET V_SerialCount = V_SerialCount + 1; --
        END WHILE; --
      END IF; --
    UNTIL done END REPEAT; --

    CLOSE cur; --
  END IF; --

  SELECT SerialID FROM `{A890A925-A355-44AA-AA99-D28A52F7DF0D}`; --

  DROP TABLE `{A890A925-A355-44AA-AA99-D28A52F7DF0D}`; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --
  DECLARE V_SerialID, V_VendorID, V_InventoryItemID, V_WarehouseID INT; --
  DECLARE V_ReceivedDate DATETIME; --
  DECLARE V_ReceivedQuantity, V_SerialCount INT; --
  DECLARE V_PurchasePrice decimal(18, 2); --
  DECLARE cur CURSOR FOR
      SELECT
        tbl_purchaseorder.VendorID,
        tbl_purchaseorderdetails.InventoryItemID,
        tbl_purchaseorderdetails.WarehouseID,
        MAX(tbl_purchaseorderdetails.DateReceived) as ReceivedDate,
        SUM(tbl_purchaseorderdetails.Received) as ReceivedQuantity,
        tbl_purchaseorderdetails.Price as PurchasePrice
      FROM tbl_purchaseorder
           INNER JOIN tbl_purchaseorderdetails ON tbl_purchaseorder.ID = tbl_purchaseorderdetails.PurchaseOrderID
           INNER JOIN tbl_inventoryitem ON tbl_purchaseorderdetails.InventoryItemID = tbl_inventoryitem.ID
      WHERE (tbl_purchaseorder.ID = P_PurchaseOrderID)
        AND (tbl_inventoryitem.Serialized = 1)
      GROUP BY tbl_purchaseorder.VendorID, tbl_purchaseorderdetails.InventoryItemID, tbl_purchaseorderdetails.WarehouseID; --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  DROP TABLE IF EXISTS `{A890A925-A355-44AA-AA99-D28A52F7DF0D}`; --

  CREATE TEMPORARY TABLE `{A890A925-A355-44AA-AA99-D28A52F7DF0D}` (SerialID INT); --

  IF EXISTS (SELECT * FROM tbl_purchaseorder WHERE Approved = 1 AND ID = P_PurchaseOrderID) THEN
    OPEN cur; --

    REPEAT
      FETCH cur INTO
        V_VendorID,
        V_InventoryItemID,
        V_WarehouseID,
        V_ReceivedDate,
        V_ReceivedQuantity,
        V_PurchasePrice; --

      IF NOT done THEN
        SET V_SerialCount = V_ReceivedQuantity; --

        SELECT Count(*)
        INTO V_SerialCount
        FROM tbl_serial
        WHERE (WarehouseID = V_WarehouseID)
          AND (InventoryItemID = V_InventoryItemID)
          AND (VendorID = V_VendorID)
          AND (PurchaseOrderID = P_PurchaseOrderID); --

        WHILE (V_SerialCount < V_ReceivedQuantity) DO
          INSERT INTO tbl_serial (WarehouseID, InventoryItemID, VendorID, PurchaseOrderID, PurchaseDate, PurchaseAmount, Status)
          VALUES (V_WarehouseID, V_InventoryItemID, V_VendorID, P_PurchaseOrderID, V_ReceivedDate, V_PurchasePrice, 'On Hand'); --

          SELECT lastval() INTO V_SerialID; --

          INSERT INTO `{A890A925-A355-44AA-AA99-D28A52F7DF0D}` (SerialID) VALUES (V_SerialID); --

          SET V_SerialCount = V_SerialCount + 1; --
        END WHILE; --
      END IF; --
    UNTIL done END REPEAT; --

    CLOSE cur; --
  END IF; --

  SELECT SerialID FROM `{A890A925-A355-44AA-AA99-D28A52F7DF0D}`; --

  DROP TABLE `{A890A925-A355-44AA-AA99-D28A52F7DF0D}`; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## serials_refresh

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_SerialID INT; --
  DECLARE done INT DEFAULT 0; --

  DECLARE cur CURSOR FOR
    SELECT ID
    FROM tbl_serial
    WHERE ((P_SerialID        IS NULL) OR (ID              = P_SerialID       ))
      AND ((P_WarehouseID     IS NULL) OR (WarehouseID     = P_WarehouseID    ))
      AND ((P_InventoryItemID IS NULL) OR (InventoryItemID = P_InventoryItemID)); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      V_SerialID; --

    IF NOT done THEN
      CALL serial_add_transaction(
         null -- P_TranType         VARCHAR(50),
        ,null -- P_TranTime         DATETIME,
        ,V_SerialID -- P_SerialID         INT,
        ,null -- P_WarehouseID      INT,
        ,null -- P_VendorID         INT,
        ,null -- P_CustomerID       INT,
        ,null -- P_OrderID          INT,
        ,null -- P_OrderDetailsID   INT,
        ,null -- P_LotNumber        VARCHAR(50),
        ,null -- P_LastUpdateUserID INT
        ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_SerialID INT; --
  DECLARE done INT DEFAULT 0; --

  DECLARE cur CURSOR FOR
    SELECT ID
    FROM tbl_serial
    WHERE ((P_SerialID        IS NULL) OR (ID              = P_SerialID       ))
      AND ((P_WarehouseID     IS NULL) OR (WarehouseID     = P_WarehouseID    ))
      AND ((P_InventoryItemID IS NULL) OR (InventoryItemID = P_InventoryItemID)); --
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
      V_SerialID; --

    IF NOT done THEN
      CALL serial_add_transaction(
         null -- P_TranType         VARCHAR(50),
        ,null -- P_TranTime         DATETIME,
        ,V_SerialID -- P_SerialID         INT,
        ,null -- P_WarehouseID      INT,
        ,null -- P_VendorID         INT,
        ,null -- P_CustomerID       INT,
        ,null -- P_OrderID          INT,
        ,null -- P_OrderDetailsID   INT,
        ,null -- P_LotNumber        VARCHAR(50),
        ,null -- P_LastUpdateUserID INT
        ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## serial_add_transaction

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_TranID             int(11); --
  DECLARE cur_TranExists         int(11); --
  DECLARE cur_TranTypeID         int(11); --
  DECLARE cur_TranType           varchar(50); --
  DECLARE cur_TranTime           datetime; --
  DECLARE cur_VendorID           int(11); --
  DECLARE cur_WarehouseID        int(11); --
  DECLARE cur_CustomerID         int(11); --
  DECLARE cur_OrderID            int(11); --
  DECLARE cur_OrderDetailsID     int(11); --
  DECLARE cur_LotNumber          varchar(50); --
  DECLARE cur_LastUpdateUserID   smallint(6); --
  DECLARE cur_LastUpdateDatetime timestamp; --

  -- variables for update
  DECLARE V_Status            varchar(20); --
  DECLARE V_VendorID          int(11); --
  DECLARE V_WarehouseID       int(11); --
  DECLARE V_LotNumber         varchar(50); --
  DECLARE V_SoldDate          date; --
  DECLARE V_CurrentCustomerID int(11); --
  DECLARE V_LastCustomerID    int(11); --
  DECLARE V_LastUpdateUserID  smallint(6); --
  DECLARE V_AcceptableTran    bool; --

  DECLARE cur CURSOR FOR
   (SELECT
     tbl_serial_transaction.ID        as TranID
    ,1                                as TranExists
    ,tbl_serial_transaction_type.ID   as TranTypeID
    ,tbl_serial_transaction_type.Name as TranType
    ,tbl_serial_transaction.TransactionDatetime
    ,tbl_serial_transaction.VendorID
    ,tbl_serial_transaction.WarehouseID
    ,tbl_serial_transaction.CustomerID
    ,tbl_serial_transaction.OrderID
    ,tbl_serial_transaction.OrderDetailsID
    ,tbl_serial_transaction.LotNumber
    ,tbl_serial_transaction.LastUpdateUserID
    ,tbl_serial_transaction.LastUpdateDatetime
    FROM tbl_serial
         INNER JOIN tbl_serial_transaction ON tbl_serial.ID = tbl_serial_transaction.SerialID
         INNER JOIN tbl_serial_transaction_type ON tbl_serial_transaction.TypeID = tbl_serial_transaction_type.ID
    WHERE (tbl_serial.ID = P_SerialID))
   UNION ALL
   (SELECT
     NULL                             as TranID
    ,0                                as TranExists
    ,tbl_serial_transaction_type.ID   as TranTypeID
    ,tbl_serial_transaction_type.Name as TranType
    ,IFNULL(P_TranTime, Now())        as TransactionDatetime
    ,P_VendorID         as VendorID
    ,P_WarehouseID      as WarehouseID
    ,P_CustomerID       as CustomerID
    ,P_OrderID          as OrderID
    ,P_OrderDetailsID   as OrderDetailsID
    ,P_LotNumber        as LotNumber
    ,P_LastUpdateUserID as LastUpdateUserID
    ,Now()              as LastUpdateDatetime
    FROM tbl_serial_transaction_type
    WHERE (tbl_serial_transaction_type.Name = P_TranType))
   ORDER BY TranExists desc, TranID asc; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  IF (P_SerialID IS NOT NULL) THEN
    -- init / reinit
    SET V_Status            = 'Unknown'; --
    SET V_VendorID          = null; --
    SET V_WarehouseID       = null; --
    SET V_LotNumber         = null; --
    SET V_SoldDate          = null; --
    SET V_LastCustomerID    = null; --
    SET V_CurrentCustomerID = null; --
    SET V_LastUpdateUserID  = null; --

    OPEN cur; --

    REPEAT
      FETCH cur INTO
       cur_TranID
      ,cur_TranExists
      ,cur_TranTypeID
      ,cur_TranType
      ,cur_TranTime
      ,cur_VendorID
      ,cur_WarehouseID
      ,cur_CustomerID
      ,cur_OrderID
      ,cur_OrderDetailsID
      ,cur_LotNumber
      ,cur_LastUpdateUserID
      ,cur_LastUpdateDatetime; --

      IF (done = 0)
      AND (cur_TranTypeID IS NOT NULL)
      THEN
        SET V_AcceptableTran = 1; --

        IF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'Reserved') THEN
          -- ( 1, 'Reserved'                ), -- means that we added serial to not approved order
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          -- SET cur_CustomerID      = null; -- we need to know for whom did we reserved that serial
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Reserved'; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Reserved')) AND (cur_TranType = 'Reserve Cancelled') THEN
          -- ( 2, 'Reserve Cancelled'       ), -- means that we removed serial from not approved order
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand', 'Reserved') AND cur_TranType IN ('Rented', 'Sold'))
            OR (V_Status IN ('Rented') AND cur_TranType IN ('Sold')) THEN
          -- ( 3, 'Rented'                  ), -- means that RENT order was approved
          -- ( 4, 'Sold'                    ), -- means that SALE order or RENT-TO-PURCHASE order was approved
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          -- SET cur_CustomerID      = null; -- we need to know who rented or bought that serial
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = CASE WHEN cur_TranType = 'Sold' THEN cur_TranTime ELSE null END; --
          SET V_Status            = CASE cur_TranType WHEN 'Rented' THEN 'Rented' WHEN 'Sold' THEN 'Sold' ELSE null END; --
          SET V_WarehouseID       = null; --
          SET V_LastCustomerID    = V_CurrentCustomerID; --
          SET V_CurrentCustomerID = cur_CustomerID; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status != 'Maintenance') AND (cur_TranType IN ('Returned')) THEN
          -- ( 5, 'Returned'                ), -- means that user return RENTED serial
          --                                   -- but serial must be cleaned out prior to re-using
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          -- SET cur_CustomerID      = null; -- we need to know whom we returned that serial from
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Maintenance'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_LastCustomerID    = V_CurrentCustomerID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (cur_TranType IN ('Lost', 'Junked')) THEN
          -- ( 6, 'Lost'                    ), -- can be added only manually to mark serial as 'Lost'
          -- ( 7, 'Junked'                  ), -- can be added only manually to mark serial as 'Junked'
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = CASE cur_TranType WHEN 'Lost' THEN 'Lost' WHEN 'Junked' THEN 'Junked' ELSE null END; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Empty')) AND (cur_TranType = 'O2 Tank out for filling') THEN
          -- ( 8, 'O2 Tank out for filling' ), -- Send    : "Empty"   -> "Sent"
          -- SET cur_VendorID        = null; -- we need to know whom we sent that serial
          SET cur_WarehouseID     = null; --
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Sent'; --
          SET V_VendorID          = cur_VendorID; --
          SET V_WarehouseID       = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Sent')) AND (cur_TranType = 'O2 Tank in from filling') THEN
          -- ( 9, 'O2 Tank in from filling' ), -- Receive : "Sent"    -> "On Hand"
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          -- SET cur_LotNumber       = null; --we need to know lot number assigned

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_LotNumber         = cur_LotNumber; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'O2 Tank out to customer') THEN
          -- (10, 'O2 Tank out to customer' ), -- Rent    : "On Hand" -> "Rented"
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          -- SET cur_CustomerID      = null; -- we need to know whom we sent that serial
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Rented'; --
          SET V_WarehouseID       = null; --
          SET V_CurrentCustomerID = cur_CustomerID; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Rented')) AND (cur_TranType = 'O2 Tank in from customer') THEN
          -- (11, 'O2 Tank in from customer'), -- Pickup  : "Rented"  -> "Empty"
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Empty'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_LastCustomerID    = V_CurrentCustomerID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'Transferred Out') THEN
          -- (14, 'Transferred Out' ), -- Transferred Out  : "On Hand" -> "Transferred Out"
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; -- we need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Transferred Out'; --
          SET V_WarehouseID       = null; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Transferred Out')) AND (cur_TranType = 'Transferred In') THEN
          -- (15, 'Transferred In' ) -- Transferred In  : "Transferred Out" -> "On Hand"
          --                                            :       <NULL>      -> "On Hand"
          -- only way to assign warehouse to serial
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; -- we need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'Out for Maintenance') THEN
          -- (12, 'Out for Maintenance' ), -- Out for Maintenance  : "On Hand" -> "Maintenance"
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we sent that serial
          SET cur_CustomerID      = null; -- we do not need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Maintenance'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Maintenance')) AND (cur_TranType = 'In from Maintenance') THEN
          -- (13, 'In from Maintenance' ) -- In from Maintenance  : "Maintenance" -> "On Hand"
          --                                                      :     <NULL>    -> "On Hand"
          -- another way to assign warehouse to serial
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; -- we do not need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSE
          SET V_AcceptableTran = 0; --

        END IF; --

        IF (V_AcceptableTran = 1) AND (cur_TranExists = 0) THEN
          INSERT INTO tbl_serial_transaction SET
           TypeID              = cur_TranTypeID
          ,SerialID            = P_SerialID
          ,TransactionDatetime = cur_TranTime
          ,VendorID            = cur_VendorID
          ,WarehouseID         = cur_WarehouseID
          ,CustomerID          = cur_CustomerID
          ,OrderID             = cur_OrderID
          ,OrderDetailsID      = cur_OrderDetailsID
          ,LotNumber           = IFNULL(cur_LotNumber, '')
          ,LastUpdateDatetime  = IFNULL(cur_LastUpdateDatetime, Now())
          ,LastUpdateUserID    = IFNULL(cur_LastUpdateUserID, 1); -- root
        END IF; --

      END IF; --
    UNTIL done END REPEAT; --

    CLOSE cur; --

    -- save into db
    UPDATE tbl_serial SET
      Status            = CASE WHEN V_Status = 'Unknown' THEN 'On Hand' ELSE V_Status END
    , VendorID          = V_VendorID
    , WarehouseID       = V_WarehouseID
    , LotNumber         = IFNULL(V_LotNumber, '')
    , SoldDate          = V_SoldDate
    , CurrentCustomerID = V_CurrentCustomerID
    , LastCustomerID    = V_LastCustomerID
    , LastUpdateUserID  = IFNULL(V_LastUpdateUserID, 1) -- root
    WHERE (ID = P_SerialID); --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_TranID             int(11); --
  DECLARE cur_TranExists         int(11); --
  DECLARE cur_TranTypeID         int(11); --
  DECLARE cur_TranType           varchar(50); --
  DECLARE cur_TranTime           datetime; --
  DECLARE cur_VendorID           int(11); --
  DECLARE cur_WarehouseID        int(11); --
  DECLARE cur_CustomerID         int(11); --
  DECLARE cur_OrderID            int(11); --
  DECLARE cur_OrderDetailsID     int(11); --
  DECLARE cur_LotNumber          varchar(50); --
  DECLARE cur_LastUpdateUserID   smallint(6); --
  DECLARE cur_LastUpdateDatetime timestamp; --

  -- variables for update
  DECLARE V_Status            varchar(20); --
  DECLARE V_VendorID          int(11); --
  DECLARE V_WarehouseID       int(11); --
  DECLARE V_LotNumber         varchar(50); --
  DECLARE V_SoldDate          date; --
  DECLARE V_CurrentCustomerID int(11); --
  DECLARE V_LastCustomerID    int(11); --
  DECLARE V_LastUpdateUserID  smallint(6); --
  DECLARE V_AcceptableTran    bool; --

  DECLARE cur CURSOR FOR
   (SELECT
     tbl_serial_transaction.ID        as TranID
    ,1                                as TranExists
    ,tbl_serial_transaction_type.ID   as TranTypeID
    ,tbl_serial_transaction_type.Name as TranType
    ,tbl_serial_transaction.TransactionDatetime
    ,tbl_serial_transaction.VendorID
    ,tbl_serial_transaction.WarehouseID
    ,tbl_serial_transaction.CustomerID
    ,tbl_serial_transaction.OrderID
    ,tbl_serial_transaction.OrderDetailsID
    ,tbl_serial_transaction.LotNumber
    ,tbl_serial_transaction.LastUpdateUserID
    ,tbl_serial_transaction.LastUpdateDatetime
    FROM tbl_serial
         INNER JOIN tbl_serial_transaction ON tbl_serial.ID = tbl_serial_transaction.SerialID
         INNER JOIN tbl_serial_transaction_type ON tbl_serial_transaction.TypeID = tbl_serial_transaction_type.ID
    WHERE (tbl_serial.ID = P_SerialID))
   UNION ALL
   (SELECT
     NULL                             as TranID
    ,0                                as TranExists
    ,tbl_serial_transaction_type.ID   as TranTypeID
    ,tbl_serial_transaction_type.Name as TranType
    ,COALESCE(P_TranTime, Now())        as TransactionDatetime
    ,P_VendorID         as VendorID
    ,P_WarehouseID      as WarehouseID
    ,P_CustomerID       as CustomerID
    ,P_OrderID          as OrderID
    ,P_OrderDetailsID   as OrderDetailsID
    ,P_LotNumber        as LotNumber
    ,P_LastUpdateUserID as LastUpdateUserID
    ,Now()              as LastUpdateDatetime
    FROM tbl_serial_transaction_type
    WHERE (tbl_serial_transaction_type.Name = P_TranType))
   ORDER BY TranExists desc, TranID asc; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  IF (P_SerialID IS NOT NULL) THEN
    -- init / reinit
    SET V_Status            = 'Unknown'; --
    SET V_VendorID          = null; --
    SET V_WarehouseID       = null; --
    SET V_LotNumber         = null; --
    SET V_SoldDate          = null; --
    SET V_LastCustomerID    = null; --
    SET V_CurrentCustomerID = null; --
    SET V_LastUpdateUserID  = null; --

    OPEN cur; --

    REPEAT
      FETCH cur INTO
       cur_TranID
      ,cur_TranExists
      ,cur_TranTypeID
      ,cur_TranType
      ,cur_TranTime
      ,cur_VendorID
      ,cur_WarehouseID
      ,cur_CustomerID
      ,cur_OrderID
      ,cur_OrderDetailsID
      ,cur_LotNumber
      ,cur_LastUpdateUserID
      ,cur_LastUpdateDatetime; --

      IF (done = 0)
      AND (cur_TranTypeID IS NOT NULL)
      THEN
        SET V_AcceptableTran = 1; --

        IF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'Reserved') THEN
          -- ( 1, 'Reserved'                ), -- means that we added serial to not approved order
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          -- SET cur_CustomerID      = null; -- we need to know for whom did we reserved that serial
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Reserved'; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Reserved')) AND (cur_TranType = 'Reserve Cancelled') THEN
          -- ( 2, 'Reserve Cancelled'       ), -- means that we removed serial from not approved order
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand', 'Reserved') AND cur_TranType IN ('Rented', 'Sold'))
            OR (V_Status IN ('Rented') AND cur_TranType IN ('Sold')) THEN
          -- ( 3, 'Rented'                  ), -- means that RENT order was approved
          -- ( 4, 'Sold'                    ), -- means that SALE order or RENT-TO-PURCHASE order was approved
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          -- SET cur_CustomerID      = null; -- we need to know who rented or bought that serial
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = CASE WHEN cur_TranType = 'Sold' THEN cur_TranTime ELSE null END; --
          SET V_Status            = CASE cur_TranType WHEN 'Rented' THEN 'Rented' WHEN 'Sold' THEN 'Sold' ELSE null END; --
          SET V_WarehouseID       = null; --
          SET V_LastCustomerID    = V_CurrentCustomerID; --
          SET V_CurrentCustomerID = cur_CustomerID; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status != 'Maintenance') AND (cur_TranType IN ('Returned')) THEN
          -- ( 5, 'Returned'                ), -- means that user return RENTED serial
          --                                   -- but serial must be cleaned out prior to re-using
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          -- SET cur_CustomerID      = null; -- we need to know whom we returned that serial from
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Maintenance'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_LastCustomerID    = V_CurrentCustomerID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (cur_TranType IN ('Lost', 'Junked')) THEN
          -- ( 6, 'Lost'                    ), -- can be added only manually to mark serial as 'Lost'
          -- ( 7, 'Junked'                  ), -- can be added only manually to mark serial as 'Junked'
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; --
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = CASE cur_TranType WHEN 'Lost' THEN 'Lost' WHEN 'Junked' THEN 'Junked' ELSE null END; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Empty')) AND (cur_TranType = 'O2 Tank out for filling') THEN
          -- ( 8, 'O2 Tank out for filling' ), -- Send    : "Empty"   -> "Sent"
          -- SET cur_VendorID        = null; -- we need to know whom we sent that serial
          SET cur_WarehouseID     = null; --
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Sent'; --
          SET V_VendorID          = cur_VendorID; --
          SET V_WarehouseID       = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Sent')) AND (cur_TranType = 'O2 Tank in from filling') THEN
          -- ( 9, 'O2 Tank in from filling' ), -- Receive : "Sent"    -> "On Hand"
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          -- SET cur_LotNumber       = null; --we need to know lot number assigned

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_LotNumber         = cur_LotNumber; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'O2 Tank out to customer') THEN
          -- (10, 'O2 Tank out to customer' ), -- Rent    : "On Hand" -> "Rented"
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          -- SET cur_CustomerID      = null; -- we need to know whom we sent that serial
          -- SET cur_OrderID         = null; --
          -- SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Rented'; --
          SET V_WarehouseID       = null; --
          SET V_CurrentCustomerID = cur_CustomerID; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Rented')) AND (cur_TranType = 'O2 Tank in from customer') THEN
          -- (11, 'O2 Tank in from customer'), -- Pickup  : "Rented"  -> "Empty"
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; --
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Empty'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_LastCustomerID    = V_CurrentCustomerID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'Transferred Out') THEN
          -- (14, 'Transferred Out' ), -- Transferred Out  : "On Hand" -> "Transferred Out"
          SET cur_VendorID        = null; --
          SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; -- we need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Transferred Out'; --
          SET V_WarehouseID       = null; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Transferred Out')) AND (cur_TranType = 'Transferred In') THEN
          -- (15, 'Transferred In' ) -- Transferred In  : "Transferred Out" -> "On Hand"
          --                                            :       <NULL>      -> "On Hand"
          -- only way to assign warehouse to serial
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; -- we need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'On Hand')) AND (cur_TranType = 'Out for Maintenance') THEN
          -- (12, 'Out for Maintenance' ), -- Out for Maintenance  : "On Hand" -> "Maintenance"
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we sent that serial
          SET cur_CustomerID      = null; -- we do not need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'Maintenance'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSEIF (V_Status IN ('Unknown', 'Maintenance')) AND (cur_TranType = 'In from Maintenance') THEN
          -- (13, 'In from Maintenance' ) -- In from Maintenance  : "Maintenance" -> "On Hand"
          --                                                      :     <NULL>    -> "On Hand"
          -- another way to assign warehouse to serial
          SET cur_VendorID        = null; --
          -- SET cur_WarehouseID     = null; -- we need to know where we returned that serial
          SET cur_CustomerID      = null; -- we do not need to know whom we sent that serial
          SET cur_OrderID         = null; --
          SET cur_OrderDetailsID  = null; --
          SET cur_LotNumber       = null; --

          SET V_SoldDate          = null; --
          SET V_Status            = 'On Hand'; --
          SET V_WarehouseID       = cur_WarehouseID; --
          SET V_CurrentCustomerID = null; --
          SET V_LastUpdateUserID  = cur_LastUpdateUserID; --

        ELSE
          SET V_AcceptableTran = 0; --

        END IF; --

        IF (V_AcceptableTran = 1) AND (cur_TranExists = 0) THEN
          INSERT INTO tbl_serial_transaction SET
           TypeID              = cur_TranTypeID
          ,SerialID            = P_SerialID
          ,TransactionDatetime = cur_TranTime
          ,VendorID            = cur_VendorID
          ,WarehouseID         = cur_WarehouseID
          ,CustomerID          = cur_CustomerID
          ,OrderID             = cur_OrderID
          ,OrderDetailsID      = cur_OrderDetailsID
          ,LotNumber           = COALESCE(cur_LotNumber, '')
          ,LastUpdateDatetime  = COALESCE(cur_LastUpdateDatetime, Now())
          ,LastUpdateUserID    = COALESCE(cur_LastUpdateUserID, 1); -- root
        END IF; --

      END IF; --
    UNTIL done END REPEAT; --

    CLOSE cur; --

    -- save into db
    UPDATE tbl_serial SET
      Status            = CASE WHEN V_Status = 'Unknown' THEN 'On Hand' ELSE V_Status END
    , VendorID          = V_VendorID
    , WarehouseID       = V_WarehouseID
    , LotNumber         = COALESCE(V_LotNumber, '')
    , SoldDate          = V_SoldDate
    , CurrentCustomerID = V_CurrentCustomerID
    , LastCustomerID    = V_LastCustomerID
    , LastUpdateUserID  = COALESCE(V_LastUpdateUserID, 1) -- root
    WHERE (ID = P_SerialID); --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## serial_order_refresh

### Original MySQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_Priority         int(11); --
  DECLARE cur_CustomerID       int(11); --
  DECLARE cur_OrderID          int(11); --
  DECLARE cur_OrderDetailsID   int(11); --
  DECLARE cur_SerialID         int(11); --
  DECLARE cur_WarehouseID      int(11); --
  DECLARE cur_TranType         varchar(50); --
  DECLARE cur_TranTime         datetime; --

  DECLARE cur CURSOR FOR
    (
     SELECT
      1 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Reserved' as TranType
     ,IFNULL(tbl_order.OrderDate, Now()) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Reserved'
          LEFT JOIN tbl_serial_transaction as LastTran ON LastTran.CustomerID     = view_orderdetails.CustomerID
                                                      AND LastTran.OrderID        = view_orderdetails.OrderID
                                                      AND LastTran.OrderDetailsID = view_orderdetails.ID
                                                      AND LastTran.TypeID         = stt.ID
          LEFT JOIN (SELECT SerialID, Max(ID) as MaxID
                     FROM tbl_serial_transaction
                     GROUP BY SerialID) as TranHistory ON LastTran.SerialID = TranHistory.SerialID
                                                      AND LastTran.ID       = TranHistory.MaxID
     WHERE (tbl_order.Approved = 0) -- we reserve only for not approved orders
       AND ((LastTran.ID IS NULL) OR (TranHistory.SerialID IS NULL))
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT
      2 as Priority
     ,LastTran.CustomerID
     ,LastTran.OrderID
     ,LastTran.OrderDetailsID
     ,LastTran.SerialID
     ,null as WarehouseID
     ,'Reserve Cancelled' as TranType
     ,Now() as TranTime
     FROM (SELECT SerialID, Max(ID) as MaxID
           FROM tbl_serial_transaction
           GROUP BY SerialID) as TranHistory
          INNER JOIN tbl_serial_transaction as LastTran ON LastTran.SerialID = TranHistory.SerialID
                                                       AND LastTran.ID       = TranHistory.MaxID
          INNER JOIN tbl_serial_transaction_type ON tbl_serial_transaction_type.ID   = LastTran.TypeID
                                                AND tbl_serial_transaction_type.Name = 'Reserved'
          INNER JOIN tbl_serial ON TranHistory.SerialID = tbl_serial.ID
          LEFT JOIN view_orderdetails ON LastTran.CustomerID     =  view_orderdetails.CustomerID
                                     AND LastTran.OrderID        =  view_orderdetails.OrderID
                                     AND LastTran.OrderDetailsID =  view_orderdetails.ID
          LEFT JOIN tbl_order ON view_orderdetails.CustomerID = tbl_order.CustomerID
                             AND view_orderdetails.OrderID    = tbl_order.ID
     WHERE ((view_orderdetails.SerialID IS NULL) OR (view_orderdetails.SerialID != LastTran.SerialID))
       AND ((LastTran.OrderID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      3 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Rented' as TranType
     ,IFNULL(tbl_order.DeliveryDate, IFNULL(tbl_order.OrderDate, Now())) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Rented'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsRented = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      3 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Sold' as TranType
     ,IFNULL(tbl_order.DeliveryDate, IFNULL(tbl_order.OrderDate, Now())) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Sold'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsSold = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      4 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,view_orderdetails.WarehouseID
     ,'Returned' as TranType
     ,IFNULL(view_orderdetails.EndDate, Now()) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Returned'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsCanceled = 0)
       AND (view_orderdetails.IsPickedup = 1)
       AND (view_orderdetails.IsRented = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      5 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Sold' as TranType
     ,IFNULL(view_orderdetails.EndDate, Now()) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Sold'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsActive = 0)
       AND (view_orderdetails.IsCanceled = 0)
       AND (view_orderdetails.IsPickedup = 0)
       AND (view_orderdetails.IsRented = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) ORDER BY SerialID, Priority, TranTime; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
     cur_Priority
    ,cur_CustomerID
    ,cur_OrderID
    ,cur_OrderDetailsID
    ,cur_SerialID
    ,cur_WarehouseID
    ,cur_TranType
    ,cur_TranTime; --

    IF (done = 0) THEN
      CALL serial_add_transaction(
          cur_TranType       -- P_TranType         VARCHAR(50)
        , cur_TranTime       -- P_TranTime         DATETIME
        , cur_SerialID       -- P_SerialID         INT,
        , cur_WarehouseID    -- P_WarehouseID      INT,
        , null               -- P_VendorID         INT,
        , cur_CustomerID     -- P_CustomerID       INT,
        , cur_OrderID        -- P_OrderID          INT,
        , cur_OrderDetailsID -- P_OrderDetailsID   INT,
        , null               -- P_LotNumber        VARCHAR(50),
        , 1                  -- P_LastUpdateUserID INT
      ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE done INT DEFAULT 0; --

  -- cursor variables
  DECLARE cur_Priority         int(11); --
  DECLARE cur_CustomerID       int(11); --
  DECLARE cur_OrderID          int(11); --
  DECLARE cur_OrderDetailsID   int(11); --
  DECLARE cur_SerialID         int(11); --
  DECLARE cur_WarehouseID      int(11); --
  DECLARE cur_TranType         varchar(50); --
  DECLARE cur_TranTime         datetime; --

  DECLARE cur CURSOR FOR
    (
     SELECT
      1 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Reserved' as TranType
     ,COALESCE(tbl_order.OrderDate, Now()) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Reserved'
          LEFT JOIN tbl_serial_transaction as LastTran ON LastTran.CustomerID     = view_orderdetails.CustomerID
                                                      AND LastTran.OrderID        = view_orderdetails.OrderID
                                                      AND LastTran.OrderDetailsID = view_orderdetails.ID
                                                      AND LastTran.TypeID         = stt.ID
          LEFT JOIN (SELECT SerialID, Max(ID) as MaxID
                     FROM tbl_serial_transaction
                     GROUP BY SerialID) as TranHistory ON LastTran.SerialID = TranHistory.SerialID
                                                      AND LastTran.ID       = TranHistory.MaxID
     WHERE (tbl_order.Approved = 0) -- we reserve only for not approved orders
       AND ((LastTran.ID IS NULL) OR (TranHistory.SerialID IS NULL))
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT
      2 as Priority
     ,LastTran.CustomerID
     ,LastTran.OrderID
     ,LastTran.OrderDetailsID
     ,LastTran.SerialID
     ,null as WarehouseID
     ,'Reserve Cancelled' as TranType
     ,Now() as TranTime
     FROM (SELECT SerialID, Max(ID) as MaxID
           FROM tbl_serial_transaction
           GROUP BY SerialID) as TranHistory
          INNER JOIN tbl_serial_transaction as LastTran ON LastTran.SerialID = TranHistory.SerialID
                                                       AND LastTran.ID       = TranHistory.MaxID
          INNER JOIN tbl_serial_transaction_type ON tbl_serial_transaction_type.ID   = LastTran.TypeID
                                                AND tbl_serial_transaction_type.Name = 'Reserved'
          INNER JOIN tbl_serial ON TranHistory.SerialID = tbl_serial.ID
          LEFT JOIN view_orderdetails ON LastTran.CustomerID     =  view_orderdetails.CustomerID
                                     AND LastTran.OrderID        =  view_orderdetails.OrderID
                                     AND LastTran.OrderDetailsID =  view_orderdetails.ID
          LEFT JOIN tbl_order ON view_orderdetails.CustomerID = tbl_order.CustomerID
                             AND view_orderdetails.OrderID    = tbl_order.ID
     WHERE ((view_orderdetails.SerialID IS NULL) OR (view_orderdetails.SerialID != LastTran.SerialID))
       AND ((LastTran.OrderID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      3 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Rented' as TranType
     ,COALESCE(tbl_order.DeliveryDate, COALESCE(tbl_order.OrderDate, Now())) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Rented'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsRented = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      3 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Sold' as TranType
     ,COALESCE(tbl_order.DeliveryDate, COALESCE(tbl_order.OrderDate, Now())) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Sold'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsSold = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      4 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,view_orderdetails.WarehouseID
     ,'Returned' as TranType
     ,COALESCE(view_orderdetails.EndDate, Now()) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Returned'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsCanceled = 0)
       AND (view_orderdetails.IsPickedup = 1)
       AND (view_orderdetails.IsRented = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) UNION ALL (
     SELECT DISTINCT
      5 as Priority
     ,view_orderdetails.CustomerID
     ,view_orderdetails.OrderID
     ,view_orderdetails.ID as OrderDetailsID
     ,view_orderdetails.SerialID
     ,null as WarehouseID
     ,'Sold' as TranType
     ,COALESCE(view_orderdetails.EndDate, Now()) as TranTime
     FROM tbl_order
          INNER JOIN view_orderdetails ON view_orderdetails.CustomerID = tbl_order.CustomerID
                                      AND view_orderdetails.OrderID    = tbl_order.ID
          INNER JOIN tbl_serial ON view_orderdetails.SerialID        = tbl_serial.ID -- serial exists
                               AND view_orderdetails.InventoryItemID = tbl_serial.InventoryItemID
          INNER JOIN tbl_serial_transaction_type as stt ON stt.Name = 'Sold'
          LEFT JOIN tbl_serial_transaction as st ON st.CustomerID     = view_orderdetails.CustomerID
                                                AND st.OrderID        = view_orderdetails.OrderID
                                                AND st.OrderDetailsID = view_orderdetails.ID
                                                AND st.SerialID       = view_orderdetails.SerialID
                                                AND st.TypeID         = stt.ID
     WHERE (tbl_order.Approved = 1)
       AND (view_orderdetails.IsActive = 0)
       AND (view_orderdetails.IsCanceled = 0)
       AND (view_orderdetails.IsPickedup = 0)
       AND (view_orderdetails.IsRented = 1)
       AND (st.ID IS NULL)
       AND ((tbl_order.ID = P_OrderID) OR (P_OrderID IS NULL))
    ) ORDER BY SerialID, Priority, TranTime; --

  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1; --

  OPEN cur; --

  REPEAT
    FETCH cur INTO
     cur_Priority
    ,cur_CustomerID
    ,cur_OrderID
    ,cur_OrderDetailsID
    ,cur_SerialID
    ,cur_WarehouseID
    ,cur_TranType
    ,cur_TranTime; --

    IF (done = 0) THEN
      CALL serial_add_transaction(
          cur_TranType       -- P_TranType         VARCHAR(50)
        , cur_TranTime       -- P_TranTime         DATETIME
        , cur_SerialID       -- P_SerialID         INT,
        , cur_WarehouseID    -- P_WarehouseID      INT,
        , null               -- P_VendorID         INT,
        , cur_CustomerID     -- P_CustomerID       INT,
        , cur_OrderID        -- P_OrderID          INT,
        , cur_OrderDetailsID -- P_OrderDetailsID   INT,
        , null               -- P_LotNumber        VARCHAR(50),
        , 1                  -- P_LastUpdateUserID INT
      ); --
    END IF; --
  UNTIL done END REPEAT; --

  CLOSE cur; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

## serial_transfer

### Original MySQL Procedure
```sql
BEGIN
  DECLARE V_SerialID, V_InventoryItemID, V_CountBefore, V_CountAfter INT; --

  SELECT tbl_serial.ID, tbl_serial.InventoryItemID
  INTO V_SerialID, V_InventoryItemID
  FROM tbl_serial
  WHERE ID = P_SerialID; --

  IF (V_SerialID IS NOT NULL) THEN
    SELECT Count(*)
    INTO V_CountBefore
    FROM tbl_serial_transaction
    WHERE SerialID = V_SerialID; --

    CALL serial_add_transaction(
       'Transferred Out'  -- P_TranType         VARCHAR(50),
      ,Now()              -- P_TranTime         DATETIME,
      ,V_SerialID         -- P_SerialID         INT,
      ,P_SrcWarehouseID   -- P_WarehouseID      INT,
      ,null               -- P_VendorID         INT,
      ,null               -- P_CustomerID       INT,
      ,null               -- P_OrderID          INT,
      ,null               -- P_OrderDetailsID   INT,
      ,null               -- P_LotNumber        VARCHAR(50),
      ,P_LastUpdateUserID -- P_LastUpdateUserID INT
      ); --

    CALL serial_add_transaction(
       'Transferred In'   -- P_TranType         VARCHAR(50),
      ,Now()              -- P_TranTime         DATETIME,
      ,V_SerialID         -- P_SerialID         INT,
      ,P_DstWarehouseID   -- P_WarehouseID      INT,
      ,null               -- P_VendorID         INT,
      ,null               -- P_CustomerID       INT,
      ,null               -- P_OrderID          INT,
      ,null               -- P_OrderDetailsID   INT,
      ,null               -- P_LotNumber        VARCHAR(50),
      ,P_LastUpdateUserID -- P_LastUpdateUserID INT
      ); --

    SELECT Count(*)
    INTO V_CountAfter
    FROM tbl_serial_transaction
    WHERE SerialID = V_SerialID; --

    IF V_CountAfter - V_CountBefore = 2 THEN
      CALL internal_inventory_transfer(
        V_InventoryItemID
      , P_SrcWarehouseID
      , P_DstWarehouseID
      , 1
      , CONCAT('Serial #', V_SerialID, ' Transfer')
      , P_LastUpdateUserID); --
    END IF; --
  END IF; --
END
```

### Converted PostgreSQL Procedure
```sql
BEGIN
  DECLARE V_SerialID, V_InventoryItemID, V_CountBefore, V_CountAfter INT; --

  SELECT tbl_serial.ID, tbl_serial.InventoryItemID
  INTO V_SerialID, V_InventoryItemID
  FROM tbl_serial
  WHERE ID = P_SerialID; --

  IF (V_SerialID IS NOT NULL) THEN
    SELECT Count(*)
    INTO V_CountBefore
    FROM tbl_serial_transaction
    WHERE SerialID = V_SerialID; --

    CALL serial_add_transaction(
       'Transferred Out'  -- P_TranType         VARCHAR(50),
      ,Now()              -- P_TranTime         DATETIME,
      ,V_SerialID         -- P_SerialID         INT,
      ,P_SrcWarehouseID   -- P_WarehouseID      INT,
      ,null               -- P_VendorID         INT,
      ,null               -- P_CustomerID       INT,
      ,null               -- P_OrderID          INT,
      ,null               -- P_OrderDetailsID   INT,
      ,null               -- P_LotNumber        VARCHAR(50),
      ,P_LastUpdateUserID -- P_LastUpdateUserID INT
      ); --

    CALL serial_add_transaction(
       'Transferred In'   -- P_TranType         VARCHAR(50),
      ,Now()              -- P_TranTime         DATETIME,
      ,V_SerialID         -- P_SerialID         INT,
      ,P_DstWarehouseID   -- P_WarehouseID      INT,
      ,null               -- P_VendorID         INT,
      ,null               -- P_CustomerID       INT,
      ,null               -- P_OrderID          INT,
      ,null               -- P_OrderDetailsID   INT,
      ,null               -- P_LotNumber        VARCHAR(50),
      ,P_LastUpdateUserID -- P_LastUpdateUserID INT
      ); --

    SELECT Count(*)
    INTO V_CountAfter
    FROM tbl_serial_transaction
    WHERE SerialID = V_SerialID; --

    IF V_CountAfter - V_CountBefore = 2 THEN
      CALL internal_inventory_transfer(
        V_InventoryItemID
      , P_SrcWarehouseID
      , P_DstWarehouseID
      , 1
      , CONCAT('Serial #', V_SerialID, ' Transfer')
      , P_LastUpdateUserID); --
    END IF; --
  END IF; --
END
```

### Metadata
- Deterministic: NO
- Return Type: 
- Security Type: DEFINER
- Data Access: CONTAINS SQL

