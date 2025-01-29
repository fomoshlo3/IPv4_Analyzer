using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    //Todo: Refactor all GetMethods to void functions for my own amusement and curiosity instead of progressing
    //Todo: update all comments


    //public sealed class NetworkCalculator
    //{
    //    public delegate byte[] Calculated();

    //    public IPv4Address IPAddress { get; private set; }

    //    public IPv4Address SubnetMask { get; private set; }

    //    public IPv4Address NetworkAddress { get; private set; }

    //    public IPv4Address BroadcastAddress { get; private set; }

    //    public IPv4Prefix Prefix { get; private set; }

    //    public int NeighboringNetworks { get; private set; }

    //    public int MaxHosts { get; private set; }

    //    public NetworkCalculator(IPv4Address ipAddress, IPv4Prefix prefix)
    //    {
    //        IPAddress = ipAddress;
    //        Prefix = prefix;

    //        SetSubnetMask();
    //        SetNetworkAddress();
    //        SetBroadcastAddress();
    //        SetMaxHosts();
    //        SetNeighboringNetworks();
    //    }



    //    /// <summary>
    //    /// Parses the suffix out of the input string an converts it to integer
    //    /// </summary>
    //    /// <param name="input"></param>
    //    /// <returns>Int Suffix</returns>
    //    [Obsolete("CIDR Prefix Number will be resolved in IPv4Prefix Class")]
    //    public int GetCIDR(string input)
    //    {
    //        int substringStart = input.LastIndexOf('/') + 1;
    //        int substringEnd = input.Length - substringStart;

    //        int CIDR = Int32.Parse(input.Substring(substringStart, substringEnd));
    //        return CIDR;
    //    }

    //    /// <summary>
    //    /// builds a byte representation of a subnetmask from suffix
    //    /// </summary>
    //    private void SetSubnetMask()
    //    {
    //        string subnetMaskAsBinary = string.Empty;

    //        for (int j = 1; j <= 32; j++)
    //        {
    //            if (j <= Prefix.Length)
    //            {
    //                subnetMaskAsBinary += '1';
    //            }
    //            else
    //            {
    //                subnetMaskAsBinary += '0';
    //            }
    //            if (j % 8 == 0 && j != 32) subnetMaskAsBinary += '.';
    //        }
    //        byte[] address = new byte[4];
    //        string[] bitArray = subnetMaskAsBinary.Split('.');
    //        for (int i = 0; i < address.Length; i++)
    //        {
    //            address[i] = Convert.ToByte(bitArray[i], 2);
    //        }

    //        SubnetMask = new IPv4Address(address);
    //    }

    //    private void SetNetworkAddress()
    //    {
    //        byte[] result = new byte[4];
    //        for (int i = 0; i < 4; i++)
    //        {
    //            result[i] = (byte)(SubnetMask.Address[i] & IPAddress.Address[i]);
    //        }

    //        NetworkAddress = new IPv4Address(result);
    //    }

    //    private void SetBroadcastAddress()
    //    {
    //        byte[] result = new byte[4];
    //        for (int i = 0; i < 4; i++)
    //        {
    //            BroadcastAddress.Address[i] = (byte)(NetworkAddress.Address[i] | (~SubnetMask.Address[i]));
    //        }
    //    }

    //    private void SetMaxHosts()
    //    {
    //        double exp = (32 - Prefix.Length);
    //        double anzahl = Math.Pow(2,exp) - 2;

    //        MaxHosts = (int)anzahl;
    //    }

    //    private void SetNeighboringNetworks()
    //    {
    //        int anzahl = 1;

    //        int exp = Prefix.Length % 8;
    //        NeighboringNetworks = anzahl <<= exp;
    //    }
    //}

    public sealed class NetworkCalculator
    {
        public IPv4Address IPAddress { get; private set; }
        public IPv4Address SubnetMask { get; private set; }
        public IPv4Address NetworkAddress { get; private set; }
        public IPv4Address BroadcastAddress { get; private set; }
        public IPv4Prefix Prefix { get; private set; }
        public int NeighboringNetworks { get; private set; }
        public int MaxHosts { get; private set; }

        public NetworkCalculator(IPv4Address ipAddress, IPv4Prefix prefix)
        {
            IPAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));

            SubnetMask = CalculateSubnetMask();
            NetworkAddress = CalculateNetworkAddress();
            BroadcastAddress = CalculateBroadcastAddress();
            MaxHosts = CalculateMaxHosts();
            NeighboringNetworks = CalculateNeighboringNetworks();
        }

        private IPv4Address CalculateSubnetMask()
        {
            uint mask = 0xFFFFFFFF << (32 - Prefix.Length);
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(mask >> (24 - i * 8) & 0xFF);
            }
            return new IPv4Address(bytes);
        }

        private IPv4Address CalculateNetworkAddress()
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(IPAddress.Address[i] & SubnetMask.Address[i]);
            }
            return new IPv4Address(bytes);
        }

        private IPv4Address CalculateBroadcastAddress()
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(NetworkAddress.Address[i] | ~SubnetMask.Address[i]);
            }
            return new IPv4Address(bytes);
        }

        private int CalculateMaxHosts()
        {
            return (int)Math.Pow(2, 32 - Prefix.Length) - 2;
        }

        private int CalculateNeighboringNetworks()
        {
            return 1 << (32 - Prefix.Length);
        }
    }
}

