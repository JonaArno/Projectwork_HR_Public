using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainServices.Interfaces;
using API.DTO.Contract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/contracts")]

    public class ContractController : AbstractController
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        /// <summary>
        /// Gets all contracts in the database
        /// </summary>
        /// <returns>A list of all contracts</returns>
        [HttpGet(Name = "GetAllContracts")]
        [ProducesResponseType(typeof(IEnumerable<ContractReturnDto>),200)]
        public IActionResult GetAllContracts()
        {
            var allContracts = _contractService.GetAllContracts();
            return Ok(allContracts);
        }

        /// <summary>
        /// Get a contract by its id.
        /// </summary>
        /// <param name="id">Unique identifier of a contract.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetContractById")]
        [ProducesResponseType(typeof(ContractReturnDto),200)]
        [ProducesResponseType(404)]
        public IActionResult GetContractById(int id)
        {
            return OkOrNotFound(_contractService.GetContractByContractId(id));
        }

        /// <summary>
        /// Create a new contract.
        /// </summary>
        /// <param name="newContract">Information related to gross salary and number of holidays of contract.</param>
        /// <returns></returns>
        [HttpPost(Name = "CreateContract")]
        [ProducesResponseType(typeof(ContractReturnDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateContract(ContractDto newContract)
        {
            var createContract = _contractService.CreateNewContract(newContract);
            if (createContract == null) return BadRequest();
            return CreatedAtRoute("GetContractById", new {id = createContract.Item1.ContractID}, createContract.Item1);
        }

        /// <summary>
        /// Update the details of an existing contract.
        /// </summary>
        /// <param name="id">Unique identifier of the contract to update.</param>
        /// <param name="updatedContract">Updated values for gross salary and number of holidays.</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateContract")]
        [ProducesResponseType(typeof(ContractReturnDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateContract(int id, [FromBody] ContractDto updatedContract)
        {
            return OkOrBadRequest(_contractService.UpdateContract(id, updatedContract));
        }

        /// <summary>
        /// Delete a contract using its identifier.
        /// </summary>
        /// <param name="id">Unique identifier of a contract.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteContract(int id)
        {
            _contractService.DeleteContract(id);
            return Ok();
        }


    }
}
