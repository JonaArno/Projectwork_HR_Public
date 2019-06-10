using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Contract
{
    public class ContractReturnDto
    {
        /// <summary>
        /// The unique identifier of the contract.
        /// </summary>
        public int ContractID { get; set; }
        /// <summary>
        /// The gross salary included in the contract.
        /// </summary>
        public decimal GrossSalary { get; set; }
        /// <summary>
        /// The number of holidays included in the contract.
        /// </summary>
        public int NumberOfHolidays { get; set; }
    }
}
