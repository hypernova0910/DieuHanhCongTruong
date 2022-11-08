using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System;
using DieuHanhCongTruong.Models;

namespace DieuHanhCongTruong.Models
{
    internal class ImportDuAnCmdNew
    {
        public CecmProgramComplexDTO ReadDataZipFile(SqlConnection cn, out string folderUnzip)
        {
            string fileLocation = string.Empty;
            folderUnzip = string.Empty;

            LocationOpenFile(ref fileLocation);
            if (string.IsNullOrEmpty(fileLocation))
                return null;

            folderUnzip = GetCopyFolder(fileLocation);

            var retval = ReadZipFile(fileLocation, folderUnzip);

            return retval;
        }

        private CecmProgramComplexDTO ReadZipFile(string fileLocation, string folderUnZip)
        {
            CecmProgramComplexDTO retVal = new CecmProgramComplexDTO();
            UnZipp(fileLocation, folderUnZip);

            string[] files = Directory.GetFiles(folderUnZip, "*.json");
            if (files.Length == 0)
                return retVal;

            string jsonFile = files.FirstOrDefault();
            if (string.IsNullOrEmpty(jsonFile))
                return retVal;

            using (StreamReader r = new StreamReader(jsonFile))
            {
                var json = r.ReadToEnd();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                retVal = JsonConvert.DeserializeObject<CecmProgramComplexDTO>(json, settings);
            }

            return retVal;
        }

        private string GetCopyFolder(string fileLocation)
        {
            string tenDuAn = Guid.NewGuid().ToString();

            var strPath = Path.GetDirectoryName(fileLocation);

            if (System.IO.Directory.Exists(strPath + "\\" + tenDuAn + "\\Temp\\") == false)
                System.IO.Directory.CreateDirectory(strPath + "\\" + tenDuAn + "\\Temp\\");

            return strPath + "\\" + tenDuAn + "\\Temp\\";
        }

        private void UnZipp(string srcDirPath, string destDirPath)
        {
            ZipInputStream zipIn = null;
            FileStream streamWriter = null;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destDirPath));

                zipIn = new ZipInputStream(File.OpenRead(srcDirPath));
                ZipEntry entry;

                while ((entry = zipIn.GetNextEntry()) != null)
                {
                    string dirPath = Path.GetDirectoryName(destDirPath + entry.Name);

                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    if (!entry.IsDirectory)
                    {
                        streamWriter = File.Create(destDirPath + entry.Name);
                        int size = 2048;
                        byte[] buffer = new byte[size];

                        while ((size = zipIn.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            streamWriter.Write(buffer, 0, size);
                        }

                        streamWriter.Close();
                    }
                }
            }
            catch (System.Threading.ThreadAbortException lException)
            {
                // do nothing
                var mess = lException.Message;
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
                throw;
            }
            finally
            {
                if (zipIn != null)
                {
                    zipIn.Close();
                }

                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
        }

        private void LocationOpenFile(ref string locationstring)
        {
            System.IO.Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "ZIP File (*.ZIP)|*.ZIP|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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
        }
    }
}

public class CecmProgramMachineBombDTO
{
    public string cecmMachineBomId { get; set; }

    public string description { get; set; }

    public string code { get; set; }

    public string dept_id { get; set; }

    // lo�i bom m�n
    public string mac_id { get; set; }

    // �a ch� mac
    public string time_test { get; set; }

    // Th�i gian t�o
    public string typeMachine { get; set; }

    public string type_machineST { get; set; }

    // FK cecm_machine_bom_type
    public string deptId { get; set; }

    // FK cert_department
    public string staffId { get; set; }

    public string type_standart { get; set; }
    public string tbtype_standart_detail { get; set; }

    // FK cert_staff
    public string status { get; set; }

    //  thay th� cho typeMachine
    public string dept_idST { get; set; }

    // thay th� cho deptId
    public string fullname { get; set; }

    // thay th� cho staffId
    public string strStatus { get; set; }

    // thay th� cho status
    public string gid { get; set; }

    public string id { get; set; }

    public string start_time { get; set; }
    public string end_time { get; set; }

    public string cecm_ProgramMachineBomb { get; set; }
}

public class CecmProgramStaffDTO
{
    public string id { get; set; }

