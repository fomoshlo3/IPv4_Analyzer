using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    public class IPLogic
    {
        public string Input;   //Felder
        public int Suffix;
        public byte[] IPAdresse;
        public byte[] Subnetzmaske;
        public byte[] NetzwerkAdresse;
        public byte[] BroadcastAdresse;
        public int AnzahlNachbarnetze;
        public double AnzahlHosts;

        public IPLogic() //Konstruktor
        {
            Input = "192.168.1.30/10";
            Suffix = GetSuffix(Input);
            IPAdresse = GetIPAdresse(Input);
            Subnetzmaske = GetSubnetzmaske(Suffix);
            NetzwerkAdresse = GetNetzwerkAdresse(IPAdresse, Subnetzmaske);
            BroadcastAdresse = GetBroadcastAdresse(NetzwerkAdresse, Subnetzmaske);
            AnzahlHosts = GetAnzahlHosts(Suffix);
            AnzahlNachbarnetze = GetAnzahlSubnetze(Suffix);
        }


        /// <summary>
        /// Parses the suffix out of the input string an converts it to integer
        /// </summary>
        /// <param name="Input"></param>
        /// <returns>Int Suffix</returns>
        public int GetSuffix(string Input)
        {
            int substringStart = Input.IndexOf('/') +1;
            int substringEnd = Input.Length - substringStart;
            int Suffix = Int32.Parse(Input.Substring(substringStart, substringEnd));
            return Suffix;
        }

        /// <summary>
        ///builds a byte representation of a subnetmask from suffix
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns>byte[] Adresse</returns>
        public byte[] GetSubnetzmaske(int Suffix) 
        {
            
            string SubnetmaskDotBinary = string.Empty;

            for (int j = 1; j <= 32; j++)
            {
                if (j <= Suffix)
                {
                    SubnetmaskDotBinary += '1';
                }
                else
                {
                    SubnetmaskDotBinary += '0';
                }
                if (j % 8 == 0 && j != 32) SubnetmaskDotBinary += '.';
            }
            byte[] Adresse = new byte[4];
            string[] bit_array = SubnetmaskDotBinary.Split('.');
            for (int i = 0; i < Adresse.Length; i++)
            {
                Adresse[i] = Convert.ToByte(bit_array[i], 2);
            }
            return Adresse;
        }

        /// <summary>
        /// Parses the Input by '.' and '/'and converts the resulting string array to a byte array explanation of an IP adress.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public byte[] GetIPAdresse(string Input)
        {
            byte[] Adresse = new byte[4];
            string[] sAdresse = Input.Substring(0, Input.IndexOf('/')).Split('.');


            for (int i = 0; i < sAdresse.Length; i++)
            {
                Adresse[i] = byte.Parse(sAdresse[i]);
            }
            return Adresse;
        }

        /// <summary>
        /// ANDs the IP adress with the subnetmask to calculate the network id.
        /// </summary>
        /// <param name="IPAdresse"></param>
        /// <param name="Subnetzmaske"></param>
        /// <returns>Byte Array with 4 fields which are explanatory for the 4 fields of an IP</returns>
        public byte[] GetNetzwerkAdresse(byte[] IPAdresse, byte[] Subnetzmaske)
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
        public byte[] GetBroadcastAdresse(byte[] NetzwerkAdresse, byte[] Subnetzmaske)
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
        public double GetAnzahlHosts(int Suffix)
        {
            double Exp = (32 - Suffix);
            double Anzahl = Math.Pow(2,Exp) - 2;
            return Anzahl;
        }
        /// <summary>
        /// calculates the number of subnetworks possible from suffix
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns></returns>
        public int GetAnzahlSubnetze(int Suffix)
        {
            int Exp = Suffix % 8;
            int Anzahl = 1;
            return Anzahl <<= Exp;
        }
    }
}

