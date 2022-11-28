using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using System.Threading;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            //bonus points: how would you deal with a primary key exception -o- put a primary key constraint on database
            ReportingStructure reportingStructure = new();
            //checking if the string is not null or empty 
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }

            //Thread.Sleep(5000);
           
            Employee foundEmployee = _employeeRepository.GetById(id);
            //checking if the id is actually in the database
            if (foundEmployee == null) 
            {
                return null; 
            }

            //build reporting structure 
            reportingStructure.Employee = foundEmployee.EmployeeId; 
            reportingStructure.NumberOfReports = GetNumOfReports(foundEmployee);

            return reportingStructure;
        }

        private int GetNumOfReports(Employee employee)
        {
            int reporteeCount = 0;
            if (employee == null)
            {
                return 0;
            }
            if (employee.DirectReports == null)
            {
                return 0;
            }
            reporteeCount += employee.DirectReports.Count;
            foreach (Employee reportee in employee.DirectReports)
            {
                if (reportee != null)
                {
                    reporteeCount += GetNumOfReports(reportee);
                }
            }
            return reporteeCount;
        }








    }
}
    
