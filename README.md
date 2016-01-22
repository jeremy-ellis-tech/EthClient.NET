# EthClient.NET v0.1a
The super simple Ethereum JSON RPC client. Use native .NET types with Ethereum.

_Currently a work in progress._ Not all of the RPC methods have been implemented yet.

## Examples
### Getting the client version
    using(var client = new RpcClient("node.address.here.with:port"))
    {
		string version = await client.Web3ClientVersionAsync();
        Console.WriteLine(version);
    }

#### Outputs
`Mist/v0.9.3/darwin/go1.4.1`

### Get RPC errors as native exceptions
Calling EthGetWork() before starting the miner:

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