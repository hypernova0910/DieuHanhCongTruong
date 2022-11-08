using DieuHanhCongTruong;
using DieuHanhCongTruong.Common;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class FrmKHKSKT : Form
    {
        private ConnectionWithExtraInfo _cn;

        public FrmKHKSKT()
        {
            _cn = UtilsDatabase._ExtraInfoConnettion;
            InitializeComponent();
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmKHDTBMVN_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            SqlConnection cn = _cn.Connection as SqlConnection;
            System.Data.DataTable datatable = new System.Data.DataTable();
            dgvKHDTBMVN.Rows.Clear();
            string sql =
                @"SELECT 
                tbl.id as id, 
                DA.name as TenDA,
                d.name as TenDVKS
                FROM KehoachKSKT tbl
                left join cecm_programData DA on tbl.program_id = DA.id 
                left join cert_department d on tbl.DVKS_id = d.id 
                WHERE 1=1 ";
            if (!string.IsNullOrEmpty(TxtTImkiem.Text))
            {
                sql += "AND LOWER(DA.name) LIKE @TenDA ";
            }
            if (timeNgaybatdau.Checked)
            {
                sql += "AND tbl.TGTH_ngaybatdau > @ngaybatdau ";
            }
            if (timeNgayketthuc.Checked)
            {
                sql += "AND tbl.TGTH_ngayketthuc > @ngayketthuc ";
            }
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, cn);
            if (!string.IsNullOrEmpty(TxtTImkiem.Text))
            {
                sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TenDA",
                    Value = "%" + TxtTImkiem.Text.ToLower() + "%",
                    SqlDbType = SqlDbType.NVarChar
                });
            }
            if (timeNgaybatdau.Checked)
            {
                sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ngaybatdau",
                    Value = timeNgaybatdau.Value,
                    SqlDbType = SqlDbType.DateTime
                });
            }
            if (timeNgayketthuc.Checked)
            {
                sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ngayketthuc",
                    Value = timeNgayketthuc.Value,
                    SqlDbType = SqlDbType.DateTime
                });
            }
            sqlAdapter.Fill(datatable);
            if (datatable.Rows.Count != 0)
            {
                int indexRow = 1;
                foreach (DataRow dr in datatable.Rows)
                {
                    long id = long.Parse(dr["id"].ToString());
                    string tenDuAn = dr["TenDA"].ToString();
                    string TenDVKS = dr["TenDVKS"].ToString();

                    dgvKHDTBMVN.Rows.Add(indexRow, tenDuAn, TenDVKS);
                    dgvKHDTBMVN.Rows[indexRow - 1].Tag = id;

                    indexRow++;
                }
            }
        }

        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            FrmThemmoiKHKSKT frm = new FrmThemmoiKHKSKT(-1);
            frm.ShowDialog();
            LoadData();
        }

        private void dgvKHDTBMVN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            long id = (long)dgvKHDTBMVN.Rows[e.RowIndex].Tag;
            if (e.ColumnIndex == cotSua.Index)
            {
                FrmThemmoiKHKSKT frm = new FrmThemmoiKHKSKT(id);
                frm.ShowDialog();
                LoadData();
            }
            else if(e.ColumnIndex == cotXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM KehoachKSKT WHERE id = " + id, _cn.Connection as SqlConnection);
                int temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData();
            }
            else if(e.ColumnIndex == cotExcel.Index)
            {
                try
                {


                    string pathFile = "";
                    saveFileDialog1.FileName = "KHKS_" + Guid.NewGuid().ToString();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        pathFile = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                    string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_KHKSKT.xls";
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

                            DataTable datatable = new DataTable();
                            string sql =
                                @"SELECT 
                                tbl.id
                                ,[Makehoach]
                              ,[Tinh_id]
                              ,[Huyen_id]
                              ,[Xa_id]
                              ,[DVKS_id]
                              ,[program_id]
                              ,[Diadiem]
                              ,[KSDH_KL]
                              ,FORMAT([KSDH_ngaybatdau], 'dd/MM/yyyy') as KSDH_ngaybatdau
                              ,FORMAT([KSDH_ngayketthuc], 'dd/MM/yyyy') as KSDH_ngayketthuc
                              ,[KSDH_ghichu]
                              ,[THBS_KL]
                              ,FORMAT([THBS_ngaybatdau], 'dd/MM/yyyy') as THBS_ngaybatdau
                              ,FORMAT([THBS_ngayketthuc], 'dd/MM/yyyy') as THBS_ngayketthuc
                              ,[THBS_ghichu]
                              ,[IATL_KL]
                              ,FORMAT([IATL_ngaybatdau], 'dd/MM/yyyy') as IATL_ngaybatdau
                              ,FORMAT([IATL_ngayketthuc], 'dd/MM/yyyy') as IATL_ngayketthuc
                              ,[IATL_ghichu]
                              ,[XDPD_KL]
                              ,FORMAT([XDPD_ngaybatdau], 'dd/MM/yyyy') as XDPD_ngaybatdau
                              ,FORMAT([XDPD_ngayketthuc], 'dd/MM/yyyy') as XDPD_ngayketthuc
                              ,[XDPD_ghichu]
                              ,[KTCL_KL]
                              ,FORMAT([KTCL_ngaybatdau], 'dd/MM/yyyy') as KTCL_ngaybatdau
                              ,FORMAT([KTCL_ngayketthuc], 'dd/MM/yyyy') as KTCL_ngayketthuc
                              ,[KTCL_ghichu]
                              ,[DKCT_KL]
                              ,FORMAT([DKCT_ngaybatdau], 'dd/MM/yyyy') as DKCT_ngaybatdau
                              ,FORMAT([DKCT_ngayketthuc], 'dd/MM/yyyy') as DKCT_ngayketthuc
                              ,[DKCT_ghichu]
                              ,[THDL_KL]
                              ,FORMAT([THDL_ngaybatdau], 'dd/MM/yyyy') as THDL_ngaybatdau
                              ,FORMAT([THDL_ngayketthuc], 'dd/MM/yyyy') as THDL_ngayketthuc
                              ,[THDL_ghichu]
                              ,[NTKQ_KL]
                              ,FORMAT([NTKQ_ngaybatdau], 'dd/MM/yyyy') as NTKQ_ngaybatdau
                              ,FORMAT([NTKQ_ngayketthuc], 'dd/MM/yyyy') as NTKQ_ngayketthuc
                              ,[NTKQ_ghichu]
                              ,[LBB_KL]
                              ,FORMAT([LBB_ngaybatdau], 'dd/MM/yyyy') as LBB_ngaybatdau
                              ,FORMAT([LBB_ngayketthuc], 'dd/MM/yyyy') as LBB_ngayketthuc
                              ,[LBB_ghichu]
                              ,FORMAT([TGTH_ngaybatdau], 'dd/MM/yyyy') as TGTH_ngaybatdau
                              ,FORMAT([TGTH_ngayketthuc], 'dd/MM/yyyy') as TGTH_ngayketthuc
                              ,[Noinhan]
                              ,[NV1_KL]
                              ,FORMAT([NV1_ngaybatdau], 'dd/MM/yyyy') as NV1_ngaybatdau
                              ,FORMAT([NV1_ngayketthuc], 'dd/MM/yyyy') as NV1_ngayketthuc
                              ,[NV1_ghichu]
                              ,[MaNV1]
                              ,[DiadiemNV1]
                              ,[MaNV2]
                              ,[DiadiemNV2]
                              ,[Ngaytao]
                              ,[geo_common]
                              ,[social_common]
                              ,[top_file]
                              ,[info_provided]
                              ,[mission_target]
                              ,[technical_requirement]
                              ,[document_collect]
                              ,[map_draw]
                              ,[survey_geo]
                              ,[medical_handle]
                              ,[quality_guarantee]
                              ,[equipment]
                              ,[method]
                              ,[deptMaster]
                              ,[deptMaster_other]
                              ,[requirement_common],
                                Tinh.Ten as TenTinh,
                                Huyen.Ten as TenHuyen,
                                Xa.Ten as TenXa,
                                d.name as TenDVKS,
                                case when deptMaster > 0 then s.nameId else deptMaster_other end as TenChiHuy,
                                cecm_programData.name as TenDA 
                                FROM KehoachKSKT tbl 
                                left join cert_department d on d.id = tbl.DVKS_id 
                                left join cecm_programData on cecm_programData.id = tbl.program_id 
                                left join cecm_provinces as Tinh on tbl.Tinh_id = Tinh.id 
                                left join cecm_provinces as Huyen on tbl.Huyen_id = Huyen.id 
                                left join cecm_provinces as Xa on tbl.Xa_id = Xa.id 
                                left join Cecm_ProgramStaff as s on tbl.deptMaster = s.id
                                WHERE tbl.id = " + id;
                            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                            //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                            SqlCommandBuilder sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                            sqlAdapter.Fill(datatable);
                            marker.AddVariable("obj", datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                DataRow dr = datatable.Rows[0];
                                marker.AddVariable("Makehoach", "Số: " + dr["Makehoach"].ToString());
                                DateTime.TryParse(dr["Ngaytao"].ToString(), out DateTime now);
                                marker.AddVariable("Ngaybaocao", dr["TenTinh"].ToString() + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                                marker.AddVariable("header", string.Format("Khảo sát kỹ thuật xác định khu vực ô nhiễm bom mìn vật nổ tại {0}, {1}, {2}", dr["TenXa"].ToString(), dr["TenHuyen"].ToString(), dr["TenTinh"].ToString()));
                            }

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
            else if(e.ColumnIndex == cotJSON.Index)
            {
                DataTable datatable = new DataTable();
                string sql =
                    @"SELECT 
                    tbl.[id]
                    ,[Makehoach]
                    ,[Tinh_id]
                    ,[Huyen_id]
                    ,[Xa_id]
                    ,[DVKS_id]
                    ,[program_id]
                    ,[Diadiem]
                    ,[KSDH_KL]
                    ,[KSDH_ngaybatdau]
                    ,[KSDH_ngayketthuc]
                    ,[KSDH_ghichu]
                    ,[THBS_KL]
                    ,[THBS_ngaybatdau]
                    ,[THBS_ngayketthuc]
                    ,[THBS_ghichu]
                    ,[IATL_KL]
                    ,[IATL_ngaybatdau]
                    ,[IATL_ngayketthuc]
                    ,[IATL_ghichu]
                    ,[XDPD_KL]
                    ,[XDPD_ngaybatdau]
                    ,[XDPD_ngayketthuc]
                    ,[XDPD_ghichu]
                    ,[KTCL_KL]
                    ,[KTCL_ngaybatdau]
                    ,[KTCL_ngayketthuc]
                    ,[KTCL_ghichu]
                    ,[DKCT_KL]
                    ,[DKCT_ngaybatdau]
                    ,[DKCT_ngayketthuc]
                    ,[DKCT_ghichu]
                    ,[THDL_KL]
                    ,[THDL_ngaybatdau]
                    ,[THDL_ngayketthuc]
                    ,[THDL_ghichu]
                    ,[NTKQ_KL]
                    ,[NTKQ_ngaybatdau]
                    ,[NTKQ_ngayketthuc]
                    ,[NTKQ_ghichu]
                    ,[LBB_KL]
                    ,[LBB_ngaybatdau]
                    ,[LBB_ngayketthuc]
                    ,[LBB_ghichu]
                    ,[TGTH_ngaybatdau]
                    ,[TGTH_ngayketthuc]
                    ,[Noinhan]
                    ,[NV1_KL]
                    ,[NV1_ngaybatdau]
                    ,[NV1_ngayketthuc]
                    ,[NV1_ghichu]
                    ,[MaNV1]
                    ,[DiadiemNV1]
                    ,[MaNV2]
                    ,[DiadiemNV2]
                    ,[Ngaytao]
                    ,[geo_common]
                    ,[social_common]
                    ,[top_file]
                    ,[info_provided]
                    ,[mission_target]
                    ,[technical_requirement]
                    ,[document_collect]
                    ,[map_draw]
                    ,[survey_geo]
                    ,[medical_handle]
                    ,[quality_guarantee]
                    ,[equipment]
                    ,[method]
                    ,[deptMaster]
                    ,[deptMaster_other],
                    Tinh.code as codeTinh,
                    Huyen.code as codeHuyen,
                    Xa.code as codeXa,
                    d.code as codeDVKS,
                    nv.nameId as deptMasterST,
                    cecm_programData.code as codeDA 
                    FROM KehoachKSKT tbl 
                    left join cert_department d on d.id = tbl.DVKS_id 
                    left join cecm_programData on cecm_programData.id = tbl.program_id 
                    left join cecm_provinces as Tinh on tbl.Tinh_id = Tinh.id 
                    left join cecm_provinces as Huyen on tbl.Huyen_id = Huyen.id 
                    left join cecm_provinces as Xa on tbl.Xa_id = Xa.id 
                    left join Cecm_ProgramStaff nv on tbl.deptMaster = nv.id 
                    WHERE tbl.id = " + id;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                SqlCommandBuilder sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                KHKSKT khkskt = new KHKSKT();
                if (datatable.Rows.Count > 0)
                {
                    DataRow dr = datatable.Rows[0];
                    DateTime.TryParse(dr["Ngaytao"].ToString(), out DateTime Ngaytao);
                    List<groundDeliveryRecords_Member> files = new List<groundDeliveryRecords_Member>();
                    SqlConnection cn = _cn.Connection as SqlConnection;
                    System.Data.DataTable datatableFiles = new System.Data.DataTable();
                    string sqlFiles =
                        @"SELECT 
                    tbl.id as id, 
                    tbl.file_type as file_type,
                    tbl.file_name as file_name,
                    tbl.khks_id as khks_id 
                    FROM KehoachKSKT_File tbl
                    WHERE tbl.khks_id = " + id;
                    SqlDataAdapter sqlAdapterFiles = new SqlDataAdapter(sqlFiles, cn);
                    sqlAdapterFiles.Fill(datatableFiles);
                    if (datatableFiles.Rows.Count != 0)
                    {
                        foreach (DataRow drFiles in datatableFiles.Rows)
                        {
                            //long id = long.Parse(dr["id"].ToString());
                            long file_type = long.Parse(drFiles["file_type"].ToString());
                            string file_name = drFiles["file_name"].ToString();
                            long khks_id = long.Parse(drFiles["khks_id"].ToString());

                            groundDeliveryRecords_Member file = new groundDeliveryRecords_Member();
                            file.string1 = file_name;
                            file.long1 = file_type;
                            //dgvFile.Rows[indexRow - 1].Tag = file;
                            files.Add(file);
                        }
                    }
                    khkskt = new KHKSKT()
                    {
                        files = files,
                        code = dr["Makehoach"].ToString(),
                        date_createST = Ngaytao.ToString("dd/MM/yyyy"),
                        province_id = long.Parse(dr["Tinh_id"].ToString()),
                        district_id = long.Parse(dr["Huyen_id"].ToString()),
                        commune_id = long.Parse(dr["Xa_id"].ToString()),
                        province_code = dr["codeTinh"].ToString(),
                        district_code = dr["codeHuyen"].ToString(),
                        commune_code = dr["codeXa"].ToString(),
                        dept_code  = dr["codeDVKS"].ToString(),
                        cecm_program_code  = dr["codeDA"].ToString(),
                        address = dr["Diadiem"].ToString(),
                        ksdh_kl = int.TryParse(dr["KSDH_KL"].ToString(), out int KSDH_KL) ? KSDH_KL : 0,
                        ksdh_date_startST = Convert.ToDateTime(dr["KSDH_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        ksdh_date_endST = Convert.ToDateTime(dr["KSDH_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        ksdh_note = dr["KSDH_ghichu"].ToString(),
                        thbs_kl = int.TryParse(dr["THBS_KL"].ToString(), out int THBS_KL) ? THBS_KL : 0,
                        thbs_date_startST = Convert.ToDateTime(dr["THBS_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        thbs_date_endST = Convert.ToDateTime(dr["THBS_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        thbs_note = dr["THBS_ghichu"].ToString(),
                        iatl_kl = int.TryParse(dr["IATL_KL"].ToString(), out int IATL_KL) ? IATL_KL : 0,
                        iatl_date_startST = Convert.ToDateTime(dr["IATL_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        iatl_date_endST = Convert.ToDateTime(dr["IATL_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        iatl_note = dr["IATL_ghichu"].ToString(),
                        xdpd_kl = int.TryParse(dr["XDPD_KL"].ToString(), out int XDPD_KL) ? XDPD_KL : 0,
                        xdpd_date_startST = Convert.ToDateTime(dr["XDPD_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        xdpd_date_endST = Convert.ToDateTime(dr["XDPD_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        xdpd_note = dr["XDPD_ghichu"].ToString(),
                        ktcl_kl = int.TryParse(dr["KTCL_KL"].ToString(), out int KTCL_KL) ? KTCL_KL : 0,
                        ktcl_date_startST = Convert.ToDateTime(dr["KTCL_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        ktcl_date_endST = Convert.ToDateTime(dr["KTCL_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        ktcl_note = dr["KTCL_ghichu"].ToString(),
                        dkct_kl = int.TryParse(dr["DKCT_KL"].ToString(), out int DKCT_KL) ? DKCT_KL : 0,
                        dkct_date_startST = Convert.ToDateTime(dr["DKCT_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        dkct_date_endST = Convert.ToDateTime(dr["DKCT_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        dkct_note = dr["DKCT_ghichu"].ToString(),
                        thdl_kl = int.TryParse(dr["THDL_KL"].ToString(), out int THDL_KL) ? THDL_KL : 0,
                        thdl_date_startST = Convert.ToDateTime(dr["THDL_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        thdl_date_endST = Convert.ToDateTime(dr["THDL_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        thdl_note = dr["THDL_ghichu"].ToString(),
                        ntkq_kl = int.TryParse(dr["NTKQ_KL"].ToString(), out int NTKQ_KL) ? NTKQ_KL : 0,
                        ntkq_date_startST = Convert.ToDateTime(dr["NTKQ_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        ntkq_date_endST = Convert.ToDateTime(dr["NTKQ_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        ntkq_note = dr["NTKQ_ghichu"].ToString(),
                        lbb_kl = int.TryParse(dr["LBB_KL"].ToString(), out int LBB_KL) ? LBB_KL : 0,
                        lbb_date_startST = Convert.ToDateTime(dr["LBB_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        lbb_date_endST = Convert.ToDateTime(dr["LBB_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        lbb_note = dr["LBB_ghichu"].ToString(),
                        tgth_date_startST = Convert.ToDateTime(dr["TGTH_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        tgth_date_endST = Convert.ToDateTime(dr["TGTH_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        address_receive = dr["Noinhan"].ToString(),
                        nv1_kl = int.TryParse(dr["NV1_KL"].ToString(), out int NV1_KL) ? NV1_KL : 0,
                        nv1_date_startST = Convert.ToDateTime(dr["NV1_ngaybatdau"].ToString()).ToString("dd/MM/yyyy"),
                        nv1_date_endST = Convert.ToDateTime(dr["NV1_ngayketthuc"].ToString()).ToString("dd/MM/yyyy"),
                        nv1_note = dr["NV1_ghichu"].ToString(),
                        mission_01_code = dr["MaNV1"].ToString(),
                        mission_01_address  = dr["DiadiemNV1"].ToString(),
                        mission_02_code  = dr["MaNV2"].ToString(),
                        mission_02_address  = dr["DiadiemNV2"].ToString(),
                        geo_common = dr["geo_common"].ToString(),
                        social_common = dr["social_common"].ToString(),
                        top_file = dr["top_file"].ToString(),
                        info_provided = dr["info_provided"].ToString(),
                        mission_target = dr["mission_target"].ToString(),
                        technical_requirement = dr["technical_requirement"].ToString(),
                        document_collect = dr["document_collect"].ToString(),
                        map_draw = dr["map_draw"].ToString(),
                        survey_geo = dr["survey_geo"].ToString(),
                        medical_handle = dr["medical_handle"].ToString(),
                        quality_guarantee = dr["quality_guarantee"].ToString(),
                        equipment = dr["equipment"].ToString(),
                        method = dr["method"].ToString(),
                        deptMasterST = dr["deptMasterST"].ToString(),
                        deptMaster_other = dr["deptMaster_other"].ToString(),
                    };
                }
                string json = JsonConvert.SerializeObject(khkskt, Formatting.Indented);

                var saveLocation = AppUtils.SaveFileTxt();
                if (string.IsNullOrEmpty(saveLocation) == false)
                {
                    System.IO.File.WriteAllText(saveLocation, json);
                    MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                }
                else
                {
                    MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
                }
            }
        }

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            timeNgaybatdau.Checked = false;
            timeNgayketthuc.Checked = false;
            LoadData();
        }

        private void timeNgaybatdau_ValueChanged(object sender, EventArgs e)
        {
            if (timeNgaybatdau.Value > timeNgayketthuc.Value)
            {
                timeNgaybatdau.Value = timeNgayketthuc.Value;
            }
        }

        private void timeNgayketthuc_ValueChanged(object sender, EventArgs e)
        {
            if (timeNgaybatdau.Value > timeNgayketthuc.Value)
            {
                timeNgayketthuc.Value = timeNgaybatdau.Value;
            }
        }
    }
}
