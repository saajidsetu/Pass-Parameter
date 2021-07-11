using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PassParameter.Models;

namespace PassParameter.Controllers
{
    public class StudentinfoController : ApiController
    {
        SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());



        [HttpGet]
        [ActionName("GetStudentinfo")]
        public List<ClsStudentinfo> Get(string studentid)
        {
            //return listEmp.First(e => e.ID == id);  
            // SqlDataReader reader = null;

            // SqlCommand sqlCmd = new SqlCommand();
            List<ClsStudentinfo> lststudent = new List<ClsStudentinfo>();

            SqlCommand sqlCmd = new SqlCommand("select qm.WeekID,qd.WeekNo,qd.ParaNo,qm.Status,qd.Year from QuranReadingMaster qm left outer join QuranReadingDetails qd on qm.WeekID = qd.WeekID where qd.StudentID = '" + studentid + "' and qd.Status not in('true') and qm.Status not in('1')", _con);
            //sqlCmd.CommandType = CommandType.Text;
            //sqlCmd.CommandText = "select qm.WeekID,qd.WeekNo,qd.ParaNo,qm.Status,qd.Status from QuranReadingMaster qm left outer join QuranReadingDetails qd on qm.WeekID = qd.WeekID where qd.StudentID = '" + studentid + "' and qd.Status not in('true') and qm.Status not in('1')";
            SqlDataAdapter da = new SqlDataAdapter();
            _con.Open();

            da.SelectCommand = sqlCmd;
            DataSet ds = new DataSet();

            ///conn.Open();
            da.Fill(ds);
            //  reader = sqlCmd.ExecuteReader();
            ClsStudentinfo studentdetails = null;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                studentdetails = new ClsStudentinfo();//WeekID	WeekNo	ParaNo	Status	Status


                studentdetails.WeekID = Convert.ToInt32(ds.Tables[0].Rows[i]["WeekID"].ToString());
                studentdetails.WeekNo = ds.Tables[0].Rows[i]["WeekNo"].ToString();
                studentdetails.ParaNo = ds.Tables[0].Rows[i]["ParaNo"].ToString();
                studentdetails.Year= ds.Tables[0].Rows[i]["Year"].ToString();

                lststudent.Add(studentdetails);
            }

            return lststudent;
            _con.Close();
        }

        [HttpGet]
        [ActionName("GetStudentname")]
        public string Getname(string studentid)
        {

            string name = "";

            SqlCommand cmd2 = new SqlCommand("select StudentName from Student_Info where StudentID='" + studentid + "'", _con);

            SqlDataAdapter da = new SqlDataAdapter();
            _con.Open();
            da.SelectCommand = cmd2;
            DataSet ds = new DataSet();

            da.Fill(ds);
            //  reader = sqlCmd.ExecuteReader();
          

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    studentdetails = new ClsStudentinfo();//WeekID	WeekNo	ParaNo	Status	Status

            if(ds.Tables[0].Rows.Count==1)
            {
                name =ds.Tables[0].Rows[0]["StudentName"].ToString();
            }

            return name;
            _con.Close();
        }

        [HttpPost]
        public  string Updatequrandetails(ClsStudentupdate studentinfo)
        {
            //int maxId = listEmp.Max(e => e.ID);  
            //employee.ID = maxId + 1;  
            //listEmp.Add(employee);  

            try
            {
                //SqlConnection myConnection = new SqlConnection();
                //myConnection.ConnectionString = @"Server=.\SQLSERVER2008R2;Database=DBCompany;User ID=sa;Password=xyz@1234;";

                string msg = "Fail";

                //SqlCommand sqlCmd = new SqlCommand("INSERT INTO tblEmployee (EmployeeId,Name,ManagerId) Values (@EmployeeId,@Name,@ManagerId)", myConnection);  
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE QuranReadingDetails SET Status = 1 WHERE WeekID='" + studentinfo.WeekID.ToString() + "' and StudentID='" + studentinfo.Studentid.ToString() + "' and Year='" + studentinfo.Year.ToString() + "' and WeekNo='" + studentinfo.WeekNo.ToString() + "' and ParaNo='" + studentinfo.ParaNo.ToString() + "'";
                sqlCmd.Connection = _con;

                _con.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();

                if(rowInserted==1)
                {
                    msg = "Success";

                }


                _con.Close();

                return msg;
            }
            catch(System.Exception ex)
            {
                throw (ex);
            }
        }

        //SqlCommand cmd2 = new SqlCommand("select StudentName from Student_Info where StudentID='" + studentid + "'", _con);

        //SqlDataAdapter da = new SqlDataAdapter();

        //da.SelectCommand = cmd2;
        //                DataSet ds = new DataSet();

        //da.Fill(ds);


        //public List<ClsStudentinfo> Get(string studentid)
        //{

        //    string qry = "select qm.WeekID,qd.WeekNo,qd.ParaNo,qm.Status,qd.Status from QuranReadingMaster qm left outer join QuranReadingDetails qd on qm.WeekID = qd.WeekID where qd.StudentID = '"+ studentid + "' and qd.Status not in('true') and qm.Status not in('1')";
        //    SqlCommand cmd = new SqlCommand(qry, _con);

        //    _con.Open();

        //    string result = (string)cmd.ExecuteScalar();

        //    if (result.CompareTo("0") != 0)
        //    {
        //    }
        //        var data = new List<ClsStudentinfo>()
        //{
        //    new ClsStudentinfo() {WeekID = ""},
        //    new ClsStudentinfo() {WeekNo = ""},
        //    new ClsStudentinfo() {ParaNo = ""}
        //};

        //    return data;
        //}



    }
}
