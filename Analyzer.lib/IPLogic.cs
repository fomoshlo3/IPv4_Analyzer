using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    public class IPLogic
    {
        private string _Input;   //Felder
        private int _Suffix;
        private byte[] _IPAdresse;
        private byte[] _Subnetzmaske;
        private byte[] _NetzwerkAdresse;
        private byte[] _BroadcastAdresse;
        private int _AnzahlNachbarnetze;
        private double _AnzahlHosts;
      
        private string _Output;

        public IPLogic() //Konstruktor
        {
            _Input = "192.168.1.30/10";
            _Suffix = GetSuffix(_Input);
            _IPAdresse = GetIPAdresse(_Input);
            _Subnetzmaske = GetSubnetzmaske(_Suffix);
            _NetzwerkAdresse = GetNetzwerkAdresse(_IPAdresse, _Subnetzmaske);
            _BroadcastAdresse = GetBroadcastAdresse(_NetzwerkAdresse, _Subnetzmaske);
            _AnzahlHosts = GetAnzahlHosts(_Suffix);
            _AnzahlNachbarnetze = GetAnzahlSubnetze(_Suffix);
            

            //Console Output
            Console.WriteLine($"IP Adresse:\t\t{_IPAdresse[0]}.{_IPAdresse[1]}.{_IPAdresse[2]}.{_IPAdresse[3]}");
            Console.WriteLine($"Subnetzmaske:\t\t{_Subnetzmaske[0]}.{_Subnetzmaske[1]}.{_Subnetzmaske[2]}.{_Subnetzmaske[3]}");
            Console.WriteLine($"Netzwerkadresse:\t{_NetzwerkAdresse[0]}.{_NetzwerkAdresse[1]}.{_NetzwerkAdresse[2]}.{_NetzwerkAdresse[3]}");
            Console.WriteLine($"Broadcastadresse:\t{_BroadcastAdresse[0]}.{_BroadcastAdresse[1]}.{_BroadcastAdresse[2]}.{_BroadcastAdresse[3]}");
            Console.WriteLine($"Anzahl Hosts:\t\t{_AnzahlHosts}");
            Console.WriteLine($"Anzahl Subnetze:\t{_AnzahlNachbarnetze}");
            Console.ReadKey();
        }


        /// <summary>
        /// Parses the suffix out of the input string an converts it to integer
        /// </summary>
        /// <param name="_Input"></param>
        /// <returns>Int Suffix</returns>
        public int GetSuffix(string _Input)
        {
            int substringStart = _Input.IndexOf('/') +1;
            int substringEnd = _Input.Length - substringStart;
            int Suffix = Int32.Parse(_Input.Substring(substringStart, substringEnd));
            return Suffix;
        }

        /// <summary>
        ///builds a byte representation of a subnetmask from suffix
        /// </summary>
        /// <param name="_Suffix"></param>
        /// <returns>byte[] Adresse</returns>
        public byte[] GetSubnetzmaske(int _Suffix) 
        {
            
            string SubnetmaskDotBinary = string.Empty;

            for (int j = 1; j <= 32; j++)
            {
                if (j <= _Suffix)
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
        /// <param name="_Input"></param>
        /// <returns></returns>
        public byte[] GetIPAdresse(string _Input)
        {
            byte[] Adresse = new byte[4];
            string[] sAdresse = _Input.Substring(0, _Input.IndexOf('/')).Split('.');


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
        /// <param name="_Suffix"></param>
        /// <returns>Number of possible hosts</returns>
        public double GetAnzahlHosts(int _Suffix)
        {
            double Exp = (32 - _Suffix);
            double Anzahl = Math.Pow(2,Exp) - 2;
            return Anzahl;
        }
        /// <summary>
        /// calculates the number of subnetworks possible from suffix
        /// </summary>
        /// <param name="_Suffix"></param>
        /// <returns></returns>
        public int GetAnzahlSubnetze(int _Suffix)
        {
            int Exp = _Suffix % 8;
            int Anzahl = 1;
            return Anzahl = (Anzahl << Exp);
        }
    }
}

