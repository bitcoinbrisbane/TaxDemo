pragma solidity ^0.4.9;

contract taxReturn {

    struct individualReturn {
        address client;
        uint256 income;
        uint256 expenses;
        uint256 taxPaid;
        uint256 lodgedDate;
        //string[] documents; //SHA3
    }

    //Unix time
    uint public constant taxReturnStartTime = 1498867200; //July 1, 2017
    uint public constant taxReturnEndTime = 1509494400; //Ocotober 31, 2017

    //address public ato;
    //string public contractUrl = "https://www.ato.gov.au/contract";

    mapping(address => individualReturn) public individualReturns;

    function taxReturn()
    {
        //assign ato address
        ato = msg.sender;
    }

    function lodge(uint256 income, uint256 expenses, uint256 taxPaid) returns (uint256 estimatedResult)
    {
        // if (taxReturnEndTime > now || now > taxReturnStartTime)
        // {
        //     throw;
        // }
        // else
        // {
            //var taxReturn = new individualReturn(msg.sender, income, 

            //)
            // taxReturn.income = income;
            // taxReturn.expenses = expenses;
            // taxPaid = taxPaid;

            if (income < 18499)
            {
                return taxPaid;
            }
            else
            {
                return taxPaid - expenses;
            }
        //}
    }

    event Lodge(address indexed from, uint income, uint expenses, uint taxPaid);
}

//Notes https://souptacular.gitbooks.io/ethereum-tutorials-and-tips-by-hudson/content/private-chain.html