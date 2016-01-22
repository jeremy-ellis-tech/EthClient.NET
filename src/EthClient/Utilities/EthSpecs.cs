namespace Eth.Utilities
{
    /// <summary>
    /// Specifications of the Ethereum protocol
    /// </summary>
    public static class EthSpecs
    {
        /// <summary>
        /// The address length in bytes
        /// </summary>
        public static int AddressLength { get { return 20; } }

        /// <summary>
        /// The block hash length in bytes
        /// </summary>
        public static int BlockHashLength { get { return 32; } }

        /// <summary>
        /// Length of Whipser identity in bytes
        /// </summary>
        public static int WhisperIdentityLength { get { return 60; } }

        /// <summary>
        /// Length of ClientID in bytes
        /// </summary>
        public static int ClientIDLength { get { return 32; } }

        /// <summary>
        /// Maximum number of bytes the hash rate can be set to
        /// </summary>
        public static int HashRateMaxLength { get { return 32; } }

        /// <summary>
        /// Length of proof of work found nonce, in bytes
        /// </summary>
        public static int NonceLength { get { return 8; } }

        /// <summary>
        /// Length of header pow hash in bytes
        /// </summary>
        public static int HeaderPowHashLength { get { return 32; } }

        /// <summary>
        /// Length of mix digest length in bytes
        /// </summary>
        public static int MixDigestLength { get { return 32; } }
    }
}
