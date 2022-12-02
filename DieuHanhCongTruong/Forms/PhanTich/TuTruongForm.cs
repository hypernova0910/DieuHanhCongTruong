using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VNRaPaBomMin;
using VNRaPaBomMin.Models;

namespace Charts
{
    public partial class TuTruongForm : Form
    {
        public List<MagnetPoint> m_listPoint = new List<MagnetPoint>();

        private Point[] m_listVertex = null;
        private double[] m_vecTime = null;

        private const int CS_lengthArea = 1000;
        private const int CS_heightArea = 450;
        private const int CS_topX = 40;
        private const int CS_topY = 60;
        private const int CS_lengthDraw = 50;
        private const int CS_topXPaint = 20;
        private const int CS_topYPaint = 20;

        private int m_lengthArea = CS_lengthArea;
        private int m_heightArea = CS_heightArea;
        private int m_topX = CS_topX;
        private int m_topY = CS_topY;
        private double m_xMax, m_xMin, m_yMax, m_yMin;
        private int m_idFrom, m_idTo, m_numberOfPoint;

        private SolidBrush m_brush_background = null;
        private Pen m_pen_boder = null;
        private Pen m_pen_line = null;
        private Pen m_pen_circle = null;
        private SolidBrush m_brush_circle = null;
        private float m_emSize = 14;
        private Font m_fontText = null;
        private SolidBrush m_brushText = null;

        private void CreateRandomPoint(int numPoint)
        {
            ///////////////////////////////////// random data
            DateTime tStart = DateTime.Now;
            MagnetPoint pointMeasure;
            double timeValue = 0;
            Random rdn = new Random();
            for (int i = 0; i < numPoint; i++)
            {
                pointMeasure = new MagnetPoint();
                timeValue = 1 + rdn.Next(0, 10);
                TimeSpan t = TimeSpan.FromSeconds(timeValue);
                pointMeasure.tMeasure = tStart + t;
                pointMeasure.dValue = rdn.Next(-200, 200);
                m_listPoint.Add(pointMeasure);
                tStart = pointMeasure.tMeasure;
            }
        }

        public string _MaMay = string.Empty;
        private SqlConnection cn = null;
        //public string ipAddr = "", databaseName = "", userName = "";

        public TuTruongForm(string maMay)
        {
            InitializeComponent();
            _MaMay = maMay;
            //ipAddr = Settings.Default["IpAddr"].ToString();
            //databaseName = Settings.Default["DatabaseName"].ToString();
            //userName = Settings.Default["UserName"].ToString();

            m_listPoint = /*loadData()*/ GetPointDatabase();
        }

        private List<MagnetPoint> GetPointDatabase()
        {
            try
            {
                List<MagnetPoint> retVal = new List<MagnetPoint>();
                //cn = frmLoggin.sqlCon;
                //SqlCommandBuilder sqlCommand = null;
                //SqlDataAdapter sqlAdapter = null;
                //System.Data.DataTable datatable = new System.Data.DataTable();
                //System.Data.DataTable dataset = null;

                //sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM cecm_data where cecm_data.code = '{0}' ", _MaMay), cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapter);
                //dataset = new System.Data.DataTable();
                //sqlAdapter.Fill(dataset);

                var database = frmLoggin.mgCon.GetDatabase("db_cecm");
                if (database != null)
                {
                    var collection = database.GetCollection<InfoConnect>("cecm_data");
                    var docs = collection.Find(doc => doc.code == _MaMay).ToList();
                    foreach (InfoConnect doc in docs)
                    {
                        //if (!AppUtils.IsNumber(dr["lat_value"].ToString()) || !AppUtils.IsNumber(dr["long_value"].ToString()) || !AppUtils.IsNumber(dr["the_value"].ToString()))
                        //    continue;

                        //x = double.Parse(dr["lat_value"].ToString());
                        //y = double.Parse(dr["long_value"].ToString());
                        //z = double.Parse(dr["the_value"].ToString());

                        //DateTime dateTimeMeasure = DateTime.MinValue;
                        //DateTime.TryParse(dr["update_time"].ToString(), out dateTimeMeasure);

                        MagnetPoint timeTuTruong = new MagnetPoint();
                        timeTuTruong.tMeasure = doc.time_action;
                        timeTuTruong.dValue = doc.the_value;

                        retVal.Add(timeTuTruong);
                    }
                }

                double x, y, z;
                //foreach (DataRow dr in dataset.Rows)
                //{
                //    if (!AppUtils.IsNumber(dr["lat_value"].ToString()) || !AppUtils.IsNumber(dr["long_value"].ToString()) || !AppUtils.IsNumber(dr["the_value"].ToString()))
                //        continue;

                //    x = double.Parse(dr["lat_value"].ToString());
                //    y = double.Parse(dr["long_value"].ToString());
                //    z = double.Parse(dr["the_value"].ToString());

                //    DateTime dateTimeMeasure = DateTime.MinValue;
                //    DateTime.TryParse(dr["update_time"].ToString(), out dateTimeMeasure);

                //    MagnetPoint timeTuTruong = new MagnetPoint();
                //    timeTuTruong.tMeasure = dateTimeMeasure;
                //    timeTuTruong.dValue = z;

                //    retVal.Add(timeTuTruong);
                //}
                

                return retVal.OrderBy(val => val.tMeasure).ToList();
            }
            catch (System.Exception ex)
            {
                //MyLogger.Log("Có lỗi trong quá trình đọc điểm từ CSDL", ex.Message);
                return null;
            }
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            if ((control.Size.Height < CS_heightArea) ||
                (control.Size.Width < CS_lengthArea))
            // Ensure the Form remains square (Height = Width).
            {
                m_lengthArea = CS_lengthArea;
                m_heightArea = CS_heightArea;
                control.Size = new Size(m_lengthArea + m_topX, m_heightArea + m_topY);
            }
            else
            {
                m_lengthArea = control.Size.Width - 2 * m_topX - 20;
                m_heightArea = control.Size.Height - m_topY - 60;
                CreateVertexBuffer(m_topX, m_topY, m_lengthArea, m_heightArea);
            }
        }

