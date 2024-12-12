-- PostgreSQL Schema Creation Script for DME/HME System
-- Based on core business modules structure

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Audit timestamp function
CREATE OR REPLACE FUNCTION update_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.last_update_datetime = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Create schemas for different modules
CREATE SCHEMA IF NOT EXISTS orders;
CREATE SCHEMA IF NOT EXISTS equipment;
CREATE SCHEMA IF NOT EXISTS pricing;
CREATE SCHEMA IF NOT EXISTS calendar;

-- Common Types
CREATE TYPE sale_rent_type AS ENUM ('sale', 'rent', 'both');
CREATE TYPE billing_cycle AS ENUM ('monthly', 'weekly', 'daily', 'one_time');
CREATE TYPE order_status AS ENUM ('pending', 'approved', 'voided', 'completed');

------------------------------------------
-- Order Management Tables
------------------------------------------

CREATE TABLE orders.order (
    order_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    customer_id uuid NOT NULL,
    order_date timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status order_status NOT NULL DEFAULT 'pending',
    sale_rent_type sale_rent_type NOT NULL,
    billing_cycle billing_cycle,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE orders.order_detail (
    order_detail_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    order_id uuid NOT NULL REFERENCES orders.order(order_id),
    item_id uuid NOT NULL,
    quantity integer NOT NULL,
    price_per_unit numeric(10,2) NOT NULL,
    billing_code varchar(20),
    dos_from date,
    dos_to date,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE orders.void_submission (
    void_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    order_id uuid NOT NULL REFERENCES orders.order(order_id),
    reason text NOT NULL,
    submitted_by uuid NOT NULL,
    submitted_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    approved_by uuid,
    approved_datetime timestamp
);

------------------------------------------
-- Equipment Management Tables
------------------------------------------

CREATE TABLE equipment.inventory_item (
    item_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    item_code varchar(50) NOT NULL UNIQUE,
    description text NOT NULL,
    item_type varchar(50) NOT NULL,
    manufacturer varchar(100),
    model_number varchar(50),
    sale_rent_type sale_rent_type NOT NULL,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE equipment.serial_number (
    serial_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    item_id uuid NOT NULL REFERENCES equipment.inventory_item(item_id),
    serial_number varchar(100) NOT NULL UNIQUE,
    status varchar(20) NOT NULL,
    location_id uuid,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE equipment.dmerc_helper (
    dmerc_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    item_id uuid NOT NULL REFERENCES equipment.inventory_item(item_id),
    billing_code varchar(20) NOT NULL,
    documentation_requirements text,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

------------------------------------------
-- Pricing and Billing Tables
------------------------------------------

CREATE TABLE pricing.price_list (
    price_list_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    name varchar(100) NOT NULL UNIQUE,
    effective_from date NOT NULL,
    effective_to date,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE pricing.price_list_item (
    price_list_item_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    price_list_id uuid NOT NULL REFERENCES pricing.price_list(price_list_id),
    item_id uuid NOT NULL REFERENCES equipment.inventory_item(item_id),
    sale_price numeric(10,2),
    rental_price numeric(10,2),
    billing_code varchar(20),
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE pricing.icd_codes (
    icd_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    code varchar(20) NOT NULL UNIQUE,
    description text NOT NULL,
    version integer NOT NULL, -- 9 or 10 for ICD version
    active boolean NOT NULL DEFAULT true,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

------------------------------------------
-- Calendar and Scheduling Tables
------------------------------------------

CREATE TABLE calendar.event (
    event_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    title varchar(200) NOT NULL,
    description text,
    start_time timestamp NOT NULL,
    end_time timestamp NOT NULL,
    event_type varchar(50) NOT NULL,
    google_event_id varchar(100),
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE calendar.appointment (
    appointment_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    event_id uuid NOT NULL REFERENCES calendar.event(event_id),
    customer_id uuid NOT NULL,
    order_id uuid REFERENCES orders.order(order_id),
    status varchar(20) NOT NULL,
    notes text,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

------------------------------------------
-- User Management Tables
------------------------------------------

CREATE TABLE users.user (
    user_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    username varchar(50) NOT NULL UNIQUE,
    email varchar(100) NOT NULL UNIQUE,
    password_hash varchar(255) NOT NULL,
    first_name varchar(50) NOT NULL,
    last_name varchar(50) NOT NULL,
    active boolean NOT NULL DEFAULT true,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE users.role (
    role_id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    name varchar(50) NOT NULL UNIQUE,
    description text,
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_update_by uuid NOT NULL,
    last_update_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE users.user_role (
    user_id uuid NOT NULL REFERENCES users.user(user_id),
    role_id uuid NOT NULL REFERENCES users.role(role_id),
    created_by uuid NOT NULL,
    created_datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, role_id)
);

-- Create update timestamp triggers
CREATE TRIGGER update_order_timestamp BEFORE UPDATE ON orders.order
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

CREATE TRIGGER update_order_detail_timestamp BEFORE UPDATE ON orders.order_detail
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

CREATE TRIGGER update_inventory_item_timestamp BEFORE UPDATE ON equipment.inventory_item
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

CREATE TRIGGER update_price_list_timestamp BEFORE UPDATE ON pricing.price_list
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

CREATE TRIGGER update_event_timestamp BEFORE UPDATE ON calendar.event
    FOR EACH ROW EXECUTE FUNCTION update_timestamp();

-- Add indexes
CREATE INDEX idx_order_customer ON orders.order(customer_id);
CREATE INDEX idx_order_detail_order ON orders.order_detail(order_id);
CREATE INDEX idx_serial_number_item ON equipment.serial_number(item_id);
CREATE INDEX idx_price_list_item_list ON pricing.price_list_item(price_list_id);
CREATE INDEX idx_appointment_event ON calendar.appointment(event_id);
CREATE INDEX idx_user_role_user ON users.user_role(user_id);

-- Comments
COMMENT ON SCHEMA orders IS 'Order management module';
COMMENT ON SCHEMA equipment IS 'Equipment and inventory management module';
COMMENT ON SCHEMA pricing IS 'Pricing and billing module';
COMMENT ON SCHEMA calendar IS 'Calendar and scheduling module';
COMMENT ON SCHEMA users IS 'User management and security module';
