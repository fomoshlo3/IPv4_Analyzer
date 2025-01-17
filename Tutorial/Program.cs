﻿using Analyzer.lib;

namespace Analyzer.app
{

    class Program
    {
        static void Main()
        {
            IPv4AddressAnalyzer ip = new IPv4AddressAnalyzer("192.168.0.1/24");

            ////Console Output
            //Console.WriteLine($"IP Adresse:\t\t{ip.IPAdresse[0]}.{ip.IPAdresse[1]}.{ip.IPAdresse[2]}.{ip.IPAdresse[3]}");
            //Console.WriteLine($"Subnetzmaske:\t\t{ip.Subnetzmaske[0]}.{ip.Subnetzmaske[1]}.{ip.Subnetzmaske[2]}.{ip.Subnetzmaske[3]}");
            //Console.WriteLine($"Netzwerkadresse:\t{ip.NetzwerkAdresse[0]}.{ip.NetzwerkAdresse[1]}.{ip.NetzwerkAdresse[2]}.{ip.NetzwerkAdresse[3]}");
            //Console.WriteLine($"Broadcastadresse:\t{ip.BroadcastAdresse[0]}.{ip.BroadcastAdresse[1]}.{ip.BroadcastAdresse[2]}.{ip.BroadcastAdresse[3]}");
            //Console.WriteLine($"Anzahl Hosts:\t\t{ip.AnzahlHosts}");
            //Console.WriteLine($"Anzahl Subnetze:\t{ip.AnzahlNachbarnetze}");
            //Console.ReadKey();
        }

    }

}

