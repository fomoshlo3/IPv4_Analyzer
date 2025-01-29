using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Analyzer.lib.Features
{
    public class NetworkHistory
    {
        private const string FilePath = "network_history.json";
        private readonly Queue<NetworkCalculator> _history = new Queue<NetworkCalculator>(3);

        public NetworkHistory()
        {
            Load();
        }

        public void Add(NetworkCalculator network)
        {
            if (_history.Count == 3)
            {
                _history.Dequeue(); // Remove the oldest entry
            }
            _history.Enqueue(network);
            Save();
        }

        public IEnumerable<NetworkCalculator> GetHistory()
        {
            return _history;
        }

        private void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_history, options);
            File.WriteAllText(FilePath, json);
        }

        private void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var history = JsonSerializer.Deserialize<Queue<NetworkCalculator>>(json);
                if (history != null)
                {
                    _history = history;
                }
            }
        }
    }
}
