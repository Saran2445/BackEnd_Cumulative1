using Cumulative1.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Initializing varaible for the School database context 
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teacher objects.
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers";
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();

                Teacher teacherObj = new Teacher();
                teacherObj.TeacherId = TeacherId;
                teacherObj.TeacherFName = TeacherFname;
                teacherObj.TeacherLName = TeacherLname;
                teacherObj.EmployeeNumber = EmployeeNumber;

                Teachers.Add(teacherObj);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return Teachers;
        }


        /// <summary>
        /// Returns an individual teacher from the database by using the primary key teacherID
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher/1</example>
        /// <param name="id">the teacher's ID in the database</param>
        /// <returns>An Teacher object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher TeacherObj = new Teacher();
            List<Class> Classes = new List<Class>();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL Query
            cmd.CommandText = "SELECT * FROM teachers JOIN classes ON teachers.teacherid= classes.teacherid WHERE teachers.teacherid = " + id.ToString() + ";";
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);

                TeacherObj.TeacherId = TeacherId;
                TeacherObj.TeacherFName = TeacherFname;
                TeacherObj.TeacherLName = TeacherLname;
                TeacherObj.EmployeeNumber = TeacherEmployeeNumber;
                TeacherObj.HireDate = TeacherHireDate;
                TeacherObj.Salary = TeacherSalary;

                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                DateTime ClassStartDate = (DateTime)ResultSet["startdate"];
                DateTime ClassFinishDate = (DateTime)ResultSet["finishdate"];

                Class NewClassObj = new Class();
                NewClassObj.ClassId = ClassId;
                NewClassObj.ClassCode = ClassCode;
                NewClassObj.ClassName = ClassName;
                NewClassObj.StartDate = ClassStartDate;
                NewClassObj.FinishDate = ClassFinishDate;

                //Add the Class details to the List
                Classes.Add(NewClassObj);
                TeacherObj.Classes = Classes;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();


            return TeacherObj;
        }
    }
}
