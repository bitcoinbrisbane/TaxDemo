using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ConsoleComplete.DTOs
{
	[FunctionOutput]
	public class IndividualReturn
	{
		[Parameter("string", "client", 1)]
		public String Client { get; set; }

		[Parameter("uint256", "income", 2)]
		public Int64 Income { get; set; }

		[Parameter("uint256", "expenses", 3)]
		public Int64 Expenses { get; set; }

		[Parameter("uint256", "lodgedDate", 4)]
		public Int64 LodgedDate { get; set; }

		public IndividualReturn()
		{
		}
	}
}
