-- Complex field rollback script
-- Generated at 20241212_135326

BEGIN;

-- ENUM Rollbacks

-- Rolling back tbl_cmnform.CMNType
-- Rollback tbl_cmnform.CMNType conversion
ALTER TABLE tbl_cmnform DROP CONSTRAINT IF EXISTS tbl_cmnform_CMNType_check;
ALTER TABLE tbl_cmnform ALTER COLUMN CMNType TYPE text;

-- Rolling back tbl_cmnform_0102a.Answer1
-- Rollback tbl_cmnform_0102a.Answer1 conversion
ALTER TABLE tbl_cmnform_0102a DROP CONSTRAINT IF EXISTS tbl_cmnform_0102a_Answer1_check;
ALTER TABLE tbl_cmnform_0102a ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0102a.Answer3
-- Rollback tbl_cmnform_0102a.Answer3 conversion
ALTER TABLE tbl_cmnform_0102a DROP CONSTRAINT IF EXISTS tbl_cmnform_0102a_Answer3_check;
ALTER TABLE tbl_cmnform_0102a ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0102a.Answer4
-- Rollback tbl_cmnform_0102a.Answer4 conversion
ALTER TABLE tbl_cmnform_0102a DROP CONSTRAINT IF EXISTS tbl_cmnform_0102a_Answer4_check;
ALTER TABLE tbl_cmnform_0102a ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0102a.Answer5
-- Rollback tbl_cmnform_0102a.Answer5 conversion
ALTER TABLE tbl_cmnform_0102a DROP CONSTRAINT IF EXISTS tbl_cmnform_0102a_Answer5_check;
ALTER TABLE tbl_cmnform_0102a ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0102a.Answer6
-- Rollback tbl_cmnform_0102a.Answer6 conversion
ALTER TABLE tbl_cmnform_0102a DROP CONSTRAINT IF EXISTS tbl_cmnform_0102a_Answer6_check;
ALTER TABLE tbl_cmnform_0102a ALTER COLUMN Answer6 TYPE text;

-- Rolling back tbl_cmnform_0102a.Answer7
-- Rollback tbl_cmnform_0102a.Answer7 conversion
ALTER TABLE tbl_cmnform_0102a DROP CONSTRAINT IF EXISTS tbl_cmnform_0102a_Answer7_check;
ALTER TABLE tbl_cmnform_0102a ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer12
-- Rollback tbl_cmnform_0102b.Answer12 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer12_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer12 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer13
-- Rollback tbl_cmnform_0102b.Answer13 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer13_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer13 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer14
-- Rollback tbl_cmnform_0102b.Answer14 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer14_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer14 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer15
-- Rollback tbl_cmnform_0102b.Answer15 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer15_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer15 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer16
-- Rollback tbl_cmnform_0102b.Answer16 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer16_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer16 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer19
-- Rollback tbl_cmnform_0102b.Answer19 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer19_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer19 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer20
-- Rollback tbl_cmnform_0102b.Answer20 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer20_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer20 TYPE text;

-- Rolling back tbl_cmnform_0102b.Answer22
-- Rollback tbl_cmnform_0102b.Answer22 conversion
ALTER TABLE tbl_cmnform_0102b DROP CONSTRAINT IF EXISTS tbl_cmnform_0102b_Answer22_check;
ALTER TABLE tbl_cmnform_0102b ALTER COLUMN Answer22 TYPE text;

-- Rolling back tbl_cmnform_0203a.Answer1
-- Rollback tbl_cmnform_0203a.Answer1 conversion
ALTER TABLE tbl_cmnform_0203a DROP CONSTRAINT IF EXISTS tbl_cmnform_0203a_Answer1_check;
ALTER TABLE tbl_cmnform_0203a ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0203a.Answer2
-- Rollback tbl_cmnform_0203a.Answer2 conversion
ALTER TABLE tbl_cmnform_0203a DROP CONSTRAINT IF EXISTS tbl_cmnform_0203a_Answer2_check;
ALTER TABLE tbl_cmnform_0203a ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_0203a.Answer3
-- Rollback tbl_cmnform_0203a.Answer3 conversion
ALTER TABLE tbl_cmnform_0203a DROP CONSTRAINT IF EXISTS tbl_cmnform_0203a_Answer3_check;
ALTER TABLE tbl_cmnform_0203a ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0203a.Answer4
-- Rollback tbl_cmnform_0203a.Answer4 conversion
ALTER TABLE tbl_cmnform_0203a DROP CONSTRAINT IF EXISTS tbl_cmnform_0203a_Answer4_check;
ALTER TABLE tbl_cmnform_0203a ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0203a.Answer6
-- Rollback tbl_cmnform_0203a.Answer6 conversion
ALTER TABLE tbl_cmnform_0203a DROP CONSTRAINT IF EXISTS tbl_cmnform_0203a_Answer6_check;
ALTER TABLE tbl_cmnform_0203a ALTER COLUMN Answer6 TYPE text;

-- Rolling back tbl_cmnform_0203a.Answer7
-- Rollback tbl_cmnform_0203a.Answer7 conversion
ALTER TABLE tbl_cmnform_0203a DROP CONSTRAINT IF EXISTS tbl_cmnform_0203a_Answer7_check;
ALTER TABLE tbl_cmnform_0203a ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_0203b.Answer1
-- Rollback tbl_cmnform_0203b.Answer1 conversion
ALTER TABLE tbl_cmnform_0203b DROP CONSTRAINT IF EXISTS tbl_cmnform_0203b_Answer1_check;
ALTER TABLE tbl_cmnform_0203b ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0203b.Answer2
-- Rollback tbl_cmnform_0203b.Answer2 conversion
ALTER TABLE tbl_cmnform_0203b DROP CONSTRAINT IF EXISTS tbl_cmnform_0203b_Answer2_check;
ALTER TABLE tbl_cmnform_0203b ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_0203b.Answer3
-- Rollback tbl_cmnform_0203b.Answer3 conversion
ALTER TABLE tbl_cmnform_0203b DROP CONSTRAINT IF EXISTS tbl_cmnform_0203b_Answer3_check;
ALTER TABLE tbl_cmnform_0203b ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0203b.Answer4
-- Rollback tbl_cmnform_0203b.Answer4 conversion
ALTER TABLE tbl_cmnform_0203b DROP CONSTRAINT IF EXISTS tbl_cmnform_0203b_Answer4_check;
ALTER TABLE tbl_cmnform_0203b ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0203b.Answer8
-- Rollback tbl_cmnform_0203b.Answer8 conversion
ALTER TABLE tbl_cmnform_0203b DROP CONSTRAINT IF EXISTS tbl_cmnform_0203b_Answer8_check;
ALTER TABLE tbl_cmnform_0203b ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_0203b.Answer9
-- Rollback tbl_cmnform_0203b.Answer9 conversion
ALTER TABLE tbl_cmnform_0203b DROP CONSTRAINT IF EXISTS tbl_cmnform_0203b_Answer9_check;
ALTER TABLE tbl_cmnform_0203b ALTER COLUMN Answer9 TYPE text;

