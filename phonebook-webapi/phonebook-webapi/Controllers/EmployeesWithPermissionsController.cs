using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using phonebook_core;
using phonebook_core.BusinessObjects;

using phonebook_webapi.Logging;
using phonebook_webapi.Models;

namespace phonebook_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesWithPermissionsController : ControllerBase
    {
        private readonly ILoggerManager logger;

        public EmployeesWithPermissionsController(ILoggerManager logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<EmployeeItem> Get()
        {
            try
            {
                var employees = EmployeeManager.GetEmployeesWithPermissions();
                return employees;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }
    }
}
