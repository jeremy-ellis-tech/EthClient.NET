# EthClient.NET v0.9 beta
The super simple Ethereum Portable Class Library (PCL) API client. Use native .NET types with Ethereum! Featuring a super simple interface and Abi encoding for function calls.

This library has _both_ an RPC and IPC client. The IPC client can be found in the EthClient.Windows class library.
The IPC client is not in a PCL since it requires classes referencing named Windows pipes.

Keep in mind this is in beta; I don't recommend using this for potentially expensive transactions.

## Examples
### List all your balances:
    using (var client = new RpcClient())
    {
        IEnumerable<byte[]> accounts = await client.EthAccountsAsync();
        foreach (var account in accounts)
        {
            BigInteger balance = await client.EthGetBalanceAsync(account, DefaultBlock.Latest);
            Console.WriteLine("Account: {0} - Balance: {1}", EthHex.ToHexString(account), balance);
        }
    }

#### Outputs:
    Account: 0x407d73d8a49eeb85d32cf465507dd71ddeadbeef - Balance: 6725234 wei
    Account: 0x545d73d8a49eebdeadbeef65507dd71d506533c1 - Balance: 234234 wei
    Account: 0xdeadbeefa49eeb85d54ad465507dd71d506286c1 - Balance: 56354345 wei

### Calling contracts:
Given an example contract, which is already deployed on the block-chain:

    contract ExampleContract
    {
      function Multiply(uint a, uint b) constant returns(uint)
      {
        return a * b;
      }
    }

We can easily make use of EthClient's built-in Abi encoding/decoding!

    using (var client = new RpcClient())
    {
        // byte[] contractAddress = ...;
        EthContract exampleContract = client.GetContractAt(contractAddress);
        var answer = await exampleContract.CallAsync<UInt256AbiValue>(new UInt256AbiValue(42), new UInt256AbiValue(24));
        Console.WriteLine("The answer is {0}!", (BigInteger)answer);
    }

### Outputs:

     The answer is 1008!

### Get RPC errors as native exceptions:
Calling `EthGetWorkAsync()` before starting the miner

    try
    {
        using (var rpcClient = new RpcClient("node.address.here.with:port"))
        {
            EthWork ret = await rpcClient.EthGetWorkAsync();
        }
    }
    catch (EthException ex)
    {
        Console.WriteLine("ErrorCode: {0}, Message: {1}", ex.ErrorCode, ex.Message);
    }

#### Outputs:
`ErrorCode: -32000, Message: mining work not ready`

## License
MIT license, see LICENSE for details

## Contributions
Contributions are very welcome. Create a feature branch off `develop`, make your changes, and raise a pull request. Unit tests encouraged.