-- Rolling back tbl_cmnform_0302.Answer14
-- Rollback tbl_cmnform_0302.Answer14 conversion
ALTER TABLE tbl_cmnform_0302 DROP CONSTRAINT IF EXISTS tbl_cmnform_0302_Answer14_check;
ALTER TABLE tbl_cmnform_0302 ALTER COLUMN Answer14 TYPE text;

-- Rolling back tbl_cmnform_0403b.Answer1
-- Rollback tbl_cmnform_0403b.Answer1 conversion
ALTER TABLE tbl_cmnform_0403b DROP CONSTRAINT IF EXISTS tbl_cmnform_0403b_Answer1_check;
ALTER TABLE tbl_cmnform_0403b ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0403b.Answer2
-- Rollback tbl_cmnform_0403b.Answer2 conversion
ALTER TABLE tbl_cmnform_0403b DROP CONSTRAINT IF EXISTS tbl_cmnform_0403b_Answer2_check;
ALTER TABLE tbl_cmnform_0403b ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_0403b.Answer3
-- Rollback tbl_cmnform_0403b.Answer3 conversion
ALTER TABLE tbl_cmnform_0403b DROP CONSTRAINT IF EXISTS tbl_cmnform_0403b_Answer3_check;
ALTER TABLE tbl_cmnform_0403b ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0403b.Answer4
-- Rollback tbl_cmnform_0403b.Answer4 conversion
ALTER TABLE tbl_cmnform_0403b DROP CONSTRAINT IF EXISTS tbl_cmnform_0403b_Answer4_check;
ALTER TABLE tbl_cmnform_0403b ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0403b.Answer5
-- Rollback tbl_cmnform_0403b.Answer5 conversion
ALTER TABLE tbl_cmnform_0403b DROP CONSTRAINT IF EXISTS tbl_cmnform_0403b_Answer5_check;
ALTER TABLE tbl_cmnform_0403b ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0403c.Answer6a
-- Rollback tbl_cmnform_0403c.Answer6a conversion
ALTER TABLE tbl_cmnform_0403c DROP CONSTRAINT IF EXISTS tbl_cmnform_0403c_Answer6a_check;
ALTER TABLE tbl_cmnform_0403c ALTER COLUMN Answer6a TYPE text;

-- Rolling back tbl_cmnform_0403c.Answer7a
-- Rollback tbl_cmnform_0403c.Answer7a conversion
ALTER TABLE tbl_cmnform_0403c DROP CONSTRAINT IF EXISTS tbl_cmnform_0403c_Answer7a_check;
ALTER TABLE tbl_cmnform_0403c ALTER COLUMN Answer7a TYPE text;

-- Rolling back tbl_cmnform_0403c.Answer8
-- Rollback tbl_cmnform_0403c.Answer8 conversion
ALTER TABLE tbl_cmnform_0403c DROP CONSTRAINT IF EXISTS tbl_cmnform_0403c_Answer8_check;
ALTER TABLE tbl_cmnform_0403c ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_0403c.Answer9a
-- Rollback tbl_cmnform_0403c.Answer9a conversion
ALTER TABLE tbl_cmnform_0403c DROP CONSTRAINT IF EXISTS tbl_cmnform_0403c_Answer9a_check;
ALTER TABLE tbl_cmnform_0403c ALTER COLUMN Answer9a TYPE text;

-- Rolling back tbl_cmnform_0403c.Answer10a
-- Rollback tbl_cmnform_0403c.Answer10a conversion
ALTER TABLE tbl_cmnform_0403c DROP CONSTRAINT IF EXISTS tbl_cmnform_0403c_Answer10a_check;
ALTER TABLE tbl_cmnform_0403c ALTER COLUMN Answer10a TYPE text;

-- Rolling back tbl_cmnform_0403c.Answer11a
-- Rollback tbl_cmnform_0403c.Answer11a conversion
ALTER TABLE tbl_cmnform_0403c DROP CONSTRAINT IF EXISTS tbl_cmnform_0403c_Answer11a_check;
ALTER TABLE tbl_cmnform_0403c ALTER COLUMN Answer11a TYPE text;

-- Rolling back tbl_cmnform_0404b.Answer1
-- Rollback tbl_cmnform_0404b.Answer1 conversion
ALTER TABLE tbl_cmnform_0404b DROP CONSTRAINT IF EXISTS tbl_cmnform_0404b_Answer1_check;
ALTER TABLE tbl_cmnform_0404b ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0404b.Answer2
-- Rollback tbl_cmnform_0404b.Answer2 conversion
ALTER TABLE tbl_cmnform_0404b DROP CONSTRAINT IF EXISTS tbl_cmnform_0404b_Answer2_check;
ALTER TABLE tbl_cmnform_0404b ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_0404b.Answer3
-- Rollback tbl_cmnform_0404b.Answer3 conversion
ALTER TABLE tbl_cmnform_0404b DROP CONSTRAINT IF EXISTS tbl_cmnform_0404b_Answer3_check;
ALTER TABLE tbl_cmnform_0404b ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0404b.Answer4
-- Rollback tbl_cmnform_0404b.Answer4 conversion
ALTER TABLE tbl_cmnform_0404b DROP CONSTRAINT IF EXISTS tbl_cmnform_0404b_Answer4_check;
ALTER TABLE tbl_cmnform_0404b ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0404b.Answer5
-- Rollback tbl_cmnform_0404b.Answer5 conversion
ALTER TABLE tbl_cmnform_0404b DROP CONSTRAINT IF EXISTS tbl_cmnform_0404b_Answer5_check;
ALTER TABLE tbl_cmnform_0404b ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer6
-- Rollback tbl_cmnform_0404c.Answer6 conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer6_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer6 TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer7a
-- Rollback tbl_cmnform_0404c.Answer7a conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer7a_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer7a TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer8
-- Rollback tbl_cmnform_0404c.Answer8 conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer8_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer9a
-- Rollback tbl_cmnform_0404c.Answer9a conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer9a_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer9a TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer10a
-- Rollback tbl_cmnform_0404c.Answer10a conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer10a_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer10a TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer11
-- Rollback tbl_cmnform_0404c.Answer11 conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer11_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer11 TYPE text;

