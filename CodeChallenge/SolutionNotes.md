# Notes on Solving Process and Further Work To Be Done

## Setup
* Opened solution in Visual Studio
* Set up Github repository 
* Began Task #1 
* Began Task #2 
* Added services, Dbs, and build to App.cs file
* Ran Tests - ran into issues with the existing threading in EmployeeControllerTests 
    * Issue: Tests would run and fail, however, if I waited the test would populate and pass. Thought it was an issue with the Async 
    * Solution: Forced loading all of employee data in EmployeeRepository first and then put it into a list 
     * Added getRequestTask.Wait(); in ControllerTests

## Task 1
* Created ReportingStructure Class  
  * public string Employee { get; set; } 
    * Made this a string and not an Employee type because we don't need the entire Employee object, just the id 
  * public int NumberOfReports { get; set; }

* Added functions in EmployeeService 
  * public ReportingStructure GetReportingStructureById(string id)
  * private int GetNumOfReports(Employee employee)
    * This is recursive instead of a series of nested loops to allow for it to drill down as far as it needs to go. 
* Created REST endpoint in EmployeeController
  * [HttpGet("{id}/NumberOfReports", Name = "GetReportsbyId")]
        public IActionResult GetReportsbyId(String id)



## Task 2
* Created Compensation Class 
  * public string Employee { get; set; }
  * public int Salary { get; set; }
  * public string EffectiveDate { get; set; }
    * kept this as a string instead of a DateTime for ease of use in this project 

* Created ICompensationService File 
  * public interface ICompensationService

* Created CompensationService File
  * public Compensation Create(Compensation compensation)
  * public Compensation GetCompById(string id) 

* CompensationRepository and ICompensationRepository files 

* CompensationContext 

* CompensationDataSeeder 
  * Modeled the Context and DataSeeder files after the employee context and dataseeder files 

* CompensationController 
  * REST endpoint for creating compensation 
    * [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
  * REST endpoint for getting compensation by employeeid 
    * [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)


## Testing 

* Added to EmployeeControllerTests 
  *   [TestMethod]
        public void GetReportingStructureById_Returns_Ok()
  *    [TestMethod]
        public void GetReportingStructure_Returns_NotFound() 
* This is the section where I ran into an issue with the existing threading. Where tests would not pass, but then given time and running the debugger, would pass. 
* I resolved this by putting the data into a list. In a real life situation this wouldn't be ideal for a very large list, but for this instance it works. 
* Also changed var GetRequestTask to  
    * Task<HttpResponseMessage> getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}/NumberOfReports");
            getRequestTask.Wait();

* Created CompensationControllerTests 
  * Initialized class 
  *    [TestMethod]
            public void CreateCompensation_Returns_Compensation()
  *[TestMethod]
            public void GetCompensation_Returns_NotFound()


## Future Work 
* IRL I would want to look more into validation of data being used. For example, making sure employeeids are only the amount of numbers/letters we expect. 
    * preferably this would happen as soon as the data is received from the external party 
    *Minimum and maximum value range check for numerical parameters and dates, minimum and maximum length check for strings.
    Array of allowed values for small sets of string parameters (e.g. days of week).
    Regular expressions for any other structured data covering the whole input string (^...$) and not using "any character" wildcard (such as . or \S)
 * Look into DDOS attack prevention with the HTTP requests 
 * Need to resolve the context and threading issue that came baked in in the code, that would be a much larger project. I'd want to restructure the code. Not sure how yet
  
  ## Conclusions 
  *Task 1 is thoroughly completed with tests working 
  *Task 2 has it's methods, controllers, and tests, but I am having trouble implementing at the persistence layer. Partially because of the threading issue
  partially because this is a new concept for me (dependency injection in the database) looking forward to learning more about it! HV 11/28/22
