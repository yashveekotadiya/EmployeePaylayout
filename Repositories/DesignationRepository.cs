using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrud.Models;
using Npgsql;

namespace EmployeeCrud.Repositories
{
    public class DesignationRepository:CommonRepository,IDesignationRepository
    {


          private readonly IHttpContextAccessor _httpContextAccessor;
        public DesignationRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Designation> GetDesignations()
        {
            List<Designation> designations = new List<Designation>();
            using (conn)
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM t_designation117", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var designation = new Designation
                            {
                                c_id = Convert.ToInt32(reader["c_id"]),
                                c_designation = reader["c_designation"].ToString(),
                            };
                            designations.Add(designation);
                        }
                    }
                }
            }
            return designations;
        }
    }
}