-- Rolling back tbl_cmnform_0404c.Answer12
-- Rollback tbl_cmnform_0404c.Answer12 conversion
ALTER TABLE tbl_cmnform_0404c DROP CONSTRAINT IF EXISTS tbl_cmnform_0404c_Answer12_check;
ALTER TABLE tbl_cmnform_0404c ALTER COLUMN Answer12 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer1
-- Rollback tbl_cmnform_0602b.Answer1 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer1_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer3
-- Rollback tbl_cmnform_0602b.Answer3 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer3_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer5
-- Rollback tbl_cmnform_0602b.Answer5 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer5_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer6
-- Rollback tbl_cmnform_0602b.Answer6 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer6_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer6 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer7
-- Rollback tbl_cmnform_0602b.Answer7 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer7_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer10
-- Rollback tbl_cmnform_0602b.Answer10 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer10_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer10 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer11
-- Rollback tbl_cmnform_0602b.Answer11 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer11_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer11 TYPE text;

-- Rolling back tbl_cmnform_0602b.Answer12
-- Rollback tbl_cmnform_0602b.Answer12 conversion
ALTER TABLE tbl_cmnform_0602b DROP CONSTRAINT IF EXISTS tbl_cmnform_0602b_Answer12_check;
ALTER TABLE tbl_cmnform_0602b ALTER COLUMN Answer12 TYPE text;

-- Rolling back tbl_cmnform_0603b.Answer1
-- Rollback tbl_cmnform_0603b.Answer1 conversion
ALTER TABLE tbl_cmnform_0603b DROP CONSTRAINT IF EXISTS tbl_cmnform_0603b_Answer1_check;
ALTER TABLE tbl_cmnform_0603b ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0603b.Answer3
-- Rollback tbl_cmnform_0603b.Answer3 conversion
ALTER TABLE tbl_cmnform_0603b DROP CONSTRAINT IF EXISTS tbl_cmnform_0603b_Answer3_check;
ALTER TABLE tbl_cmnform_0603b ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0603b.Answer4
-- Rollback tbl_cmnform_0603b.Answer4 conversion
ALTER TABLE tbl_cmnform_0603b DROP CONSTRAINT IF EXISTS tbl_cmnform_0603b_Answer4_check;
ALTER TABLE tbl_cmnform_0603b ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0603b.Answer5
-- Rollback tbl_cmnform_0603b.Answer5 conversion
ALTER TABLE tbl_cmnform_0603b DROP CONSTRAINT IF EXISTS tbl_cmnform_0603b_Answer5_check;
ALTER TABLE tbl_cmnform_0603b ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0702a.Answer1
-- Rollback tbl_cmnform_0702a.Answer1 conversion
ALTER TABLE tbl_cmnform_0702a DROP CONSTRAINT IF EXISTS tbl_cmnform_0702a_Answer1_check;
ALTER TABLE tbl_cmnform_0702a ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0702a.Answer2
-- Rollback tbl_cmnform_0702a.Answer2 conversion
ALTER TABLE tbl_cmnform_0702a DROP CONSTRAINT IF EXISTS tbl_cmnform_0702a_Answer2_check;
ALTER TABLE tbl_cmnform_0702a ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_0702a.Answer3
-- Rollback tbl_cmnform_0702a.Answer3 conversion
ALTER TABLE tbl_cmnform_0702a DROP CONSTRAINT IF EXISTS tbl_cmnform_0702a_Answer3_check;
ALTER TABLE tbl_cmnform_0702a ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0702a.Answer4
-- Rollback tbl_cmnform_0702a.Answer4 conversion
ALTER TABLE tbl_cmnform_0702a DROP CONSTRAINT IF EXISTS tbl_cmnform_0702a_Answer4_check;
ALTER TABLE tbl_cmnform_0702a ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0702a.Answer5
-- Rollback tbl_cmnform_0702a.Answer5 conversion
ALTER TABLE tbl_cmnform_0702a DROP CONSTRAINT IF EXISTS tbl_cmnform_0702a_Answer5_check;
ALTER TABLE tbl_cmnform_0702a ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0702b.Answer6
-- Rollback tbl_cmnform_0702b.Answer6 conversion
ALTER TABLE tbl_cmnform_0702b DROP CONSTRAINT IF EXISTS tbl_cmnform_0702b_Answer6_check;
ALTER TABLE tbl_cmnform_0702b ALTER COLUMN Answer6 TYPE text;

-- Rolling back tbl_cmnform_0702b.Answer7
-- Rollback tbl_cmnform_0702b.Answer7 conversion
ALTER TABLE tbl_cmnform_0702b DROP CONSTRAINT IF EXISTS tbl_cmnform_0702b_Answer7_check;
ALTER TABLE tbl_cmnform_0702b ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_0702b.Answer8
-- Rollback tbl_cmnform_0702b.Answer8 conversion
ALTER TABLE tbl_cmnform_0702b DROP CONSTRAINT IF EXISTS tbl_cmnform_0702b_Answer8_check;
ALTER TABLE tbl_cmnform_0702b ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_0702b.Answer12
-- Rollback tbl_cmnform_0702b.Answer12 conversion
ALTER TABLE tbl_cmnform_0702b DROP CONSTRAINT IF EXISTS tbl_cmnform_0702b_Answer12_check;
ALTER TABLE tbl_cmnform_0702b ALTER COLUMN Answer12 TYPE text;

-- Rolling back tbl_cmnform_0702b.Answer13
-- Rollback tbl_cmnform_0702b.Answer13 conversion
ALTER TABLE tbl_cmnform_0702b DROP CONSTRAINT IF EXISTS tbl_cmnform_0702b_Answer13_check;
ALTER TABLE tbl_cmnform_0702b ALTER COLUMN Answer13 TYPE text;

-- Rolling back tbl_cmnform_0702b.Answer14
-- Rollback tbl_cmnform_0702b.Answer14 conversion
ALTER TABLE tbl_cmnform_0702b DROP CONSTRAINT IF EXISTS tbl_cmnform_0702b_Answer14_check;
ALTER TABLE tbl_cmnform_0702b ALTER COLUMN Answer14 TYPE text;

