using Analyzer.lib;

namespace IPAnalyzer.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LblStructure.Text =
                $"IP Adresse:\n" +
                $"Subnetzmaske:\n" +
                $"Netzwerkadresse:\n" +
                $"Broadcastadresse:\n" +
                $"Anzahl Hosts:\n" +
                $"Anzahl Subnetze:\n";
        }

        private void CmdStartIPLogic_Click(object sender, EventArgs e)
        {
            //if(TBxInputSubnetmask.Text is null)
            //{
                //try
                //{
                    IPLogic Task = new(TBxInputIPAdress.Text);
                    LblOutput.Text = 
                         
                    $"{Task.IPAdresse[0]}.{Task.IPAdresse[1]}.{Task.IPAdresse[2]}.{Task.IPAdresse[3]}\n" +
                    $"{Task.Subnetzmaske[0]}.{Task.Subnetzmaske[1]}.{Task.Subnetzmaske[2]}.{Task.Subnetzmaske[3]}\n" +
                    $"{Task.NetzwerkAdresse[0]}.{Task.NetzwerkAdresse[1]}.{Task.NetzwerkAdresse[2]}.{Task.NetzwerkAdresse[3]}\n" + 
                    $"{Task.BroadcastAdresse[0]}.{Task.BroadcastAdresse[1]}.{Task.BroadcastAdresse[2]}.{Task.BroadcastAdresse[3]}\n" +
                    $"{Task.AnzahlHosts}\n" +
                    $"{Task.AnzahlNachbarnetze}";
                    
                //}
                //catch(FormatException) // xxx.xxx.xxx.xxx/45 TODO: Googlen
                //{
                //    MessageBox.Show("Die CIDR fehlt.");
                    
                //}

            //}
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}