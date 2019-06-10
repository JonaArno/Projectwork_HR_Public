using System;
using System.Collections.Generic;
using System.Text;
using API.Model;

namespace API.DomainOperations.Interfaces
{
    public interface IContractOperations
    {
        IEnumerable<Contract> GetAllContracts();
        Contract GetContractById(int id);
        Contract GetContractByUserId(int userId);
        User RemoveContractFromUser(User user);
        Contract CreateNewContract(Contract contract);
        Contract UpdateContract(Contract contract, decimal newGrossSalary, int newNumberOfHolidays);
        void DeleteContract(Contract contract);
        bool ContractExists(int id);
    }
}
