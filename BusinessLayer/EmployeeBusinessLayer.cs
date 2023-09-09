using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;

namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        private string connectionString { get; }
        //private Employee employee { get; set; }
        public EmployeeBusinessLayer()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;            
        }
        public List<Employee> GetAllEmployess()
        {
            //Reads the connection string from web.config file. The connection string name is DBCS
            //string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //Create List of employees collection object which can store list of employees
            List<Employee> employees = new List<Employee>();
            //Establish the Connection to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //Creating the command object by passing the stored procedure that is used to
                //retrieve all the employess from the tblEmployee table and the connection object
                //on which the stored procedure is going to execute
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                //Specify the command type as stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                //Open the connection
                con.Open();
                //Execute the command and stored the result in Data Reader as the method ExecuteReader
                //is going to return a Data Reader result set
                SqlDataReader rdr = cmd.ExecuteReader();
                //Read each employee from the SQL Data Reader and stored in employee object
                while (rdr.Read())
                {
                    //Creating the employee object to store employee information
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]);
                    //Adding that employee into List of employees collection object
                    employees.Add(employee);
                }
            }
            //Return the list of employees that is stored in the list collection of employees
            return employees;
        }

        public void AddEmployee(Employee employee)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //@Name, @Gender, @City,@Salary, @DateOfBirth
                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = employee.Name
                };                
                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = employee.Gender
                };                
                SqlParameter parameterCity = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = employee.City
                };                
                SqlParameter parameterSalary = new SqlParameter
                {
                    ParameterName = "@Salary",
                    Value = employee.Salary
                };
                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = employee.DateOfBirth
                };

                cmd.Parameters.Add(parameterName);
                cmd.Parameters.Add(parameterGender);
                cmd.Parameters.Add(parameterCity);
                cmd.Parameters.Add(parameterSalary);
                cmd.Parameters.Add(parameterDateOfBirth);

                connection.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = employee.ID                    
                };
                SqlParameter Name = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = employee.Name                    
                };
                SqlParameter Gender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = employee.Gender                    
                };
                SqlParameter Salary = new SqlParameter
                {
                    ParameterName = "@Salary",
                    Value = employee.Salary
                };
                SqlParameter DateOfBirh = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = employee.DateOfBirth                    
                };
                SqlParameter City = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = employee.City                   
                };
                cmd.Parameters.Add(Id);
                cmd.Parameters.Add(Name);
                cmd.Parameters.Add(Gender);
                cmd.Parameters.Add(Salary);
                cmd.Parameters.Add(City);
                cmd.Parameters.Add(DateOfBirh);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", connection) 
                { 
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@Id", Value = id
                };

                cmd.Parameters.Add(Id);
                connection.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public Employee GetEmployeeDetails(int id)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Employee  employee = new Employee();
                SqlCommand cmd = new SqlCommand("spEmployeeDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                cmd.Parameters.Add(Id);
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //Creating the employee object to store employee information                    
                    employee.ID = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]);
                    //Adding that employee into List of employees collection object                    
                }
                return employee;
            }
        }

    }
}
