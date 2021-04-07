import csv


def ow_iter_sensory(fn):
    with open(fn) as fh:
        for row in csv.DictReader(fh):
            neuron = row['Neuron']
            _sense = row['Landmark']
            _type = row['Neurotransmitter']

            yield neuron, _type, _sense


def ow_iter_connectome(fn):
    with open(fn) as fh:
        for row in csv.DictReader(fh):
            _type = row['Neurotransmitter']  # Generic_GJ | Dopamine | Glutamate | Serotonin | FMRFamide | Acetylcholine
            _type2 = row['Type']  # GapJunction | Send
            _nrconn = int(row['Number of Connections'])

            yield (row['Origin'], row['Target']), (_type, _type2), int(_nrconn)


def ow_iter_muscle(fn):
    with open(fn) as fh:
        for row in csv.DictReader(fh):
            _type = row['Neurotransmitter']
            _nrconn = int(row['Number of Connections'])

            yield (row['Neuron'], row['Muscle']), _type, int(_nrconn)
