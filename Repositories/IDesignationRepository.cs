using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrud.Models;

namespace EmployeeCrud.Repositories
{
    public interface IDesignationRepository 
    {
        List<Designation> GetDesignations();
    }
}