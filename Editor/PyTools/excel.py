from pyexcel_xlsx import get_data
from mytype import Type


class ExcelReader:
    def __init__(self, path, header_index=0):
        self.path = path
        self.header_index = header_index
        self.result = {}
        self.data = None

    def read(self):
        self.data = get_data(self.path)

        for sheet_name, data in self.data.items():
            self.result[sheet_name] = self._analyze(data)

    def _analyze(self, data):
        v = []

        parameters = []
        for key in data[self.header_index]:
            if not key:
                break
            parameters.append(self._analyze_parameter(key))

        for i in range(self.header_index + 1, len(data)):
            r = data[i]
            row_value = []

            for j in range(len(parameters)):
                if j < len(r):
                    cell = r[j]
                else:
                    cell = ""

                type = parameters[j][1]
                try:
                    row_value.append(self._analyze_cell(cell, type))
                except:
                    print("({}, {}) でエラー".format(i + 1, j + 1))

            v.append(row_value)

        return {'param': parameters, 'value': v}

    @staticmethod
    def _first_lower(s):
        return s[0].lower() + s[1:]

    def _analyze_parameter(self, text):
        if '(' not in text:
            return self._first_lower(text.strip()), None

        first = text.index('(')
        end = text.index(')')

        name = self._first_lower(text[:first].strip())
        type = Type.value_of(text[first + 1:end])

        return name, type

    def _analyze_cell(self, cell, type):
        if type is None:
            return cell
        elif type == Type.INT:
            if not cell:
                return 0
            return int(cell)
        elif type == Type.STRING:
            if not cell:
                return ""
            return cell
        elif type == Type.FLOAT:
            return float(cell)

        if type.is_list:
            if not cell:
                return []

            cell = str(cell).strip()

            if not cell:
                return []

            if cell[0] == '[':
                cell = cell[1:-1]

            if type == Type.INT_LIST:
                return [int(e.strip()) for e in cell.split(',')]
            elif type == Type.STRING_LIST:
                return [e.strip() for e in cell.split(',')]

        return cell

if __name__ == "__main__":
    path = "../シナリオ/アイテム.xlsx"

    excel = ExcelReader(path)
    excel.read()
    result = excel.result

    for s, d in result.items():
        print(s)
        print(d['param'])
        for e in d['value']:
            print(e)

        print("total: {}".format(len(d['value'])))
