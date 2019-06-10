using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Contract;

namespace API.DTO.User
{
    public class UserContractUpdatedReturnDto
    {
        /// <summary>
        /// User associated with the contract.
        /// </summary>
        public UserReturnMinifiedDto User { get; set; }
        /// <summary>
        /// New contract associated with the user.
        /// </summary>
        public ContractReturnDto Contract { get; set; }
    }
}
