import csv
from collections import  defaultdict

import readers

"""
    filters redundant neuron connections
    from lego weight dataset
"""

connectome = defaultdict(int)


for (f,t), w in readers.leg_iter_connectome('NeuronData07312014.csv'):
    connectome[f, t] = w

with open('CElegansConnectome_Latest.csv', 'w', newline='') as fh:
    cr = csv.writer(fh, delimiter=',', quoting=csv.QUOTE_NONE)

    cr.writerow(('PreSynaptic', 'PostSynaptic', 'Weight'))

    for (f, t), w in connectome.items():
        cr.writerow((f, t, w))
