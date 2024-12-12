-- Migration script generated at 20241212_134933

BEGIN;

CREATE TABLE tbl_authorizationtype (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL DEFAULT ,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_batchpayment (
    ID integer NOT NULL,
    InsuranceCompanyID integer NOT NULL,
    CheckNumber varchar(14) NOT NULL,
    CheckDate date NOT NULL,
    CheckAmount decimal(18,2) NOT NULL,
    AmountUsed decimal(18,2) NOT NULL,
    LastUpdateUserID smallint NOT NULL,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_billingtype (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_changes (
    TableName varchar(64) NOT NULL,
    SessionID integer NOT NULL,
    LastUpdateUserID smallint NOT NULL,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (TableName)
);


CREATE TABLE tbl_cmnform (
    ID integer NOT NULL,
    CMNType varchar CHECK (value IN ('dmerc 01.02a','dmerc 01.02b','dmerc 02.03a','dmerc 02.03b','dmerc 03.02','dmerc 04.03b','dmerc 04.03c','dmerc 06.02b','dmerc 07.02a','dmerc 07.02b','dmerc 08.02','dmerc 09.02','dmerc 10.02a','dmerc 10.02b','dmerc 484.2','dmerc drorder','dmerc uro','dme 04.04b','dme 04.04c','dme 06.03b','dme 07.03a','dme 09.03','dme 10.03','dme 484.03')) NOT NULL DEFAULT DME 484.03,
    InitialDate date,
    RevisedDate date,
    RecertificationDate date,
    CustomerID integer,
    Customer_ICD9_1 varchar(8),
    Customer_ICD9_2 varchar(8),
    Customer_ICD9_3 varchar(8),
    Customer_ICD9_4 varchar(8),
    DoctorID integer,
    POSTypeID integer,
    FacilityID integer,
    AnsweringName varchar(50) NOT NULL DEFAULT ,
    AnsweringTitle varchar(50) NOT NULL DEFAULT ,
    AnsweringEmployer varchar(50) NOT NULL DEFAULT ,
    EstimatedLengthOfNeed integer NOT NULL DEFAULT 0,
    Signature_Name varchar(50) NOT NULL DEFAULT ,
    Signature_Date date,
    OnFile smallint NOT NULL DEFAULT 0,
    OrderID integer,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MIR varchar[] CHECK (value <@ ARRAY['cmntype','initialdate','customerid','customer','icd9_1.required','icd9_1.unknown','icd9_1.inactive','icd9_2.unknown','icd9_2.inactive','icd9_3.unknown','icd9_3.inactive','icd9_4.unknown','icd9_4.inactive','doctorid','doctor','postypeid','estimatedlengthofneed','signature_name','signature_date','answers']) NOT NULL DEFAULT ,
    Customer_UsingICD10 smallint NOT NULL DEFAULT 0,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_cmnform_0102a (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer6 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0102b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer12 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer13 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer14 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer15 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer16 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer19 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer20 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer21_Ulcer1_Stage varchar(30),
    Answer21_Ulcer1_MaxLength double precision,
    Answer21_Ulcer1_MaxWidth double precision,
    Answer21_Ulcer2_Stage varchar(30),
    Answer21_Ulcer2_MaxLength double precision,
    Answer21_Ulcer2_MaxWidth double precision,
    Answer21_Ulcer3_Stage varchar(30),
    Answer21_Ulcer3_MaxLength double precision,
    Answer21_Ulcer3_MaxWidth double precision,
    Answer22 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0203a (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer2 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 integer,
    Answer6 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0203b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer2 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 integer,
    Answer8 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer9 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0302 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer12 integer,
    Answer14 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0403b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer2 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0403c (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer6a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer6b integer NOT NULL DEFAULT 0,
    Answer7a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7b integer NOT NULL DEFAULT 0,
    Answer8 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer9a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer9b integer NOT NULL DEFAULT 0,
    Answer10a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer10b integer NOT NULL DEFAULT 0,
    Answer10c integer NOT NULL DEFAULT 0,
    Answer11a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer11b integer NOT NULL DEFAULT 0,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0404b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer2 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer3 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer4 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer5 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0404c (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer6 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7b varchar(10),
    Answer8 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer9a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer9b varchar(10),
    Answer10a varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer10b varchar(10),
    Answer10c varchar(10),
    Answer11 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer12 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0602b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer2 date,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 integer,
    Answer5 varchar CHECK (value IN ('1','2','3','4','5')) NOT NULL DEFAULT 1,
    Answer6 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer8_begun date,
    Answer8_ended date,
    Answer9 date,
    Answer10 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    Answer11 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer12 varchar CHECK (value IN ('2','4')) NOT NULL DEFAULT 2,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0603b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer2 integer,
    Answer3 varchar CHECK (value IN ('1','2','3','4','5')) NOT NULL DEFAULT 5,
    Answer4 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer5 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer6 date,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0702a (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer2 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0702b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer6 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer7 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer8 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer12 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer13 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer14 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0703a (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer2 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer3 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0802 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1_HCPCS varchar(5) NOT NULL DEFAULT ,
    Answer1_MG integer,
    Answer1_Times integer,
    Answer2_HCPCS varchar(5) NOT NULL DEFAULT ,
    Answer2_MG integer,
    Answer2_Times integer,
    Answer3_HCPCS varchar(5) NOT NULL DEFAULT ,
    Answer3_MG integer,
    Answer3_Times integer,
    Answer4 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    Answer5_1 varchar CHECK (value IN ('1','2','3','4','5')) NOT NULL DEFAULT 1,
    Answer5_2 varchar CHECK (value IN ('1','2','3','4','5')) NOT NULL DEFAULT 1,
    Answer5_3 varchar CHECK (value IN ('1','2','3','4','5')) NOT NULL DEFAULT 1,
    Answer8 varchar(60) NOT NULL DEFAULT ,
    Answer9 varchar(20) NOT NULL DEFAULT ,
    Answer10 varchar(2) NOT NULL DEFAULT ,
    Answer11 date,
    Answer12 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT N,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0902 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('1','3','4')) NOT NULL DEFAULT 1,
    Answer2 varchar(50) NOT NULL DEFAULT ,
    Answer3 varchar(50) NOT NULL DEFAULT ,
    Answer4 varchar CHECK (value IN ('1','3','4')) NOT NULL DEFAULT 1,
    Answer5 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    Answer6 integer NOT NULL DEFAULT 1,
    Answer7 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_0903 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1a varchar(10),
    Answer1b varchar(10),
    Answer1c varchar(10),
    Answer2a varchar(50),
    Answer2b varchar(50),
    Answer2c varchar(50),
    Answer3 varchar CHECK (value IN ('1','2','3','4')) NOT NULL DEFAULT 1,
    Answer4 varchar CHECK (value IN ('1','2')) NOT NULL DEFAULT 1,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_1002a (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer3 integer,
    Concentration_AminoAcid double precision,
    Concentration_Dextrose double precision,
    Concentration_Lipids double precision,
    Dose_AminoAcid double precision,
    Dose_Dextrose double precision,
    Dose_Lipids double precision,
    DaysPerWeek_Lipids double precision,
    GmsPerDay_AminoAcid double precision,
    Answer5 varchar CHECK (value IN ('1','3','7')) NOT NULL DEFAULT 1,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_1002b (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer7 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer8 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer10a varchar(50) NOT NULL DEFAULT ,
    Answer10b varchar(50) NOT NULL DEFAULT ,
    Answer11a varchar(50) NOT NULL DEFAULT ,
    Answer11b varchar(50) NOT NULL DEFAULT ,
    Answer12 integer,
    Answer13 varchar CHECK (value IN ('1','2','3','4')) NOT NULL DEFAULT 1,
    Answer14 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer15 varchar(50) NOT NULL DEFAULT ,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_1003 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer2 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer3a varchar(10),
    Answer3b varchar(10),
    Answer4a integer,
    Answer4b integer,
    Answer5 varchar CHECK (value IN ('1','2','3','4')) NOT NULL DEFAULT 1,
    Answer6 integer,
    Answer7 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer8a integer,
    Answer8b integer,
    Answer8c integer,
    Answer8d integer,
    Answer8e integer,
    Answer8f integer,
    Answer8g integer,
    Answer8h integer,
    Answer9 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_48403 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1a integer,
    Answer1b integer,
    Answer1c date,
    Answer2 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    Answer3 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    Answer4 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer5 varchar(10),
    Answer6a integer,
    Answer6b integer,
    Answer6c date,
    Answer7 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer8 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer9 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_4842 (
    CMNFormID integer NOT NULL DEFAULT 0,
    Answer1a integer,
    Answer1b integer,
    Answer1c date,
    Answer2 varchar CHECK (value IN ('y','n')) NOT NULL DEFAULT Y,
    Answer3 varchar CHECK (value IN ('1','2','3')) NOT NULL DEFAULT 1,
    PhysicianAddress varchar(50) NOT NULL DEFAULT ,
    PhysicianCity varchar(50) NOT NULL DEFAULT ,
    PhysicianState varchar(50) NOT NULL DEFAULT ,
    PhysicianZip varchar(50) NOT NULL DEFAULT ,
    PhysicianName varchar(50) NOT NULL DEFAULT ,
    Answer5 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer6 varchar(10),
    Answer7a integer,
    Answer7b integer,
    Answer7c date,
    Answer8 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer9 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    Answer10 varchar CHECK (value IN ('y','n','d')) NOT NULL DEFAULT D,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_details (
    CMNFormID integer NOT NULL DEFAULT 0,
    BillingCode varchar(50),
    InventoryItemID integer NOT NULL DEFAULT 0,
    OrderedQuantity double precision NOT NULL DEFAULT 0,
    OrderedUnits varchar(50),
    BillablePrice double precision NOT NULL DEFAULT 0,
    AllowablePrice double precision NOT NULL DEFAULT 0,
    Period varchar CHECK (value IN ('one time','daily','weekly','monthly','quarterly','semi-annually','annually','custom')) NOT NULL DEFAULT One time,
    Modifier1 varchar(8) NOT NULL DEFAULT ,
    Modifier2 varchar(8) NOT NULL DEFAULT ,
    Modifier3 varchar(8) NOT NULL DEFAULT ,
    Modifier4 varchar(8) NOT NULL DEFAULT ,
    PredefinedTextID integer,
    ID integer NOT NULL,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_cmnform_drorder (
    CMNFormID integer NOT NULL DEFAULT 0,
    Prognosis varchar(50) NOT NULL DEFAULT ,
    MedicalJustification text NOT NULL,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_cmnform_uro (
    CMNFormID integer NOT NULL DEFAULT 0,
    Prognosis varchar(50) NOT NULL DEFAULT ,
    PRIMARY KEY (CMNFormID)
);


CREATE TABLE tbl_company (
    Address1 varchar(40) NOT NULL DEFAULT ,
    Address2 varchar(40) NOT NULL DEFAULT ,
    BillCustomerCopayUpfront smallint NOT NULL DEFAULT 0,
    City varchar(25) NOT NULL DEFAULT ,
    Fax varchar(50) NOT NULL DEFAULT ,
    FederalTaxID varchar(9) NOT NULL DEFAULT ,
    TaxonomyCode varchar(20) NOT NULL DEFAULT 332B00000X,
    EIN varchar(20) NOT NULL DEFAULT ,
    SSN varchar(20) NOT NULL DEFAULT ,
    TaxIDType varchar CHECK (value IN ('ssn','ein')) NOT NULL,
    ID integer NOT NULL,
    Name varchar(50) NOT NULL DEFAULT ,
    ParticipatingProvider smallint NOT NULL DEFAULT 0,
    Phone varchar(50) NOT NULL DEFAULT ,
    Phone2 varchar(50) NOT NULL DEFAULT ,
    POAuthorizationCodeReqiered smallint NOT NULL DEFAULT 0,
    Print_PricesOnOrders smallint NOT NULL DEFAULT 0,
    Picture mediumblob,
    POSTypeID integer DEFAULT 12,
    State char(2) NOT NULL DEFAULT ,
    SystemGenerate_BlanketAssignments smallint NOT NULL DEFAULT 0,
    SystemGenerate_CappedRentalLetters smallint NOT NULL DEFAULT 0,
    SystemGenerate_CustomerAccountNumbers smallint NOT NULL DEFAULT 0,
    SystemGenerate_DeliveryPickupTickets smallint NOT NULL DEFAULT 0,
    SystemGenerate_DroctorsOrder smallint NOT NULL DEFAULT 0,
    SystemGenerate_HIPPAForms smallint NOT NULL DEFAULT 0,
    SystemGenerate_PatientBillOfRights smallint NOT NULL DEFAULT 0,
    SystemGenerate_PurchaseOrderNumber smallint NOT NULL DEFAULT 0,
    WriteoffDifference smallint NOT NULL DEFAULT 0,
    Zip varchar(10) NOT NULL DEFAULT ,
    IncludeLocationInfo smallint NOT NULL DEFAULT 0,
    Contact varchar(50) NOT NULL DEFAULT ,
    Print_CompanyInfoOnInvoice smallint NOT NULL DEFAULT 0,
    Print_CompanyInfoOnDelivery smallint NOT NULL DEFAULT 0,
    Print_CompanyInfoOnPickup smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Show_InactiveCustomers smallint NOT NULL DEFAULT 0,
    WarehouseID integer,
    NPI varchar(10),
    TaxRateID integer,
    ImagingServer varchar(250),
    ZirmedNumber varchar(20) NOT NULL DEFAULT ,
    AutomaticallyReorderInventory smallint NOT NULL DEFAULT 1,
    AvailityNumber varchar(50) NOT NULL DEFAULT ,
    Show_QuantityOnHand smallint NOT NULL DEFAULT 0,
    Use_Icd10ForNewCmnRx smallint NOT NULL DEFAULT 0,
    OrderSurveyID integer,
    AbilityIntegrationSettings text NOT NULL,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_compliance (
    ID integer NOT NULL,
    CustomerID integer NOT NULL DEFAULT 0,
    OrderID integer,
    DeliveryDate date NOT NULL DEFAULT 0000-00-00,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_compliance_items (
    ComplianceID integer NOT NULL DEFAULT 0,
    InventoryItemID integer NOT NULL DEFAULT 0
);


CREATE TABLE tbl_compliance_notes (
    ID integer NOT NULL,
    ComplianceID integer NOT NULL DEFAULT 0,
    Date date NOT NULL DEFAULT 0000-00-00,
    Done smallint NOT NULL DEFAULT 0,
    Notes text NOT NULL,
    CreatedByUserID smallint,
    AssignedToUserID smallint,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_customer (
    AccountNumber varchar(40) NOT NULL DEFAULT ,
    Address1 varchar(40) NOT NULL DEFAULT ,
    Address2 varchar(40) NOT NULL DEFAULT ,
    BillingTypeID integer,
    City varchar(25) NOT NULL DEFAULT ,
    Courtesy varchar CHECK (value IN ('dr.','miss','mr.','mrs.','rev.')) NOT NULL DEFAULT Dr.,
    CustomerBalance double precision,
    CustomerClassCode char(2),
    CustomerTypeID integer,
    DeceasedDate date,
    DateofBirth date,
    FirstName varchar(25) NOT NULL DEFAULT ,
    ID integer NOT NULL,
    LastName varchar(30) NOT NULL DEFAULT ,
    LocationID integer,
    MiddleName char(1) NOT NULL DEFAULT ,
    Phone varchar(50) NOT NULL DEFAULT ,
    Phone2 varchar(50) NOT NULL DEFAULT ,
    State char(2) NOT NULL DEFAULT ,
    Suffix varchar(4) NOT NULL DEFAULT ,
    TotalBalance double precision,
    Zip varchar(10) NOT NULL DEFAULT ,
    BillActive smallint NOT NULL DEFAULT 0,
    BillAddress1 varchar(40) NOT NULL DEFAULT ,
    BillAddress2 varchar(40) NOT NULL DEFAULT ,
    BillCity varchar(25) NOT NULL DEFAULT ,
    BillName varchar(50) NOT NULL DEFAULT ,
    BillState char(2) NOT NULL DEFAULT ,
    BillZip varchar(10) NOT NULL DEFAULT ,
    CommercialAccount smallint,
    DeliveryDirections text NOT NULL,
    EmploymentStatus varchar CHECK (value IN ('unknown','full time','part time','retired','student','unemployed')) NOT NULL DEFAULT Unknown,
    Gender varchar CHECK (value IN ('male','female')) NOT NULL DEFAULT Male,
    Height double precision,
    License varchar(50) NOT NULL DEFAULT ,
    MaritalStatus varchar CHECK (value IN ('unknown','single','married','legaly separated','divorced','widowed')) NOT NULL DEFAULT Unknown,
    MilitaryBranch varchar CHECK (value IN ('n/a','army','air force','navy','marines','coast guard','national guard')) NOT NULL DEFAULT N/A,
    MilitaryStatus varchar CHECK (value IN ('n/a','active','reserve','retired')) NOT NULL DEFAULT N/A,
    ShipActive smallint NOT NULL DEFAULT 0,
    ShipAddress1 varchar(40) NOT NULL DEFAULT ,
    ShipAddress2 varchar(40) NOT NULL DEFAULT ,
    ShipCity varchar(25) NOT NULL DEFAULT ,
    ShipName varchar(50) NOT NULL DEFAULT ,
    ShipState char(2) NOT NULL DEFAULT ,
    ShipZip varchar(10) NOT NULL DEFAULT ,
    SSNumber varchar(50) NOT NULL DEFAULT ,
    StudentStatus varchar CHECK (value IN ('n/a','full time','part time')) NOT NULL DEFAULT N/A,
    Weight double precision,
    Basis varchar CHECK (value IN ('bill','allowed')) NOT NULL DEFAULT Bill,
    Block12HCFA smallint NOT NULL DEFAULT 0,
    Block13HCFA smallint NOT NULL DEFAULT 0,
    CommercialAcctCreditLimit double precision,
    CommercialAcctTerms varchar(50) NOT NULL DEFAULT ,
    CopayDollar double precision,
    Deductible double precision,
    Frequency varchar CHECK (value IN ('per visit','monthly','yearly')) NOT NULL DEFAULT Per Visit,
    Hardship smallint NOT NULL DEFAULT 0,
    MonthsValid integer NOT NULL DEFAULT 0,
    OutOfPocket double precision,
    SignatureOnFile date,
    SignatureType char(1),
    TaxRateID integer,
    Doctor1_ID integer,
    Doctor2_ID integer,
    EmergencyContact text NOT NULL,
    FacilityID integer,
    LegalRepID integer,
    ReferralID integer,
    SalesRepID integer,
    AccidentType varchar CHECK (value IN ('auto','no','other')) NOT NULL,
    StateOfAccident char(2) NOT NULL DEFAULT ,
    DateOfInjury date,
    Emergency smallint NOT NULL DEFAULT 0,
    EmploymentRelated smallint NOT NULL DEFAULT 0,
    FirstConsultDate date,
    ICD9_1 varchar(6),
    ICD9_2 varchar(6),
    ICD9_3 varchar(6),
    ICD9_4 varchar(6),
    POSTypeID integer,
    ReturnToWorkDate date,
    CopayPercent double precision,
    SetupDate date NOT NULL DEFAULT 0000-00-00,
    HIPPANote smallint NOT NULL DEFAULT 0,
    SupplierStandards smallint NOT NULL DEFAULT 0,
    InactiveDate date,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    InvoiceFormID integer DEFAULT 4,
    MIR varchar[] CHECK (value <@ ARRAY['accountnumber','firstname','lastname','address1','city','state','zip','employmentstatus','gender','maritalstatus','militarybranch','militarystatus','studentstatus','monthsvalid','dateofbirth','signatureonfile','doctor1_id','doctor1','icd9_1']) NOT NULL DEFAULT ,
    Email varchar(150),
    Collections boolean NOT NULL DEFAULT b'0',
    ICD10_01 varchar(8),
    ICD10_02 varchar(8),
    ICD10_03 varchar(8),
    ICD10_04 varchar(8),
    ICD10_05 varchar(8),
    ICD10_06 varchar(8),
    ICD10_07 varchar(8),
    ICD10_08 varchar(8),
    ICD10_09 varchar(8),
    ICD10_10 varchar(8),
    ICD10_11 varchar(8),
    ICD10_12 varchar(8),
    PRIMARY KEY (ID)
);

CREATE UNIQUE INDEX tbl_customer_AccountNumber_idx ON tbl_customer (AccountNumber);

CREATE INDEX tbl_customer_FirstName_idx ON tbl_customer (FirstName);

CREATE INDEX tbl_customer_LastName_idx ON tbl_customer (LastName);

CREATE INDEX tbl_customer_DateofBirth_idx ON tbl_customer (DateofBirth);

CREATE INDEX tbl_customer_MiddleName_idx ON tbl_customer (MiddleName);


CREATE TABLE tbl_customerclass (
    Code char(2) NOT NULL DEFAULT ,
    Description varchar(50) NOT NULL DEFAULT ,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_customertype (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_customer_insurance (
    Address1 varchar(40) NOT NULL DEFAULT ,
    Address2 varchar(40) NOT NULL DEFAULT ,
    City varchar(25) NOT NULL DEFAULT ,
    State char(2) NOT NULL DEFAULT ,
    Zip varchar(10) NOT NULL DEFAULT ,
    Basis varchar CHECK (value IN ('bill','allowed')) NOT NULL DEFAULT Bill,
    CustomerID integer NOT NULL DEFAULT 0,
    DateofBirth date,
    Gender varchar CHECK (value IN ('male','female')) NOT NULL DEFAULT Male,
    GroupNumber varchar(50) NOT NULL DEFAULT ,
    ID integer NOT NULL,
    InactiveDate date,
    InsuranceCompanyID integer NOT NULL DEFAULT 0,
    InsuranceType char(2),
    FirstName varchar(25) NOT NULL DEFAULT ,
    LastName varchar(30) NOT NULL DEFAULT ,
    MiddleName char(1) NOT NULL DEFAULT ,
    Employer varchar(50) NOT NULL DEFAULT ,
    Mobile varchar(50) NOT NULL DEFAULT ,
    PaymentPercent integer,
    Phone varchar(50) NOT NULL DEFAULT ,
    PolicyNumber varchar(50) NOT NULL DEFAULT ,
    Rank integer,
    RelationshipCode char(2),
    RequestEligibility smallint NOT NULL DEFAULT 0,
    RequestEligibilityOn date,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MIR varchar[] CHECK (value <@ ARRAY['firstname','lastname','address1','city','state','zip','gender','dateofbirth','insurancecompanyid','insurancecompany','insurancetype','policynumber','relationshipcode']) NOT NULL DEFAULT ,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_customer_notes (
    ID integer NOT NULL,
    CustomerID integer NOT NULL,
    Notes text NOT NULL,
    Active smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Operator varchar(50),
    CallbackDate timestamp,
    CreatedBy smallint,
    CreatedAt timestamp,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_denial (
    Code varchar(6) NOT NULL,
    Description varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_depositdetails (
    OrderDetailsID integer NOT NULL,
    OrderID integer NOT NULL,
    CustomerID integer NOT NULL,
    Amount decimal(18,2) NOT NULL,
    LastUpdateUserID smallint NOT NULL,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (OrderDetailsID)
);

CREATE INDEX tbl_depositdetails_CustomerID_idx ON tbl_depositdetails (CustomerID);

CREATE INDEX tbl_depositdetails_OrderID_idx ON tbl_depositdetails (OrderID);

CREATE INDEX tbl_depositdetails_OrderDetailsID_idx ON tbl_depositdetails (OrderDetailsID);

ALTER TABLE tbl_depositdetails ADD CONSTRAINT FK_DEPOSITDETAILS_DEPOSITS FOREIGN KEY (CustomerID) REFERENCES tbl_deposits(CustomerID);

ALTER TABLE tbl_depositdetails ADD CONSTRAINT FK_DEPOSITDETAILS_DEPOSITS FOREIGN KEY (OrderID) REFERENCES tbl_deposits(OrderID);

ALTER TABLE tbl_depositdetails ADD CONSTRAINT FK_DEPOSITDETAILS_ORDERDETAILS FOREIGN KEY (OrderDetailsID) REFERENCES tbl_orderdetails(ID);


CREATE TABLE tbl_deposits (
    CustomerID integer NOT NULL,
    OrderID integer NOT NULL,
    Amount decimal(18,2) NOT NULL,
    Date date NOT NULL,
    PaymentMethod varchar CHECK (value IN ('cash','check','credit card')) NOT NULL,
    Notes text NOT NULL,
    LastUpdateUserID smallint NOT NULL,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (CustomerID, OrderID)
);

ALTER TABLE tbl_deposits ADD CONSTRAINT FK_DEPOSITS_ORDER FOREIGN KEY (CustomerID) REFERENCES tbl_order(CustomerID);

ALTER TABLE tbl_deposits ADD CONSTRAINT FK_DEPOSITS_ORDER FOREIGN KEY (OrderID) REFERENCES tbl_order(ID);


CREATE TABLE tbl_eligibilityrequest (
    ID integer NOT NULL,
    CustomerID integer NOT NULL DEFAULT 0,
    CustomerInsuranceID integer NOT NULL DEFAULT 0,
    Region varchar CHECK (value IN ('region a','region b','region c','region d','zirmed','medi-cal','availity','office ally','ability')) NOT NULL DEFAULT Region A,
    RequestBatchID integer,
    RequestTime timestamp NOT NULL DEFAULT 1900-01-01 00:00:00,
    RequestText text NOT NULL,
    ResponseBatchID integer,
    ResponseTime timestamp,
    ResponseText text,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_facility (
    Address1 varchar(40) NOT NULL,
    Address2 varchar(40) NOT NULL,
    City varchar(25) NOT NULL,
    Contact varchar(50) NOT NULL,
    DefaultDeliveryWeek varchar CHECK (value IN ('1st week of month','2nd week of month','3rd week of month','4th week of month','as needed')) NOT NULL,
    Directions text,
    Fax varchar(50) NOT NULL,
    ID integer NOT NULL,
    MedicaidID varchar(50) NOT NULL,
    MedicareID varchar(50) NOT NULL,
    Name varchar(50) NOT NULL,
    Phone varchar(50) NOT NULL,
    Phone2 varchar(50) NOT NULL,
    POSTypeID integer DEFAULT 12,
    State varchar(2) NOT NULL,
    Zip varchar(10) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    NPI varchar(10),
    MIR varchar[] CHECK (value <@ ARRAY['name','address1','city','state','zip','postypeid','npi']) NOT NULL DEFAULT ,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_hao (
    Code varchar(10) NOT NULL,
    Description text NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_image (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL DEFAULT ,
    Type varchar(50) NOT NULL DEFAULT ,
    Description text,
    CustomerID integer,
    OrderID integer,
    InvoiceID integer,
    DoctorID integer,
    CMNFormID integer,
    Thumbnail bytea,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_insurancetype (
    Code varchar(2) NOT NULL,
    Description varchar(40) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_inventory (
    WarehouseID integer NOT NULL DEFAULT 0,
    InventoryItemID integer NOT NULL DEFAULT 0,
    OnHand double precision NOT NULL DEFAULT 0,
    Committed double precision NOT NULL DEFAULT 0,
    OnOrder double precision NOT NULL DEFAULT 0,
    UnAvailable double precision NOT NULL DEFAULT 0,
    Rented double precision NOT NULL DEFAULT 0,
    Sold double precision NOT NULL DEFAULT 0,
    BackOrdered double precision NOT NULL DEFAULT 0,
    ReOrderPoint double precision NOT NULL DEFAULT 0,
    CostPerUnit decimal(18,2) NOT NULL DEFAULT 0.00,
    TotalCost decimal(18,2) NOT NULL DEFAULT 0.00,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (WarehouseID, InventoryItemID)
);


CREATE TABLE tbl_inventoryitem (
    Barcode varchar(50) NOT NULL DEFAULT ,
    BarcodeType varchar(50) NOT NULL DEFAULT ,
    Basis varchar CHECK (value IN ('bill','allowed')) NOT NULL DEFAULT Bill,
    CommissionPaidAt varchar CHECK (value IN ('billing','payment','never')) NOT NULL DEFAULT Billing,
    VendorID integer,
    FlatRate smallint NOT NULL DEFAULT 0,
    FlatRateAmount double precision,
    Frequency varchar CHECK (value IN ('one time','monthly','weekly','never')) NOT NULL DEFAULT One time,
    ID integer NOT NULL,
    InventoryCode varchar(50) NOT NULL DEFAULT ,
    ModelNumber varchar(50) NOT NULL DEFAULT ,
    Name varchar(100) NOT NULL DEFAULT ,
    O2Tank smallint NOT NULL DEFAULT 0,
    Percentage smallint NOT NULL DEFAULT 0,
    PercentageAmount double precision NOT NULL DEFAULT 0,
    PredefinedTextID integer,
    ProductTypeID integer,
    Serialized smallint NOT NULL DEFAULT 0,
    Service smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Inactive smallint NOT NULL DEFAULT 0,
    ManufacturerID integer,
    PurchasePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    UserField1 varchar(100) NOT NULL DEFAULT ,
    UserField2 varchar(100) NOT NULL DEFAULT ,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_inventory_transaction (
    ID integer NOT NULL,
    InventoryItemID integer NOT NULL DEFAULT 0,
    WarehouseID integer NOT NULL DEFAULT 0,
    TypeID integer NOT NULL DEFAULT 0,
    Date date NOT NULL DEFAULT 0000-00-00,
    Quantity double precision,
    Cost decimal(18,2),
    Description varchar(30),
    SerialID integer,
    VendorID integer,
    CustomerID integer,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PurchaseOrderID integer,
    PurchaseOrderDetailsID integer,
    InvoiceID integer,
    ManufacturerID integer,
    OrderDetailsID integer,
    OrderID integer,
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_inventory_transaction_TypeID_idx ON tbl_inventory_transaction (TypeID);

CREATE INDEX tbl_inventory_transaction_CustomerID_idx ON tbl_inventory_transaction (CustomerID);

CREATE INDEX tbl_inventory_transaction_OrderID_idx ON tbl_inventory_transaction (OrderID);

CREATE INDEX tbl_inventory_transaction_OrderDetailsID_idx ON tbl_inventory_transaction (OrderDetailsID);

CREATE INDEX tbl_inventory_transaction_InventoryItemID_idx ON tbl_inventory_transaction (InventoryItemID);

CREATE INDEX tbl_inventory_transaction_WarehouseID_idx ON tbl_inventory_transaction (WarehouseID);

CREATE INDEX tbl_inventory_transaction_TypeID_idx ON tbl_inventory_transaction (TypeID);

CREATE INDEX tbl_inventory_transaction_InventoryItemID_idx ON tbl_inventory_transaction (InventoryItemID);

CREATE INDEX tbl_inventory_transaction_WarehouseID_idx ON tbl_inventory_transaction (WarehouseID);

CREATE INDEX tbl_inventory_transaction_TypeID_idx ON tbl_inventory_transaction (TypeID);

CREATE INDEX tbl_inventory_transaction_PurchaseOrderID_idx ON tbl_inventory_transaction (PurchaseOrderID);

CREATE INDEX tbl_inventory_transaction_PurchaseOrderDetailsID_idx ON tbl_inventory_transaction (PurchaseOrderDetailsID);

CREATE INDEX tbl_inventory_transaction_InventoryItemID_idx ON tbl_inventory_transaction (InventoryItemID);

CREATE INDEX tbl_inventory_transaction_WarehouseID_idx ON tbl_inventory_transaction (WarehouseID);


CREATE TABLE tbl_inventory_transaction_type (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL DEFAULT ,
    OnHand integer NOT NULL DEFAULT 0,
    Committed integer NOT NULL DEFAULT 0,
    OnOrder integer NOT NULL DEFAULT 0,
    UnAvailable integer NOT NULL DEFAULT 0,
    Rented integer NOT NULL DEFAULT 0,
    Sold integer NOT NULL DEFAULT 0,
    BackOrdered integer NOT NULL DEFAULT 0,
    AdjTotalCost integer NOT NULL DEFAULT 0,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_invoice (
    ID integer NOT NULL,
    CustomerID integer NOT NULL DEFAULT 0,
    OrderID integer,
    Approved smallint NOT NULL DEFAULT 0,
    InvoiceDate date,
    InvoiceBalance decimal(18,2) NOT NULL DEFAULT 0.00,
    SubmittedTo varchar CHECK (value IN ('ins1','ins2','ins3','ins4','patient')) NOT NULL DEFAULT Ins1,
    SubmittedBy varchar(50),
    SubmittedDate date,
    SubmittedBatch varchar(50),
    CustomerInsurance1_ID integer,
    CustomerInsurance2_ID integer,
    CustomerInsurance3_ID integer,
    CustomerInsurance4_ID integer,
    ICD9_1 varchar(6),
    ICD9_2 varchar(6),
    ICD9_3 varchar(6),
    ICD9_4 varchar(6),
    DoctorID integer,
    POSTypeID integer,
    TaxRateID integer,
    TaxRatePercent double precision,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Discount double precision DEFAULT 0,
    AcceptAssignment smallint NOT NULL DEFAULT 0,
    ClaimNote varchar(80),
    FacilityID integer,
    ReferralID integer,
    SalesrepID integer,
    Archived smallint NOT NULL DEFAULT 0,
    ICD10_01 varchar(8),
    ICD10_02 varchar(8),
    ICD10_03 varchar(8),
    ICD10_04 varchar(8),
    ICD10_05 varchar(8),
    ICD10_06 varchar(8),
    ICD10_07 varchar(8),
    ICD10_08 varchar(8),
    ICD10_09 varchar(8),
    ICD10_10 varchar(8),
    ICD10_11 varchar(8),
    ICD10_12 varchar(8),
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_invoice_CustomerID_idx ON tbl_invoice (CustomerID);

CREATE INDEX tbl_invoice_ID_idx ON tbl_invoice (ID);

CREATE INDEX tbl_invoice_CustomerID_idx ON tbl_invoice (CustomerID);

CREATE INDEX tbl_invoice_OrderID_idx ON tbl_invoice (OrderID);

ALTER TABLE tbl_invoice ADD CONSTRAINT FK_ORDER_2 FOREIGN KEY (CustomerID) REFERENCES tbl_order(CustomerID);

ALTER TABLE tbl_invoice ADD CONSTRAINT FK_ORDER_2 FOREIGN KEY (OrderID) REFERENCES tbl_order(ID);


CREATE TABLE tbl_invoicedetails (
    ID integer NOT NULL,
    InvoiceID integer NOT NULL DEFAULT 0,
    CustomerID integer NOT NULL DEFAULT 0,
    InventoryItemID integer NOT NULL DEFAULT 0,
    PriceCodeID integer NOT NULL DEFAULT 0,
    OrderID integer,
    OrderDetailsID integer,
    Balance decimal(18,2) NOT NULL DEFAULT 0.00,
    BillableAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    AllowableAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    Taxes decimal(18,2) NOT NULL DEFAULT 0.00,
    Quantity double precision NOT NULL DEFAULT 0,
    InvoiceDate date,
    DOSFrom date NOT NULL DEFAULT 0000-00-00,
    DOSTo date,
    BillingCode varchar(50),
    Modifier1 varchar(8) NOT NULL DEFAULT ,
    Modifier2 varchar(8) NOT NULL DEFAULT ,
    Modifier3 varchar(8) NOT NULL DEFAULT ,
    Modifier4 varchar(8) NOT NULL DEFAULT ,
    DXPointer varchar(50),
    BillingMonth integer NOT NULL DEFAULT 0,
    SendCMN_RX_w_invoice smallint NOT NULL DEFAULT 0,
    SpecialCode varchar(50),
    ReviewCode varchar(50),
    MedicallyUnnecessary smallint NOT NULL DEFAULT 0,
    AuthorizationNumber varchar(50),
    AuthorizationTypeID integer,
    InvoiceNotes varchar(255),
    InvoiceRecord varchar(255),
    CMNFormID integer,
    HAOCode varchar(10),
    BillIns1 smallint NOT NULL DEFAULT 1,
    BillIns2 smallint NOT NULL DEFAULT 1,
    BillIns3 smallint NOT NULL DEFAULT 1,
    BillIns4 smallint NOT NULL DEFAULT 1,
    Hardship smallint NOT NULL DEFAULT 0,
    ShowSpanDates smallint NOT NULL DEFAULT 0,
    PaymentAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    WriteoffAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    CurrentPayer varchar CHECK (value IN ('ins1','ins2','ins3','ins4','patient','none')) NOT NULL DEFAULT Ins1,
    Pendings smallint NOT NULL DEFAULT 0,
    Submits smallint NOT NULL DEFAULT 0,
    Payments smallint NOT NULL DEFAULT 0,
    SubmittedDate date,
    Submitted smallint NOT NULL DEFAULT 0,
    CurrentInsuranceCompanyID integer,
    CurrentCustomerInsuranceID integer,
    AcceptAssignment smallint NOT NULL DEFAULT 0,
    DeductibleAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    DrugNoteField varchar(20),
    DrugControlNumber varchar(50),
    NopayIns1 smallint NOT NULL DEFAULT 0,
    PointerICD10 smallint NOT NULL DEFAULT 0,
    DXPointer10 varchar(50),
    HaoDescription varchar(100),
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_invoicedetails_CustomerID_idx ON tbl_invoicedetails (CustomerID);

CREATE INDEX tbl_invoicedetails_InvoiceID_idx ON tbl_invoicedetails (InvoiceID);

CREATE INDEX tbl_invoicedetails_ID_idx ON tbl_invoicedetails (ID);

ALTER TABLE tbl_invoicedetails ADD CONSTRAINT FK_INVOICE FOREIGN KEY (CustomerID) REFERENCES tbl_invoice(CustomerID);

ALTER TABLE tbl_invoicedetails ADD CONSTRAINT FK_INVOICE FOREIGN KEY (InvoiceID) REFERENCES tbl_invoice(ID);


CREATE TABLE tbl_invoiceform (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    ReportFileName varchar(50) NOT NULL,
    MarginTop double precision NOT NULL DEFAULT 0.25,
    MarginLeft double precision NOT NULL DEFAULT 0.19,
    MarginBottom double precision NOT NULL DEFAULT 0.18,
    MarginRight double precision NOT NULL DEFAULT 0.22,
    SpecialCoding varchar(20),
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_invoicenotes (
    ID integer NOT NULL,
    InvoiceDetailsID integer NOT NULL DEFAULT 0,
    InvoiceID integer NOT NULL DEFAULT 0,
    CustomerID integer NOT NULL DEFAULT 0,
    CallbackDate date,
    Done smallint NOT NULL DEFAULT 0,
    Notes text NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_invoice_transaction (
    ID integer NOT NULL,
    InvoiceDetailsID integer NOT NULL DEFAULT 0,
    InvoiceID integer NOT NULL DEFAULT 0,
    CustomerID integer NOT NULL DEFAULT 0,
    InsuranceCompanyID integer,
    CustomerInsuranceID integer,
    TransactionTypeID integer NOT NULL DEFAULT 0,
    TransactionDate date,
    Amount decimal(18,2) NOT NULL DEFAULT 0.00,
    Quantity double precision NOT NULL DEFAULT 0,
    Taxes decimal(18,2) NOT NULL DEFAULT 0.00,
    BatchNumber varchar(20) NOT NULL DEFAULT ,
    Comments text,
    Extra text,
    Approved smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Deductible decimal(18,2) NOT NULL DEFAULT 0.00,
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_invoice_transaction_CustomerID_idx ON tbl_invoice_transaction (CustomerID);

CREATE INDEX tbl_invoice_transaction_InvoiceID_idx ON tbl_invoice_transaction (InvoiceID);

CREATE INDEX tbl_invoice_transaction_InvoiceDetailsID_idx ON tbl_invoice_transaction (InvoiceDetailsID);

ALTER TABLE tbl_invoice_transaction ADD CONSTRAINT FK_INVOICE_TRANSACTION_INVOICE FOREIGN KEY (CustomerID) REFERENCES tbl_invoicedetails(CustomerID);

ALTER TABLE tbl_invoice_transaction ADD CONSTRAINT FK_INVOICE_TRANSACTION_INVOICE FOREIGN KEY (InvoiceID) REFERENCES tbl_invoicedetails(InvoiceID);

ALTER TABLE tbl_invoice_transaction ADD CONSTRAINT FK_INVOICE_TRANSACTION_INVOICE FOREIGN KEY (InvoiceDetailsID) REFERENCES tbl_invoicedetails(ID);


CREATE TABLE tbl_invoice_transactiontype (
    ID integer NOT NULL DEFAULT 0,
    Name varchar(50) NOT NULL DEFAULT ,
    Balance integer NOT NULL DEFAULT 0,
    Allowable integer NOT NULL DEFAULT 0,
    Amount integer NOT NULL DEFAULT 0,
    Taxes integer NOT NULL DEFAULT 0,
    PRIMARY KEY (ID)
);

CREATE UNIQUE INDEX tbl_invoice_transactiontype_Name_idx ON tbl_invoice_transactiontype (Name);


CREATE TABLE tbl_kit (
    ID integer NOT NULL,
    Name varchar(50),
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_kitdetails (
    ID integer NOT NULL,
    KitID integer NOT NULL,
    WarehouseID integer NOT NULL,
    InventoryItemID integer NOT NULL,
    PriceCodeID integer,
    Quantity integer NOT NULL,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_legalrep (
    Address1 varchar(40) NOT NULL,
    Address2 varchar(40) NOT NULL,
    City varchar(25) NOT NULL,
    Courtesy varchar CHECK (value IN ('dr.','miss','mr.','mrs.','rev.')) NOT NULL,
    FirstName varchar(25) NOT NULL,
    OfficePhone varchar(50) NOT NULL,
    ID integer NOT NULL,
    LastName varchar(30) NOT NULL,
    MiddleName varchar(1) NOT NULL,
    Mobile varchar(50) NOT NULL,
    Pager varchar(50) NOT NULL,
    State varchar(2) NOT NULL,
    Suffix varchar(4) NOT NULL,
    Zip varchar(10) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FirmName varchar(50),
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_location (
    ID integer NOT NULL,
    Contact varchar(50) NOT NULL DEFAULT ,
    Name varchar(50) NOT NULL DEFAULT ,
    Code varchar(40) NOT NULL DEFAULT ,
    City varchar(25) NOT NULL DEFAULT ,
    Address1 varchar(40) NOT NULL DEFAULT ,
    Address2 varchar(40) NOT NULL DEFAULT ,
    State char(2) NOT NULL DEFAULT ,
    Zip varchar(10) NOT NULL DEFAULT ,
    Fax varchar(50) NOT NULL DEFAULT ,
    FEDTaxID varchar(50) NOT NULL DEFAULT ,
    TaxIDType varchar CHECK (value IN ('ssn','ein')) NOT NULL DEFAULT SSN,
    Phone varchar(50) NOT NULL DEFAULT ,
    Phone2 varchar(50) NOT NULL DEFAULT ,
    PrintInfoOnDelPupTicket smallint,
    PrintInfoOnInvoiceAcctStatements smallint,
    PrintInfoOnPartProvider smallint,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    NPI varchar(10),
    InvoiceFormID integer,
    PriceCodeID integer,
    ParticipatingProvider smallint,
    Email varchar(50),
    WarehouseID integer,
    POSTypeID integer DEFAULT 12,
    TaxRateID integer,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_manufacturer (
    AccountNumber varchar(40) NOT NULL,
    Address1 varchar(40) NOT NULL,
    Address2 varchar(40) NOT NULL,
    City varchar(25) NOT NULL,
    Contact varchar(50) NOT NULL,
    Fax varchar(50) NOT NULL,
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    Phone varchar(50) NOT NULL,
    Phone2 varchar(50) NOT NULL,
    State varchar(2) NOT NULL,
    Zip varchar(10) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_medicalconditions (
    Code varchar(6) NOT NULL,
    Description varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_object (
    ID integer NOT NULL,
    Description varchar(50) NOT NULL,
    Name varchar(50) NOT NULL,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_order (
    ID integer NOT NULL,
    CustomerID integer NOT NULL DEFAULT 0,
    Approved smallint NOT NULL DEFAULT 0,
    RetailSales smallint NOT NULL DEFAULT 0,
    OrderDate date,
    DeliveryDate date,
    BillDate date,
    EndDate date,
    ShippingMethodID integer,
    SpecialInstructions text,
    TicketMesage varchar(50),
    CustomerInsurance1_ID integer,
    CustomerInsurance2_ID integer,
    CustomerInsurance3_ID integer,
    CustomerInsurance4_ID integer,
    ICD9_1 varchar(6),
    ICD9_2 varchar(6),
    ICD9_3 varchar(6),
    ICD9_4 varchar(6),
    DoctorID integer,
    POSTypeID integer,
    TakenBy varchar(50) DEFAULT ,
    Discount double precision,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    SaleType varchar CHECK (value IN ('retail','back office')) NOT NULL DEFAULT Back Office,
    State varchar CHECK (value IN ('new','approved','closed','canceled')) NOT NULL DEFAULT New,
    MIR varchar[] CHECK (value <@ ARRAY['billdate','customerid','deliverydate','customer.inactive','customer.mir','policy1.required','policy1.mir','policy2.required','policy2.mir','facility.mir','postype.required','icd9.required','icd9.1.unknown','icd9.1.inactive','icd9.2.unknown','icd9.2.inactive','icd9.3.unknown','icd9.3.inactive','icd9.4.unknown','icd9.4.inactive','icd10.required','icd10.01.unknown','icd10.01.inactive','icd10.02.unknown','icd10.02.inactive','icd10.03.unknown','icd10.03.inactive','icd10.04.unknown','icd10.04.inactive','icd10.05.unknown','icd10.05.inactive','icd10.06.unknown','icd10.06.inactive','icd10.07.unknown','icd10.07.inactive','icd10.08.unknown','icd10.08.inactive','icd10.09.unknown','icd10.09.inactive','icd10.10.unknown','icd10.10.inactive','icd10.11.unknown','icd10.11.inactive','icd10.12.unknown','icd10.12.inactive']) NOT NULL DEFAULT ,
    AcceptAssignment smallint NOT NULL DEFAULT 0,
    ClaimNote varchar(80),
    FacilityID integer,
    ReferralID integer,
    SalesrepID integer,
    LocationID integer,
    Archived smallint NOT NULL DEFAULT 0,
    TakenAt timestamp,
    ICD10_01 varchar(8),
    ICD10_02 varchar(8),
    ICD10_03 varchar(8),
    ICD10_04 varchar(8),
    ICD10_05 varchar(8),
    ICD10_06 varchar(8),
    ICD10_07 varchar(8),
    ICD10_08 varchar(8),
    ICD10_09 varchar(8),
    ICD10_10 varchar(8),
    ICD10_11 varchar(8),
    ICD10_12 varchar(8),
    UserField1 varchar(100) NOT NULL DEFAULT ,
    UserField2 varchar(100) NOT NULL DEFAULT ,
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_order_CustomerID_idx ON tbl_order (CustomerID);

CREATE INDEX tbl_order_ID_idx ON tbl_order (ID);


CREATE TABLE tbl_orderdeposits (
    OrderDetailsID integer NOT NULL,
    OrderID integer NOT NULL,
    CustomerID integer NOT NULL,
    Amount decimal(18,2) NOT NULL,
    Date date NOT NULL,
    LastUpdateUserID smallint NOT NULL,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (OrderDetailsID)
);

CREATE INDEX tbl_orderdeposits_CustomerID_idx ON tbl_orderdeposits (CustomerID);

CREATE INDEX tbl_orderdeposits_OrderID_idx ON tbl_orderdeposits (OrderID);

CREATE INDEX tbl_orderdeposits_OrderDetailsID_idx ON tbl_orderdeposits (OrderDetailsID);

ALTER TABLE tbl_orderdeposits ADD CONSTRAINT FK_ORDERDEPOSITS_ORDER FOREIGN KEY (CustomerID) REFERENCES tbl_order(CustomerID);

ALTER TABLE tbl_orderdeposits ADD CONSTRAINT FK_ORDERDEPOSITS_ORDER FOREIGN KEY (OrderID) REFERENCES tbl_order(ID);

ALTER TABLE tbl_orderdeposits ADD CONSTRAINT FK_ORDERDEPOSITS_ORDERDETAILS FOREIGN KEY (OrderDetailsID) REFERENCES tbl_orderdetails(ID);


CREATE TABLE tbl_orderdetails (
    ID integer NOT NULL,
    OrderID integer NOT NULL DEFAULT 0,
    CustomerID integer NOT NULL DEFAULT 0,
    SerialNumber varchar(50),
    InventoryItemID integer NOT NULL DEFAULT 0,
    PriceCodeID integer NOT NULL DEFAULT 0,
    SaleRentType varchar CHECK (value IN ('medicare oxygen rental','one time rental','monthly rental','capped rental','parental capped rental','rent to purchase','one time sale','re-occurring sale')) NOT NULL DEFAULT Monthly Rental,
    SerialID integer,
    BillablePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    AllowablePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    Taxable smallint NOT NULL DEFAULT 0,
    FlatRate smallint NOT NULL DEFAULT 0,
    DOSFrom date NOT NULL DEFAULT 0000-00-00,
    DOSTo date,
    PickupDate date,
    ShowSpanDates smallint NOT NULL DEFAULT 0,
    OrderedQuantity double precision NOT NULL DEFAULT 0,
    OrderedUnits varchar(50),
    OrderedWhen varchar CHECK (value IN ('one time','daily','weekly','monthly','quarterly','semi-annually','annually')) NOT NULL DEFAULT One time,
    OrderedConverter double precision NOT NULL DEFAULT 1,
    BilledQuantity double precision NOT NULL DEFAULT 0,
    BilledUnits varchar(50),
    BilledWhen varchar CHECK (value IN ('one time','daily','weekly','monthly','calendar monthly','quarterly','semi-annually','annually','custom')) NOT NULL DEFAULT One time,
    BilledConverter double precision NOT NULL DEFAULT 1,
    DeliveryQuantity double precision NOT NULL DEFAULT 0,
    DeliveryUnits varchar(50),
    DeliveryConverter double precision NOT NULL DEFAULT 1,
    BillingCode varchar(50),
    Modifier1 varchar(8) NOT NULL DEFAULT ,
    Modifier2 varchar(8) NOT NULL DEFAULT ,
    Modifier3 varchar(8) NOT NULL DEFAULT ,
    Modifier4 varchar(8) NOT NULL DEFAULT ,
    DXPointer varchar(50),
    BillingMonth integer NOT NULL DEFAULT 1,
    BillItemOn varchar CHECK (value IN ('day of delivery','last day of the month','last day of the period','day of pick-up')) NOT NULL DEFAULT Day of Delivery,
    AuthorizationNumber varchar(50),
    AuthorizationTypeID integer,
    ReasonForPickup varchar(50),
    SendCMN_RX_w_invoice smallint NOT NULL DEFAULT 0,
    MedicallyUnnecessary smallint NOT NULL DEFAULT 0,
    Sale smallint NOT NULL DEFAULT 0,
    SpecialCode varchar(50),
    ReviewCode varchar(50),
    NextOrderID integer,
    ReoccuringID integer,
    CMNFormID integer,
    HAOCode varchar(10),
    State varchar CHECK (value IN ('new','approved','pickup','closed','canceled')) NOT NULL DEFAULT New,
    BillIns1 smallint NOT NULL DEFAULT 1,
    BillIns2 smallint NOT NULL DEFAULT 1,
    BillIns3 smallint NOT NULL DEFAULT 1,
    BillIns4 smallint NOT NULL DEFAULT 1,
    EndDate date,
    MIR varchar[] CHECK (value <@ ARRAY['inventoryitemid','pricecodeid','salerenttype','orderedquantity','orderedunits','orderedwhen','orderedconverter','billedquantity','billedunits','billedwhen','billedconverter','deliveryquantity','deliveryunits','deliveryconverter','billingcode','billitemon','dxpointer9','dxpointer10','modifier1','modifier2','modifier3','cmnform.required','cmnform.recertificationdate','cmnform.formexpired','cmnform.mir','enddate.invalid','enddate.unconfirmed','authorizationnumber.expired','authorizationnumber.expires']) NOT NULL DEFAULT ,
    NextBillingDate date,
    WarehouseID integer NOT NULL,
    AcceptAssignment smallint NOT NULL DEFAULT 0,
    DrugNoteField varchar(20),
    DrugControlNumber varchar(50),
    NopayIns1 smallint NOT NULL DEFAULT 0,
    PointerICD10 smallint NOT NULL DEFAULT 0,
    DXPointer10 varchar(50),
    MIR.ORDER varchar[] CHECK (value <@ ARRAY['customer.inactive','customer.mir','policy1.required','policy1.mir','policy2.required','policy2.mir','facility.mir','postype.required','icd9.required','icd9.1.unknown','icd9.1.inactive','icd9.2.unknown','icd9.2.inactive','icd9.3.unknown','icd9.3.inactive','icd9.4.unknown','icd9.4.inactive','icd10.required','icd10.01.unknown','icd10.01.inactive','icd10.02.unknown','icd10.02.inactive','icd10.03.unknown','icd10.03.inactive','icd10.04.unknown','icd10.04.inactive','icd10.05.unknown','icd10.05.inactive','icd10.06.unknown','icd10.06.inactive','icd10.07.unknown','icd10.07.inactive','icd10.08.unknown','icd10.08.inactive','icd10.09.unknown','icd10.09.inactive','icd10.10.unknown','icd10.10.inactive','icd10.11.unknown','icd10.11.inactive','icd10.12.unknown','icd10.12.inactive']) NOT NULL DEFAULT ,
    HaoDescription varchar(100),
    UserField1 varchar(100) NOT NULL DEFAULT ,
    UserField2 varchar(100) NOT NULL DEFAULT ,
    AuthorizationExpirationDate date,
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_orderdetails_CustomerID_idx ON tbl_orderdetails (CustomerID);

CREATE INDEX tbl_orderdetails_OrderID_idx ON tbl_orderdetails (OrderID);

CREATE INDEX tbl_orderdetails_ID_idx ON tbl_orderdetails (ID);

CREATE INDEX tbl_orderdetails_CustomerID_idx ON tbl_orderdetails (CustomerID);

CREATE INDEX tbl_orderdetails_OrderID_idx ON tbl_orderdetails (OrderID);

CREATE INDEX tbl_orderdetails_ID_idx ON tbl_orderdetails (ID);

CREATE INDEX tbl_orderdetails_InventoryItemID_idx ON tbl_orderdetails (InventoryItemID);

CREATE INDEX tbl_orderdetails_CustomerID_idx ON tbl_orderdetails (CustomerID);

CREATE INDEX tbl_orderdetails_NextOrderID_idx ON tbl_orderdetails (NextOrderID);

CREATE INDEX tbl_orderdetails_InventoryItemID_idx ON tbl_orderdetails (InventoryItemID);

CREATE INDEX tbl_orderdetails_SerialNumber_idx ON tbl_orderdetails (SerialNumber);

ALTER TABLE tbl_orderdetails ADD CONSTRAINT FK_NEXTORDER FOREIGN KEY (CustomerID) REFERENCES tbl_order(CustomerID);

ALTER TABLE tbl_orderdetails ADD CONSTRAINT FK_NEXTORDER FOREIGN KEY (NextOrderID) REFERENCES tbl_order(ID);

ALTER TABLE tbl_orderdetails ADD CONSTRAINT FK_ORDER FOREIGN KEY (CustomerID) REFERENCES tbl_order(CustomerID);

ALTER TABLE tbl_orderdetails ADD CONSTRAINT FK_ORDER FOREIGN KEY (OrderID) REFERENCES tbl_order(ID);


CREATE TABLE tbl_order_survey (
    ID integer NOT NULL,
    SurveyID integer NOT NULL,
    OrderID integer NOT NULL,
    Form text NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);

CREATE UNIQUE INDEX tbl_order_survey_OrderID_idx ON tbl_order_survey (OrderID);


CREATE TABLE tbl_payer (
    InsuranceCompanyID integer NOT NULL,
    ParticipatingProvider smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint NOT NULL,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ExtractOrderingPhysician smallint NOT NULL DEFAULT 1,
    ExtractReferringPhysician smallint NOT NULL DEFAULT 0,
    ExtractRenderingProvider smallint NOT NULL DEFAULT 0,
    TaxonomyCodePrefix varchar(10) NOT NULL DEFAULT ,
    PRIMARY KEY (InsuranceCompanyID)
);


CREATE TABLE tbl_paymentplan (
    ID integer NOT NULL,
    CustomerID integer NOT NULL,
    Period varchar CHECK (value IN ('weekly','bi-weekly','monthly')) NOT NULL DEFAULT Weekly,
    FirstPayment date NOT NULL DEFAULT 1900-01-01,
    PaymentCount integer NOT NULL,
    PaymentAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    Details text,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_paymentplan_payments (
    ID integer NOT NULL,
    PaymentPlanID integer NOT NULL,
    CustomerID integer NOT NULL,
    Index integer NOT NULL,
    DueDate date NOT NULL DEFAULT 1900-01-01,
    DueAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    PaymentDate date,
    PaymentAmount decimal(18,2) NOT NULL DEFAULT 0.00,
    Details text,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_permissions (
    UserID smallint NOT NULL,
    ObjectID smallint NOT NULL,
    ADD_EDIT smallint NOT NULL DEFAULT 0,
    DELETE smallint NOT NULL DEFAULT 0,
    PROCESS smallint NOT NULL DEFAULT 0,
    VIEW smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (UserID, ObjectID)
);


CREATE TABLE tbl_postype (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_predefinedtext (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL DEFAULT ,
    Type varchar CHECK (value IN ('document text','account statements','compliance notes','customer notes','invoice notes','hao')) NOT NULL DEFAULT Document Text,
    Text text NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_pricecode (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_pricecode_item (
    AcceptAssignment smallint NOT NULL DEFAULT 0,
    OrderedQuantity double precision NOT NULL DEFAULT 0,
    OrderedUnits varchar(50),
    OrderedWhen varchar CHECK (value IN ('one time','daily','weekly','monthly','quarterly','semi-annually','annually')) NOT NULL DEFAULT One time,
    OrderedConverter double precision NOT NULL DEFAULT 1,
    BilledUnits varchar(50),
    BilledWhen varchar CHECK (value IN ('one time','daily','weekly','monthly','calendar monthly','quarterly','semi-annually','annually','custom')) NOT NULL DEFAULT One time,
    BilledConverter double precision NOT NULL DEFAULT 1,
    DeliveryUnits varchar(50),
    DeliveryConverter double precision NOT NULL DEFAULT 1,
    BillingCode varchar(50),
    BillItemOn varchar CHECK (value IN ('day of delivery','last day of the month','last day of the period','day of pick-up')) NOT NULL DEFAULT Day of Delivery,
    DefaultCMNType varchar CHECK (value IN ('dmerc 02.03a','dmerc 02.03b','dmerc 03.02','dmerc 07.02b','dmerc 08.02','dmerc drorder','dmerc uro','dme 04.04b','dme 04.04c','dme 06.03b','dme 07.03a','dme 09.03','dme 10.03','dme 484.03')) NOT NULL DEFAULT DME 484.03,
    DefaultOrderType varchar CHECK (value IN ('sale','rental')) NOT NULL DEFAULT Sale,
    AuthorizationTypeID integer,
    FlatRate smallint NOT NULL DEFAULT 0,
    ID integer NOT NULL,
    InventoryItemID integer NOT NULL DEFAULT 0,
    Modifier1 varchar(8) NOT NULL DEFAULT ,
    Modifier2 varchar(8) NOT NULL DEFAULT ,
    Modifier3 varchar(8) NOT NULL DEFAULT ,
    Modifier4 varchar(8) NOT NULL DEFAULT ,
    PriceCodeID integer NOT NULL DEFAULT 0,
    PredefinedTextID integer,
    Rent_AllowablePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    Rent_BillablePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    Sale_AllowablePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    Sale_BillablePrice decimal(18,2) NOT NULL DEFAULT 0.00,
    RentalType varchar CHECK (value IN ('medicare oxygen rental','one time rental','monthly rental','capped rental','parental capped rental','rent to purchase')) NOT NULL DEFAULT Monthly Rental,
    ReoccuringSale smallint NOT NULL DEFAULT 0,
    ShowSpanDates smallint NOT NULL DEFAULT 0,
    Taxable smallint NOT NULL DEFAULT 0,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    BillInsurance smallint NOT NULL DEFAULT 1,
    DrugNoteField varchar(20),
    DrugControlNumber varchar(50),
    UserField1 varchar(100) NOT NULL DEFAULT ,
    UserField2 varchar(100) NOT NULL DEFAULT ,
    PRIMARY KEY (ID)
);

CREATE UNIQUE INDEX tbl_pricecode_item_InventoryItemID_idx ON tbl_pricecode_item (InventoryItemID);

CREATE UNIQUE INDEX tbl_pricecode_item_PriceCodeID_idx ON tbl_pricecode_item (PriceCodeID);


CREATE TABLE tbl_producttype (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_provider (
    ID integer NOT NULL,
    LocationID integer NOT NULL DEFAULT 0,
    InsuranceCompanyID integer NOT NULL DEFAULT 0,
    ProviderNumber varchar(25) NOT NULL DEFAULT ,
    Password varchar(20) NOT NULL DEFAULT ,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ProviderNumberType varchar(6) NOT NULL DEFAULT 1C,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_providernumbertype (
    Code varchar(6) NOT NULL,
    Description varchar(100) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_purchaseorder (
    Approved smallint NOT NULL DEFAULT 0,
    Cost decimal(18,2) NOT NULL DEFAULT 0.00,
    Freight decimal(18,2) NOT NULL DEFAULT 0.00,
    ID integer NOT NULL,
    Tax decimal(18,2) NOT NULL DEFAULT 0.00,
    TotalDue decimal(18,2) NOT NULL DEFAULT 0.00,
    VendorID integer NOT NULL,
    ShipToName varchar(50) NOT NULL,
    ShipToAddress1 varchar(40) NOT NULL,
    ShipToAddress2 varchar(40) NOT NULL,
    ShipToCity varchar(25) NOT NULL,
    ShipToState varchar(2) NOT NULL,
    ShipToZip varchar(10) NOT NULL,
    ShipToPhone varchar(50) NOT NULL,
    OrderDate date,
    CompanyName varchar(50) NOT NULL,
    CompanyAddress1 varchar(40) NOT NULL,
    CompanyAddress2 varchar(40) NOT NULL,
    CompanyCity varchar(25) NOT NULL,
    CompanyState varchar(2) NOT NULL,
    CompanyZip varchar(10) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ShipVia varchar CHECK (value IN ('best way','ups/rps')),
    FOB varchar(50),
    VendorSalesRep varchar(50),
    Terms text,
    CompanyPhone varchar(50),
    TaxRateID integer,
    Reoccuring smallint NOT NULL DEFAULT 0,
    CreatedDate date,
    CreatedUserID smallint,
    SubmittedDate date,
    SubmittedUserID smallint,
    LocationID integer,
    Number varchar(40) NOT NULL DEFAULT ,
    Archived smallint NOT NULL DEFAULT 0,
    ConfirmationNumber varchar(50),
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_purchaseorder_LocationID_idx ON tbl_purchaseorder (LocationID);

CREATE INDEX tbl_purchaseorder_ID_idx ON tbl_purchaseorder (ID);

CREATE INDEX tbl_purchaseorder_Number_idx ON tbl_purchaseorder (Number);

CREATE INDEX tbl_purchaseorder_VendorID_idx ON tbl_purchaseorder (VendorID);

CREATE INDEX tbl_purchaseorder_OrderDate_idx ON tbl_purchaseorder (OrderDate);

CREATE INDEX tbl_purchaseorder_SubmittedDate_idx ON tbl_purchaseorder (SubmittedDate);

CREATE INDEX tbl_purchaseorder_Approved_idx ON tbl_purchaseorder (Approved);


CREATE TABLE tbl_purchaseorderdetails (
    BackOrder integer NOT NULL DEFAULT 0,
    Ordered integer NOT NULL DEFAULT 0,
    Received integer NOT NULL DEFAULT 0,
    Price double precision NOT NULL DEFAULT 0,
    Customer varchar(50),
    DatePromised date,
    DateReceived date,
    DropShipToCustomer smallint NOT NULL DEFAULT 0,
    ID integer NOT NULL,
    InventoryItemID integer NOT NULL,
    PurchaseOrderID integer,
    WarehouseID integer,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    VendorSTKNumber varchar(50),
    ReferenceNumber varchar(50),
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_purchaseorderdetails_PurchaseOrderID_idx ON tbl_purchaseorderdetails (PurchaseOrderID);


CREATE TABLE tbl_referral (
    Address1 varchar(40) NOT NULL,
    Address2 varchar(40) NOT NULL,
    City varchar(25) NOT NULL,
    Courtesy varchar CHECK (value IN ('dr.','miss','mr.','mrs.','rev.')) NOT NULL,
    Employer varchar(50) NOT NULL,
    Fax varchar(50) NOT NULL,
    FirstName varchar(25) NOT NULL,
    HomePhone varchar(50) NOT NULL,
    ID integer NOT NULL,
    LastName varchar(30) NOT NULL,
    MiddleName varchar(1) NOT NULL,
    Mobile varchar(50) NOT NULL,
    ReferralTypeID integer,
    State varchar(2) NOT NULL,
    Suffix varchar(4) NOT NULL,
    WorkPhone varchar(50) NOT NULL,
    Zip varchar(10) NOT NULL,
    LastContacted date,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_referraltype (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_relationship (
    Code char(2) NOT NULL DEFAULT ,
    Description varchar(100) NOT NULL DEFAULT ,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_salesrep (
    Address1 varchar(40) NOT NULL,
    Address2 varchar(40) NOT NULL,
    City varchar(25) NOT NULL,
    Courtesy varchar CHECK (value IN ('dr.','miss','mr.','mrs.','rev.')) NOT NULL,
    FirstName varchar(25) NOT NULL,
    HomePhone varchar(50) NOT NULL,
    ID integer NOT NULL,
    LastName varchar(30) NOT NULL,
    MiddleName varchar(1) NOT NULL,
    Mobile varchar(50) NOT NULL,
    Pager varchar(50) NOT NULL,
    State varchar(2) NOT NULL,
    Suffix varchar(4) NOT NULL,
    Zip varchar(10) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_serial (
    ID integer NOT NULL,
    CurrentCustomerID integer,
    InventoryItemID integer NOT NULL DEFAULT 0,
    LastCustomerID integer,
    ManufacturerID integer,
    VendorID integer,
    WarehouseID integer,
    LengthOfWarranty varchar(50) NOT NULL DEFAULT ,
    LotNumber varchar(50) NOT NULL DEFAULT ,
    MaintenanceRecord text NOT NULL,
    ManufaturerSerialNumber varchar(50) NOT NULL DEFAULT ,
    ModelNumber varchar(50) NOT NULL DEFAULT ,
    MonthsRented varchar(50) NOT NULL DEFAULT ,
    NextMaintenanceDate date,
    PurchaseOrderID integer,
    PurchaseAmount double precision NOT NULL DEFAULT 0,
    PurchaseDate date,
    SerialNumber varchar(50) NOT NULL DEFAULT ,
    SoldDate date,
    Status varchar CHECK (value IN ('empty','filled','junked','lost','reserved','on hand','rented','sold','sent','maintenance','transferred out')) NOT NULL DEFAULT Empty,
    Warranty varchar(50) NOT NULL DEFAULT ,
    OwnRent varchar CHECK (value IN ('own','rent')) NOT NULL DEFAULT Own,
    FirstRented date,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    SalvageValue decimal(18,2),
    SalePrice decimal(18,2),
    ConsignmentType varchar(20),
    ConsignmentName varchar(50),
    ConsignmentDate timestamp,
    VendorStockNumber varchar(20),
    LotNumberExpires timestamp,
    PRIMARY KEY (ID)
);

CREATE INDEX tbl_serial_InventoryItemID_idx ON tbl_serial (InventoryItemID);

CREATE INDEX tbl_serial_SerialNumber_idx ON tbl_serial (SerialNumber);


CREATE TABLE tbl_serial_maintenance (
    ID integer NOT NULL,
    SerialID integer NOT NULL,
    AdditionalEquipment text,
    DescriptionOfProblem text,
    DescriptionOfWork text,
    MaintenanceRecord text,
    LaborHours varchar(255),
    Technician varchar(255),
    MaintenanceDue date,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MaintenanceCost decimal(18,2) NOT NULL DEFAULT 0.00,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_serial_transaction (
    ID integer NOT NULL,
    TypeID integer NOT NULL DEFAULT 0,
    SerialID integer NOT NULL DEFAULT 0,
    TransactionDatetime timestamp NOT NULL,
    VendorID integer,
    WarehouseID integer,
    CustomerID integer,
    OrderID integer,
    OrderDetailsID integer,
    LotNumber varchar(50) NOT NULL DEFAULT ,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_serial_transaction_type (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_sessions (
    ID integer NOT NULL,
    UserID smallint NOT NULL,
    LoginTime timestamp NOT NULL,
    LastUpdateTime timestamp NOT NULL,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_shippingmethod (
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Type varchar(50),
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_signaturetype (
    Code char(1) NOT NULL DEFAULT ,
    Description varchar(100) NOT NULL DEFAULT ,
    PRIMARY KEY (Code)
);


CREATE TABLE tbl_submitter (
    ID integer NOT NULL,
    ECSFormat varchar CHECK (value IN ('region a','region b','region c','region d')),
    Name varchar(50) NOT NULL DEFAULT ,
    Number varchar(16) NOT NULL DEFAULT ,
    Password varchar(50) NOT NULL DEFAULT ,
    Production smallint NOT NULL DEFAULT 0,
    ContactName varchar(50) NOT NULL DEFAULT ,
    Address1 varchar(40) NOT NULL DEFAULT ,
    Address2 varchar(40) NOT NULL DEFAULT ,
    City varchar(25) NOT NULL DEFAULT ,
    State char(2) NOT NULL DEFAULT ,
    Zip varchar(10) NOT NULL DEFAULT ,
    Phone1 varchar(50) NOT NULL DEFAULT ,
    LastBatchNumber varchar(50) NOT NULL DEFAULT ,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_survey (
    ID integer NOT NULL,
    Name varchar(100) NOT NULL,
    Description varchar(200) NOT NULL,
    Template text NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_taxrate (
    ID integer NOT NULL,
    CityTax double precision,
    CountyTax double precision,
    Name varchar(50) NOT NULL,
    OtherTax double precision,
    StateTax double precision,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_user (
    ID smallint NOT NULL,
    Login varchar(16) NOT NULL,
    Password varchar(32) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Email varchar(150) NOT NULL DEFAULT ,
    PRIMARY KEY (ID)
);

CREATE UNIQUE INDEX tbl_user_Login_idx ON tbl_user (Login);


CREATE TABLE tbl_user_location (
    UserID smallint NOT NULL,
    LocationID integer NOT NULL,
    PRIMARY KEY (UserID, LocationID)
);

CREATE UNIQUE INDEX tbl_user_location_LocationID_idx ON tbl_user_location (LocationID);

CREATE UNIQUE INDEX tbl_user_location_UserID_idx ON tbl_user_location (UserID);


CREATE TABLE tbl_user_notifications (
    ID integer NOT NULL,
    Type varchar(50) NOT NULL,
    Args varchar(255) NOT NULL,
    UserID smallint NOT NULL,
    Datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_variables (
    Name varchar(31) NOT NULL,
    Value varchar(255) NOT NULL,
    PRIMARY KEY (Name)
);


CREATE TABLE tbl_vendor (
    AccountNumber varchar(40) NOT NULL,
    Address1 varchar(40) NOT NULL,
    Address2 varchar(40) NOT NULL,
    City varchar(25) NOT NULL,
    Contact varchar(50) NOT NULL,
    Fax varchar(50) NOT NULL,
    ID integer NOT NULL,
    Name varchar(50) NOT NULL,
    Phone varchar(50) NOT NULL,
    Phone2 varchar(50) NOT NULL,
    State varchar(2) NOT NULL,
    Zip varchar(10) NOT NULL,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Comments text,
    FOBDelivery varchar(50),
    Terms varchar(50),
    ShipVia varchar(50),
    PRIMARY KEY (ID)
);


CREATE TABLE tbl_warehouse (
    Address1 varchar(40) NOT NULL DEFAULT ,
    Address2 varchar(40) NOT NULL DEFAULT ,
    City varchar(25) NOT NULL DEFAULT ,
    Contact varchar(50) NOT NULL DEFAULT ,
    Fax varchar(50) NOT NULL DEFAULT ,
    ID integer NOT NULL,
    Name varchar(50) NOT NULL DEFAULT ,
    Phone varchar(50) NOT NULL DEFAULT ,
    Phone2 varchar(50) NOT NULL DEFAULT ,
    State char(2) NOT NULL DEFAULT ,
    Zip varchar(10) NOT NULL DEFAULT ,
    LastUpdateUserID smallint,
    LastUpdateDatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ID)
);


CREATE VIEW tbl_doctor AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_doctor` AS select `dmeworks`.`tbl_doctor`.`Address1` AS `Address1`,`dmeworks`.`tbl_doctor`.`Address2` AS `Address2`,`dmeworks`.`tbl_doctor`.`City` AS `City`,`dmeworks`.`tbl_doctor`.`Contact` AS `Contact`,`dmeworks`.`tbl_doctor`.`Courtesy` AS `Courtesy`,`dmeworks`.`tbl_doctor`.`Fax` AS `Fax`,`dmeworks`.`tbl_doctor`.`FirstName` AS `FirstName`,`dmeworks`.`tbl_doctor`.`ID` AS `ID`,`dmeworks`.`tbl_doctor`.`LastName` AS `LastName`,`dmeworks`.`tbl_doctor`.`LicenseNumber` AS `LicenseNumber`,`dmeworks`.`tbl_doctor`.`LicenseExpired` AS `LicenseExpired`,`dmeworks`.`tbl_doctor`.`MedicaidNumber` AS `MedicaidNumber`,`dmeworks`.`tbl_doctor`.`MiddleName` AS `MiddleName`,`dmeworks`.`tbl_doctor`.`OtherID` AS `OtherID`,`dmeworks`.`tbl_doctor`.`FEDTaxID` AS `FEDTaxID`,`dmeworks`.`tbl_doctor`.`DEANumber` AS `DEANumber`,`dmeworks`.`tbl_doctor`.`Phone` AS `Phone`,`dmeworks`.`tbl_doctor`.`Phone2` AS `Phone2`,`dmeworks`.`tbl_doctor`.`State` AS `State`,`dmeworks`.`tbl_doctor`.`Suffix` AS `Suffix`,`dmeworks`.`tbl_doctor`.`Title` AS `Title`,`dmeworks`.`tbl_doctor`.`TypeID` AS `TypeID`,`dmeworks`.`tbl_doctor`.`UPINNumber` AS `UPINNumber`,`dmeworks`.`tbl_doctor`.`Zip` AS `Zip`,`dmeworks`.`tbl_doctor`.`LastUpdateUserID` AS `LastUpdateUserID`,`dmeworks`.`tbl_doctor`.`LastUpdateDatetime` AS `LastUpdateDatetime`,`dmeworks`.`tbl_doctor`.`MIR` AS `MIR`,`dmeworks`.`tbl_doctor`.`NPI` AS `NPI`,`dmeworks`.`tbl_doctor`.`PecosEnrolled` AS `PecosEnrolled` from `dmeworks`.`tbl_doctor`
;

CREATE VIEW tbl_doctortype AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_doctortype` AS select `dmeworks`.`tbl_doctortype`.`ID` AS `ID`,`dmeworks`.`tbl_doctortype`.`Name` AS `Name`,`dmeworks`.`tbl_doctortype`.`LastUpdateUserID` AS `LastUpdateUserID`,`dmeworks`.`tbl_doctortype`.`LastUpdateDatetime` AS `LastUpdateDatetime` from `dmeworks`.`tbl_doctortype`
;

CREATE VIEW tbl_icd10 AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_icd10` AS select `dmeworks`.`tbl_icd10`.`Code` AS `Code`,`dmeworks`.`tbl_icd10`.`Description` AS `Description`,`dmeworks`.`tbl_icd10`.`Header` AS `Header`,`dmeworks`.`tbl_icd10`.`ActiveDate` AS `ActiveDate`,`dmeworks`.`tbl_icd10`.`InactiveDate` AS `InactiveDate`,`dmeworks`.`tbl_icd10`.`LastUpdateUserID` AS `LastUpdateUserID`,`dmeworks`.`tbl_icd10`.`LastUpdateDatetime` AS `LastUpdateDatetime` from `dmeworks`.`tbl_icd10` where (`dmeworks`.`tbl_icd10`.`Header` = 0)
;

CREATE VIEW tbl_icd9 AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_icd9` AS select `dmeworks`.`tbl_icd9`.`Code` AS `Code`,`dmeworks`.`tbl_icd9`.`Description` AS `Description`,`dmeworks`.`tbl_icd9`.`ActiveDate` AS `ActiveDate`,`dmeworks`.`tbl_icd9`.`InactiveDate` AS `InactiveDate`,`dmeworks`.`tbl_icd9`.`LastUpdateUserID` AS `LastUpdateUserID`,`dmeworks`.`tbl_icd9`.`LastUpdateDatetime` AS `LastUpdateDatetime` from `dmeworks`.`tbl_icd9`
;

CREATE VIEW tbl_insurancecompany AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_insurancecompany` AS select `dmeworks`.`tbl_insurancecompany`.`Address1` AS `Address1`,`dmeworks`.`tbl_insurancecompany`.`Address2` AS `Address2`,`dmeworks`.`tbl_insurancecompany`.`Basis` AS `Basis`,`dmeworks`.`tbl_insurancecompany`.`City` AS `City`,`dmeworks`.`tbl_insurancecompany`.`Contact` AS `Contact`,`dmeworks`.`tbl_insurancecompany`.`ECSFormat` AS `ECSFormat`,`dmeworks`.`tbl_insurancecompany`.`ExpectedPercent` AS `ExpectedPercent`,`dmeworks`.`tbl_insurancecompany`.`Fax` AS `Fax`,`dmeworks`.`tbl_insurancecompany`.`ID` AS `ID`,`dmeworks`.`tbl_insurancecompany`.`Name` AS `Name`,`dmeworks`.`tbl_insurancecompany`.`Phone` AS `Phone`,`dmeworks`.`tbl_insurancecompany`.`Phone2` AS `Phone2`,`dmeworks`.`tbl_insurancecompany`.`PriceCodeID` AS `PriceCodeID`,`dmeworks`.`tbl_insurancecompany`.`PrintHAOOnInvoice` AS `PrintHAOOnInvoice`,`dmeworks`.`tbl_insurancecompany`.`PrintInvOnInvoice` AS `PrintInvOnInvoice`,`dmeworks`.`tbl_insurancecompany`.`State` AS `State`,`dmeworks`.`tbl_insurancecompany`.`Title` AS `Title`,`dmeworks`.`tbl_insurancecompany`.`Type` AS `Type`,`dmeworks`.`tbl_insurancecompany`.`Zip` AS `Zip`,`dmeworks`.`tbl_insurancecompany`.`MedicareNumber` AS `MedicareNumber`,`dmeworks`.`tbl_insurancecompany`.`OfficeAllyNumber` AS `OfficeAllyNumber`,`dmeworks`.`tbl_insurancecompany`.`ZirmedNumber` AS `ZirmedNumber`,`dmeworks`.`tbl_insurancecompany`.`LastUpdateUserID` AS `LastUpdateUserID`,`dmeworks`.`tbl_insurancecompany`.`LastUpdateDatetime` AS `LastUpdateDatetime`,`dmeworks`.`tbl_insurancecompany`.`InvoiceFormID` AS `InvoiceFormID`,`dmeworks`.`tbl_insurancecompany`.`MedicaidNumber` AS `MedicaidNumber`,`dmeworks`.`tbl_insurancecompany`.`MIR` AS `MIR`,`dmeworks`.`tbl_insurancecompany`.`GroupID` AS `GroupID`,`dmeworks`.`tbl_insurancecompany`.`AvailityNumber` AS `AvailityNumber`,`dmeworks`.`tbl_insurancecompany`.`AbilityNumber` AS `AbilityNumber` from `dmeworks`.`tbl_insurancecompany`
;

CREATE VIEW tbl_insurancecompanygroup AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_insurancecompanygroup` AS select `dmeworks`.`tbl_insurancecompanygroup`.`ID` AS `ID`,`dmeworks`.`tbl_insurancecompanygroup`.`Name` AS `Name`,`dmeworks`.`tbl_insurancecompanygroup`.`LastUpdateUserID` AS `LastUpdateUserID`,`dmeworks`.`tbl_insurancecompanygroup`.`LastUpdateDatetime` AS `LastUpdateDatetime` from `dmeworks`.`tbl_insurancecompanygroup`
;

CREATE VIEW tbl_insurancecompanytype AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_insurancecompanytype` AS select `dmeworks`.`tbl_insurancecompanytype`.`ID` AS `ID`,`dmeworks`.`tbl_insurancecompanytype`.`Name` AS `Name` from `dmeworks`.`tbl_insurancecompanytype`
;

CREATE VIEW tbl_zipcode AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `tbl_zipcode` AS select `dmeworks`.`tbl_zipcode`.`Zip` AS `Zip`,`dmeworks`.`tbl_zipcode`.`State` AS `State`,`dmeworks`.`tbl_zipcode`.`City` AS `City` from `dmeworks`.`tbl_zipcode`
;

CREATE VIEW view_billinglist AS
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_billinglist` AS select distinct `details`.`OrderID` AS `OrderID`,if((`details`.`BillingMonth` <= 0),1,`details`.`BillingMonth`) AS `BillingMonth`,(((((if(((`tbl_order`.`CustomerInsurance1_ID` is not null) and (`details`.`BillIns1` = 1)),1,0) + if(((`tbl_order`.`CustomerInsurance2_ID` is not null) and (`details`.`BillIns2` = 1)),2,0)) + if(((`tbl_order`.`CustomerInsurance3_ID` is not null) and (`details`.`BillIns3` = 1)),4,0)) + if(((`tbl_order`.`CustomerInsurance4_ID` is not null) and (`details`.`BillIns4` = 1)),8,0)) + if((`details`.`EndDate` is not null),32,0)) + if((`details`.`AcceptAssignment` = 1),16,0)) AS `BillingFlags`,`tbl_customer`.`BillingTypeID` AS `BillingTypeID` from ((`view_orderdetails_core` `details` join `tbl_order` on(((`tbl_order`.`ID` = `details`.`OrderID`) and (`tbl_order`.`CustomerID` = `details`.`CustomerID`)))) left join `tbl_customer` on((`tbl_customer`.`ID` = `tbl_order`.`CustomerID`))) where (((`details`.`InvoiceDate` <= curdate()) or (`details`.`EndDate` <= curdate())) and (`details`.`MIR` = '') and (`details`.`IsActive` = 1) and (isnull(`details`.`EndDate`) or (`tbl_order`.`BillDate` < `details`.`EndDate`)) and (`tbl_order`.`MIR` = '') and (`tbl_order`.`Approved` = 1) and (`tbl_order`.`OrderDate` is not null) and (`tbl_order`.`BillDate` is not null))
;

CREATE VIEW view_invoicetransaction_statistics AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_invoicetransaction_statistics` AS select sql_small_result `detail`.`CustomerID` AS `CustomerID`,`detail`.`OrderID` AS `OrderID`,`detail`.`InvoiceID` AS `InvoiceID`,`detail`.`ID` AS `InvoiceDetailsID`,`detail`.`BillableAmount` AS `BillableAmount`,`detail`.`AllowableAmount` AS `AllowableAmount`,`detail`.`Quantity` AS `Quantity`,`detail`.`Hardship` AS `Hardship`,`detail`.`BillingCode` AS `BillingCode`,`detail`.`InventoryItemID` AS `InventoryItemID`,`detail`.`DOSFrom` AS `DOSFrom`,`detail`.`DOSTo` AS `DOSTo`,`insurance1`.`ID` AS `Insurance1_ID`,`insurance2`.`ID` AS `Insurance2_ID`,`insurance3`.`ID` AS `Insurance3_ID`,`insurance4`.`ID` AS `Insurance4_ID`,`insurance1`.`InsuranceCompanyID` AS `InsuranceCompany1_ID`,`insurance2`.`InsuranceCompanyID` AS `InsuranceCompany2_ID`,`insurance3`.`InsuranceCompanyID` AS `InsuranceCompany3_ID`,`insurance4`.`InsuranceCompanyID` AS `InsuranceCompany4_ID`,(case when (ifnull(`insurance1`.`PaymentPercent`,0) < 0) then 0 when (100 < ifnull(`insurance1`.`PaymentPercent`,0)) then 100 else ifnull(`insurance1`.`PaymentPercent`,0) end) AS `Percent`,ifnull(`insurance1`.`Basis`,'Bill') AS `Basis`,`detail`.`PaymentAmount` AS `PaymentAmount`,`detail`.`WriteoffAmount` AS `WriteoffAmount`,((((if((`insurance1`.`ID` is not null),1,0) + if((`insurance2`.`ID` is not null),2,0)) + if((`insurance3`.`ID` is not null),4,0)) + if((`insurance4`.`ID` is not null),8,0)) + if((1 = 1),16,0)) AS `Insurances`,((((if((`insurance1`.`ID` is not null),(`detail`.`Pendings` & 1),0) + if((`insurance2`.`ID` is not null),(`detail`.`Pendings` & 2),0)) + if((`insurance3`.`ID` is not null),(`detail`.`Pendings` & 4),0)) + if((`insurance4`.`ID` is not null),(`detail`.`Pendings` & 8),0)) + if((1 = 1),(`detail`.`Pendings` & 16),0)) AS `PendingSubmissions`,((((if((`insurance1`.`ID` is not null),(`detail`.`Submits` & 1),0) + if((`insurance2`.`ID` is not null),(`detail`.`Submits` & 2),0)) + if((`insurance3`.`ID` is not null),(`detail`.`Submits` & 4),0)) + if((`insurance4`.`ID` is not null),(`detail`.`Submits` & 8),0)) + if((1 = 1),(`detail`.`Submits` & 16),0)) AS `Submits`,((((if((`insurance1`.`ID` is not null),(`detail`.`Payments` & 1),0) + if((`insurance2`.`ID` is not null),(`detail`.`Payments` & 2),0)) + if((`insurance3`.`ID` is not null),(`detail`.`Payments` & 4),0)) + if((`insurance4`.`ID` is not null),(`detail`.`Payments` & 8),0)) + if((1 = 1),(`detail`.`Payments` & 16),0)) AS `Payments`,`detail`.`CurrentCustomerInsuranceID` AS `CurrentInsuranceID`,`detail`.`CurrentInsuranceCompanyID` AS `CurrentInsuranceCompanyID`,`detail`.`Submitted` AS `InvoiceSubmitted`,`detail`.`SubmittedDate` AS `SubmittedDate`,`detail`.`CurrentPayer` AS `CurrentPayer`,`detail`.`NopayIns1` AS `NopayIns1` from (((((`tbl_invoicedetails` `detail` join `tbl_invoice` `invoice` on(((`invoice`.`CustomerID` = `detail`.`CustomerID`) and (`invoice`.`ID` = `detail`.`InvoiceID`)))) left join `tbl_customer_insurance` `insurance1` on(((`insurance1`.`ID` = `invoice`.`CustomerInsurance1_ID`) and (`insurance1`.`CustomerID` = `invoice`.`CustomerID`) and (`detail`.`BillIns1` = 1)))) left join `tbl_customer_insurance` `insurance2` on(((`insurance2`.`ID` = `invoice`.`CustomerInsurance2_ID`) and (`insurance2`.`CustomerID` = `invoice`.`CustomerID`) and (`detail`.`BillIns2` = 1)))) left join `tbl_customer_insurance` `insurance3` on(((`insurance3`.`ID` = `invoice`.`CustomerInsurance3_ID`) and (`insurance3`.`CustomerID` = `invoice`.`CustomerID`) and (`detail`.`BillIns3` = 1)))) left join `tbl_customer_insurance` `insurance4` on(((`insurance4`.`ID` = `invoice`.`CustomerInsurance4_ID`) and (`insurance4`.`CustomerID` = `invoice`.`CustomerID`) and (`detail`.`BillIns4` = 1))))
;

CREATE VIEW view_mir AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_mir` AS select `tbl_details`.`ID` AS `OrderDetailsID`,`c01`.`tbl_order`.`ID` AS `OrderID`,`c01`.`tbl_order`.`Approved` AS `OrderApproved`,`c01`.`tbl_customer`.`ID` AS `CustomerID`,concat(`c01`.`tbl_customer`.`LastName`,' ',`c01`.`tbl_customer`.`FirstName`) AS `CustomerName`,`policy1`.`ID` AS `CustomerInsuranceID_1`,`insco1`.`ID` AS `InsuranceCompanyID_1`,`policy2`.`ID` AS `CustomerInsuranceID_2`,`insco2`.`ID` AS `InsuranceCompanyID_2`,`c01`.`tbl_cmnform`.`ID` AS `CMNFormID`,`c01`.`tbl_facility`.`ID` AS `FacilityID`,`tbl_doctor`.`ID` AS `DoctorID`,`tbl_details`.`SaleRentType` AS `SaleRentType`,`tbl_details`.`BillingCode` AS `BillingCode`,concat_ws(', ',if((`tbl_details`.`BillIns1` = 1),'Ins1',NULL),if((`tbl_details`.`BillIns2` = 1),'Ins2',NULL),if((`tbl_details`.`BillIns3` = 1),'Ins3',NULL),if((`tbl_details`.`BillIns4` = 1),'Ins4',NULL),'Patient') AS `Payers`,`c01`.`tbl_inventoryitem`.`Name` AS `InventoryItem`,`c01`.`tbl_pricecode`.`Name` AS `PriceCode`,concat_ws(', ',if((`c01`.`tbl_order`.`MIR` <> ''),'order',NULL),if((`tbl_details`.`MIR` <> ''),'details',NULL),if((0 < find_in_set('CustomerID',`c01`.`tbl_order`.`MIR`)),'customer required',NULL),if((0 < find_in_set('Customer.Inactive',`c01`.`tbl_order`.`MIR`)),'customer inactive',NULL),if((0 < find_in_set('Customer.MIR',`c01`.`tbl_order`.`MIR`)),'customer',NULL),if((0 < find_in_set('Facility.MIR',`c01`.`tbl_order`.`MIR`)),'facility',NULL),if((0 < find_in_set('Policy1.Required',`c01`.`tbl_order`.`MIR`)),'policy1 required',NULL),if((0 < find_in_set('Policy1.MIR',`c01`.`tbl_order`.`MIR`)),'policy1',NULL),if((0 < find_in_set('Policy2.Required',`c01`.`tbl_order`.`MIR`)),'policy2 required',NULL),if((0 < find_in_set('Policy2.MIR',`c01`.`tbl_order`.`MIR`)),'policy2',NULL),if((0 < find_in_set('CMNForm.Required',`tbl_details`.`MIR`)),'cmn form required',NULL),if((0 < find_in_set('CMNForm.MIR',`tbl_details`.`MIR`)),'cmn form',NULL),if((0 < find_in_set('Answers',`c01`.`tbl_cmnform`.`MIR`)),'cmn answers',NULL),NULL) AS `MIR`,concat_ws('\r\n',if((`c01`.`tbl_order`.`MIR` <> ''),replace(concat('Order: ',cast(`c01`.`tbl_order`.`MIR` as char charset latin1)),',',', '),NULL),if((`tbl_details`.`MIR` <> ''),replace(concat('Details: ',cast(`tbl_details`.`MIR` as char charset latin1)),',',', '),NULL),if((`c01`.`tbl_customer`.`MIR` <> ''),replace(concat('Customer: ',cast(`c01`.`tbl_customer`.`MIR` as char charset latin1)),',',', '),NULL),if((`tbl_doctor`.`MIR` <> ''),replace(concat('Doctor: ',cast(`tbl_doctor`.`MIR` as char charset latin1)),',',', '),NULL),if((`policy1`.`MIR` <> ''),replace(concat('Policy 1: ',cast(`policy1`.`MIR` as char charset latin1)),',',', '),NULL),if((`insco1`.`MIR` <> ''),replace(concat('Ins Co 1: ',cast(`insco1`.`MIR` as char charset latin1)),',',', '),NULL),if((`policy2`.`MIR` <> ''),replace(concat('Policy 2: ',cast(`policy2`.`MIR` as char charset latin1)),',',', '),NULL),if((`insco2`.`MIR` <> ''),replace(concat('Ins Co 2: ',cast(`insco2`.`MIR` as char charset latin1)),',',', '),NULL),if((`c01`.`tbl_cmnform`.`MIR` <> ''),replace(concat('CMN Form: ',cast(`c01`.`tbl_cmnform`.`MIR` as char charset latin1)),',',', '),NULL),if((`c01`.`tbl_facility`.`MIR` <> ''),replace(concat('Facility: ',cast(`c01`.`tbl_facility`.`MIR` as char charset latin1)),',',', '),NULL),NULL) AS `Details` from ((((((((((((`c01`.`view_orderdetails_core` `tbl_details` join `c01`.`tbl_order` on(((`tbl_details`.`OrderID` = `c01`.`tbl_order`.`ID`) and (`tbl_details`.`CustomerID` = `c01`.`tbl_order`.`CustomerID`)))) join `c01`.`tbl_customer` on((`tbl_details`.`CustomerID` = `c01`.`tbl_customer`.`ID`))) left join `c01`.`tbl_doctor` on((`c01`.`tbl_customer`.`Doctor1_ID` = `tbl_doctor`.`ID`))) left join `c01`.`tbl_facility` on((`c01`.`tbl_order`.`FacilityID` = `c01`.`tbl_facility`.`ID`))) left join `c01`.`tbl_cmnform` on(((`tbl_details`.`CMNFormID` = `c01`.`tbl_cmnform`.`ID`) and (`tbl_details`.`CustomerID` = `c01`.`tbl_cmnform`.`CustomerID`)))) left join `c01`.`tbl_inventoryitem` on((`tbl_details`.`InventoryItemID` = `c01`.`tbl_inventoryitem`.`ID`))) left join `c01`.`tbl_pricecode` on((`tbl_details`.`PriceCodeID` = `c01`.`tbl_pricecode`.`ID`))) left join `c01`.`tbl_customer_insurance` `policy1` on(((`policy1`.`CustomerID` = `c01`.`tbl_order`.`CustomerID`) and (`policy1`.`ID` = `c01`.`tbl_order`.`CustomerInsurance1_ID`)))) left join `c01`.`tbl_insurancecompany` `insco1` on((`insco1`.`ID` = `policy1`.`InsuranceCompanyID`))) left join `c01`.`tbl_customer_insurance` `policy2` on(((`policy2`.`CustomerID` = `c01`.`tbl_order`.`CustomerID`) and (`policy2`.`ID` = `c01`.`tbl_order`.`CustomerInsurance2_ID`) and (`tbl_details`.`BillIns2` = 1)))) left join `c01`.`tbl_insurancecompany` `insco2` on((`insco2`.`ID` = `policy2`.`InsuranceCompanyID`))) join `c01`.`tbl_company` on((`c01`.`tbl_company`.`ID` = 1))) where (((`c01`.`tbl_company`.`Show_InactiveCustomers` = 1) or isnull(`c01`.`tbl_customer`.`InactiveDate`) or (now() < `c01`.`tbl_customer`.`InactiveDate`)) and (`tbl_details`.`IsActive` = 1) and ((`tbl_details`.`MIR` <> '') or (`c01`.`tbl_order`.`MIR` <> '')))
;

CREATE VIEW view_orderdetails AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_orderdetails` AS select `det`.`ID` AS `ID`,`det`.`OrderID` AS `OrderID`,`det`.`CustomerID` AS `CustomerID`,`det`.`SerialNumber` AS `SerialNumber`,`det`.`InventoryItemID` AS `InventoryItemID`,`det`.`PriceCodeID` AS `PriceCodeID`,`det`.`SaleRentType` AS `SaleRentType`,`det`.`SerialID` AS `SerialID`,`det`.`BillablePrice` AS `BillablePrice`,`det`.`AllowablePrice` AS `AllowablePrice`,`det`.`Taxable` AS `Taxable`,`det`.`FlatRate` AS `FlatRate`,`det`.`DOSFrom` AS `DOSFrom`,`det`.`DOSTo` AS `DOSTo`,`det`.`PickupDate` AS `PickupDate`,`det`.`ShowSpanDates` AS `ShowSpanDates`,`det`.`OrderedQuantity` AS `OrderedQuantity`,`det`.`OrderedUnits` AS `OrderedUnits`,`det`.`OrderedWhen` AS `OrderedWhen`,`det`.`OrderedConverter` AS `OrderedConverter`,`det`.`BilledQuantity` AS `BilledQuantity`,`det`.`BilledUnits` AS `BilledUnits`,`det`.`BilledWhen` AS `BilledWhen`,`det`.`BilledConverter` AS `BilledConverter`,`det`.`DeliveryQuantity` AS `DeliveryQuantity`,`det`.`DeliveryUnits` AS `DeliveryUnits`,`det`.`DeliveryConverter` AS `DeliveryConverter`,`det`.`BillingCode` AS `BillingCode`,`det`.`Modifier1` AS `Modifier1`,`det`.`Modifier2` AS `Modifier2`,`det`.`Modifier3` AS `Modifier3`,`det`.`Modifier4` AS `Modifier4`,`det`.`DXPointer` AS `DXPointer`,`det`.`BillingMonth` AS `BillingMonth`,`det`.`BillItemOn` AS `BillItemOn`,`det`.`AuthorizationNumber` AS `AuthorizationNumber`,`det`.`AuthorizationTypeID` AS `AuthorizationTypeID`,`det`.`ReasonForPickup` AS `ReasonForPickup`,`det`.`SendCMN_RX_w_invoice` AS `SendCMN_RX_w_invoice`,`det`.`MedicallyUnnecessary` AS `MedicallyUnnecessary`,`det`.`Sale` AS `Sale`,`det`.`SpecialCode` AS `SpecialCode`,`det`.`ReviewCode` AS `ReviewCode`,`det`.`NextOrderID` AS `NextOrderID`,`det`.`ReoccuringID` AS `ReoccuringID`,`det`.`CMNFormID` AS `CMNFormID`,`det`.`HAOCode` AS `HAOCode`,`det`.`State` AS `State`,`det`.`BillIns1` AS `BillIns1`,`det`.`BillIns2` AS `BillIns2`,`det`.`BillIns3` AS `BillIns3`,`det`.`BillIns4` AS `BillIns4`,`det`.`EndDate` AS `EndDate`,`det`.`MIR` AS `MIR`,`det`.`NextBillingDate` AS `NextBillingDate`,`det`.`WarehouseID` AS `WarehouseID`,`det`.`AcceptAssignment` AS `AcceptAssignment`,`det`.`DrugNoteField` AS `DrugNoteField`,`det`.`DrugControlNumber` AS `DrugControlNumber`,`det`.`NopayIns1` AS `NopayIns1`,`det`.`PointerICD10` AS `PointerICD10`,`det`.`DXPointer10` AS `DXPointer10`,`det`.`MIR.ORDER` AS `MIR.ORDER`,`det`.`HaoDescription` AS `HaoDescription`,`det`.`UserField1` AS `UserField1`,`det`.`UserField2` AS `UserField2`,`det`.`AuthorizationExpirationDate` AS `AuthorizationExpirationDate`,`det`.`IsActive` AS `IsActive`,`det`.`IsCanceled` AS `IsCanceled`,`det`.`IsSold` AS `IsSold`,`det`.`IsRented` AS `IsRented`,`det`.`ActualSaleRentType` AS `ActualSaleRentType`,`det`.`ActualBillItemOn` AS `ActualBillItemOn`,`det`.`ActualOrderedWhen` AS `ActualOrderedWhen`,`det`.`ActualBilledWhen` AS `ActualBilledWhen`,`det`.`ActualDosTo` AS `ActualDosTo`,`det`.`InvoiceDate` AS `InvoiceDate`,`det`.`IsOxygen` AS `IsOxygen`,`det`.`IsZeroAmount` AS `IsZeroAmount`,(case when (((`det`.`State` = 'Pickup') and (`det`.`EndDate` is not null)) or ((`det`.`State` = 'Closed') and (`OrderMustBeClosed`(`ord`.`DeliveryDate`,`det`.`DOSFrom`,`det`.`ActualSaleRentType`,`det`.`BillingMonth`,`det`.`Modifier1`,`det`.`Modifier2`,`det`.`Modifier3`,`det`.`Modifier4`) = 0))) then 1 else 0 end) AS `IsPickedup` from (`view_orderdetails_core` `det` join `tbl_order` `ord` on(((`det`.`CustomerID` = `ord`.`CustomerID`) and (`det`.`OrderID` = `ord`.`ID`))))
;

CREATE VIEW view_orderdetails_core AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_orderdetails_core` AS select `det`.`ID` AS `ID`,`det`.`OrderID` AS `OrderID`,`det`.`CustomerID` AS `CustomerID`,`det`.`SerialNumber` AS `SerialNumber`,`det`.`InventoryItemID` AS `InventoryItemID`,`det`.`PriceCodeID` AS `PriceCodeID`,`det`.`SaleRentType` AS `SaleRentType`,`det`.`SerialID` AS `SerialID`,`det`.`BillablePrice` AS `BillablePrice`,`det`.`AllowablePrice` AS `AllowablePrice`,`det`.`Taxable` AS `Taxable`,`det`.`FlatRate` AS `FlatRate`,`det`.`DOSFrom` AS `DOSFrom`,`det`.`DOSTo` AS `DOSTo`,`det`.`PickupDate` AS `PickupDate`,`det`.`ShowSpanDates` AS `ShowSpanDates`,`det`.`OrderedQuantity` AS `OrderedQuantity`,`det`.`OrderedUnits` AS `OrderedUnits`,`det`.`OrderedWhen` AS `OrderedWhen`,`det`.`OrderedConverter` AS `OrderedConverter`,`det`.`BilledQuantity` AS `BilledQuantity`,`det`.`BilledUnits` AS `BilledUnits`,`det`.`BilledWhen` AS `BilledWhen`,`det`.`BilledConverter` AS `BilledConverter`,`det`.`DeliveryQuantity` AS `DeliveryQuantity`,`det`.`DeliveryUnits` AS `DeliveryUnits`,`det`.`DeliveryConverter` AS `DeliveryConverter`,`det`.`BillingCode` AS `BillingCode`,`det`.`Modifier1` AS `Modifier1`,`det`.`Modifier2` AS `Modifier2`,`det`.`Modifier3` AS `Modifier3`,`det`.`Modifier4` AS `Modifier4`,`det`.`DXPointer` AS `DXPointer`,`det`.`BillingMonth` AS `BillingMonth`,`det`.`BillItemOn` AS `BillItemOn`,`det`.`AuthorizationNumber` AS `AuthorizationNumber`,`det`.`AuthorizationTypeID` AS `AuthorizationTypeID`,`det`.`ReasonForPickup` AS `ReasonForPickup`,`det`.`SendCMN_RX_w_invoice` AS `SendCMN_RX_w_invoice`,`det`.`MedicallyUnnecessary` AS `MedicallyUnnecessary`,`det`.`Sale` AS `Sale`,`det`.`SpecialCode` AS `SpecialCode`,`det`.`ReviewCode` AS `ReviewCode`,`det`.`NextOrderID` AS `NextOrderID`,`det`.`ReoccuringID` AS `ReoccuringID`,`det`.`CMNFormID` AS `CMNFormID`,`det`.`HAOCode` AS `HAOCode`,`det`.`State` AS `State`,`det`.`BillIns1` AS `BillIns1`,`det`.`BillIns2` AS `BillIns2`,`det`.`BillIns3` AS `BillIns3`,`det`.`BillIns4` AS `BillIns4`,`det`.`EndDate` AS `EndDate`,`det`.`MIR` AS `MIR`,`det`.`NextBillingDate` AS `NextBillingDate`,`det`.`WarehouseID` AS `WarehouseID`,`det`.`AcceptAssignment` AS `AcceptAssignment`,`det`.`DrugNoteField` AS `DrugNoteField`,`det`.`DrugControlNumber` AS `DrugControlNumber`,`det`.`NopayIns1` AS `NopayIns1`,`det`.`PointerICD10` AS `PointerICD10`,`det`.`DXPointer10` AS `DXPointer10`,`det`.`MIR.ORDER` AS `MIR.ORDER`,`det`.`HaoDescription` AS `HaoDescription`,`det`.`UserField1` AS `UserField1`,`det`.`UserField2` AS `UserField2`,`det`.`AuthorizationExpirationDate` AS `AuthorizationExpirationDate`,(case when (`det`.`State` in ('Closed','Canceled')) then 0 else 1 end) AS `IsActive`,(case when (`det`.`State` = 'Canceled') then 1 else 0 end) AS `IsCanceled`,(case when (`det`.`SaleRentType` in ('One Time Sale','Re-occurring Sale')) then 1 else 0 end) AS `IsSold`,(case when (`det`.`SaleRentType` in ('Capped Rental','Medicare Oxygen Rental','Parental Capped Rental','Rent to Purchase','Monthly Rental','One Time Rental')) then 1 else 0 end) AS `IsRented`,ifnull(`det`.`SaleRentType`,'') AS `ActualSaleRentType`,(case when (`det`.`BillItemOn` not in ('Day of Delivery','Last day of the Period')) then '' when ((`det`.`SaleRentType` = 'One Time Rental') and (`det`.`BillItemOn` <> 'Last day of the Period')) then '' else ifnull(`det`.`BillItemOn`,'') end) AS `ActualBillItemOn`,(case when ((`det`.`SaleRentType` = 'Capped Rental') and (`det`.`OrderedWhen` in ('One time','Monthly'))) then `det`.`OrderedWhen` when ((`det`.`SaleRentType` = 'Medicare Oxygen Rental') and (`det`.`OrderedWhen` in ('One time','Monthly'))) then `det`.`OrderedWhen` when ((`det`.`SaleRentType` = 'Parental Capped Rental') and (`det`.`OrderedWhen` in ('One time','Monthly'))) then `det`.`OrderedWhen` when ((`det`.`SaleRentType` = 'Rent to Purchase') and (`det`.`OrderedWhen` in ('One time','Monthly'))) then `det`.`OrderedWhen` when ((`det`.`SaleRentType` = 'One Time Sale') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly','Quarterly','Semi-Annually','Annually'))) then '' when ((`det`.`SaleRentType` = 'Re-occurring Sale') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly','Quarterly','Semi-Annually','Annually'))) then '' when ((`det`.`SaleRentType` = 'Monthly Rental') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly','Quarterly','Semi-Annually','Annually'))) then '' when ((`det`.`SaleRentType` = 'One Time Rental') and (`det`.`OrderedWhen` in ('One time','Daily','Weekly','Monthly','Quarterly','Semi-Annually','Annually'))) then `det`.`OrderedWhen` when ((`det`.`BilledWhen` = 'One time') and (`det`.`OrderedWhen` <> 'One time')) then '' when ((`det`.`BilledWhen` = 'Daily') and (`det`.`OrderedWhen` not in ('One time','Daily'))) then '' when ((`det`.`BilledWhen` = 'Weekly') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly'))) then '' when ((`det`.`BilledWhen` = 'Monthly') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly'))) then '' when ((`det`.`BilledWhen` = 'Calendar Monthly') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly'))) then '' when ((`det`.`BilledWhen` = 'Quarterly') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly','Quarterly'))) then '' when ((`det`.`BilledWhen` = 'Semi-Annually') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly','Quarterly','Semi-Annually'))) then '' when ((`det`.`BilledWhen` = 'Annually') and (`det`.`OrderedWhen` not in ('One time','Daily','Weekly','Monthly','Quarterly','Semi-Annually','Annually'))) then '' when ((`det`.`BilledWhen` = 'Custom') and (`det`.`OrderedWhen` not in ('One time','Daily'))) then '' else ifnull(`det`.`OrderedWhen`,'') end) AS `ActualOrderedWhen`,(case when ((`det`.`SaleRentType` = 'Capped Rental') and (`det`.`BilledWhen` <> 'Monthly')) then '' when ((`det`.`SaleRentType` = 'Medicare Oxygen Rental') and (`det`.`BilledWhen` <> 'Monthly')) then '' when ((`det`.`SaleRentType` = 'Parental Capped Rental') and (`det`.`BilledWhen` <> 'Monthly')) then '' when ((`det`.`SaleRentType` = 'Rent to Purchase') and (`det`.`BilledWhen` <> 'Monthly')) then '' when ((`det`.`SaleRentType` = 'One Time Sale') and (`det`.`BilledWhen` not in ('One time','Daily','Weekly','Monthly','Calendar Monthly','Quarterly','Semi-Annually','Annually','Custom'))) then '' when ((`det`.`SaleRentType` = 'Re-occurring Sale') and (`det`.`BilledWhen` not in ('Daily','Weekly','Monthly','Calendar Monthly','Quarterly','Semi-Annually','Annually','Custom'))) then '' when ((`det`.`SaleRentType` = 'Monthly Rental') and (`det`.`BilledWhen` not in ('Daily','Weekly','Monthly','Calendar Monthly','Quarterly','Semi-Annually','Annually','Custom'))) then '' when ((`det`.`SaleRentType` = 'One Time Rental') and (`det`.`BilledWhen` <> 'One time')) then '' else ifnull(`det`.`BilledWhen`,'') end) AS `ActualBilledWhen`,(case when (`det`.`SaleRentType` = 'Capped Rental') then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when (`det`.`SaleRentType` = 'Medicare Oxygen Rental') then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when (`det`.`SaleRentType` = 'Parental Capped Rental') then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when (`det`.`SaleRentType` = 'Rent to Purchase') then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when (`det`.`SaleRentType` = 'One Time Sale') then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,`det`.`BilledWhen`) when (`det`.`SaleRentType` = 'Re-occurring Sale') then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,`det`.`BilledWhen`) when (`det`.`SaleRentType` = 'Monthly Rental') then `GetPeriodEnd2`(`det`.`DOSFrom`,`det`.`DOSTo`,`det`.`EndDate`,`det`.`BilledWhen`) when (`det`.`SaleRentType` = 'One Time Rental') then `det`.`EndDate` else `det`.`DOSFrom` end) AS `ActualDosTo`,(case when ((`det`.`SaleRentType` = 'Capped Rental') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when ((`det`.`SaleRentType` = 'Medicare Oxygen Rental') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when ((`det`.`SaleRentType` = 'Parental Capped Rental') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when ((`det`.`SaleRentType` = 'Rent to Purchase') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,'Monthly') when ((`det`.`SaleRentType` = 'One Time Sale') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,`det`.`BilledWhen`) when ((`det`.`SaleRentType` = 'Re-occurring Sale') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,`det`.`BilledWhen`) when ((`det`.`SaleRentType` = 'Monthly Rental') and (`det`.`BillItemOn` = 'Last day of the Period')) then `GetPeriodEnd`(`det`.`DOSFrom`,`det`.`DOSTo`,`det`.`BilledWhen`) when ((`det`.`SaleRentType` = 'One Time Rental') and (`det`.`BillItemOn` = 'Last day of the Period')) then `det`.`EndDate` else `det`.`DOSFrom` end) AS `InvoiceDate`,(case when (`det`.`BillingCode` in ('A4606','A4608','A4616','E0424','E0430','E0431','E0434','E0435','E0439')) then 1 when (`det`.`BillingCode` in ('E0440','E0441','E0442','E0443','E0444','E0445','E0455','E1390','E1391')) then 1 when (`det`.`BillingCode` = 'E1392') then 1 else 0 end) AS `IsOxygen`,if(((abs(ifnull(`det`.`BillablePrice`,0)) <= 1e-5) and (abs(ifnull(`det`.`AllowablePrice`,0)) <= 1e-5)),1,0) AS `IsZeroAmount` from `tbl_orderdetails` `det`
;

CREATE VIEW view_pricecode AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_pricecode` AS select sql_small_result `tbl_pricecode`.`ID` AS `ID`,`tbl_pricecode`.`Name` AS `Name`,if((`tbl_pricecode`.`Name` like '%RETAIL%'),1,0) AS `IsRetail` from `tbl_pricecode`
;

CREATE VIEW view_reoccuringlist AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_reoccuringlist` AS select `tbl_order`.`ID` AS `OrderID`,`details`.`BilledWhen` AS `BilledWhen`,`details`.`ActualBillItemOn` AS `BillItemOn` from (`view_orderdetails` `details` join `tbl_order` on(((`details`.`CustomerID` = `tbl_order`.`CustomerID`) and (`details`.`OrderID` = `tbl_order`.`ID`)))) where ((`details`.`SaleRentType` = 'Re-occurring Sale') and if((`details`.`BillingMonth` <= 1),((`GetNextDosFrom`(`details`.`DOSFrom`,`details`.`DOSTo`,`details`.`ActualBilledWhen`) + interval -(1) month) <= now()),((`details`.`DOSFrom` + interval -(1) month) <= now())))
;

CREATE VIEW view_sequence AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_sequence` AS select ((16 * `s2`.`num`) + `s1`.`num`) AS `num` from (`view_sequence_core` `s1` join `view_sequence_core` `s2`)
;

CREATE VIEW view_sequence_core AS
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_sequence_core` AS select cast(0 as unsigned) AS `num` union all select cast(1 as unsigned) AS `num` union all select cast(2 as unsigned) AS `num` union all select cast(3 as unsigned) AS `num` union all select cast(4 as unsigned) AS `num` union all select cast(5 as unsigned) AS `num` union all select cast(6 as unsigned) AS `num` union all select cast(7 as unsigned) AS `num` union all select cast(8 as unsigned) AS `num` union all select cast(9 as unsigned) AS `num` union all select cast(10 as unsigned) AS `num` union all select cast(11 as unsigned) AS `num` union all select cast(12 as unsigned) AS `num` union all select cast(13 as unsigned) AS `num` union all select cast(14 as unsigned) AS `num` union all select cast(15 as unsigned) AS `num`
;

CREATE VIEW view_taxrate AS
CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_taxrate` AS select sql_small_result `tbl_taxrate`.`ID` AS `ID`,`tbl_taxrate`.`CityTax` AS `CityTax`,`tbl_taxrate`.`CountyTax` AS `CountyTax`,`tbl_taxrate`.`Name` AS `Name`,`tbl_taxrate`.`OtherTax` AS `OtherTax`,`tbl_taxrate`.`StateTax` AS `StateTax`,`tbl_taxrate`.`LastUpdateUserID` AS `LastUpdateUserID`,`tbl_taxrate`.`LastUpdateDatetime` AS `LastUpdateDatetime`,(((ifnull(`tbl_taxrate`.`CityTax`,0) + ifnull(`tbl_taxrate`.`CountyTax`,0)) + ifnull(`tbl_taxrate`.`OtherTax`,0)) + ifnull(`tbl_taxrate`.`StateTax`,0)) AS `TotalTax` from `tbl_taxrate`
;

COMMIT;
