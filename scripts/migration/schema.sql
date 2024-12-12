CREATE TABLE backup_tbl_authorizationtype_lastupdatedatetime (
backup_id SERIAL NOT NULL,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (backup_id)
);


CREATE TABLE bak_bac_bac_566 (
backup_id integer NOT NULL DEFAULT 0,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE bak_bac_bac_908 (
backup_id integer NOT NULL DEFAULT 0,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE bak_bac_bac_997 (
backup_id integer NOT NULL DEFAULT 0,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE bak_tbl_dat_244 (
backup_id SERIAL NOT NULL,
id integer NOT NULL DEFAULT 0,
complianceid integer NOT NULL DEFAULT 0,
date date NOT NULL DEFAULT '0000-00-00',
done smallint NOT NULL DEFAULT 0,
notes text NOT NULL,
createdbyuserid smallint NULL,
assignedtouserid smallint NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (backup_id)
);


CREATE TABLE bak_tbl_dat_594 (
backup_id SERIAL NOT NULL,
id integer NOT NULL DEFAULT 0,
inventoryitemid integer NOT NULL DEFAULT 0,
warehouseid integer NOT NULL DEFAULT 0,
typeid integer NOT NULL DEFAULT 0,
date date NOT NULL DEFAULT '0000-00-00',
quantity double precision NULL,
cost decimal NULL,
description varchar(30) NULL,
serialid integer NULL,
vendorid integer NULL,
customerid integer NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
purchaseorderid integer NULL,
purchaseorderdetailsid integer NULL,
invoiceid integer NULL,
manufacturerid integer NULL,
orderdetailsid integer NULL,
orderid integer NULL,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (backup_id)
);


CREATE TABLE bak_tbl_del_949 (
backup_id SERIAL NOT NULL,
id integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
orderid integer NULL,
deliverydate date NOT NULL DEFAULT '0000-00-00',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (backup_id)
);


CREATE TABLE bak_tbl_dos_292 (
backup_id SERIAL NOT NULL,
id integer NOT NULL DEFAULT 0,
orderid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
serialnumber varchar(50) NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
pricecodeid integer NOT NULL DEFAULT 0,
salerenttype varchar NOT NULL DEFAULT 'Monthly Rental',
serialid integer NULL,
billableprice decimal NOT NULL DEFAULT '0.00',
allowableprice decimal NOT NULL DEFAULT '0.00',
taxable smallint NOT NULL DEFAULT 0,
flatrate smallint NOT NULL DEFAULT 0,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
pickupdate date NULL,
showspandates smallint NOT NULL DEFAULT 0,
orderedquantity double precision NOT NULL DEFAULT 0,
orderedunits varchar(50) NULL,
orderedwhen varchar NOT NULL DEFAULT 'One time',
orderedconverter double precision NOT NULL DEFAULT 1,
billedquantity double precision NOT NULL DEFAULT 0,
billedunits varchar(50) NULL,
billedwhen varchar NOT NULL DEFAULT 'One time',
billedconverter double precision NOT NULL DEFAULT 1,
deliveryquantity double precision NOT NULL DEFAULT 0,
deliveryunits varchar(50) NULL,
deliveryconverter double precision NOT NULL DEFAULT 1,
billingcode varchar(50) NULL,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
dxpointer varchar(50) NULL,
billingmonth integer NOT NULL DEFAULT 1,
billitemon varchar NOT NULL DEFAULT 'Day of Delivery',
authorizationnumber varchar(50) NULL,
authorizationtypeid integer NULL,
reasonforpickup varchar(50) NULL,
sendcmn_rx_w_invoice smallint NOT NULL DEFAULT 0,
medicallyunnecessary smallint NOT NULL DEFAULT 0,
sale smallint NOT NULL DEFAULT 0,
specialcode varchar(50) NULL,
reviewcode varchar(50) NULL,
nextorderid integer NULL,
reoccuringid integer NULL,
cmnformid integer NULL,
haocode varchar(10) NULL,
state varchar NOT NULL DEFAULT 'New',
billins1 smallint NOT NULL DEFAULT 1,
billins2 smallint NOT NULL DEFAULT 1,
billins3 smallint NOT NULL DEFAULT 1,
billins4 smallint NOT NULL DEFAULT 1,
enddate date NULL,
mir varchar[] NOT NULL DEFAULT '{}',
nextbillingdate date NULL,
warehouseid integer NOT NULL,
acceptassignment smallint NOT NULL DEFAULT 0,
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
nopayins1 smallint NOT NULL DEFAULT 0,
pointericd10 smallint NOT NULL DEFAULT 0,
dxpointer10 varchar(50) NULL,
mir_order varchar[] NOT NULL DEFAULT '{}',
haodescription varchar(100) NULL,
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT '',
authorizationexpirationdate date NULL,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (salerenttype  (value IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 'capped rental', 'parental capped rental', 'rent to purchase', 'one time sale', 're-occurring sale'))),
    CHECK (orderedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'quarterly', 'semi-annually', 'annually'))),
    CHECK (billedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom'))),
    CHECK (billitemon  (value IN ('day of delivery', 'last day of the month', 'last day of the period', 'day of pick-up'))),
    CHECK (state  (value IN ('new', 'approved', 'pickup', 'closed', 'canceled')))
,    PRIMARY KEY (backup_id)
);


CREATE TABLE bak_tbl_dos_663 (
backup_id SERIAL NOT NULL,
id integer NOT NULL DEFAULT 0,
invoiceid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
inventoryitemid integer NOT NULL DEFAULT 0,
pricecodeid integer NOT NULL DEFAULT 0,
orderid integer NULL,
orderdetailsid integer NULL,
balance decimal NOT NULL DEFAULT '0.00',
billableamount decimal NOT NULL DEFAULT '0.00',
allowableamount decimal NOT NULL DEFAULT '0.00',
taxes decimal NOT NULL DEFAULT '0.00',
quantity double precision NOT NULL DEFAULT 0,
invoicedate date NULL,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
billingcode varchar(50) NULL,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
dxpointer varchar(50) NULL,
billingmonth integer NOT NULL DEFAULT 0,
sendcmn_rx_w_invoice smallint NOT NULL DEFAULT 0,
specialcode varchar(50) NULL,
reviewcode varchar(50) NULL,
medicallyunnecessary smallint NOT NULL DEFAULT 0,
authorizationnumber varchar(50) NULL,
authorizationtypeid integer NULL,
invoicenotes varchar(255) NULL,
invoicerecord varchar(255) NULL,
cmnformid integer NULL,
haocode varchar(10) NULL,
billins1 smallint NOT NULL DEFAULT 1,
billins2 smallint NOT NULL DEFAULT 1,
billins3 smallint NOT NULL DEFAULT 1,
billins4 smallint NOT NULL DEFAULT 1,
hardship smallint NOT NULL DEFAULT 0,
showspandates smallint NOT NULL DEFAULT 0,
paymentamount decimal NOT NULL DEFAULT '0.00',
writeoffamount decimal NOT NULL DEFAULT '0.00',
currentpayer varchar NOT NULL DEFAULT 'Ins1',
pendings smallint NOT NULL DEFAULT 0,
submits smallint NOT NULL DEFAULT 0,
payments smallint NOT NULL DEFAULT 0,
submitteddate date NULL,
submitted smallint NOT NULL DEFAULT 0,
currentinsurancecompanyid integer NULL,
currentcustomerinsuranceid integer NULL,
acceptassignment smallint NOT NULL DEFAULT 0,
deductibleamount decimal NOT NULL DEFAULT '0.00',
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
nopayins1 smallint NOT NULL DEFAULT 0,
pointericd10 smallint NOT NULL DEFAULT 0,
dxpointer10 varchar(50) NULL,
haodescription varchar(100) NULL,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (currentpayer  (value IN ('ins1', 'ins2', 'ins3', 'ins4', 'patient', 'none')))
,    PRIMARY KEY (backup_id)
);


CREATE TABLE bak_tbl_set_665 (
backup_id SERIAL NOT NULL,
accountnumber varchar(40) NOT NULL DEFAULT '',
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
billingtypeid integer NULL,
city varchar(25) NOT NULL DEFAULT '',
courtesy varchar NOT NULL DEFAULT 'Dr.',
customerbalance double precision NULL,
customerclasscode char(2) NULL,
customertypeid integer NULL,
deceaseddate date NULL,
dateofbirth date NULL,
firstname varchar(25) NOT NULL DEFAULT '',
id integer NOT NULL DEFAULT 0,
lastname varchar(30) NOT NULL DEFAULT '',
locationid integer NULL,
middlename char(1) NOT NULL DEFAULT '',
phone varchar(50) NOT NULL DEFAULT '',
phone2 varchar(50) NOT NULL DEFAULT '',
state char(2) NOT NULL DEFAULT '',
suffix varchar(4) NOT NULL DEFAULT '',
totalbalance double precision NULL,
zip varchar(10) NOT NULL DEFAULT '',
billactive smallint NOT NULL DEFAULT 0,
billaddress1 varchar(40) NOT NULL DEFAULT '',
billaddress2 varchar(40) NOT NULL DEFAULT '',
billcity varchar(25) NOT NULL DEFAULT '',
billname varchar(50) NOT NULL DEFAULT '',
billstate char(2) NOT NULL DEFAULT '',
billzip varchar(10) NOT NULL DEFAULT '',
commercialaccount smallint NULL,
deliverydirections text NOT NULL,
employmentstatus varchar NOT NULL DEFAULT 'Unknown',
gender varchar NOT NULL DEFAULT 'Male',
height double precision NULL,
license varchar(50) NOT NULL DEFAULT '',
maritalstatus varchar NOT NULL DEFAULT 'Unknown',
militarybranch varchar NOT NULL DEFAULT 'N/A',
militarystatus varchar NOT NULL DEFAULT 'N/A',
shipactive smallint NOT NULL DEFAULT 0,
shipaddress1 varchar(40) NOT NULL DEFAULT '',
shipaddress2 varchar(40) NOT NULL DEFAULT '',
shipcity varchar(25) NOT NULL DEFAULT '',
shipname varchar(50) NOT NULL DEFAULT '',
shipstate char(2) NOT NULL DEFAULT '',
shipzip varchar(10) NOT NULL DEFAULT '',
ssnumber varchar(50) NOT NULL DEFAULT '',
studentstatus varchar NOT NULL DEFAULT 'N/A',
weight double precision NULL,
basis varchar NOT NULL DEFAULT 'Bill',
block12hcfa smallint NOT NULL DEFAULT 0,
block13hcfa smallint NOT NULL DEFAULT 0,
commercialacctcreditlimit double precision NULL,
commercialacctterms varchar(50) NOT NULL DEFAULT '',
copaydollar double precision NULL,
deductible double precision NULL,
frequency varchar NOT NULL DEFAULT 'Per Visit',
hardship smallint NOT NULL DEFAULT 0,
monthsvalid integer NOT NULL DEFAULT 0,
outofpocket double precision NULL,
signatureonfile date NULL,
signaturetype char(1) NULL,
taxrateid integer NULL,
doctor1_id integer NULL,
doctor2_id integer NULL,
emergencycontact text NOT NULL,
facilityid integer NULL,
legalrepid integer NULL,
referralid integer NULL,
salesrepid integer NULL,
accidenttype varchar NOT NULL,
stateofaccident char(2) NOT NULL DEFAULT '',
dateofinjury date NULL,
emergency smallint NOT NULL DEFAULT 0,
employmentrelated smallint NOT NULL DEFAULT 0,
firstconsultdate date NULL,
icd9_1 varchar(6) NULL,
icd9_2 varchar(6) NULL,
icd9_3 varchar(6) NULL,
icd9_4 varchar(6) NULL,
postypeid integer NULL,
returntoworkdate date NULL,
copaypercent double precision NULL,
setupdate date NOT NULL DEFAULT '0000-00-00',
hippanote smallint NOT NULL DEFAULT 0,
supplierstandards smallint NOT NULL DEFAULT 0,
inactivedate date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
invoiceformid integer NULL DEFAULT 4,
mir varchar[] NOT NULL DEFAULT '{}',
email varchar(150) NULL,
collections bit NOT NULL DEFAULT 'b'0'',
icd10_01 varchar(8) NULL,
icd10_02 varchar(8) NULL,
icd10_03 varchar(8) NULL,
icd10_04 varchar(8) NULL,
icd10_05 varchar(8) NULL,
icd10_06 varchar(8) NULL,
icd10_07 varchar(8) NULL,
icd10_08 varchar(8) NULL,
icd10_09 varchar(8) NULL,
icd10_10 varchar(8) NULL,
icd10_11 varchar(8) NULL,
icd10_12 varchar(8) NULL,
backup_timestamp timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (courtesy  (value IN ('dr.', 'miss', 'mr.', 'mrs.', 'rev.'))),
    CHECK (employmentstatus  (value IN ('unknown', 'full time', 'part time', 'retired', 'student', 'unemployed'))),
    CHECK (gender  (value IN ('male', 'female'))),
    CHECK (maritalstatus  (value IN ('unknown', 'single', 'married', 'legaly separated', 'divorced', 'widowed'))),
    CHECK (militarybranch  (value IN ('n/a', 'army', 'air force', 'navy', 'marines', 'coast guard', 'national guard'))),
    CHECK (militarystatus  (value IN ('n/a', 'active', 'reserve', 'retired'))),
    CHECK (studentstatus  (value IN ('n/a', 'full time', 'part time'))),
    CHECK (basis  (value IN ('bill', 'allowed'))),
    CHECK (frequency  (value IN ('per visit', 'monthly', 'yearly'))),
    CHECK (accidenttype  (value IN ('auto', 'no', 'other')))
,    PRIMARY KEY (backup_id)
);


CREATE TABLE tbl_authorizationtype (
id SERIAL NOT NULL,
name varchar(50) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_batchpayment (
id SERIAL NOT NULL,
insurancecompanyid integer NOT NULL,
checknumber varchar(14) NOT NULL,
checkdate date NOT NULL,
checkamount decimal NOT NULL,
amountused decimal NOT NULL,
lastupdateuserid smallint NOT NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_billingtype (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_changes (
tablename varchar(64) NOT NULL,
sessionid integer NOT NULL,
lastupdateuserid smallint NOT NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (tablename)
);


CREATE TABLE tbl_cmnform (
id SERIAL NOT NULL,
cmntype varchar NOT NULL DEFAULT 'DME 484.03',
initialdate date NULL,
reviseddate date NULL,
recertificationdate date NULL,
customerid integer NULL,
customer_icd9_1 varchar(8) NULL,
customer_icd9_2 varchar(8) NULL,
customer_icd9_3 varchar(8) NULL,
customer_icd9_4 varchar(8) NULL,
doctorid integer NULL,
postypeid integer NULL,
facilityid integer NULL,
answeringname varchar(50) NOT NULL DEFAULT '',
answeringtitle varchar(50) NOT NULL DEFAULT '',
answeringemployer varchar(50) NOT NULL DEFAULT '',
estimatedlengthofneed integer NOT NULL DEFAULT 0,
signature_name varchar(50) NOT NULL DEFAULT '',
signature_date date NULL,
onfile smallint NOT NULL DEFAULT 0,
orderid integer NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
mir varchar[] NOT NULL DEFAULT '{}',
customer_usingicd10 smallint NOT NULL DEFAULT 0
    CHECK (cmntype  (value IN ('dmerc 01.02a', 'dmerc 01.02b', 'dmerc 02.03a', 'dmerc 02.03b', 'dmerc 03.02', 'dmerc 04.03b', 'dmerc 04.03c', 'dmerc 06.02b', 'dmerc 07.02a', 'dmerc 07.02b', 'dmerc 08.02', 'dmerc 09.02', 'dmerc 10.02a', 'dmerc 10.02b', 'dmerc 484.2', 'dmerc drorder', 'dmerc uro', 'dme 04.04b', 'dme 04.04c', 'dme 06.03b', 'dme 07.03a', 'dme 09.03', 'dme 10.03', 'dme 484.03')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_cmnform_0102a (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer3 varchar NOT NULL DEFAULT 'D',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 varchar NOT NULL DEFAULT 'D',
answer6 varchar NOT NULL DEFAULT 'D',
answer7 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer5  (value IN ('y', 'n', 'd'))),
    CHECK (answer6  (value IN ('y', 'n', 'd'))),
    CHECK (answer7  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0102b (
cmnformid integer NOT NULL DEFAULT 0,
answer12 varchar NOT NULL DEFAULT 'D',
answer13 varchar NOT NULL DEFAULT 'D',
answer14 varchar NOT NULL DEFAULT 'D',
answer15 varchar NOT NULL DEFAULT 'D',
answer16 varchar NOT NULL DEFAULT 'D',
answer19 varchar NOT NULL DEFAULT 'D',
answer20 varchar NOT NULL DEFAULT 'D',
answer21_ulcer1_stage varchar(30) NULL,
answer21_ulcer1_maxlength double precision NULL,
answer21_ulcer1_maxwidth double precision NULL,
answer21_ulcer2_stage varchar(30) NULL,
answer21_ulcer2_maxlength double precision NULL,
answer21_ulcer2_maxwidth double precision NULL,
answer21_ulcer3_stage varchar(30) NULL,
answer21_ulcer3_maxlength double precision NULL,
answer21_ulcer3_maxwidth double precision NULL,
answer22 varchar NOT NULL DEFAULT '1'
    CHECK (answer12  (value IN ('y', 'n', 'd'))),
    CHECK (answer13  (value IN ('y', 'n', 'd'))),
    CHECK (answer14  (value IN ('y', 'n', 'd'))),
    CHECK (answer15  (value IN ('y', 'n', 'd'))),
    CHECK (answer16  (value IN ('y', 'n', 'd'))),
    CHECK (answer19  (value IN ('y', 'n', 'd'))),
    CHECK (answer20  (value IN ('y', 'n', 'd'))),
    CHECK (answer22  (value IN ('1', '2', '3')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0203a (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer2 varchar NOT NULL DEFAULT 'D',
answer3 varchar NOT NULL DEFAULT 'D',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 integer NULL,
answer6 varchar NOT NULL DEFAULT 'D',
answer7 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer2  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer6  (value IN ('y', 'n', 'd'))),
    CHECK (answer7  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0203b (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer2 varchar NOT NULL DEFAULT 'D',
answer3 varchar NOT NULL DEFAULT 'D',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 integer NULL,
answer8 varchar NOT NULL DEFAULT 'D',
answer9 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer2  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer8  (value IN ('y', 'n', 'd'))),
    CHECK (answer9  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0302 (
cmnformid integer NOT NULL DEFAULT 0,
answer12 integer NULL,
answer14 varchar NOT NULL DEFAULT 'D'
    CHECK (answer14  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0403b (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer2 varchar NOT NULL DEFAULT 'D',
answer3 varchar NOT NULL DEFAULT 'D',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer2  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer5  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0403c (
cmnformid integer NOT NULL DEFAULT 0,
answer6a varchar NOT NULL DEFAULT 'D',
answer6b integer NOT NULL DEFAULT 0,
answer7a varchar NOT NULL DEFAULT 'D',
answer7b integer NOT NULL DEFAULT 0,
answer8 varchar NOT NULL DEFAULT 'D',
answer9a varchar NOT NULL DEFAULT 'D',
answer9b integer NOT NULL DEFAULT 0,
answer10a varchar NOT NULL DEFAULT 'D',
answer10b integer NOT NULL DEFAULT 0,
answer10c integer NOT NULL DEFAULT 0,
answer11a varchar NOT NULL DEFAULT 'D',
answer11b integer NOT NULL DEFAULT 0
    CHECK (answer6a  (value IN ('y', 'n', 'd'))),
    CHECK (answer7a  (value IN ('y', 'n', 'd'))),
    CHECK (answer8  (value IN ('y', 'n', 'd'))),
    CHECK (answer9a  (value IN ('y', 'n', 'd'))),
    CHECK (answer10a  (value IN ('y', 'n', 'd'))),
    CHECK (answer11a  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0404b (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'N',
answer2 varchar NOT NULL DEFAULT 'N',
answer3 varchar NOT NULL DEFAULT 'N',
answer4 varchar NOT NULL DEFAULT 'N',
answer5 varchar NOT NULL DEFAULT 'N'
    CHECK (answer1  (value IN ('y', 'n'))),
    CHECK (answer2  (value IN ('y', 'n'))),
    CHECK (answer3  (value IN ('y', 'n'))),
    CHECK (answer4  (value IN ('y', 'n'))),
    CHECK (answer5  (value IN ('y', 'n')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0404c (
cmnformid integer NOT NULL DEFAULT 0,
answer6 varchar NOT NULL DEFAULT 'D',
answer7a varchar NOT NULL DEFAULT 'D',
answer7b varchar(10) NULL,
answer8 varchar NOT NULL DEFAULT 'D',
answer9a varchar NOT NULL DEFAULT 'D',
answer9b varchar(10) NULL,
answer10a varchar NOT NULL DEFAULT 'D',
answer10b varchar(10) NULL,
answer10c varchar(10) NULL,
answer11 varchar NOT NULL DEFAULT 'D',
answer12 varchar NOT NULL DEFAULT 'D'
    CHECK (answer6  (value IN ('y', 'n', 'd'))),
    CHECK (answer7a  (value IN ('y', 'n', 'd'))),
    CHECK (answer8  (value IN ('y', 'n', 'd'))),
    CHECK (answer9a  (value IN ('y', 'n', 'd'))),
    CHECK (answer10a  (value IN ('y', 'n', 'd'))),
    CHECK (answer11  (value IN ('y', 'n', 'd'))),
    CHECK (answer12  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0602b (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer2 date NULL,
answer3 varchar NOT NULL DEFAULT 'D',
answer4 integer NULL,
answer5 varchar NOT NULL DEFAULT '1',
answer6 varchar NOT NULL DEFAULT 'D',
answer7 varchar NOT NULL DEFAULT 'D',
answer8_begun date NULL,
answer8_ended date NULL,
answer9 date NULL,
answer10 varchar NOT NULL DEFAULT '1',
answer11 varchar NOT NULL DEFAULT 'D',
answer12 varchar NOT NULL DEFAULT '2'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer5  (value IN ('1', '2', '3', '4', '5'))),
    CHECK (answer6  (value IN ('y', 'n', 'd'))),
    CHECK (answer7  (value IN ('y', 'n', 'd'))),
    CHECK (answer10  (value IN ('1', '2', '3'))),
    CHECK (answer11  (value IN ('y', 'n', 'd'))),
    CHECK (answer12  (value IN ('2', '4')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0603b (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'N',
answer2 integer NULL,
answer3 varchar NOT NULL DEFAULT '5',
answer4 varchar NOT NULL DEFAULT 'N',
answer5 varchar NOT NULL DEFAULT 'N',
answer6 date NULL
    CHECK (answer1  (value IN ('y', 'n'))),
    CHECK (answer3  (value IN ('1', '2', '3', '4', '5'))),
    CHECK (answer4  (value IN ('y', 'n'))),
    CHECK (answer5  (value IN ('y', 'n')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0702a (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer2 varchar NOT NULL DEFAULT 'D',
answer3 varchar NOT NULL DEFAULT 'D',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer2  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer5  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0702b (
cmnformid integer NOT NULL DEFAULT 0,
answer6 varchar NOT NULL DEFAULT 'D',
answer7 varchar NOT NULL DEFAULT 'D',
answer8 varchar NOT NULL DEFAULT 'D',
answer12 varchar NOT NULL DEFAULT 'D',
answer13 varchar NOT NULL DEFAULT 'D',
answer14 varchar NOT NULL DEFAULT 'D'
    CHECK (answer6  (value IN ('y', 'n', 'd'))),
    CHECK (answer7  (value IN ('y', 'n', 'd'))),
    CHECK (answer8  (value IN ('y', 'n', 'd'))),
    CHECK (answer12  (value IN ('y', 'n', 'd'))),
    CHECK (answer13  (value IN ('y', 'n', 'd'))),
    CHECK (answer14  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0703a (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'D',
answer2 varchar NOT NULL DEFAULT 'D',
answer3 varchar NOT NULL DEFAULT 'D',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('y', 'n', 'd'))),
    CHECK (answer2  (value IN ('y', 'n', 'd'))),
    CHECK (answer3  (value IN ('y', 'n', 'd'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer5  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0802 (
cmnformid integer NOT NULL DEFAULT 0,
answer1_hcpcs varchar(5) NOT NULL DEFAULT '',
answer1_mg integer NULL,
answer1_times integer NULL,
answer2_hcpcs varchar(5) NOT NULL DEFAULT '',
answer2_mg integer NULL,
answer2_times integer NULL,
answer3_hcpcs varchar(5) NOT NULL DEFAULT '',
answer3_mg integer NULL,
answer3_times integer NULL,
answer4 varchar NOT NULL DEFAULT 'N',
answer5_1 varchar NOT NULL DEFAULT '1',
answer5_2 varchar NOT NULL DEFAULT '1',
answer5_3 varchar NOT NULL DEFAULT '1',
answer8 varchar(60) NOT NULL DEFAULT '',
answer9 varchar(20) NOT NULL DEFAULT '',
answer10 varchar(2) NOT NULL DEFAULT '',
answer11 date NULL,
answer12 varchar NOT NULL DEFAULT 'N'
    CHECK (answer4  (value IN ('y', 'n'))),
    CHECK (answer5_1  (value IN ('1', '2', '3', '4', '5'))),
    CHECK (answer5_2  (value IN ('1', '2', '3', '4', '5'))),
    CHECK (answer5_3  (value IN ('1', '2', '3', '4', '5'))),
    CHECK (answer12  (value IN ('y', 'n')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0902 (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT '1',
answer2 varchar(50) NOT NULL DEFAULT '',
answer3 varchar(50) NOT NULL DEFAULT '',
answer4 varchar NOT NULL DEFAULT '1',
answer5 varchar NOT NULL DEFAULT '1',
answer6 integer NOT NULL DEFAULT 1,
answer7 varchar NOT NULL DEFAULT 'D'
    CHECK (answer1  (value IN ('1', '3', '4'))),
    CHECK (answer4  (value IN ('1', '3', '4'))),
    CHECK (answer5  (value IN ('1', '2', '3'))),
    CHECK (answer7  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_0903 (
cmnformid integer NOT NULL DEFAULT 0,
answer1a varchar(10) NULL,
answer1b varchar(10) NULL,
answer1c varchar(10) NULL,
answer2a varchar(50) NULL,
answer2b varchar(50) NULL,
answer2c varchar(50) NULL,
answer3 varchar NOT NULL DEFAULT '1',
answer4 varchar NOT NULL DEFAULT '1'
    CHECK (answer3  (value IN ('1', '2', '3', '4'))),
    CHECK (answer4  (value IN ('1', '2')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_1002a (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'Y',
answer3 integer NULL,
concentration_aminoacid double precision NULL,
concentration_dextrose double precision NULL,
concentration_lipids double precision NULL,
dose_aminoacid double precision NULL,
dose_dextrose double precision NULL,
dose_lipids double precision NULL,
daysperweek_lipids double precision NULL,
gmsperday_aminoacid double precision NULL,
answer5 varchar NOT NULL DEFAULT '1'
    CHECK (answer1  (value IN ('y', 'n'))),
    CHECK (answer5  (value IN ('1', '3', '7')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_1002b (
cmnformid integer NOT NULL DEFAULT 0,
answer7 varchar NOT NULL DEFAULT 'Y',
answer8 varchar NOT NULL DEFAULT 'Y',
answer10a varchar(50) NOT NULL DEFAULT '',
answer10b varchar(50) NOT NULL DEFAULT '',
answer11a varchar(50) NOT NULL DEFAULT '',
answer11b varchar(50) NOT NULL DEFAULT '',
answer12 integer NULL,
answer13 varchar NOT NULL DEFAULT '1',
answer14 varchar NOT NULL DEFAULT 'D',
answer15 varchar(50) NOT NULL DEFAULT ''
    CHECK (answer7  (value IN ('y', 'n'))),
    CHECK (answer8  (value IN ('y', 'n'))),
    CHECK (answer13  (value IN ('1', '2', '3', '4'))),
    CHECK (answer14  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_1003 (
cmnformid integer NOT NULL DEFAULT 0,
answer1 varchar NOT NULL DEFAULT 'Y',
answer2 varchar NOT NULL DEFAULT 'Y',
answer3a varchar(10) NULL,
answer3b varchar(10) NULL,
answer4a integer NULL,
answer4b integer NULL,
answer5 varchar NOT NULL DEFAULT '1',
answer6 integer NULL,
answer7 varchar NOT NULL DEFAULT 'Y',
answer8a integer NULL,
answer8b integer NULL,
answer8c integer NULL,
answer8d integer NULL,
answer8e integer NULL,
answer8f integer NULL,
answer8g integer NULL,
answer8h integer NULL,
answer9 varchar NOT NULL DEFAULT '1'
    CHECK (answer1  (value IN ('y', 'n'))),
    CHECK (answer2  (value IN ('y', 'n'))),
    CHECK (answer5  (value IN ('1', '2', '3', '4'))),
    CHECK (answer7  (value IN ('y', 'n'))),
    CHECK (answer9  (value IN ('1', '2', '3')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_48403 (
cmnformid integer NOT NULL DEFAULT 0,
answer1a integer NULL,
answer1b integer NULL,
answer1c date NULL,
answer2 varchar NOT NULL DEFAULT '1',
answer3 varchar NOT NULL DEFAULT '1',
answer4 varchar NOT NULL DEFAULT 'D',
answer5 varchar(10) NULL,
answer6a integer NULL,
answer6b integer NULL,
answer6c date NULL,
answer7 varchar NOT NULL DEFAULT 'Y',
answer8 varchar NOT NULL DEFAULT 'Y',
answer9 varchar NOT NULL DEFAULT 'Y'
    CHECK (answer2  (value IN ('1', '2', '3'))),
    CHECK (answer3  (value IN ('1', '2', '3'))),
    CHECK (answer4  (value IN ('y', 'n', 'd'))),
    CHECK (answer7  (value IN ('y', 'n'))),
    CHECK (answer8  (value IN ('y', 'n'))),
    CHECK (answer9  (value IN ('y', 'n')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_4842 (
cmnformid integer NOT NULL DEFAULT 0,
answer1a integer NULL,
answer1b integer NULL,
answer1c date NULL,
answer2 varchar NOT NULL DEFAULT 'Y',
answer3 varchar NOT NULL DEFAULT '1',
physicianaddress varchar(50) NOT NULL DEFAULT '',
physiciancity varchar(50) NOT NULL DEFAULT '',
physicianstate varchar(50) NOT NULL DEFAULT '',
physicianzip varchar(50) NOT NULL DEFAULT '',
physicianname varchar(50) NOT NULL DEFAULT '',
answer5 varchar NOT NULL DEFAULT 'D',
answer6 varchar(10) NULL,
answer7a integer NULL,
answer7b integer NULL,
answer7c date NULL,
answer8 varchar NOT NULL DEFAULT 'D',
answer9 varchar NOT NULL DEFAULT 'D',
answer10 varchar NOT NULL DEFAULT 'D'
    CHECK (answer2  (value IN ('y', 'n'))),
    CHECK (answer3  (value IN ('1', '2', '3'))),
    CHECK (answer5  (value IN ('y', 'n', 'd'))),
    CHECK (answer8  (value IN ('y', 'n', 'd'))),
    CHECK (answer9  (value IN ('y', 'n', 'd'))),
    CHECK (answer10  (value IN ('y', 'n', 'd')))
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_details (
cmnformid integer NOT NULL DEFAULT 0,
billingcode varchar(50) NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
orderedquantity double precision NOT NULL DEFAULT 0,
orderedunits varchar(50) NULL,
billableprice double precision NOT NULL DEFAULT 0,
allowableprice double precision NOT NULL DEFAULT 0,
period varchar NOT NULL DEFAULT 'One time',
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
predefinedtextid integer NULL,
id SERIAL NOT NULL
    CHECK (period  (value IN ('one time', 'daily', 'weekly', 'monthly', 'quarterly', 'semi-annually', 'annually', 'custom')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_cmnform_drorder (
cmnformid integer NOT NULL DEFAULT 0,
prognosis varchar(50) NOT NULL DEFAULT '',
medicaljustification text NOT NULL
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_cmnform_uro (
cmnformid integer NOT NULL DEFAULT 0,
prognosis varchar(50) NOT NULL DEFAULT ''
,    PRIMARY KEY (cmnformid)
);


CREATE TABLE tbl_company (
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
billcustomercopayupfront smallint NOT NULL DEFAULT 0,
city varchar(25) NOT NULL DEFAULT '',
fax varchar(50) NOT NULL DEFAULT '',
federaltaxid varchar(9) NOT NULL DEFAULT '',
taxonomycode varchar(20) NOT NULL DEFAULT '332B00000X',
ein varchar(20) NOT NULL DEFAULT '',
ssn varchar(20) NOT NULL DEFAULT '',
taxidtype varchar NOT NULL,
id SERIAL NOT NULL,
name varchar(50) NOT NULL DEFAULT '',
participatingprovider smallint NOT NULL DEFAULT 0,
phone varchar(50) NOT NULL DEFAULT '',
phone2 varchar(50) NOT NULL DEFAULT '',
poauthorizationcodereqiered smallint NOT NULL DEFAULT 0,
print_pricesonorders smallint NOT NULL DEFAULT 0,
picture bytea NULL,
postypeid integer NULL DEFAULT 12,
state char(2) NOT NULL DEFAULT '',
systemgenerate_blanketassignments smallint NOT NULL DEFAULT 0,
systemgenerate_cappedrentalletters smallint NOT NULL DEFAULT 0,
systemgenerate_customeraccountnumbers smallint NOT NULL DEFAULT 0,
systemgenerate_deliverypickuptickets smallint NOT NULL DEFAULT 0,
systemgenerate_droctorsorder smallint NOT NULL DEFAULT 0,
systemgenerate_hippaforms smallint NOT NULL DEFAULT 0,
systemgenerate_patientbillofrights smallint NOT NULL DEFAULT 0,
systemgenerate_purchaseordernumber smallint NOT NULL DEFAULT 0,
writeoffdifference smallint NOT NULL DEFAULT 0,
zip varchar(10) NOT NULL DEFAULT '',
includelocationinfo smallint NOT NULL DEFAULT 0,
contact varchar(50) NOT NULL DEFAULT '',
print_companyinfooninvoice smallint NOT NULL DEFAULT 0,
print_companyinfoondelivery smallint NOT NULL DEFAULT 0,
print_companyinfoonpickup smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
show_inactivecustomers smallint NOT NULL DEFAULT 0,
warehouseid integer NULL,
npi varchar(10) NULL,
taxrateid integer NULL,
imagingserver varchar(250) NULL,
zirmednumber varchar(20) NOT NULL DEFAULT '',
automaticallyreorderinventory smallint NOT NULL DEFAULT 1,
availitynumber varchar(50) NOT NULL DEFAULT '',
show_quantityonhand smallint NOT NULL DEFAULT 0,
use_icd10fornewcmnrx smallint NOT NULL DEFAULT 0,
ordersurveyid integer NULL,
abilityintegrationsettings text NOT NULL
    CHECK (taxidtype  (value IN ('ssn', 'ein')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_compliance (
id SERIAL NOT NULL,
customerid integer NOT NULL DEFAULT 0,
orderid integer NULL,
deliverydate date NOT NULL DEFAULT '0000-00-00',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_compliance_items (
complianceid integer NOT NULL DEFAULT 0,
inventoryitemid integer NOT NULL DEFAULT 0
);


CREATE TABLE tbl_compliance_notes (
id SERIAL NOT NULL,
complianceid integer NOT NULL DEFAULT 0,
date date NOT NULL DEFAULT '0000-00-00',
done smallint NOT NULL DEFAULT 0,
notes text NOT NULL,
createdbyuserid smallint NULL,
assignedtouserid smallint NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_customer (
accountnumber varchar(40) NOT NULL DEFAULT '',
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
billingtypeid integer NULL,
city varchar(25) NOT NULL DEFAULT '',
courtesy varchar NOT NULL DEFAULT 'Dr.',
customerbalance double precision NULL,
customerclasscode char(2) NULL,
customertypeid integer NULL,
deceaseddate date NULL,
dateofbirth date NULL,
firstname varchar(25) NOT NULL DEFAULT '',
id SERIAL NOT NULL,
lastname varchar(30) NOT NULL DEFAULT '',
locationid integer NULL,
middlename char(1) NOT NULL DEFAULT '',
phone varchar(50) NOT NULL DEFAULT '',
phone2 varchar(50) NOT NULL DEFAULT '',
state char(2) NOT NULL DEFAULT '',
suffix varchar(4) NOT NULL DEFAULT '',
totalbalance double precision NULL,
zip varchar(10) NOT NULL DEFAULT '',
billactive smallint NOT NULL DEFAULT 0,
billaddress1 varchar(40) NOT NULL DEFAULT '',
billaddress2 varchar(40) NOT NULL DEFAULT '',
billcity varchar(25) NOT NULL DEFAULT '',
billname varchar(50) NOT NULL DEFAULT '',
billstate char(2) NOT NULL DEFAULT '',
billzip varchar(10) NOT NULL DEFAULT '',
commercialaccount smallint NULL,
deliverydirections text NOT NULL,
employmentstatus varchar NOT NULL DEFAULT 'Unknown',
gender varchar NOT NULL DEFAULT 'Male',
height double precision NULL,
license varchar(50) NOT NULL DEFAULT '',
maritalstatus varchar NOT NULL DEFAULT 'Unknown',
militarybranch varchar NOT NULL DEFAULT 'N/A',
militarystatus varchar NOT NULL DEFAULT 'N/A',
shipactive smallint NOT NULL DEFAULT 0,
shipaddress1 varchar(40) NOT NULL DEFAULT '',
shipaddress2 varchar(40) NOT NULL DEFAULT '',
shipcity varchar(25) NOT NULL DEFAULT '',
shipname varchar(50) NOT NULL DEFAULT '',
shipstate char(2) NOT NULL DEFAULT '',
shipzip varchar(10) NOT NULL DEFAULT '',
ssnumber varchar(50) NOT NULL DEFAULT '',
studentstatus varchar NOT NULL DEFAULT 'N/A',
weight double precision NULL,
basis varchar NOT NULL DEFAULT 'Bill',
block12hcfa smallint NOT NULL DEFAULT 0,
block13hcfa smallint NOT NULL DEFAULT 0,
commercialacctcreditlimit double precision NULL,
commercialacctterms varchar(50) NOT NULL DEFAULT '',
copaydollar double precision NULL,
deductible double precision NULL,
frequency varchar NOT NULL DEFAULT 'Per Visit',
hardship smallint NOT NULL DEFAULT 0,
monthsvalid integer NOT NULL DEFAULT 0,
outofpocket double precision NULL,
signatureonfile date NULL,
signaturetype char(1) NULL,
taxrateid integer NULL,
doctor1_id integer NULL,
doctor2_id integer NULL,
emergencycontact text NOT NULL,
facilityid integer NULL,
legalrepid integer NULL,
referralid integer NULL,
salesrepid integer NULL,
accidenttype varchar NOT NULL,
stateofaccident char(2) NOT NULL DEFAULT '',
dateofinjury date NULL,
emergency smallint NOT NULL DEFAULT 0,
employmentrelated smallint NOT NULL DEFAULT 0,
firstconsultdate date NULL,
icd9_1 varchar(6) NULL,
icd9_2 varchar(6) NULL,
icd9_3 varchar(6) NULL,
icd9_4 varchar(6) NULL,
postypeid integer NULL,
returntoworkdate date NULL,
copaypercent double precision NULL,
setupdate date NOT NULL DEFAULT '0000-00-00',
hippanote smallint NOT NULL DEFAULT 0,
supplierstandards smallint NOT NULL DEFAULT 0,
inactivedate date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
invoiceformid integer NULL DEFAULT 4,
mir varchar[] NOT NULL DEFAULT '{}',
email varchar(150) NULL,
collections bit NOT NULL DEFAULT 'b'0'',
icd10_01 varchar(8) NULL,
icd10_02 varchar(8) NULL,
icd10_03 varchar(8) NULL,
icd10_04 varchar(8) NULL,
icd10_05 varchar(8) NULL,
icd10_06 varchar(8) NULL,
icd10_07 varchar(8) NULL,
icd10_08 varchar(8) NULL,
icd10_09 varchar(8) NULL,
icd10_10 varchar(8) NULL,
icd10_11 varchar(8) NULL,
icd10_12 varchar(8) NULL
    CHECK (courtesy  (value IN ('dr.', 'miss', 'mr.', 'mrs.', 'rev.'))),
    CHECK (employmentstatus  (value IN ('unknown', 'full time', 'part time', 'retired', 'student', 'unemployed'))),
    CHECK (gender  (value IN ('male', 'female'))),
    CHECK (maritalstatus  (value IN ('unknown', 'single', 'married', 'legaly separated', 'divorced', 'widowed'))),
    CHECK (militarybranch  (value IN ('n/a', 'army', 'air force', 'navy', 'marines', 'coast guard', 'national guard'))),
    CHECK (militarystatus  (value IN ('n/a', 'active', 'reserve', 'retired'))),
    CHECK (studentstatus  (value IN ('n/a', 'full time', 'part time'))),
    CHECK (basis  (value IN ('bill', 'allowed'))),
    CHECK (frequency  (value IN ('per visit', 'monthly', 'yearly'))),
    CHECK (accidenttype  (value IN ('auto', 'no', 'other')))
,    PRIMARY KEY (id)
);

CREATE UNIQUE INDEX tbl_customer_accountnumber ON tbl_customer (accountnumber);
CREATE INDEX tbl_customer_idx_first_last_dob_middle ON tbl_customer (firstname, lastname, dateofbirth, middlename);

CREATE TABLE tbl_customer_insurance (
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
city varchar(25) NOT NULL DEFAULT '',
state char(2) NOT NULL DEFAULT '',
zip varchar(10) NOT NULL DEFAULT '',
basis varchar NOT NULL DEFAULT 'Bill',
customerid integer NOT NULL DEFAULT 0,
dateofbirth date NULL,
gender varchar NOT NULL DEFAULT 'Male',
groupnumber varchar(50) NOT NULL DEFAULT '',
id SERIAL NOT NULL,
inactivedate date NULL,
insurancecompanyid integer NOT NULL DEFAULT 0,
insurancetype char(2) NULL,
firstname varchar(25) NOT NULL DEFAULT '',
lastname varchar(30) NOT NULL DEFAULT '',
middlename char(1) NOT NULL DEFAULT '',
employer varchar(50) NOT NULL DEFAULT '',
mobile varchar(50) NOT NULL DEFAULT '',
paymentpercent integer NULL,
phone varchar(50) NOT NULL DEFAULT '',
policynumber varchar(50) NOT NULL DEFAULT '',
rank integer NULL,
relationshipcode char(2) NULL,
requesteligibility smallint NOT NULL DEFAULT 0,
requesteligibilityon date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
mir varchar[] NOT NULL DEFAULT '{}'
    CHECK (basis  (value IN ('bill', 'allowed'))),
    CHECK (gender  (value IN ('male', 'female')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_customer_notes (
id SERIAL NOT NULL,
customerid integer NOT NULL,
notes text NOT NULL,
active smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
operator varchar(50) NULL,
callbackdate timestamp NULL,
createdby smallint NULL,
createdat timestamp NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_customerclass (
code char(2) NOT NULL DEFAULT '',
description varchar(50) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_customertype (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_denial (
code varchar(6) NOT NULL,
description varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_depositdetails (
orderdetailsid integer NOT NULL,
orderid integer NOT NULL,
customerid integer NOT NULL,
amount decimal NOT NULL,
lastupdateuserid smallint NOT NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (orderdetailsid)
);

CREATE INDEX tbl_depositdetails_idx_deposits ON tbl_depositdetails (customerid, orderid, orderdetailsid);

CREATE TABLE tbl_deposits (
customerid integer NOT NULL,
orderid integer NOT NULL,
amount decimal NOT NULL,
date date NOT NULL,
paymentmethod varchar NOT NULL,
notes text NOT NULL,
lastupdateuserid smallint NOT NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (paymentmethod  (value IN ('cash', 'check', 'credit card')))
,    PRIMARY KEY (customerid, orderid)
);


CREATE TABLE tbl_doctor (
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
contact varchar(50) NOT NULL,
courtesy varchar NOT NULL,
fax varchar(50) NOT NULL,
firstname varchar(25) NOT NULL,
id integer NOT NULL DEFAULT 0,
lastname varchar(30) NOT NULL,
licensenumber varchar(16) NOT NULL,
licenseexpired date NULL,
medicaidnumber varchar(16) NOT NULL,
middlename varchar(1) NOT NULL,
otherid varchar(16) NOT NULL,
fedtaxid varchar(9) NOT NULL DEFAULT '',
deanumber varchar(20) NOT NULL DEFAULT '',
phone varchar(50) NOT NULL,
phone2 varchar(50) NOT NULL,
state varchar(2) NOT NULL,
suffix varchar(4) NOT NULL,
title varchar(50) NOT NULL,
typeid integer NULL,
upinnumber varchar(11) NOT NULL,
zip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
mir varchar[] NOT NULL DEFAULT '{}',
npi varchar(10) NULL,
pecosenrolled smallint NOT NULL DEFAULT 0
    CHECK (courtesy  (value IN ('dr.', 'miss', 'mr.', 'mrs.', 'rev.')))
);


CREATE TABLE tbl_doctortype (
id integer NOT NULL DEFAULT 0,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00'
);


CREATE TABLE tbl_eligibilityrequest (
id SERIAL NOT NULL,
customerid integer NOT NULL DEFAULT 0,
customerinsuranceid integer NOT NULL DEFAULT 0,
region varchar NOT NULL DEFAULT 'Region A',
requestbatchid integer NULL,
requesttime timestamp NOT NULL DEFAULT '1900-01-01 00:00:00',
requesttext text NOT NULL,
responsebatchid integer NULL,
responsetime timestamp NULL,
responsetext text NULL
    CHECK (region  (value IN ('region a', 'region b', 'region c', 'region d', 'zirmed', 'medi-cal', 'availity', 'office ally', 'ability')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_facility (
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
contact varchar(50) NOT NULL,
defaultdeliveryweek varchar NOT NULL,
directions text NULL,
fax varchar(50) NOT NULL,
id SERIAL NOT NULL,
medicaidid varchar(50) NOT NULL,
medicareid varchar(50) NOT NULL,
name varchar(50) NOT NULL,
phone varchar(50) NOT NULL,
phone2 varchar(50) NOT NULL,
postypeid integer NULL DEFAULT 12,
state varchar(2) NOT NULL,
zip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
npi varchar(10) NULL,
mir varchar[] NOT NULL DEFAULT '{}'
    CHECK (defaultdeliveryweek  (value IN ('1st week of month', '2nd week of month', '3rd week of month', '4th week of month', 'as needed')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_hao (
code varchar(10) NOT NULL,
description text NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_icd10 (
code varchar(8) NOT NULL,
description varchar(255) NOT NULL DEFAULT '',
header smallint NOT NULL DEFAULT 0,
activedate date NULL,
inactivedate date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00'
);


CREATE TABLE tbl_icd9 (
code varchar(6) NOT NULL DEFAULT '',
description varchar(255) NOT NULL DEFAULT '',
activedate date NULL,
inactivedate date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00'
);


CREATE TABLE tbl_image (
id SERIAL NOT NULL,
name varchar(50) NOT NULL DEFAULT '',
type varchar(50) NOT NULL DEFAULT '',
description text NULL,
customerid integer NULL,
orderid integer NULL,
invoiceid integer NULL,
doctorid integer NULL,
cmnformid integer NULL,
thumbnail bytea NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_insurancecompany (
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
basis varchar NOT NULL DEFAULT 'Bill',
city varchar(25) NOT NULL DEFAULT '',
contact varchar(50) NOT NULL DEFAULT '',
ecsformat varchar NOT NULL DEFAULT 'Region A',
expectedpercent double precision NULL,
fax varchar(50) NOT NULL DEFAULT '',
id integer NOT NULL DEFAULT 0,
name varchar(50) NOT NULL DEFAULT '',
phone varchar(50) NOT NULL DEFAULT '',
phone2 varchar(50) NOT NULL DEFAULT '',
pricecodeid integer NULL,
printhaooninvoice smallint NULL,
printinvoninvoice smallint NULL,
state char(2) NOT NULL DEFAULT '',
title varchar(50) NOT NULL DEFAULT '',
type integer NULL,
zip varchar(10) NOT NULL DEFAULT '',
medicarenumber varchar(50) NOT NULL DEFAULT '',
officeallynumber varchar(50) NOT NULL DEFAULT '',
zirmednumber varchar(50) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
invoiceformid integer NULL,
medicaidnumber varchar(50) NOT NULL DEFAULT '',
mir varchar[] NOT NULL DEFAULT '{}',
groupid integer NULL,
availitynumber varchar(50) NOT NULL DEFAULT '',
abilitynumber varchar(50) NOT NULL DEFAULT ''
    CHECK (basis  (value IN ('bill', 'allowed'))),
    CHECK (ecsformat  (value IN ('region a', 'region b', 'region c', 'region d', 'zirmed', 'medi-cal', 'availity', 'office ally', 'ability')))
);


CREATE TABLE tbl_insurancecompanygroup (
id integer NOT NULL DEFAULT 0,
name varchar(50) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00'
);


CREATE TABLE tbl_insurancecompanytype (
id integer NOT NULL DEFAULT 0,
name varchar(50) NOT NULL DEFAULT ''
);


CREATE TABLE tbl_insurancetype (
code varchar(2) NOT NULL,
description varchar(40) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_inventory (
warehouseid integer NOT NULL DEFAULT 0,
inventoryitemid integer NOT NULL DEFAULT 0,
onhand double precision NOT NULL DEFAULT 0,
committed double precision NOT NULL DEFAULT 0,
onorder double precision NOT NULL DEFAULT 0,
unavailable double precision NOT NULL DEFAULT 0,
rented double precision NOT NULL DEFAULT 0,
sold double precision NOT NULL DEFAULT 0,
backordered double precision NOT NULL DEFAULT 0,
reorderpoint double precision NOT NULL DEFAULT 0,
costperunit decimal NOT NULL DEFAULT '0.00',
totalcost decimal NOT NULL DEFAULT '0.00',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (warehouseid, inventoryitemid)
);


CREATE TABLE tbl_inventory_transaction (
id SERIAL NOT NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
warehouseid integer NOT NULL DEFAULT 0,
typeid integer NOT NULL DEFAULT 0,
date date NOT NULL DEFAULT '0000-00-00',
quantity double precision NULL,
cost decimal NULL,
description varchar(30) NULL,
serialid integer NULL,
vendorid integer NULL,
customerid integer NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
purchaseorderid integer NULL,
purchaseorderdetailsid integer NULL,
invoiceid integer NULL,
manufacturerid integer NULL,
orderdetailsid integer NULL,
orderid integer NULL
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_inventory_transaction_idx_typeid_custid_orid_ordetailsid_itemid_warehouseid ON tbl_inventory_transaction (typeid, customerid, orderid, orderdetailsid, inventoryitemid, warehouseid);
CREATE INDEX tbl_inventory_transaction_idx_typeid_itemid_warehouseid ON tbl_inventory_transaction (typeid, inventoryitemid, warehouseid);
CREATE INDEX tbl_inventory_transaction_idx_typeid_poid_podetailsid_itemid_warehouseid ON tbl_inventory_transaction (typeid, purchaseorderid, purchaseorderdetailsid, inventoryitemid, warehouseid);

CREATE TABLE tbl_inventory_transaction_type (
id SERIAL NOT NULL,
name varchar(50) NOT NULL DEFAULT '',
onhand integer NOT NULL DEFAULT 0,
committed integer NOT NULL DEFAULT 0,
onorder integer NOT NULL DEFAULT 0,
unavailable integer NOT NULL DEFAULT 0,
rented integer NOT NULL DEFAULT 0,
sold integer NOT NULL DEFAULT 0,
backordered integer NOT NULL DEFAULT 0,
adjtotalcost integer NOT NULL DEFAULT 0
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_inventoryitem (
barcode varchar(50) NOT NULL DEFAULT '',
barcodetype varchar(50) NOT NULL DEFAULT '',
basis varchar NOT NULL DEFAULT 'Bill',
commissionpaidat varchar NOT NULL DEFAULT 'Billing',
vendorid integer NULL,
flatrate smallint NOT NULL DEFAULT 0,
flatrateamount double precision NULL,
frequency varchar NOT NULL DEFAULT 'One time',
id SERIAL NOT NULL,
inventorycode varchar(50) NOT NULL DEFAULT '',
modelnumber varchar(50) NOT NULL DEFAULT '',
name varchar(100) NOT NULL DEFAULT '',
o2tank smallint NOT NULL DEFAULT 0,
percentage smallint NOT NULL DEFAULT 0,
percentageamount double precision NOT NULL DEFAULT 0,
predefinedtextid integer NULL,
producttypeid integer NULL,
serialized smallint NOT NULL DEFAULT 0,
service smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
inactive smallint NOT NULL DEFAULT 0,
manufacturerid integer NULL,
purchaseprice decimal NOT NULL DEFAULT '0.00',
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT ''
    CHECK (basis  (value IN ('bill', 'allowed'))),
    CHECK (commissionpaidat  (value IN ('billing', 'payment', 'never'))),
    CHECK (frequency  (value IN ('one time', 'monthly', 'weekly', 'never')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_invoice (
id SERIAL NOT NULL,
customerid integer NOT NULL DEFAULT 0,
orderid integer NULL,
approved smallint NOT NULL DEFAULT 0,
invoicedate date NULL,
invoicebalance decimal NOT NULL DEFAULT '0.00',
submittedto varchar NOT NULL DEFAULT 'Ins1',
submittedby varchar(50) NULL,
submitteddate date NULL,
submittedbatch varchar(50) NULL,
customerinsurance1_id integer NULL,
customerinsurance2_id integer NULL,
customerinsurance3_id integer NULL,
customerinsurance4_id integer NULL,
icd9_1 varchar(6) NULL,
icd9_2 varchar(6) NULL,
icd9_3 varchar(6) NULL,
icd9_4 varchar(6) NULL,
doctorid integer NULL,
postypeid integer NULL,
taxrateid integer NULL,
taxratepercent double precision NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
discount double precision NULL DEFAULT 0,
acceptassignment smallint NOT NULL DEFAULT 0,
claimnote varchar(80) NULL,
facilityid integer NULL,
referralid integer NULL,
salesrepid integer NULL,
archived smallint NOT NULL DEFAULT 0,
icd10_01 varchar(8) NULL,
icd10_02 varchar(8) NULL,
icd10_03 varchar(8) NULL,
icd10_04 varchar(8) NULL,
icd10_05 varchar(8) NULL,
icd10_06 varchar(8) NULL,
icd10_07 varchar(8) NULL,
icd10_08 varchar(8) NULL,
icd10_09 varchar(8) NULL,
icd10_10 varchar(8) NULL,
icd10_11 varchar(8) NULL,
icd10_12 varchar(8) NULL
    CHECK (submittedto  (value IN ('ins1', 'ins2', 'ins3', 'ins4', 'patient')))
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_invoice_idx_customerid_id ON tbl_invoice (customerid, id);
CREATE INDEX tbl_invoice_idx_customerid_orderid ON tbl_invoice (customerid, orderid);

CREATE TABLE tbl_invoice_transaction (
id SERIAL NOT NULL,
invoicedetailsid integer NOT NULL DEFAULT 0,
invoiceid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
insurancecompanyid integer NULL,
customerinsuranceid integer NULL,
transactiontypeid integer NOT NULL DEFAULT 0,
transactiondate date NULL,
amount decimal NOT NULL DEFAULT '0.00',
quantity double precision NOT NULL DEFAULT 0,
taxes decimal NOT NULL DEFAULT '0.00',
batchnumber varchar(20) NOT NULL DEFAULT '',
comments text NULL,
extra text NULL,
approved smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
deductible decimal NOT NULL DEFAULT '0.00'
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_invoice_transaction_idx_customerid_invoiceid_invoicedetailsid ON tbl_invoice_transaction (customerid, invoiceid, invoicedetailsid);

CREATE TABLE tbl_invoice_transactiontype (
id integer NOT NULL DEFAULT 0,
name varchar(50) NOT NULL DEFAULT '',
balance integer NOT NULL DEFAULT 0,
allowable integer NOT NULL DEFAULT 0,
amount integer NOT NULL DEFAULT 0,
taxes integer NOT NULL DEFAULT 0
,    PRIMARY KEY (id)
);

CREATE UNIQUE INDEX tbl_invoice_transactiontype_ix_invoice_transactiontype_name ON tbl_invoice_transactiontype (name);

CREATE TABLE tbl_invoicedetails (
id SERIAL NOT NULL,
invoiceid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
inventoryitemid integer NOT NULL DEFAULT 0,
pricecodeid integer NOT NULL DEFAULT 0,
orderid integer NULL,
orderdetailsid integer NULL,
balance decimal NOT NULL DEFAULT '0.00',
billableamount decimal NOT NULL DEFAULT '0.00',
allowableamount decimal NOT NULL DEFAULT '0.00',
taxes decimal NOT NULL DEFAULT '0.00',
quantity double precision NOT NULL DEFAULT 0,
invoicedate date NULL,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
billingcode varchar(50) NULL,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
dxpointer varchar(50) NULL,
billingmonth integer NOT NULL DEFAULT 0,
sendcmn_rx_w_invoice smallint NOT NULL DEFAULT 0,
specialcode varchar(50) NULL,
reviewcode varchar(50) NULL,
medicallyunnecessary smallint NOT NULL DEFAULT 0,
authorizationnumber varchar(50) NULL,
authorizationtypeid integer NULL,
invoicenotes varchar(255) NULL,
invoicerecord varchar(255) NULL,
cmnformid integer NULL,
haocode varchar(10) NULL,
billins1 smallint NOT NULL DEFAULT 1,
billins2 smallint NOT NULL DEFAULT 1,
billins3 smallint NOT NULL DEFAULT 1,
billins4 smallint NOT NULL DEFAULT 1,
hardship smallint NOT NULL DEFAULT 0,
showspandates smallint NOT NULL DEFAULT 0,
paymentamount decimal NOT NULL DEFAULT '0.00',
writeoffamount decimal NOT NULL DEFAULT '0.00',
currentpayer varchar NOT NULL DEFAULT 'Ins1',
pendings smallint NOT NULL DEFAULT 0,
submits smallint NOT NULL DEFAULT 0,
payments smallint NOT NULL DEFAULT 0,
submitteddate date NULL,
submitted smallint NOT NULL DEFAULT 0,
currentinsurancecompanyid integer NULL,
currentcustomerinsuranceid integer NULL,
acceptassignment smallint NOT NULL DEFAULT 0,
deductibleamount decimal NOT NULL DEFAULT '0.00',
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
nopayins1 smallint NOT NULL DEFAULT 0,
pointericd10 smallint NOT NULL DEFAULT 0,
dxpointer10 varchar(50) NULL,
haodescription varchar(100) NULL
    CHECK (currentpayer  (value IN ('ins1', 'ins2', 'ins3', 'ins4', 'patient', 'none')))
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_invoicedetails_idx_customerid_invoiceid_id ON tbl_invoicedetails (customerid, invoiceid, id);

CREATE TABLE tbl_invoiceform (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
reportfilename varchar(50) NOT NULL,
margintop double precision NOT NULL DEFAULT 0.25,
marginleft double precision NOT NULL DEFAULT 0.19,
marginbottom double precision NOT NULL DEFAULT 0.18,
marginright double precision NOT NULL DEFAULT 0.22,
specialcoding varchar(20) NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_invoicenotes (
id SERIAL NOT NULL,
invoicedetailsid integer NOT NULL DEFAULT 0,
invoiceid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
callbackdate date NULL,
done smallint NOT NULL DEFAULT 0,
notes text NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_kit (
id SERIAL NOT NULL,
name varchar(50) NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_kitdetails (
id SERIAL NOT NULL,
kitid integer NOT NULL,
warehouseid integer NOT NULL,
inventoryitemid integer NOT NULL,
pricecodeid integer NULL,
quantity integer NOT NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_legalrep (
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
courtesy varchar NOT NULL,
firstname varchar(25) NOT NULL,
officephone varchar(50) NOT NULL,
id SERIAL NOT NULL,
lastname varchar(30) NOT NULL,
middlename varchar(1) NOT NULL,
mobile varchar(50) NOT NULL,
pager varchar(50) NOT NULL,
state varchar(2) NOT NULL,
suffix varchar(4) NOT NULL,
zip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
firmname varchar(50) NULL
    CHECK (courtesy  (value IN ('dr.', 'miss', 'mr.', 'mrs.', 'rev.')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_location (
id SERIAL NOT NULL,
contact varchar(50) NOT NULL DEFAULT '',
name varchar(50) NOT NULL DEFAULT '',
code varchar(40) NOT NULL DEFAULT '',
city varchar(25) NOT NULL DEFAULT '',
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
state char(2) NOT NULL DEFAULT '',
zip varchar(10) NOT NULL DEFAULT '',
fax varchar(50) NOT NULL DEFAULT '',
fedtaxid varchar(50) NOT NULL DEFAULT '',
taxidtype varchar NOT NULL DEFAULT 'SSN',
phone varchar(50) NOT NULL DEFAULT '',
phone2 varchar(50) NOT NULL DEFAULT '',
printinfoondelpupticket smallint NULL,
printinfooninvoiceacctstatements smallint NULL,
printinfoonpartprovider smallint NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
npi varchar(10) NULL,
invoiceformid integer NULL,
pricecodeid integer NULL,
participatingprovider smallint NULL,
email varchar(50) NULL,
warehouseid integer NULL,
postypeid integer NULL DEFAULT 12,
taxrateid integer NULL
    CHECK (taxidtype  (value IN ('ssn', 'ein')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_manufacturer (
accountnumber varchar(40) NOT NULL,
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
contact varchar(50) NOT NULL,
fax varchar(50) NOT NULL,
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
phone varchar(50) NOT NULL,
phone2 varchar(50) NOT NULL,
state varchar(2) NOT NULL,
zip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_medicalconditions (
code varchar(6) NOT NULL,
description varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_object (
id SERIAL NOT NULL,
description varchar(50) NOT NULL,
name varchar(50) NOT NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_order (
id SERIAL NOT NULL,
customerid integer NOT NULL DEFAULT 0,
approved smallint NOT NULL DEFAULT 0,
retailsales smallint NOT NULL DEFAULT 0,
orderdate date NULL,
deliverydate date NULL,
billdate date NULL,
enddate date NULL,
shippingmethodid integer NULL,
specialinstructions text NULL,
ticketmesage varchar(50) NULL,
customerinsurance1_id integer NULL,
customerinsurance2_id integer NULL,
customerinsurance3_id integer NULL,
customerinsurance4_id integer NULL,
icd9_1 varchar(6) NULL,
icd9_2 varchar(6) NULL,
icd9_3 varchar(6) NULL,
icd9_4 varchar(6) NULL,
doctorid integer NULL,
postypeid integer NULL,
takenby varchar(50) NULL DEFAULT '',
discount double precision NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
saletype varchar NOT NULL DEFAULT 'Back Office',
state varchar NOT NULL DEFAULT 'New',
mir varchar[] NOT NULL DEFAULT '{}',
acceptassignment smallint NOT NULL DEFAULT 0,
claimnote varchar(80) NULL,
facilityid integer NULL,
referralid integer NULL,
salesrepid integer NULL,
locationid integer NULL,
archived smallint NOT NULL DEFAULT 0,
takenat timestamp NULL,
icd10_01 varchar(8) NULL,
icd10_02 varchar(8) NULL,
icd10_03 varchar(8) NULL,
icd10_04 varchar(8) NULL,
icd10_05 varchar(8) NULL,
icd10_06 varchar(8) NULL,
icd10_07 varchar(8) NULL,
icd10_08 varchar(8) NULL,
icd10_09 varchar(8) NULL,
icd10_10 varchar(8) NULL,
icd10_11 varchar(8) NULL,
icd10_12 varchar(8) NULL,
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT ''
    CHECK (saletype  (value IN ('retail', 'back office'))),
    CHECK (state  (value IN ('new', 'approved', 'closed', 'canceled')))
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_order_idx_customerid_id ON tbl_order (customerid, id);

CREATE TABLE tbl_order_survey (
id SERIAL NOT NULL,
surveyid integer NOT NULL,
orderid integer NOT NULL,
form text NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);

CREATE UNIQUE INDEX tbl_order_survey_orderid ON tbl_order_survey (orderid);

CREATE TABLE tbl_orderdeposits (
orderdetailsid integer NOT NULL,
orderid integer NOT NULL,
customerid integer NOT NULL,
amount decimal NOT NULL,
date date NOT NULL,
lastupdateuserid smallint NOT NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (orderdetailsid)
);

CREATE INDEX tbl_orderdeposits_idx_orderdeposits ON tbl_orderdeposits (customerid, orderid, orderdetailsid);

CREATE TABLE tbl_orderdetails (
id SERIAL NOT NULL,
orderid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
serialnumber varchar(50) NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
pricecodeid integer NOT NULL DEFAULT 0,
salerenttype varchar NOT NULL DEFAULT 'Monthly Rental',
serialid integer NULL,
billableprice decimal NOT NULL DEFAULT '0.00',
allowableprice decimal NOT NULL DEFAULT '0.00',
taxable smallint NOT NULL DEFAULT 0,
flatrate smallint NOT NULL DEFAULT 0,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
pickupdate date NULL,
showspandates smallint NOT NULL DEFAULT 0,
orderedquantity double precision NOT NULL DEFAULT 0,
orderedunits varchar(50) NULL,
orderedwhen varchar NOT NULL DEFAULT 'One time',
orderedconverter double precision NOT NULL DEFAULT 1,
billedquantity double precision NOT NULL DEFAULT 0,
billedunits varchar(50) NULL,
billedwhen varchar NOT NULL DEFAULT 'One time',
billedconverter double precision NOT NULL DEFAULT 1,
deliveryquantity double precision NOT NULL DEFAULT 0,
deliveryunits varchar(50) NULL,
deliveryconverter double precision NOT NULL DEFAULT 1,
billingcode varchar(50) NULL,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
dxpointer varchar(50) NULL,
billingmonth integer NOT NULL DEFAULT 1,
billitemon varchar NOT NULL DEFAULT 'Day of Delivery',
authorizationnumber varchar(50) NULL,
authorizationtypeid integer NULL,
reasonforpickup varchar(50) NULL,
sendcmn_rx_w_invoice smallint NOT NULL DEFAULT 0,
medicallyunnecessary smallint NOT NULL DEFAULT 0,
sale smallint NOT NULL DEFAULT 0,
specialcode varchar(50) NULL,
reviewcode varchar(50) NULL,
nextorderid integer NULL,
reoccuringid integer NULL,
cmnformid integer NULL,
haocode varchar(10) NULL,
state varchar NOT NULL DEFAULT 'New',
billins1 smallint NOT NULL DEFAULT 1,
billins2 smallint NOT NULL DEFAULT 1,
billins3 smallint NOT NULL DEFAULT 1,
billins4 smallint NOT NULL DEFAULT 1,
enddate date NULL,
mir varchar[] NOT NULL DEFAULT '{}',
nextbillingdate date NULL,
warehouseid integer NOT NULL,
acceptassignment smallint NOT NULL DEFAULT 0,
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
nopayins1 smallint NOT NULL DEFAULT 0,
pointericd10 smallint NOT NULL DEFAULT 0,
dxpointer10 varchar(50) NULL,
mir_order varchar[] NOT NULL DEFAULT '{}',
haodescription varchar(100) NULL,
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT '',
authorizationexpirationdate date NULL
    CHECK (salerenttype  (value IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 'capped rental', 'parental capped rental', 'rent to purchase', 'one time sale', 're-occurring sale'))),
    CHECK (orderedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'quarterly', 'semi-annually', 'annually'))),
    CHECK (billedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom'))),
    CHECK (billitemon  (value IN ('day of delivery', 'last day of the month', 'last day of the period', 'day of pick-up'))),
    CHECK (state  (value IN ('new', 'approved', 'pickup', 'closed', 'canceled')))
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_orderdetails_idx_customerid_orderid_id ON tbl_orderdetails (customerid, orderid, id);
CREATE INDEX tbl_orderdetails_idx_customerid_orderid_id_inventoryitemid ON tbl_orderdetails (customerid, orderid, id, inventoryitemid);
CREATE INDEX tbl_orderdetails_idx_customerid_nextorderid ON tbl_orderdetails (customerid, nextorderid);
CREATE INDEX tbl_orderdetails_idx_inventoryitemid_serialnumber ON tbl_orderdetails (inventoryitemid, serialnumber);

CREATE TABLE tbl_payer (
insurancecompanyid integer NOT NULL,
participatingprovider smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NOT NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
extractorderingphysician smallint NOT NULL DEFAULT 1,
extractreferringphysician smallint NOT NULL DEFAULT 0,
extractrenderingprovider smallint NOT NULL DEFAULT 0,
taxonomycodeprefix varchar(10) NOT NULL DEFAULT ''
,    PRIMARY KEY (insurancecompanyid)
);


CREATE TABLE tbl_paymentplan (
id SERIAL NOT NULL,
customerid integer NOT NULL,
period varchar NOT NULL DEFAULT 'Weekly',
firstpayment date NOT NULL DEFAULT '1900-01-01',
paymentcount integer NOT NULL,
paymentamount decimal NOT NULL DEFAULT '0.00',
details text NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (period  (value IN ('weekly', 'bi-weekly', 'monthly')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_paymentplan_payments (
id SERIAL NOT NULL,
paymentplanid integer NOT NULL,
customerid integer NOT NULL,
index integer NOT NULL,
duedate date NOT NULL DEFAULT '1900-01-01',
dueamount decimal NOT NULL DEFAULT '0.00',
paymentdate date NULL,
paymentamount decimal NOT NULL DEFAULT '0.00',
details text NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_permissions (
userid smallint NOT NULL,
objectid smallint NOT NULL,
add_edit smallint NOT NULL DEFAULT 0,
delete smallint NOT NULL DEFAULT 0,
process smallint NOT NULL DEFAULT 0,
view smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (userid, objectid)
);


CREATE TABLE tbl_postype (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_predefinedtext (
id SERIAL NOT NULL,
name varchar(50) NOT NULL DEFAULT '',
type varchar NOT NULL DEFAULT 'Document Text',
text text NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (type  (value IN ('document text', 'account statements', 'compliance notes', 'customer notes', 'invoice notes', 'hao')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_pricecode (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_pricecode_item (
acceptassignment smallint NOT NULL DEFAULT 0,
orderedquantity double precision NOT NULL DEFAULT 0,
orderedunits varchar(50) NULL,
orderedwhen varchar NOT NULL DEFAULT 'One time',
orderedconverter double precision NOT NULL DEFAULT 1,
billedunits varchar(50) NULL,
billedwhen varchar NOT NULL DEFAULT 'One time',
billedconverter double precision NOT NULL DEFAULT 1,
deliveryunits varchar(50) NULL,
deliveryconverter double precision NOT NULL DEFAULT 1,
billingcode varchar(50) NULL,
billitemon varchar NOT NULL DEFAULT 'Day of Delivery',
defaultcmntype varchar NOT NULL DEFAULT 'DME 484.03',
defaultordertype varchar NOT NULL DEFAULT 'Sale',
authorizationtypeid integer NULL,
flatrate smallint NOT NULL DEFAULT 0,
id SERIAL NOT NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
pricecodeid integer NOT NULL DEFAULT 0,
predefinedtextid integer NULL,
rent_allowableprice decimal NOT NULL DEFAULT '0.00',
rent_billableprice decimal NOT NULL DEFAULT '0.00',
sale_allowableprice decimal NOT NULL DEFAULT '0.00',
sale_billableprice decimal NOT NULL DEFAULT '0.00',
rentaltype varchar NOT NULL DEFAULT 'Monthly Rental',
reoccuringsale smallint NOT NULL DEFAULT 0,
showspandates smallint NOT NULL DEFAULT 0,
taxable smallint NOT NULL DEFAULT 0,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
billinsurance smallint NOT NULL DEFAULT 1,
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT ''
    CHECK (orderedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'quarterly', 'semi-annually', 'annually'))),
    CHECK (billedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom'))),
    CHECK (billitemon  (value IN ('day of delivery', 'last day of the month', 'last day of the period', 'day of pick-up'))),
    CHECK (defaultcmntype  (value IN ('dmerc 02.03a', 'dmerc 02.03b', 'dmerc 03.02', 'dmerc 07.02b', 'dmerc 08.02', 'dmerc drorder', 'dmerc uro', 'dme 04.04b', 'dme 04.04c', 'dme 06.03b', 'dme 07.03a', 'dme 09.03', 'dme 10.03', 'dme 484.03'))),
    CHECK (defaultordertype  (value IN ('sale', 'rental'))),
    CHECK (rentaltype  (value IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 'capped rental', 'parental capped rental', 'rent to purchase')))
,    PRIMARY KEY (id)
);

CREATE UNIQUE INDEX tbl_pricecode_item_inventoryitemid ON tbl_pricecode_item (inventoryitemid, pricecodeid);

CREATE TABLE tbl_producttype (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_provider (
id SERIAL NOT NULL,
locationid integer NOT NULL DEFAULT 0,
insurancecompanyid integer NOT NULL DEFAULT 0,
providernumber varchar(25) NOT NULL DEFAULT '',
password varchar(20) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
providernumbertype varchar(6) NOT NULL DEFAULT '1C'
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_providernumbertype (
code varchar(6) NOT NULL,
description varchar(100) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_purchaseorder (
approved smallint NOT NULL DEFAULT 0,
cost decimal NOT NULL DEFAULT '0.00',
freight decimal NOT NULL DEFAULT '0.00',
id SERIAL NOT NULL,
tax decimal NOT NULL DEFAULT '0.00',
totaldue decimal NOT NULL DEFAULT '0.00',
vendorid integer NOT NULL,
shiptoname varchar(50) NOT NULL,
shiptoaddress1 varchar(40) NOT NULL,
shiptoaddress2 varchar(40) NOT NULL,
shiptocity varchar(25) NOT NULL,
shiptostate varchar(2) NOT NULL,
shiptozip varchar(10) NOT NULL,
shiptophone varchar(50) NOT NULL,
orderdate date NULL,
companyname varchar(50) NOT NULL,
companyaddress1 varchar(40) NOT NULL,
companyaddress2 varchar(40) NOT NULL,
companycity varchar(25) NOT NULL,
companystate varchar(2) NOT NULL,
companyzip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
shipvia varchar NULL,
fob varchar(50) NULL,
vendorsalesrep varchar(50) NULL,
terms text NULL,
companyphone varchar(50) NULL,
taxrateid integer NULL,
reoccuring smallint NOT NULL DEFAULT 0,
createddate date NULL,
createduserid smallint NULL,
submitteddate date NULL,
submitteduserid smallint NULL,
locationid integer NULL,
number varchar(40) NOT NULL DEFAULT '',
archived smallint NOT NULL DEFAULT 0,
confirmationnumber varchar(50) NULL
    CHECK (shipvia  (value IN ('best way', 'ups/rps')))
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_purchaseorder_ix_purchaseorder_search ON tbl_purchaseorder (locationid, id, number, vendorid, orderdate, submitteddate, approved);

CREATE TABLE tbl_purchaseorderdetails (
backorder integer NOT NULL DEFAULT 0,
ordered integer NOT NULL DEFAULT 0,
received integer NOT NULL DEFAULT 0,
price double precision NOT NULL DEFAULT 0,
customer varchar(50) NULL,
datepromised date NULL,
datereceived date NULL,
dropshiptocustomer smallint NOT NULL DEFAULT 0,
id SERIAL NOT NULL,
inventoryitemid integer NOT NULL,
purchaseorderid integer NULL,
warehouseid integer NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
vendorstknumber varchar(50) NULL,
referencenumber varchar(50) NULL
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_purchaseorderdetails_ix_purchaseorderdetails_parent ON tbl_purchaseorderdetails (purchaseorderid);

CREATE TABLE tbl_referral (
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
courtesy varchar NOT NULL,
employer varchar(50) NOT NULL,
fax varchar(50) NOT NULL,
firstname varchar(25) NOT NULL,
homephone varchar(50) NOT NULL,
id SERIAL NOT NULL,
lastname varchar(30) NOT NULL,
middlename varchar(1) NOT NULL,
mobile varchar(50) NOT NULL,
referraltypeid integer NULL,
state varchar(2) NOT NULL,
suffix varchar(4) NOT NULL,
workphone varchar(50) NOT NULL,
zip varchar(10) NOT NULL,
lastcontacted date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (courtesy  (value IN ('dr.', 'miss', 'mr.', 'mrs.', 'rev.')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_referraltype (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_relationship (
code char(2) NOT NULL DEFAULT '',
description varchar(100) NOT NULL DEFAULT ''
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_salesrep (
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
courtesy varchar NOT NULL,
firstname varchar(25) NOT NULL,
homephone varchar(50) NOT NULL,
id SERIAL NOT NULL,
lastname varchar(30) NOT NULL,
middlename varchar(1) NOT NULL,
mobile varchar(50) NOT NULL,
pager varchar(50) NOT NULL,
state varchar(2) NOT NULL,
suffix varchar(4) NOT NULL,
zip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (courtesy  (value IN ('dr.', 'miss', 'mr.', 'mrs.', 'rev.')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_serial (
id SERIAL NOT NULL,
currentcustomerid integer NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
lastcustomerid integer NULL,
manufacturerid integer NULL,
vendorid integer NULL,
warehouseid integer NULL,
lengthofwarranty varchar(50) NOT NULL DEFAULT '',
lotnumber varchar(50) NOT NULL DEFAULT '',
maintenancerecord text NOT NULL,
manufaturerserialnumber varchar(50) NOT NULL DEFAULT '',
modelnumber varchar(50) NOT NULL DEFAULT '',
monthsrented varchar(50) NOT NULL DEFAULT '',
nextmaintenancedate date NULL,
purchaseorderid integer NULL,
purchaseamount double precision NOT NULL DEFAULT 0,
purchasedate date NULL,
serialnumber varchar(50) NOT NULL DEFAULT '',
solddate date NULL,
status varchar NOT NULL DEFAULT 'Empty',
warranty varchar(50) NOT NULL DEFAULT '',
ownrent varchar NOT NULL DEFAULT 'Own',
firstrented date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
salvagevalue decimal NULL,
saleprice decimal NULL,
consignmenttype varchar(20) NULL,
consignmentname varchar(50) NULL,
consignmentdate timestamp NULL,
vendorstocknumber varchar(20) NULL,
lotnumberexpires timestamp NULL
    CHECK (status  (value IN ('empty', 'filled', 'junked', 'lost', 'reserved', 'on hand', 'rented', 'sold', 'sent', 'maintenance', 'transferred out'))),
    CHECK (ownrent  (value IN ('own', 'rent')))
,    PRIMARY KEY (id)
);

CREATE INDEX tbl_serial_idx_inventoryitemid_serialnumber ON tbl_serial (inventoryitemid, serialnumber);

CREATE TABLE tbl_serial_maintenance (
id SERIAL NOT NULL,
serialid integer NOT NULL,
additionalequipment text NULL,
descriptionofproblem text NULL,
descriptionofwork text NULL,
maintenancerecord text NULL,
laborhours varchar(255) NULL,
technician varchar(255) NULL,
maintenancedue date NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
maintenancecost decimal NOT NULL DEFAULT '0.00'
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_serial_transaction (
id SERIAL NOT NULL,
typeid integer NOT NULL DEFAULT 0,
serialid integer NOT NULL DEFAULT 0,
transactiondatetime timestamp NOT NULL,
vendorid integer NULL,
warehouseid integer NULL,
customerid integer NULL,
orderid integer NULL,
orderdetailsid integer NULL,
lotnumber varchar(50) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_serial_transaction_type (
id SERIAL NOT NULL,
name varchar(50) NOT NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_sessions (
id SERIAL NOT NULL,
userid smallint NOT NULL,
logintime timestamp NOT NULL,
lastupdatetime timestamp NOT NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_shippingmethod (
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
type varchar(50) NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_signaturetype (
code char(1) NOT NULL DEFAULT '',
description varchar(100) NOT NULL DEFAULT ''
,    PRIMARY KEY (code)
);


CREATE TABLE tbl_submitter (
id SERIAL NOT NULL,
ecsformat varchar NULL,
name varchar(50) NOT NULL DEFAULT '',
number varchar(16) NOT NULL DEFAULT '',
password varchar(50) NOT NULL DEFAULT '',
production smallint NOT NULL DEFAULT 0,
contactname varchar(50) NOT NULL DEFAULT '',
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
city varchar(25) NOT NULL DEFAULT '',
state char(2) NOT NULL DEFAULT '',
zip varchar(10) NOT NULL DEFAULT '',
phone1 varchar(50) NOT NULL DEFAULT '',
lastbatchnumber varchar(50) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
    CHECK (ecsformat  (value IN ('region a', 'region b', 'region c', 'region d')))
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_survey (
id SERIAL NOT NULL,
name varchar(100) NOT NULL,
description varchar(200) NOT NULL,
template text NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_taxrate (
id SERIAL NOT NULL,
citytax double precision NULL,
countytax double precision NULL,
name varchar(50) NOT NULL,
othertax double precision NULL,
statetax double precision NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_user (
id smallint NOT NULL,
login varchar(16) NOT NULL,
password varchar(32) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
email varchar(150) NOT NULL DEFAULT ''
,    PRIMARY KEY (id)
);

CREATE UNIQUE INDEX tbl_user_login ON tbl_user (login);

CREATE TABLE tbl_user_location (
userid smallint NOT NULL,
locationid integer NOT NULL
,    PRIMARY KEY (userid, locationid)
);

CREATE UNIQUE INDEX tbl_user_location_locationid ON tbl_user_location (locationid, userid);

CREATE TABLE tbl_user_notifications (
id SERIAL NOT NULL,
type varchar(50) NOT NULL,
args varchar(255) NOT NULL,
userid smallint NOT NULL,
datetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_variables (
name varchar(31) NOT NULL,
value varchar(255) NOT NULL
,    PRIMARY KEY (name)
);


CREATE TABLE tbl_vendor (
accountnumber varchar(40) NOT NULL,
address1 varchar(40) NOT NULL,
address2 varchar(40) NOT NULL,
city varchar(25) NOT NULL,
contact varchar(50) NOT NULL,
fax varchar(50) NOT NULL,
id SERIAL NOT NULL,
name varchar(50) NOT NULL,
phone varchar(50) NOT NULL,
phone2 varchar(50) NOT NULL,
state varchar(2) NOT NULL,
zip varchar(10) NOT NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
comments text NULL,
fobdelivery varchar(50) NULL,
terms varchar(50) NULL,
shipvia varchar(50) NULL
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_warehouse (
address1 varchar(40) NOT NULL DEFAULT '',
address2 varchar(40) NOT NULL DEFAULT '',
city varchar(25) NOT NULL DEFAULT '',
contact varchar(50) NOT NULL DEFAULT '',
fax varchar(50) NOT NULL DEFAULT '',
id SERIAL NOT NULL,
name varchar(50) NOT NULL DEFAULT '',
phone varchar(50) NOT NULL DEFAULT '',
phone2 varchar(50) NOT NULL DEFAULT '',
state char(2) NOT NULL DEFAULT '',
zip varchar(10) NOT NULL DEFAULT '',
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,    PRIMARY KEY (id)
);


CREATE TABLE tbl_zipcode (
zip varchar(10) NOT NULL,
state varchar(2) NOT NULL,
city varchar(30) NOT NULL
);


CREATE TABLE view_billinglist (
orderid integer NOT NULL DEFAULT 0,
billingmonth bigint NOT NULL DEFAULT 0,
billingflags integer NOT NULL DEFAULT 0,
billingtypeid integer NULL
);


CREATE TABLE view_invoicetransaction_statistics (
customerid integer NOT NULL DEFAULT 0,
orderid integer NULL,
invoiceid integer NOT NULL DEFAULT 0,
invoicedetailsid integer NOT NULL DEFAULT 0,
billableamount decimal NOT NULL DEFAULT '0.00',
allowableamount decimal NOT NULL DEFAULT '0.00',
quantity double precision NOT NULL DEFAULT 0,
hardship smallint NOT NULL DEFAULT 0,
billingcode varchar(50) NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
insurance1_id integer NULL DEFAULT 0,
insurance2_id integer NULL DEFAULT 0,
insurance3_id integer NULL DEFAULT 0,
insurance4_id integer NULL DEFAULT 0,
insurancecompany1_id integer NULL DEFAULT 0,
insurancecompany2_id integer NULL DEFAULT 0,
insurancecompany3_id integer NULL DEFAULT 0,
insurancecompany4_id integer NULL DEFAULT 0,
percent bigint NOT NULL DEFAULT 0,
basis varchar(7) NOT NULL DEFAULT '',
paymentamount decimal NOT NULL DEFAULT '0.00',
writeoffamount decimal NOT NULL DEFAULT '0.00',
insurances integer NOT NULL DEFAULT 0,
pendingsubmissions decimal NOT NULL DEFAULT '0',
submits decimal NOT NULL DEFAULT '0',
payments decimal NOT NULL DEFAULT '0',
currentinsuranceid integer NULL,
currentinsurancecompanyid integer NULL,
invoicesubmitted smallint NOT NULL DEFAULT 0,
submitteddate date NULL,
currentpayer varchar NOT NULL DEFAULT 'Ins1',
nopayins1 smallint NOT NULL DEFAULT 0
    CHECK (currentpayer  (value IN ('ins1', 'ins2', 'ins3', 'ins4', 'patient', 'none')))
);


CREATE TABLE view_mir (
orderdetailsid integer NOT NULL DEFAULT 0,
orderid integer NOT NULL DEFAULT 0,
orderapproved smallint NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
customername varchar(56) NULL,
customerinsuranceid_1 integer NULL DEFAULT 0,
insurancecompanyid_1 integer NULL DEFAULT 0,
customerinsuranceid_2 integer NULL DEFAULT 0,
insurancecompanyid_2 integer NULL DEFAULT 0,
cmnformid integer NULL DEFAULT 0,
facilityid integer NULL DEFAULT 0,
doctorid integer NULL DEFAULT 0,
salerenttype varchar NOT NULL DEFAULT 'Monthly Rental',
billingcode varchar(50) NULL,
payers varchar(31) NULL,
inventoryitem varchar(100) NULL DEFAULT '',
pricecode varchar(50) NULL,
mir varchar(170) NULL,
details text NULL
    CHECK (salerenttype  (value IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 'capped rental', 'parental capped rental', 'rent to purchase', 'one time sale', 're-occurring sale')))
);


CREATE TABLE view_orderdetails (
id integer NOT NULL DEFAULT 0,
orderid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
serialnumber varchar(50) NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
pricecodeid integer NOT NULL DEFAULT 0,
salerenttype varchar NOT NULL DEFAULT 'Monthly Rental',
serialid integer NULL,
billableprice decimal NOT NULL DEFAULT '0.00',
allowableprice decimal NOT NULL DEFAULT '0.00',
taxable smallint NOT NULL DEFAULT 0,
flatrate smallint NOT NULL DEFAULT 0,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
pickupdate date NULL,
showspandates smallint NOT NULL DEFAULT 0,
orderedquantity double precision NOT NULL DEFAULT 0,
orderedunits varchar(50) NULL,
orderedwhen varchar NOT NULL DEFAULT 'One time',
orderedconverter double precision NOT NULL DEFAULT 1,
billedquantity double precision NOT NULL DEFAULT 0,
billedunits varchar(50) NULL,
billedwhen varchar NOT NULL DEFAULT 'One time',
billedconverter double precision NOT NULL DEFAULT 1,
deliveryquantity double precision NOT NULL DEFAULT 0,
deliveryunits varchar(50) NULL,
deliveryconverter double precision NOT NULL DEFAULT 1,
billingcode varchar(50) NULL,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
dxpointer varchar(50) NULL,
billingmonth integer NOT NULL DEFAULT 1,
billitemon varchar NOT NULL DEFAULT 'Day of Delivery',
authorizationnumber varchar(50) NULL,
authorizationtypeid integer NULL,
reasonforpickup varchar(50) NULL,
sendcmn_rx_w_invoice smallint NOT NULL DEFAULT 0,
medicallyunnecessary smallint NOT NULL DEFAULT 0,
sale smallint NOT NULL DEFAULT 0,
specialcode varchar(50) NULL,
reviewcode varchar(50) NULL,
nextorderid integer NULL,
reoccuringid integer NULL,
cmnformid integer NULL,
haocode varchar(10) NULL,
state varchar NOT NULL DEFAULT 'New',
billins1 smallint NOT NULL DEFAULT 1,
billins2 smallint NOT NULL DEFAULT 1,
billins3 smallint NOT NULL DEFAULT 1,
billins4 smallint NOT NULL DEFAULT 1,
enddate date NULL,
mir varchar[] NOT NULL DEFAULT '{}',
nextbillingdate date NULL,
warehouseid integer NOT NULL,
acceptassignment smallint NOT NULL DEFAULT 0,
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
nopayins1 smallint NOT NULL DEFAULT 0,
pointericd10 smallint NOT NULL DEFAULT 0,
dxpointer10 varchar(50) NULL,
mir_order varchar[] NOT NULL DEFAULT '{}',
haodescription varchar(100) NULL,
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT '',
authorizationexpirationdate date NULL,
isactive integer NOT NULL DEFAULT 0,
iscanceled integer NOT NULL DEFAULT 0,
issold integer NOT NULL DEFAULT 0,
isrented integer NOT NULL DEFAULT 0,
actualsalerenttype varchar(22) NOT NULL DEFAULT '',
actualbillitemon varchar(22) NOT NULL DEFAULT '',
actualorderedwhen varchar(13) NOT NULL DEFAULT '',
actualbilledwhen varchar(16) NOT NULL DEFAULT '',
actualdosto timestamp NULL,
invoicedate timestamp NULL,
isoxygen integer NOT NULL DEFAULT 0,
iszeroamount integer NOT NULL DEFAULT 0,
ispickedup integer NOT NULL DEFAULT 0
    CHECK (salerenttype  (value IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 'capped rental', 'parental capped rental', 'rent to purchase', 'one time sale', 're-occurring sale'))),
    CHECK (orderedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'quarterly', 'semi-annually', 'annually'))),
    CHECK (billedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom'))),
    CHECK (billitemon  (value IN ('day of delivery', 'last day of the month', 'last day of the period', 'day of pick-up'))),
    CHECK (state  (value IN ('new', 'approved', 'pickup', 'closed', 'canceled')))
);


CREATE TABLE view_orderdetails_core (
id integer NOT NULL DEFAULT 0,
orderid integer NOT NULL DEFAULT 0,
customerid integer NOT NULL DEFAULT 0,
serialnumber varchar(50) NULL,
inventoryitemid integer NOT NULL DEFAULT 0,
pricecodeid integer NOT NULL DEFAULT 0,
salerenttype varchar NOT NULL DEFAULT 'Monthly Rental',
serialid integer NULL,
billableprice decimal NOT NULL DEFAULT '0.00',
allowableprice decimal NOT NULL DEFAULT '0.00',
taxable smallint NOT NULL DEFAULT 0,
flatrate smallint NOT NULL DEFAULT 0,
dosfrom date NOT NULL DEFAULT '0000-00-00',
dosto date NULL,
pickupdate date NULL,
showspandates smallint NOT NULL DEFAULT 0,
orderedquantity double precision NOT NULL DEFAULT 0,
orderedunits varchar(50) NULL,
orderedwhen varchar NOT NULL DEFAULT 'One time',
orderedconverter double precision NOT NULL DEFAULT 1,
billedquantity double precision NOT NULL DEFAULT 0,
billedunits varchar(50) NULL,
billedwhen varchar NOT NULL DEFAULT 'One time',
billedconverter double precision NOT NULL DEFAULT 1,
deliveryquantity double precision NOT NULL DEFAULT 0,
deliveryunits varchar(50) NULL,
deliveryconverter double precision NOT NULL DEFAULT 1,
billingcode varchar(50) NULL,
modifier1 varchar(8) NOT NULL DEFAULT '',
modifier2 varchar(8) NOT NULL DEFAULT '',
modifier3 varchar(8) NOT NULL DEFAULT '',
modifier4 varchar(8) NOT NULL DEFAULT '',
dxpointer varchar(50) NULL,
billingmonth integer NOT NULL DEFAULT 1,
billitemon varchar NOT NULL DEFAULT 'Day of Delivery',
authorizationnumber varchar(50) NULL,
authorizationtypeid integer NULL,
reasonforpickup varchar(50) NULL,
sendcmn_rx_w_invoice smallint NOT NULL DEFAULT 0,
medicallyunnecessary smallint NOT NULL DEFAULT 0,
sale smallint NOT NULL DEFAULT 0,
specialcode varchar(50) NULL,
reviewcode varchar(50) NULL,
nextorderid integer NULL,
reoccuringid integer NULL,
cmnformid integer NULL,
haocode varchar(10) NULL,
state varchar NOT NULL DEFAULT 'New',
billins1 smallint NOT NULL DEFAULT 1,
billins2 smallint NOT NULL DEFAULT 1,
billins3 smallint NOT NULL DEFAULT 1,
billins4 smallint NOT NULL DEFAULT 1,
enddate date NULL,
mir varchar[] NOT NULL DEFAULT '{}',
nextbillingdate date NULL,
warehouseid integer NOT NULL,
acceptassignment smallint NOT NULL DEFAULT 0,
drugnotefield varchar(20) NULL,
drugcontrolnumber varchar(50) NULL,
nopayins1 smallint NOT NULL DEFAULT 0,
pointericd10 smallint NOT NULL DEFAULT 0,
dxpointer10 varchar(50) NULL,
mir_order varchar[] NOT NULL DEFAULT '{}',
haodescription varchar(100) NULL,
userfield1 varchar(100) NOT NULL DEFAULT '',
userfield2 varchar(100) NOT NULL DEFAULT '',
authorizationexpirationdate date NULL,
isactive integer NOT NULL DEFAULT 0,
iscanceled integer NOT NULL DEFAULT 0,
issold integer NOT NULL DEFAULT 0,
isrented integer NOT NULL DEFAULT 0,
actualsalerenttype varchar(22) NOT NULL DEFAULT '',
actualbillitemon varchar(22) NOT NULL DEFAULT '',
actualorderedwhen varchar(13) NOT NULL DEFAULT '',
actualbilledwhen varchar(16) NOT NULL DEFAULT '',
actualdosto timestamp NULL,
invoicedate timestamp NULL,
isoxygen integer NOT NULL DEFAULT 0,
iszeroamount integer NOT NULL DEFAULT 0
    CHECK (salerenttype  (value IN ('medicare oxygen rental', 'one time rental', 'monthly rental', 'capped rental', 'parental capped rental', 'rent to purchase', 'one time sale', 're-occurring sale'))),
    CHECK (orderedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'quarterly', 'semi-annually', 'annually'))),
    CHECK (billedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom'))),
    CHECK (billitemon  (value IN ('day of delivery', 'last day of the month', 'last day of the period', 'day of pick-up'))),
    CHECK (state  (value IN ('new', 'approved', 'pickup', 'closed', 'canceled')))
);


CREATE TABLE view_pricecode (
id integer NOT NULL DEFAULT 0,
name varchar(50) NOT NULL,
isretail integer NOT NULL DEFAULT 0
);


CREATE TABLE view_reoccuringlist (
orderid integer NOT NULL DEFAULT 0,
billedwhen varchar NOT NULL DEFAULT 'One time',
billitemon varchar(22) NOT NULL DEFAULT ''
    CHECK (billedwhen  (value IN ('one time', 'daily', 'weekly', 'monthly', 'calendar monthly', 'quarterly', 'semi-annually', 'annually', 'custom')))
);


CREATE TABLE view_sequence (
num bigint NOT NULL DEFAULT 0
);


CREATE TABLE view_sequence_core (
num bigint NOT NULL DEFAULT 0
);


CREATE TABLE view_taxrate (
id integer NOT NULL DEFAULT 0,
citytax double precision NULL,
countytax double precision NULL,
name varchar(50) NOT NULL,
othertax double precision NULL,
statetax double precision NULL,
lastupdateuserid smallint NULL,
lastupdatedatetime timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
totaltax double precision NOT NULL DEFAULT 0
);