-- Rolling back tbl_cmnform_0703a.Answer1
-- Rollback tbl_cmnform_0703a.Answer1 conversion
ALTER TABLE tbl_cmnform_0703a DROP CONSTRAINT IF EXISTS tbl_cmnform_0703a_Answer1_check;
ALTER TABLE tbl_cmnform_0703a ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0703a.Answer2
-- Rollback tbl_cmnform_0703a.Answer2 conversion
ALTER TABLE tbl_cmnform_0703a DROP CONSTRAINT IF EXISTS tbl_cmnform_0703a_Answer2_check;
ALTER TABLE tbl_cmnform_0703a ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_0703a.Answer3
-- Rollback tbl_cmnform_0703a.Answer3 conversion
ALTER TABLE tbl_cmnform_0703a DROP CONSTRAINT IF EXISTS tbl_cmnform_0703a_Answer3_check;
ALTER TABLE tbl_cmnform_0703a ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0703a.Answer4
-- Rollback tbl_cmnform_0703a.Answer4 conversion
ALTER TABLE tbl_cmnform_0703a DROP CONSTRAINT IF EXISTS tbl_cmnform_0703a_Answer4_check;
ALTER TABLE tbl_cmnform_0703a ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0703a.Answer5
-- Rollback tbl_cmnform_0703a.Answer5 conversion
ALTER TABLE tbl_cmnform_0703a DROP CONSTRAINT IF EXISTS tbl_cmnform_0703a_Answer5_check;
ALTER TABLE tbl_cmnform_0703a ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0802.Answer4
-- Rollback tbl_cmnform_0802.Answer4 conversion
ALTER TABLE tbl_cmnform_0802 DROP CONSTRAINT IF EXISTS tbl_cmnform_0802_Answer4_check;
ALTER TABLE tbl_cmnform_0802 ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0802.Answer5_1
-- Rollback tbl_cmnform_0802.Answer5_1 conversion
ALTER TABLE tbl_cmnform_0802 DROP CONSTRAINT IF EXISTS tbl_cmnform_0802_Answer5_1_check;
ALTER TABLE tbl_cmnform_0802 ALTER COLUMN Answer5_1 TYPE text;

-- Rolling back tbl_cmnform_0802.Answer5_2
-- Rollback tbl_cmnform_0802.Answer5_2 conversion
ALTER TABLE tbl_cmnform_0802 DROP CONSTRAINT IF EXISTS tbl_cmnform_0802_Answer5_2_check;
ALTER TABLE tbl_cmnform_0802 ALTER COLUMN Answer5_2 TYPE text;

-- Rolling back tbl_cmnform_0802.Answer5_3
-- Rollback tbl_cmnform_0802.Answer5_3 conversion
ALTER TABLE tbl_cmnform_0802 DROP CONSTRAINT IF EXISTS tbl_cmnform_0802_Answer5_3_check;
ALTER TABLE tbl_cmnform_0802 ALTER COLUMN Answer5_3 TYPE text;

-- Rolling back tbl_cmnform_0802.Answer12
-- Rollback tbl_cmnform_0802.Answer12 conversion
ALTER TABLE tbl_cmnform_0802 DROP CONSTRAINT IF EXISTS tbl_cmnform_0802_Answer12_check;
ALTER TABLE tbl_cmnform_0802 ALTER COLUMN Answer12 TYPE text;

-- Rolling back tbl_cmnform_0902.Answer1
-- Rollback tbl_cmnform_0902.Answer1 conversion
ALTER TABLE tbl_cmnform_0902 DROP CONSTRAINT IF EXISTS tbl_cmnform_0902_Answer1_check;
ALTER TABLE tbl_cmnform_0902 ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_0902.Answer4
-- Rollback tbl_cmnform_0902.Answer4 conversion
ALTER TABLE tbl_cmnform_0902 DROP CONSTRAINT IF EXISTS tbl_cmnform_0902_Answer4_check;
ALTER TABLE tbl_cmnform_0902 ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_0902.Answer5
-- Rollback tbl_cmnform_0902.Answer5 conversion
ALTER TABLE tbl_cmnform_0902 DROP CONSTRAINT IF EXISTS tbl_cmnform_0902_Answer5_check;
ALTER TABLE tbl_cmnform_0902 ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_0902.Answer7
-- Rollback tbl_cmnform_0902.Answer7 conversion
ALTER TABLE tbl_cmnform_0902 DROP CONSTRAINT IF EXISTS tbl_cmnform_0902_Answer7_check;
ALTER TABLE tbl_cmnform_0902 ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_0903.Answer3
-- Rollback tbl_cmnform_0903.Answer3 conversion
ALTER TABLE tbl_cmnform_0903 DROP CONSTRAINT IF EXISTS tbl_cmnform_0903_Answer3_check;
ALTER TABLE tbl_cmnform_0903 ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_0903.Answer4
-- Rollback tbl_cmnform_0903.Answer4 conversion
ALTER TABLE tbl_cmnform_0903 DROP CONSTRAINT IF EXISTS tbl_cmnform_0903_Answer4_check;
ALTER TABLE tbl_cmnform_0903 ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_1002a.Answer1
-- Rollback tbl_cmnform_1002a.Answer1 conversion
ALTER TABLE tbl_cmnform_1002a DROP CONSTRAINT IF EXISTS tbl_cmnform_1002a_Answer1_check;
ALTER TABLE tbl_cmnform_1002a ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_1002a.Answer5
-- Rollback tbl_cmnform_1002a.Answer5 conversion
ALTER TABLE tbl_cmnform_1002a DROP CONSTRAINT IF EXISTS tbl_cmnform_1002a_Answer5_check;
ALTER TABLE tbl_cmnform_1002a ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_1002b.Answer7
-- Rollback tbl_cmnform_1002b.Answer7 conversion
ALTER TABLE tbl_cmnform_1002b DROP CONSTRAINT IF EXISTS tbl_cmnform_1002b_Answer7_check;
ALTER TABLE tbl_cmnform_1002b ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_1002b.Answer8
-- Rollback tbl_cmnform_1002b.Answer8 conversion
ALTER TABLE tbl_cmnform_1002b DROP CONSTRAINT IF EXISTS tbl_cmnform_1002b_Answer8_check;
ALTER TABLE tbl_cmnform_1002b ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_1002b.Answer13
-- Rollback tbl_cmnform_1002b.Answer13 conversion
ALTER TABLE tbl_cmnform_1002b DROP CONSTRAINT IF EXISTS tbl_cmnform_1002b_Answer13_check;
ALTER TABLE tbl_cmnform_1002b ALTER COLUMN Answer13 TYPE text;

-- Rolling back tbl_cmnform_1002b.Answer14
-- Rollback tbl_cmnform_1002b.Answer14 conversion
ALTER TABLE tbl_cmnform_1002b DROP CONSTRAINT IF EXISTS tbl_cmnform_1002b_Answer14_check;
ALTER TABLE tbl_cmnform_1002b ALTER COLUMN Answer14 TYPE text;

