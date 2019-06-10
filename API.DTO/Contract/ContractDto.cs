using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Contract
{
    public class ContractDto
    {
        /// <summary>
        /// The gross salary with which the contract should be created
        /// </summary>
        public decimal GrossSalary { get; set; }
        /// <summary>
        /// The number of holidays included in the contract.
        /// </summary>
        public int NumberOfHolidays { get; set; }
    }
}