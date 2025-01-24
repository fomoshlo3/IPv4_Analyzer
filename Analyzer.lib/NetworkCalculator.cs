using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    //Todo: refactor to only taking a byte array of 4 fields length for the ip address and a int less than 32 representing a CIDR
    //      or an second byte array of 4 fields representing a subnetmask
    //Todo: update all comments
    public sealed class NetworkCalculator
    {
        public IPv4Address IPAddress { get; private set; }

        public IPv4Address? SubnetMask { get; private set; }

        public IPv4Address? NetworkAddress { get; private set; }

        public IPv4Address? BroadcastAddress { get; private set; }

        public IPv4Prefix Prefix { get; private set; }

        public int? NeighboringNetworks { get; private set; }

        public int? MaxHosts { get; private set; }

        public NetworkCalculator(IPv4Address ipAddress, IPv4Prefix prefix)
        {
            IPAddress = ipAddress;
            Prefix = prefix;
        }



        /// <summary>
        /// Parses the suffix out of the input string an converts it to integer
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Int Suffix</returns>
        [Obsolete("CIDR Prefix Number will be resolved in IPv4Prefix Class")]
        public int GetCIDR(string input)
        {
            int substringStart = input.LastIndexOf('/') + 1;
            int substringEnd = input.Length - substringStart;

            int CIDR = Int32.Parse(input.Substring(substringStart, substringEnd));
            return CIDR;
        }

        /// <summary>
        ///builds a byte representation of a subnetmask from suffix
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns>byte[] Adresse</returns>
        public byte[] GetSubnetMask()
        {

            string subnetMaskAsBinary = string.Empty;

            for (int j = 1; j <= 32; j++)
            {
                if (j <= Prefix.Length)
                {
                    subnetMaskAsBinary += '1';
                }
                else
                {
                    subnetMaskAsBinary += '0';
                }
                if (j % 8 == 0 && j != 32) subnetMaskAsBinary += '.';
            }
            byte[] address = new byte[4];
            string[] bitArray = subnetMaskAsBinary.Split('.');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = Convert.ToByte(bitArray[i], 2);
            }
            return address;
        }

        public byte[] GetNetworkAddress(byte[] iPAddress, byte[] subnetMask)
        {
            byte[] result = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = (byte)(subnetMask[i] & iPAddress[i]);
            }
            return result;
        }

        public byte[] GetBroadcastAddress(byte[] networkAddress, byte[] subnetMask)
        {
            byte[] result = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = (byte)(networkAddress[i] | (~subnetMask[i]));
            }
            return result;
        }

        public double GetMaxHosts(IPv4Prefix Prefix)
        {
            double Exp = (32 - Prefix.Length);
            double Anzahl = Math.Pow(2,Exp) - 2;

            return Anzahl;
        }

        public int GetMaxSubnets(int prefix)
        {
            int Anzahl = 1;

            int Exp = prefix % 8;
            return Anzahl <<= Exp;
        }
    }
}

