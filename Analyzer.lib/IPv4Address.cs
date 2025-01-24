using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.lib
{
    public class IPv4Address
    {
        public byte[] Address { get; private set; }

        public IPv4Address(string ipString)
        {
            Address = this.Parse(ipString);
        }
        public IPv4Address(byte[] ipBytemask)
        {
            Address = ipBytemask;
        }

        //public byte[] TryParse(string ipString)
        //{
        //    try
        //    {
        //        return this.Parse(ipString);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}


        private byte[] Parse(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                throw new ArgumentNullException("IPv4 Address cannot be null or empty.");
            }
            
            if (ipString.Count(c => c == '.') != 3)
            {
                throw new ArgumentException("IPv4 Address should consist of 4 octets intersected by a period.");
            }

            string[] octets = ipString.Split('.');
            byte[] result = new byte[octets.Length];

            for(int i = 0; i < octets.Length; i++)
            {
                try
                {
                    result[i] = byte.Parse(octets[i]);
                }
                catch(OverflowException)
                {
                    throw new ArgumentException("IPv4 Address segments can only consist of numbers ranging from 0 to 255.");
                }
                catch(FormatException)
                {
                    throw new ArgumentException("IPv4 Address segments can only take numbers.");
                }
            }

            return result;
        }

        public override string ToString()
        {
            return $"{Address[0]}.{Address[1]}.{Address[2]}.{Address[3]}";
        }
    }
}
