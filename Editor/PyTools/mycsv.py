import csv

def csv2list(path):
    with open(path) as f:
        l = [{k: v for k, v in row.items()} for row in csv.DictReader(f, skipinitialspace=True)]

    return l