    // T�n
    public string nameIdST { get; set; }

    public string cecmProgramTeamId { get; set; }
    public string cecmProgramTeamIdST { get; set; }
    public string type_standart { get; set; }
    public string theMaster { get; set; }
    public string departmentId { get; set; }
    public string level { get; set; }
    public string birthday { get; set; }
    public string thesex { get; set; }
    public string phone { get; set; }

    public string email { get; set; }

    public string type_standartST { get; set; }
    public string description { get; set; }
    public string deptid { get; set; }

    public string nameOther { get; set; }

    // Tr�nh �
    public string edu { get; set; }

    public string code { get; set; }

    // ch�c v�
    public string rank { get; set; }

    // id ke hoach
    public string planid { get; set; }

    // id nhan vien
    public string staffid { get; set; }

    // Vai tr�
    public string roletype { get; set; }

    // id du an
    public string programid { get; set; }

    public string eduST { get; set; }

    public string deptST { get; set; }

    public string nameRank { get; set; }

    public string roletypeST { get; set; }

    public string startTime { get; set; }

    public string endTime { get; set; }

    public string programName { get; set; }

    public string programpackageid { get; set; }

    public string programpackageidST { get; set; }

    public string levelasign { get; set; }

    public string planidST { get; set; }
}

public class CecmProgramAreaMapDTO
{
    public string cecmProgramAreaMapId { get; set; }

    public string cecmProgramId { get; set; }

    public string cecmProgramAreaId { get; set; }

    public string cecmProgramStationId { get; set; }

    public string positionLat { get; set; }

    public string positionLong { get; set; }

    public string polygon { get; set; }

    public string parentId { get; set; }

    public string parentIdDistrict { get; set; }

    public string parentIdCommune { get; set; }

    public string villageId { get; set; }

    public string address { get; set; }

    public string code { get; set; }

    public string comboboxName { get; set; }

    public string polygonGeom { get; set; }

    public string polygonGeomST { get; set; }

    public string postringGeomST { get; set; }

    public string doc_file { get; set; }

    public string photo_file { get; set; }

    public string vn2000 { get; set; }

    public string positionLongVN2000 { get; set; }

    public string positionLatVN2000 { get; set; }

    public string meridian { get; set; }

    public string pzone { get; set; }

    public string lx { get; set; }

    public string ly { get; set; }

    public string cecmProgramStationIdST { get; set; }

    public string cecmProgramIdST { get; set; }

    public string areamap { get; set; }

    public string bottom_lat { get; set; }

    public string top_lat { get; set; }

    public string left_long { get; set; }

    public string right_long { get; set; }

    public long? bma_id { get; set; }

    public string bma_idST { get; set; }
    public long? cha_id { get; set; }

    public string cha_idST { get; set; }
    public long? hopphan_id { get; set; }

    public string hopphan_idST { get; set; }
    public List<long> lst_hopphan { get; set; }

    public List<string> lst_hopphanST { get; set; }

    public string lst_hopphanComplex { get; set; }

    public List<long> lst_dept_rapha { get; set; }

    public List<string> lst_dept_raphaST { get; set; }

    public string lst_dept_raphaComplex { get; set; }

    public List<long> lst_dept_khaosat { get; set; }

    public List<string> lst_dept_khaosatST { get; set; }

    public string lst_dept_khaosatComplex { get; set; }

    public string file_anhtmp { get; set; }


    public List<OLuoi> cecmProgramAreaMapSub_lstSubTable { get; set; }

    public override string ToString()
    {
        return code;
    }
}

public class CecmProgramComplexDTO
{
    public CecmProgramDTO cecmProgramDTO { get; set; }

    public List<CecmProgramAreaMapDTO> lstArea { get; set; }

    public List<CecmProgramTeamDTO> lstCecmProgramTeamDTO { get; set; }

    public List<CecmProgramStaffDTO> lstCecmProgramStaffDTO { get; set; }

    public List<CecmProgramMachineBombDTO> lstCecmProgramMachineBombDTO { get; set; }

    public List<CecmProgramDeviceDTO> lstCecmProgramDeviceDTO { get; set; }

    public List<CecmPlanProgramWrapDTO> cecmPlanProgramWrapDTO
    {
        get
        {
            return new List<CecmPlanProgramWrapDTO>();
        }
        set
        {
        }
    }
}

