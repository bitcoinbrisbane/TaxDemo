using System;
using System.IO;
using System.Text;

namespace ConsoleComplete
{
	class MainClass
	{
		///Users/lucascullen/GitHub/BitcoinBrisbane/TaxDemo/TaxDemo/Contracts
		private String CONTRACT_PATH = @"/Users/lucascullen/GitHub/BitcoinBrisbane/TaxDemo/TaxDemo/Bin/Contracts/";
		private String CONTRACT_FILE_NAME = "taxReturn";

		EnvironmentVariableTarget x;

		public static void Main(string[] args)
		{
			//Connect to Geth node
			Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3();
			var password = "Test";

			var accounts = web3.Personal.ListAccounts.SendRequestAsync().Result;

			for (Int32 i = 0; i < accounts.Length; i++)
			{
				var balance = web3.Eth.GetBalance.SendRequestAsync(accounts[i]).Result;
				Console.WriteLine(accounts[i] + " " + balance.Value);
			}

			//var newAccountResponse = web3.Personal.NewAccount.SendRequestAsync(password).Result;
			//accounts = web3.Personal.ListAccounts.SendRequestAsync().Result;

			Boolean unlockResponse = web3.Personal.UnlockAccount.SendRequestAsync(accounts[0], password, 120).Result;

			//

			//Contract
			var bytes = GetBytesFromFile(CONTRACT_PATH + CONTRACT_FILE_NAME + ".bin"); //"60a0604052601f60608190527f68747470733a2f2f7777772e61746f2e676f762e61752f636f6e747261637400608090815261003e9160019190610069565b50341561004757fe5b5b60008054600160a060020a03191633600160a060020a03161790555b610109565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106100aa57805160ff19168380011785556100d7565b828001600101855582156100d7579182015b828111156100d75782518255916020019190600101906100bc565b5b506100e49291506100e8565b5090565b61010691905b808211156100e457600081556001016100ee565b5090565b90565b610359806101186000396000f3006060604052361561005c5763ffffffff60e060020a60003504166306521586811461005e5780631c9d2c09146100ee57806350cd74a41461011a578063674edff8146101455780639f6d8ab414610196578063cdb12aa3146101b8575bfe5b341561006657fe5b61006e6101da565b6040805160208082528351818301528351919283929083019185019080838382156100b4575b8051825260208311156100b457601f199092019160209182019101610094565b505050905090810190601f1680156100e05780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156100f657fe5b6100fe610267565b60408051600160a060020a039092168252519081900360200190f35b341561012257fe5b610133600435602435604435610276565b60408051918252519081900360200190f35b341561014d57fe5b610161600160a060020a03600435166102e4565b60408051600160a060020a03909616865260208601949094528484019290925260608401526080830152519081900360a00190f35b341561019e57fe5b61013361031d565b60408051918252519081900360200190f35b34156101c057fe5b610133610325565b60408051918252519081900360200190f35b60018054604080516020600284861615610100026000190190941693909304601f8101849004840282018401909252818152929183018282801561025f5780601f106102345761010080835404028352916020019161025f565b820191906000526020600020905b81548152906001019060200180831161024257829003601f168201915b505050505081565b600054600160a060020a031681565b60006000426359f90e80118061028f5750635956e60042115b1561029957610000565b50600160a060020a0333166000908152600260208190526040909120600181018690559081018490556148438510156102d4578291506102da565b83830391505b5b5b509392505050565b600260208190526000918252604090912080546001820154928201546003830154600490930154600160a060020a039092169392909185565b635956e60081565b6359f90e80815600a165627a7a72305820a493a84a41269e8415f8e38315cee41887d177cb78d53069c4b9d1daea81df050029";

			//Deploy the contract

			String contractHash = web3.Eth.DeployContract.SendRequestAsync(bytes, accounts[0], new Nethereum.Hex.HexTypes.HexBigInteger(1000000)) .Result;
			//const String txHash = "0x2f432ba0d2b8047a552ea6d6907be67ffbfa679f8f52a04df0876032c4d77409";

			//Write out the response from the contract
			var isMining = web3.Eth.Mining.IsMining.SendRequestAsync().Result;

			if (isMining != true)
			{
				var mResult = web3.Miner.Start.SendRequestAsync().Result;
			}

			var receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash).Result;
			Console.Write("Processing");

			while (receipt == null)
			{
				System.Threading.Thread.Sleep(3000);
				receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash).Result;
				Console.Write(".");
			}

			if (receipt != null)
			{
				Console.WriteLine(receipt.BlockHash);
			}

			//@"[{""constant"":false,""inputs"":[{""name"":""x"",""type"":""uint8""},{""name"":""y"",""type"":""uint8""}],""name"":""multiply"",""outputs"":[{""name"":""product"",""type"":""uint8""}],""payable"":false,""type"":""function""}]"; //GetABIFromFile(CONTRACT_PATH + "math.abi");
			var abi = GetABIFromFile(CONTRACT_PATH + CONTRACT_FILE_NAME + ".abi");

			var contractAddress = receipt.ContractAddress;
			var contract = web3.Eth.GetContract(abi, contractAddress);

			//var multiplyFunction = contract.GetFunction("multiply");

			//object[] x = new object[2] { 2, 3 };
			//var resultant = multiplyFunction.CallAsync<Int64>(x).Result;

			var lodgeFunction = contract.GetFunction("lodge");
			object[] inputs = new object[3] { 100000, 11000, 30000 };

			var estimated = lodgeFunction.CallAsync<Int64>(inputs).Result;
			Console.WriteLine(estimated);

			unlockResponse = web3.Personal.UnlockAccount.SendRequestAsync(accounts[1], password, 120).Result;

			if (unlockResponse == true)
			{
				var estimatedTx = lodgeFunction.SendTransactionAsync(accounts[1], inputs).Result;
				Console.WriteLine(estimatedTx);
				receipt = null;

				while (receipt == null)
				{
					System.Threading.Thread.Sleep(3000);
					receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(estimatedTx).Result;
					Console.Write(".");
				}
			}

			var individualReturns = contract.GetFunction("individualReturns");
			var return0 = individualReturns.CallDeserializingToObjectAsync<DTOs.IndividualReturn>("0xb712a7797a7d52fe92d17a5e251aa19784cd18b0").Result;
		}

		private static string GetABIFromFile(String path)
		{
			string abi = File.ReadAllText(path, Encoding.UTF8);
			return abi;
		}

		private static string GetBytesFromFile(String path)
		{
			var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
			{
				String text = streamReader.ReadToEnd();
				return "0x" + text;
			}
		}
	}
}