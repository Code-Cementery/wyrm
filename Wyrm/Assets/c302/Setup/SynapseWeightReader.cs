using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace c302
{
    public class SynapseWeightReader : IDisposable
    {

        public Connectome ReadSynapses(TextAsset file)
        {
            // internal variables:
            Dictionary<string, List<(string, int)>> syn = new Dictionary<string, List<(string, int)>>();

            using (var rdr = new StringReader(file.text))
            {
                string[] header = rdr.ReadLine().Split(',');
                string line;

                while ((line = rdr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    string preSyn = values[0];
                    string postSyn = values[1];
                    int fireWeight = int.Parse(values[2]);

                    // make mark of new synapse & init state
                    if (!syn.ContainsKey(preSyn))
                        syn[preSyn] = new List<(string, int)>();

                    syn[preSyn].Add((postSyn, fireWeight));
                }
            }

            return new Connectome(syn);
        }

        public void Dispose()
        {
        }

    }
}
