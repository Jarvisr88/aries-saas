-- Complex field conversion script
-- Generated at 20241212_135326

BEGIN;

-- ENUM Conversions

-- Converting tbl_cmnform.CMNType
-- Convert tbl_cmnform.CMNType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform ADD COLUMN CMNType_new VARCHAR(13);
UPDATE tbl_cmnform SET CMNType_new = CMNType::text;
ALTER TABLE tbl_cmnform DROP COLUMN CMNType;
ALTER TABLE tbl_cmnform RENAME COLUMN CMNType_new TO CMNType;
ALTER TABLE tbl_cmnform ADD CONSTRAINT tbl_cmnform_CMNType_check CHECK (CMNType IN ('DMERC 01.02A', 'DMERC 01.02B', 'DMERC 02.03A', 'DMERC 02.03B', 'DMERC 03.02', 'DMERC 04.03B', 'DMERC 04.03C', 'DMERC 06.02B', 'DMERC 07.02A', 'DMERC 07.02B', 'DMERC 08.02', 'DMERC 09.02', 'DMERC 10.02A', 'DMERC 10.02B', 'DMERC 484.2', 'DMERC DRORDER', 'DMERC URO', 'DME 04.04B', 'DME 04.04C', 'DME 06.03B', 'DME 07.03A', 'DME 09.03', 'DME 10.03', 'DME 484.03'));

