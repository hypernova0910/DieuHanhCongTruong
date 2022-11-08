using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using DieuHanhCongTruong.Common;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class FrmThemmoiBaoCaoKQKV : Form
    {
        public SqlConnection _Cn = null;
        private ConnectionWithExtraInfo _cn = null;
        public int id_BSKQ = int.MinValue;
        //public int DuanId = 0;
        public int TinhId;
        public int HuyenId;
        public int XaId;
        public string timeVl = "", time2 = "";
        public string PersonId = "0";
        public FrmThemmoiBaoCaoKQKV(int _id_BSKQ = int.MinValue)
        {
            id_BSKQ = _id_BSKQ;
            _cn = UtilsDatabase._ExtraInfoConnettion;
            _Cn = _cn.Connection as SqlConnection;
            InitializeComponent();
        }

        private void LoadCBStaff(ComboBox cb)
        {
            cb.DataSource = null;
            if (cb_TenDA.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + cb_TenDA.SelectedValue, "id", "nameId");
            }

        }

        private bool InsertData()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("insert into cecm_report_survey_area (Duan,Duan_id,cecm_program_area_map_id,MaBaocaongay,DVKS_id,Hopphan,Tinh_id,Huyen_id,Xa_id,Thon,MaNV,timeBatdau,timeKetthuc,timeTongSo,Doiso,Doitruong,Ngaytao,DientichKS,NguonBangChung,SLBangChungYeuCau,isactive,SLDaKS,DTDaKS,DTKSKhacYeuCau,DTDaKTCL,DTPhaiLamLai,TinHieu,Oxanhla,Odo,Oxam,Ovang,Oxanhdatroi,TongSoOKS,SoOONhiem,SoKVONhiem,DTKhuONhiem,MucDoONhiem,TienDoTH,GiamSat,ChiHuyCT,GiamSat_id,ChiHuyCT_id,KVKhaoSat,PhotoFile,DrawFile,NguoiVe,TGVe) " +
                    "values (@Duan,@Duan_id,@cecm_program_area_map_id,@MaBaocaongay,@DVKS_id,@Hopphan,@Tinh_id,@Huyen_id,@Xa_id,@Thon,@MaNV,@timeBatdau,@timeKetthuc,@timeTongSo,@Doiso,@Doitruong,@Ngaytao,@DientichKS,@NguonBangChung,@SLBangChungYeuCau,@isactive,@SLDaKS,@DTDaKS,@DTKSKhacYeuCau,@DTDaKTCL,@DTPhaiLamLai,@TinHieu,@Oxanhla,@Odo,@Oxam,@Ovang,@Oxanhdatroi,@TongSoOKS,@SoOONhiem,@SoKVONhiem,@DTKhuONhiem,@MucDoONhiem,@TienDoTH,@GiamSat,@ChiHuyCT,@GiamSat_id,@ChiHuyCT_id,@KVKhaoSat,@PhotoFile,@DrawFile,@NguoiVe,@TGVe)", _Cn);
                cmd1.Transaction = _cn.Transaction as SqlTransaction;

                //Duan
                SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.NVarChar, 255);
                Duan.Value = cb_TenDA.Text;
                cmd1.Parameters.Add(Duan);

                //Duan_id
                SqlParameter Duan_id = new SqlParameter("@Duan_id", SqlDbType.BigInt);
                Duan_id.Value = cb_TenDA.SelectedValue;
                cmd1.Parameters.Add(Duan_id);

                //cecm_program_area_map_id
                SqlParameter cecm_program_area_map_id = new SqlParameter("@cecm_program_area_map_id", SqlDbType.BigInt);
                cecm_program_area_map_id.Value = cbKhuVucKS.SelectedValue;
                cmd1.Parameters.Add(cecm_program_area_map_id);

                //MaBaocaongay
                SqlParameter MaBaocaongay = new SqlParameter("@MaBaocaongay", SqlDbType.NVarChar, 255);
                MaBaocaongay.Value = txtMaBCngay.Text;
                cmd1.Parameters.Add(MaBaocaongay);

                //DVKS_id
                SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                DVKS_id.Value = cbDVKS.SelectedValue;
                cmd1.Parameters.Add(DVKS_id);

                //Hopphan
                SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 255);
                Hopphan.Value = txtHopphan.Text;
                cmd1.Parameters.Add(Hopphan);

                //Tinh_id
                SqlParameter Tinh_id = new SqlParameter("@Tinh_id", SqlDbType.BigInt);
                Tinh_id.Value = comboBox_Tinh.SelectedValue;
                cmd1.Parameters.Add(Tinh_id);

                //Huyen_id
                SqlParameter Huyen_id = new SqlParameter("@Huyen_id", SqlDbType.BigInt);
                Huyen_id.Value = comboBox_Huyen.SelectedValue;
                cmd1.Parameters.Add(Huyen_id);

                //[Xa_id]
                SqlParameter Xa_id = new SqlParameter("@Xa_id", SqlDbType.BigInt);
                Xa_id.Value = comboBox_Xa.SelectedValue;
                cmd1.Parameters.Add(Xa_id);
                //
                //Thon
                SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 255);
                Thon.Value = txtThon.Text;
                cmd1.Parameters.Add(Thon);

                //MaBaocaongay
                SqlParameter MaNV = new SqlParameter("@MaNV", SqlDbType.NVarChar, 255);
                MaNV.Value = txtMaNV.Text;
                cmd1.Parameters.Add(MaNV);

                //timeBatdau
                SqlParameter timeBatdau = new SqlParameter("@timeBatdau", SqlDbType.NVarChar, 255);
                timeBatdau.Value = TimeBD.Value;
                cmd1.Parameters.Add(timeBatdau);

                //timeKetthuc
                SqlParameter timeKetthuc = new SqlParameter("@timeKetthuc", SqlDbType.NVarChar, 255);
                timeKetthuc.Value = TimeKT.Value;
                cmd1.Parameters.Add(timeKetthuc);

                //timeTongSo
                SqlParameter timeTongSo = new SqlParameter("@timeTongSo", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTongNgay.Text) == false)
                {
                    timeTongSo.Value = int.Parse(tbTongNgay.Text);
                    cmd1.Parameters.Add(timeTongSo);
                }
                else
                {
                    timeTongSo.Value = 0;
                    cmd1.Parameters.Add(timeTongSo);
                }

                //[Doiso]
                SqlParameter Doiso = new SqlParameter("@Doiso", SqlDbType.NVarChar, 255);
                Doiso.Value = cbb_Doiso.Text;
                cmd1.Parameters.Add(Doiso);

                //Doitruong
                SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 255);
                Doitruong.Value = txtDoitruong.Text;
                cmd1.Parameters.Add(Doitruong);

                //MaBaocaongay
                SqlParameter Ngaytao = new SqlParameter("@Ngaytao", SqlDbType.NVarChar, 255);
                Ngaytao.Value = timeNgaytao.Value;
                cmd1.Parameters.Add(Ngaytao);

                //DientichKS
                SqlParameter DientichKS = new SqlParameter("@DientichKS", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbYeuCau.Text) == false)
                {
                    DientichKS.Value = float.Parse(tbYeuCau.Text);
                    cmd1.Parameters.Add(DientichKS);
                }
                else
                {
                    DientichKS.Value = 0;
                    cmd1.Parameters.Add(DientichKS);
                }


                //NguonBangChung
                SqlParameter NguonBangChung = new SqlParameter("@NguonBangChung", SqlDbType.NVarChar, 255);
                NguonBangChung.Value = cbNguonBC.Text;
                cmd1.Parameters.Add(NguonBangChung);

                //tbSoBC
                SqlParameter SLBangChungYeuCau = new SqlParameter("@SLBangChungYeuCau", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoBC.Text) == false)
                {
                    SLBangChungYeuCau.Value = int.Parse(tbSoBC.Text);
                    cmd1.Parameters.Add(SLBangChungYeuCau);
                }
                else
                {
                    SLBangChungYeuCau.Value = 0;
                    cmd1.Parameters.Add(SLBangChungYeuCau);
                }
                //[isactive
                SqlParameter isactive = new SqlParameter("@isactive", SqlDbType.NVarChar, 255);
                isactive.Value = cbTinhTrang.Text;
                cmd1.Parameters.Add(isactive);
                //
                //SLDaKS
                SqlParameter SLDaKS = new SqlParameter("@SLDaKS", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoNghiNgo.Text) == false)
                {
                    SLDaKS.Value = int.Parse(tbSoNghiNgo.Text);
                    cmd1.Parameters.Add(SLDaKS);
                }
                else
                {
                    SLDaKS.Value = 0;
                    cmd1.Parameters.Add(SLDaKS);
                }
                //DTDaKS
                SqlParameter DTDaKS = new SqlParameter("@DTDaKS", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDaKS.Text) == false)
                {
                    DTDaKS.Value = float.Parse(tbDaKS.Text);
                    cmd1.Parameters.Add(DTDaKS);
                }
                else
                {
                    DTDaKS.Value = 0;
                    cmd1.Parameters.Add(DTDaKS);
                }

                //DTKSKhacYeuCau
                SqlParameter DTKSKhacYeuCau = new SqlParameter("@DTKSKhacYeuCau", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTkhac.Text) == false)
                {
                    DTKSKhacYeuCau.Value = float.Parse(tbDTkhac.Text);
                    cmd1.Parameters.Add(DTKSKhacYeuCau);
                }
                else
                {
                    DTKSKhacYeuCau.Value = 0;
                    cmd1.Parameters.Add(DTKSKhacYeuCau);
                }
                //DTDaKTCL
                SqlParameter DTDaKTCL = new SqlParameter("@DTDaKTCL", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTDaKT.Text) == false)
                {
                    DTDaKTCL.Value = float.Parse(tbDTDaKT.Text);
                    cmd1.Parameters.Add(DTDaKTCL);
                }
                else
                {
                    DTDaKTCL.Value = 0;
                    cmd1.Parameters.Add(DTDaKTCL);
                }
                //DTPhaiLamLai
                SqlParameter DTPhaiLamLai = new SqlParameter("@DTPhaiLamLai", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTLamLai.Text) == false)
                {
                    DTPhaiLamLai.Value = float.Parse(tbDTLamLai.Text);
                    cmd1.Parameters.Add(DTPhaiLamLai);
                }
                else
                {
                    DTPhaiLamLai.Value = 0;
                    cmd1.Parameters.Add(DTPhaiLamLai);
                }
                //[TinHieu]
                SqlParameter TinHieu = new SqlParameter("@TinHieu", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTinHieu.Text) == false)
                {
                    TinHieu.Value = int.Parse(tbTinHieu.Text);
                    cmd1.Parameters.Add(TinHieu);
                }
                else
                {
                    TinHieu.Value = 0;
                    cmd1.Parameters.Add(TinHieu);
                }

                //Oxanhla
                SqlParameter Oxanhla = new SqlParameter("@Oxanhla", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoOXanhLa.Text) == false)
                {
                    Oxanhla.Value = int.Parse(tbSoOXanhLa.Text);
                    cmd1.Parameters.Add(Oxanhla);
                }
                else
                {
                    Oxanhla.Value = 0;
                    cmd1.Parameters.Add(Oxanhla);
                }

                //Odo
                SqlParameter Odo = new SqlParameter("@Odo", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoODo.Text) == false)
                {
                    Odo.Value = int.Parse(tbSoODo.Text);
                    cmd1.Parameters.Add(Odo);
                }
                else
                {
                    Odo.Value = 0;
                    cmd1.Parameters.Add(Odo);
                }
                //DientichKS
                SqlParameter Oxam = new SqlParameter("@Oxam", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoOXam.Text) == false)
                {
                    Oxam.Value = int.Parse(tbSoOXam.Text);
                    cmd1.Parameters.Add(Oxam);
                }
                else
                {
                    Oxam.Value = 0;
                    cmd1.Parameters.Add(Oxam);
                }
                //Ovang
                SqlParameter Ovang = new SqlParameter("@Ovang", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoOVang.Text) == false)
                {
                    Ovang.Value = int.Parse(tbSoOVang.Text);
                    cmd1.Parameters.Add(Ovang);
                }
                else
                {
                    Ovang.Value = 0;
                    cmd1.Parameters.Add(Ovang);
                }
                //Otrang
                //SqlParameter Otrang = new SqlParameter("@Otrang", SqlDbType.Int);
                //if (string.IsNullOrEmpty(tbSoOTrang.Text) == false)
                //{
                //    Otrang.Value = int.Parse(tbSoOTrang.Text);
                //    cmd1.Parameters.Add(Otrang);
                //}
                //else
                //{
                //    Otrang.Value = 0;
                //    cmd1.Parameters.Add(Otrang);
                //}
                //[Oxanhdatroi
                SqlParameter Oxanhdatroi = new SqlParameter("@Oxanhdatroi", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbOXanhDaTroi.Text) == false)
                {
                    Oxanhdatroi.Value = int.Parse(tbOXanhDaTroi.Text);
                    cmd1.Parameters.Add(Oxanhdatroi);
                }
                else
                {
                    Oxanhdatroi.Value = 0;
                    cmd1.Parameters.Add(Oxanhdatroi);
                }
                //
                //TongSoOKS
                SqlParameter TongSoOKS = new SqlParameter("@TongSoOKS", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTongSoO.Text) == false)
                {
                    TongSoOKS.Value = int.Parse(tbTongSoO.Text);
                    cmd1.Parameters.Add(TongSoOKS);
                }
                else
                {
                    TongSoOKS.Value = 0;
                    cmd1.Parameters.Add(TongSoOKS);
                }
                //SoOONhiem
                SqlParameter SoOONhiem = new SqlParameter("@SoOONhiem", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbOKDNhiem.Text) == false)
                {
                    SoOONhiem.Value = int.Parse(tbOKDNhiem.Text);
                    cmd1.Parameters.Add(SoOONhiem);
                }
                else
                {
                    SoOONhiem.Value = 0;
                    cmd1.Parameters.Add(SoOONhiem);
                }
                //SoKVONhiem
                SqlParameter SoKVONhiem = new SqlParameter("@SoKVONhiem", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbKVONhiem.Text) == false)
                {
                    SoKVONhiem.Value = int.Parse(tbKVONhiem.Text);
                    cmd1.Parameters.Add(SoKVONhiem);
                }
                else
                {
                    SoKVONhiem.Value = 0;
                    cmd1.Parameters.Add(SoKVONhiem);
                }
                //MucDoONhiem
                SqlParameter MucDoONhiem = new SqlParameter("@MucDoONhiem", SqlDbType.NVarChar, 255);
                MucDoONhiem.Value = cbMucDoNhiem.Text;
                cmd1.Parameters.Add(MucDoONhiem);

                //DTKhuONhiem
                SqlParameter DTKhuONhiem = new SqlParameter("@DTKhuONhiem", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTKDNhiem.Text) == false)
                {
                    DTKhuONhiem.Value = float.Parse(tbDTKDNhiem.Text);
                    cmd1.Parameters.Add(DTKhuONhiem);
                }
                else
                {
                    DTKhuONhiem.Value = 0;
                    cmd1.Parameters.Add(DTKhuONhiem);
                }
                //[TienDoTH]
                SqlParameter TienDoTH = new SqlParameter("@TienDoTH", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTienDo.Text) == false)
                {
                    TienDoTH.Value = int.Parse(tbTienDo.Text);
                    cmd1.Parameters.Add(TienDoTH);
                }
                else
                {
                    TienDoTH.Value = 0;
                    cmd1.Parameters.Add(TienDoTH);
                }

                //GiamSat
                SqlParameter GiamSat = new SqlParameter("@GiamSat", SqlDbType.NVarChar, 255);
                GiamSat.Value = tbGiamSat.Text;
                cmd1.Parameters.Add(GiamSat);

                //ChiHuyCT
                SqlParameter ChiHuyCT = new SqlParameter("@ChiHuyCT", SqlDbType.NVarChar, 255);
                ChiHuyCT.Value = tbChiHuyCT.Text;
                cmd1.Parameters.Add(ChiHuyCT);

                //ChiHuyCT_id
                SqlParameter ChiHuyCT_id = new SqlParameter("@ChiHuyCT_id", SqlDbType.BigInt);
                ChiHuyCT_id.Value = cbChiHuyCT.SelectedValue;
                cmd1.Parameters.Add(ChiHuyCT_id);

                //GiamSat_id
                SqlParameter GiamSat_id = new SqlParameter("@GiamSat_id", SqlDbType.BigInt);
                GiamSat_id.Value = cbGiamSat.SelectedValue;
                cmd1.Parameters.Add(GiamSat_id);

                //KVKhaoSat
                SqlParameter KVKhaoSat = new SqlParameter("@KVKhaoSat", SqlDbType.NVarChar, 255);
                KVKhaoSat.Value = cbKhuVucKS.Text;
                cmd1.Parameters.Add(KVKhaoSat);

                SqlParameter PhotoFile = new SqlParameter("@PhotoFile", SqlDbType.NVarChar, 255);
                PhotoFile.Value = lblImage.Text;
                cmd1.Parameters.Add(PhotoFile);

                //KVKhaoSat
                SqlParameter DrawFile = new SqlParameter("@DrawFile", SqlDbType.NVarChar, 255);
                DrawFile.Value = lbSodo.Text;
                cmd1.Parameters.Add(DrawFile);

                //ChiHuyCT
                SqlParameter NguoiVe = new SqlParameter("@NguoiVe", SqlDbType.NVarChar, 255);
                NguoiVe.Value = tbNguoive.Text;
                cmd1.Parameters.Add(NguoiVe);

                //KVKhaoSat
                SqlParameter TGVe = new SqlParameter("@TGVe", SqlDbType.NVarChar, 255);
                TGVe.Value = timeTGVe.Value;
                cmd1.Parameters.Add(TGVe);

                int temp1 = 0;
                temp1 = cmd1.ExecuteNonQuery();
                if (temp1 > 0)
                {
                    //MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                    //this.DialogResult = DialogResult.OK;
                    return true;
                }
                else
                {
                    //MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                    //this.DialogResult = DialogResult.None;
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return false;
            }
        }
        private bool UpdateInfomation(int dem)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand(string.Format(
                    "update cecm_report_survey_area set " +
                    "Duan = @Duan , " +
                    "Duan_id = @Duan_id, " +
                    "cecm_program_area_map_id = @cecm_program_area_map_id, " +
                    "MaBaocaongay = @MaBaocaongay  , " +
                    "DVKS_id = @DVKS_id  , " +
                    "Hopphan = @Hopphan  , " +
                    "Tinh_id=@Tinh_id  , " +
                    "Huyen_id=@Huyen_id  , " +
                    "Xa_id = @Xa_id, " +
                    "Thon = @Thon, " +
                    "MaNV = @MaNV, " +
                    "timeBatdau = @timeBatdau, " +
                    "timeKetthuc = @timeKetthuc, " +
                    "timeTongSo = @timeTongSo, " +
                    "Doiso = @Doiso, " +
                    "Doitruong = @Doitruong , " +
                    "Ngaytao = @Ngaytao, " +
                    "DientichKS = @DientichKS, " +
                    "NguonBangChung = @NguonBangChung, " +
                    "SLBangChungYeuCau = @SLBangChungYeuCau, " +
                    "isactive = @isactive, " +
                    "SLDaKS = @SLDaKS, " +
                    "DTDaKS = @DTDaKS, " +
                    "DTKSKhacYeuCau = @DTKSKhacYeuCau, " +
                    "DTDaKTCL = @DTDaKTCL, " +
                    "DTPhaiLamLai = @DTPhaiLamLai, " +
                    "TinHieu = @TinHieu, " +
                    "Oxanhla = @Oxanhla, " +
                    "Odo = @Odo, " +
                    "Oxam = @Oxam, " +
                    "Ovang = @Ovang, " +
                    //"Otrang = @Otrang, " +
                    "Oxanhdatroi = @Oxanhdatroi, " +
                    "TongSoOKS = @TongSoOKS, " +
                    "SoOONhiem = @SoOONhiem, " +
                    "SoKVONhiem = @SoKVONhiem, " +
                    "DTKhuONhiem = @DTKhuONhiem, " +
                    "DTKhongONhiem = @DTKhongONhiem, " +
                    "MucDoONhiem = @MucDoONhiem, " +
                    "TienDoTH = @TienDoTH, " +
                    "GiamSat = @GiamSat, " +
                    "ChiHuyCT = @ChiHuyCT, " +
                    "GiamSat_id = @GiamSat_id, " +
                    "ChiHuyCT_id = @ChiHuyCT_id, " +
                    "KVKhaoSat = @KVKhaoSat," +
                    "PhotoFile = @PhotoFile," +
                    "DrawFile=@DrawFile," +
                    "NguoiVe=@NguoiVe," +
                    "TGVe=@TGVe " +
                    "where cecm_report_survey_area.id = {0}", dem), _Cn);
                cmd1.Transaction = _cn.Transaction as SqlTransaction;

                //Duan
                SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.NVarChar, 255);
                Duan.Value = cb_TenDA.Text;
                cmd1.Parameters.Add(Duan);

                //Duan_id
                SqlParameter Duan_id = new SqlParameter("@Duan_id", SqlDbType.BigInt);
                Duan_id.Value = cb_TenDA.SelectedValue;
                cmd1.Parameters.Add(Duan_id);

                //cecm_program_area_map_id
                SqlParameter cecm_program_area_map_id = new SqlParameter("@cecm_program_area_map_id", SqlDbType.BigInt);
                cecm_program_area_map_id.Value = cbKhuVucKS.SelectedValue;
                cmd1.Parameters.Add(cecm_program_area_map_id);

                //MaBaocaongay
                SqlParameter MaBaocaongay = new SqlParameter("@MaBaocaongay", SqlDbType.NVarChar, 255);
                MaBaocaongay.Value = txtMaBCngay.Text;
                cmd1.Parameters.Add(MaBaocaongay);

                //DVKS_id
                SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                DVKS_id.Value = cbDVKS.SelectedValue;
                cmd1.Parameters.Add(DVKS_id);

                //Hopphan
                SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 255);
                Hopphan.Value = txtHopphan.Text;
                cmd1.Parameters.Add(Hopphan);

                //Tinh_id
                SqlParameter Tinh_id = new SqlParameter("@Tinh_id", SqlDbType.BigInt);
                Tinh_id.Value = comboBox_Tinh.SelectedValue;
                cmd1.Parameters.Add(Tinh_id);

                //Huyen_id
                SqlParameter Huyen_id = new SqlParameter("@Huyen_id", SqlDbType.BigInt);
                Huyen_id.Value = comboBox_Huyen.SelectedValue;
                cmd1.Parameters.Add(Huyen_id);

                //[Xa_id]
                SqlParameter Xa_id = new SqlParameter("@Xa_id", SqlDbType.BigInt);
                Xa_id.Value = comboBox_Xa.SelectedValue;
                cmd1.Parameters.Add(Xa_id);
                //
                //Thon
                SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 255);
                Thon.Value = txtThon.Text;
                cmd1.Parameters.Add(Thon);

                //MaBaocaongay
                SqlParameter MaNV = new SqlParameter("@MaNV", SqlDbType.NVarChar, 255);
                MaNV.Value = txtMaNV.Text;
                cmd1.Parameters.Add(MaNV);

                //timeBatdau
                SqlParameter timeBatdau = new SqlParameter("@timeBatdau", SqlDbType.NVarChar, 255);
                timeBatdau.Value = TimeBD.Value;
                cmd1.Parameters.Add(timeBatdau);

                //timeKetthuc
                SqlParameter timeKetthuc = new SqlParameter("@timeKetthuc", SqlDbType.NVarChar, 255);
                timeKetthuc.Value = TimeKT.Value;
                cmd1.Parameters.Add(timeKetthuc);

                //timeTongSo
                SqlParameter timeTongSo = new SqlParameter("@timeTongSo", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTongNgay.Text) == false)
                {
                    timeTongSo.Value = int.Parse(tbTongNgay.Text);
                    cmd1.Parameters.Add(timeTongSo);
                }
                else
                {
                    timeTongSo.Value = 0;
                    cmd1.Parameters.Add(timeTongSo);
                }

                //[Doiso]
                SqlParameter Doiso = new SqlParameter("@Doiso", SqlDbType.NVarChar, 255);
                Doiso.Value = cbb_Doiso.Text;
                cmd1.Parameters.Add(Doiso);

                //Doitruong
                SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 255);
                Doitruong.Value = txtDoitruong.Text;
                cmd1.Parameters.Add(Doitruong);

                //MaBaocaongay
                SqlParameter Ngaytao = new SqlParameter("@Ngaytao", SqlDbType.NVarChar, 255);
                Ngaytao.Value = timeNgaytao.Value;
                cmd1.Parameters.Add(Ngaytao);

                //DientichKS
                SqlParameter DientichKS = new SqlParameter("@DientichKS", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbYeuCau.Text) == false)
                {
                    DientichKS.Value = float.Parse(tbYeuCau.Text);
                    cmd1.Parameters.Add(DientichKS);
                }
                else
                {
                    DientichKS.Value = 0;
                    cmd1.Parameters.Add(DientichKS);
                }


                //NguonBangChung
                SqlParameter NguonBangChung = new SqlParameter("@NguonBangChung", SqlDbType.NVarChar, 255);
                NguonBangChung.Value = cbNguonBC.Text;
                cmd1.Parameters.Add(NguonBangChung);

                //tbSoBC
                SqlParameter SLBangChungYeuCau = new SqlParameter("@SLBangChungYeuCau", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoBC.Text) == false)
                {
                    SLBangChungYeuCau.Value = int.Parse(tbSoBC.Text);
                    cmd1.Parameters.Add(SLBangChungYeuCau);
                }
                else
                {
                    SLBangChungYeuCau.Value = 0;
                    cmd1.Parameters.Add(SLBangChungYeuCau);
                }
                //[isactive
                SqlParameter isactive = new SqlParameter("@isactive", SqlDbType.NVarChar, 255);
                isactive.Value = cbTinhTrang.Text;
                cmd1.Parameters.Add(isactive);
                //
                //SLDaKS
                SqlParameter SLDaKS = new SqlParameter("@SLDaKS", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoNghiNgo.Text) == false)
                {
                    SLDaKS.Value = int.Parse(tbSoNghiNgo.Text);
                    cmd1.Parameters.Add(SLDaKS);
                }
                else
                {
                    SLDaKS.Value = 0;
                    cmd1.Parameters.Add(SLDaKS);
                }
                //DTDaKS
                SqlParameter DTDaKS = new SqlParameter("@DTDaKS", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDaKS.Text) == false)
                {
                    DTDaKS.Value = float.Parse(tbDaKS.Text);
                    cmd1.Parameters.Add(DTDaKS);
                }
                else
                {
                    DTDaKS.Value = 0;
                    cmd1.Parameters.Add(DTDaKS);
                }

                //DTKSKhacYeuCau
                SqlParameter DTKSKhacYeuCau = new SqlParameter("@DTKSKhacYeuCau", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTkhac.Text) == false)
                {
                    DTKSKhacYeuCau.Value = float.Parse(tbDTkhac.Text);
                    cmd1.Parameters.Add(DTKSKhacYeuCau);
                }
                else
                {
                    DTKSKhacYeuCau.Value = 0;
                    cmd1.Parameters.Add(DTKSKhacYeuCau);
                }
                //DTDaKTCL
                SqlParameter DTDaKTCL = new SqlParameter("@DTDaKTCL", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTDaKT.Text) == false)
                {
                    DTDaKTCL.Value = float.Parse(tbDTDaKT.Text);
                    cmd1.Parameters.Add(DTDaKTCL);
                }
                else
                {
                    DTDaKTCL.Value = 0;
                    cmd1.Parameters.Add(DTDaKTCL);
                }
                //DTPhaiLamLai
                SqlParameter DTPhaiLamLai = new SqlParameter("@DTPhaiLamLai", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTLamLai.Text) == false)
                {
                    DTPhaiLamLai.Value = float.Parse(tbDTLamLai.Text);
                    cmd1.Parameters.Add(DTPhaiLamLai);
                }
                else
                {
                    DTPhaiLamLai.Value = 0;
                    cmd1.Parameters.Add(DTPhaiLamLai);
                }
                //[TinHieu]
                SqlParameter TinHieu = new SqlParameter("@TinHieu", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTinHieu.Text) == false)
                {
                    TinHieu.Value = int.Parse(tbTinHieu.Text);
                    cmd1.Parameters.Add(TinHieu);
                }
                else
                {
                    TinHieu.Value = 0;
                    cmd1.Parameters.Add(TinHieu);
                }

                //Oxanhla
                SqlParameter Oxanhla = new SqlParameter("@Oxanhla", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoOXanhLa.Text) == false)
                {
                    Oxanhla.Value = int.Parse(tbSoOXanhLa.Text);
                    cmd1.Parameters.Add(Oxanhla);
                }
                else
                {
                    Oxanhla.Value = 0;
                    cmd1.Parameters.Add(Oxanhla);
                }

                //Odo
                SqlParameter Odo = new SqlParameter("@Odo", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoODo.Text) == false)
                {
                    Odo.Value = int.Parse(tbSoODo.Text);
                    cmd1.Parameters.Add(Odo);
                }
                else
                {
                    Odo.Value = 0;
                    cmd1.Parameters.Add(Odo);
                }
                //DientichKS
                SqlParameter Oxam = new SqlParameter("@Oxam", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoOXam.Text) == false)
                {
                    Oxam.Value = int.Parse(tbSoOXam.Text);
                    cmd1.Parameters.Add(Oxam);
                }
                else
                {
                    Oxam.Value = 0;
                    cmd1.Parameters.Add(Oxam);
                }
                //Ovang
                SqlParameter Ovang = new SqlParameter("@Ovang", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbSoOVang.Text) == false)
                {
                    Ovang.Value = int.Parse(tbSoOVang.Text);
                    cmd1.Parameters.Add(Ovang);
                }
                else
                {
                    Ovang.Value = 0;
                    cmd1.Parameters.Add(Ovang);
                }
                //Otrang
                //SqlParameter Otrang = new SqlParameter("@Otrang", SqlDbType.Int);
                //if (string.IsNullOrEmpty(tbSoOTrang.Text) == false)
                //{
                //    Otrang.Value = int.Parse(tbSoOTrang.Text);
                //    cmd1.Parameters.Add(Otrang);
                //}
                //else
                //{
                //    Otrang.Value = 0;
                //    cmd1.Parameters.Add(Otrang);
                //}
                //[Oxanhdatroi
                SqlParameter Oxanhdatroi = new SqlParameter("@Oxanhdatroi", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbOXanhDaTroi.Text) == false)
                {
                    Oxanhdatroi.Value = int.Parse(tbOXanhDaTroi.Text);
                    cmd1.Parameters.Add(Oxanhdatroi);
                }
                else
                {
                    Oxanhdatroi.Value = 0;
                    cmd1.Parameters.Add(Oxanhdatroi);
                }
                //
                //TongSoOKS
                SqlParameter TongSoOKS = new SqlParameter("@TongSoOKS", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTongSoO.Text) == false)
                {
                    TongSoOKS.Value = int.Parse(tbTongSoO.Text);
                    cmd1.Parameters.Add(TongSoOKS);
                }
                else
                {
                    TongSoOKS.Value = 0;
                    cmd1.Parameters.Add(TongSoOKS);
                }
                //SoOONhiem
                SqlParameter SoOONhiem = new SqlParameter("@SoOONhiem", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbOKDNhiem.Text) == false)
                {
                    SoOONhiem.Value = int.Parse(tbOKDNhiem.Text);
                    cmd1.Parameters.Add(SoOONhiem);
                }
                else
                {
                    SoOONhiem.Value = 0;
                    cmd1.Parameters.Add(SoOONhiem);
                }
                //SoKVONhiem
                SqlParameter SoKVONhiem = new SqlParameter("@SoKVONhiem", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbKVONhiem.Text) == false)
                {
                    SoKVONhiem.Value = int.Parse(tbKVONhiem.Text);
                    cmd1.Parameters.Add(SoKVONhiem);
                }
                else
                {
                    SoKVONhiem.Value = 0;
                    cmd1.Parameters.Add(SoKVONhiem);
                }
                //MucDoONhiem
                SqlParameter MucDoONhiem = new SqlParameter("@MucDoONhiem", SqlDbType.NVarChar, 255);
                MucDoONhiem.Value = cbMucDoNhiem.Text;
                cmd1.Parameters.Add(MucDoONhiem);

                //DTKhuONhiem
                SqlParameter DTKhuONhiem = new SqlParameter("@DTKhuONhiem", SqlDbType.Float);
                if (string.IsNullOrEmpty(tbDTKDNhiem.Text) == false)
                {
                    DTKhuONhiem.Value = float.Parse(tbDTKDNhiem.Text);
                    cmd1.Parameters.Add(DTKhuONhiem);
                }
                else
                {
                    DTKhuONhiem.Value = 0;
                    cmd1.Parameters.Add(DTKhuONhiem);
                }

                //DTKhongONhiem
                SqlParameter DTKhongONhiem = new SqlParameter("@DTKhongONhiem", SqlDbType.Float);
                DTKhongONhiem.Value = nudKhongON.Value;
                cmd1.Parameters.Add(DTKhongONhiem);

                //[TienDoTH]
                SqlParameter TienDoTH = new SqlParameter("@TienDoTH", SqlDbType.Int);
                if (string.IsNullOrEmpty(tbTienDo.Text) == false)
                {
                    TienDoTH.Value = int.Parse(tbTienDo.Text);
                    cmd1.Parameters.Add(TienDoTH);
                }
                else
                {
                    TienDoTH.Value = 0;
                    cmd1.Parameters.Add(TienDoTH);
                }
                //ChiHuyCT
                SqlParameter ChiHuyCT = new SqlParameter("@ChiHuyCT", SqlDbType.NVarChar, 255);
                ChiHuyCT.Value = tbChiHuyCT.Text;
                cmd1.Parameters.Add(ChiHuyCT);

                //GiamSat
                SqlParameter GiamSat = new SqlParameter("@GiamSat", SqlDbType.NVarChar, 255);
                GiamSat.Value = tbGiamSat.Text;
                cmd1.Parameters.Add(GiamSat);

                //ChiHuyCT_id
                SqlParameter ChiHuyCT_id = new SqlParameter("@ChiHuyCT_id", SqlDbType.BigInt);
                ChiHuyCT_id.Value = cbChiHuyCT.SelectedValue;
                cmd1.Parameters.Add(ChiHuyCT_id);

                //GiamSat_id
                SqlParameter GiamSat_id = new SqlParameter("@GiamSat_id", SqlDbType.BigInt);
                GiamSat_id.Value = cbGiamSat.SelectedValue;
                cmd1.Parameters.Add(GiamSat_id);

                //KVKhaoSat
                SqlParameter KVKhaoSat = new SqlParameter("@KVKhaoSat", SqlDbType.NVarChar, 255);
                KVKhaoSat.Value = cbKhuVucKS.Text;
                cmd1.Parameters.Add(KVKhaoSat);

                SqlParameter PhotoFile = new SqlParameter("@PhotoFile", SqlDbType.NVarChar, 255);
                PhotoFile.Value = lblImage.Text;
                cmd1.Parameters.Add(PhotoFile);

                //KVKhaoSat
                SqlParameter DrawFile = new SqlParameter("@DrawFile", SqlDbType.NVarChar, 255);
                DrawFile.Value = lbSodo.Text;
                cmd1.Parameters.Add(DrawFile);

                //ChiHuyCT
                SqlParameter NguoiVe = new SqlParameter("@NguoiVe", SqlDbType.NVarChar, 255);
                NguoiVe.Value = tbNguoive.Text;
                cmd1.Parameters.Add(NguoiVe);

                //KVKhaoSat
                SqlParameter TGVe = new SqlParameter("@TGVe", SqlDbType.NVarChar, 255);
                TGVe.Value = timeTGVe.Value;
                cmd1.Parameters.Add(TGVe);

                int temp1 = 0;
                temp1 = cmd1.ExecuteNonQuery();
                if (temp1 > 0)
                {
                    //MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                    //this.DialogResult = DialogResult.OK;
                    return true;
                }
                else
                {
                    //MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                    //this.DialogResult = DialogResult.None;
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return false;
            }
            return true;
        }

        private bool DeleteSubTable()
        {
            try
            {
                SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_report_survey_area_BMVN WHERE cecm_report_survey_area_id = " + id_BSKQ, _cn.Connection as SqlConnection);
                cmd2.Transaction = _cn.Transaction as SqlTransaction;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool UpdateBMVN()
        {
            try
            {
                foreach (DataGridViewRow dataGridViewRow in DGV_KetQuaBom.Rows)
                {
                    CecmReportSurveyAreaBMVN bmvn = dataGridViewRow.Tag as CecmReportSurveyAreaBMVN;
                    //if (id_kqks != 0),Dientichcaybui,Dientichcayto,MatdoTB,Sotinhieu=,Ghichu,Dientichtretruc,Matdothua,Matdoday,Matdo, DaxulyM2
                    //{
                    string sql =
                        "INSERT INTO cecm_report_survey_area_BMVN" +
                        "(Kyhieu, Loai, idRectangle,SL,Tinhtrang,Vido,Kinhdo,Deep,Kichthuoc, PPXuLy, cecm_report_survey_area_id) " +
                        "VALUES(@Kyhieu, @Loai, @idRectangle, @SL,@Tinhtrang,@Vido,@Kinhdo,@Deep,@Kichthuoc, @PPXuLy, @cecm_report_survey_area_id)";
                    SqlCommand cmd2 = new SqlCommand(sql, _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter Kyhieu = new SqlParameter("@Kyhieu", SqlDbType.NVarChar, 50);
                    //Vungcoso.Value = comboBox_Vungcoso.SelectedItem.ToString();
                    Kyhieu.Value = bmvn.Kyhieu != null ? bmvn.Kyhieu : "";
                    cmd2.Parameters.Add(Kyhieu);

                    SqlParameter Loai = new SqlParameter("@Loai", SqlDbType.NVarChar, 50);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    Loai.Value = bmvn.Loai != null ? bmvn.Loai : "";
                    cmd2.Parameters.Add(Loai);

                    SqlParameter idRectangle = new SqlParameter("@idRectangle", SqlDbType.BigInt);
                    idRectangle.Value = bmvn.idRectangle;
                    cmd2.Parameters.Add(idRectangle);

                    SqlParameter SL = new SqlParameter("@SL", SqlDbType.Int);
                    SL.Value = bmvn.SL;
                    cmd2.Parameters.Add(SL);

                    SqlParameter Tinhtrang = new SqlParameter("@Tinhtrang", SqlDbType.NVarChar, 50);
                    Tinhtrang.Value = bmvn.Tinhtrang != null ? bmvn.Tinhtrang : "";
                    cmd2.Parameters.Add(Tinhtrang);

                    SqlParameter Vido = new SqlParameter("@Vido", SqlDbType.Float);
                    Vido.Value = bmvn.Vido;
                    cmd2.Parameters.Add(Vido);

                    SqlParameter Kinhdo = new SqlParameter("@Kinhdo", SqlDbType.Float);
                    Kinhdo.Value = bmvn.Kinhdo;
                    cmd2.Parameters.Add(Kinhdo);

                    SqlParameter Deep = new SqlParameter("@Deep", SqlDbType.Float);
                    Deep.Value = bmvn.Deep;
                    cmd2.Parameters.Add(Deep);

                    SqlParameter Kichthuoc = new SqlParameter("@Kichthuoc", SqlDbType.Float);
                    Kichthuoc.Value = bmvn.Kichthuoc;
                    cmd2.Parameters.Add(Kichthuoc);

                    SqlParameter PPXuLy = new SqlParameter("@PPXuLy", SqlDbType.NVarChar, 50);
                    PPXuLy.Value = bmvn.PPXuLy != null ? bmvn.PPXuLy : "";
                    cmd2.Parameters.Add(PPXuLy);

                    SqlParameter cecm_report_survey_area_id = new SqlParameter("@cecm_report_survey_area_id", SqlDbType.BigInt);
                    cecm_report_survey_area_id.Value = id_BSKQ;
                    cmd2.Parameters.Add(cecm_report_survey_area_id);

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

        private void LoadCBDA()
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, name FROM cecm_programData", _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            //comboBox_TenDA.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProgram.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    comboBox_TenDA.Items.Add(dr["name"].ToString());
            //}
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["name"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            cb_TenDA.DataSource = datatableProgram;
            cb_TenDA.ValueMember = "id";
            cb_TenDA.DisplayMember = "name";
        }

        private void LoadDVKS()
        {
            cbDVKS.DataSource = null;
            if (cb_TenDA.SelectedValue is long)
            {
                SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter(
                    string.Format(@"SELECT id, name 
                    FROM cert_department 
                    WHERE id_web in (SELECT dept_id_web FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0}) 
                    or id in (SELECT dept_id FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0})", cb_TenDA.SelectedValue, TableName.KHAO_SAT), _cn.Connection as SqlConnection);
                //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                System.Data.DataTable datatableStaff = new System.Data.DataTable();
                sqlAdapterProgram.Fill(datatableStaff);
                DataRow drStaff = datatableStaff.NewRow();
                drStaff["id"] = -1;
                drStaff["name"] = "Chưa chọn";
                datatableStaff.Rows.InsertAt(drStaff, 0);
                cbDVKS.DataSource = datatableStaff;
                cbDVKS.ValueMember = "id";
                cbDVKS.DisplayMember = "name";
            }
        }

        private void LoadCBTinh()
        {
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 1", _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_Tinh.DataSource = datatableProgram;
            comboBox_Tinh.ValueMember = "id";
            comboBox_Tinh.DisplayMember = "Ten";
        }

        private void LoadCBHuyen()
        {
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 2 AND parent_id = " + comboBox_Tinh.SelectedValue, _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_Huyen.DataSource = datatableProgram;
            comboBox_Huyen.ValueMember = "id";
            comboBox_Huyen.DisplayMember = "Ten";
        }

        private void LoadCBXa()
        {
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 3 AND parentiddistrict = " + comboBox_Huyen.SelectedValue, _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_Xa.DataSource = datatableProgram;
            comboBox_Xa.ValueMember = "id";
            comboBox_Xa.DisplayMember = "Ten";
        }

        private void LoadCBBoxprovince(string idprovince)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                // Set value Xa
                sqlAdapter = new SqlDataAdapter("SELECT Ten FROM cecm_provinces", _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                cbKhuVucKS.Items.Clear();
                cbKhuVucKS.Items.Add("Chọn");
                foreach (DataRow dr in datatable.Rows)
                {
                    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                        continue;

                    cbKhuVucKS.Items.Add(dr["Ten"].ToString());
                }
                if (idprovince != string.Empty)
                {
                    sqlAdapter = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_province WHERE cecm_provinces.id = {0}", idprovince), _Cn);
                    sqlCommand = new SqlCommandBuilder(sqlAdapter);
                    datatable = new System.Data.DataTable();
                    sqlAdapter.Fill(datatable);
                    foreach (DataRow dr in datatable.Rows)
                    {
                        cbKhuVucKS.SelectedIndex = cbKhuVucKS.FindStringExact(dr["Ten"].ToString());
                    }
                }
                else
                    cbKhuVucKS.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        private void LoadDataBMVN_onChange_cbDA()
        {
            if(!(cb_TenDA.SelectedValue is long))
            {
                return;
            }
            DGV_KetQuaBom.Rows.Clear();

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            string sql =
                "SELECT " +
                "Cecm_VNTerrainMinePoint.id as 'STT'," +
                "Cecm_VNTerrainMinePoint.[Kyhieu] as 'Kí hiệu'," +
                "Cecm_VNTerrainMinePoint.[Loai] as 'Loại'," +
                "Cecm_VNTerrainMinePoint.idRectangle as idRectangle," +
                "OLuoi.o_id as 'Mã ô'," +
                "Cecm_VNTerrainMinePoint.[SL] as'SL'," +
                "Cecm_VNTerrainMinePoint.XPoint as 'Kinh độ'," +
                "Cecm_VNTerrainMinePoint.YPoint as 'Vĩ độ'," +
                "Deep as 'Độ sâu'," +
                "Cecm_VNTerrainMinePoint.[Tinhtrang] as 'Tình trạng' " +
                "FROM Cecm_VNTerrainMinePoint " +
                //"left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId " +
                "left join OLuoi on OLuoi.gid = Cecm_VNTerrainMinePoint.idRectangle " +
                "where 1 = 1 " +
                "and Cecm_VNTerrainMinePoint.programId = @cecm_program_id ";
            sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
            sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "cecm_program_id",
                Value = cb_TenDA.SelectedValue,
                SqlDbType = SqlDbType.BigInt,
            });
            //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            DGV_KetQuaBom.DataSource = null;

            if (datatable.Rows.Count != 0)
            {
                int indexRow = 1;
                foreach (DataRow dr in datatable.Rows)
                {

                    var idKqks = dr["STT"].ToString();
                    var Kyhieu = dr["Kí hiệu"].ToString();
                    //var row2 = dr["Loại"].ToString();
                    var Loại = "";
                    var Ma_o = dr["Mã ô"].ToString();
                    var SL = dr["SL"].ToString();
                    var Kinhdo = dr["Kinh độ"].ToString();
                    var Vido = dr["Vĩ độ"].ToString();
                    var Deep = dr["Độ sâu"].ToString();
                    //var row8 = dr["Tình trạng"].ToString();
                    var Tinhtrang = "";

                    CecmReportSurveyAreaBMVN bmvn = new CecmReportSurveyAreaBMVN();
                    bmvn.Kyhieu = dr["Kí hiệu"].ToString();
                    //bmvn.Loai = dr["Loại"].ToString(); 
                    bmvn.idRectangle = long.Parse(dr["idRectangle"].ToString());
                    bmvn.Kinhdo = double.Parse(dr["Kinh độ"].ToString());
                    bmvn.Vido = double.Parse(dr["Vĩ độ"].ToString());
                    bmvn.Deep = double.Parse(dr["Độ sâu"].ToString());

                    DGV_KetQuaBom.Rows.Add(indexRow, Loại, Kyhieu, Ma_o, "", Deep, Kinhdo, Vido, Tinhtrang);
                    DGV_KetQuaBom.Rows[indexRow - 1].Tag = bmvn;

                    indexRow++;
                }

            }
        }

        private void LoadDataBMVN()
        {
            try
            {
                //DGV_KetQuaBom.DataSource = null;
                //DGV_KetQuaBom.Columns.Clear();
                DGV_KetQuaBom.Rows.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                string sql = "Select tbl.*, ol.o_id as ol_idST " +
                    "FROM cecm_report_survey_area_BMVN tbl " +
                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                    "WHERE tbl.cecm_report_survey_area_id = " + id_BSKQ;
                sqlAdapter = new SqlDataAdapter(sql, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                DataTable datatable = new DataTable();
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idKqks = indexRow;
                        var Kyhieu = dr["Kyhieu"].ToString();
                        var Loai = dr["Loai"].ToString();
                        var ol_idST = dr["ol_idST"].ToString();
                        //var Kichthuoc = dr["Kichthuoc"].ToString();
                        var SL = dr["SL"].ToString();
                        var Kinhdo = dr["Kinhdo"].ToString();
                        var Vido = dr["Vido"].ToString();
                        var Deep = dr["Deep"].ToString();
                        var Tinhtrang = dr["Tinhtrang"].ToString();

                        CecmReportSurveyAreaBMVN bmvn = new CecmReportSurveyAreaBMVN();
                        bmvn.Kyhieu = dr["Kyhieu"].ToString();
                        bmvn.Loai = dr["Loai"].ToString();
                        bmvn.idRectangle = long.Parse(dr["idRectangle"].ToString());
                        bmvn.SL = int.Parse(dr["SL"].ToString());
                        bmvn.Kinhdo = double.Parse(dr["Kinhdo"].ToString());
                        bmvn.Vido = double.Parse(dr["Vido"].ToString());
                        bmvn.Deep = double.Parse(dr["Deep"].ToString());
                        bmvn.Tinhtrang = dr["Tinhtrang"].ToString();
                        if (double.TryParse(dr["Kichthuoc"].ToString(), out double Kichthuoc))
                        {
                            bmvn.Kichthuoc = Kichthuoc;
                        }
                        bmvn.PPXuLy = dr["PPXuLy"].ToString();

                        DGV_KetQuaBom.Rows.Add(indexRow, Loai, Kyhieu, ol_idST, bmvn.Kichthuoc, Deep, Kinhdo, Vido, Tinhtrang);
                        DGV_KetQuaBom.Rows[indexRow - 1].Tag = bmvn;

                        indexRow++;
                    }

                }
                //DGV_KetQuaBom.DataSource = null;
                //DGV_KetQuaBom.DataSource = dataset.Tables["KQKS"];
                //DGV_KetQuaBom.ReadOnly = true;

                //DGV_KetQuaBom.Columns[0].Name = "Loai";
                //DGV_KetQuaBom.Columns[0].HeaderText = "Chủng loại";
                //DGV_KetQuaBom.Columns[1].Name = "Kyhieu";
                //DGV_KetQuaBom.Columns[1].HeaderText = "Ký hiệu";
                //DGV_KetQuaBom.Columns[2].Name = "O";
                //DGV_KetQuaBom.Columns[2].HeaderText = "Ô số";
                //DGV_KetQuaBom.Columns[3].Name = "Kichthuoc";
                //DGV_KetQuaBom.Columns[3].HeaderText = "Kích thước";
                //DGV_KetQuaBom.Columns[4].Name = "Deep";
                //DGV_KetQuaBom.Columns[4].HeaderText = "Độ sâu";
                //DGV_KetQuaBom.Columns[5].Name = "XPoint";
                //DGV_KetQuaBom.Columns[5].HeaderText = "Kinh độ";
                //DGV_KetQuaBom.Columns[6].Name = "YPoint";
                //DGV_KetQuaBom.Columns[6].HeaderText = "Vĩ độ";
                //DGV_KetQuaBom.Columns[7].Name = "Tinhtrang";
                //DGV_KetQuaBom.Columns[7].HeaderText = "Tình trạng";

                //DGV_KetQuaBom.Columns[8].Name = "id";
                //DGV_KetQuaBom.Columns[8].HeaderText = "id";
                //DGV_KetQuaBom.Columns[9].Name = "idRectangle";
                //DGV_KetQuaBom.Columns[9].HeaderText = "idRectangle";

                //DGV_KetQuaBom.Columns[8].Visible = false;
                //DGV_KetQuaBom.Columns[9].Visible = false;
                //DGV_KetQuaBom.DefaultCellStyle.SelectionBackColor = Color.White;
                //DGV_KetQuaBom.DefaultCellStyle.SelectionForeColor = Color.Black;
                //for (int i = 0; i < DGV_KetQuaBom.ColumnCount; i++)
                //{
                //    DGV_KetQuaBom.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //    DGV_KetQuaBom.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //}
                //if (DGV_KetQuaBom.Rows != null && DGV_KetQuaBom.Rows.Count != 0)
                //{
                //    DGV_KetQuaBom.Rows[0].Selected = true;
                //    DGV_KetQuaBom.AllowUserToAddRows = false;
                //    DGV_KetQuaBom.BackgroundColor = Color.White;
                //    DGV_KetQuaBom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //    DGV_KetQuaBom.RowHeadersVisible = false;
                //    DGV_KetQuaBom.AllowUserToResizeRows = false;
                //}

            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if(DuanId == 0)
            //{
            //    MessageBox.Show("VUi lòng nhập dự án", "Thất bại");
            //}
            //else
            //{
            //    if (comboBox_Huyen.Text == "" || comboBox_Tinh.Text == "" || comboBox_Xa.Text == "" || cbKhuVucKS.Text =="")
            //    {
            //        MessageBox.Show("Vui lòng kiểm tra lại tỉnh huyện xã và khu vực ");
            //    }
            //    else
            //    {
            //        if (id_BSKQ == int.MinValue)
            //        {
            //            InsertData();
            //            this.DialogResult = DialogResult.OK;
            //        }

            //        else
            //        {

            //            UpdateInfomation(id_BSKQ);
            //            this.DialogResult = DialogResult.OK;
            //        }
            //    }


            //}
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            _cn.BeginTransaction();
            bool success = false;
            if (id_BSKQ <= 0)
            {
                success = InsertData();
                id_BSKQ = UtilsDatabase.GetLastIdIndentifyTable(_cn, "cecm_report_survey_area");
            }
            else
            {
                success = UpdateInfomation(id_BSKQ);
            }
            bool successDeleteSub = DeleteSubTable();
            bool successBMVN = UpdateBMVN();
            if (success && successDeleteSub && successBMVN)
            {
                _cn.Transaction.Commit();
                MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                this.DialogResult = DialogResult.OK;

            }
            else
            {
                _cn.Transaction.Rollback();
                MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                this.DialogResult = DialogResult.None;
            }
            this.Close();
        }

        private void loadInsertData()
        {

            SqlCommandBuilder sqlCommand = null;
            //LoadCBBoxprovince("");
            if (id_BSKQ != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT * from cecm_report_survey_area where cecm_report_survey_area.id = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {


                        txtHopphan.Text = dr["Hopphan"].ToString();

                        cb_TenDA.Text = dr["Duan"].ToString();

                        comboBox_Tinh.Text = dr["Tinh"].ToString();

                        comboBox_Huyen.Text = dr["Huyen"].ToString();

                        comboBox_Xa.Text = dr["Xa"].ToString();

                        timeNgaytao.Text = dr["Ngaytao"].ToString();

                        txtMaNV.Text = dr["MaNV"].ToString();

                        txtDoitruong.Text = dr["Doitruong"].ToString();

                        tbTongNgay.Text = dr["timeTongSo"].ToString();
                        TimeBD.Text = dr["timeBatdau"].ToString();

                        txtMaBCngay.Text = dr["MaBaocaongay"].ToString();


                        txtThon.Text = dr["Thon"].ToString();
                        TimeKT.Text = dr["timeKetthuc"].ToString();

                        tbChiHuyCT.Text = dr["ChiHuyCT"].ToString();

                        cbb_Doiso.Text = dr["Doiso"].ToString();
                        tbYeuCau.Text = dr["DientichKS"].ToString();

                        cbNguonBC.Text = dr["NguonBangChung"].ToString();

                        tbSoBC.Text = dr["SLBangChungYeuCau"].ToString();

                        tbDTKDNhiem.Text = dr["DTKhuONhiem"].ToString();

                        tbKVONhiem.Text = dr["SoKVONhiem"].ToString();

                        tbTongSoO.Text = dr["TongSoOKS"].ToString();

                        tbDTDaKT.Text = dr["DTDaKTCL"].ToString();

                        tbDTkhac.Text = dr["DTKSKhacYeuCau"].ToString();
                        tbSoOVang.Text = dr["Ovang"].ToString();
                        tbSoOXanhLa.Text = dr["Oxanhla"].ToString();

                        tbDaKS.Text = dr["DTDaKS"].ToString();

                        tbOXanhDaTroi.Text = dr["Oxanhdatroi"].ToString();

                        tbTienDo.Text = dr["TienDoTH"].ToString();
                        cbMucDoNhiem.Text = dr["MucDoONhiem"].ToString();

                        tbDTLamLai.Text = dr["DTPhaiLamLai"].ToString();
                        tbSoNghiNgo.Text = dr["SLDaKS"].ToString();
                        tbSoODo.Text = dr["Odo"].ToString();

                        tbTinHieu.Text = dr["TinHieu"].ToString();

                        //tbSoOTrang.Text = dr["Otrang"].ToString();
                        tbSoOXam.Text = dr["Oxam"].ToString();

                        tbOKDNhiem.Text = dr["SoOONhiem"].ToString();

                        tbNguoive.Text = dr["Hopphan"].ToString();


                        cbTinhTrang.Text = dr["isactive"].ToString();
                        cbKhuVucKS.Text = dr["KVKhaoSat"].ToString();

                        lblImage.Text = dr["PhotoFile"].ToString();
                        lbSodo.Text = dr["DrawFile"].ToString();
                        tbNguoive.Text = dr["NguoiVe"].ToString();
                        timeTGVe.Text = dr["TGVe"].ToString();

                    }
                }
            }
            //DGV_KetQuaBom.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //DGV_KetQuaBom.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ActiveCaption;
            //DGV_KetQuaBom.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;
            //DGV_KetQuaBom.EnableHeadersVisualStyles = false;


            //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT name FROM cecm_provincial", _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
            //sqlAdapterProvince.Fill(datatableProvince);
            //comboBox_Tinh.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    comboBox_Tinh.Items.Add(dr["name"].ToString());
            //}

            //SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            //System.Data.DataTable datatableProgram = new System.Data.DataTable();
            //sqlAdapterProgram.Fill(datatableProgram);
            ////comboBox_TenDA.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProgram.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    cb_TenDA.Items.Add(dr["name"].ToString());
            //}
            SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter("SELECT name FROM [cert_command_person]", _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterPerson);
            System.Data.DataTable datatablePerson = new System.Data.DataTable();
            sqlAdapterPerson.Fill(datatablePerson);
            cbb_Doiso.Items.Add("Chọn");
            foreach (DataRow dr in datatablePerson.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString()))
                    continue;

                cbb_Doiso.Items.Add(dr["name"].ToString());
            }
            LoadDataBMVN();
            //DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            //Editlink.UseColumnTextForLinkValue = true;
            //Editlink.HeaderText = "Edit";
            //Editlink.DataPropertyName = "lnkColumn";
            //Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            //Editlink.Text = "Edit";
            //DGV_KetQuaBom.Columns.Add(Editlink);

            //Delete link

            //DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            //Deletelink.UseColumnTextForLinkValue = true;
            //Deletelink.HeaderText = "delete";
            //Deletelink.DataPropertyName = "lnkColumn";
            //Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            //Deletelink.Text = "Delete";
            //DGV_KetQuaBom.Columns.Add(Deletelink);

            if (string.IsNullOrEmpty(cb_TenDA.Text) == true)
            {
                //SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_programData where name = N'{0}'", cb_TenDA.Text), _Cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                //System.Data.DataTable datatableWard = new System.Data.DataTable();
                //sqlAdapterWard.Fill(datatableWard);

                //foreach (DataRow dr in datatableWard.Rows)
                //{
                //    DuanId = int.Parse(dr["id"].ToString());
                //}
                LoadDataBMVN();
                //DataGridViewLinkColumn Editlink1 = new DataGridViewLinkColumn();
                //Editlink1.UseColumnTextForLinkValue = true;
                //Editlink1.HeaderText = "Edit";
                //Editlink1.DataPropertyName = "lnkColumn";
                //Editlink1.LinkBehavior = LinkBehavior.SystemDefault;
                //Editlink1.Text = "Edit";
                //DGV_KetQuaBom.Columns.Add(Editlink1);

                //Delete link

                //DataGridViewLinkColumn Deletelink1 = new DataGridViewLinkColumn();
                //Deletelink1.UseColumnTextForLinkValue = true;
                //Deletelink1.HeaderText = "delete";
                //Deletelink1.DataPropertyName = "lnkColumn";
                //Deletelink1.LinkBehavior = LinkBehavior.SystemDefault;
                //Deletelink1.Text = "Delete";
                //DGV_KetQuaBom.Columns.Add(Deletelink1);
            }
        }
        private void loadEditData(int idVal)
        {
            SqlCommandBuilder sqlCommand = null;
            //LoadCBBoxprovince("");

            SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter("SELECT name FROM [cert_command_person]", _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterPerson);
            System.Data.DataTable datatablePerson = new System.Data.DataTable();
            sqlAdapterPerson.Fill(datatablePerson);
            cbb_Doiso.Items.Add("Chọn");
            foreach (DataRow dr in datatablePerson.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString()))
                    continue;

                cbb_Doiso.Items.Add(dr["name"].ToString());
            }

            if (id_BSKQ != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT * from cecm_report_survey_area where cecm_report_survey_area.id = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {


                        txtHopphan.Text = dr["Hopphan"].ToString();

                        cb_TenDA.Text = dr["Duan"].ToString();
                        cbKhuVucKS.SelectedValue = long.TryParse(dr["cecm_program_area_map_id"].ToString(), out long cecm_program_area_map_id) ? cecm_program_area_map_id : -1;

                        //comboBox_Tinh.Text = dr["Tinh"].ToString();

                        //comboBox_Huyen.Text = dr["Huyen"].ToString();

                        //comboBox_Xa.Text = dr["Xa"].ToString();

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

                        timeNgaytao.Text = dr["Ngaytao"].ToString();

                        txtMaNV.Text = dr["MaNV"].ToString();

                        

                        tbTongNgay.Text = dr["timeTongSo"].ToString();
                        TimeBD.Text = dr["timeBatdau"].ToString();

                        txtMaBCngay.Text = dr["MaBaocaongay"].ToString();


                        txtThon.Text = dr["Thon"].ToString();
                        TimeKT.Text = dr["timeKetthuc"].ToString();

                        tbGiamSat.Text = dr["GiamSat"].ToString();
                        tbChiHuyCT.Text = dr["ChiHuyCT"].ToString();

                        if(long.TryParse(dr["GiamSat_id"].ToString(), out long GiamSat_id))
                        {
                            cbGiamSat.SelectedValue = GiamSat_id;
                        }

                        if (long.TryParse(dr["ChiHuyCT_id"].ToString(), out long ChiHuyCT_id))
                        {
                            cbChiHuyCT.SelectedValue = ChiHuyCT_id;
                        }

                        cbb_Doiso.Text = dr["Doiso"].ToString();
                        txtDoitruong.Text = dr["Doitruong"].ToString();
                        tbYeuCau.Text = dr["DientichKS"].ToString();

                        cbNguonBC.Text = dr["NguonBangChung"].ToString();

                        tbSoBC.Text = dr["SLBangChungYeuCau"].ToString();

                        tbDTKDNhiem.Text = dr["DTKhuONhiem"].ToString();

                        if(double.TryParse(dr["DTKhongONhiem"].ToString(), out double DTKhongONhiem))
                        {
                            nudKhongON.Value = (decimal)DTKhongONhiem;
                        }

                        tbKVONhiem.Text = dr["SoKVONhiem"].ToString();

                        tbTongSoO.Text = dr["TongSoOKS"].ToString();

                        tbDTDaKT.Text = dr["DTDaKTCL"].ToString();

                        tbDTkhac.Text = dr["DTKSKhacYeuCau"].ToString();
                        tbSoOVang.Text = dr["Ovang"].ToString();
                        tbSoOXanhLa.Text = dr["Oxanhla"].ToString();

                        tbDaKS.Text = dr["DTDaKS"].ToString();

                        tbOXanhDaTroi.Text = dr["Oxanhdatroi"].ToString();

                        tbTienDo.Text = dr["TienDoTH"].ToString();
                        cbMucDoNhiem.Text = dr["MucDoONhiem"].ToString();

                        tbDTLamLai.Text = dr["DTPhaiLamLai"].ToString();
                        tbSoNghiNgo.Text = dr["SLDaKS"].ToString();
                        tbSoODo.Text = dr["Odo"].ToString();

                        tbTinHieu.Text = dr["TinHieu"].ToString();

                        //tbSoOTrang.Text = dr["Otrang"].ToString();
                        tbSoOXam.Text = dr["Oxam"].ToString();

                        tbOKDNhiem.Text = dr["SoOONhiem"].ToString();

                        tbNguoive.Text = dr["Hopphan"].ToString();


                        cbTinhTrang.Text = dr["isactive"].ToString();
                        cbKhuVucKS.Text = dr["KVKhaoSat"].ToString();

                        lblImage.Text = dr["PhotoFile"].ToString();
                        lbSodo.Text = dr["DrawFile"].ToString();
                        tbNguoive.Text = dr["NguoiVe"].ToString();
                        timeTGVe.Text = dr["TGVe"].ToString();
                        if(long.TryParse(dr["DVKS_id"].ToString(), out long DVKS_id))
                        {
                            cbDVKS.SelectedValue = DVKS_id;
                        }
                    }
                }
            }
            //DGV_KetQuaBom.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //DGV_KetQuaBom.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ActiveCaption;
            //DGV_KetQuaBom.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;
            //DGV_KetQuaBom.EnableHeadersVisualStyles = false;


            //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT name FROM cecm_provincial", _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
            //sqlAdapterProvince.Fill(datatableProvince);
            //comboBox_Tinh.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    comboBox_Tinh.Items.Add(dr["name"].ToString());
            //}

            //SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            //System.Data.DataTable datatableProgram = new System.Data.DataTable();
            //sqlAdapterProgram.Fill(datatableProgram);
            ////comboBox_TenDA.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProgram.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    cb_TenDA.Items.Add(dr["name"].ToString());
            //}
            
            LoadDataBMVN();
            //DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            //Editlink.UseColumnTextForLinkValue = true;
            //Editlink.HeaderText = "Sửa";
            //Editlink.DataPropertyName = "lnkColumn";
            //Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            //Editlink.Text = "Edit";
            //DGV_KetQuaBom.Columns.Add(Editlink);

            //Delete link

            //DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            //Deletelink.UseColumnTextForLinkValue = true;
            //Deletelink.HeaderText = "Xóa";
            //Deletelink.DataPropertyName = "lnkColumn";
            //Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            //Deletelink.Text = "Delete";
            //DGV_KetQuaBom.Columns.Add(Deletelink);

            if (string.IsNullOrEmpty(cb_TenDA.Text) == false)
            {
                //SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_programData where name = N'{0}'", cb_TenDA.Text), _Cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                //System.Data.DataTable datatableWard = new System.Data.DataTable();
                //sqlAdapterWard.Fill(datatableWard);

                //foreach (DataRow dr in datatableWard.Rows)
                //{
                //    DuanId = int.Parse(dr["id"].ToString());
                //}
                LoadDataBMVN();
                //DataGridViewLinkColumn Editlink1 = new DataGridViewLinkColumn();
                //Editlink1.UseColumnTextForLinkValue = true;
                //Editlink1.HeaderText = "Edit";
                //Editlink1.DataPropertyName = "lnkColumn";
                //Editlink1.LinkBehavior = LinkBehavior.SystemDefault;
                //Editlink1.Text = "Edit";
                //DGV_KetQuaBom.Columns.Add(Editlink1);

                //Delete link

                //DataGridViewLinkColumn Deletelink1 = new DataGridViewLinkColumn();
                //Deletelink1.UseColumnTextForLinkValue = true;
                //Deletelink1.HeaderText = "delete";
                //Deletelink1.DataPropertyName = "lnkColumn";
                //Deletelink1.LinkBehavior = LinkBehavior.SystemDefault;
                //Deletelink1.Text = "Delete";
                //DGV_KetQuaBom.Columns.Add(Deletelink1);
            }
        }
        private void FrmThemmoiBaocaoKQ_Load(object sender, EventArgs e)
        {
            //LoadCBBoxprovince("");
            LoadCBTinh();
            LoadCBDA();
            if (id_BSKQ == int.MinValue)
            {
                loadInsertData();
                this.Text = "THÊM MỚI BÁO CÁO KHẢO SÁT KHU VỰC";
            }
            else
            {
                loadEditData(id_BSKQ);
                this.Text = "CHỈNH SỬA BÁO CÁO KHẢO SÁT KHU VỰC";
            }
        }

        private void comboBox_Tinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            comboBox_Huyen.Text = null;
            comboBox_Xa.Text = null;
            if (comboBox_Tinh.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Tinh.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    TinhId = int.Parse(dr["id"].ToString());
                }

                SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 2 and parent_id = {0}", TinhId), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);
                comboBox_Huyen.Items.Clear();
                comboBox_Huyen.Items.Add("Chọn");
                foreach (DataRow dr in datatableCounty.Rows)
                {
                    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                        continue;

                    comboBox_Huyen.Items.Add(dr["Ten"].ToString());
                }


            }
        }

        private void comboBox_Huyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            comboBox_Xa.Text = null;
            if (comboBox_Huyen.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Huyen.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);

                foreach (DataRow dr in datatableCounty.Rows)
                {
                    HuyenId = int.Parse(dr["id"].ToString());
                }

                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 3 and parentiddistrict = {0}", HuyenId), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);
                comboBox_Xa.Items.Clear();
                comboBox_Xa.Items.Add("Chọn");
                foreach (DataRow dr in datatableWard.Rows)
                {
                    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                        continue;

                    comboBox_Xa.Items.Add(dr["Ten"].ToString());
                }
            }
        }

        private void comboBox_TenDA_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (cb_TenDA.SelectedValue is long)
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT startTime,endTime FROM cecm_programData where id = {0}", cb_TenDA.SelectedValue), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    timeVl = TimeBD.Text = dr["startTime"].ToString();
                    time2 = TimeKT.Text = dr["endTime"].ToString();
                    //DuanId = int.Parse(dr["id"].ToString());
                }
                LoadDataBMVN_onChange_cbDA();
                LoadDVKS();
                LoadCBStaff(cbChiHuyCT);
                LoadCBStaff(cbGiamSat);
                LoadCBVungDA((long)cb_TenDA.SelectedValue);
            }
        }

        private void comboBox_Xa_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            if (comboBox_Xa.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Xa.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);

                foreach (DataRow dr in datatableCounty.Rows)
                {
                    XaId = int.Parse(dr["id"].ToString());
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == cotSua.Index)
            {
                //int id_kqks = Convert.ToInt32(DGV_KetQuaBom.Rows[e.RowIndex].Cells["id"].Value);
                //CecmReportSurveyAreaBMVN bmvn = DGV_KetQuaBom.Rows[e.RowIndex].Tag as CecmReportSurveyAreaBMVN;
                FrmThemmoiBomminKV frm = new FrmThemmoiBomminKV(DGV_KetQuaBom.Rows[e.RowIndex], (long)cb_TenDA.SelectedValue);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DGV_KetQuaBom.DataSource = null;
                    LoadDataBMVN();
                    //DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
                    //Editlink.UseColumnTextForLinkValue = true;
                    //Editlink.HeaderText = "Edit";
                    //Editlink.DataPropertyName = "lnkColumn";
                    //Editlink.LinkBehavior = LinkBehavior.SystemDefault;
                    //Editlink.Text = "Edit";
                    //DGV_KetQuaBom.Columns.Add(Editlink);

                    //Delete link

                    //DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
                    //Deletelink.UseColumnTextForLinkValue = true;
                    //Deletelink.HeaderText = "delete";
                    //Deletelink.DataPropertyName = "lnkColumn";
                    //Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
                    //Deletelink.Text = "Delete";
                    //DGV_KetQuaBom.Columns.Add(Deletelink);
                }
            }
            //delete column
            if (e.ColumnIndex == cotXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                DGV_KetQuaBom.Rows.RemoveAt(e.RowIndex);
                //int id_kqks = Convert.ToInt32(DGV_KetQuaBom.Rows[e.RowIndex].Cells["id"].Value);
                //SqlCommand cmd2 = new SqlCommand("DELETE FROM Cecm_VNTerrainMinePoint WHERE id = " + id_kqks, _Cn);
                //int temp = 0;
                //temp = cmd2.ExecuteNonQuery();
                //if (temp > 0)
                //{
                //    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                //    LoadDataBMVN();
                //    //DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
                //    //Editlink.UseColumnTextForLinkValue = true;
                //    //Editlink.HeaderText = "Edit";
                //    //Editlink.DataPropertyName = "lnkColumn";
                //    //Editlink.LinkBehavior = LinkBehavior.SystemDefault;
                //    //Editlink.Text = "Edit";
                //    //DGV_KetQuaBom.Columns.Add(Editlink);

                //    //Delete link

                //    //DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
                //    //Deletelink.UseColumnTextForLinkValue = true;
                //    //Deletelink.HeaderText = "delete";
                //    //Deletelink.DataPropertyName = "lnkColumn";
                //    //Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
                //    //Deletelink.Text = "Delete";
                //    //DGV_KetQuaBom.Columns.Add(Deletelink);
                //}
                //else
                //{
                //    MessageBox.Show("Xóa dữ liệu ko thành công... ", "Thất bại");
                //}
            }
        }

        private void LoadData()
        {
            LoadDataBMVN();
            //DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            //Editlink.UseColumnTextForLinkValue = true;
            //Editlink.HeaderText = "Edit";
            //Editlink.DataPropertyName = "lnkColumn";
            //Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            //Editlink.Text = "Edit";
            //DGV_KetQuaBom.Columns.Add(Editlink);

            //Delete link

            //DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            //Deletelink.UseColumnTextForLinkValue = true;
            //Deletelink.HeaderText = "delete";
            //Deletelink.DataPropertyName = "lnkColumn";
            //Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            //Deletelink.Text = "Delete";
            //DGV_KetQuaBom.Columns.Add(Deletelink);
        }

        private void cbb_Doiso_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            if (cbb_Doiso.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter(string.Format("SELECT id, master FROM [cert_command_person] where name = N'{0}'", cbb_Doiso.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterPerson);
                System.Data.DataTable datatablePerson = new System.Data.DataTable();
                sqlAdapterPerson.Fill(datatablePerson);
                foreach (DataRow dr in datatablePerson.Rows)
                {
                    if (string.IsNullOrEmpty(dr["id"].ToString()))
                        continue;

                    PersonId = dr["id"].ToString();
                    txtDoitruong.Text = dr["master"].ToString();
                }
            }
        }

        private void tbDaKS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnFileAnh_Click(object sender, EventArgs e)
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "jpg File (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(locationstring) == false)
                lblImage.Text = locationstring;
        }

        private void btnSoDo_Click(object sender, EventArgs e)
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "DWG File (*.dwg)|*.dwg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(locationstring) == false)
                lbSodo.Text = locationstring;
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmThemmoiBaoCaoKQKV_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmThemmoiBaoCaoKQKV_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void TimeBD_ValueChanged(object sender, EventArgs e)
        {
            //int result = DateTime.Compare(TimeKT.Value, TimeBD.Value);
            if (TimeKT.Value > TimeBD.Value)
            {
                //MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Lỗi",
                //                 MessageBoxButtons.OK,
                //                 MessageBoxIcon.Warning);
                //TimeBD.Text = timeVl;
                //TimeKT.Text = time2;
                TimeKT.Value = TimeBD.Value;
                tbTongNgay.Text = "0";
            }
            else
            {
                tbTongNgay.Text = TimeKT.Value.Subtract(TimeBD.Value).TotalDays.ToString();
            }
        }

        private void TimeKT_ValueChanged(object sender, EventArgs e)
        {
            if (TimeKT.Value > TimeBD.Value)
            {
                //MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Lỗi",
                //                 MessageBoxButtons.OK,
                //                 MessageBoxIcon.Warning);
                //TimeBD.Text = timeVl;
                //TimeKT.Text = time2;
                TimeBD.Value = TimeKT.Value;
                tbTongNgay.Text = "0";
            }
            else
            {
                tbTongNgay.Text = TimeKT.Value.Subtract(TimeBD.Value).TotalDays.ToString();
            }
        }

        private void cb_TenDA_Validating(object sender, CancelEventArgs e)
        {
            if (cb_TenDA.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(cb_TenDA, "Chưa chọn dự án");
                return;
            }
            if ((long)cb_TenDA.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(cb_TenDA, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(cb_TenDA, "Chưa chọn dự án");
            }
        }

        private void comboBox_Tinh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            LoadCBHuyen();
        }

        private void comboBox_Huyen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            LoadCBXa();
        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Tinh.SelectedValue <= 0)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Tinh, "Chưa chọn tỉnh");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Tinh, "");
            }
        }

        private void comboBox_Huyen_Validating(object sender, CancelEventArgs e)
        {
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Tinh.SelectedValue <= 0)
            {
                return;
            }
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Huyen.SelectedValue <= 0)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Huyen, "Chưa chọn huyện");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Huyen, "");
            }
        }

        private void comboBox_Xa_Validating(object sender, CancelEventArgs e)
        {
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Huyen.SelectedValue <= 0)
            {
                return;
            }
            if (!(comboBox_Xa.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Xa.SelectedValue <= 0)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Xa, "");
            }
        }

        private void tbSoOMau_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbSoODo.Text, out int Odo);
            int.TryParse(tbSoOVang.Text, out int Ovang);
            int.TryParse(tbOXanhDaTroi.Text, out int Onau);
            //int.TryParse(tbSoOTrang.Text, out int Otrang);
            int.TryParse(tbSoOXam.Text, out int Oxam);
            tbTongSoO.Text = (Odo + Ovang + Onau + Oxam).ToString();
        }

        private void cbGiamSat_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbGiamSat.SelectedValue is long))
            {
                return;
            }
            if ((long)cbGiamSat.SelectedValue > 0)
            {
                tbGiamSat.ReadOnly = true;
                tbGiamSat.Text = "";
            }
            else
            {
                tbGiamSat.ReadOnly = false;
            }
        }

        private void cbChiHuyCT_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbChiHuyCT.SelectedValue is long))
            {
                return;
            }
            if ((long)cbChiHuyCT.SelectedValue > 0)
            {
                tbChiHuyCT.ReadOnly = true;
                tbChiHuyCT.Text = "";
            }
            else
            {
                tbChiHuyCT.ReadOnly = false;
            }
        }

        private void cbDVKS_Validating(object sender, CancelEventArgs e)
        {
            if (cbDVKS.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(cbDVKS, "Chưa chọn đơn vị khảo sát");
                return;
            }
            if ((long)cbDVKS.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(cbDVKS, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(cbDVKS, "Chưa chọn đơn vị khảo sát");
            }
        }

        private void TimeBD_TabIndexChanged(object sender, EventArgs e)
        {
            int result = DateTime.Compare(TimeKT.Value, TimeBD.Value);
            if (result < 0)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Lỗi",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                TimeBD.Text = timeVl;
                TimeKT.Text = time2;
            }
            else
            {
                tbTongNgay.Text = TimeKT.Value.Subtract(TimeBD.Value).TotalDays.ToString();
            }

        }

        private void txtMaBCngay_Validating(object sender, CancelEventArgs e)
        {
            if(txtMaBCngay.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMaBCngay, "Chưa nhập mã");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMaBCngay, "");
            }
        }

        private void LoadCBVungDA(long idDA)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name FROM cecm_program_area_map where cecm_program_id = " + idDA), _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            DataRow dr = datatable.NewRow();
            dr["id"] = -1;
            dr["name"] = "Chưa chọn";
            datatable.Rows.InsertAt(dr, 0);
            cbKhuVucKS.DataSource = datatable;
            cbKhuVucKS.DisplayMember = "name";
            cbKhuVucKS.ValueMember = "id";
        }

    }
}
