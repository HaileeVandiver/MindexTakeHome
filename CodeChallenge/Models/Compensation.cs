using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public string Employee { get; set; }
        public int Salary { get; set; }
        public string EffectiveDate { get; set; }


    }
}   
