using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    public class IPLogic
    {
        private string _Input = string.Empty;   //Felder
        private int _Suffix;
        private byte[] _IPAdresse;
        private byte[] _Subnetzmaske;
        private byte[] _NetzwerkAdresse;
        private byte[] _BroadcastAdresse;
        private int _AnzahlNachbarnetze;
        private double _AnzahlHosts;

        public string Input { get => _Input;private set  => _Input = value; }
        public int Suffix { get => _Suffix;private set=> _Suffix = value; }
        public byte[] IPAdresse { get => _IPAdresse; private set => _IPAdresse = value; }
        public byte[] Subnetzmaske { get => _Subnetzmaske; private set => _Subnetzmaske = value; }
        public byte[] NetzwerkAdresse { get => _NetzwerkAdresse ;private set => _NetzwerkAdresse = value; }
        public byte[] BroadcastAdresse { get => _BroadcastAdresse ;private set => _BroadcastAdresse = value; }
        public int AnzahlNachbarnetze { get => _AnzahlNachbarnetze;private set => _AnzahlNachbarnetze = value; }
        public double AnzahlHosts { get => _AnzahlHosts; private set => _AnzahlHosts = value; }
        
        public IPLogic() //Konstruktor
        {
            //TODO: Dynamische Eingabe, hab mich hier in der Projektplanung verzettelt. Wollte WinForms machen, die sind aber noch sehr verwirrend für mich.
            Input = "192.168.0.4/25";
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
        public static int GetSuffix(string Input)
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
        public static byte[] GetSubnetzmaske(int Suffix) 
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
        public static byte[] GetIPAdresse(string Input)
        {
            byte[] Adresse = new byte[4];
            string[] sAdresse = Input[..Input.IndexOf('/')].Split('.');


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
        public static byte[] GetNetzwerkAdresse(byte[] IPAdresse, byte[] Subnetzmaske)
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
        public static byte[] GetBroadcastAdresse(byte[] NetzwerkAdresse, byte[] Subnetzmaske)
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
        public static double GetAnzahlHosts(int Suffix)
        {
            double Exp = (32 - Suffix);
            double Anzahl = Math.Pow(2,Exp) - 2;
            return Anzahl;
        }
        /// <summary>
        /// calculates the number of subnetworks possible from suffix
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns>Count of subnetworks</returns>
        public static int GetAnzahlSubnetze(int Suffix)
        {
            int Exp = Suffix % 8;
            int Anzahl = 1;
            return Anzahl <<= Exp;
        }
    }
}

