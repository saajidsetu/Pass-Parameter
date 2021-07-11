using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace PassParameter.Controllers
{
    public class HomeController : ApiController
    {
        SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());
        //SqlCommand _cmd = new SqlCommand();
        //SqlDataAdapter adp = null;
        public string Index(string id, string name)
        {
            return "ID =" + id + "<br /> Name=" + name;
        }


        public bool GetStudentLogin(string studentid, string password)
        {
            bool resultret = false;

            try
            {
                if (studentid != "" && password != "")
                {

                    SqlParameter[] parms =
                    {
                    new SqlParameter("@UserID",studentid.ToString().Trim()),
                    new SqlParameter("@Password",FormsAuthentication.HashPasswordForStoringInConfigFile(password.ToString().Trim(), "SHA1")),
                    new SqlParameter("@LogDetails",""),
                };


                    SqlCommand cmd = new SqlCommand("logInStudent", _con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parms);
                    _con.Open();
                    string result = (string)cmd.ExecuteScalar();
                    if (result.CompareTo("0") != 0)
                    {
                        resultret = true;

                    }
                    else
                    {
                        resultret = false;
                    }
                    //else if (result == "0")// lblMessage.InnerText = "error->Incorrect UserID or Password";
                    //else //lblMessage.InnerText = "error->Database error. Please contract technical person";
                }
                else
                {
                    resultret = false;
                }
                _con.Close();
            }
            catch (Exception ex)
            {
                //  lblMessage.InnerText = "error->" + ex.Message;
            }
            return resultret;
        }

       
    }
}
