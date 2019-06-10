using System;
using System.Collections.Generic;
using System.Text;
using API.DTO;
using API.DTO.Contract;
using API.DTO.Links;
using API.Model;

namespace API.DomainServices.Interfaces
{
    public interface IContractService
    {
        IEnumerable<ContractReturnDto> GetAllContracts();
        ContractReturnDto GetContractByContractId(int contractId);
        Tuple<ContractReturnDto, LinkDto> CreateNewContract(ContractDto contract);
        ContractReturnDto UpdateContract(int id, ContractDto updatedContract);
        void DeleteContract(int id);
    }
}