-- Converting tbl_cmnform_0102a.Answer1
-- Convert tbl_cmnform_0102a.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102a ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0102a SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0102a DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0102a RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0102a ADD CONSTRAINT tbl_cmnform_0102a_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102a.Answer3
-- Convert tbl_cmnform_0102a.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102a ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0102a SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0102a DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0102a RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0102a ADD CONSTRAINT tbl_cmnform_0102a_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102a.Answer4
-- Convert tbl_cmnform_0102a.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102a ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0102a SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0102a DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0102a RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0102a ADD CONSTRAINT tbl_cmnform_0102a_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102a.Answer5
-- Convert tbl_cmnform_0102a.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102a ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0102a SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0102a DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0102a RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0102a ADD CONSTRAINT tbl_cmnform_0102a_Answer5_check CHECK (Answer5 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102a.Answer6
-- Convert tbl_cmnform_0102a.Answer6 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102a ADD COLUMN Answer6_new VARCHAR(1);
UPDATE tbl_cmnform_0102a SET Answer6_new = Answer6::text;
ALTER TABLE tbl_cmnform_0102a DROP COLUMN Answer6;
ALTER TABLE tbl_cmnform_0102a RENAME COLUMN Answer6_new TO Answer6;
ALTER TABLE tbl_cmnform_0102a ADD CONSTRAINT tbl_cmnform_0102a_Answer6_check CHECK (Answer6 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102a.Answer7
-- Convert tbl_cmnform_0102a.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102a ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_0102a SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_0102a DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_0102a RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_0102a ADD CONSTRAINT tbl_cmnform_0102a_Answer7_check CHECK (Answer7 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer12
-- Convert tbl_cmnform_0102b.Answer12 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer12_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer12_new = Answer12::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer12;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer12_new TO Answer12;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer12_check CHECK (Answer12 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer13
-- Convert tbl_cmnform_0102b.Answer13 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer13_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer13_new = Answer13::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer13;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer13_new TO Answer13;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer13_check CHECK (Answer13 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer14
-- Convert tbl_cmnform_0102b.Answer14 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer14_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer14_new = Answer14::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer14;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer14_new TO Answer14;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer14_check CHECK (Answer14 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer15
-- Convert tbl_cmnform_0102b.Answer15 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer15_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer15_new = Answer15::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer15;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer15_new TO Answer15;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer15_check CHECK (Answer15 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer16
-- Convert tbl_cmnform_0102b.Answer16 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer16_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer16_new = Answer16::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer16;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer16_new TO Answer16;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer16_check CHECK (Answer16 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer19
-- Convert tbl_cmnform_0102b.Answer19 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer19_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer19_new = Answer19::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer19;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer19_new TO Answer19;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer19_check CHECK (Answer19 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer20
-- Convert tbl_cmnform_0102b.Answer20 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer20_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer20_new = Answer20::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer20;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer20_new TO Answer20;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer20_check CHECK (Answer20 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0102b.Answer22
-- Convert tbl_cmnform_0102b.Answer22 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0102b ADD COLUMN Answer22_new VARCHAR(1);
UPDATE tbl_cmnform_0102b SET Answer22_new = Answer22::text;
ALTER TABLE tbl_cmnform_0102b DROP COLUMN Answer22;
ALTER TABLE tbl_cmnform_0102b RENAME COLUMN Answer22_new TO Answer22;
ALTER TABLE tbl_cmnform_0102b ADD CONSTRAINT tbl_cmnform_0102b_Answer22_check CHECK (Answer22 IN ('1', '2', '3'));

-- Converting tbl_cmnform_0203a.Answer1
-- Convert tbl_cmnform_0203a.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203a ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0203a SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0203a DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0203a RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0203a ADD CONSTRAINT tbl_cmnform_0203a_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203a.Answer2
-- Convert tbl_cmnform_0203a.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203a ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_0203a SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_0203a DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_0203a RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_0203a ADD CONSTRAINT tbl_cmnform_0203a_Answer2_check CHECK (Answer2 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203a.Answer3
-- Convert tbl_cmnform_0203a.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203a ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0203a SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0203a DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0203a RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0203a ADD CONSTRAINT tbl_cmnform_0203a_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203a.Answer4
-- Convert tbl_cmnform_0203a.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203a ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0203a SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0203a DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0203a RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0203a ADD CONSTRAINT tbl_cmnform_0203a_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203a.Answer6
-- Convert tbl_cmnform_0203a.Answer6 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203a ADD COLUMN Answer6_new VARCHAR(1);
UPDATE tbl_cmnform_0203a SET Answer6_new = Answer6::text;
ALTER TABLE tbl_cmnform_0203a DROP COLUMN Answer6;
ALTER TABLE tbl_cmnform_0203a RENAME COLUMN Answer6_new TO Answer6;
ALTER TABLE tbl_cmnform_0203a ADD CONSTRAINT tbl_cmnform_0203a_Answer6_check CHECK (Answer6 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203a.Answer7
-- Convert tbl_cmnform_0203a.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203a ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_0203a SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_0203a DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_0203a RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_0203a ADD CONSTRAINT tbl_cmnform_0203a_Answer7_check CHECK (Answer7 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203b.Answer1
-- Convert tbl_cmnform_0203b.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203b ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0203b SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0203b DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0203b RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0203b ADD CONSTRAINT tbl_cmnform_0203b_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203b.Answer2
-- Convert tbl_cmnform_0203b.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203b ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_0203b SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_0203b DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_0203b RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_0203b ADD CONSTRAINT tbl_cmnform_0203b_Answer2_check CHECK (Answer2 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203b.Answer3
-- Convert tbl_cmnform_0203b.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203b ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0203b SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0203b DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0203b RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0203b ADD CONSTRAINT tbl_cmnform_0203b_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203b.Answer4
-- Convert tbl_cmnform_0203b.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203b ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0203b SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0203b DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0203b RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0203b ADD CONSTRAINT tbl_cmnform_0203b_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203b.Answer8
-- Convert tbl_cmnform_0203b.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203b ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_0203b SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_0203b DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_0203b RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_0203b ADD CONSTRAINT tbl_cmnform_0203b_Answer8_check CHECK (Answer8 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0203b.Answer9
-- Convert tbl_cmnform_0203b.Answer9 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0203b ADD COLUMN Answer9_new VARCHAR(1);
UPDATE tbl_cmnform_0203b SET Answer9_new = Answer9::text;
ALTER TABLE tbl_cmnform_0203b DROP COLUMN Answer9;
ALTER TABLE tbl_cmnform_0203b RENAME COLUMN Answer9_new TO Answer9;
ALTER TABLE tbl_cmnform_0203b ADD CONSTRAINT tbl_cmnform_0203b_Answer9_check CHECK (Answer9 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0302.Answer14
-- Convert tbl_cmnform_0302.Answer14 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0302 ADD COLUMN Answer14_new VARCHAR(1);
UPDATE tbl_cmnform_0302 SET Answer14_new = Answer14::text;
ALTER TABLE tbl_cmnform_0302 DROP COLUMN Answer14;
ALTER TABLE tbl_cmnform_0302 RENAME COLUMN Answer14_new TO Answer14;
ALTER TABLE tbl_cmnform_0302 ADD CONSTRAINT tbl_cmnform_0302_Answer14_check CHECK (Answer14 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403b.Answer1
-- Convert tbl_cmnform_0403b.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403b ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0403b SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0403b DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0403b RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0403b ADD CONSTRAINT tbl_cmnform_0403b_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403b.Answer2
-- Convert tbl_cmnform_0403b.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403b ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_0403b SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_0403b DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_0403b RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_0403b ADD CONSTRAINT tbl_cmnform_0403b_Answer2_check CHECK (Answer2 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403b.Answer3
-- Convert tbl_cmnform_0403b.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403b ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0403b SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0403b DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0403b RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0403b ADD CONSTRAINT tbl_cmnform_0403b_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403b.Answer4
-- Convert tbl_cmnform_0403b.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403b ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0403b SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0403b DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0403b RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0403b ADD CONSTRAINT tbl_cmnform_0403b_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403b.Answer5
-- Convert tbl_cmnform_0403b.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403b ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0403b SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0403b DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0403b RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0403b ADD CONSTRAINT tbl_cmnform_0403b_Answer5_check CHECK (Answer5 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403c.Answer6a
-- Convert tbl_cmnform_0403c.Answer6a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403c ADD COLUMN Answer6a_new VARCHAR(1);
UPDATE tbl_cmnform_0403c SET Answer6a_new = Answer6a::text;
ALTER TABLE tbl_cmnform_0403c DROP COLUMN Answer6a;
ALTER TABLE tbl_cmnform_0403c RENAME COLUMN Answer6a_new TO Answer6a;
ALTER TABLE tbl_cmnform_0403c ADD CONSTRAINT tbl_cmnform_0403c_Answer6a_check CHECK (Answer6a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403c.Answer7a
-- Convert tbl_cmnform_0403c.Answer7a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403c ADD COLUMN Answer7a_new VARCHAR(1);
UPDATE tbl_cmnform_0403c SET Answer7a_new = Answer7a::text;
ALTER TABLE tbl_cmnform_0403c DROP COLUMN Answer7a;
ALTER TABLE tbl_cmnform_0403c RENAME COLUMN Answer7a_new TO Answer7a;
ALTER TABLE tbl_cmnform_0403c ADD CONSTRAINT tbl_cmnform_0403c_Answer7a_check CHECK (Answer7a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403c.Answer8
-- Convert tbl_cmnform_0403c.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403c ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_0403c SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_0403c DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_0403c RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_0403c ADD CONSTRAINT tbl_cmnform_0403c_Answer8_check CHECK (Answer8 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403c.Answer9a
-- Convert tbl_cmnform_0403c.Answer9a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403c ADD COLUMN Answer9a_new VARCHAR(1);
UPDATE tbl_cmnform_0403c SET Answer9a_new = Answer9a::text;
ALTER TABLE tbl_cmnform_0403c DROP COLUMN Answer9a;
ALTER TABLE tbl_cmnform_0403c RENAME COLUMN Answer9a_new TO Answer9a;
ALTER TABLE tbl_cmnform_0403c ADD CONSTRAINT tbl_cmnform_0403c_Answer9a_check CHECK (Answer9a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403c.Answer10a
-- Convert tbl_cmnform_0403c.Answer10a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403c ADD COLUMN Answer10a_new VARCHAR(1);
UPDATE tbl_cmnform_0403c SET Answer10a_new = Answer10a::text;
ALTER TABLE tbl_cmnform_0403c DROP COLUMN Answer10a;
ALTER TABLE tbl_cmnform_0403c RENAME COLUMN Answer10a_new TO Answer10a;
ALTER TABLE tbl_cmnform_0403c ADD CONSTRAINT tbl_cmnform_0403c_Answer10a_check CHECK (Answer10a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0403c.Answer11a
-- Convert tbl_cmnform_0403c.Answer11a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0403c ADD COLUMN Answer11a_new VARCHAR(1);
UPDATE tbl_cmnform_0403c SET Answer11a_new = Answer11a::text;
ALTER TABLE tbl_cmnform_0403c DROP COLUMN Answer11a;
ALTER TABLE tbl_cmnform_0403c RENAME COLUMN Answer11a_new TO Answer11a;
ALTER TABLE tbl_cmnform_0403c ADD CONSTRAINT tbl_cmnform_0403c_Answer11a_check CHECK (Answer11a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404b.Answer1
-- Convert tbl_cmnform_0404b.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404b ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0404b SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0404b DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0404b RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0404b ADD CONSTRAINT tbl_cmnform_0404b_Answer1_check CHECK (Answer1 IN ('Y', 'N'));

-- Converting tbl_cmnform_0404b.Answer2
-- Convert tbl_cmnform_0404b.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404b ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_0404b SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_0404b DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_0404b RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_0404b ADD CONSTRAINT tbl_cmnform_0404b_Answer2_check CHECK (Answer2 IN ('Y', 'N'));

-- Converting tbl_cmnform_0404b.Answer3
-- Convert tbl_cmnform_0404b.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404b ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0404b SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0404b DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0404b RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0404b ADD CONSTRAINT tbl_cmnform_0404b_Answer3_check CHECK (Answer3 IN ('Y', 'N'));

-- Converting tbl_cmnform_0404b.Answer4
-- Convert tbl_cmnform_0404b.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404b ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0404b SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0404b DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0404b RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0404b ADD CONSTRAINT tbl_cmnform_0404b_Answer4_check CHECK (Answer4 IN ('Y', 'N'));

-- Converting tbl_cmnform_0404b.Answer5
-- Convert tbl_cmnform_0404b.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404b ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0404b SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0404b DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0404b RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0404b ADD CONSTRAINT tbl_cmnform_0404b_Answer5_check CHECK (Answer5 IN ('Y', 'N'));

-- Converting tbl_cmnform_0404c.Answer6
-- Convert tbl_cmnform_0404c.Answer6 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer6_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer6_new = Answer6::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer6;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer6_new TO Answer6;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer6_check CHECK (Answer6 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404c.Answer7a
-- Convert tbl_cmnform_0404c.Answer7a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer7a_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer7a_new = Answer7a::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer7a;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer7a_new TO Answer7a;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer7a_check CHECK (Answer7a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404c.Answer8
-- Convert tbl_cmnform_0404c.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer8_check CHECK (Answer8 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404c.Answer9a
-- Convert tbl_cmnform_0404c.Answer9a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer9a_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer9a_new = Answer9a::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer9a;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer9a_new TO Answer9a;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer9a_check CHECK (Answer9a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404c.Answer10a
-- Convert tbl_cmnform_0404c.Answer10a from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer10a_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer10a_new = Answer10a::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer10a;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer10a_new TO Answer10a;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer10a_check CHECK (Answer10a IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404c.Answer11
-- Convert tbl_cmnform_0404c.Answer11 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer11_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer11_new = Answer11::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer11;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer11_new TO Answer11;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer11_check CHECK (Answer11 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0404c.Answer12
-- Convert tbl_cmnform_0404c.Answer12 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0404c ADD COLUMN Answer12_new VARCHAR(1);
UPDATE tbl_cmnform_0404c SET Answer12_new = Answer12::text;
ALTER TABLE tbl_cmnform_0404c DROP COLUMN Answer12;
ALTER TABLE tbl_cmnform_0404c RENAME COLUMN Answer12_new TO Answer12;
ALTER TABLE tbl_cmnform_0404c ADD CONSTRAINT tbl_cmnform_0404c_Answer12_check CHECK (Answer12 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0602b.Answer1
-- Convert tbl_cmnform_0602b.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0602b.Answer3
-- Convert tbl_cmnform_0602b.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0602b.Answer5
-- Convert tbl_cmnform_0602b.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer5_check CHECK (Answer5 IN ('1', '2', '3', '4', '5'));

-- Converting tbl_cmnform_0602b.Answer6
-- Convert tbl_cmnform_0602b.Answer6 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer6_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer6_new = Answer6::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer6;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer6_new TO Answer6;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer6_check CHECK (Answer6 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0602b.Answer7
-- Convert tbl_cmnform_0602b.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer7_check CHECK (Answer7 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0602b.Answer10
-- Convert tbl_cmnform_0602b.Answer10 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer10_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer10_new = Answer10::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer10;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer10_new TO Answer10;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer10_check CHECK (Answer10 IN ('1', '2', '3'));

-- Converting tbl_cmnform_0602b.Answer11
-- Convert tbl_cmnform_0602b.Answer11 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer11_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer11_new = Answer11::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer11;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer11_new TO Answer11;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer11_check CHECK (Answer11 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0602b.Answer12
-- Convert tbl_cmnform_0602b.Answer12 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0602b ADD COLUMN Answer12_new VARCHAR(1);
UPDATE tbl_cmnform_0602b SET Answer12_new = Answer12::text;
ALTER TABLE tbl_cmnform_0602b DROP COLUMN Answer12;
ALTER TABLE tbl_cmnform_0602b RENAME COLUMN Answer12_new TO Answer12;
ALTER TABLE tbl_cmnform_0602b ADD CONSTRAINT tbl_cmnform_0602b_Answer12_check CHECK (Answer12 IN ('2', '4'));

-- Converting tbl_cmnform_0603b.Answer1
-- Convert tbl_cmnform_0603b.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0603b ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0603b SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0603b DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0603b RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0603b ADD CONSTRAINT tbl_cmnform_0603b_Answer1_check CHECK (Answer1 IN ('Y', 'N'));

-- Converting tbl_cmnform_0603b.Answer3
-- Convert tbl_cmnform_0603b.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0603b ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0603b SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0603b DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0603b RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0603b ADD CONSTRAINT tbl_cmnform_0603b_Answer3_check CHECK (Answer3 IN ('1', '2', '3', '4', '5'));

-- Converting tbl_cmnform_0603b.Answer4
-- Convert tbl_cmnform_0603b.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0603b ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0603b SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0603b DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0603b RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0603b ADD CONSTRAINT tbl_cmnform_0603b_Answer4_check CHECK (Answer4 IN ('Y', 'N'));

-- Converting tbl_cmnform_0603b.Answer5
-- Convert tbl_cmnform_0603b.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0603b ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0603b SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0603b DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0603b RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0603b ADD CONSTRAINT tbl_cmnform_0603b_Answer5_check CHECK (Answer5 IN ('Y', 'N'));

-- Converting tbl_cmnform_0702a.Answer1
-- Convert tbl_cmnform_0702a.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702a ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0702a SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0702a DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0702a RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0702a ADD CONSTRAINT tbl_cmnform_0702a_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702a.Answer2
-- Convert tbl_cmnform_0702a.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702a ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_0702a SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_0702a DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_0702a RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_0702a ADD CONSTRAINT tbl_cmnform_0702a_Answer2_check CHECK (Answer2 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702a.Answer3
-- Convert tbl_cmnform_0702a.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702a ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0702a SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0702a DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0702a RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0702a ADD CONSTRAINT tbl_cmnform_0702a_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702a.Answer4
-- Convert tbl_cmnform_0702a.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702a ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0702a SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0702a DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0702a RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0702a ADD CONSTRAINT tbl_cmnform_0702a_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702a.Answer5
-- Convert tbl_cmnform_0702a.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702a ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0702a SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0702a DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0702a RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0702a ADD CONSTRAINT tbl_cmnform_0702a_Answer5_check CHECK (Answer5 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702b.Answer6
-- Convert tbl_cmnform_0702b.Answer6 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702b ADD COLUMN Answer6_new VARCHAR(1);
UPDATE tbl_cmnform_0702b SET Answer6_new = Answer6::text;
ALTER TABLE tbl_cmnform_0702b DROP COLUMN Answer6;
ALTER TABLE tbl_cmnform_0702b RENAME COLUMN Answer6_new TO Answer6;
ALTER TABLE tbl_cmnform_0702b ADD CONSTRAINT tbl_cmnform_0702b_Answer6_check CHECK (Answer6 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702b.Answer7
-- Convert tbl_cmnform_0702b.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702b ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_0702b SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_0702b DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_0702b RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_0702b ADD CONSTRAINT tbl_cmnform_0702b_Answer7_check CHECK (Answer7 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702b.Answer8
-- Convert tbl_cmnform_0702b.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702b ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_0702b SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_0702b DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_0702b RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_0702b ADD CONSTRAINT tbl_cmnform_0702b_Answer8_check CHECK (Answer8 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702b.Answer12
-- Convert tbl_cmnform_0702b.Answer12 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702b ADD COLUMN Answer12_new VARCHAR(1);
UPDATE tbl_cmnform_0702b SET Answer12_new = Answer12::text;
ALTER TABLE tbl_cmnform_0702b DROP COLUMN Answer12;
ALTER TABLE tbl_cmnform_0702b RENAME COLUMN Answer12_new TO Answer12;
ALTER TABLE tbl_cmnform_0702b ADD CONSTRAINT tbl_cmnform_0702b_Answer12_check CHECK (Answer12 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702b.Answer13
-- Convert tbl_cmnform_0702b.Answer13 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702b ADD COLUMN Answer13_new VARCHAR(1);
UPDATE tbl_cmnform_0702b SET Answer13_new = Answer13::text;
ALTER TABLE tbl_cmnform_0702b DROP COLUMN Answer13;
ALTER TABLE tbl_cmnform_0702b RENAME COLUMN Answer13_new TO Answer13;
ALTER TABLE tbl_cmnform_0702b ADD CONSTRAINT tbl_cmnform_0702b_Answer13_check CHECK (Answer13 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0702b.Answer14
-- Convert tbl_cmnform_0702b.Answer14 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0702b ADD COLUMN Answer14_new VARCHAR(1);
UPDATE tbl_cmnform_0702b SET Answer14_new = Answer14::text;
ALTER TABLE tbl_cmnform_0702b DROP COLUMN Answer14;
ALTER TABLE tbl_cmnform_0702b RENAME COLUMN Answer14_new TO Answer14;
ALTER TABLE tbl_cmnform_0702b ADD CONSTRAINT tbl_cmnform_0702b_Answer14_check CHECK (Answer14 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0703a.Answer1
-- Convert tbl_cmnform_0703a.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0703a ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0703a SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0703a DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0703a RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0703a ADD CONSTRAINT tbl_cmnform_0703a_Answer1_check CHECK (Answer1 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0703a.Answer2
-- Convert tbl_cmnform_0703a.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0703a ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_0703a SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_0703a DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_0703a RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_0703a ADD CONSTRAINT tbl_cmnform_0703a_Answer2_check CHECK (Answer2 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0703a.Answer3
-- Convert tbl_cmnform_0703a.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0703a ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0703a SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0703a DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0703a RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0703a ADD CONSTRAINT tbl_cmnform_0703a_Answer3_check CHECK (Answer3 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0703a.Answer4
-- Convert tbl_cmnform_0703a.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0703a ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0703a SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0703a DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0703a RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0703a ADD CONSTRAINT tbl_cmnform_0703a_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0703a.Answer5
-- Convert tbl_cmnform_0703a.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0703a ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0703a SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0703a DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0703a RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0703a ADD CONSTRAINT tbl_cmnform_0703a_Answer5_check CHECK (Answer5 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0802.Answer4
-- Convert tbl_cmnform_0802.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0802 ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0802 SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0802 DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0802 RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0802 ADD CONSTRAINT tbl_cmnform_0802_Answer4_check CHECK (Answer4 IN ('Y', 'N'));

-- Converting tbl_cmnform_0802.Answer5_1
-- Convert tbl_cmnform_0802.Answer5_1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0802 ADD COLUMN Answer5_1_new VARCHAR(1);
UPDATE tbl_cmnform_0802 SET Answer5_1_new = Answer5_1::text;
ALTER TABLE tbl_cmnform_0802 DROP COLUMN Answer5_1;
ALTER TABLE tbl_cmnform_0802 RENAME COLUMN Answer5_1_new TO Answer5_1;
ALTER TABLE tbl_cmnform_0802 ADD CONSTRAINT tbl_cmnform_0802_Answer5_1_check CHECK (Answer5_1 IN ('1', '2', '3', '4', '5'));

-- Converting tbl_cmnform_0802.Answer5_2
-- Convert tbl_cmnform_0802.Answer5_2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0802 ADD COLUMN Answer5_2_new VARCHAR(1);
UPDATE tbl_cmnform_0802 SET Answer5_2_new = Answer5_2::text;
ALTER TABLE tbl_cmnform_0802 DROP COLUMN Answer5_2;
ALTER TABLE tbl_cmnform_0802 RENAME COLUMN Answer5_2_new TO Answer5_2;
ALTER TABLE tbl_cmnform_0802 ADD CONSTRAINT tbl_cmnform_0802_Answer5_2_check CHECK (Answer5_2 IN ('1', '2', '3', '4', '5'));

-- Converting tbl_cmnform_0802.Answer5_3
-- Convert tbl_cmnform_0802.Answer5_3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0802 ADD COLUMN Answer5_3_new VARCHAR(1);
UPDATE tbl_cmnform_0802 SET Answer5_3_new = Answer5_3::text;
ALTER TABLE tbl_cmnform_0802 DROP COLUMN Answer5_3;
ALTER TABLE tbl_cmnform_0802 RENAME COLUMN Answer5_3_new TO Answer5_3;
ALTER TABLE tbl_cmnform_0802 ADD CONSTRAINT tbl_cmnform_0802_Answer5_3_check CHECK (Answer5_3 IN ('1', '2', '3', '4', '5'));

-- Converting tbl_cmnform_0802.Answer12
-- Convert tbl_cmnform_0802.Answer12 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0802 ADD COLUMN Answer12_new VARCHAR(1);
UPDATE tbl_cmnform_0802 SET Answer12_new = Answer12::text;
ALTER TABLE tbl_cmnform_0802 DROP COLUMN Answer12;
ALTER TABLE tbl_cmnform_0802 RENAME COLUMN Answer12_new TO Answer12;
ALTER TABLE tbl_cmnform_0802 ADD CONSTRAINT tbl_cmnform_0802_Answer12_check CHECK (Answer12 IN ('Y', 'N'));

-- Converting tbl_cmnform_0902.Answer1
-- Convert tbl_cmnform_0902.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0902 ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_0902 SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_0902 DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_0902 RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_0902 ADD CONSTRAINT tbl_cmnform_0902_Answer1_check CHECK (Answer1 IN ('1', '3', '4'));

-- Converting tbl_cmnform_0902.Answer4
-- Convert tbl_cmnform_0902.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0902 ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0902 SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0902 DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0902 RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0902 ADD CONSTRAINT tbl_cmnform_0902_Answer4_check CHECK (Answer4 IN ('1', '3', '4'));

-- Converting tbl_cmnform_0902.Answer5
-- Convert tbl_cmnform_0902.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0902 ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_0902 SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_0902 DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_0902 RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_0902 ADD CONSTRAINT tbl_cmnform_0902_Answer5_check CHECK (Answer5 IN ('1', '2', '3'));

-- Converting tbl_cmnform_0902.Answer7
-- Convert tbl_cmnform_0902.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0902 ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_0902 SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_0902 DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_0902 RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_0902 ADD CONSTRAINT tbl_cmnform_0902_Answer7_check CHECK (Answer7 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_0903.Answer3
-- Convert tbl_cmnform_0903.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0903 ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_0903 SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_0903 DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_0903 RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_0903 ADD CONSTRAINT tbl_cmnform_0903_Answer3_check CHECK (Answer3 IN ('1', '2', '3', '4'));

-- Converting tbl_cmnform_0903.Answer4
-- Convert tbl_cmnform_0903.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_0903 ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_0903 SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_0903 DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_0903 RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_0903 ADD CONSTRAINT tbl_cmnform_0903_Answer4_check CHECK (Answer4 IN ('1', '2'));

-- Converting tbl_cmnform_1002a.Answer1
-- Convert tbl_cmnform_1002a.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1002a ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_1002a SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_1002a DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_1002a RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_1002a ADD CONSTRAINT tbl_cmnform_1002a_Answer1_check CHECK (Answer1 IN ('Y', 'N'));

-- Converting tbl_cmnform_1002a.Answer5
-- Convert tbl_cmnform_1002a.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1002a ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_1002a SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_1002a DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_1002a RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_1002a ADD CONSTRAINT tbl_cmnform_1002a_Answer5_check CHECK (Answer5 IN ('1', '3', '7'));

-- Converting tbl_cmnform_1002b.Answer7
-- Convert tbl_cmnform_1002b.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1002b ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_1002b SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_1002b DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_1002b RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_1002b ADD CONSTRAINT tbl_cmnform_1002b_Answer7_check CHECK (Answer7 IN ('Y', 'N'));

-- Converting tbl_cmnform_1002b.Answer8
-- Convert tbl_cmnform_1002b.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1002b ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_1002b SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_1002b DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_1002b RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_1002b ADD CONSTRAINT tbl_cmnform_1002b_Answer8_check CHECK (Answer8 IN ('Y', 'N'));

-- Converting tbl_cmnform_1002b.Answer13
-- Convert tbl_cmnform_1002b.Answer13 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1002b ADD COLUMN Answer13_new VARCHAR(1);
UPDATE tbl_cmnform_1002b SET Answer13_new = Answer13::text;
ALTER TABLE tbl_cmnform_1002b DROP COLUMN Answer13;
ALTER TABLE tbl_cmnform_1002b RENAME COLUMN Answer13_new TO Answer13;
ALTER TABLE tbl_cmnform_1002b ADD CONSTRAINT tbl_cmnform_1002b_Answer13_check CHECK (Answer13 IN ('1', '2', '3', '4'));

-- Converting tbl_cmnform_1002b.Answer14
-- Convert tbl_cmnform_1002b.Answer14 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1002b ADD COLUMN Answer14_new VARCHAR(1);
UPDATE tbl_cmnform_1002b SET Answer14_new = Answer14::text;
ALTER TABLE tbl_cmnform_1002b DROP COLUMN Answer14;
ALTER TABLE tbl_cmnform_1002b RENAME COLUMN Answer14_new TO Answer14;
ALTER TABLE tbl_cmnform_1002b ADD CONSTRAINT tbl_cmnform_1002b_Answer14_check CHECK (Answer14 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_1003.Answer1
-- Convert tbl_cmnform_1003.Answer1 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1003 ADD COLUMN Answer1_new VARCHAR(1);
UPDATE tbl_cmnform_1003 SET Answer1_new = Answer1::text;
ALTER TABLE tbl_cmnform_1003 DROP COLUMN Answer1;
ALTER TABLE tbl_cmnform_1003 RENAME COLUMN Answer1_new TO Answer1;
ALTER TABLE tbl_cmnform_1003 ADD CONSTRAINT tbl_cmnform_1003_Answer1_check CHECK (Answer1 IN ('Y', 'N'));

-- Converting tbl_cmnform_1003.Answer2
-- Convert tbl_cmnform_1003.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1003 ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_1003 SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_1003 DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_1003 RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_1003 ADD CONSTRAINT tbl_cmnform_1003_Answer2_check CHECK (Answer2 IN ('Y', 'N'));

-- Converting tbl_cmnform_1003.Answer5
-- Convert tbl_cmnform_1003.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1003 ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_1003 SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_1003 DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_1003 RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_1003 ADD CONSTRAINT tbl_cmnform_1003_Answer5_check CHECK (Answer5 IN ('1', '2', '3', '4'));

-- Converting tbl_cmnform_1003.Answer7
-- Convert tbl_cmnform_1003.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1003 ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_1003 SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_1003 DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_1003 RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_1003 ADD CONSTRAINT tbl_cmnform_1003_Answer7_check CHECK (Answer7 IN ('Y', 'N'));

-- Converting tbl_cmnform_1003.Answer9
-- Convert tbl_cmnform_1003.Answer9 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_1003 ADD COLUMN Answer9_new VARCHAR(1);
UPDATE tbl_cmnform_1003 SET Answer9_new = Answer9::text;
ALTER TABLE tbl_cmnform_1003 DROP COLUMN Answer9;
ALTER TABLE tbl_cmnform_1003 RENAME COLUMN Answer9_new TO Answer9;
ALTER TABLE tbl_cmnform_1003 ADD CONSTRAINT tbl_cmnform_1003_Answer9_check CHECK (Answer9 IN ('1', '2', '3'));

-- Converting tbl_cmnform_48403.Answer2
-- Convert tbl_cmnform_48403.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_48403 ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_48403 SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_48403 DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_48403 RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_48403 ADD CONSTRAINT tbl_cmnform_48403_Answer2_check CHECK (Answer2 IN ('1', '2', '3'));

-- Converting tbl_cmnform_48403.Answer3
-- Convert tbl_cmnform_48403.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_48403 ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_48403 SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_48403 DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_48403 RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_48403 ADD CONSTRAINT tbl_cmnform_48403_Answer3_check CHECK (Answer3 IN ('1', '2', '3'));

-- Converting tbl_cmnform_48403.Answer4
-- Convert tbl_cmnform_48403.Answer4 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_48403 ADD COLUMN Answer4_new VARCHAR(1);
UPDATE tbl_cmnform_48403 SET Answer4_new = Answer4::text;
ALTER TABLE tbl_cmnform_48403 DROP COLUMN Answer4;
ALTER TABLE tbl_cmnform_48403 RENAME COLUMN Answer4_new TO Answer4;
ALTER TABLE tbl_cmnform_48403 ADD CONSTRAINT tbl_cmnform_48403_Answer4_check CHECK (Answer4 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_48403.Answer7
-- Convert tbl_cmnform_48403.Answer7 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_48403 ADD COLUMN Answer7_new VARCHAR(1);
UPDATE tbl_cmnform_48403 SET Answer7_new = Answer7::text;
ALTER TABLE tbl_cmnform_48403 DROP COLUMN Answer7;
ALTER TABLE tbl_cmnform_48403 RENAME COLUMN Answer7_new TO Answer7;
ALTER TABLE tbl_cmnform_48403 ADD CONSTRAINT tbl_cmnform_48403_Answer7_check CHECK (Answer7 IN ('Y', 'N'));

-- Converting tbl_cmnform_48403.Answer8
-- Convert tbl_cmnform_48403.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_48403 ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_48403 SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_48403 DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_48403 RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_48403 ADD CONSTRAINT tbl_cmnform_48403_Answer8_check CHECK (Answer8 IN ('Y', 'N'));

-- Converting tbl_cmnform_48403.Answer9
-- Convert tbl_cmnform_48403.Answer9 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_48403 ADD COLUMN Answer9_new VARCHAR(1);
UPDATE tbl_cmnform_48403 SET Answer9_new = Answer9::text;
ALTER TABLE tbl_cmnform_48403 DROP COLUMN Answer9;
ALTER TABLE tbl_cmnform_48403 RENAME COLUMN Answer9_new TO Answer9;
ALTER TABLE tbl_cmnform_48403 ADD CONSTRAINT tbl_cmnform_48403_Answer9_check CHECK (Answer9 IN ('Y', 'N'));

-- Converting tbl_cmnform_4842.Answer2
-- Convert tbl_cmnform_4842.Answer2 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_4842 ADD COLUMN Answer2_new VARCHAR(1);
UPDATE tbl_cmnform_4842 SET Answer2_new = Answer2::text;
ALTER TABLE tbl_cmnform_4842 DROP COLUMN Answer2;
ALTER TABLE tbl_cmnform_4842 RENAME COLUMN Answer2_new TO Answer2;
ALTER TABLE tbl_cmnform_4842 ADD CONSTRAINT tbl_cmnform_4842_Answer2_check CHECK (Answer2 IN ('Y', 'N'));

-- Converting tbl_cmnform_4842.Answer3
-- Convert tbl_cmnform_4842.Answer3 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_4842 ADD COLUMN Answer3_new VARCHAR(1);
UPDATE tbl_cmnform_4842 SET Answer3_new = Answer3::text;
ALTER TABLE tbl_cmnform_4842 DROP COLUMN Answer3;
ALTER TABLE tbl_cmnform_4842 RENAME COLUMN Answer3_new TO Answer3;
ALTER TABLE tbl_cmnform_4842 ADD CONSTRAINT tbl_cmnform_4842_Answer3_check CHECK (Answer3 IN ('1', '2', '3'));

-- Converting tbl_cmnform_4842.Answer5
-- Convert tbl_cmnform_4842.Answer5 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_4842 ADD COLUMN Answer5_new VARCHAR(1);
UPDATE tbl_cmnform_4842 SET Answer5_new = Answer5::text;
ALTER TABLE tbl_cmnform_4842 DROP COLUMN Answer5;
ALTER TABLE tbl_cmnform_4842 RENAME COLUMN Answer5_new TO Answer5;
ALTER TABLE tbl_cmnform_4842 ADD CONSTRAINT tbl_cmnform_4842_Answer5_check CHECK (Answer5 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_4842.Answer8
-- Convert tbl_cmnform_4842.Answer8 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_4842 ADD COLUMN Answer8_new VARCHAR(1);
UPDATE tbl_cmnform_4842 SET Answer8_new = Answer8::text;
ALTER TABLE tbl_cmnform_4842 DROP COLUMN Answer8;
ALTER TABLE tbl_cmnform_4842 RENAME COLUMN Answer8_new TO Answer8;
ALTER TABLE tbl_cmnform_4842 ADD CONSTRAINT tbl_cmnform_4842_Answer8_check CHECK (Answer8 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_4842.Answer9
-- Convert tbl_cmnform_4842.Answer9 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_4842 ADD COLUMN Answer9_new VARCHAR(1);
UPDATE tbl_cmnform_4842 SET Answer9_new = Answer9::text;
ALTER TABLE tbl_cmnform_4842 DROP COLUMN Answer9;
ALTER TABLE tbl_cmnform_4842 RENAME COLUMN Answer9_new TO Answer9;
ALTER TABLE tbl_cmnform_4842 ADD CONSTRAINT tbl_cmnform_4842_Answer9_check CHECK (Answer9 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_4842.Answer10
-- Convert tbl_cmnform_4842.Answer10 from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_4842 ADD COLUMN Answer10_new VARCHAR(1);
UPDATE tbl_cmnform_4842 SET Answer10_new = Answer10::text;
ALTER TABLE tbl_cmnform_4842 DROP COLUMN Answer10;
ALTER TABLE tbl_cmnform_4842 RENAME COLUMN Answer10_new TO Answer10;
ALTER TABLE tbl_cmnform_4842 ADD CONSTRAINT tbl_cmnform_4842_Answer10_check CHECK (Answer10 IN ('Y', 'N', 'D'));

-- Converting tbl_cmnform_details.Period
-- Convert tbl_cmnform_details.Period from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_cmnform_details ADD COLUMN Period_new VARCHAR(13);
UPDATE tbl_cmnform_details SET Period_new = Period::text;
ALTER TABLE tbl_cmnform_details DROP COLUMN Period;
ALTER TABLE tbl_cmnform_details RENAME COLUMN Period_new TO Period;
ALTER TABLE tbl_cmnform_details ADD CONSTRAINT tbl_cmnform_details_Period_check CHECK (Period IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Quarterly', 'Semi-Annually', 'Annually', 'Custom'));

-- Converting tbl_company.TaxIDType
-- Convert tbl_company.TaxIDType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_company ADD COLUMN TaxIDType_new VARCHAR(3);
UPDATE tbl_company SET TaxIDType_new = TaxIDType::text;
ALTER TABLE tbl_company DROP COLUMN TaxIDType;
ALTER TABLE tbl_company RENAME COLUMN TaxIDType_new TO TaxIDType;
ALTER TABLE tbl_company ADD CONSTRAINT tbl_company_TaxIDType_check CHECK (TaxIDType IN ('SSN', 'EIN'));

-- Converting tbl_customer.Courtesy
-- Convert tbl_customer.Courtesy from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN Courtesy_new VARCHAR(4);
UPDATE tbl_customer SET Courtesy_new = Courtesy::text;
ALTER TABLE tbl_customer DROP COLUMN Courtesy;
ALTER TABLE tbl_customer RENAME COLUMN Courtesy_new TO Courtesy;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_Courtesy_check CHECK (Courtesy IN ('Dr.', 'Miss', 'Mr.', 'Mrs.', 'Rev.'));

-- Converting tbl_customer.EmploymentStatus
-- Convert tbl_customer.EmploymentStatus from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN EmploymentStatus_new VARCHAR(10);
UPDATE tbl_customer SET EmploymentStatus_new = EmploymentStatus::text;
ALTER TABLE tbl_customer DROP COLUMN EmploymentStatus;
ALTER TABLE tbl_customer RENAME COLUMN EmploymentStatus_new TO EmploymentStatus;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_EmploymentStatus_check CHECK (EmploymentStatus IN ('Unknown', 'Full Time', 'Part Time', 'Retired', 'Student', 'Unemployed'));

-- Converting tbl_customer.Gender
-- Convert tbl_customer.Gender from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN Gender_new VARCHAR(6);
UPDATE tbl_customer SET Gender_new = Gender::text;
ALTER TABLE tbl_customer DROP COLUMN Gender;
ALTER TABLE tbl_customer RENAME COLUMN Gender_new TO Gender;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_Gender_check CHECK (Gender IN ('Male', 'Female'));

-- Converting tbl_customer.MaritalStatus
-- Convert tbl_customer.MaritalStatus from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN MaritalStatus_new VARCHAR(16);
UPDATE tbl_customer SET MaritalStatus_new = MaritalStatus::text;
ALTER TABLE tbl_customer DROP COLUMN MaritalStatus;
ALTER TABLE tbl_customer RENAME COLUMN MaritalStatus_new TO MaritalStatus;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_MaritalStatus_check CHECK (MaritalStatus IN ('Unknown', 'Single', 'Married', 'Legaly Separated', 'Divorced', 'Widowed'));

-- Converting tbl_customer.MilitaryBranch
-- Convert tbl_customer.MilitaryBranch from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN MilitaryBranch_new VARCHAR(14);
UPDATE tbl_customer SET MilitaryBranch_new = MilitaryBranch::text;
ALTER TABLE tbl_customer DROP COLUMN MilitaryBranch;
ALTER TABLE tbl_customer RENAME COLUMN MilitaryBranch_new TO MilitaryBranch;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_MilitaryBranch_check CHECK (MilitaryBranch IN ('N/A', 'Army', 'Air Force', 'Navy', 'Marines', 'Coast Guard', 'National Guard'));

-- Converting tbl_customer.MilitaryStatus
-- Convert tbl_customer.MilitaryStatus from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN MilitaryStatus_new VARCHAR(7);
UPDATE tbl_customer SET MilitaryStatus_new = MilitaryStatus::text;
ALTER TABLE tbl_customer DROP COLUMN MilitaryStatus;
ALTER TABLE tbl_customer RENAME COLUMN MilitaryStatus_new TO MilitaryStatus;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_MilitaryStatus_check CHECK (MilitaryStatus IN ('N/A', 'Active', 'Reserve', 'Retired'));

-- Converting tbl_customer.StudentStatus
-- Convert tbl_customer.StudentStatus from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN StudentStatus_new VARCHAR(9);
UPDATE tbl_customer SET StudentStatus_new = StudentStatus::text;
ALTER TABLE tbl_customer DROP COLUMN StudentStatus;
ALTER TABLE tbl_customer RENAME COLUMN StudentStatus_new TO StudentStatus;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_StudentStatus_check CHECK (StudentStatus IN ('N/A', 'Full Time', 'Part Time'));

-- Converting tbl_customer.Basis
-- Convert tbl_customer.Basis from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN Basis_new VARCHAR(7);
UPDATE tbl_customer SET Basis_new = Basis::text;
ALTER TABLE tbl_customer DROP COLUMN Basis;
ALTER TABLE tbl_customer RENAME COLUMN Basis_new TO Basis;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_Basis_check CHECK (Basis IN ('Bill', 'Allowed'));

-- Converting tbl_customer.Frequency
-- Convert tbl_customer.Frequency from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN Frequency_new VARCHAR(9);
UPDATE tbl_customer SET Frequency_new = Frequency::text;
ALTER TABLE tbl_customer DROP COLUMN Frequency;
ALTER TABLE tbl_customer RENAME COLUMN Frequency_new TO Frequency;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_Frequency_check CHECK (Frequency IN ('Per Visit', 'Monthly', 'Yearly'));

-- Converting tbl_customer.AccidentType
-- Convert tbl_customer.AccidentType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN AccidentType_new VARCHAR(5);
UPDATE tbl_customer SET AccidentType_new = AccidentType::text;
ALTER TABLE tbl_customer DROP COLUMN AccidentType;
ALTER TABLE tbl_customer RENAME COLUMN AccidentType_new TO AccidentType;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_AccidentType_check CHECK (AccidentType IN ('Auto', 'No', 'Other'));

-- Converting tbl_customer_insurance.Basis
-- Convert tbl_customer_insurance.Basis from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer_insurance ADD COLUMN Basis_new VARCHAR(7);
UPDATE tbl_customer_insurance SET Basis_new = Basis::text;
ALTER TABLE tbl_customer_insurance DROP COLUMN Basis;
ALTER TABLE tbl_customer_insurance RENAME COLUMN Basis_new TO Basis;
ALTER TABLE tbl_customer_insurance ADD CONSTRAINT tbl_customer_insurance_Basis_check CHECK (Basis IN ('Bill', 'Allowed'));

-- Converting tbl_customer_insurance.Gender
-- Convert tbl_customer_insurance.Gender from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_customer_insurance ADD COLUMN Gender_new VARCHAR(6);
UPDATE tbl_customer_insurance SET Gender_new = Gender::text;
ALTER TABLE tbl_customer_insurance DROP COLUMN Gender;
ALTER TABLE tbl_customer_insurance RENAME COLUMN Gender_new TO Gender;
ALTER TABLE tbl_customer_insurance ADD CONSTRAINT tbl_customer_insurance_Gender_check CHECK (Gender IN ('Male', 'Female'));

-- Converting tbl_deposits.PaymentMethod
-- Convert tbl_deposits.PaymentMethod from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_deposits ADD COLUMN PaymentMethod_new VARCHAR(11);
UPDATE tbl_deposits SET PaymentMethod_new = PaymentMethod::text;
ALTER TABLE tbl_deposits DROP COLUMN PaymentMethod;
ALTER TABLE tbl_deposits RENAME COLUMN PaymentMethod_new TO PaymentMethod;
ALTER TABLE tbl_deposits ADD CONSTRAINT tbl_deposits_PaymentMethod_check CHECK (PaymentMethod IN ('Cash', 'Check', 'Credit Card'));

-- Converting tbl_doctor.Courtesy
-- Convert tbl_doctor.Courtesy from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_doctor ADD COLUMN Courtesy_new VARCHAR(4);
UPDATE tbl_doctor SET Courtesy_new = Courtesy::text;
ALTER TABLE tbl_doctor DROP COLUMN Courtesy;
ALTER TABLE tbl_doctor RENAME COLUMN Courtesy_new TO Courtesy;
ALTER TABLE tbl_doctor ADD CONSTRAINT tbl_doctor_Courtesy_check CHECK (Courtesy IN ('Dr.', 'Miss', 'Mr.', 'Mrs.', 'Rev.'));

-- Converting tbl_eligibilityrequest.Region
-- Convert tbl_eligibilityrequest.Region from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_eligibilityrequest ADD COLUMN Region_new VARCHAR(11);
UPDATE tbl_eligibilityrequest SET Region_new = Region::text;
ALTER TABLE tbl_eligibilityrequest DROP COLUMN Region;
ALTER TABLE tbl_eligibilityrequest RENAME COLUMN Region_new TO Region;
ALTER TABLE tbl_eligibilityrequest ADD CONSTRAINT tbl_eligibilityrequest_Region_check CHECK (Region IN ('Region A', 'Region B', 'Region C', 'Region D', 'Zirmed', 'Medi-Cal', 'Availity', 'Office Ally', 'Ability'));

-- Converting tbl_facility.DefaultDeliveryWeek
-- Convert tbl_facility.DefaultDeliveryWeek from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_facility ADD COLUMN DefaultDeliveryWeek_new VARCHAR(17);
UPDATE tbl_facility SET DefaultDeliveryWeek_new = DefaultDeliveryWeek::text;
ALTER TABLE tbl_facility DROP COLUMN DefaultDeliveryWeek;
ALTER TABLE tbl_facility RENAME COLUMN DefaultDeliveryWeek_new TO DefaultDeliveryWeek;
ALTER TABLE tbl_facility ADD CONSTRAINT tbl_facility_DefaultDeliveryWeek_check CHECK (DefaultDeliveryWeek IN ('1st week of month', '2nd week of month', '3rd week of month', '4th week of month', 'as needed'));

-- Converting tbl_insurancecompany.Basis
-- Convert tbl_insurancecompany.Basis from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_insurancecompany ADD COLUMN Basis_new VARCHAR(7);
UPDATE tbl_insurancecompany SET Basis_new = Basis::text;
ALTER TABLE tbl_insurancecompany DROP COLUMN Basis;
ALTER TABLE tbl_insurancecompany RENAME COLUMN Basis_new TO Basis;
ALTER TABLE tbl_insurancecompany ADD CONSTRAINT tbl_insurancecompany_Basis_check CHECK (Basis IN ('Bill', 'Allowed'));

-- Converting tbl_insurancecompany.ECSFormat
-- Convert tbl_insurancecompany.ECSFormat from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_insurancecompany ADD COLUMN ECSFormat_new VARCHAR(11);
UPDATE tbl_insurancecompany SET ECSFormat_new = ECSFormat::text;
ALTER TABLE tbl_insurancecompany DROP COLUMN ECSFormat;
ALTER TABLE tbl_insurancecompany RENAME COLUMN ECSFormat_new TO ECSFormat;
ALTER TABLE tbl_insurancecompany ADD CONSTRAINT tbl_insurancecompany_ECSFormat_check CHECK (ECSFormat IN ('Region A', 'Region B', 'Region C', 'Region D', 'Zirmed', 'Medi-Cal', 'Availity', 'Office Ally', 'Ability'));

-- Converting tbl_inventoryitem.Basis
-- Convert tbl_inventoryitem.Basis from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_inventoryitem ADD COLUMN Basis_new VARCHAR(7);
UPDATE tbl_inventoryitem SET Basis_new = Basis::text;
ALTER TABLE tbl_inventoryitem DROP COLUMN Basis;
ALTER TABLE tbl_inventoryitem RENAME COLUMN Basis_new TO Basis;
ALTER TABLE tbl_inventoryitem ADD CONSTRAINT tbl_inventoryitem_Basis_check CHECK (Basis IN ('Bill', 'Allowed'));

-- Converting tbl_inventoryitem.CommissionPaidAt
-- Convert tbl_inventoryitem.CommissionPaidAt from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_inventoryitem ADD COLUMN CommissionPaidAt_new VARCHAR(7);
UPDATE tbl_inventoryitem SET CommissionPaidAt_new = CommissionPaidAt::text;
ALTER TABLE tbl_inventoryitem DROP COLUMN CommissionPaidAt;
ALTER TABLE tbl_inventoryitem RENAME COLUMN CommissionPaidAt_new TO CommissionPaidAt;
ALTER TABLE tbl_inventoryitem ADD CONSTRAINT tbl_inventoryitem_CommissionPaidAt_check CHECK (CommissionPaidAt IN ('Billing', 'Payment', 'Never'));

-- Converting tbl_inventoryitem.Frequency
-- Convert tbl_inventoryitem.Frequency from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_inventoryitem ADD COLUMN Frequency_new VARCHAR(8);
UPDATE tbl_inventoryitem SET Frequency_new = Frequency::text;
ALTER TABLE tbl_inventoryitem DROP COLUMN Frequency;
ALTER TABLE tbl_inventoryitem RENAME COLUMN Frequency_new TO Frequency;
ALTER TABLE tbl_inventoryitem ADD CONSTRAINT tbl_inventoryitem_Frequency_check CHECK (Frequency IN ('One time', 'Monthly', 'Weekly', 'Never'));

-- Converting tbl_invoice.SubmittedTo
-- Convert tbl_invoice.SubmittedTo from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_invoice ADD COLUMN SubmittedTo_new VARCHAR(7);
UPDATE tbl_invoice SET SubmittedTo_new = SubmittedTo::text;
ALTER TABLE tbl_invoice DROP COLUMN SubmittedTo;
ALTER TABLE tbl_invoice RENAME COLUMN SubmittedTo_new TO SubmittedTo;
ALTER TABLE tbl_invoice ADD CONSTRAINT tbl_invoice_SubmittedTo_check CHECK (SubmittedTo IN ('Ins1', 'Ins2', 'Ins3', 'Ins4', 'Patient'));

-- Converting tbl_invoicedetails.CurrentPayer
-- Convert tbl_invoicedetails.CurrentPayer from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_invoicedetails ADD COLUMN CurrentPayer_new VARCHAR(7);
UPDATE tbl_invoicedetails SET CurrentPayer_new = CurrentPayer::text;
ALTER TABLE tbl_invoicedetails DROP COLUMN CurrentPayer;
ALTER TABLE tbl_invoicedetails RENAME COLUMN CurrentPayer_new TO CurrentPayer;
ALTER TABLE tbl_invoicedetails ADD CONSTRAINT tbl_invoicedetails_CurrentPayer_check CHECK (CurrentPayer IN ('Ins1', 'Ins2', 'Ins3', 'Ins4', 'Patient', 'None'));

-- Converting tbl_legalrep.Courtesy
-- Convert tbl_legalrep.Courtesy from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_legalrep ADD COLUMN Courtesy_new VARCHAR(4);
UPDATE tbl_legalrep SET Courtesy_new = Courtesy::text;
ALTER TABLE tbl_legalrep DROP COLUMN Courtesy;
ALTER TABLE tbl_legalrep RENAME COLUMN Courtesy_new TO Courtesy;
ALTER TABLE tbl_legalrep ADD CONSTRAINT tbl_legalrep_Courtesy_check CHECK (Courtesy IN ('Dr.', 'Miss', 'Mr.', 'Mrs.', 'Rev.'));

-- Converting tbl_location.TaxIDType
-- Convert tbl_location.TaxIDType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_location ADD COLUMN TaxIDType_new VARCHAR(3);
UPDATE tbl_location SET TaxIDType_new = TaxIDType::text;
ALTER TABLE tbl_location DROP COLUMN TaxIDType;
ALTER TABLE tbl_location RENAME COLUMN TaxIDType_new TO TaxIDType;
ALTER TABLE tbl_location ADD CONSTRAINT tbl_location_TaxIDType_check CHECK (TaxIDType IN ('SSN', 'EIN'));

-- Converting tbl_order.SaleType
-- Convert tbl_order.SaleType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_order ADD COLUMN SaleType_new VARCHAR(11);
UPDATE tbl_order SET SaleType_new = SaleType::text;
ALTER TABLE tbl_order DROP COLUMN SaleType;
ALTER TABLE tbl_order RENAME COLUMN SaleType_new TO SaleType;
ALTER TABLE tbl_order ADD CONSTRAINT tbl_order_SaleType_check CHECK (SaleType IN ('Retail', 'Back Office'));

-- Converting tbl_order.State
-- Convert tbl_order.State from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_order ADD COLUMN State_new VARCHAR(8);
UPDATE tbl_order SET State_new = State::text;
ALTER TABLE tbl_order DROP COLUMN State;
ALTER TABLE tbl_order RENAME COLUMN State_new TO State;
ALTER TABLE tbl_order ADD CONSTRAINT tbl_order_State_check CHECK (State IN ('New', 'Approved', 'Closed', 'Canceled'));

-- Converting tbl_orderdetails.SaleRentType
-- Convert tbl_orderdetails.SaleRentType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN SaleRentType_new VARCHAR(22);
UPDATE tbl_orderdetails SET SaleRentType_new = SaleRentType::text;
ALTER TABLE tbl_orderdetails DROP COLUMN SaleRentType;
ALTER TABLE tbl_orderdetails RENAME COLUMN SaleRentType_new TO SaleRentType;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_SaleRentType_check CHECK (SaleRentType IN ('Medicare Oxygen Rental', 'One Time Rental', 'Monthly Rental', 'Capped Rental', 'Parental Capped Rental', 'Rent to Purchase', 'One Time Sale', 'Re-occurring Sale'));

-- Converting tbl_orderdetails.OrderedWhen
-- Convert tbl_orderdetails.OrderedWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN OrderedWhen_new VARCHAR(13);
UPDATE tbl_orderdetails SET OrderedWhen_new = OrderedWhen::text;
ALTER TABLE tbl_orderdetails DROP COLUMN OrderedWhen;
ALTER TABLE tbl_orderdetails RENAME COLUMN OrderedWhen_new TO OrderedWhen;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_OrderedWhen_check CHECK (OrderedWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Quarterly', 'Semi-Annually', 'Annually'));

-- Converting tbl_orderdetails.BilledWhen
-- Convert tbl_orderdetails.BilledWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN BilledWhen_new VARCHAR(16);
UPDATE tbl_orderdetails SET BilledWhen_new = BilledWhen::text;
ALTER TABLE tbl_orderdetails DROP COLUMN BilledWhen;
ALTER TABLE tbl_orderdetails RENAME COLUMN BilledWhen_new TO BilledWhen;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_BilledWhen_check CHECK (BilledWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Calendar Monthly', 'Quarterly', 'Semi-Annually', 'Annually', 'Custom'));

-- Converting tbl_orderdetails.BillItemOn
-- Convert tbl_orderdetails.BillItemOn from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN BillItemOn_new VARCHAR(22);
UPDATE tbl_orderdetails SET BillItemOn_new = BillItemOn::text;
ALTER TABLE tbl_orderdetails DROP COLUMN BillItemOn;
ALTER TABLE tbl_orderdetails RENAME COLUMN BillItemOn_new TO BillItemOn;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_BillItemOn_check CHECK (BillItemOn IN ('Day of Delivery', 'Last day of the Month', 'Last day of the Period', 'Day of Pick-up'));

-- Converting tbl_orderdetails.State
-- Convert tbl_orderdetails.State from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN State_new VARCHAR(8);
UPDATE tbl_orderdetails SET State_new = State::text;
ALTER TABLE tbl_orderdetails DROP COLUMN State;
ALTER TABLE tbl_orderdetails RENAME COLUMN State_new TO State;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_State_check CHECK (State IN ('New', 'Approved', 'Pickup', 'Closed', 'Canceled'));

-- Converting tbl_paymentplan.Period
-- Convert tbl_paymentplan.Period from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_paymentplan ADD COLUMN Period_new VARCHAR(9);
UPDATE tbl_paymentplan SET Period_new = Period::text;
ALTER TABLE tbl_paymentplan DROP COLUMN Period;
ALTER TABLE tbl_paymentplan RENAME COLUMN Period_new TO Period;
ALTER TABLE tbl_paymentplan ADD CONSTRAINT tbl_paymentplan_Period_check CHECK (Period IN ('Weekly', 'Bi-weekly', 'Monthly'));

-- Converting tbl_predefinedtext.Type
-- Convert tbl_predefinedtext.Type from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_predefinedtext ADD COLUMN Type_new VARCHAR(18);
UPDATE tbl_predefinedtext SET Type_new = Type::text;
ALTER TABLE tbl_predefinedtext DROP COLUMN Type;
ALTER TABLE tbl_predefinedtext RENAME COLUMN Type_new TO Type;
ALTER TABLE tbl_predefinedtext ADD CONSTRAINT tbl_predefinedtext_Type_check CHECK (Type IN ('Document Text', 'Account Statements', 'Compliance Notes', 'Customer Notes', 'Invoice Notes', 'HAO'));

-- Converting tbl_pricecode_item.OrderedWhen
-- Convert tbl_pricecode_item.OrderedWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_pricecode_item ADD COLUMN OrderedWhen_new VARCHAR(13);
UPDATE tbl_pricecode_item SET OrderedWhen_new = OrderedWhen::text;
ALTER TABLE tbl_pricecode_item DROP COLUMN OrderedWhen;
ALTER TABLE tbl_pricecode_item RENAME COLUMN OrderedWhen_new TO OrderedWhen;
ALTER TABLE tbl_pricecode_item ADD CONSTRAINT tbl_pricecode_item_OrderedWhen_check CHECK (OrderedWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Quarterly', 'Semi-Annually', 'Annually'));

-- Converting tbl_pricecode_item.BilledWhen
-- Convert tbl_pricecode_item.BilledWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_pricecode_item ADD COLUMN BilledWhen_new VARCHAR(16);
UPDATE tbl_pricecode_item SET BilledWhen_new = BilledWhen::text;
ALTER TABLE tbl_pricecode_item DROP COLUMN BilledWhen;
ALTER TABLE tbl_pricecode_item RENAME COLUMN BilledWhen_new TO BilledWhen;
ALTER TABLE tbl_pricecode_item ADD CONSTRAINT tbl_pricecode_item_BilledWhen_check CHECK (BilledWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Calendar Monthly', 'Quarterly', 'Semi-Annually', 'Annually', 'Custom'));

-- Converting tbl_pricecode_item.BillItemOn
-- Convert tbl_pricecode_item.BillItemOn from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_pricecode_item ADD COLUMN BillItemOn_new VARCHAR(22);
UPDATE tbl_pricecode_item SET BillItemOn_new = BillItemOn::text;
ALTER TABLE tbl_pricecode_item DROP COLUMN BillItemOn;
ALTER TABLE tbl_pricecode_item RENAME COLUMN BillItemOn_new TO BillItemOn;
ALTER TABLE tbl_pricecode_item ADD CONSTRAINT tbl_pricecode_item_BillItemOn_check CHECK (BillItemOn IN ('Day of Delivery', 'Last day of the Month', 'Last day of the Period', 'Day of Pick-up'));

-- Converting tbl_pricecode_item.DefaultCMNType
-- Convert tbl_pricecode_item.DefaultCMNType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_pricecode_item ADD COLUMN DefaultCMNType_new VARCHAR(13);
UPDATE tbl_pricecode_item SET DefaultCMNType_new = DefaultCMNType::text;
ALTER TABLE tbl_pricecode_item DROP COLUMN DefaultCMNType;
ALTER TABLE tbl_pricecode_item RENAME COLUMN DefaultCMNType_new TO DefaultCMNType;
ALTER TABLE tbl_pricecode_item ADD CONSTRAINT tbl_pricecode_item_DefaultCMNType_check CHECK (DefaultCMNType IN ('DMERC 02.03A', 'DMERC 02.03B', 'DMERC 03.02', 'DMERC 07.02B', 'DMERC 08.02', 'DMERC DRORDER', 'DMERC URO', 'DME 04.04B', 'DME 04.04C', 'DME 06.03B', 'DME 07.03A', 'DME 09.03', 'DME 10.03', 'DME 484.03'));

-- Converting tbl_pricecode_item.DefaultOrderType
-- Convert tbl_pricecode_item.DefaultOrderType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_pricecode_item ADD COLUMN DefaultOrderType_new VARCHAR(6);
UPDATE tbl_pricecode_item SET DefaultOrderType_new = DefaultOrderType::text;
ALTER TABLE tbl_pricecode_item DROP COLUMN DefaultOrderType;
ALTER TABLE tbl_pricecode_item RENAME COLUMN DefaultOrderType_new TO DefaultOrderType;
ALTER TABLE tbl_pricecode_item ADD CONSTRAINT tbl_pricecode_item_DefaultOrderType_check CHECK (DefaultOrderType IN ('Sale', 'Rental'));

-- Converting tbl_pricecode_item.RentalType
-- Convert tbl_pricecode_item.RentalType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_pricecode_item ADD COLUMN RentalType_new VARCHAR(22);
UPDATE tbl_pricecode_item SET RentalType_new = RentalType::text;
ALTER TABLE tbl_pricecode_item DROP COLUMN RentalType;
ALTER TABLE tbl_pricecode_item RENAME COLUMN RentalType_new TO RentalType;
ALTER TABLE tbl_pricecode_item ADD CONSTRAINT tbl_pricecode_item_RentalType_check CHECK (RentalType IN ('Medicare Oxygen Rental', 'One Time Rental', 'Monthly Rental', 'Capped Rental', 'Parental Capped Rental', 'Rent to Purchase'));

-- Converting tbl_purchaseorder.ShipVia
-- Convert tbl_purchaseorder.ShipVia from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_purchaseorder ADD COLUMN ShipVia_new VARCHAR(8);
UPDATE tbl_purchaseorder SET ShipVia_new = ShipVia::text;
ALTER TABLE tbl_purchaseorder DROP COLUMN ShipVia;
ALTER TABLE tbl_purchaseorder RENAME COLUMN ShipVia_new TO ShipVia;
ALTER TABLE tbl_purchaseorder ADD CONSTRAINT tbl_purchaseorder_ShipVia_check CHECK (ShipVia IN ('BEST WAY', 'UPS/RPS'));

-- Converting tbl_referral.Courtesy
-- Convert tbl_referral.Courtesy from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_referral ADD COLUMN Courtesy_new VARCHAR(4);
UPDATE tbl_referral SET Courtesy_new = Courtesy::text;
ALTER TABLE tbl_referral DROP COLUMN Courtesy;
ALTER TABLE tbl_referral RENAME COLUMN Courtesy_new TO Courtesy;
ALTER TABLE tbl_referral ADD CONSTRAINT tbl_referral_Courtesy_check CHECK (Courtesy IN ('Dr.', 'Miss', 'Mr.', 'Mrs.', 'Rev.'));

-- Converting tbl_salesrep.Courtesy
-- Convert tbl_salesrep.Courtesy from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_salesrep ADD COLUMN Courtesy_new VARCHAR(4);
UPDATE tbl_salesrep SET Courtesy_new = Courtesy::text;
ALTER TABLE tbl_salesrep DROP COLUMN Courtesy;
ALTER TABLE tbl_salesrep RENAME COLUMN Courtesy_new TO Courtesy;
ALTER TABLE tbl_salesrep ADD CONSTRAINT tbl_salesrep_Courtesy_check CHECK (Courtesy IN ('Dr.', 'Miss', 'Mr.', 'Mrs.', 'Rev.'));

-- Converting tbl_serial.Status
-- Convert tbl_serial.Status from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_serial ADD COLUMN Status_new VARCHAR(15);
UPDATE tbl_serial SET Status_new = Status::text;
ALTER TABLE tbl_serial DROP COLUMN Status;
ALTER TABLE tbl_serial RENAME COLUMN Status_new TO Status;
ALTER TABLE tbl_serial ADD CONSTRAINT tbl_serial_Status_check CHECK (Status IN ('Empty', 'Filled', 'Junked', 'Lost', 'Reserved', 'On Hand', 'Rented', 'Sold', 'Sent', 'Maintenance', 'Transferred Out'));

-- Converting tbl_serial.OwnRent
-- Convert tbl_serial.OwnRent from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_serial ADD COLUMN OwnRent_new VARCHAR(4);
UPDATE tbl_serial SET OwnRent_new = OwnRent::text;
ALTER TABLE tbl_serial DROP COLUMN OwnRent;
ALTER TABLE tbl_serial RENAME COLUMN OwnRent_new TO OwnRent;
ALTER TABLE tbl_serial ADD CONSTRAINT tbl_serial_OwnRent_check CHECK (OwnRent IN ('Own', 'Rent'));

-- Converting tbl_submitter.ECSFormat
-- Convert tbl_submitter.ECSFormat from ENUM to VARCHAR with CHECK constraint
ALTER TABLE tbl_submitter ADD COLUMN ECSFormat_new VARCHAR(8);
UPDATE tbl_submitter SET ECSFormat_new = ECSFormat::text;
ALTER TABLE tbl_submitter DROP COLUMN ECSFormat;
ALTER TABLE tbl_submitter RENAME COLUMN ECSFormat_new TO ECSFormat;
ALTER TABLE tbl_submitter ADD CONSTRAINT tbl_submitter_ECSFormat_check CHECK (ECSFormat IN ('Region A', 'Region B', 'Region C', 'Region D'));

-- Converting view_invoicetransaction_statistics.CurrentPayer
-- Convert view_invoicetransaction_statistics.CurrentPayer from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_invoicetransaction_statistics ADD COLUMN CurrentPayer_new VARCHAR(7);
UPDATE view_invoicetransaction_statistics SET CurrentPayer_new = CurrentPayer::text;
ALTER TABLE view_invoicetransaction_statistics DROP COLUMN CurrentPayer;
ALTER TABLE view_invoicetransaction_statistics RENAME COLUMN CurrentPayer_new TO CurrentPayer;
ALTER TABLE view_invoicetransaction_statistics ADD CONSTRAINT view_invoicetransaction_statistics_CurrentPayer_check CHECK (CurrentPayer IN ('Ins1', 'Ins2', 'Ins3', 'Ins4', 'Patient', 'None'));

-- Converting view_mir.SaleRentType
-- Convert view_mir.SaleRentType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_mir ADD COLUMN SaleRentType_new VARCHAR(22);
UPDATE view_mir SET SaleRentType_new = SaleRentType::text;
ALTER TABLE view_mir DROP COLUMN SaleRentType;
ALTER TABLE view_mir RENAME COLUMN SaleRentType_new TO SaleRentType;
ALTER TABLE view_mir ADD CONSTRAINT view_mir_SaleRentType_check CHECK (SaleRentType IN ('Medicare Oxygen Rental', 'One Time Rental', 'Monthly Rental', 'Capped Rental', 'Parental Capped Rental', 'Rent to Purchase', 'One Time Sale', 'Re-occurring Sale'));

-- Converting view_orderdetails.SaleRentType
-- Convert view_orderdetails.SaleRentType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN SaleRentType_new VARCHAR(22);
UPDATE view_orderdetails SET SaleRentType_new = SaleRentType::text;
ALTER TABLE view_orderdetails DROP COLUMN SaleRentType;
ALTER TABLE view_orderdetails RENAME COLUMN SaleRentType_new TO SaleRentType;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_SaleRentType_check CHECK (SaleRentType IN ('Medicare Oxygen Rental', 'One Time Rental', 'Monthly Rental', 'Capped Rental', 'Parental Capped Rental', 'Rent to Purchase', 'One Time Sale', 'Re-occurring Sale'));

-- Converting view_orderdetails.OrderedWhen
-- Convert view_orderdetails.OrderedWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN OrderedWhen_new VARCHAR(13);
UPDATE view_orderdetails SET OrderedWhen_new = OrderedWhen::text;
ALTER TABLE view_orderdetails DROP COLUMN OrderedWhen;
ALTER TABLE view_orderdetails RENAME COLUMN OrderedWhen_new TO OrderedWhen;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_OrderedWhen_check CHECK (OrderedWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Quarterly', 'Semi-Annually', 'Annually'));

-- Converting view_orderdetails.BilledWhen
-- Convert view_orderdetails.BilledWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN BilledWhen_new VARCHAR(16);
UPDATE view_orderdetails SET BilledWhen_new = BilledWhen::text;
ALTER TABLE view_orderdetails DROP COLUMN BilledWhen;
ALTER TABLE view_orderdetails RENAME COLUMN BilledWhen_new TO BilledWhen;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_BilledWhen_check CHECK (BilledWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Calendar Monthly', 'Quarterly', 'Semi-Annually', 'Annually', 'Custom'));

-- Converting view_orderdetails.BillItemOn
-- Convert view_orderdetails.BillItemOn from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN BillItemOn_new VARCHAR(22);
UPDATE view_orderdetails SET BillItemOn_new = BillItemOn::text;
ALTER TABLE view_orderdetails DROP COLUMN BillItemOn;
ALTER TABLE view_orderdetails RENAME COLUMN BillItemOn_new TO BillItemOn;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_BillItemOn_check CHECK (BillItemOn IN ('Day of Delivery', 'Last day of the Month', 'Last day of the Period', 'Day of Pick-up'));

-- Converting view_orderdetails.State
-- Convert view_orderdetails.State from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN State_new VARCHAR(8);
UPDATE view_orderdetails SET State_new = State::text;
ALTER TABLE view_orderdetails DROP COLUMN State;
ALTER TABLE view_orderdetails RENAME COLUMN State_new TO State;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_State_check CHECK (State IN ('New', 'Approved', 'Pickup', 'Closed', 'Canceled'));

-- Converting view_orderdetails_core.SaleRentType
-- Convert view_orderdetails_core.SaleRentType from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN SaleRentType_new VARCHAR(22);
UPDATE view_orderdetails_core SET SaleRentType_new = SaleRentType::text;
ALTER TABLE view_orderdetails_core DROP COLUMN SaleRentType;
ALTER TABLE view_orderdetails_core RENAME COLUMN SaleRentType_new TO SaleRentType;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_SaleRentType_check CHECK (SaleRentType IN ('Medicare Oxygen Rental', 'One Time Rental', 'Monthly Rental', 'Capped Rental', 'Parental Capped Rental', 'Rent to Purchase', 'One Time Sale', 'Re-occurring Sale'));

-- Converting view_orderdetails_core.OrderedWhen
-- Convert view_orderdetails_core.OrderedWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN OrderedWhen_new VARCHAR(13);
UPDATE view_orderdetails_core SET OrderedWhen_new = OrderedWhen::text;
ALTER TABLE view_orderdetails_core DROP COLUMN OrderedWhen;
ALTER TABLE view_orderdetails_core RENAME COLUMN OrderedWhen_new TO OrderedWhen;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_OrderedWhen_check CHECK (OrderedWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Quarterly', 'Semi-Annually', 'Annually'));

-- Converting view_orderdetails_core.BilledWhen
-- Convert view_orderdetails_core.BilledWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN BilledWhen_new VARCHAR(16);
UPDATE view_orderdetails_core SET BilledWhen_new = BilledWhen::text;
ALTER TABLE view_orderdetails_core DROP COLUMN BilledWhen;
ALTER TABLE view_orderdetails_core RENAME COLUMN BilledWhen_new TO BilledWhen;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_BilledWhen_check CHECK (BilledWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Calendar Monthly', 'Quarterly', 'Semi-Annually', 'Annually', 'Custom'));

-- Converting view_orderdetails_core.BillItemOn
-- Convert view_orderdetails_core.BillItemOn from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN BillItemOn_new VARCHAR(22);
UPDATE view_orderdetails_core SET BillItemOn_new = BillItemOn::text;
ALTER TABLE view_orderdetails_core DROP COLUMN BillItemOn;
ALTER TABLE view_orderdetails_core RENAME COLUMN BillItemOn_new TO BillItemOn;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_BillItemOn_check CHECK (BillItemOn IN ('Day of Delivery', 'Last day of the Month', 'Last day of the Period', 'Day of Pick-up'));

-- Converting view_orderdetails_core.State
-- Convert view_orderdetails_core.State from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN State_new VARCHAR(8);
UPDATE view_orderdetails_core SET State_new = State::text;
ALTER TABLE view_orderdetails_core DROP COLUMN State;
ALTER TABLE view_orderdetails_core RENAME COLUMN State_new TO State;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_State_check CHECK (State IN ('New', 'Approved', 'Pickup', 'Closed', 'Canceled'));

-- Converting view_reoccuringlist.BilledWhen
-- Convert view_reoccuringlist.BilledWhen from ENUM to VARCHAR with CHECK constraint
ALTER TABLE view_reoccuringlist ADD COLUMN BilledWhen_new VARCHAR(16);
UPDATE view_reoccuringlist SET BilledWhen_new = BilledWhen::text;
ALTER TABLE view_reoccuringlist DROP COLUMN BilledWhen;
ALTER TABLE view_reoccuringlist RENAME COLUMN BilledWhen_new TO BilledWhen;
ALTER TABLE view_reoccuringlist ADD CONSTRAINT view_reoccuringlist_BilledWhen_check CHECK (BilledWhen IN ('One time', 'Daily', 'Weekly', 'Monthly', 'Calendar Monthly', 'Quarterly', 'Semi-Annually', 'Annually', 'Custom'));

-- SET Conversions

-- Converting tbl_cmnform.MIR
-- Convert tbl_cmnform.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_cmnform ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_cmnform SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_cmnform DROP COLUMN MIR;
ALTER TABLE tbl_cmnform RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_cmnform ADD CONSTRAINT tbl_cmnform_MIR_check CHECK (MIR <@ ARRAY['CMNType', 'InitialDate', 'CustomerID', 'Customer', 'ICD9_1.Required', 'ICD9_1.Unknown', 'ICD9_1.Inactive', 'ICD9_2.Unknown', 'ICD9_2.Inactive', 'ICD9_3.Unknown', 'ICD9_3.Inactive', 'ICD9_4.Unknown', 'ICD9_4.Inactive', 'DoctorID', 'Doctor', 'POSTypeID', 'EstimatedLengthOfNeed', 'Signature_Name', 'Signature_Date', 'Answers']);

-- Converting tbl_customer.MIR
-- Convert tbl_customer.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_customer ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_customer SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_customer DROP COLUMN MIR;
ALTER TABLE tbl_customer RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_customer ADD CONSTRAINT tbl_customer_MIR_check CHECK (MIR <@ ARRAY['AccountNumber', 'FirstName', 'LastName', 'Address1', 'City', 'State', 'Zip', 'EmploymentStatus', 'Gender', 'MaritalStatus', 'MilitaryBranch', 'MilitaryStatus', 'StudentStatus', 'MonthsValid', 'DateofBirth', 'SignatureOnFile', 'Doctor1_ID', 'Doctor1', 'ICD9_1']);

-- Converting tbl_customer_insurance.MIR
-- Convert tbl_customer_insurance.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_customer_insurance ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_customer_insurance SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_customer_insurance DROP COLUMN MIR;
ALTER TABLE tbl_customer_insurance RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_customer_insurance ADD CONSTRAINT tbl_customer_insurance_MIR_check CHECK (MIR <@ ARRAY['FirstName', 'LastName', 'Address1', 'City', 'State', 'Zip', 'Gender', 'DateofBirth', 'InsuranceCompanyID', 'InsuranceCompany', 'InsuranceType', 'PolicyNumber', 'RelationshipCode']);

-- Converting tbl_doctor.MIR
-- Convert tbl_doctor.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_doctor ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_doctor SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_doctor DROP COLUMN MIR;
ALTER TABLE tbl_doctor RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_doctor ADD CONSTRAINT tbl_doctor_MIR_check CHECK (MIR <@ ARRAY['FirstName', 'LastName', 'Address1', 'City', 'State', 'Zip', 'NPI', 'Phone']);

-- Converting tbl_facility.MIR
-- Convert tbl_facility.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_facility ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_facility SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_facility DROP COLUMN MIR;
ALTER TABLE tbl_facility RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_facility ADD CONSTRAINT tbl_facility_MIR_check CHECK (MIR <@ ARRAY['Name', 'Address1', 'City', 'State', 'Zip', 'POSTypeID', 'NPI']);

-- Converting tbl_insurancecompany.MIR
-- Convert tbl_insurancecompany.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_insurancecompany ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_insurancecompany SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_insurancecompany DROP COLUMN MIR;
ALTER TABLE tbl_insurancecompany RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_insurancecompany ADD CONSTRAINT tbl_insurancecompany_MIR_check CHECK (MIR <@ ARRAY['MedicareNumber']);

-- Converting tbl_order.MIR
-- Convert tbl_order.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_order ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_order SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_order DROP COLUMN MIR;
ALTER TABLE tbl_order RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_order ADD CONSTRAINT tbl_order_MIR_check CHECK (MIR <@ ARRAY['BillDate', 'CustomerID', 'DeliveryDate', 'Customer.Inactive', 'Customer.MIR', 'Policy1.Required', 'Policy1.MIR', 'Policy2.Required', 'Policy2.MIR', 'Facility.MIR', 'PosType.Required', 'ICD9.Required', 'ICD9.1.Unknown', 'ICD9.1.Inactive', 'ICD9.2.Unknown', 'ICD9.2.Inactive', 'ICD9.3.Unknown', 'ICD9.3.Inactive', 'ICD9.4.Unknown', 'ICD9.4.Inactive', 'ICD10.Required', 'ICD10.01.Unknown', 'ICD10.01.Inactive', 'ICD10.02.Unknown', 'ICD10.02.Inactive', 'ICD10.03.Unknown', 'ICD10.03.Inactive', 'ICD10.04.Unknown', 'ICD10.04.Inactive', 'ICD10.05.Unknown', 'ICD10.05.Inactive', 'ICD10.06.Unknown', 'ICD10.06.Inactive', 'ICD10.07.Unknown', 'ICD10.07.Inactive', 'ICD10.08.Unknown', 'ICD10.08.Inactive', 'ICD10.09.Unknown', 'ICD10.09.Inactive', 'ICD10.10.Unknown', 'ICD10.10.Inactive', 'ICD10.11.Unknown', 'ICD10.11.Inactive', 'ICD10.12.Unknown', 'ICD10.12.Inactive']);

-- Converting tbl_orderdetails.MIR
-- Convert tbl_orderdetails.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN MIR_new VARCHAR[];
UPDATE tbl_orderdetails SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE tbl_orderdetails DROP COLUMN MIR;
ALTER TABLE tbl_orderdetails RENAME COLUMN MIR_new TO MIR;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_MIR_check CHECK (MIR <@ ARRAY['InventoryItemID', 'PriceCodeID', 'SaleRentType', 'OrderedQuantity', 'OrderedUnits', 'OrderedWhen', 'OrderedConverter', 'BilledQuantity', 'BilledUnits', 'BilledWhen', 'BilledConverter', 'DeliveryQuantity', 'DeliveryUnits', 'DeliveryConverter', 'BillingCode', 'BillItemOn', 'DXPointer9', 'DXPointer10', 'Modifier1', 'Modifier2', 'Modifier3', 'CMNForm.Required', 'CMNForm.RecertificationDate', 'CMNForm.FormExpired', 'CMNForm.MIR', 'EndDate.Invalid', 'EndDate.Unconfirmed', 'AuthorizationNumber.Expired', 'AuthorizationNumber.Expires']);

-- Converting tbl_orderdetails.MIR.ORDER
-- Convert tbl_orderdetails.MIR.ORDER from SET to VARCHAR[] with CHECK constraint
ALTER TABLE tbl_orderdetails ADD COLUMN MIR.ORDER_new VARCHAR[];
UPDATE tbl_orderdetails SET MIR.ORDER_new = string_to_array(MIR.ORDER, ',');
ALTER TABLE tbl_orderdetails DROP COLUMN MIR.ORDER;
ALTER TABLE tbl_orderdetails RENAME COLUMN MIR.ORDER_new TO MIR.ORDER;
ALTER TABLE tbl_orderdetails ADD CONSTRAINT tbl_orderdetails_MIR.ORDER_check CHECK (MIR.ORDER <@ ARRAY['Customer.Inactive', 'Customer.MIR', 'Policy1.Required', 'Policy1.MIR', 'Policy2.Required', 'Policy2.MIR', 'Facility.MIR', 'PosType.Required', 'ICD9.Required', 'ICD9.1.Unknown', 'ICD9.1.Inactive', 'ICD9.2.Unknown', 'ICD9.2.Inactive', 'ICD9.3.Unknown', 'ICD9.3.Inactive', 'ICD9.4.Unknown', 'ICD9.4.Inactive', 'ICD10.Required', 'ICD10.01.Unknown', 'ICD10.01.Inactive', 'ICD10.02.Unknown', 'ICD10.02.Inactive', 'ICD10.03.Unknown', 'ICD10.03.Inactive', 'ICD10.04.Unknown', 'ICD10.04.Inactive', 'ICD10.05.Unknown', 'ICD10.05.Inactive', 'ICD10.06.Unknown', 'ICD10.06.Inactive', 'ICD10.07.Unknown', 'ICD10.07.Inactive', 'ICD10.08.Unknown', 'ICD10.08.Inactive', 'ICD10.09.Unknown', 'ICD10.09.Inactive', 'ICD10.10.Unknown', 'ICD10.10.Inactive', 'ICD10.11.Unknown', 'ICD10.11.Inactive', 'ICD10.12.Unknown', 'ICD10.12.Inactive']);

-- Converting view_orderdetails.MIR
-- Convert view_orderdetails.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN MIR_new VARCHAR[];
UPDATE view_orderdetails SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE view_orderdetails DROP COLUMN MIR;
ALTER TABLE view_orderdetails RENAME COLUMN MIR_new TO MIR;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_MIR_check CHECK (MIR <@ ARRAY['InventoryItemID', 'PriceCodeID', 'SaleRentType', 'OrderedQuantity', 'OrderedUnits', 'OrderedWhen', 'OrderedConverter', 'BilledQuantity', 'BilledUnits', 'BilledWhen', 'BilledConverter', 'DeliveryQuantity', 'DeliveryUnits', 'DeliveryConverter', 'BillingCode', 'BillItemOn', 'DXPointer9', 'DXPointer10', 'Modifier1', 'Modifier2', 'Modifier3', 'CMNForm.Required', 'CMNForm.RecertificationDate', 'CMNForm.FormExpired', 'CMNForm.MIR', 'EndDate.Invalid', 'EndDate.Unconfirmed', 'AuthorizationNumber.Expired', 'AuthorizationNumber.Expires']);

-- Converting view_orderdetails.MIR.ORDER
-- Convert view_orderdetails.MIR.ORDER from SET to VARCHAR[] with CHECK constraint
ALTER TABLE view_orderdetails ADD COLUMN MIR.ORDER_new VARCHAR[];
UPDATE view_orderdetails SET MIR.ORDER_new = string_to_array(MIR.ORDER, ',');
ALTER TABLE view_orderdetails DROP COLUMN MIR.ORDER;
ALTER TABLE view_orderdetails RENAME COLUMN MIR.ORDER_new TO MIR.ORDER;
ALTER TABLE view_orderdetails ADD CONSTRAINT view_orderdetails_MIR.ORDER_check CHECK (MIR.ORDER <@ ARRAY['Customer.Inactive', 'Customer.MIR', 'Policy1.Required', 'Policy1.MIR', 'Policy2.Required', 'Policy2.MIR', 'Facility.MIR', 'PosType.Required', 'ICD9.Required', 'ICD9.1.Unknown', 'ICD9.1.Inactive', 'ICD9.2.Unknown', 'ICD9.2.Inactive', 'ICD9.3.Unknown', 'ICD9.3.Inactive', 'ICD9.4.Unknown', 'ICD9.4.Inactive', 'ICD10.Required', 'ICD10.01.Unknown', 'ICD10.01.Inactive', 'ICD10.02.Unknown', 'ICD10.02.Inactive', 'ICD10.03.Unknown', 'ICD10.03.Inactive', 'ICD10.04.Unknown', 'ICD10.04.Inactive', 'ICD10.05.Unknown', 'ICD10.05.Inactive', 'ICD10.06.Unknown', 'ICD10.06.Inactive', 'ICD10.07.Unknown', 'ICD10.07.Inactive', 'ICD10.08.Unknown', 'ICD10.08.Inactive', 'ICD10.09.Unknown', 'ICD10.09.Inactive', 'ICD10.10.Unknown', 'ICD10.10.Inactive', 'ICD10.11.Unknown', 'ICD10.11.Inactive', 'ICD10.12.Unknown', 'ICD10.12.Inactive']);

-- Converting view_orderdetails_core.MIR
-- Convert view_orderdetails_core.MIR from SET to VARCHAR[] with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN MIR_new VARCHAR[];
UPDATE view_orderdetails_core SET MIR_new = string_to_array(MIR, ',');
ALTER TABLE view_orderdetails_core DROP COLUMN MIR;
ALTER TABLE view_orderdetails_core RENAME COLUMN MIR_new TO MIR;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_MIR_check CHECK (MIR <@ ARRAY['InventoryItemID', 'PriceCodeID', 'SaleRentType', 'OrderedQuantity', 'OrderedUnits', 'OrderedWhen', 'OrderedConverter', 'BilledQuantity', 'BilledUnits', 'BilledWhen', 'BilledConverter', 'DeliveryQuantity', 'DeliveryUnits', 'DeliveryConverter', 'BillingCode', 'BillItemOn', 'DXPointer9', 'DXPointer10', 'Modifier1', 'Modifier2', 'Modifier3', 'CMNForm.Required', 'CMNForm.RecertificationDate', 'CMNForm.FormExpired', 'CMNForm.MIR', 'EndDate.Invalid', 'EndDate.Unconfirmed', 'AuthorizationNumber.Expired', 'AuthorizationNumber.Expires']);

-- Converting view_orderdetails_core.MIR.ORDER
-- Convert view_orderdetails_core.MIR.ORDER from SET to VARCHAR[] with CHECK constraint
ALTER TABLE view_orderdetails_core ADD COLUMN MIR.ORDER_new VARCHAR[];
UPDATE view_orderdetails_core SET MIR.ORDER_new = string_to_array(MIR.ORDER, ',');
ALTER TABLE view_orderdetails_core DROP COLUMN MIR.ORDER;
ALTER TABLE view_orderdetails_core RENAME COLUMN MIR.ORDER_new TO MIR.ORDER;
ALTER TABLE view_orderdetails_core ADD CONSTRAINT view_orderdetails_core_MIR.ORDER_check CHECK (MIR.ORDER <@ ARRAY['Customer.Inactive', 'Customer.MIR', 'Policy1.Required', 'Policy1.MIR', 'Policy2.Required', 'Policy2.MIR', 'Facility.MIR', 'PosType.Required', 'ICD9.Required', 'ICD9.1.Unknown', 'ICD9.1.Inactive', 'ICD9.2.Unknown', 'ICD9.2.Inactive', 'ICD9.3.Unknown', 'ICD9.3.Inactive', 'ICD9.4.Unknown', 'ICD9.4.Inactive', 'ICD10.Required', 'ICD10.01.Unknown', 'ICD10.01.Inactive', 'ICD10.02.Unknown', 'ICD10.02.Inactive', 'ICD10.03.Unknown', 'ICD10.03.Inactive', 'ICD10.04.Unknown', 'ICD10.04.Inactive', 'ICD10.05.Unknown', 'ICD10.05.Inactive', 'ICD10.06.Unknown', 'ICD10.06.Inactive', 'ICD10.07.Unknown', 'ICD10.07.Inactive', 'ICD10.08.Unknown', 'ICD10.08.Inactive', 'ICD10.09.Unknown', 'ICD10.09.Inactive', 'ICD10.10.Unknown', 'ICD10.10.Inactive', 'ICD10.11.Unknown', 'ICD10.11.Inactive', 'ICD10.12.Unknown', 'ICD10.12.Inactive']);

COMMIT;