public class CecmProgramDeviceDTO
{
    public string gid { get; set; }
    public string action_type { get; set; }
    public string cecmProgramId { get; set; }//Id dự án
    public string cecmProgramIdST { get; set; }//Id dự án
    public string nameId { get; set; } //combobox chon ten thieets bij
    public string nameIdST { get; set; }
    public string nameOther { get; set; }
    public string type_machine { get; set; }
    public string name { get; set; }
    public string files { get; set; }
    public string status { get; set; }
    public string notes { get; set; }
    public string code { get; set; }

    public string action_typeST { get; set; }
    public string type_machineST { get; set; }
    public string statusST { get; set; }
    public string deptId { get; set; }
    public string deptIdST { get; set; }
    public string time_test { get; set; } //thoi gia kiem dinh
    public string time_testST { get; set; } //file
    public string type_standart { get; set; }//Loại nhân sự theo chuẩn
    public string type_standart_detail { get; set; }//Loại nhân sự theo chuẩn - chi tiết
}

public class CecmPlanProgramWrapDTO
{
    public CecmPlanProgramDTO cecmPlanProgramDTO { get; set; }
    public List<CecmProgramStaffDTO> lstPlanPerson { get; set; }
    public List<CecmProgramMachineBombDTO> lstCecmPlanMachineBombDTO { get; set; }
    public List<CertDeviceDTO> lstCertDevice { get; set; }
}

public class CertDeviceDTO
{
    public string id { get; set; }
    public string action_type { get; set; }
    public string type_machine { get; set; }
    public string name { get; set; }
    public string files { get; set; }
    public string status { get; set; }
    public string notes { get; set; }
    public string code { get; set; }

    public string action_typeST { get; set; }
    public string type_machineST { get; set; }
    public string statusST { get; set; }
    public string deptId { get; set; }
    public string deptIdST { get; set; }
    public string gid { get; set; }
}

public class CecmPlanProgramDTO
{
    public string id { get; set; }
    public string province_id { get; set; }
    public string province_idST { get; set; }
    public string start_time { get; set; }
    public string start_timeST { get; set; }
    public string end_time { get; set; }
    public string end_timeST { get; set; }
    public string type_special { get; set; }
    public string type_specialST { get; set; }
    public string money_implement { get; set; }
    public string money_source { get; set; }
    public string money_sourceST { get; set; }
    public string person { get; set; }
    public string personST { get; set; }
    public string dept_manage { get; set; }
    public string dept_manageST { get; set; }
    public string dept_type_manage { get; set; }
    public string dept_type_manageST { get; set; }
    public string sercurity_method { get; set; }
    public string map_use { get; set; }
    public string map_useST { get; set; }
    public string cecm_program_id { get; set; }
    public string cecm_program_idST { get; set; }
    public string the_note { get; set; }
    public string dept_impl { get; set; }
    public string dept_implST { get; set; }
    public string time_total { get; set; }
    public string longValue { get; set; }
    public string latValue { get; set; }
    public string area_land_process { get; set; }
    public string area_water_process { get; set; }
    public string source_mouney_other { get; set; }
    public string person_assign_other { get; set; }
    public string process_tech_other { get; set; }
    public string method_bm_other { get; set; }
    public List<string> doc_lstAction_type { get; set; }
    public List<string> doc_source_money { get; set; }
    public List<string> lst_person_assign { get; set; }
    public List<string> lst_process_tech { get; set; }
    public List<string> lst_method_bm { get; set; }
    public string comboboxPlan { get; set; }
    public string edu_person_other { get; set; }
    public string edu_bomb_other { get; set; }
    public string edu_action_other { get; set; }

    public string victim_support_dept_province { get; set; }
    public string victim_support_dept_hospital { get; set; }
    public string victim_support_dept_other_all { get; set; }

    public List<string> lst_edu_person { get; set; }
    public List<string> lst_edu_bomb { get; set; }
    public List<string> lst_edu_action { get; set; }
    public List<string> lst_victim_support_type { get; set; }
    public List<string> lst_victim_support_dept_other { get; set; }
    public string dept_manage_edu { get; set; }
    public string dept_type_manage_edu { get; set; }

