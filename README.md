# EthClient.NET v0.1a
The super simple Ethereum Portable Class Library (PCL) API client. Use native .NET types with Ethereum.

_Currently a work in progress._

This library has _both_ an RPC and IPC client. The IPC client can be found in the EthClient.Windows class library.
The IPC client is not in a PCL since it requires classes referencing named Windows pipes.

## Examples
### List all your balances
    using (var client = new RpcClient())
    {
        IEnumerable<byte[]> accounts = await client.EthAccountsAsync();
        foreach (var account in accounts)
        {
            BigInteger balance = await client.EthGetBalanceAsync(account, DefaultBlock.Latest);
            Console.WriteLine("Account: {0} - Balance: {1}", EthHex.ToHexString(account), balance);
        }
    }

#### Outputs
    Account: 0x407d73d8a49eeb85d32cf465507dd71ddeadbeef - Balance: 6725234 wei
    Account: 0x545d73d8a49eebdeadbeef65507dd71d506533c1 - Balance: 234234 wei
    Account: 0xdeadbeefa49eeb85d54ad465507dd71d506286c1 - Balance: 56354345 wei

### Get RPC errors as native exceptions
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

#### Outputs
`ErrorCode: -32000, Message: mining work not ready`