        private void TuTruongForm_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            if ((control.Size.Height < CS_heightArea) ||
                (control.Size.Width < CS_lengthArea))
            // Ensure the Form remains square (Height = Width).
            {
                m_lengthArea = CS_lengthArea;
                m_heightArea = CS_heightArea;
                control.Size = new Size(CS_lengthArea + 2 * CS_topX + 2 * CS_topXPaint, CS_heightArea + CS_topY * 2 + 2 * CS_topYPaint);
            }
            else
            {
                m_lengthArea = control.Size.Width - 2 * (m_topX + CS_topXPaint);
                m_heightArea = control.Size.Height - 2 * (m_topY + CS_topYPaint);
                CreateVertexBuffer(m_topX, m_topY, m_lengthArea, m_heightArea);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitPaintObject();
            this.Size = new Size(CS_lengthArea + 2 * CS_topX + 2 * CS_topXPaint, CS_heightArea + CS_topY * 2 + 2 * CS_topYPaint);

            this.ResizeRedraw = true; // redraw form when resize

            if (m_listPoint.Count < 1)
                return;

            int idTo = 0;
            if (m_listPoint.Count > CS_lengthDraw)
            {
                SBControlDraw.Maximum = m_listPoint.Count - CS_lengthDraw;
                idTo = CS_lengthDraw;
            }
            else
            {
                SBControlDraw.Maximum = m_listPoint.Count - 1;
                idTo = SBControlDraw.Maximum;
            }

            SBControlDraw.Minimum = 0;
            int idFrom = 0;
            labelGraph.Text = "Biểu đồ từ trường " + m_listPoint[idFrom].tMeasure.ToString("dd/MM/yyyy HH:mm:ss") + " đến " + m_listPoint[idTo].tMeasure.ToString("dd/MM/yyyy HH:mm:ss");
            ////////////////////////////////// create vertex buffer
            FindMinMax(idFrom, idTo);
            CreateVertexBuffer(m_topX, m_topY, m_lengthArea, m_heightArea);
        }

        private int FindMinMax(int idFrom, int idTo)
        {
            m_numberOfPoint = idTo - idFrom;
            m_idTo = idTo;
            m_idFrom = idFrom;
            if ((m_numberOfPoint <= 0) ||
                (m_numberOfPoint > m_listPoint.Count))
                return 0;
            /////////////////////////////// find min, max of X,Y
            m_xMin = m_xMax = 0;
            m_yMin = m_yMax = m_listPoint[idFrom].dValue;

            DateTime tStart = m_listPoint[idFrom].tMeasure;
            if (m_vecTime != null && m_vecTime.Length > 0)
                Array.Clear(m_vecTime, 0, m_vecTime.Length);

            m_vecTime = new double[m_numberOfPoint];
            m_vecTime[0] = 0.0;

            double tTotal = 0;
            int index;
            for (int i = 1; i < m_numberOfPoint; i++)
            {
                index = i + idFrom;
                TimeSpan t = m_listPoint[index].tMeasure - tStart;
                tTotal = t.TotalSeconds;
                m_vecTime[i] = tTotal;
                m_yMin = Math.Min(m_listPoint[index].dValue, m_yMin);
                m_yMax = Math.Max(m_listPoint[index].dValue, m_yMax);
            }
            m_xMax = tTotal;
            return m_numberOfPoint;
        }

