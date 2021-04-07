from collections import defaultdict
import sys
import readers
import pandas as pd
import matplotlib.pyplot as plt


"""
    finds correlation between datasets
"""

neurotrans_sensory = {'FMRFamide', 'Acetylcholine', 'Dopamine', 'Glutamate', 'Tyramine', 'Serotonin'}
neurotrans_connectome = {'FMRFamide', 'Acetylcholine', 'Dopamine', 'Generic_GJ', 'Acetylcholine_Tyramine', 'Glutamate', 'Serotonin', 'Serotonin_Acetylcholine', 'Serotonin_Glutamate', 'GABA', 'Octapamine'}
neurotrans_muscle = {'FMRFamide', '', 'Acetylcholine', 'Dopamine', 'Glutamate', 'Serotonin', 'FRMFemide', 'GABA', 'Serotonin, Acetylcholine', 'Acetylcholine, Tyramine'}

neurotrans = neurotrans_sensory | neurotrans_muscle | neurotrans_connectome
n_type = {'GapJunction', 'Send'}

# df_sensor = pd.read_csv('OpenWormWeights/Sensory.csv')
# df_conn = pd.read_csv('OpenWormWeights/Connectome.csv')
# df_muscle = pd.read_csv('OpenWormWeights/NeuronsToMuscle.csv')

data = {}

# 1. sensory csv
for neuron, sense, neurotrans in readers.ow_iter_sensory('OpenWormWeights/Sensory.csv'):
    pass

# 2. neuron csv
for (nf, nt), (neurotrans, ntype), connections in readers.ow_iter_connectome('OpenWormWeights/Connectome.csv'):
    data[nf,nt] = ([nf,nt,neurotrans,connections,0,'conn'])
    pass

# 3. muscle connection csv
for (nf, mt), neurotrans, connections in readers.ow_iter_muscle('OpenWormWeights/NeuronsToMuscle.csv'):
    data[nf,mt] = ([nf,mt,neurotrans,connections,0,'muscle'])
    pass

# 4. add lego weights
for tpl, weight in readers.leg_iter_connectome('LegoWeights/CElegansConnectome.csv'):
    if tpl not in data:
        # print("Data skipped:", tpl)
        pass
    else:
        data[tpl][4] = weight

# create dataframe
df = pd.DataFrame(data.values(), columns=['Neuron', 'Target', 'Transmitter', 'Connections', 'Weight', 'Conntype'])
# idx = pd.MultiIndex.from_frame(df, names=["Neuron", "Target"])
# df.set_index(idx)
groups = df.groupby('Conntype')

# ==========================================================================
corr3 = groups['Connections', 'Weight'].corr()
print("Connections~Weight corr:", corr3)

#       >>> Connections == Weights for interneurons
#       >>> this shows a great bias in muscular weights.. perhaps other columns define them
print("\n")
# ==========================================================================

def unique_pairs(_df, *cols, save: str=None):
    _pp = _df.groupby(list(cols)).size().reset_index().rename(columns={0: 'count'})

    if save:
        lbl = f'out_data/pairs_{cols[0].lower()}_{cols[1].lower()}.csv' if isinstance(save, bool) else 'out_data/'+save+'.csv'
        _pp.to_csv(lbl)
    return _pp

p1 = unique_pairs(df, 'Transmitter', 'Conntype', save='transmitter_count')
p2 = unique_pairs(df, 'Conntype', 'Connections', save='connection_histogram')

df_musc = groups.get_group('muscle')
df_conn = groups.get_group('conn')
p3 = unique_pairs(df_conn, 'Transmitter', 'Connections', save='transmitter_connection_histogram')

# Transmitter', 'Connections', 'Weight', 'Conntype
#p4 = unique_pairs(df_conn, 'Neuron', 'Target', 'Weight', save=False)
#print(p4)

# ==========================================================================
