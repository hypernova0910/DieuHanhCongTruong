using DieuHanhCongTruong.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class FrmThemmoiKHKSKT : Form
    {
        private long id;
        private ConnectionWithExtraInfo _cn = null;

        public FrmThemmoiKHKSKT(long id)
        {
            this.id = id;
            _cn = UtilsDatabase._ExtraInfoConnettion;
            InitializeComponent();
            panel1.VerticalScroll.Value = 0;
            panel1.AutoScrollPosition = new Point(0, 0);
            foreach (Control control in panel1.Controls)
            {
                if (control is RichTextBox)
                {
                    RichTextBox rtb = (RichTextBox)control;
                    rtb.VScroll += HandleRichTextBoxAdjustScroll;
                    //rtb.TextChanged += HandleRichTextBoxAdjustScroll;
                }
            }
        }

        private const UInt32 SB_TOP = 0x6;
        private const UInt32 WM_VSCROLL = 0x115;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private void HandleRichTextBoxAdjustScroll(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            PostMessage(rtb.Handle, WM_VSCROLL, (IntPtr)SB_TOP, IntPtr.Zero);
            
            //rtb.
        }

        //private IntPtr handle;

        private void FrmThemmoiKHKSKT_Load(object sender, EventArgs e)
        {
            UtilsDatabase.LoadCBDA(comboBox_TenDA);
            UtilsDatabase.LoadCBTinh(comboBox_Tinh);
            LoadCBStaff(cbDeptMaster);
            if (id > 0)
            {
                this.Text = "CHỈNH SỬA KẾ HOẠCH KHẢO SÁT KỸ THUẬT XÁC ĐỊNH KHU VỰC Ô NHIỄM BOM MÌN VẬT NỔ";
                DataTable datatable = new DataTable();
                string sql = "SELECT * FROM KehoachKSKT WHERE id = " + id;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    DataRow dr = datatable.Rows[0];
                    txtMaKH.Text = dr["Makehoach"].ToString();
                    if(DateTime.TryParse(dr["Ngaytao"].ToString(), out DateTime Ngaytao))
                    {
                        timeNgaytao.Value = Ngaytao;
                    }
                    tbMaNV1.Text = dr["MaNV1"].ToString();
                    tbDiadiemNV1.Text = dr["DiadiemNV1"].ToString();
                    tbMaNV2.Text = dr["MaNV2"].ToString();
                    tbDiadiemNV2.Text = dr["DiadiemNV2"].ToString();
                    if (long.TryParse(dr["Tinh_id"].ToString(), out long Tinh_id))
                    {
                        comboBox_Tinh.SelectedValue = Tinh_id;
                    }
                    if (long.TryParse(dr["Huyen_id"].ToString(), out long Huyen_id))
                    {
                        comboBox_Huyen.SelectedValue = Huyen_id;
                    }
                    if (long.TryParse(dr["Xa_id"].ToString(), out long Xa_id))
                    {
                        comboBox_Xa.SelectedValue = Xa_id;
                    }
                    if (long.TryParse(dr["program_id"].ToString(), out long program_id))
                    {
                        comboBox_TenDA.SelectedValue = program_id;
                    }
                    if (long.TryParse(dr["DVKS_id"].ToString(), out long DVKS_id))
                    {
                        cbDVKS.SelectedValue = DVKS_id;
                    }
                    tbDiadiem.Text = dr["Diadiem"].ToString();

                    if (int.TryParse(dr["KSDH_KL"].ToString(), out int KSDH_KL))
                    {
                        nudKSDH_KL.Value = KSDH_KL;
                    }
                    dtpKSDH_ngaybatdau.Value = DateTime.Parse(dr["KSDH_ngaybatdau"].ToString());
                    dtpKSDH_ngayketthuc.Value = DateTime.Parse(dr["KSDH_ngayketthuc"].ToString());
                    tbKSDH_ghichu.Text = dr["KSDH_ghichu"].ToString();

                    if (int.TryParse(dr["THBS_KL"].ToString(), out int THBS_KL))
                    {
                        nudTHBS_KL.Value = THBS_KL;
                    }
                    dtpTHBS_ngaybatdau.Value = DateTime.Parse(dr["THBS_ngaybatdau"].ToString());
                    dtpTHBS_ngayketthuc.Value = DateTime.Parse(dr["THBS_ngayketthuc"].ToString());
                    tbTHBS_ghichu.Text = dr["THBS_ghichu"].ToString();

                    if (int.TryParse(dr["IATL_KL"].ToString(), out int IATL_KL))
                    {
                        nudIATL_KL.Value = IATL_KL;
                    }
                    dtpIATL_ngaybatdau.Value = DateTime.Parse(dr["IATL_ngaybatdau"].ToString());
                    dtpIATL_ngayketthuc.Value = DateTime.Parse(dr["IATL_ngayketthuc"].ToString());
                    tbIATL_ghichu.Text = dr["IATL_ghichu"].ToString();

                    if (int.TryParse(dr["XDPD_KL"].ToString(), out int XDPD_KL))
                    {
                        nudXDPD_KL.Value = XDPD_KL;
                    }
                    dtpXDPD_ngaybatdau.Value = DateTime.Parse(dr["XDPD_ngaybatdau"].ToString());
                    dtpXDPD_ngayketthuc.Value = DateTime.Parse(dr["XDPD_ngayketthuc"].ToString());
                    tbXDPD_ghichu.Text = dr["XDPD_ghichu"].ToString();

                    if (int.TryParse(dr["KTCL_KL"].ToString(), out int KTCL_KL))
                    {
                        nudKTCL_KL.Value = KTCL_KL;
                    }
                    dtpKTCL_ngaybatdau.Value = DateTime.Parse(dr["KTCL_ngaybatdau"].ToString());
                    dtpKTCL_ngayketthuc.Value = DateTime.Parse(dr["KTCL_ngayketthuc"].ToString());
                    tbKTCL_ghichu.Text = dr["KTCL_ghichu"].ToString();

                    if (int.TryParse(dr["DKCT_KL"].ToString(), out int DKCT_KL))
                    {
                        nudDKCT_KL.Value = DKCT_KL;
                    }
                    dtpDKCT_ngaybatdau.Value = DateTime.Parse(dr["DKCT_ngaybatdau"].ToString());
                    dtpDKCT_ngayketthuc.Value = DateTime.Parse(dr["DKCT_ngayketthuc"].ToString());
                    tbDKCT_ghichu.Text = dr["DKCT_ghichu"].ToString();

                    if (int.TryParse(dr["THDL_KL"].ToString(), out int THDL_KL))
                    {
                        nudTHDL_KL.Value = THDL_KL;
                    }
                    dtpTHDL_ngaybatdau.Value = DateTime.Parse(dr["THDL_ngaybatdau"].ToString());
                    dtpTHDL_ngayketthuc.Value = DateTime.Parse(dr["THDL_ngayketthuc"].ToString());
                    tbTHDL_ghichu.Text = dr["THDL_ghichu"].ToString();

                    if (int.TryParse(dr["NTKQ_KL"].ToString(), out int NTKQ_KL))
                    {
                        nudNTKQ_KL.Value = NTKQ_KL;
                    }
                    dtpNTKQ_ngaybatdau.Value = DateTime.Parse(dr["NTKQ_ngaybatdau"].ToString());
                    dtpNTKQ_ngayketthuc.Value = DateTime.Parse(dr["NTKQ_ngayketthuc"].ToString());
                    tbNTKQ_ghichu.Text = dr["NTKQ_ghichu"].ToString();

                    if(int.TryParse(dr["LBB_KL"].ToString(), out int LBB_KL))
                    {
                        nudLBB_KL.Value = LBB_KL;
                    }
                    dtpLBB_ngaybatdau.Value = DateTime.Parse(dr["LBB_ngaybatdau"].ToString());
                    dtpLBB_ngayketthuc.Value = DateTime.Parse(dr["LBB_ngayketthuc"].ToString());
                    tbLBB_ghichu.Text = dr["LBB_ghichu"].ToString();
                    
                    if(decimal.TryParse(dr["NV1_KL"].ToString(), out decimal NV1_KL))
                    {
                        nudNV1_KL.Value = NV1_KL;
                    }
                    dtpNV1_ngaybatdau.Value = DateTime.Parse(dr["NV1_ngaybatdau"].ToString());
                    dtpNV1_ngayketthuc.Value = DateTime.Parse(dr["NV1_ngayketthuc"].ToString());
                    tbNV1_ghichu.Text = dr["NV1_ghichu"].ToString();

                    dtpTGTH_ngaybatdau.Value = DateTime.Parse(dr["TGTH_ngaybatdau"].ToString());
                    dtpTGTH_ngayketthuc.Value = DateTime.Parse(dr["TGTH_ngayketthuc"].ToString());

                    rtbGeoCommon.Text = dr["geo_common"].ToString();
                    rtbSocialCommon.Text = dr["social_common"].ToString();
                    tbDoc_file.Text = dr["top_file"].ToString();
                    rtbInfoProvided.Text = dr["info_provided"].ToString();
                    rtbMissionTarget.Text = dr["mission_target"].ToString();
                    rtbTechnicalRequirement.Text = dr["technical_requirement"].ToString();
                    rtbRequirementCommon.Text = dr["requirement_common"].ToString();
                    rtbDocumentCollect.Text = dr["document_collect"].ToString();
                    rtbMapDraw.Text = dr["map_draw"].ToString();
                    rtbSurveyGeo.Text = dr["survey_geo"].ToString();
                    rtbMedicalHandle.Text = dr["medical_handle"].ToString();
                    rtbQualityGuarantee.Text = dr["quality_guarantee"].ToString();
                    rtbEquipment.Text = dr["equipment"].ToString();
                    rtbMethod.Text = dr["method"].ToString();
                    cbDeptMaster.SelectedValue = long.TryParse(dr["deptMaster"].ToString(), out long deptMaster) ? deptMaster : -1;
                    tbDeptMasterOther.Text = dr["deptMaster_other"].ToString();

                    rtbSocialCommon.Text = dr["social_common"].ToString();
                    rtbSocialCommon.Text = dr["social_common"].ToString();
                    rtbSocialCommon.Text = dr["social_common"].ToString();

                    rtbNoinhan.Text = dr["Noinhan"].ToString();

                    LoadFiles();
                }
            }
            else
            {
                this.Text = "THÊM MỚI KẾ HOẠCH KHẢO SÁT KỸ THUẬT XÁC ĐỊNH KHU VỰC Ô NHIỄM BOM MÌN VẬT NỔ";
            }
        }

        private void LoadCBStaff(ComboBox cb)
        {
            cb.DataSource = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + comboBox_TenDA.SelectedValue, "id", "nameId");
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private bool DeleteSubTable()
        {
            try
            {
                SqlCommand cmd2 = new SqlCommand("DELETE FROM KehoachKSKT_File WHERE khks_id = " + id, _cn.Connection as SqlConnection);
                cmd2.Transaction = _cn.Transaction as SqlTransaction;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool UpdateFile()
        {
            try
            {
                foreach (DataGridViewRow dataGridViewRow in dgvFile.Rows)
                {
                    KHKSKT_File file = dataGridViewRow.Tag as KHKSKT_File;
                    //if (id_kqks != 0),Dientichcaybui,Dientichcayto,MatdoTB,Sotinhieu=,Ghichu,Dientichtretruc,Matdothua,Matdoday,Matdo, DaxulyM2
                    //{
                    string sql =
                        "INSERT INTO KehoachKSKT_File" +
                        "(file_name, file_type, khks_id) " +
                        "VALUES(@file_name, @file_type, @khks_id)";
                    SqlCommand cmd2 = new SqlCommand(sql, _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter file_name = new SqlParameter("@file_name", SqlDbType.NVarChar, 1000);
                    //Vungcoso.Value = comboBox_Vungcoso.SelectedItem.ToString();
                    file_name.Value = file.file_name != null ? file.file_name : "";
                    cmd2.Parameters.Add(file_name);

                    SqlParameter file_type = new SqlParameter("@file_type", SqlDbType.BigInt);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    file_type.Value = file.file_type;
                    cmd2.Parameters.Add(file_type);

                    SqlParameter khks_id = new SqlParameter("@khks_id", SqlDbType.BigInt);
                    khks_id.Value = id;
                    cmd2.Parameters.Add(khks_id);

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            _cn.BeginTransaction();
            bool success = UpdateInfomation();
            if (id <= 0)
            {
                id = UtilsDatabase.GetLastIdIndentifyTable(_cn, "KehoachKSKT");
            }
            bool successDelete = DeleteSubTable();
            bool successFile = UpdateFile();
            if (success && successDelete && successFile)
            {
                _cn.Transaction.Commit();
                MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                this.Close();
            }
            else
            {
                _cn.Transaction.Rollback();
                MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
            }
        }

        private bool UpdateInfomation()
        {
            try
            {
                string sql = "";
                //update
                if (id > 0)
                {
                    sql =
                        @"UPDATE KehoachKSKT SET 
                        Makehoach=@Makehoach,
                        Ngaytao=@Ngaytao,
                        MaNV1=@MaNV1,
                        DiadiemNV1=@DiadiemNV1,
                        MaNV2=@MaNV2,
                        DiadiemNV2=@DiadiemNV2,
                        Tinh_id=@Tinh_id,
                        Huyen_id=@Huyen_id,
                        Xa_id=@Xa_id,
                        DVKS_id=@DVKS_id,
                        program_id=@program_id,
                        Diadiem=@Diadiem,
                        geo_common=@geo_common,
                        social_common=@social_common,
                        top_file=@top_file,
                        info_provided=@info_provided,
                        mission_target=@mission_target,

                        KSDH_KL=@KSDH_KL,
                        KSDH_ngaybatdau=@KSDH_ngaybatdau,
                        KSDH_ngayketthuc=@KSDH_ngayketthuc,
                        KSDH_ghichu=@KSDH_ghichu,

                        THBS_KL=@THBS_KL,
                        THBS_ngaybatdau=@THBS_ngaybatdau,
                        THBS_ngayketthuc=@THBS_ngayketthuc,
                        THBS_ghichu=@THBS_ghichu,

                        IATL_KL=@IATL_KL,
                        IATL_ngaybatdau=@IATL_ngaybatdau,
                        IATL_ngayketthuc=@IATL_ngayketthuc,
                        IATL_ghichu=@IATL_ghichu,

                        XDPD_KL=@XDPD_KL,
                        XDPD_ngaybatdau=@XDPD_ngaybatdau,
                        XDPD_ngayketthuc=@XDPD_ngayketthuc,
                        XDPD_ghichu=@XDPD_ghichu,

                        KTCL_KL=@KTCL_KL,
                        KTCL_ngaybatdau=@KTCL_ngaybatdau,
                        KTCL_ngayketthuc=@KTCL_ngayketthuc,
                        KTCL_ghichu=@KTCL_ghichu,

                        DKCT_KL=@DKCT_KL,
                        DKCT_ngaybatdau=@DKCT_ngaybatdau,
                        DKCT_ngayketthuc=@DKCT_ngayketthuc,
                        DKCT_ghichu=@DKCT_ghichu,

                        THDL_KL=@THDL_KL,
                        THDL_ngaybatdau=@THDL_ngaybatdau,
                        THDL_ngayketthuc=@THDL_ngayketthuc,
                        THDL_ghichu=@THDL_ghichu,

                        NTKQ_KL=@NTKQ_KL,
                        NTKQ_ngaybatdau=@NTKQ_ngaybatdau,
                        NTKQ_ngayketthuc=@NTKQ_ngayketthuc,
                        NTKQ_ghichu=@NTKQ_ghichu,

                        LBB_KL=@LBB_KL,
                        LBB_ngaybatdau=@LBB_ngaybatdau,
                        LBB_ngayketthuc=@LBB_ngayketthuc,
                        LBB_ghichu=@LBB_ghichu,

                        NV1_KL=@NV1_KL,
                        NV1_ngaybatdau=@NV1_ngaybatdau,
                        NV1_ngayketthuc=@NV1_ngayketthuc,
                        NV1_ghichu=@NV1_ghichu,

                        TGTH_ngaybatdau=@TGTH_ngaybatdau,
                        TGTH_ngayketthuc=@TGTH_ngayketthuc,

                        technical_requirement=@technical_requirement,
                        requirement_common=@requirement_common,
                        document_collect=@document_collect,
                        map_draw=@map_draw,
                        survey_geo=@survey_geo,
                        medical_handle=@medical_handle,
                        quality_guarantee=@quality_guarantee,
                        equipment=@equipment,
                        method=@method,

                        deptMaster=@deptMaster,
                        deptMaster_other=@deptMaster_other,

                        Noinhan=@Noinhan

                        WHERE id = " + id;
                }
                //insert
                else
                {
                    sql =
                        "INSERT INTO " +
                        "KehoachKSKT(Makehoach,Ngaytao,MaNV1,DiadiemNV1,MaNV2,DiadiemNV2,Tinh_id,Huyen_id,Xa_id,DVKS_id,program_id,Diadiem,KSDH_KL,KSDH_ngaybatdau,KSDH_ngayketthuc,KSDH_ghichu,THBS_KL,THBS_ngaybatdau,THBS_ngayketthuc,THBS_ghichu,IATL_KL,IATL_ngaybatdau,IATL_ngayketthuc,IATL_ghichu,XDPD_KL,XDPD_ngaybatdau,XDPD_ngayketthuc,XDPD_ghichu,KTCL_KL,KTCL_ngaybatdau,KTCL_ngayketthuc,KTCL_ghichu,DKCT_KL,DKCT_ngaybatdau,DKCT_ngayketthuc,DKCT_ghichu,THDL_KL,THDL_ngaybatdau,THDL_ngayketthuc,THDL_ghichu,NTKQ_KL,NTKQ_ngaybatdau,NTKQ_ngayketthuc,NTKQ_ghichu,LBB_KL,LBB_ngaybatdau,LBB_ngayketthuc,LBB_ghichu,NV1_KL,NV1_ngaybatdau,NV1_ngayketthuc,NV1_ghichu,TGTH_ngaybatdau,TGTH_ngayketthuc,Noinhan,geo_common,social_common,top_file,info_provided,mission_target,technical_requirement,requirement_common,document_collect,map_draw,survey_geo,medical_handle,quality_guarantee,equipment,method,deptMaster,deptMaster_other) " +
                        "VALUES(@Makehoach,@Ngaytao,@MaNV1,@DiadiemNV1,@MaNV2,@DiadiemNV2,@Tinh_id,@Huyen_id,@Xa_id,@DVKS_id,@program_id,@Diadiem,@KSDH_KL,@KSDH_ngaybatdau,@KSDH_ngayketthuc,@KSDH_ghichu,@THBS_KL,@THBS_ngaybatdau,@THBS_ngayketthuc,@THBS_ghichu,@IATL_KL,@IATL_ngaybatdau,@IATL_ngayketthuc,@IATL_ghichu,@XDPD_KL,@XDPD_ngaybatdau,@XDPD_ngayketthuc,@XDPD_ghichu,@KTCL_KL,@KTCL_ngaybatdau,@KTCL_ngayketthuc,@KTCL_ghichu,@DKCT_KL,@DKCT_ngaybatdau,@DKCT_ngayketthuc,@DKCT_ghichu,@THDL_KL,@THDL_ngaybatdau,@THDL_ngayketthuc,@THDL_ghichu,@NTKQ_KL,@NTKQ_ngaybatdau,@NTKQ_ngayketthuc,@NTKQ_ghichu,@LBB_KL,@LBB_ngaybatdau,@LBB_ngayketthuc,@LBB_ghichu,@NV1_KL,@NV1_ngaybatdau,@NV1_ngayketthuc,@NV1_ghichu,@TGTH_ngaybatdau,@TGTH_ngayketthuc,@Noinhan,@geo_common,@social_common,@top_file,@info_provided,@mission_target,@technical_requirement,@requirement_common,@document_collect,@map_draw,@survey_geo,@medical_handle,@quality_guarantee,@equipment,@method,@deptMaster,@deptMaster_other)";
                }
                SqlCommand cmd = new SqlCommand(sql, _cn.Connection as SqlConnection);
                cmd.Transaction = _cn.Transaction as SqlTransaction;

                SqlParameter Makehoach = new SqlParameter("@Makehoach", SqlDbType.NVarChar, 50);
                Makehoach.Value = txtMaKH.Text;
                cmd.Parameters.Add(Makehoach);

                SqlParameter Ngaytao = new SqlParameter("@Ngaytao", SqlDbType.Date);
                Ngaytao.Value = timeNgaytao.Value;
                cmd.Parameters.Add(Ngaytao);

                SqlParameter MaNV1 = new SqlParameter("@MaNV1", SqlDbType.NVarChar, 50);
                MaNV1.Value = tbMaNV1.Text;
                cmd.Parameters.Add(MaNV1);

                SqlParameter DiadiemNV1 = new SqlParameter("@DiadiemNV1", SqlDbType.NVarChar, 100);
                DiadiemNV1.Value = tbDiadiemNV1.Text;
                cmd.Parameters.Add(DiadiemNV1);

                SqlParameter MaNV2 = new SqlParameter("@MaNV2", SqlDbType.NVarChar, 50);
                MaNV2.Value = tbMaNV2.Text;
                cmd.Parameters.Add(MaNV2);

                SqlParameter DiadiemNV2 = new SqlParameter("@DiadiemNV2", SqlDbType.NVarChar, 100);
                DiadiemNV2.Value = tbDiadiemNV2.Text;
                cmd.Parameters.Add(DiadiemNV2);

                SqlParameter Tinh_id = new SqlParameter("@Tinh_id", SqlDbType.BigInt);
                Tinh_id.Value = comboBox_Tinh.SelectedValue;
                cmd.Parameters.Add(Tinh_id);

                SqlParameter Huyen_id = new SqlParameter("@Huyen_id", SqlDbType.BigInt);
                Huyen_id.Value = comboBox_Huyen.SelectedValue;
                cmd.Parameters.Add(Huyen_id);

                SqlParameter Xa_id = new SqlParameter("@Xa_id", SqlDbType.BigInt);
                Xa_id.Value = comboBox_Xa.SelectedValue;
                cmd.Parameters.Add(Xa_id);

                SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                DVKS_id.Value = cbDVKS.SelectedValue;
                cmd.Parameters.Add(DVKS_id);

                SqlParameter program_id = new SqlParameter("@program_id", SqlDbType.BigInt);
                program_id.Value = comboBox_TenDA.SelectedValue;
                cmd.Parameters.Add(program_id);

                SqlParameter Diadiem = new SqlParameter("@Diadiem", SqlDbType.NVarChar, 255);
                Diadiem.Value = tbDiadiem.Text;
                cmd.Parameters.Add(Diadiem);

                SqlParameter KSDH_KL = new SqlParameter("@KSDH_KL", SqlDbType.Int);
                KSDH_KL.Value = nudKSDH_KL.Value;
                cmd.Parameters.Add(KSDH_KL);

                SqlParameter KSDH_ngaybatdau = new SqlParameter("@KSDH_ngaybatdau", SqlDbType.Date);
                KSDH_ngaybatdau.Value = dtpKSDH_ngaybatdau.Value;
                cmd.Parameters.Add(KSDH_ngaybatdau);

                SqlParameter KSDH_ngayketthuc = new SqlParameter("@KSDH_ngayketthuc", SqlDbType.Date);
                KSDH_ngayketthuc.Value = dtpKSDH_ngayketthuc.Value;
                cmd.Parameters.Add(KSDH_ngayketthuc);

                SqlParameter KSDH_ghichu = new SqlParameter("@KSDH_ghichu", SqlDbType.NVarChar, 255);
                KSDH_ghichu.Value = tbKSDH_ghichu.Text;
                cmd.Parameters.Add(KSDH_ghichu);

                SqlParameter THBS_KL = new SqlParameter("@THBS_KL", SqlDbType.Int);
                THBS_KL.Value = nudTHBS_KL.Value;
                cmd.Parameters.Add(THBS_KL);

                SqlParameter THBS_ngaybatdau = new SqlParameter("@THBS_ngaybatdau", SqlDbType.Date);
                THBS_ngaybatdau.Value = dtpTHBS_ngaybatdau.Value;
                cmd.Parameters.Add(THBS_ngaybatdau);

                SqlParameter THBS_ngayketthuc = new SqlParameter("@THBS_ngayketthuc", SqlDbType.Date);
                THBS_ngayketthuc.Value = dtpTHBS_ngayketthuc.Value;
                cmd.Parameters.Add(THBS_ngayketthuc);

                SqlParameter THBS_ghichu = new SqlParameter("@THBS_ghichu", SqlDbType.NVarChar, 255);
                THBS_ghichu.Value = tbTHBS_ghichu.Text;
                cmd.Parameters.Add(THBS_ghichu);

                SqlParameter IATL_KL = new SqlParameter("@IATL_KL", SqlDbType.Int);
                IATL_KL.Value = nudIATL_KL.Value;
                cmd.Parameters.Add(IATL_KL);

                SqlParameter IATL_ngaybatdau = new SqlParameter("@IATL_ngaybatdau", SqlDbType.Date);
                IATL_ngaybatdau.Value = dtpIATL_ngaybatdau.Value;
                cmd.Parameters.Add(IATL_ngaybatdau);

                SqlParameter IATL_ngayketthuc = new SqlParameter("@IATL_ngayketthuc", SqlDbType.Date);
                IATL_ngayketthuc.Value = dtpIATL_ngayketthuc.Value;
                cmd.Parameters.Add(IATL_ngayketthuc);

                SqlParameter IATL_ghichu = new SqlParameter("@IATL_ghichu", SqlDbType.NVarChar, 255);
                IATL_ghichu.Value = tbIATL_ghichu.Text;
                cmd.Parameters.Add(IATL_ghichu);

                SqlParameter XDPD_KL = new SqlParameter("@XDPD_KL", SqlDbType.Int);
                XDPD_KL.Value = nudXDPD_KL.Value;
                cmd.Parameters.Add(XDPD_KL);

                SqlParameter XDPD_ngaybatdau = new SqlParameter("@XDPD_ngaybatdau", SqlDbType.Date);
                XDPD_ngaybatdau.Value = dtpXDPD_ngaybatdau.Value;
                cmd.Parameters.Add(XDPD_ngaybatdau);

                SqlParameter XDPD_ngayketthuc = new SqlParameter("@XDPD_ngayketthuc", SqlDbType.Date);
                XDPD_ngayketthuc.Value = dtpXDPD_ngayketthuc.Value;
                cmd.Parameters.Add(XDPD_ngayketthuc);

                SqlParameter XDPD_ghichu = new SqlParameter("@XDPD_ghichu", SqlDbType.NVarChar, 255);
                XDPD_ghichu.Value = tbXDPD_ghichu.Text;
                cmd.Parameters.Add(XDPD_ghichu);

                SqlParameter KTCL_KL = new SqlParameter("@KTCL_KL", SqlDbType.Int);
                KTCL_KL.Value = nudKTCL_KL.Value;
                cmd.Parameters.Add(KTCL_KL);

                SqlParameter KTCL_ngaybatdau = new SqlParameter("@KTCL_ngaybatdau", SqlDbType.Date);
                KTCL_ngaybatdau.Value = dtpKTCL_ngaybatdau.Value;
                cmd.Parameters.Add(KTCL_ngaybatdau);

                SqlParameter KTCL_ngayketthuc = new SqlParameter("@KTCL_ngayketthuc", SqlDbType.Date);
                KTCL_ngayketthuc.Value = dtpKTCL_ngayketthuc.Value;
                cmd.Parameters.Add(KTCL_ngayketthuc);

                SqlParameter KTCL_ghichu = new SqlParameter("@KTCL_ghichu", SqlDbType.NVarChar, 255);
                KTCL_ghichu.Value = tbKTCL_ghichu.Text;
                cmd.Parameters.Add(KTCL_ghichu);

                SqlParameter DKCT_KL = new SqlParameter("@DKCT_KL", SqlDbType.Int);
                DKCT_KL.Value = nudDKCT_KL.Value;
                cmd.Parameters.Add(DKCT_KL);

                SqlParameter DKCT_ngaybatdau = new SqlParameter("@DKCT_ngaybatdau", SqlDbType.Date);
                DKCT_ngaybatdau.Value = dtpDKCT_ngaybatdau.Value;
                cmd.Parameters.Add(DKCT_ngaybatdau);

                SqlParameter DKCT_ngayketthuc = new SqlParameter("@DKCT_ngayketthuc", SqlDbType.Date);
                DKCT_ngayketthuc.Value = dtpDKCT_ngayketthuc.Value;
                cmd.Parameters.Add(DKCT_ngayketthuc);

                SqlParameter DKCT_ghichu = new SqlParameter("@DKCT_ghichu", SqlDbType.NVarChar, 255);
                DKCT_ghichu.Value = tbDKCT_ghichu.Text;
                cmd.Parameters.Add(DKCT_ghichu);

                SqlParameter THDL_KL = new SqlParameter("@THDL_KL", SqlDbType.Int);
                THDL_KL.Value = nudTHDL_KL.Value;
                cmd.Parameters.Add(THDL_KL);

                SqlParameter THDL_ngaybatdau = new SqlParameter("@THDL_ngaybatdau", SqlDbType.Date);
                THDL_ngaybatdau.Value = dtpTHDL_ngaybatdau.Value;
                cmd.Parameters.Add(THDL_ngaybatdau);

                SqlParameter THDL_ngayketthuc = new SqlParameter("@THDL_ngayketthuc", SqlDbType.Date);
                THDL_ngayketthuc.Value = dtpTHDL_ngayketthuc.Value;
                cmd.Parameters.Add(THDL_ngayketthuc);

                SqlParameter THDL_ghichu = new SqlParameter("@THDL_ghichu", SqlDbType.NVarChar, 255);
                THDL_ghichu.Value = tbTHDL_ghichu.Text;
                cmd.Parameters.Add(THDL_ghichu);

                SqlParameter NTKQ_KL = new SqlParameter("@NTKQ_KL", SqlDbType.Int);
                NTKQ_KL.Value = nudNTKQ_KL.Value;
                cmd.Parameters.Add(NTKQ_KL);

                SqlParameter NTKQ_ngaybatdau = new SqlParameter("@NTKQ_ngaybatdau", SqlDbType.Date);
                NTKQ_ngaybatdau.Value = dtpNTKQ_ngaybatdau.Value;
                cmd.Parameters.Add(NTKQ_ngaybatdau);

                SqlParameter NTKQ_ngayketthuc = new SqlParameter("@NTKQ_ngayketthuc", SqlDbType.Date);
                NTKQ_ngayketthuc.Value = dtpNTKQ_ngayketthuc.Value;
                cmd.Parameters.Add(NTKQ_ngayketthuc);

                SqlParameter NTKQ_ghichu = new SqlParameter("@NTKQ_ghichu", SqlDbType.NVarChar, 255);
                NTKQ_ghichu.Value = tbNTKQ_ghichu.Text;
                cmd.Parameters.Add(NTKQ_ghichu);

                SqlParameter LBB_KL = new SqlParameter("@LBB_KL", SqlDbType.Int);
                LBB_KL.Value = nudLBB_KL.Value;
                cmd.Parameters.Add(LBB_KL);

                SqlParameter LBB_ngaybatdau = new SqlParameter("@LBB_ngaybatdau", SqlDbType.Date);
                LBB_ngaybatdau.Value = dtpLBB_ngaybatdau.Value;
                cmd.Parameters.Add(LBB_ngaybatdau);

                SqlParameter LBB_ngayketthuc = new SqlParameter("@LBB_ngayketthuc", SqlDbType.Date);
                LBB_ngayketthuc.Value = dtpLBB_ngayketthuc.Value;
                cmd.Parameters.Add(LBB_ngayketthuc);

                SqlParameter LBB_ghichu = new SqlParameter("@LBB_ghichu", SqlDbType.NVarChar, 255);
                LBB_ghichu.Value = tbLBB_ghichu.Text;
                cmd.Parameters.Add(LBB_ghichu);

                SqlParameter NV1_KL = new SqlParameter("@NV1_KL", SqlDbType.Float);
                NV1_KL.Value = nudNV1_KL.Value;
                cmd.Parameters.Add(NV1_KL);

                SqlParameter NV1_ngaybatdau = new SqlParameter("@NV1_ngaybatdau", SqlDbType.Date);
                NV1_ngaybatdau.Value = dtpNV1_ngaybatdau.Value;
                cmd.Parameters.Add(NV1_ngaybatdau);

                SqlParameter NV1_ngayketthuc = new SqlParameter("@NV1_ngayketthuc", SqlDbType.Date);
                NV1_ngayketthuc.Value = dtpNV1_ngayketthuc.Value;
                cmd.Parameters.Add(NV1_ngayketthuc);

                SqlParameter NV1_ghichu = new SqlParameter("@NV1_ghichu", SqlDbType.NVarChar, 255);
                NV1_ghichu.Value = tbNV1_ghichu.Text;
                cmd.Parameters.Add(NV1_ghichu);

                SqlParameter TGTH_ngaybatdau = new SqlParameter("@TGTH_ngaybatdau", SqlDbType.Date);
                TGTH_ngaybatdau.Value = dtpTGTH_ngaybatdau.Value;
                cmd.Parameters.Add(TGTH_ngaybatdau);

                SqlParameter TGTH_ngayketthuc = new SqlParameter("@TGTH_ngayketthuc", SqlDbType.Date);
                TGTH_ngayketthuc.Value = dtpTGTH_ngayketthuc.Value;
                cmd.Parameters.Add(TGTH_ngayketthuc);

                SqlParameter Noinhan = new SqlParameter("@Noinhan", SqlDbType.NVarChar, 1000);
                Noinhan.Value = rtbNoinhan.Text;
                cmd.Parameters.Add(Noinhan);

                SqlParameter geo_common = new SqlParameter("@geo_common", SqlDbType.NVarChar, 1000);
                geo_common.Value = rtbGeoCommon.Text;
                cmd.Parameters.Add(geo_common);

                SqlParameter social_common = new SqlParameter("@social_common", SqlDbType.NVarChar, 1000);
                social_common.Value = rtbSocialCommon.Text;
                cmd.Parameters.Add(social_common);

                SqlParameter top_file = new SqlParameter("@top_file", SqlDbType.NVarChar, 1000);
                top_file.Value = tbDoc_file.Text;
                cmd.Parameters.Add(top_file);

                SqlParameter info_provided = new SqlParameter("@info_provided", SqlDbType.NVarChar, 1000);
                info_provided.Value = rtbInfoProvided.Text;
                cmd.Parameters.Add(info_provided);

                SqlParameter mission_target = new SqlParameter("@mission_target", SqlDbType.NVarChar, 1000);
                mission_target.Value = rtbMissionTarget.Text;
                cmd.Parameters.Add(mission_target);

                SqlParameter technical_requirement = new SqlParameter("@technical_requirement", SqlDbType.NVarChar, 1000);
                technical_requirement.Value = rtbTechnicalRequirement.Text;
                cmd.Parameters.Add(technical_requirement);

                SqlParameter requirement_common = new SqlParameter("@requirement_common", SqlDbType.NVarChar, 1000);
                requirement_common.Value = rtbRequirementCommon.Text;
                cmd.Parameters.Add(requirement_common);

                SqlParameter document_collect = new SqlParameter("@document_collect", SqlDbType.NVarChar, 1000);
                document_collect.Value = rtbDocumentCollect.Text;
                cmd.Parameters.Add(document_collect);

                SqlParameter map_draw = new SqlParameter("@map_draw", SqlDbType.NVarChar, 1000);
                map_draw.Value = rtbMapDraw.Text;
                cmd.Parameters.Add(map_draw);

                SqlParameter survey_geo = new SqlParameter("@survey_geo", SqlDbType.NVarChar, 1000);
                survey_geo.Value = rtbSurveyGeo.Text;
                cmd.Parameters.Add(survey_geo);

                SqlParameter medical_handle = new SqlParameter("@medical_handle", SqlDbType.NVarChar, 1000);
                medical_handle.Value = rtbMedicalHandle.Text;
                cmd.Parameters.Add(medical_handle);

                SqlParameter quality_guarantee = new SqlParameter("@quality_guarantee", SqlDbType.NVarChar, 1000);
                quality_guarantee.Value = rtbQualityGuarantee.Text;
                cmd.Parameters.Add(quality_guarantee);

                SqlParameter equipment = new SqlParameter("@equipment", SqlDbType.NVarChar, 1000);
                equipment.Value = rtbEquipment.Text;
                cmd.Parameters.Add(equipment);

                SqlParameter method = new SqlParameter("@method", SqlDbType.NVarChar, 1000);
                method.Value = rtbMethod.Text;
                cmd.Parameters.Add(method);

                SqlParameter deptMaster = new SqlParameter("@deptMaster", SqlDbType.BigInt);
                deptMaster.Value = cbDeptMaster.SelectedValue == null ? -1 : cbDeptMaster.SelectedValue;
                cmd.Parameters.Add(deptMaster);

                SqlParameter deptMaster_other = new SqlParameter("@deptMaster_other", SqlDbType.NVarChar, 1000);
                deptMaster_other.Value = tbDeptMasterOther.Text;
                cmd.Parameters.Add(deptMaster_other);

                int temp = 0;
                temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(Exception ex)
            {

                return false;
            }
            
        }

        private void comboBox_TenDA_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_TenDA.SelectedValue is long)
            {
                UtilsDatabase.LoadDVKS(cbDVKS, (long)comboBox_TenDA.SelectedValue);
            }
            LoadCBStaff(cbDeptMaster);
        }

        private void comboBox_TenDA_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_TenDA.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
                return;
            }
            if ((long)comboBox_TenDA.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_TenDA, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
            }
        }

        private void comboBox_Tinh_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox_Tinh.SelectedValue is long)
            {
                UtilsDatabase.LoadCBHuyen(comboBox_Huyen, (long)comboBox_Tinh.SelectedValue);
            }
        }

        private void comboBox_Huyen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_Huyen.SelectedValue is long)
            {
                UtilsDatabase.LoadCBXa(comboBox_Xa, (long)comboBox_Huyen.SelectedValue);
            }
        }

        private void btOpentbDoc_file_Click(object sender, EventArgs e)
        {
            //if (!(comboBox_TenDA.SelectedValue is long))
            //{
            //    MessageBox.Show("Bạn chưa chọn dự án");
            //    return;
            //}
            //long idDA = (long)comboBox_TenDA.SelectedValue;
            string filePath = AppUtils.OpenFileDialogCopy(AppUtils.ReportFolder);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDoc_file.Text = filePath;
        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_Tinh.SelectedValue == null)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Tinh, "Chưa chọn tỉnh");
            }
            else if((long)comboBox_Tinh.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Tinh, "");
            }
            else
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Tinh, "Chưa chọn tỉnh");
            }
        }

        private void comboBox_Huyen_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_Huyen.SelectedValue == null)
            {
                e.Cancel = true;
                //comboBox_Huyen.Focus();
                errorProvider1.SetError(comboBox_Huyen, "Chưa chọn huyện");
            }
            else if ((long)comboBox_Huyen.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Huyen, "");
            }
            else
            {
                e.Cancel = true;
                //comboBox_Huyen.Focus();
                errorProvider1.SetError(comboBox_Huyen, "Chưa chọn huyện");
            }
        }

        private void comboBox_Xa_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_Xa.SelectedValue == null)
            {
                e.Cancel = true;
                //comboBox_Xa.Focus();
                errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            }
            else if ((long)comboBox_Xa.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Xa, "");
            }
            else
            {
                e.Cancel = true;
                //comboBox_Xa.Focus();
                errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            }
        }

        private void LoadFiles()
        {
            SqlConnection cn = _cn.Connection as SqlConnection;
            System.Data.DataTable datatable = new System.Data.DataTable();
            dgvFile.Rows.Clear();
            string sql =
                @"SELECT 
                tbl.id as id, 
                tbl.file_type as file_type,
                tbl.file_name as file_name,
                tbl.khks_id as khks_id 
                FROM KehoachKSKT_File tbl
                WHERE tbl.khks_id = " + id;
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, cn);
            sqlAdapter.Fill(datatable);
            if (datatable.Rows.Count != 0)
            {
                int indexRow = 1;
                foreach (DataRow dr in datatable.Rows)
                {
                    long id = long.Parse(dr["id"].ToString());
                    long file_type = long.Parse(dr["file_type"].ToString());
                    string file_name = dr["file_name"].ToString();
                    long khks_id = long.Parse(dr["khks_id"].ToString());

                    dgvFile.Rows.Add(indexRow, ThemFile.file_types[file_type - 1], file_name);
                    KHKSKT_File file = new KHKSKT_File();
                    file.file_name = file_name;
                    file.file_type = file_type;
                    dgvFile.Rows[indexRow - 1].Tag = file;

                    indexRow++;
                }
            }
        }

        private void btnThemFile_Click(object sender, EventArgs e)
        {
            ThemFile frm = new ThemFile(dgvFile);
            frm.Text = "THÊM MỚI FILE ĐÍNH KÈM";
            frm.ShowDialog();
            //LoadFiles();
        }

        private void dgvFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            //KHKSKT_File file = (KHKSKT_File)dgvFile.Rows[e.RowIndex].Tag;
            if (e.ColumnIndex == cotSua.Index)
            {
                ThemFile frm = new ThemFile(dgvFile.Rows[e.RowIndex]);
                frm.Text = "CHỈNH SỬA FILE ĐÍNH KÈM";
                frm.ShowDialog();
                //LoadFiles();
            }
            else if (e.ColumnIndex == cotXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                for (int i = e.RowIndex + 1; i < dgvFile.Rows.Count; i++)
                {
                    dgvFile.Rows[i].Cells[0].Value = (int)dgvFile.Rows[i].Cells[0].Value - 1;
                }
                dgvFile.Rows.RemoveAt(e.RowIndex);
                
            }
            else if(e.ColumnIndex == cotFile.Index)
            {
                string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(AppUtils.ReportFolder), dgvFile.Rows[e.RowIndex].Cells[cotFile.Index].Value.ToString());
                if (System.IO.File.Exists(pathFile))
                {
                    var savePath = AppUtils.SaveFileDlg(pathFile);
                    AppUtils.CopyFile(pathFile, savePath);
                }
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            tbDoc_file.Text = "";
        }

        private void tbDoc_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(AppUtils.ReportFolder), tbDoc_file.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void cbDVKS_Validating(object sender, CancelEventArgs e)
        {
            if (cbDVKS.SelectedValue == null)
            {
                e.Cancel = true;
                //comboBox_Xa.Focus();
                errorProvider1.SetError(cbDVKS, "Chưa chọn ĐVKS");
            }
            else if ((long)cbDVKS.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(cbDVKS, "");
            }
            else
            {
                e.Cancel = true;
                //comboBox_Xa.Focus();
                errorProvider1.SetError(cbDVKS, "Chưa chọn ĐVKS");
            }
        }

        private void txtMaKH_Validating(object sender, CancelEventArgs e)
        {
            if(txtMaKH.Text.Trim() != "")
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMaKH, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMaKH, "Chưa nhập mã kế hoạch");
            }
        }

        private void cbDeptMaster_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbDeptMaster.SelectedValue is long)
            {
                if((long)cbDeptMaster.SelectedValue > 0)
                {
                    tbDeptMasterOther.Text = "";
                    tbDeptMasterOther.ReadOnly = true;
                }
                else
                {
                    tbDeptMasterOther.ReadOnly = false;
                }
            }
        }
    }
}
