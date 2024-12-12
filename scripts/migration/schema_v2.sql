-- Drop existing tables if they exist
DROP TABLE IF EXISTS backup_tbl_authorizationtype_lastupdatedatetime CASCADE;
DROP TABLE IF EXISTS bak_bac_bac_566 CASCADE;
DROP TABLE IF EXISTS bak_bac_bac_908 CASCADE;
DROP TABLE IF EXISTS bak_bac_bac_997 CASCADE;
DROP TABLE IF EXISTS bak_tbl_dat_244 CASCADE;
DROP TABLE IF EXISTS bak_tbl_dat_594 CASCADE;
DROP TABLE IF EXISTS bak_tbl_del_949 CASCADE;
DROP TABLE IF EXISTS bak_tbl_dos_292 CASCADE;
DROP TABLE IF EXISTS bak_tbl_dos_663 CASCADE;
DROP TABLE IF EXISTS bak_tbl_set_665 CASCADE;
DROP TABLE IF EXISTS tbl_customer CASCADE;
DROP TABLE IF EXISTS tbl_orderdetails CASCADE;
DROP TABLE IF EXISTS tbl_inventory CASCADE;
DROP TABLE IF EXISTS tbl_vendor CASCADE;
DROP TABLE IF EXISTS tbl_insurancecompany CASCADE;
DROP TABLE IF EXISTS tbl_invoice_transactiontype CASCADE;
DROP TABLE IF EXISTS tbl_predefinedtext CASCADE;
DROP TABLE IF EXISTS tbl_authorizationtype CASCADE;
DROP TABLE IF EXISTS tbl_batchpayment CASCADE;
DROP TABLE IF EXISTS tbl_billingtype CASCADE;
DROP TABLE IF EXISTS tbl_changes CASCADE;
DROP TABLE IF EXISTS tbl_cmnform CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0102a CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0102b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0203a CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0203b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0302 CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0403b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0403c CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0404b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0404c CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0602b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0603b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0702a CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0702b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0703a CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0802 CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0902 CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_0903 CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_1002a CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_1002b CASCADE;
DROP TABLE IF EXISTS tbl_cmnform_1003 CASCADE;
DROP TABLE IF EXISTS view_invoicetransaction_statistics CASCADE;

-- Fixing tables with proper syntax, defaults, and constraints