    public string dept_manage_eduST { get; set; }
    public string dept_type_manage_eduST { get; set; }

    public string start_timeSTCal { get; set; }
    public string end_timeSTCal { get; set; }
    public string contentwork { get; set; }
    public string cecmprogrampackageid { get; set; }
    public string cecmprogrampackageidST { get; set; }
}

public class CecmProgramTeamDTO
{
    public string gid { get; set; }
    public string cecmProgramId { get; set; }
    public string cecmProgramIdST { get; set; }
    public string deptId { get; set; }
    public string deptIdST { get; set; }
    public string deptName { get; set; }
    public string name { get; set; }
    public string levels { get; set; }
    public string levelsST { get; set; }
    public string type { get; set; }
    public string typeST { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
    public string total_member { get; set; }
    public string notes { get; set; }
    public string long1 { get; set; }
    public string long2 { get; set; }
    public string string1 { get; set; }
    public string string2 { get; set; }

    public override string ToString()
    {
        return name;
    }
}

public class CecmProgramDTO
{
    public string cecmProgramId { get; set; }
    public string name { get; set; }
    public string positionLat { get; set; }
    public string positionLong { get; set; }
    public string timeTotal { get; set; }//tổng thời gian cần hoàn thành (ngày)
    public string moneyTotal { get; set; }//Tổng kinh phí
    public string moneySource { get; set; }//nguồn kinh phí
    public string programType { get; set; }//Loại chương trình
    public string code { get; set; }//mã dự án
    public string address { get; set; }
    public string startTime { get; set; } //thời gian bắt đầu
    public string endTime { get; set; }//Thời gian kết thúc
    public string description { get; set; }
    public string isActive { get; set; }//trạng thái
    public string mapId { get; set; }//bản đồ dự án
    public string acreage { get; set; } //diện tích dự án
    public string acreagewater { get; set; } //diện tích dưới nước
    public string proviceId { get; set; }//địa bàn diễn ra
    public string deptId { get; set; }//Đơn vị phụ trách
    public string result { get; set; }//kết quả
    public string departmentMaster { get; set; }//Đơn vị quản lý
    public string departmentName { get; set; }//Đơn vị quản lý ngoài
    public string contact { get; set; }//Địa chỉ liên hệ cảu đơn vị
    public string deptHead { get; set; }//Người đứng đầu
    public string deptHeadPost { get; set; }//chức vụ người đứng đầu
    public string deptHeadPhone { get; set; }
    public string deptHeadEmail { get; set; }

    public string parentId { get; set; }//ID cha
    public string levelType { get; set; }//cấp độ dự án
    public string textPolygon { get; set; }
    public string deptName { get; set; }//Thay thế cho deptId
    public string timeWork { get; set; }//Tổng thời gian = startTime + endTime
    public string proviceName { get; set; }//thay thế cho proviceId
    public string stt { get; set; }//thay thế cho isActive
    public string typeName { get; set; }//thay thế cho programType
    public string parentName { get; set; }//thay thế cho parentId

    public string money_pre { get; set; }
    public string source_mouney_other { get; set; }
    public string person_assign_other { get; set; }
    public string doc_number { get; set; }
    public string doc_date { get; set; }
    public string doc_person_type { get; set; }
    public string doc_file { get; set; }
    public List<string> doc_lstAction_type { get; set; }
    public List<string> doc_source_money { get; set; }
    public List<string> lst_person_assign { get; set; }

    public string programTypeST { get; set; }
    public string proviceIdST { get; set; }
    public string deptIdST { get; set; }
    public string departmentMasterST { get; set; }
    public string parentIdST { get; set; }
    public string levelTypeST { get; set; }
    public string startTimeST { get; set; }
    public string endTimeST { get; set; }
    public string doc_dateST { get; set; }
    public string deptCDT { get; set; }
    public string deptCDTName { get; set; }
    public string deptCDTcontact { get; set; }
    public string deptCDTHead { get; set; }
    public string deptCDTPost { get; set; }
    public string deptCDTTel { get; set; }
    //public List<CecmProgramPackageDTO> lstPackage{ get; set; }

