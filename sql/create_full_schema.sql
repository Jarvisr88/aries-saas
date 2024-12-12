-- PostgreSQL Full Schema Creation Script
-- Based on original MySQL database with 119 tables

-- Enable required extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS "btree_gist";

-- Create schemas for different modules
CREATE SCHEMA IF NOT EXISTS orders;
CREATE SCHEMA IF NOT EXISTS equipment;
CREATE SCHEMA IF NOT EXISTS medical;
CREATE SCHEMA IF NOT EXISTS billing;
CREATE SCHEMA IF NOT EXISTS users;

-- Common Types and Enums
CREATE TYPE sale_rent_type AS ENUM ('sale', 'rent', 'both');
CREATE TYPE billing_cycle AS ENUM ('monthly', 'weekly', 'daily', 'one_time');
CREATE TYPE order_status AS ENUM ('pending', 'approved', 'voided', 'completed');
CREATE TYPE cmn_form_type AS ENUM (
    'dmerc 01.02a', 'dmerc 01.02b', 'dmerc 02.03a', 'dmerc 02.03b',
    'dmerc 03.02', 'dmerc 04.03b', 'dmerc 04.03c', 'dmerc 06.02b',
    'dmerc 07.02a', 'dmerc 07.02b', 'dmerc 08.02', 'dmerc 09.02',
    'dmerc 10.02a', 'dmerc 10.02b', 'dmerc 484.2', 'dmerc drorder',
    'dmerc uro', 'dme 04.04b', 'dme 04.04c', 'dme 06.03b',
    'dme 07.03a', 'dme 09.03', 'dme 10.03', 'dme 484.03'
);

-- Audit timestamp function
CREATE OR REPLACE FUNCTION update_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.last_update_datetime = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ language 'plpgsql';

------------------------------------------
-- Authorization Tables
------------------------------------------

CREATE TABLE billing.authorization_type (
    id SERIAL PRIMARY KEY,
    name varchar(50) NOT NULL,
    last_update_user_id integer,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE billing.batch_payment (
    id SERIAL PRIMARY KEY,
    insurance_company_id integer NOT NULL,
    check_number varchar(14) NOT NULL,
    check_date date NOT NULL,
    check_amount decimal(18,2) NOT NULL,
    amount_used decimal(18,2) NOT NULL,
    last_update_user_id integer NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

------------------------------------------
-- Medical Documentation Tables
------------------------------------------

CREATE TABLE medical.cmn_form (
    id SERIAL PRIMARY KEY,
    cmn_type cmn_form_type NOT NULL,
    initial_date date,
    revised_date date,
    recertification_date date,
    customer_id integer,
    customer_icd9_1 varchar(8),
    customer_icd9_2 varchar(8),
    customer_icd9_3 varchar(8),
    customer_icd9_4 varchar(8),
    doctor_id integer,
    pos_type_id integer,
    facility_id integer,
    answering_name varchar(50) NOT NULL,
    answering_title varchar(50) NOT NULL,
    answering_employer varchar(50) NOT NULL,
    estimated_length_of_need integer NOT NULL DEFAULT 0,
    signature_name varchar(50) NOT NULL,
    signature_date date,
    on_file smallint NOT NULL DEFAULT 0,
    order_id integer,
    last_update_user_id integer,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    customer_using_icd10 smallint NOT NULL DEFAULT 0
);

-- CMN Form Type Tables
CREATE TABLE medical.cmn_form_0102a (
    cmn_form_id integer PRIMARY KEY REFERENCES medical.cmn_form(id),
    answer1 char(1) CHECK (answer1 IN ('Y','N','D')) NOT NULL DEFAULT 'D',
    answer3 char(1) CHECK (answer3 IN ('Y','N','D')) NOT NULL DEFAULT 'D',
    answer4 char(1) CHECK (answer4 IN ('Y','N','D')) NOT NULL DEFAULT 'D',
    answer5 char(1) CHECK (answer5 IN ('Y','N','D')) NOT NULL DEFAULT 'D',
    answer6 char(1) CHECK (answer6 IN ('Y','N','D')) NOT NULL DEFAULT 'D',
    answer7 char(1) CHECK (answer7 IN ('Y','N','D')) NOT NULL DEFAULT 'D'
);

-- Add more CMN form type tables here...

------------------------------------------
-- Customer and Order Tables
------------------------------------------

CREATE TABLE orders.customer (
    id SERIAL PRIMARY KEY,
    account_number varchar(40) NOT NULL,
    first_name varchar(50) NOT NULL,
    last_name varchar(50) NOT NULL,
    middle_name varchar(50),
    address1 varchar(40) NOT NULL,
    address2 varchar(40),
    city varchar(25) NOT NULL,
    state char(2) NOT NULL,
    zip varchar(10) NOT NULL,
    phone varchar(20),
    date_of_birth date,
    ssn varchar(11),
    gender char(1) CHECK (gender IN ('M','F')),
    billing_type_id integer,
    doctor1_id integer,
    doctor2_id integer,
    facility_id integer,
    inactive_date date,
    last_update_user_id integer,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE orders.order (
    id SERIAL PRIMARY KEY,
    customer_id integer NOT NULL REFERENCES orders.customer(id),
    approved smallint NOT NULL DEFAULT 0,
    retail_sales smallint NOT NULL DEFAULT 0,
    delivery_date date,
    pickup_date date,
    facility_id integer,
    pos_type_id integer,
    customer_insurance1_id integer,
    customer_insurance2_id integer,
    customer_insurance3_id integer,
    customer_insurance4_id integer,
    last_update_user_id integer NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status order_status NOT NULL DEFAULT 'pending'
);

-- Continue with all other tables...
-- This is just a sample of the full schema
-- The complete script would include all 119 tables

-- Add indexes
CREATE INDEX idx_customer_name ON orders.customer(last_name, first_name);
CREATE INDEX idx_customer_ssn ON orders.customer(ssn);
CREATE INDEX idx_order_customer ON orders.order(customer_id);
CREATE INDEX idx_cmn_form_customer ON medical.cmn_form(customer_id);

-- Add triggers for timestamp updates
CREATE TRIGGER update_customer_timestamp 
    BEFORE UPDATE ON orders.customer
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

CREATE TRIGGER update_order_timestamp 
    BEFORE UPDATE ON orders.order
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

-- Add comments
COMMENT ON SCHEMA orders IS 'Order management module';
COMMENT ON SCHEMA medical IS 'Medical documentation module';
COMMENT ON SCHEMA billing IS 'Billing and payment module';
COMMENT ON SCHEMA equipment IS 'Equipment and inventory module';
COMMENT ON SCHEMA users IS 'User management module';
