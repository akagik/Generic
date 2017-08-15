from enum import Enum


class Type(Enum):
    INT = ('int', False)
    FLOAT = ('float', False)
    STRING = ('string', False)
    INT_LIST = ('list<int>', True)
    STRING_LIST = ('list<string>', True)

    @property
    def text(self):
        return self.value[0]

    @property
    def is_list(self):
        return self.value[1]

    def __repr__(self):
        return self.text

    def __str__(self):
        return self.text

    @classmethod
    def value_of(cls, name):
        for t in cls:
            if t.text.lower() == name:
                return t

        print("{} という名前の Type は存在しない".format(name))