    public string approve_status { get; set; }
    public string approve_userid { get; set; }
    public string approve_reason { get; set; }
    public string approve_createddate { get; set; }
    public string approve_updateddate { get; set; }
    public string approve_publish_deptid { get; set; }
    public string approve_publish_deptid_type { get; set; }
    public string approve_statusST { get; set; }
    public string approve_useridST { get; set; }
    public string approve_createddateST { get; set; }
    public string approve_updateddateST { get; set; }
    public string approve_publish_deptidST { get; set; }
    public string approve_publish_deptid_typeST { get; set; }
    public string packageInfor { get; set; }
    private List<string> doc_source_moneyST { get; set; }
    public string doc_source_moneyComplex { get; set; }

    //new
    public List<string> lst_proviceOther { get; set; }

    public List<string> lst_step_type { get; set; }
    public string departmentphone { get; set; }
    public string departmentemail { get; set; }
    public string departmentfax { get; set; }
    public string deptCDTphone { get; set; }
    public string deptCDTemail { get; set; }
    public string deptCDTfax { get; set; }
    public string deptCDTHeadEmail { get; set; }
    public string files { get; set; }
    public string notes { get; set; }
    //new

    public string acreagesea { get; set; } //diện tích dự án dưới biển

    // thong tin ve co quan dơn vi dieu tra - khao sat - ra pha bom min vat no:
    public string doc_number1 { get; set; }

    public string doc_date1 { get; set; }
    public string doc_date1ST { get; set; }
    public string doc_person_type1 { get; set; }
    public string doc_file1 { get; set; }

    // dieu tra
    public string departmentMaster2 { get; set; }//Đơn vị quản lý

    public string departmentMaster2ST { get; set; }//Đơn vị quản lý
    public string departmentName2 { get; set; }//Đơn vị quản lý ngoài
    public string departmentphone2 { get; set; }
    public string departmentemail2 { get; set; }
    public string contact2 { get; set; }//Địa chỉ liên hệ cảu đơn vị
    public string departmentfax2 { get; set; }
    public string deptHead2 { get; set; }//Người đứng đầu
    public string deptHeadPost2 { get; set; }//chức vụ người đứng đầu
    public string deptHeadPhone2 { get; set; }
    public string deptHeadEmail2 { get; set; }
    public string doc_number2 { get; set; }
    public string doc_date2 { get; set; }
    public string doc_date2ST { get; set; }
    public string doc_person_type2 { get; set; }
    public string doc_file2 { get; set; }

    // tuyen truyen giao duc phong tranh bom min, vat no
    public string departmentMaster6 { get; set; }//Đơn vị quản lý

    public string departmentName6 { get; set; }//Đơn vị quản lý ngoài
    public string departmentphone6 { get; set; }
    public string departmentemail6 { get; set; }
    public string contact6 { get; set; }//Địa chỉ liên hệ cảu đơn vị
    public string departmentfax6 { get; set; }
    public string deptHead6 { get; set; }//Người đứng đầu
    public string deptHeadPost6 { get; set; }//chức vụ người đứng đầu
    public string deptHeadPhone6 { get; set; }
    public string deptHeadEmail6 { get; set; }
    public string doc_number6 { get; set; }
    public string doc_date6 { get; set; }
    public string doc_date6ST { get; set; }
    public string doc_person_type6 { get; set; }
    public string doc_file6 { get; set; }

    // ho tro nan nhan
    public string departmentMaster7 { get; set; }//Đơn vị quản lý

    public string departmentName7 { get; set; }//Đơn vị quản lý ngoài
    public string departmentphone7 { get; set; }
    public string departmentemail7 { get; set; }
    public string contact7 { get; set; }//Địa chỉ liên hệ cảu đơn vị
    public string departmentfax7 { get; set; }
    public string deptHead7 { get; set; }//Người đứng đầu
    public string deptHeadPost7 { get; set; }//chức vụ người đứng đầu
    public string deptHeadPhone7 { get; set; }
    public string deptHeadEmail7 { get; set; }
    public string doc_number7 { get; set; }
    public string doc_date7 { get; set; }
    public string doc_date7ST { get; set; }
    public string doc_person_type7 { get; set; }
    public string doc_file7 { get; set; }

    // khao sat
    public string departmentMaster3 { get; set; }//Đơn vị quản lý

