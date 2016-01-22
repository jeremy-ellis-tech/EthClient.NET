# EthClient.NET v0.1a
The super simple Ethereum JSON RPC client. Use native .NET types with Ethereum.

_Currently a work in progress._ Not all of the RPC methods have been implemented yet.

## Examples
### List all your balances
    using (var client = new RpcClient())
    {
        ICollection<byte[]> accounts = await client.EthAccountsAsync();

        var defaultBlock = new DefaultBlock(DefaultBlockParameterOption.Latest);

        foreach (var account in accounts)
        {
            var balance = await client.EthGetBalanceAsync(account, defaultBlock);
            Console.WriteLine("Account: {0} - Balance: {1} wei", EthHex.ByteArrayToHexString(account), balance);
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