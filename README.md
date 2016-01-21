# EthClient.NET v0.1a
The super simple Ethereum Json RPC client.

Use native .NET types with Ethereum. No nasty byte hacking needed.

_Currently a work in progress._

##Examples
These examples assume the node is on the same PC as the one running the program. If not, replace `var client = new RpcClient()` with `var client = new RpcClient(new Uri("http://ip.address.here:port"))`
###Getting the client version
    using(var client = new RpcClient())
    {
		string version = await client.Web3ClientVersion();
        Console.WriteLine(version);
    }

#### Outputs
`Mist/v0.9.3/darwin/go1.4.1`

###Getting the peer count
    using(var client = new RpcClient())
    {
		var peerCount = await client.NetPeerCount();
        Console.WriteLine(peerCount);
    }

#### Outputs
`3`