    public string departmentMaster3ST { get; set; }
    public string departmentName3 { get; set; }//Đơn vị quản lý ngoài
    public string departmentphone3 { get; set; }
    public string departmentemail3 { get; set; }
    public string contact3 { get; set; }//Địa chỉ liên hệ cảu đơn vị
    public string departmentfax3 { get; set; }
    public string deptHead3 { get; set; }//Người đứng đầu
    public string deptHeadPost3 { get; set; }//chức vụ người đứng đầu
    public string deptHeadPhone3 { get; set; }
    public string deptHeadEmail3 { get; set; }
    public string doc_number3 { get; set; }
    public string doc_date3 { get; set; }
    public string doc_date3ST { get; set; }
    public string doc_person_type3 { get; set; }
    public string doc_file3 { get; set; }

    // tt ve cơ quan tham dinh
    public string deptTD { get; set; }

    public string deptTDST { get; set; }
    public string deptTDName { get; set; }
    public string deptTDPhone { get; set; }
    public string deptTDEmail { get; set; }
    public string deptTDContact { get; set; }
    public string deptTDfax { get; set; }
    public string deptTDHead { get; set; }
    public string deptTDPost { get; set; }
    public string deptTDHeadTel { get; set; }
    public string deptTDHeadEmail { get; set; }

    // tt ve cơ quan tham dinh
    public string deptQL { get; set; }

    public string deptQLST { get; set; }
    public string deptQLName { get; set; }
    public string deptQLPhone { get; set; }
    public string deptQLEmail { get; set; }
    public string deptQLContact { get; set; }
    public string deptQLfax { get; set; }
    public string deptQLHead { get; set; }
    public string deptQLPost { get; set; }
    public string deptQLHeadTel { get; set; }
    public string deptQLHeadEmail { get; set; }

    public string doc_number4 { get; set; }
    public string doc_date4 { get; set; }
    public string doc_date4ST { get; set; }
    public string doc_person_type4 { get; set; }
    public string doc_file4 { get; set; }

    public string doc_number5 { get; set; }
    public string doc_date5 { get; set; }
    public string doc_date5ST { get; set; }
    public string doc_person_type5 { get; set; }
    public string doc_file5 { get; set; }

    public List<CecmProgramSubDeptDTO> lst_dept_edu { get; set; }
    public List<CecmProgramSubDeptDTO> lst_dept_support { get; set; }
    public List<CecmProgramSubDeptDTO> lst_dept_investigate { get; set; }
    public List<CecmProgramSubDeptDTO> lst_dept_survey { get; set; }
    public List<CecmProgramSubDeptDTO> lst_dept_destroy { get; set; }
}

public class CecmProgramSubDeptDTO
{
    public long gid { get; set; }

    public long main_id { get; set; }

    public string table_name { get; set; }

    public string field_name { get; set; }

    public long dept_id { get; set; }

    public string dept_idST { get; set; }

    public string dept_other { get; set; }

    public string dept_phone { get; set; }

    public string dept_email { get; set; }

    public string dept_contact { get; set; }

    public string dept_fax { get; set; }

    public string depthead { get; set; }

    public string depthead_pos { get; set; }

    public string depthead_phone { get; set; }

    public string depthead_email { get; set; }

    public string doc_number { get; set; }

    public string doc_date { get; set; }

    public string doc_dateST { get; set; }

    public string doc_person_type { get; set; }

    public string doc_file { get; set; }

    public string notes { get; set; }

}

//Ô lưới
public class CecmprogramareamapSubDTO
{
    public long gid { get; set; }
    public long cecm_program_areamap_ID { get; set; }
    public string o_id { get; set; }
    public double lat_center { get; set; }
    public double long_center { get; set; }
    public double lat_corner1 { get; set; }
    public double long_corner1 { get; set; }
    public double lat_corner2 { get; set; }
    public double long_corner2 { get; set; }
    public double lat_corner3 { get; set; }
    public double long_corner3 { get; set; }
    public double lat_corner4 { get; set; }
    public double long_corner4 { get; set; }
    public long khaosat_deptId { get; set; }
    public string khaosat_deptIdST { get; set; }
    public long raPha_deptId { get; set; }
    public string raPha_deptIdST { get; set; }
}
