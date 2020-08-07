using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using TestEmployeeWebAPI.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace TestEmployeeWebAPI.Controllers
{
    public class TestEmployeeController : ApiController
    {
        [AllowAnonymous]
        public HttpResponseMessage GET()
        {
            DataTable table = new DataTable();
            string query = @"
                    select EmployeeID,EmployeeName,Department,MailID,
                    convert(varchar(10),DOJ,120) as DOJ from tblTestEmployee
                    ";

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }


        }
        [AllowAnonymous]
        public string POST(tblTestEmployee emp)
        {

            try
            {
                DataTable table = new DataTable();
                 string query = @"
                   insert into tblTestEmployee
                    (EmployeeName,
                     Department,
                     MailID,
                    DOJ) values (
                    '" + emp.EmployeeName + "'," +
                    "'" + emp.Department + "'," +
                    "'" + emp.MailID + "'," +
                    "'" + emp.DOJ + "')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Employee Added Successfully";
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }
        }
        [AllowAnonymous]
        public string PUT(tblTestEmployee emp)
        {

            try
            {
                DataTable table = new DataTable();
                string query = @"
                   UPDATE tblTestEmployee SET 
                    EmployeeName = '" + emp.EmployeeName + @"'
                    ,Department = '" + emp.Department + @"'
                    ,MailID = '" + emp.MailID + @"'
                    ,DOJ = '" + emp.DOJ + @"'
                    WHERE EmployeeID = "+ emp.EmployeeID +@"";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "update Successfully";
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);

                //return "update failed";
            }
        }
        [AllowAnonymous]
        public string Delete(int id)
        {

            try
            {
                DataTable table = new DataTable();
                string query = @"
                   DELETE FROM tblTestEmployee  WHERE EmployeeID =" + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "deleted Successfully";
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);

                //return "update failed";
            }
        }

    }
}
