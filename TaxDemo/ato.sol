pragma solidity ^0.4.9;

contract taxReturn {

    struct individualReturn {
        address client;
        uint income;
        uint expenses;
        uint taxPaid;
        uint lodgedDate;
        string[] documents; //SHA3
    }

    //Unix time
    uint public constant taxReturnStartTime = 1498867200; //July 1, 2017
    uint public constant taxReturnEndTime = 1509494400; //Ocotober 31, 2017

    address public ato;
    string public contractUrl = "https://www.ato.gov.au/contract";

    mapping(address => individualReturn) public individualReturns;

    function taxReturn()
    {
        //assign ato address
        ato = msg.sender;
    }

    function lodge(uint income, uint expenses, uint taxPaid)
    {
        if (taxReturnEndTime > now || now > taxReturnStartTime)
        {
            throw;
        }
        else
        {
            //var taxReturn = new individualReturn ();

            var taxReturn = individualReturns[msg.sender];
            taxReturn.income = income;
            taxReturn.expenses = expenses;
            taxPaid = taxPaid;

            //mapping(msg.sender => client) public Clients;
        }
    }

    event Lodged();
}