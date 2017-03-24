# TaxDemo
Demo code for the Ethereum Brisbane meetup

## Goals for the night
1. Setup a private geth chain
2. Create a visual stuido project
3. Use visual studio code to write a solidty smart contract
4. Complie the smart contract
5. Use c# NuGet package to deploy the contract
6. Write a UI

### Notes
```
geth --identity "ato" --nodiscover --datadir "/Users/lucascullen/Chains/ato" --genisis "/Users/lucascullen/GitHub/bitcoinbrisbane/TaxDemo/TaxDemo/genisis.json"
geth account new
curl 60.226.74.183 -X POST --data '{"jsonrpc":"2.0","method":"eth_blockNumber","params":[],"id":83}'
```
