from collections import defaultdict
import sys
import readers

"""
    filters 3 csv into one (muscle, sensor, neuron syn)
    from openworm weight dataset
"""

connectome = defaultdict(int)


# 1. sensory csv
for neuron, sense, neurotrans in readers.ow_iter_sensory('OpenWormWeights/Sensory.csv'):
    pass
    # @TODO: ITT: weight by type

# 2. neuron csv
for (nf, nt), (neurotrans, ntype), (connections, weight) in readers.ow_iter_connectome('OpenWormWeights/Connectome.csv'):
    connectome[nf, nt] = weight

# 3. muscle connection csv
for (nf, mt), neurotrans, (connections, weight) in readers.ow_iter_muscle('OpenWormWeights/NeuronsToMuscle.csv'):
    connectome[nf, mt] = weight

sys.exit()

# Write
with open('CElegansConnectome_OpenWorm.csv', 'w', newline='') as fh:
    cr = csv.writer(fh, delimiter=',', quoting=csv.QUOTE_NONE)

    cr.writerow(('PreSynaptic', 'PostSynaptic', 'Weight'))

    for (f, t), w in connectome.items():
        cr.writerow((f, t, w))
