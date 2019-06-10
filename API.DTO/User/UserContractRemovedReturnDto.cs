using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Contract;

namespace API.DTO.User
{
    public class UserContractRemovedReturnDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Full name of the user whose contract was removed.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Current contract associated with the user.
        /// </summary>
        public ContractReturnDto Contract { get; set; }
    }
}