-- Rolling back tbl_cmnform_1003.Answer1
-- Rollback tbl_cmnform_1003.Answer1 conversion
ALTER TABLE tbl_cmnform_1003 DROP CONSTRAINT IF EXISTS tbl_cmnform_1003_Answer1_check;
ALTER TABLE tbl_cmnform_1003 ALTER COLUMN Answer1 TYPE text;

-- Rolling back tbl_cmnform_1003.Answer2
-- Rollback tbl_cmnform_1003.Answer2 conversion
ALTER TABLE tbl_cmnform_1003 DROP CONSTRAINT IF EXISTS tbl_cmnform_1003_Answer2_check;
ALTER TABLE tbl_cmnform_1003 ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_1003.Answer5
-- Rollback tbl_cmnform_1003.Answer5 conversion
ALTER TABLE tbl_cmnform_1003 DROP CONSTRAINT IF EXISTS tbl_cmnform_1003_Answer5_check;
ALTER TABLE tbl_cmnform_1003 ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_1003.Answer7
-- Rollback tbl_cmnform_1003.Answer7 conversion
ALTER TABLE tbl_cmnform_1003 DROP CONSTRAINT IF EXISTS tbl_cmnform_1003_Answer7_check;
ALTER TABLE tbl_cmnform_1003 ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_1003.Answer9
-- Rollback tbl_cmnform_1003.Answer9 conversion
ALTER TABLE tbl_cmnform_1003 DROP CONSTRAINT IF EXISTS tbl_cmnform_1003_Answer9_check;
ALTER TABLE tbl_cmnform_1003 ALTER COLUMN Answer9 TYPE text;

-- Rolling back tbl_cmnform_48403.Answer2
-- Rollback tbl_cmnform_48403.Answer2 conversion
ALTER TABLE tbl_cmnform_48403 DROP CONSTRAINT IF EXISTS tbl_cmnform_48403_Answer2_check;
ALTER TABLE tbl_cmnform_48403 ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_48403.Answer3
-- Rollback tbl_cmnform_48403.Answer3 conversion
ALTER TABLE tbl_cmnform_48403 DROP CONSTRAINT IF EXISTS tbl_cmnform_48403_Answer3_check;
ALTER TABLE tbl_cmnform_48403 ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_48403.Answer4
-- Rollback tbl_cmnform_48403.Answer4 conversion
ALTER TABLE tbl_cmnform_48403 DROP CONSTRAINT IF EXISTS tbl_cmnform_48403_Answer4_check;
ALTER TABLE tbl_cmnform_48403 ALTER COLUMN Answer4 TYPE text;

-- Rolling back tbl_cmnform_48403.Answer7
-- Rollback tbl_cmnform_48403.Answer7 conversion
ALTER TABLE tbl_cmnform_48403 DROP CONSTRAINT IF EXISTS tbl_cmnform_48403_Answer7_check;
ALTER TABLE tbl_cmnform_48403 ALTER COLUMN Answer7 TYPE text;

-- Rolling back tbl_cmnform_48403.Answer8
-- Rollback tbl_cmnform_48403.Answer8 conversion
ALTER TABLE tbl_cmnform_48403 DROP CONSTRAINT IF EXISTS tbl_cmnform_48403_Answer8_check;
ALTER TABLE tbl_cmnform_48403 ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_48403.Answer9
-- Rollback tbl_cmnform_48403.Answer9 conversion
ALTER TABLE tbl_cmnform_48403 DROP CONSTRAINT IF EXISTS tbl_cmnform_48403_Answer9_check;
ALTER TABLE tbl_cmnform_48403 ALTER COLUMN Answer9 TYPE text;

-- Rolling back tbl_cmnform_4842.Answer2
-- Rollback tbl_cmnform_4842.Answer2 conversion
ALTER TABLE tbl_cmnform_4842 DROP CONSTRAINT IF EXISTS tbl_cmnform_4842_Answer2_check;
ALTER TABLE tbl_cmnform_4842 ALTER COLUMN Answer2 TYPE text;

-- Rolling back tbl_cmnform_4842.Answer3
-- Rollback tbl_cmnform_4842.Answer3 conversion
ALTER TABLE tbl_cmnform_4842 DROP CONSTRAINT IF EXISTS tbl_cmnform_4842_Answer3_check;
ALTER TABLE tbl_cmnform_4842 ALTER COLUMN Answer3 TYPE text;

-- Rolling back tbl_cmnform_4842.Answer5
-- Rollback tbl_cmnform_4842.Answer5 conversion
ALTER TABLE tbl_cmnform_4842 DROP CONSTRAINT IF EXISTS tbl_cmnform_4842_Answer5_check;
ALTER TABLE tbl_cmnform_4842 ALTER COLUMN Answer5 TYPE text;

-- Rolling back tbl_cmnform_4842.Answer8
-- Rollback tbl_cmnform_4842.Answer8 conversion
ALTER TABLE tbl_cmnform_4842 DROP CONSTRAINT IF EXISTS tbl_cmnform_4842_Answer8_check;
ALTER TABLE tbl_cmnform_4842 ALTER COLUMN Answer8 TYPE text;

-- Rolling back tbl_cmnform_4842.Answer9
-- Rollback tbl_cmnform_4842.Answer9 conversion
ALTER TABLE tbl_cmnform_4842 DROP CONSTRAINT IF EXISTS tbl_cmnform_4842_Answer9_check;
ALTER TABLE tbl_cmnform_4842 ALTER COLUMN Answer9 TYPE text;

-- Rolling back tbl_cmnform_4842.Answer10
-- Rollback tbl_cmnform_4842.Answer10 conversion
ALTER TABLE tbl_cmnform_4842 DROP CONSTRAINT IF EXISTS tbl_cmnform_4842_Answer10_check;
ALTER TABLE tbl_cmnform_4842 ALTER COLUMN Answer10 TYPE text;

-- Rolling back tbl_cmnform_details.Period
-- Rollback tbl_cmnform_details.Period conversion
ALTER TABLE tbl_cmnform_details DROP CONSTRAINT IF EXISTS tbl_cmnform_details_Period_check;
ALTER TABLE tbl_cmnform_details ALTER COLUMN Period TYPE text;

-- Rolling back tbl_company.TaxIDType
-- Rollback tbl_company.TaxIDType conversion
ALTER TABLE tbl_company DROP CONSTRAINT IF EXISTS tbl_company_TaxIDType_check;
ALTER TABLE tbl_company ALTER COLUMN TaxIDType TYPE text;

-- Rolling back tbl_customer.Courtesy
-- Rollback tbl_customer.Courtesy conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_Courtesy_check;
ALTER TABLE tbl_customer ALTER COLUMN Courtesy TYPE text;

