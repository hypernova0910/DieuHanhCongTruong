using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DieuHanhCongTruong.Properties;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Syncfusion.XlsIO;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Common;

namespace DieuHanhCongTruong
{
    public partial class FormRPBM7 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "CONSTRUCTIONDIARYGENERAL";
        public FormRPBM7()
        {
            cn = frmLoggin.sqlCon;
            InitializeComponent();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }

        private void LoadData(string name, string date1, string date2)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;

                dataGridView1.Rows.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM7] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idDuAn = dr["ID"].ToString();
                        var tenDuAn = dr["Dự án"].ToString();
                        var startTime = dr["Thời gian"].ToString();
                        var diaDiem = dr["Địa điểm"].ToString();

                        dataGridView1.Rows.Add(indexRow, tenDuAn, startTime, diaDiem);
                        dataGridView1.Rows[indexRow - 1].Tag = idDuAn;

                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return;
            }
        }

        private void FormRPBM7_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            //comboBox_TenDA.Items.Add("Chọn");
            foreach (DataRow dr in datatableProgram.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString()))
                    continue;

                TxtTImkiem.Items.Add(dr["name"].ToString());
            }
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dataGridView1.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);
            if (e.ColumnIndex == 4)
            {
                SqlCommandBuilder sqlCommand = null;


                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM7.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM7_" + Guid.NewGuid().ToString();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pathFile = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                if (pathFile != null)
                {
                    using (ExcelEngine excelEngine = new ExcelEngine())
                    {
                        IApplication application = excelEngine.Excel;
                        application.DefaultVersion = ExcelVersion.Excel97to2003;

                        //Open an existing spreadsheet, which will be used as a template for generating the new spreadsheet.
                        //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                        IWorkbook workbook;

                        //Open existing Excel template
                        //Stream cfFileStream = assembly.GetManifestResourceStream("TemplateMarker.Data.TemplateMarker.xlsx");
                        FileStream cfFileStream = new FileStream(sourceFile, FileMode.Open);
                        workbook = excelEngine.Excel.Workbooks.Open(cfFileStream);

                        //The first worksheet in the workbook is accessed.
                        IWorksheet worksheet = workbook.Worksheets[0];

                        //Create Template Marker processor.
                        //Apply the marker to export data from datatable to worksheet.
                        ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();
                        //marker.AddVariable("SalesList", table);

                        System.Data.DataTable datatable = new System.Data.DataTable();
                        SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(
                            "SELECT " +
                            "case when boss.nameId is null then tbl.boss_id_other else boss.nameId end as bossId, " +
                            "case when captain.nameId is null then tbl.captain_id_other else captain.nameId end as captainId, " +
                            "case when cadres.nameId is null then tbl.cadres_id_other else cadres.nameId end as cadresId, " +
                            "case when captain_sign.nameId is null then tbl.captain_sign_id_other else captain_sign.nameId end as captain_signId, " +
                            "tbl.*, " +
                            "Tinh.Ten as province_idST, " +
                            "cecm_programData.code as 'cecm_program_code'  " +
                            "FROM RPBM7 tbl " +
                            "left join cecm_provinces Tinh on tbl.province_id = Tinh.id " +
                            "left join Cecm_ProgramStaff boss on tbl.boss_id = boss.id " +
                            "left join Cecm_ProgramStaff captain on tbl.captain_id = captain.id " +
                            "left join Cecm_ProgramStaff cadres on tbl.cadres_id = cadres.id " +
                            "left join Cecm_ProgramStaff captain_sign on tbl.captain_sign_id = captain_sign.id " +
                            "left join cecm_programData on cecm_programData.id = tbl.cecm_program_id " +
                            "where gid = " + id_kqks, cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatable);
                        marker.AddVariable("obj", datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            DataRow dr = datatable.Rows[0];
                            DateTime now = DateTime.Now;
                           
                            marker.AddVariable("Ngaybaocao", dr["province_idST"].ToString().Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                            marker.AddVariable("Symbols", dr["symbol"] + " / NK - TCRPBM");

                        }

                        System.Data.DataTable datatableSub1 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub1 = null;
                        SqlDataAdapter sqlAdapterSub1 = null;
                        string sql1 =
                            "select " +
                            "ROW_NUMBER() OVER (ORDER BY tbl.gid) as 'STT', " +
                            "tbl.string2, " +
                            "tbl.string3, " +
                            "tbl.string4, " +
                            "CASE WHEN ndcv.the_content is null then tbl.string1 else ndcv.the_content end as CongViec " +
                            "from groundDeliveryRecordsMember tbl " +
                            "left join NoiDungCongViec ndcv on tbl.long2 = ndcv.id " +
                            "where tbl.main_id = " + id_kqks + " " +
                            "and tbl.table_name = N'" + table_name + "' " +
                            "and field_name = '" + FrmThemmoiRPBM7.field_name_cv + "' " +
                            "and tbl.long1 = 1";
                        sqlAdapterSub1 = new SqlDataAdapter(sql1, cn);
                        sqlCommandSub1 = new SqlCommandBuilder(sqlAdapterSub1);
                        sqlAdapterSub1.Fill(datatableSub1);
                        marker.AddVariable("datatableSub1", datatableSub1);

                        System.Data.DataTable datatableSub2 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub2 = null;
                        SqlDataAdapter sqlAdapterSub2 = null;
                        string sql2 =
                            "select " +
                            "ROW_NUMBER() OVER (ORDER BY tbl.gid) as 'STT', " +
                            "tbl.string2, " +
                            "tbl.string3, " +
                            "tbl.string4, " +
                            "CASE WHEN ndcv.the_content is null then tbl.string1 else ndcv.the_content end as CongViec " +
                            "from groundDeliveryRecordsMember tbl " +
                            "left join NoiDungCongViec ndcv on tbl.long2 = ndcv.id " +
                            "where tbl.main_id = " + id_kqks + " " +
                            "and tbl.table_name = N'" + table_name + "' " +
                            "and field_name = '" + FrmThemmoiRPBM7.field_name_cv + "' " +
                            "and tbl.long1 = 9";
                        sqlAdapterSub2 = new SqlDataAdapter(sql2, cn);
                        sqlCommandSub2 = new SqlCommandBuilder(sqlAdapterSub2);
                        sqlAdapterSub2.Fill(datatableSub2);
                        marker.AddVariable("datatableSub2", datatableSub2);

                        System.Data.DataTable datatableSub3 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub3 = null;
                        SqlDataAdapter sqlAdapterSub3 = null;
                        string sql3 =
                            "select " +
                            "ROW_NUMBER() OVER (ORDER BY tbl.gid) as 'STT', " +
                            "tbl.string2, " +
                            "tbl.string3, " +
                            "tbl.string4, " +
                            "CASE WHEN ndcv.the_content is null then tbl.string1 else ndcv.the_content end as CongViec " +
                            "from groundDeliveryRecordsMember tbl " +
                            "left join NoiDungCongViec ndcv on tbl.long2 = ndcv.id " +
                            "where tbl.main_id = " + id_kqks + " " +
                            "and tbl.table_name = N'" + table_name + "' " +
                            "and field_name = '" + FrmThemmoiRPBM7.field_name_cv + "' " +
                            "and tbl.long1 = 18";
                        sqlAdapterSub3 = new SqlDataAdapter(sql3, cn);
                        sqlCommandSub3 = new SqlCommandBuilder(sqlAdapterSub3);
                        sqlAdapterSub3.Fill(datatableSub3);
                        marker.AddVariable("datatableSub3", datatableSub3);

                        System.Data.DataTable datatableSub4 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub4 = null;
                        SqlDataAdapter sqlAdapterSub4 = null;
                        string sql4 =
                            "select " +
                            "ROW_NUMBER() OVER (ORDER BY tbl.gid) as 'STT', " +
                            "tbl.string2, " +
                            "tbl.string3, " +
                            "tbl.string4, " +
                            "tbl.long2, " +
                            "CASE WHEN mst.dvs_name is null or tbl.long1 = 14 then tbl.string1 else mst.dvs_name end as LoaiBMVN " +
                            "from groundDeliveryRecordsMember tbl " +
                            "left join mst_division mst on tbl.long1 = mst.dvs_value and mst.dvs_group_cd = '001' " +
                            "where tbl.main_id = " + id_kqks + " " +
                            "and tbl.table_name = N'" + table_name + "' " +
                            "and field_name = '" + FrmThemmoiRPBM7.field_name_bmvn + "' ";
                        sqlAdapterSub4 = new SqlDataAdapter(sql4, cn);
                        sqlCommandSub4 = new SqlCommandBuilder(sqlAdapterSub4);
                        sqlAdapterSub4.Fill(datatableSub4);
                        marker.AddVariable("datatableSub4", datatableSub4);

                        //marker.AddVariable("str", "Nguyễn Minh Hiếu");
                        marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                        //Saving and closing the workbook
                        workbook.SaveAs(pathFile);
                        workbook.ActiveSheetIndex = 0;

                        //Close the workbook
                        workbook.Close();
                        cfFileStream.Close();

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook workbook1 = excel.Workbooks.Open(pathFile);
                        workbook1.Worksheets.get_Item(1).Activate();
                        workbook1.Save();
                        workbook1.Close();

                        FileInfo fi = new FileInfo(pathFile);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(pathFile);
                        }
                    }
                }
            }

            if (e.ColumnIndex == 5)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM7 left join cecm_programData on cecm_programData.id = RPBM7.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM7 lst = new classRPBM7();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.deptid_survey = int.Parse(dr["deptid_survey"].ToString());
                    try
                    {
                        lst.deptid_surveyST = dr["deptid_surveyST"].ToString();
                    }
                    catch
                    {
                        lst.deptid_surveyST = "";
                    }
                    lst.address = dr["address"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    //lst.dates = dr["dates"].ToString();
                    lst.captain_id = int.Parse(dr["captain_id"].ToString());
                    try
                    {
                        lst.captain_idST = dr["captain_idST"].ToString();
                    }
                    catch
                    {
                        lst.captain_idST = "";
                    }
                    lst.captain_id_other = dr["captain_id_other"].ToString();

                    lst.cadres_id = int.Parse(dr["cadres_id"].ToString());
                    try
                    {
                        lst.cadres_idST = dr["cadres_idST"].ToString();
                    }
                    catch
                    {
                        lst.cadres_idST = "";
                    }
                    lst.cadres_id_other = dr["cadres_id_other"].ToString();

                    lst.dates_tcST = dr["dates_tcST"].ToString();
                    lst.weather = dr["weather"].ToString();
                    lst.comment = dr["comment"].ToString();
                    lst.address_tc = dr["address_tc"].ToString();
                    lst.weather = dr["weather"].ToString();
                    lst.comment = dr["comment"].ToString();

                    lst.boss_id = int.Parse(dr["boss_id"].ToString());
                    try
                    {
                        lst.boss_idST = dr["boss_idST"].ToString();
                    }
                    catch
                    {
                        lst.boss_idST = "";
                    }
                    lst.boss_id_other = dr["boss_id_other"].ToString();

                    lst.captain_sign_id = int.Parse(dr["captain_sign_id"].ToString());
                    try
                    {
                        lst.captain_sign_idST = dr["captain_sign_idST"].ToString();
                    }
                    catch
                    {
                        lst.captain_sign_idST = "";
                    }
                    lst.captain_sign_id_other = dr["captain_sign_id_other"].ToString();
                    lst.files = dr["files"].ToString();

                    List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                    SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = @main_id and table_name = @table_name and field_name = @field_name", cn);
                    sqlAdapterProvince1.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "main_id",
                        Value = id_kqks,
                        SqlDbType = SqlDbType.BigInt,
                    });
                    sqlAdapterProvince1.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "table_name",
                        Value = table_name,
                        SqlDbType = SqlDbType.NVarChar,
                    });
                    sqlAdapterProvince1.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "field_name",
                        Value = FrmThemmoiRPBM7.field_name_cv,
                        SqlDbType = SqlDbType.NVarChar,
                    });
                    sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                    System.Data.DataTable datatableProvince1 = new System.Data.DataTable();
                    sqlAdapterProvince1.Fill(datatableProvince1);
                    foreach (DataRow dr1 in datatableProvince1.Rows)
                    {
                        groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                        A.gid = int.Parse(dr1["gid"].ToString());
                        A.table_name = dr1["table_name"].ToString();
                        A.field_name = dr1["field_name"].ToString();
                        A.main_id = int.Parse(dr1["main_id"].ToString());
                        A.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                        A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;
                        A.string1 = dr1["string1"].ToString();
                        A.string2 = dr1["string2"].ToString();
                        A.string3 = dr1["string3"].ToString();
                        A.string4 = dr1["string4"].ToString();
                        A.string5 = dr1["string5"].ToString();
                        A.string6 = dr1["string6"].ToString();
                        Lst1.Add(A);
                    }
                    lst.constructionDiaryGeneralInfor_lstSubTable = Lst1;

                    List<groundDeliveryRecords_Member> Lst2 = new List<groundDeliveryRecords_Member>();
                    SqlDataAdapter sqlAdapterProvince2 = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = @main_id and table_name = @table_name and field_name = @field_name", cn);
                    sqlAdapterProvince2.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "main_id",
                        Value = id_kqks,
                        SqlDbType = SqlDbType.BigInt,
                    });
                    sqlAdapterProvince2.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "table_name",
                        Value = table_name,
                        SqlDbType = SqlDbType.NVarChar,
                    });
                    sqlAdapterProvince2.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "field_name",
                        Value = FrmThemmoiRPBM7.field_name_bmvn,
                        SqlDbType = SqlDbType.NVarChar,
                    });
                    sqlCommand = new SqlCommandBuilder(sqlAdapterProvince2);
                    System.Data.DataTable datatableProvince2 = new System.Data.DataTable();
                    sqlAdapterProvince2.Fill(datatableProvince2);
                    foreach (DataRow dr1 in datatableProvince2.Rows)
                    {
                        groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                        A.gid = int.Parse(dr1["gid"].ToString());
                        A.table_name = dr1["table_name"].ToString();
                        A.field_name = dr1["field_name"].ToString();
                        A.main_id = int.Parse(dr1["main_id"].ToString());
                        A.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                        A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;
                        A.string1 = dr1["string1"].ToString();
                        A.string2 = dr1["string2"].ToString();
                        A.string3 = dr1["string3"].ToString();
                        A.string4 = dr1["string4"].ToString();
                        Lst2.Add(A);
                    }
                    lst.reportConfDestroy_Bomb_lstSubTable = Lst2;

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM7");
                    if (string.IsNullOrEmpty(saveLocation) == false)
                    {
                        System.IO.File.WriteAllText(saveLocation, json);
                        MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
                    }
                    //CompressedFile frm = new CompressedFile(LstDaily, Phieudieutra, Baocao, Bando);
                    //frm.ShowDialog();
                }
            }
            if (e.ColumnIndex == 6)
            {
                UtilsDatabase.DeleteMemberByTablename(table_name);
                FrmThemmoiRPBM7 frm = new FrmThemmoiRPBM7(id_kqks);
                frm.Text = "CHỈNH SỬA NHẬT KÝ THI CÔNG (MẪU CHUNG)";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM7] WHERE gid = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                    UtilsDatabase.DeleteMemberByMainId(table_name, id_kqks);

                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
        }

        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            UtilsDatabase.DeleteMemberByTablename(table_name);
            FrmThemmoiRPBM7 frm = new FrmThemmoiRPBM7(0);
            frm.Text = "THÊM MỚI NHẬT KÝ THI CÔNG (MẪU CHUNG)";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


