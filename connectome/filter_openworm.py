import csv
import sys
from collections import  defaultdict

"""
    filters 3 csv into one (muscle, sensor, neuron syn)
    from openworm weight dataset
"""

connectome = defaultdict(int)

# 0. get neurotransmitter weights
# @TODO: ITT: discord?

# {'GapJunction', 'Send'}
# 1 NT:
# {'FMRFamide', 'Acetylcholine', 'Dopamine', 'Glutamate', 'Tyramine', 'Serotonin'}
# 2 NT:
# {'FMRFamide', 'Acetylcholine', 'Dopamine', 'Generic_GJ', 'Acetylcholine_Tyramine', 'Glutamate', 'Serotonin', 'Serotonin_Acetylcholine', 'Serotonin_Glutamate', 'GABA', 'Octapamine'}
# 3 NT:
# {'FMRFamide', '', 'Acetylcholine', 'Dopamine', 'Glutamate', 'Serotonin', 'FRMFemide', 'GABA', 'Serotonin, Acetylcholine', 'Acetylcholine, Tyramine'}


# 1. sensory csv
with open('OpenWormWeights/Sensory.csv') as fh:
    for row in csv.DictReader(fh):
        neuron = row['Neuron']
        _sense = row['Landmark']
        _type = row['Neurotransmitter']

        # @TODO: ITT: weight by type

# 2. neuron csv
with open('OpenWormWeights/Connectome.csv') as fh:
    for row in csv.DictReader(fh):
        _type = row['Neurotransmitter'] # Generic_GJ | Dopamine | Glutamate | Serotonin | FMRFamide | Acetylcholine
        _type2 = row['Type'] # GapJunction | Send
        _nrconn = int(row['Number of Connections'])

        # @TODO: ITT: weight by type
        weight = _nrconn

        connectome[row['Origin'], row['Target']] = weight

# 3. muscle connection csv
with open('OpenWormWeights/NeuronsToMuscle.csv') as fh:
    for row in csv.DictReader(fh):
        _type = row['Neurotransmitter']
        _nrconn = int(row['Number of Connections'])

        # @TODO: ITT: weight by type
        weight = _nrconn

        connectome[row['Neuron'], row['Muscle']] = weight


sys.exit()

# Write
with open('CElegansConnectome_OpenWorm.csv', 'w', newline='') as fh:
    cr = csv.writer(fh, delimiter=',', quoting=csv.QUOTE_NONE)

    cr.writerow(('PreSynaptic', 'PostSynaptic', 'Weight'))

    for (f, t), w in connectome.items():
        cr.writerow((f, t, w))
