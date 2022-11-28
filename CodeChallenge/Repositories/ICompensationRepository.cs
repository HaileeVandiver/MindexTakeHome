using CodeChallenge.Models;
using System.Threading.Tasks;
using System;

namespace CodeChallenge.Repositories
{
    //separates the contract and the implementation
    public interface ICompensationRepository
    {
        Compensation GetById(String id);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}
