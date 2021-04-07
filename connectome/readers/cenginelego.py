import csv


def leg_iter_connectome(fn):
    with open(fn) as fh:
        for row in csv.DictReader(fh):
            yield (row['PreSynaptic'], row['PostSynaptic']), int(row['Weight'])
