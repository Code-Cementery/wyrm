import csv
from collections import  defaultdict

"""
    filters redundant neuron connections
"""

connectome = defaultdict(int)

with open('NeuronData07312014.csv') as f:
    cf = csv.DictReader(f)

    for row in cf:
        connectome[row['Presynaptic'], row['Postsynaptic']] = int(row['Weight'])

with open('CElegansConnectome_Latest.csv', 'w', newline='') as fh:
    cr = csv.writer(fh, delimiter=',', quoting=csv.QUOTE_NONE)

    cr.writerow(('PreSynaptic', 'PostSynaptic', 'Weight'))

    for (f, t), w in connectome.items():
        cr.writerow((f, t, w))