-- Rolling back tbl_customer.EmploymentStatus
-- Rollback tbl_customer.EmploymentStatus conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_EmploymentStatus_check;
ALTER TABLE tbl_customer ALTER COLUMN EmploymentStatus TYPE text;

-- Rolling back tbl_customer.Gender
-- Rollback tbl_customer.Gender conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_Gender_check;
ALTER TABLE tbl_customer ALTER COLUMN Gender TYPE text;

-- Rolling back tbl_customer.MaritalStatus
-- Rollback tbl_customer.MaritalStatus conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_MaritalStatus_check;
ALTER TABLE tbl_customer ALTER COLUMN MaritalStatus TYPE text;

-- Rolling back tbl_customer.MilitaryBranch
-- Rollback tbl_customer.MilitaryBranch conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_MilitaryBranch_check;
ALTER TABLE tbl_customer ALTER COLUMN MilitaryBranch TYPE text;

-- Rolling back tbl_customer.MilitaryStatus
-- Rollback tbl_customer.MilitaryStatus conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_MilitaryStatus_check;
ALTER TABLE tbl_customer ALTER COLUMN MilitaryStatus TYPE text;

-- Rolling back tbl_customer.StudentStatus
-- Rollback tbl_customer.StudentStatus conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_StudentStatus_check;
ALTER TABLE tbl_customer ALTER COLUMN StudentStatus TYPE text;

-- Rolling back tbl_customer.Basis
-- Rollback tbl_customer.Basis conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_Basis_check;
ALTER TABLE tbl_customer ALTER COLUMN Basis TYPE text;

-- Rolling back tbl_customer.Frequency
-- Rollback tbl_customer.Frequency conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_Frequency_check;
ALTER TABLE tbl_customer ALTER COLUMN Frequency TYPE text;

-- Rolling back tbl_customer.AccidentType
-- Rollback tbl_customer.AccidentType conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_AccidentType_check;
ALTER TABLE tbl_customer ALTER COLUMN AccidentType TYPE text;

-- Rolling back tbl_customer_insurance.Basis
-- Rollback tbl_customer_insurance.Basis conversion
ALTER TABLE tbl_customer_insurance DROP CONSTRAINT IF EXISTS tbl_customer_insurance_Basis_check;
ALTER TABLE tbl_customer_insurance ALTER COLUMN Basis TYPE text;

-- Rolling back tbl_customer_insurance.Gender
-- Rollback tbl_customer_insurance.Gender conversion
ALTER TABLE tbl_customer_insurance DROP CONSTRAINT IF EXISTS tbl_customer_insurance_Gender_check;
ALTER TABLE tbl_customer_insurance ALTER COLUMN Gender TYPE text;

-- Rolling back tbl_deposits.PaymentMethod
-- Rollback tbl_deposits.PaymentMethod conversion
ALTER TABLE tbl_deposits DROP CONSTRAINT IF EXISTS tbl_deposits_PaymentMethod_check;
ALTER TABLE tbl_deposits ALTER COLUMN PaymentMethod TYPE text;

-- Rolling back tbl_doctor.Courtesy
-- Rollback tbl_doctor.Courtesy conversion
ALTER TABLE tbl_doctor DROP CONSTRAINT IF EXISTS tbl_doctor_Courtesy_check;
ALTER TABLE tbl_doctor ALTER COLUMN Courtesy TYPE text;

-- Rolling back tbl_eligibilityrequest.Region
-- Rollback tbl_eligibilityrequest.Region conversion
ALTER TABLE tbl_eligibilityrequest DROP CONSTRAINT IF EXISTS tbl_eligibilityrequest_Region_check;
ALTER TABLE tbl_eligibilityrequest ALTER COLUMN Region TYPE text;

-- Rolling back tbl_facility.DefaultDeliveryWeek
-- Rollback tbl_facility.DefaultDeliveryWeek conversion
ALTER TABLE tbl_facility DROP CONSTRAINT IF EXISTS tbl_facility_DefaultDeliveryWeek_check;
ALTER TABLE tbl_facility ALTER COLUMN DefaultDeliveryWeek TYPE text;

-- Rolling back tbl_insurancecompany.Basis
-- Rollback tbl_insurancecompany.Basis conversion
ALTER TABLE tbl_insurancecompany DROP CONSTRAINT IF EXISTS tbl_insurancecompany_Basis_check;
ALTER TABLE tbl_insurancecompany ALTER COLUMN Basis TYPE text;

-- Rolling back tbl_insurancecompany.ECSFormat
-- Rollback tbl_insurancecompany.ECSFormat conversion
ALTER TABLE tbl_insurancecompany DROP CONSTRAINT IF EXISTS tbl_insurancecompany_ECSFormat_check;
ALTER TABLE tbl_insurancecompany ALTER COLUMN ECSFormat TYPE text;

-- Rolling back tbl_inventoryitem.Basis
-- Rollback tbl_inventoryitem.Basis conversion
ALTER TABLE tbl_inventoryitem DROP CONSTRAINT IF EXISTS tbl_inventoryitem_Basis_check;
ALTER TABLE tbl_inventoryitem ALTER COLUMN Basis TYPE text;

-- Rolling back tbl_inventoryitem.CommissionPaidAt
-- Rollback tbl_inventoryitem.CommissionPaidAt conversion
ALTER TABLE tbl_inventoryitem DROP CONSTRAINT IF EXISTS tbl_inventoryitem_CommissionPaidAt_check;
ALTER TABLE tbl_inventoryitem ALTER COLUMN CommissionPaidAt TYPE text;

-- Rolling back tbl_inventoryitem.Frequency
-- Rollback tbl_inventoryitem.Frequency conversion
ALTER TABLE tbl_inventoryitem DROP CONSTRAINT IF EXISTS tbl_inventoryitem_Frequency_check;
ALTER TABLE tbl_inventoryitem ALTER COLUMN Frequency TYPE text;

-- Rolling back tbl_invoice.SubmittedTo
-- Rollback tbl_invoice.SubmittedTo conversion
ALTER TABLE tbl_invoice DROP CONSTRAINT IF EXISTS tbl_invoice_SubmittedTo_check;
ALTER TABLE tbl_invoice ALTER COLUMN SubmittedTo TYPE text;

-- Rolling back tbl_invoicedetails.CurrentPayer
-- Rollback tbl_invoicedetails.CurrentPayer conversion
ALTER TABLE tbl_invoicedetails DROP CONSTRAINT IF EXISTS tbl_invoicedetails_CurrentPayer_check;
ALTER TABLE tbl_invoicedetails ALTER COLUMN CurrentPayer TYPE text;

