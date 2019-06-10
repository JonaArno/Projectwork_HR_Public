using System;
using System.Collections.Generic;
using System.Text;
using API.DomainOperations.Interfaces;
using API.DomainServices.Interfaces;
using API.DTO;
using API.DTO.Contract;
using API.DTO.Links;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices
{
    public class ContractService : AbstractService, IContractService
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContractOperations _contractOperations;
        private readonly IHolidayOperations _holidayOperations;
        private readonly IUserOperations _userOperations;
        private readonly IWorkTimeOperations _workTimeOperations;
        private readonly IDepartmentOperations _departmentOperations;

        public ContractService(IContractOperations contractOperations, IHolidayOperations holidayOperations, IUserOperations userOperations, IWorkTimeOperations workTimeOperations, IDepartmentOperations departmentOperations, IUrlHelper urlHelper)
        {
            _contractOperations = contractOperations;
            _holidayOperations = holidayOperations;
            _userOperations = userOperations;
            _workTimeOperations = workTimeOperations;
            _departmentOperations = departmentOperations;
            _urlHelper = urlHelper;
        }

        public IEnumerable<ContractReturnDto> GetAllContracts()
        {
            return Mapper.Map<IEnumerable<ContractReturnDto>>(_contractOperations.GetAllContracts());
        }

        public ContractReturnDto GetContractByContractId(int contractId)
        {
            return !_contractOperations.ContractExists(contractId) ? null : Mapper.Map<ContractReturnDto>(_contractOperations.GetContractById(contractId));
        }

        public Tuple<ContractReturnDto, LinkDto> CreateNewContract(ContractDto contract)
        {
            if (contract.NumberOfHolidays < 0 || contract.GrossSalary < 0) return null;
            var createdContract = _contractOperations.CreateNewContract(Mapper.Map<Contract>(contract));
            return new Tuple<ContractReturnDto, LinkDto>(Mapper.Map<ContractReturnDto>(createdContract),CreateLink(createdContract.ContractID,"GetContractById", this._urlHelper));
        }

        public ContractReturnDto UpdateContract(int id, ContractDto updatedContract)
        {
            return !_contractOperations.ContractExists(id) ? null : Mapper.Map<ContractReturnDto>(_contractOperations.UpdateContract(_contractOperations.GetContractById(id), updatedContract.GrossSalary, updatedContract.NumberOfHolidays));
        }

        public void DeleteContract(int id)
        {
            if (!_contractOperations.ContractExists(id)) return;
            _contractOperations.DeleteContract(_contractOperations.GetContractById(id));
        }
    }
}