        private int CreateVertexBuffer(int topX, int topY, int lengthArea, int heightArea)
        {
            double deltaX = m_xMax - m_xMin;
            double deltaY = m_yMax - m_yMin;
            if (deltaX == 0.0)
                deltaX = 1.0;
            if (deltaY == 0.0)
                deltaY = 1.0;
            ///////////////////////////// calculate scale X, scale Y
            double dScaleX = (double)lengthArea / deltaX;
            double dScaleY = (double)heightArea / deltaY;

            if (m_listVertex != null && m_listVertex.Length > 0)
                Array.Clear(m_listVertex, 0, m_listVertex.Length);
            m_listVertex = new Point[m_numberOfPoint];

            int index;
            for (int i = 0; i < m_numberOfPoint; i++)
            {
                index = i + m_idFrom;
                Point point = new Point();
                point.X = topX + (int)((m_vecTime[i] - m_xMin) * dScaleX);
                point.Y = (topY + heightArea) - (int)((m_listPoint[index].dValue - m_yMin) * dScaleY);
                m_listVertex[i] = point;
            }
            return m_numberOfPoint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraphMeasure(e, m_topX, m_topY, m_lengthArea, m_heightArea);
        }

        private void InitPaintObject()
        {
            m_brush_background = new SolidBrush(Color.Black);
            m_pen_boder = new Pen(Color.Gray);
            m_pen_line = new Pen(Color.Red);
            m_pen_circle = new Pen(Color.SeaGreen);
            m_brush_circle = new SolidBrush(Color.Purple);
            m_emSize = 14;
            m_fontText = new Font(FontFamily.GenericSansSerif, m_emSize, FontStyle.Regular);
            m_brushText = new SolidBrush(Color.White);//new SolidBrush(Color.Aquamarine);
        }

        private void DrawGraphMeasure(PaintEventArgs e, int topX, int topY, int lengthArea, int heightArea)
        {
            int numberOfSection = m_listVertex.Length;
            if (numberOfSection < 2)
                return;
            Rectangle rec = new Rectangle(topX - CS_topXPaint, topY - CS_topYPaint, lengthArea + 3 * CS_topXPaint, heightArea + 3 * CS_topYPaint);
            e.Graphics.FillRectangle(m_brush_background, rec);
            e.Graphics.DrawRectangle(m_pen_boder, rec);
            e.Graphics.DrawLines(m_pen_line, m_listVertex);

            for (int i = 0; i < numberOfSection; i++)
            {
                //  e.Graphics.DrawLine(pen, m_listVertex[i - 1], m_listVertex[i]);
                DrawDots(e, m_listVertex[i], m_pen_circle, m_brush_circle);
                DrawText(e, m_listVertex[i], m_idFrom + i, m_fontText, m_brushText);
            }
        }

        private void DrawDots(PaintEventArgs e, Point p1, Pen pen, SolidBrush brush)
        {
            e.Graphics.DrawPie(pen, p1.X - 5, p1.Y - 5, 10, 10, 0, 360);
            e.Graphics.FillPie(brush, p1.X - 5, p1.Y - 5, 10, 10, 0, 360);
        }

        private void DrawText(PaintEventArgs e, Point p1, int index, Font fontText, SolidBrush brushText)
        {
            string letter = String.Format("{0:0.##}", m_listPoint[index].dValue);
            e.Graphics.DrawString(letter, fontText, brushText, p1.X + 6, p1.Y - 6);
        }

        private void SelectDraw(object sender, EventArgs e)
        {
            int idFrom = SBControlDraw.Value;
            int idTo = idFrom + CS_lengthDraw;
            if(m_listPoint.Count <= idFrom || m_listPoint.Count <= idTo)
            {
                return;
            }
            labelGraph.Text = "Biểu đồ từ trường " + m_listPoint[idFrom].tMeasure.ToString("dd/MM/yyyy HH:mm:ss") + " đến " + m_listPoint[idTo].tMeasure.ToString("dd/MM/yyyy HH:mm:ss");
            ////////////////////////////////// create vertex buffer
            FindMinMax(idFrom, idTo);
            CreateVertexBuffer(m_topX, m_topY, m_lengthArea, m_heightArea);
            Invalidate();
        }
    }
}