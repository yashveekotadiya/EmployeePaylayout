using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrud.Models;
using Npgsql;

namespace EmployeeCrud.Repositories
{
    public class EmployeeRepository : CommonRepository, IEmployeeRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public List<Employee> GetAllEmployee()
        {
            List<Employee> EmployeeList = new List<Employee>();


            conn.Open();


            using (var cmd = new NpgsqlCommand("SELECT c_id,c_employeename,c_hiredate,c_gender,c_salary,c_designationid FROM t_employee117", conn))
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var employee = new Employee
                    {
                        c_id = Convert.ToInt32(dr["c_id"]),
                        c_employeename = dr["c_employeename"].ToString(),
                        c_hiredate = Convert.ToDateTime(dr["c_hiredate"]).Date,
                        c_gender = dr["c_gender"].ToString(),
                        c_salary = Convert.ToDecimal(dr["c_salary"]),
                        c_designationid = Convert.ToInt32(dr["c_designationid"]),

                    };

                    EmployeeList.Add(employee);
                }
            }
            conn.Close();
            return EmployeeList;
        }

        // public List<Designation> GetDesignations()
        //         {
        //             List<Designation> designations = new List<Designation>();
        //             using (conn)
        //             {
        //                 conn.Open();
        //                 using (var cmd = new NpgsqlCommand("SELECT * FROM t_designation115", conn))
        //                 {
        //                     using (var reader = cmd.ExecuteReader())
        //                     {
        //                         while (reader.Read())
        //                         {
        //                             var designation = new Designation
        //                             {
        //                                 c_id = Convert.ToInt32(reader["c_id"]),
        //                                 c_designation = reader["c_designation"].ToString(),
        //                             };
        //                             designations.Add(designation);
        //                         }
        //                     }
        //                 }
        //             }
        //             return designations;
        //         }




        // public Employee GetOneEmployee(int id)
        // {
        //     var employee = new Employee();

        //     conn.Open();

        //     using (var cmd = new NpgsqlCommand("SELECT c_id,c_employeename,c_hiredate,c_gender,c_salary,c_designationid ,c_basic,c_da,c_hra,c_taxable_salary,c_tax FROM t_employee117 WHERE c_id = @Id", conn))
        //     {
        //         cmd.Parameters.AddWithValue("@Id", id);
        //         using (var dr = cmd.ExecuteReader())
        //         {
        //             while (dr.Read())
        //             {
        //                 // employee.c_id = Convert.ToInt32(dr["c_id"]);
        //                 // employee.c_employeename = dr["c_employeename"].ToString();
        //                 // employee.c_hiredate = Convert.ToDateTime(dr["c_hiredate"]).Date;
        //                 // employee.c_gender = dr["c_gender"].ToString();
        //                 // employee.c_salary = Convert.ToDecimal(dr["c_salary"]);
        //                 // employee.c_designationid = Convert.ToInt32(dr["c_designationid"]);
        //                 // employee.Basic = Convert.ToDecimal(dr["c_basic"]);
        //                 // employee.DA = Convert.ToDecimal(dr["c_da"]);
        //                 // employee.HRA = Convert.ToDecimal(dr["c_hra"]);
        //                 // employee.Tax = Convert.ToDecimal(dr["c_taxable_salary"]);
        //                 // employee.TakeHomePay = Convert.ToDecimal(dr["c_tax"]);
        //             employee.c_id = Convert.ToInt32(reader["c_id"]),
        //             employee.c_employeename = reader["c_employeename"].ToString(),
        //             employee.c_hiredate = Convert.ToDateTime(reader["c_hiredate"]),
        //             employee.c_gender = reader["c_gender"].ToString(),
        //             employee.c_designationid = Convert.ToInt32(reader["c_designationid"]),
        //             employee.c_salary = Convert.ToDecimal(reader["c_salary"]),
        //             // Handle DBNull for other fields
        //             employee.c_basic = reader["c_basic"] != DBNull.Value ? Convert.ToDecimal(reader["c_basic"]) : 0,
        //             employee.c_da = reader["c_da"] != DBNull.Value ? Convert.ToDecimal(reader["c_da"]) : 0,
        //             employee.c_hra = reader["c_hra"] != DBNull.Value ? Convert.ToDecimal(reader["c_hra"]) : 0,
        //             employee.c_tax = reader["c_tax"] != DBNull.Value ? Convert.ToDecimal(reader["c_tax"]) : 0,
        //             employee.c_takehomepay = reader["c_takehomepay"] != DBNull.Value ? Convert.ToDecimal(reader["c_takehomepay"]) : 0
        //             }
        //         }
        //     }
        //     conn.Close();
        //     return employee;
        // }
        public Employee GetOneEmployee(int id)
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM t_employee117 WHERE c_id = @EmployeeId";
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            c_id = Convert.ToInt32(reader["c_id"]),
                            c_employeename = reader["c_employeename"].ToString(),
                            c_hiredate = Convert.ToDateTime(reader["c_hiredate"]),
                            c_gender = reader["c_gender"].ToString(),
                            c_designationid = Convert.ToInt32(reader["c_designationid"]),
                            c_salary = Convert.ToDecimal(reader["c_salary"]),
                            // Handle DBNull for other fields
                            Basic = reader["c_basic"] != DBNull.Value ? Convert.ToDecimal(reader["c_basic"]) : 0,
                            DA = reader["c_da"] != DBNull.Value ? Convert.ToDecimal(reader["c_da"]) : 0,
                            HRA = reader["c_hra"] != DBNull.Value ? Convert.ToDecimal(reader["c_hra"]) : 0,
                            Tax = reader["c_tax"] != DBNull.Value ? Convert.ToDecimal(reader["c_tax"]) : 0,
                            Taxable = reader["c_taxable_salary"] != DBNull.Value ? Convert.ToDecimal(reader["c_taxable_salary"]) : 0,
                            TakeHomePay = reader["c_takehomepay"] != DBNull.Value ? Convert.ToDecimal(reader["c_takehomepay"]) : 0
                        };
                        conn.Close();
                        return employee;

                    }
                    else
                    {
                        return null; // Employee not found
                    }
                }

            }


        }







        public void InsertEmployee(Employee employee)
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("INSERT INTO t_employee117 (c_employeename, c_hiredate, c_gender, c_salary, c_designationid) VALUES (@Name, @HireDate, @Gender, @Salary, @DesignationId)", conn))
            {
                cmd.Parameters.AddWithValue("@Name", employee.c_employeename);
                cmd.Parameters.AddWithValue("@HireDate", employee.c_hiredate);
                cmd.Parameters.AddWithValue("@Gender", employee.c_gender);
                cmd.Parameters.AddWithValue("@Salary", employee.c_salary);
                cmd.Parameters.AddWithValue("@DesignationId", employee.c_designationid);

                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }





        public void UpdateEmployeePayroll(int employeeId)
        {
            try
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE t_employee117 SET c_basic = c_salary * 0.6, c_da = c_salary * 0.25, c_hra = c_salary * 0.15, c_taxable_salary = CASE WHEN c_salary > 25000 THEN c_salary - 25000 ELSE c_salary END, c_tax = CASE WHEN c_salary > 25000 THEN (c_salary - 25000) * 0.1 ELSE 0 END, c_takehomepay = (c_salary * 0.6) + (c_salary * 0.25) + (c_salary * 0.15) - (CASE WHEN c_salary > 25000 THEN (c_salary - 25000) * 0.1 ELSE 0 END) WHERE c_id = @EmployeeId";

                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                conn.Close();
            }
        }

        // public void UpdateEmployee(Employee employee)
        // {
        //     conn.Open();

        //     using (var cmd = new NpgsqlCommand("UPDATE t_employee117 SET c_employeename = @Name, c_hiredate = @HireDate, c_gender = @Gender, c_salary = @Salary, c_designationid = @DesignationId WHERE c_id = @Id", conn))
        //     {
        //         cmd.Parameters.AddWithValue("@Name", employee.c_employeename);
        //         cmd.Parameters.AddWithValue("@HireDate", employee.c_hiredate);
        //         cmd.Parameters.AddWithValue("@Gender", employee.c_gender);
        //         cmd.Parameters.AddWithValue("@Salary", employee.c_salary);
        //         cmd.Parameters.AddWithValue("@DesignationId", employee.c_designationid);
        //         cmd.Parameters.AddWithValue("@Id", employee.c_id);

        //         cmd.ExecuteNonQuery();
        //     }

        //     conn.Close();
        // }
        public void UpdateEmployee(Employee employee)
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("UPDATE t_employee117 SET c_employeename = @Name, c_hiredate = @HireDate, c_gender = @Gender, c_salary = @Salary, c_designationid = @DesignationId WHERE c_id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Name", employee.c_employeename);
                cmd.Parameters.AddWithValue("@HireDate", employee.c_hiredate);
                cmd.Parameters.AddWithValue("@Gender", employee.c_gender);
                cmd.Parameters.AddWithValue("@Salary", employee.c_salary);
                cmd.Parameters.AddWithValue("@DesignationId", employee.c_designationid);
                cmd.Parameters.AddWithValue("@Id", employee.c_id);

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }





        public void DeleteEmployee(Employee employee)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM t_employee117 WHERE c_id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", employee.c_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }







    }
}