-- Rolling back tbl_legalrep.Courtesy
-- Rollback tbl_legalrep.Courtesy conversion
ALTER TABLE tbl_legalrep DROP CONSTRAINT IF EXISTS tbl_legalrep_Courtesy_check;
ALTER TABLE tbl_legalrep ALTER COLUMN Courtesy TYPE text;

-- Rolling back tbl_location.TaxIDType
-- Rollback tbl_location.TaxIDType conversion
ALTER TABLE tbl_location DROP CONSTRAINT IF EXISTS tbl_location_TaxIDType_check;
ALTER TABLE tbl_location ALTER COLUMN TaxIDType TYPE text;

-- Rolling back tbl_order.SaleType
-- Rollback tbl_order.SaleType conversion
ALTER TABLE tbl_order DROP CONSTRAINT IF EXISTS tbl_order_SaleType_check;
ALTER TABLE tbl_order ALTER COLUMN SaleType TYPE text;

-- Rolling back tbl_order.State
-- Rollback tbl_order.State conversion
ALTER TABLE tbl_order DROP CONSTRAINT IF EXISTS tbl_order_State_check;
ALTER TABLE tbl_order ALTER COLUMN State TYPE text;

-- Rolling back tbl_orderdetails.SaleRentType
-- Rollback tbl_orderdetails.SaleRentType conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_SaleRentType_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN SaleRentType TYPE text;

-- Rolling back tbl_orderdetails.OrderedWhen
-- Rollback tbl_orderdetails.OrderedWhen conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_OrderedWhen_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN OrderedWhen TYPE text;

-- Rolling back tbl_orderdetails.BilledWhen
-- Rollback tbl_orderdetails.BilledWhen conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_BilledWhen_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN BilledWhen TYPE text;

-- Rolling back tbl_orderdetails.BillItemOn
-- Rollback tbl_orderdetails.BillItemOn conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_BillItemOn_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN BillItemOn TYPE text;

-- Rolling back tbl_orderdetails.State
-- Rollback tbl_orderdetails.State conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_State_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN State TYPE text;

-- Rolling back tbl_paymentplan.Period
-- Rollback tbl_paymentplan.Period conversion
ALTER TABLE tbl_paymentplan DROP CONSTRAINT IF EXISTS tbl_paymentplan_Period_check;
ALTER TABLE tbl_paymentplan ALTER COLUMN Period TYPE text;

-- Rolling back tbl_predefinedtext.Type
-- Rollback tbl_predefinedtext.Type conversion
ALTER TABLE tbl_predefinedtext DROP CONSTRAINT IF EXISTS tbl_predefinedtext_Type_check;
ALTER TABLE tbl_predefinedtext ALTER COLUMN Type TYPE text;

-- Rolling back tbl_pricecode_item.OrderedWhen
-- Rollback tbl_pricecode_item.OrderedWhen conversion
ALTER TABLE tbl_pricecode_item DROP CONSTRAINT IF EXISTS tbl_pricecode_item_OrderedWhen_check;
ALTER TABLE tbl_pricecode_item ALTER COLUMN OrderedWhen TYPE text;

-- Rolling back tbl_pricecode_item.BilledWhen
-- Rollback tbl_pricecode_item.BilledWhen conversion
ALTER TABLE tbl_pricecode_item DROP CONSTRAINT IF EXISTS tbl_pricecode_item_BilledWhen_check;
ALTER TABLE tbl_pricecode_item ALTER COLUMN BilledWhen TYPE text;

-- Rolling back tbl_pricecode_item.BillItemOn
-- Rollback tbl_pricecode_item.BillItemOn conversion
ALTER TABLE tbl_pricecode_item DROP CONSTRAINT IF EXISTS tbl_pricecode_item_BillItemOn_check;
ALTER TABLE tbl_pricecode_item ALTER COLUMN BillItemOn TYPE text;

-- Rolling back tbl_pricecode_item.DefaultCMNType
-- Rollback tbl_pricecode_item.DefaultCMNType conversion
ALTER TABLE tbl_pricecode_item DROP CONSTRAINT IF EXISTS tbl_pricecode_item_DefaultCMNType_check;
ALTER TABLE tbl_pricecode_item ALTER COLUMN DefaultCMNType TYPE text;

-- Rolling back tbl_pricecode_item.DefaultOrderType
-- Rollback tbl_pricecode_item.DefaultOrderType conversion
ALTER TABLE tbl_pricecode_item DROP CONSTRAINT IF EXISTS tbl_pricecode_item_DefaultOrderType_check;
ALTER TABLE tbl_pricecode_item ALTER COLUMN DefaultOrderType TYPE text;

-- Rolling back tbl_pricecode_item.RentalType
-- Rollback tbl_pricecode_item.RentalType conversion
ALTER TABLE tbl_pricecode_item DROP CONSTRAINT IF EXISTS tbl_pricecode_item_RentalType_check;
ALTER TABLE tbl_pricecode_item ALTER COLUMN RentalType TYPE text;

-- Rolling back tbl_purchaseorder.ShipVia
-- Rollback tbl_purchaseorder.ShipVia conversion
ALTER TABLE tbl_purchaseorder DROP CONSTRAINT IF EXISTS tbl_purchaseorder_ShipVia_check;
ALTER TABLE tbl_purchaseorder ALTER COLUMN ShipVia TYPE text;

-- Rolling back tbl_referral.Courtesy
-- Rollback tbl_referral.Courtesy conversion
ALTER TABLE tbl_referral DROP CONSTRAINT IF EXISTS tbl_referral_Courtesy_check;
ALTER TABLE tbl_referral ALTER COLUMN Courtesy TYPE text;

-- Rolling back tbl_salesrep.Courtesy
-- Rollback tbl_salesrep.Courtesy conversion
ALTER TABLE tbl_salesrep DROP CONSTRAINT IF EXISTS tbl_salesrep_Courtesy_check;
ALTER TABLE tbl_salesrep ALTER COLUMN Courtesy TYPE text;

-- Rolling back tbl_serial.Status
-- Rollback tbl_serial.Status conversion
ALTER TABLE tbl_serial DROP CONSTRAINT IF EXISTS tbl_serial_Status_check;
ALTER TABLE tbl_serial ALTER COLUMN Status TYPE text;

-- Rolling back tbl_serial.OwnRent
-- Rollback tbl_serial.OwnRent conversion
ALTER TABLE tbl_serial DROP CONSTRAINT IF EXISTS tbl_serial_OwnRent_check;
ALTER TABLE tbl_serial ALTER COLUMN OwnRent TYPE text;

