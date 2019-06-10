using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Data;
using API.DomainOperations.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.DomainOperations
{
    public class ContractOperations : IContractOperations
    {
        private readonly HrApplicationContext _context;
        public ContractOperations(HrApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            return _context.Contracts.ToList();
        }

        public Contract GetContractById(int id)
        {
            return _context.Contracts.First(cont => cont.ContractID == id);
        }

        public Contract GetContractByUserId(int userId)
        {
            return _context.Contracts.First(c => c.User.UserID == userId);
        }

        public User RemoveContractFromUser(User user)
        {
            user.Contract = null;
            user.LastModified = DateTime.Now;
            _context.Users.Update(user);
            if (!Save()) throw new Exception($"Issue while removing contract from user with id {user.UserID}.");
            return user;
        }

        public Contract CreateNewContract(Contract contract)
        {
            contract.CreationDate = DateTime.Now;
            contract.LastModified = DateTime.Now;
            _context.Contracts.Add(contract);
            if (!Save()) throw new Exception($"Error while saving contract to database");
            return contract;
        }

        public Contract UpdateContract(Contract contract, decimal newGrossSalary, int newNumberOfHolidays)
        {
            contract.GrossSalary = newGrossSalary;
            contract.NumberOfHolidays = newNumberOfHolidays;
            contract.LastModified = DateTime.Now;
            _context.Contracts.Update(contract);
            if (!Save()) throw new Exception($"Issue while saving updated contract with id {contract.ContractID} to the database.");
            return contract;
        }

        public void DeleteContract(Contract contract)
        {
            _context.Contracts.Remove(contract);
            if (!Save()) throw new Exception($"Issue removing contract with id {contract.ContractID} from the database.");
        }

        public bool ContractExists(int id)
        {
            return _context.Contracts.Any(cont => cont.ContractID == id);
        }
        
        private bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}
