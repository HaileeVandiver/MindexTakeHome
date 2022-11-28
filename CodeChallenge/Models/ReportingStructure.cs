namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        //made this not an employee type because do not need access to entire employee object only id and reportees 
        public string Employee { get; set; }
        public int NumberOfReports { get; set; }
    }
}