-- Rolling back tbl_submitter.ECSFormat
-- Rollback tbl_submitter.ECSFormat conversion
ALTER TABLE tbl_submitter DROP CONSTRAINT IF EXISTS tbl_submitter_ECSFormat_check;
ALTER TABLE tbl_submitter ALTER COLUMN ECSFormat TYPE text;

-- Rolling back view_invoicetransaction_statistics.CurrentPayer
-- Rollback view_invoicetransaction_statistics.CurrentPayer conversion
ALTER TABLE view_invoicetransaction_statistics DROP CONSTRAINT IF EXISTS view_invoicetransaction_statistics_CurrentPayer_check;
ALTER TABLE view_invoicetransaction_statistics ALTER COLUMN CurrentPayer TYPE text;

-- Rolling back view_mir.SaleRentType
-- Rollback view_mir.SaleRentType conversion
ALTER TABLE view_mir DROP CONSTRAINT IF EXISTS view_mir_SaleRentType_check;
ALTER TABLE view_mir ALTER COLUMN SaleRentType TYPE text;

-- Rolling back view_orderdetails.SaleRentType
-- Rollback view_orderdetails.SaleRentType conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_SaleRentType_check;
ALTER TABLE view_orderdetails ALTER COLUMN SaleRentType TYPE text;

-- Rolling back view_orderdetails.OrderedWhen
-- Rollback view_orderdetails.OrderedWhen conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_OrderedWhen_check;
ALTER TABLE view_orderdetails ALTER COLUMN OrderedWhen TYPE text;

-- Rolling back view_orderdetails.BilledWhen
-- Rollback view_orderdetails.BilledWhen conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_BilledWhen_check;
ALTER TABLE view_orderdetails ALTER COLUMN BilledWhen TYPE text;

-- Rolling back view_orderdetails.BillItemOn
-- Rollback view_orderdetails.BillItemOn conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_BillItemOn_check;
ALTER TABLE view_orderdetails ALTER COLUMN BillItemOn TYPE text;

-- Rolling back view_orderdetails.State
-- Rollback view_orderdetails.State conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_State_check;
ALTER TABLE view_orderdetails ALTER COLUMN State TYPE text;

-- Rolling back view_orderdetails_core.SaleRentType
-- Rollback view_orderdetails_core.SaleRentType conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_SaleRentType_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN SaleRentType TYPE text;

-- Rolling back view_orderdetails_core.OrderedWhen
-- Rollback view_orderdetails_core.OrderedWhen conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_OrderedWhen_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN OrderedWhen TYPE text;

-- Rolling back view_orderdetails_core.BilledWhen
-- Rollback view_orderdetails_core.BilledWhen conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_BilledWhen_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN BilledWhen TYPE text;

-- Rolling back view_orderdetails_core.BillItemOn
-- Rollback view_orderdetails_core.BillItemOn conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_BillItemOn_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN BillItemOn TYPE text;

-- Rolling back view_orderdetails_core.State
-- Rollback view_orderdetails_core.State conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_State_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN State TYPE text;

-- Rolling back view_reoccuringlist.BilledWhen
-- Rollback view_reoccuringlist.BilledWhen conversion
ALTER TABLE view_reoccuringlist DROP CONSTRAINT IF EXISTS view_reoccuringlist_BilledWhen_check;
ALTER TABLE view_reoccuringlist ALTER COLUMN BilledWhen TYPE text;

-- SET Rollbacks

-- Rolling back tbl_cmnform.MIR
-- Rollback tbl_cmnform.MIR conversion
ALTER TABLE tbl_cmnform DROP CONSTRAINT IF EXISTS tbl_cmnform_MIR_check;
ALTER TABLE tbl_cmnform ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_customer.MIR
-- Rollback tbl_customer.MIR conversion
ALTER TABLE tbl_customer DROP CONSTRAINT IF EXISTS tbl_customer_MIR_check;
ALTER TABLE tbl_customer ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_customer_insurance.MIR
-- Rollback tbl_customer_insurance.MIR conversion
ALTER TABLE tbl_customer_insurance DROP CONSTRAINT IF EXISTS tbl_customer_insurance_MIR_check;
ALTER TABLE tbl_customer_insurance ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_doctor.MIR
-- Rollback tbl_doctor.MIR conversion
ALTER TABLE tbl_doctor DROP CONSTRAINT IF EXISTS tbl_doctor_MIR_check;
ALTER TABLE tbl_doctor ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_facility.MIR
-- Rollback tbl_facility.MIR conversion
ALTER TABLE tbl_facility DROP CONSTRAINT IF EXISTS tbl_facility_MIR_check;
ALTER TABLE tbl_facility ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_insurancecompany.MIR
-- Rollback tbl_insurancecompany.MIR conversion
ALTER TABLE tbl_insurancecompany DROP CONSTRAINT IF EXISTS tbl_insurancecompany_MIR_check;
ALTER TABLE tbl_insurancecompany ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_order.MIR
-- Rollback tbl_order.MIR conversion
ALTER TABLE tbl_order DROP CONSTRAINT IF EXISTS tbl_order_MIR_check;
ALTER TABLE tbl_order ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_orderdetails.MIR
-- Rollback tbl_orderdetails.MIR conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_MIR_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN MIR TYPE text;

-- Rolling back tbl_orderdetails.MIR.ORDER
-- Rollback tbl_orderdetails.MIR.ORDER conversion
ALTER TABLE tbl_orderdetails DROP CONSTRAINT IF EXISTS tbl_orderdetails_MIR.ORDER_check;
ALTER TABLE tbl_orderdetails ALTER COLUMN MIR.ORDER TYPE text;

-- Rolling back view_orderdetails.MIR
-- Rollback view_orderdetails.MIR conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_MIR_check;
ALTER TABLE view_orderdetails ALTER COLUMN MIR TYPE text;

-- Rolling back view_orderdetails.MIR.ORDER
-- Rollback view_orderdetails.MIR.ORDER conversion
ALTER TABLE view_orderdetails DROP CONSTRAINT IF EXISTS view_orderdetails_MIR.ORDER_check;
ALTER TABLE view_orderdetails ALTER COLUMN MIR.ORDER TYPE text;

-- Rolling back view_orderdetails_core.MIR
-- Rollback view_orderdetails_core.MIR conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_MIR_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN MIR TYPE text;

-- Rolling back view_orderdetails_core.MIR.ORDER
-- Rollback view_orderdetails_core.MIR.ORDER conversion
ALTER TABLE view_orderdetails_core DROP CONSTRAINT IF EXISTS view_orderdetails_core_MIR.ORDER_check;
ALTER TABLE view_orderdetails_core ALTER COLUMN MIR.ORDER TYPE text;

COMMIT;
