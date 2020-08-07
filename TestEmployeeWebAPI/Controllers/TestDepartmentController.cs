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
    public class TestDepartmentController : ApiController
    {
        [AllowAnonymous]
        [Route("api/TestDepartment")]
        public HttpResponseMessage GET()
        {
            DataTable table = new DataTable();
               string query = @"
                    select DepartmentID,DepartmentName from tblTestDepartment
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
        [Route("api/TestDepartment")]
        public string POST(tblTestDepartment dep)
        {

            try
            {
                DataTable table = new DataTable();
                string query = @"
                   insert into tblTestDepartment values ('"+dep.DepartmentName+"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch (Exception)
            {
                return "failed Add";
            }
        }

        [AllowAnonymous]
        [Route("api/TestDepartment")]
        public string Put(tblTestDepartment dep)
        {

            try
            {
                DataTable table = new DataTable();
                string query = @"
                   UPDATE tblTestDepartment SET DepartmentName = '"+ dep.DepartmentName +"' WHERE DepartmentID =" +dep.DepartmentID ;

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
        [Route("api/TestDepartment/{id}")]
        public string Delete(int id)
        {

            try
            {
                DataTable table = new DataTable();
                string query = @"
                   DELETE FROM tblTestDepartment  WHERE DepartmentID =" + id;

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