-- Existing backup tables
CREATE TABLE backup_tbl_authorizationtype_lastupdatedatetime (
    backup_id SERIAL PRIMARY KEY,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bak_bac_bac_566 (
    backup_id INTEGER NOT NULL DEFAULT 0,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bak_bac_bac_908 (
    backup_id INTEGER NOT NULL DEFAULT 0,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bak_bac_bac_997 (
    backup_id INTEGER NOT NULL DEFAULT 0,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bak_tbl_dat_244 (
    backup_id SERIAL PRIMARY KEY,
    id INTEGER NOT NULL DEFAULT 0,
    complianceid INTEGER NOT NULL DEFAULT 0,
    date DATE DEFAULT NULL,
    done SMALLINT NOT NULL DEFAULT 0,
    notes TEXT NOT NULL,
    createdbyuserid SMALLINT,
    assignedtouserid SMALLINT,
    lastupdateuserid SMALLINT,
    lastupdatedatetime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bak_tbl_dat_594 (
    backup_id SERIAL PRIMARY KEY,
    id INTEGER NOT NULL DEFAULT 0,
    inventoryitemid INTEGER NOT NULL DEFAULT 0,
    warehouseid INTEGER NOT NULL DEFAULT 0,
    typeid INTEGER NOT NULL DEFAULT 0,
    date DATE DEFAULT NULL,
    quantity DOUBLE PRECISION,
    cost DECIMAL,
    description VARCHAR(30),
    serialid INTEGER,
    vendorid INTEGER,
    customerid INTEGER,
    lastupdateuserid SMALLINT,
    lastupdatedatetime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    purchaseorderid INTEGER,
    purchaseorderdetailsid INTEGER,
    invoiceid INTEGER,
    manufacturerid INTEGER,
    orderdetailsid INTEGER,
    orderid INTEGER,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Additional backup tables
CREATE TABLE bak_tbl_del_949 (
    backup_id SERIAL PRIMARY KEY,
    id INTEGER NOT NULL DEFAULT 0,
    customerid INTEGER NOT NULL DEFAULT 0,
    orderid INTEGER,
    deliverydate DATE DEFAULT NULL,
    lastupdateuserid SMALLINT,
    lastupdatedatetime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bak_tbl_dos_292 (
    backup_id SERIAL PRIMARY KEY,
    id INTEGER NOT NULL DEFAULT 0,
    orderid INTEGER NOT NULL DEFAULT 0,
    customerid INTEGER NOT NULL DEFAULT 0,
    serialnumber VARCHAR(50),
    inventoryitemid INTEGER NOT NULL DEFAULT 0,
    pricecodeid INTEGER NOT NULL DEFAULT 0,
    salerenttype VARCHAR(50) NOT NULL DEFAULT 'Monthly Rental',
    serialid INTEGER,
    billableprice NUMERIC(18,2) NOT NULL DEFAULT 0.00,
    allowableprice NUMERIC(18,2) NOT NULL DEFAULT 0.00,
    taxable SMALLINT NOT NULL DEFAULT 0,
    dosfrom DATE DEFAULT NULL,
    orderedwhen VARCHAR(50) NOT NULL DEFAULT 'One time',
    billedwhen VARCHAR(50) NOT NULL DEFAULT 'One time',
    billingmonth INTEGER NOT NULL DEFAULT 1,
    billitemon VARCHAR(50) NOT NULL DEFAULT 'Day of Delivery',
    state VARCHAR(50) NOT NULL DEFAULT 'New',
    backup_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CHECK (salerenttype IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 
                         'capped rental', 'parental capped rental', 'rent to purchase', 
                         'one time sale', 're-occurring sale')),
    CHECK (orderedwhen IN ('one time', 'daily', 'weekly', 'monthly', 
                        'quarterly', 'semi-annually', 'annually')),
    CHECK (billedwhen IN ('one time', 'daily', 'weekly', 'monthly', 
                       'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom')),
    CHECK (billitemon IN ('day of delivery', 'last day of the month', 
                       'last day of the period', 'day of pick-up')),
    CHECK (state IN ('new', 'approved', 'pickup', 'closed', 'canceled'))
);

-- Core business tables
CREATE TABLE tbl_customer (
    id SERIAL PRIMARY KEY,
    accountnumber VARCHAR(40) NOT NULL,
    name VARCHAR(100),
    address TEXT,
    phone VARCHAR(20),
    email VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_invoice (
    id SERIAL PRIMARY KEY,
    customerid INTEGER REFERENCES tbl_customer(id),
    invoice_number VARCHAR(50) NOT NULL,
    invoice_date DATE NOT NULL,
    due_date DATE,
    total_amount NUMERIC(10,2),
    status VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_invoice_transactiontype (
    id INTEGER NOT NULL DEFAULT 0 PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    description TEXT,
    amount NUMERIC(10,2),
    transaction_date TIMESTAMP,
    invoice_id INTEGER REFERENCES tbl_invoice(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_orderdetails (
    id SERIAL PRIMARY KEY,
    orderid INTEGER NOT NULL DEFAULT 0,
    customerid INTEGER NOT NULL DEFAULT 0,
    serialnumber VARCHAR(50),
    inventoryitemid INTEGER NOT NULL DEFAULT 0,
    pricecodeid INTEGER NOT NULL DEFAULT 0,
    salerenttype VARCHAR(50) NOT NULL DEFAULT 'Monthly Rental',
    serialid INTEGER,
    billableprice NUMERIC(18, 2) NOT NULL DEFAULT 0.00,
    allowableprice NUMERIC(18, 2) NOT NULL DEFAULT 0.00,
    taxable SMALLINT NOT NULL DEFAULT 0,
    dosfrom DATE DEFAULT NULL,
    orderedwhen VARCHAR(50) NOT NULL DEFAULT 'One time',
    billedwhen VARCHAR(50) NOT NULL DEFAULT 'One time',
    billingmonth INTEGER NOT NULL DEFAULT 1,
    billitemon VARCHAR(50) NOT NULL DEFAULT 'Day of Delivery',
    state VARCHAR(50) NOT NULL DEFAULT 'New',
    CHECK (salerenttype IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 
                         'capped rental', 'parental capped rental', 'rent to purchase', 
                         'one time sale', 're-occurring sale')),
    CHECK (orderedwhen IN ('one time', 'daily', 'weekly', 'monthly', 
                        'quarterly', 'semi-annually', 'annually')),
    CHECK (billedwhen IN ('one time', 'daily', 'weekly', 'monthly', 
                       'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom')),
    CHECK (billitemon IN ('day of delivery', 'last day of the month', 
                       'last day of the period', 'day of pick-up')),
    CHECK (state IN ('new', 'approved', 'pickup', 'closed', 'canceled'))
);

CREATE TABLE tbl_inventory (
    warehouseid INTEGER NOT NULL DEFAULT 0,
    inventoryitemid INTEGER NOT NULL DEFAULT 0,
    onhand DOUBLE PRECISION NOT NULL DEFAULT 0,
    committed DOUBLE PRECISION NOT NULL DEFAULT 0,
    onorder DOUBLE PRECISION NOT NULL DEFAULT 0,
    unavailable DOUBLE PRECISION NOT NULL DEFAULT 0,
    rented DOUBLE PRECISION NOT NULL DEFAULT 0,
    sold DOUBLE PRECISION NOT NULL DEFAULT 0,
    backordered DOUBLE PRECISION NOT NULL DEFAULT 0,
    reorderpoint DOUBLE PRECISION NOT NULL DEFAULT 0,
    costperunit NUMERIC(18, 2) NOT NULL DEFAULT 0.00,
    totalcost NUMERIC(18, 2) NOT NULL DEFAULT 0.00,
    lastupdateuserid SMALLINT,
    lastupdatedatetime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (warehouseid, inventoryitemid)
);

CREATE TABLE tbl_vendor (
    id SERIAL PRIMARY KEY,
    accountnumber VARCHAR(40) NOT NULL,
    name VARCHAR(50) NOT NULL,
    contact VARCHAR(50),
    phone VARCHAR(50),
    city VARCHAR(25),
    state CHAR(2),
    zip VARCHAR(10),
    lastupdatedatetime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_insurancecompany (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    basis VARCHAR(15) NOT NULL DEFAULT 'Bill',
    city VARCHAR(25),
    state CHAR(2),
    zip VARCHAR(10),
    CHECK (basis IN ('bill', 'allowed'))
);

CREATE TABLE tbl_predefinedtext (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL DEFAULT '',
    type VARCHAR(50) NOT NULL DEFAULT 'Document Text',
    text TEXT NOT NULL
);

-- CMN Form Tables
CREATE TABLE tbl_cmnform (
    id SERIAL PRIMARY KEY,
    form_id VARCHAR(50) NOT NULL,
    form_name VARCHAR(100),
    form_description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT true
);

CREATE TABLE tbl_cmnform_0102a (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    provider_id INTEGER,
    diagnosis_code VARCHAR(20),
    treatment_date DATE,
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0102b (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    equipment_type VARCHAR(100),
    rental_period INTEGER,
    justification TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0203a (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    assessment_date DATE,
    assessment_type VARCHAR(50),
    assessment_result TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0203b (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    treatment_plan TEXT,
    expected_duration INTEGER,
    follow_up_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0302 (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    equipment_category VARCHAR(100),
    medical_necessity TEXT,
    prescription_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0403b (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    oxygen_flow_rate NUMERIC(5,2),
    oxygen_frequency INTEGER,
    testing_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0403c (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    oxygen_saturation NUMERIC(5,2),
    test_condition VARCHAR(50),
    physician_review_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0404b (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    wheelchair_type VARCHAR(100),
    mobility_assessment TEXT,
    assessment_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_cmnform_0404c (
    id SERIAL PRIMARY KEY,
    form_id INTEGER REFERENCES tbl_cmnform(id),
    patient_id INTEGER,
    wheelchair_accessories TEXT,
    justification TEXT,
    approval_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Additional Business Tables
CREATE TABLE tbl_authorizationtype (
    id SERIAL PRIMARY KEY,
    auth_name VARCHAR(100) NOT NULL,
    auth_description TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_batchpayment (
    id SERIAL PRIMARY KEY,
    batch_number VARCHAR(50) NOT NULL,
    payment_date DATE,
    total_amount NUMERIC(10,2),
    payment_method VARCHAR(50),
    status VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_billingtype (
    id SERIAL PRIMARY KEY,
    billing_name VARCHAR(100) NOT NULL,
    billing_code VARCHAR(20),
    description TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_changes (
    id SERIAL PRIMARY KEY,
    table_name VARCHAR(100) NOT NULL,
    record_id INTEGER,
    change_type VARCHAR(20),
    changed_by INTEGER,
    change_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    old_value TEXT,
    new_value TEXT
);

-- Document Management Tables
CREATE TABLE tbl_document (
    id SERIAL PRIMARY KEY,
    document_type VARCHAR(50) NOT NULL,
    file_name VARCHAR(255) NOT NULL,
    file_path TEXT,
    file_size BIGINT,
    mime_type VARCHAR(100),
    uploaded_by INTEGER,
    upload_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_archived BOOLEAN DEFAULT false,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_document_metadata (
    id SERIAL PRIMARY KEY,
    document_id INTEGER REFERENCES tbl_document(id),
    metadata_key VARCHAR(100) NOT NULL,
    metadata_value TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Settings and Configuration Tables
CREATE TABLE tbl_settings (
    id SERIAL PRIMARY KEY,
    setting_key VARCHAR(100) NOT NULL UNIQUE,
    setting_value TEXT,
    setting_group VARCHAR(50),
    is_system BOOLEAN DEFAULT false,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_configuration (
    id SERIAL PRIMARY KEY,
    config_key VARCHAR(100) NOT NULL UNIQUE,
    config_value TEXT,
    description TEXT,
    is_encrypted BOOLEAN DEFAULT false,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Audit and Logging Tables
CREATE TABLE tbl_audit_log (
    id SERIAL PRIMARY KEY,
    user_id INTEGER,
    action VARCHAR(50) NOT NULL,
    table_name VARCHAR(100),
    record_id INTEGER,
    old_value TEXT,
    new_value TEXT,
    ip_address VARCHAR(45),
    user_agent TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_system_log (
    id SERIAL PRIMARY KEY,
    log_level VARCHAR(20) NOT NULL,
    log_message TEXT NOT NULL,
    source VARCHAR(100),
    exception_details TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Additional Business Tables
CREATE TABLE tbl_notification (
    id SERIAL PRIMARY KEY,
    user_id INTEGER,
    notification_type VARCHAR(50) NOT NULL,
    title VARCHAR(200),
    message TEXT,
    is_read BOOLEAN DEFAULT false,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    read_at TIMESTAMP
);

CREATE TABLE tbl_workflow (
    id SERIAL PRIMARY KEY,
    workflow_name VARCHAR(100) NOT NULL,
    workflow_type VARCHAR(50),
    status VARCHAR(20),
    current_step INTEGER,
    total_steps INTEGER,
    created_by INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_workflow_step (
    id SERIAL PRIMARY KEY,
    workflow_id INTEGER REFERENCES tbl_workflow(id),
    step_number INTEGER NOT NULL,
    step_name VARCHAR(100) NOT NULL,
    step_type VARCHAR(50),
    step_data JSON,
    status VARCHAR(20),
    completed_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Medical Equipment Tables
CREATE TABLE tbl_equipment_category (
    id SERIAL PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL,
    parent_category_id INTEGER REFERENCES tbl_equipment_category(id),
    description TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_equipment_model (
    id SERIAL PRIMARY KEY,
    category_id INTEGER REFERENCES tbl_equipment_category(id),
    model_name VARCHAR(100) NOT NULL,
    manufacturer VARCHAR(100),
    specifications JSON,
    warranty_period INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_equipment_maintenance (
    id SERIAL PRIMARY KEY,
    equipment_id INTEGER,
    maintenance_type VARCHAR(50) NOT NULL,
    scheduled_date DATE,
    completed_date DATE,
    technician_id INTEGER,
    notes TEXT,
    status VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Insurance and Claims Tables
CREATE TABLE tbl_insurance_plan (
    id SERIAL PRIMARY KEY,
    insurance_company_id INTEGER REFERENCES tbl_insurancecompany(id),
    plan_name VARCHAR(100) NOT NULL,
    plan_type VARCHAR(50),
    coverage_details JSON,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_insurance_claim (
    id SERIAL PRIMARY KEY,
    patient_id INTEGER,
    insurance_plan_id INTEGER REFERENCES tbl_insurance_plan(id),
    claim_number VARCHAR(50) UNIQUE,
    service_date DATE,
    diagnosis_codes TEXT[],
    procedure_codes TEXT[],
    claim_amount NUMERIC(10,2),
    status VARCHAR(20),
    submitted_date TIMESTAMP,
    processed_date TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_claim_history (
    id SERIAL PRIMARY KEY,
    claim_id INTEGER REFERENCES tbl_insurance_claim(id),
    status_change VARCHAR(20),
    notes TEXT,
    changed_by INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Patient Care Tables
CREATE TABLE tbl_patient_assessment (
    id SERIAL PRIMARY KEY,
    patient_id INTEGER,
    assessment_type VARCHAR(50) NOT NULL,
    assessment_date DATE,
    assessed_by INTEGER,
    findings TEXT,
    recommendations TEXT,
    follow_up_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_care_plan (
    id SERIAL PRIMARY KEY,
    patient_id INTEGER,
    plan_type VARCHAR(50) NOT NULL,
    start_date DATE,
    end_date DATE,
    goals TEXT[],
    interventions TEXT[],
    status VARCHAR(20),
    created_by INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create view for invoice transaction statistics
CREATE OR REPLACE VIEW view_invoicetransaction_statistics AS
SELECT 
    i.customerid,
    COUNT(t.id) as total_transactions,
    SUM(t.amount) as total_amount,
    AVG(t.amount) as avg_amount,
    MAX(t.transaction_date) as last_transaction_date
FROM 
    tbl_invoice i
    JOIN tbl_invoice_transactiontype t ON i.id = t.invoice_id
GROUP BY 
    i.customerid;

-- Adding indices for commonly queried fields
CREATE INDEX idx_tbl_customer_accountnumber ON tbl_customer (accountnumber);
CREATE INDEX idx_tbl_orderdetails_orderid ON tbl_orderdetails (orderid, customerid);

-- Adding indices for CMN form tables
CREATE INDEX idx_tbl_cmnform_form_id ON tbl_cmnform (form_id);
CREATE INDEX idx_tbl_cmnform_0102a_patient ON tbl_cmnform_0102a (patient_id);
CREATE INDEX idx_tbl_cmnform_0102b_patient ON tbl_cmnform_0102b (patient_id);
CREATE INDEX idx_tbl_cmnform_0203a_patient ON tbl_cmnform_0203a (patient_id);
CREATE INDEX idx_tbl_cmnform_0203b_patient ON tbl_cmnform_0203b (patient_id);
CREATE INDEX idx_tbl_cmnform_0302_patient ON tbl_cmnform_0302 (patient_id);
CREATE INDEX idx_tbl_cmnform_0403b_patient ON tbl_cmnform_0403b (patient_id);
CREATE INDEX idx_tbl_cmnform_0403c_patient ON tbl_cmnform_0403c (patient_id);
CREATE INDEX idx_tbl_cmnform_0404b_patient ON tbl_cmnform_0404b (patient_id);
CREATE INDEX idx_tbl_cmnform_0404c_patient ON tbl_cmnform_0404c (patient_id);

-- Adding indices for new tables
CREATE INDEX idx_tbl_document_type ON tbl_document (document_type);
CREATE INDEX idx_tbl_document_metadata_docid ON tbl_document_metadata (document_id);
CREATE INDEX idx_tbl_settings_key ON tbl_settings (setting_key);
CREATE INDEX idx_tbl_configuration_key ON tbl_configuration (config_key);
CREATE INDEX idx_tbl_audit_log_userid ON tbl_audit_log (user_id);
CREATE INDEX idx_tbl_notification_userid ON tbl_notification (user_id);
CREATE INDEX idx_tbl_workflow_status ON tbl_workflow (status);
CREATE INDEX idx_tbl_workflow_step_workflow ON tbl_workflow_step (workflow_id);

-- Adding indices for medical equipment and insurance tables
CREATE INDEX idx_tbl_equipment_category_parent ON tbl_equipment_category (parent_category_id);
CREATE INDEX idx_tbl_equipment_model_category ON tbl_equipment_model (category_id);
CREATE INDEX idx_tbl_equipment_maintenance_equipment ON tbl_equipment_maintenance (equipment_id);
CREATE INDEX idx_tbl_insurance_plan_company ON tbl_insurance_plan (insurance_company_id);
CREATE INDEX idx_tbl_insurance_claim_patient ON tbl_insurance_claim (patient_id);
CREATE INDEX idx_tbl_insurance_claim_plan ON tbl_insurance_claim (insurance_plan_id);
CREATE INDEX idx_tbl_claim_history_claim ON tbl_claim_history (claim_id);
CREATE INDEX idx_tbl_patient_assessment_patient ON tbl_patient_assessment (patient_id);
CREATE INDEX idx_tbl_care_plan_patient ON tbl_care_plan (patient_id);

-- Scheduling Tables
CREATE TABLE tbl_appointment (
    id SERIAL PRIMARY KEY,
    patient_id INTEGER,
    provider_id INTEGER,
    appointment_type VARCHAR(50) NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    status VARCHAR(20),
    notes TEXT,
    location VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_schedule_template (
    id SERIAL PRIMARY KEY,
    template_name VARCHAR(100) NOT NULL,
    provider_id INTEGER,
    day_of_week INTEGER CHECK (day_of_week BETWEEN 0 AND 6),
    start_time TIME,
    end_time TIME,
    slot_duration INTEGER,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Reporting Tables
CREATE TABLE tbl_report_template (
    id SERIAL PRIMARY KEY,
    report_name VARCHAR(100) NOT NULL,
    description TEXT,
    query_template TEXT,
    parameters JSON,
    created_by INTEGER,
    is_system BOOLEAN DEFAULT false,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_report_schedule (
    id SERIAL PRIMARY KEY,
    template_id INTEGER REFERENCES tbl_report_template(id),
    schedule_type VARCHAR(20),
    schedule_config JSON,
    recipient_emails TEXT[],
    is_active BOOLEAN DEFAULT true,
    last_run_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_report_execution (
    id SERIAL PRIMARY KEY,
    template_id INTEGER REFERENCES tbl_report_template(id),
    schedule_id INTEGER REFERENCES tbl_report_schedule(id),
    parameters JSON,
    status VARCHAR(20),
    start_time TIMESTAMP,
    end_time TIMESTAMP,
    output_location TEXT,
    error_message TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- System Management Tables
CREATE TABLE tbl_job_queue (
    id SERIAL PRIMARY KEY,
    job_type VARCHAR(50) NOT NULL,
    priority INTEGER DEFAULT 0,
    payload JSON,
    status VARCHAR(20),
    attempts INTEGER DEFAULT 0,
    max_attempts INTEGER DEFAULT 3,
    next_attempt_at TIMESTAMP,
    last_error TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_system_metrics (
    id SERIAL PRIMARY KEY,
    metric_name VARCHAR(100) NOT NULL,
    metric_value NUMERIC,
    dimensions JSON,
    collected_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_api_key (
    id SERIAL PRIMARY KEY,
    key_name VARCHAR(100) NOT NULL,
    api_key VARCHAR(255) NOT NULL,
    created_by INTEGER,
    expires_at TIMESTAMP,
    last_used_at TIMESTAMP,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Reference Tables
CREATE TABLE tbl_zip_codes (
    id SERIAL PRIMARY KEY,
    zip_code VARCHAR(10) NOT NULL,
    city VARCHAR(100),
    state VARCHAR(2),
    latitude NUMERIC(9,6),
    longitude NUMERIC(9,6),
    timezone VARCHAR(50),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_holidays (
    id SERIAL PRIMARY KEY,
    holiday_date DATE NOT NULL,
    holiday_name VARCHAR(100) NOT NULL,
    description TEXT,
    is_federal BOOLEAN DEFAULT false,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Adding indices for final tables
CREATE INDEX idx_tbl_appointment_patient ON tbl_appointment (patient_id);
CREATE INDEX idx_tbl_appointment_provider ON tbl_appointment (provider_id);
CREATE INDEX idx_tbl_appointment_datetime ON tbl_appointment (start_time, end_time);
CREATE INDEX idx_tbl_schedule_template_provider ON tbl_schedule_template (provider_id, day_of_week);
CREATE INDEX idx_tbl_report_schedule_template ON tbl_report_schedule (template_id);
CREATE INDEX idx_tbl_report_execution_template ON tbl_report_execution (template_id);
CREATE INDEX idx_tbl_job_queue_status ON tbl_job_queue (status, next_attempt_at);
CREATE INDEX idx_tbl_system_metrics_name ON tbl_system_metrics (metric_name, collected_at);
CREATE INDEX idx_tbl_api_key_name ON tbl_api_key (key_name);
CREATE INDEX idx_tbl_zip_codes_zip ON tbl_zip_codes (zip_code);
CREATE INDEX idx_tbl_holidays_date ON tbl_holidays (holiday_date);

-- Add foreign key constraints
ALTER TABLE tbl_appointment 
    ADD CONSTRAINT fk_appointment_patient FOREIGN KEY (patient_id) REFERENCES tbl_customer(id),
    ADD CONSTRAINT fk_appointment_provider FOREIGN KEY (provider_id) REFERENCES tbl_customer(id);

ALTER TABLE tbl_schedule_template
    ADD CONSTRAINT fk_schedule_provider FOREIGN KEY (provider_id) REFERENCES tbl_customer(id);

ALTER TABLE tbl_report_template
    ADD CONSTRAINT fk_report_creator FOREIGN KEY (created_by) REFERENCES tbl_customer(id);

ALTER TABLE tbl_api_key
    ADD CONSTRAINT fk_apikey_creator FOREIGN KEY (created_by) REFERENCES tbl_customer(id);

-- Create materialized view for common reporting queries
CREATE MATERIALIZED VIEW mv_daily_metrics AS
SELECT 
    DATE(created_at) as metric_date,
    COUNT(DISTINCT patient_id) as total_patients,
    COUNT(DISTINCT provider_id) as total_providers,
    COUNT(*) as total_appointments,
    COUNT(CASE WHEN status = 'completed' THEN 1 END) as completed_appointments
FROM tbl_appointment
GROUP BY DATE(created_at)
WITH DATA;

CREATE INDEX idx_mv_daily_metrics_date ON mv_daily_metrics (metric_date);

-- Create function to refresh materialized view
CREATE OR REPLACE FUNCTION refresh_daily_metrics()
    RETURNS void
    LANGUAGE plpgsql
AS $$
BEGIN
    REFRESH MATERIALIZED VIEW CONCURRENTLY mv_daily_metrics;
END;
$$;
