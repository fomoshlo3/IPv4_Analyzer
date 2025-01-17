using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    public sealed class IPv4AddressAnalyzer
    {
        public string Input { get; set; } = string.Empty;

        private int _CIDR;

        public int CIDR
        {
            get { return _CIDR; }
            set { _CIDR = value; }
        }


        public IPv4AddressAnalyzer(string input) //Konstruktor
        {
            Input = input;
        }


        /// <summary>
        /// Parses the suffix out of the input string an converts it to integer
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Int Suffix</returns>
        public int GetCIDR(string input)
        {
            int substringStart = input.IndexOf('/') +1;
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
                if (j <= CIDR)
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

        /// <summary>
        /// Parses the Input by '.' and '/'and converts the resulting string array to a byte array explanation of an IP adress.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] GetIPAdresse(string input)
        {
            byte[] address = new byte[4];
            string[] sAddress = input[..input.IndexOf('/')].Split('.');


            for (int i = 0; i < sAddress.Length; i++)
            {
                address[i] = byte.Parse(sAddress[i]);
            }
            return address;
        }

        /// <summary>
        /// ANDs the IP adress with the subnetmask to calculate the network id.
        /// </summary>
        /// <param name="IPAdresse"></param>
        /// <param name="Subnetzmaske"></param>
        /// <returns>Byte Array with 4 fields which are explanatory for the 4 fields of an IP</returns>
        public byte[] GetNetworkAddress(byte[] IPAdresse, byte[] Subnetzmaske)
        {
            byte[] Adresse = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                Adresse[i] = (byte)(Subnetzmaske[i] & IPAdresse[i]);
            }
            return Adresse;
        }
        /// <summary>
        /// Inverts the Host Part of the NetworkAdress
        /// </summary>
        /// <param name="Subnetzmaske"></param>
        /// <param name="NetzwerkAdresse"></param>
        /// <returns>Byte Array with 4 fields which are explanatory for the 4 fields of an IP</returns>
        public byte[] GetBroadcastAdress(byte[] NetzwerkAdresse, byte[] Subnetzmaske)
        {
            byte[] Adresse = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                Adresse[i] = (byte)(NetzwerkAdresse[i] | (~Subnetzmaske[i]));
            }
            return Adresse;
        }
        /// <summary>
        /// calculates the number of hosts possible from suffix
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns>Number of possible hosts</returns>
        public double GetAnzahlHosts()
        {
            double Exp = (32 - CIDR);
            double Anzahl = Math.Pow(2,Exp) - 2;
            return Anzahl;
        }
        /// <summary>
        /// calculates the number of subnetworks possible from suffix
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns>Count of subnetworks</returns>
        public int GetAnzahlSubnetze()
        {
            int Exp = CIDR % 8;
            int Anzahl = 1;
            return Anzahl <<= Exp;
        }
    }
}

