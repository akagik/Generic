from enum import Enum
from mytype import Type

class Accessor(Enum):
    PUBLIC = "public"
    PRIVATE = "private"
    PROTECTED = "protected"
    DEFAULT = ""

    @property
    def code(self):
        return self.value


class CodeGenerator:
    LINE_FEED_CODE = "\n"
    TAB_WIDTH = 4
    EXPRESSION_END = ";"

    def __init__(self):
        self.indent = 0
        self.code = ""

    @property
    def indent_str(self):
        return " " * self.indent * self.TAB_WIDTH

    def _put_end_sentence(self):
        self.code += "{}}}{}".format(self.indent_str, self.LINE_FEED_CODE)

    def put_blank(self, size=1):
        for i in range(size):
            self.code += "{}{}".format(self.indent_str, self.LINE_FEED_CODE)

    def put_exp(self, exp):
        self.code += "{}{}{}{}".format(self.indent_str, exp, self.EXPRESSION_END, self.LINE_FEED_CODE)

    def put_using(self, target):
        self.code += "{}using {}{}{}".format(self.indent_str, target, self.EXPRESSION_END, self.LINE_FEED_CODE)

    def put_exp(self, exp):
        self.code += "{}{}{}{}".format(self.indent_str, exp, self.EXPRESSION_END, self.LINE_FEED_CODE)

    def put_namespace(self, name):
        self.code += "{}namespace {}{}".format(self.indent_str, name, self.LINE_FEED_CODE)
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_namespace(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_foreach(self, elem_tyep, varname, list_name):
        self.code += "{}foreach ({} {} in {}){}".format(
            self.indent_str, 
            elem_tyep,
            varname,
            list_name,
            self.LINE_FEED_CODE,
        )
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_foreach(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_if(self, condition):
        self.code += "{}if ({}){}".format(self.indent_str, condition, self.LINE_FEED_CODE)
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def put_elseif(self, condition):
        self.code += "{}else if ({}){}".format(self.indent_str, condition, self.LINE_FEED_CODE)
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_if(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_switch(self, exp):
        self.code += "{}switch ({}){}".format(self.indent_str, exp, self.LINE_FEED_CODE)
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_switch(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_sentence(self, text):
        self.code += "{}{}{}".format(self.indent_str, text, self.LINE_FEED_CODE)
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_sentence(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_case(self, exp):
        self.code += '{}case {}:{}'.format(self.indent_str, exp, self.LINE_FEED_CODE)
        self.indent += 1

    def end_case(self):
        self.indent -= 1

    def put_attributre(self, name, parameter=""):
        self.code += "{}[{}{}]{}".format(
            self.indent_str,
            name,
            "({})".format(parameter) if parameter else "",
            self.LINE_FEED_CODE,
        )

    def put_function(self, return_type, name, parameter, accessor=Accessor.PUBLIC, is_static=False):
        ac_code = "" if accessor == Accessor.DEFAULT else accessor.code + " "
        st_code = "static " if is_static else ""

        self.code += "{}{}{}{} {}({}){}".format(
            self.indent_str,
            ac_code,
            st_code,
            return_type,
            name,
            parameter,
            self.LINE_FEED_CODE,
        )
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def put_extension_function(self, return_type, name, this, parameter="", accessor=Accessor.PUBLIC):
        ac_code = "" if accessor == Accessor.DEFAULT else accessor.code + " "
        st_code = "static "

        params = "this {}{}".format(this, ", {}".format(parameter) if parameter else "")

        self.code += "{}{}{}{} {}({}){}".format(
            self.indent_str,
            ac_code,
            st_code,
            return_type,
            name,
            params,
            self.LINE_FEED_CODE,
        )
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_function(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_class(self, name, accessor=Accessor.PUBLIC, is_static=False):
        ac_code = "" if accessor == Accessor.DEFAULT else accessor.code + " "
        st_code = "static " if is_static else ""

        self.code += "{}{}{}class {}{}".format(
            self.indent_str,
            ac_code,
            st_code,
            name,
            self.LINE_FEED_CODE,
        )
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_class(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_enum(self, name, accessor=Accessor.PUBLIC):
        ac_code = "" if accessor == Accessor.DEFAULT else accessor.code + " "

        self.code += "{}{}enum {}{}".format(
            self.indent_str,
            ac_code,
            name,
            self.LINE_FEED_CODE,
        )
        self.code += "{}{{{}".format(self.indent_str, self.LINE_FEED_CODE)
        self.indent += 1

    def end_enum(self):
        self.indent -= 1
        self._put_end_sentence()

    def put_enum_elem(self, name):
        self.code += "{}{},{}".format(
            self.indent_str,
            name,
            self.LINE_FEED_CODE,
        )

class GenGenerator:
    @classmethod
    def generate(cls, class_name, params, values):
        plen = len(params)

        cg = CodeGenerator()

        cg.put_using("UnityEngine")
        cg.put_using("System.Collections.Generic")
        cg.put_blank()
        cg.put_namespace("Novel")
        cg.put_class("{}Gen".format(class_name), is_static=True)
        cg.put_function("void", "Generate", "out List<{}> l".format(class_name), is_static=True)

        cg.put_exp("l = new List<{}>()".format(class_name))

        for i, row in enumerate(values):
            cg.put_exp("l.Add(new {0}({1}))".format(
                class_name,
                ', '.join(['{}: {}'.format(params[j][0], cls._get_arg(e, params[j][1])) for j, e in enumerate(row)]))
            )

        cg.end_function()
        cg.end_class()
        cg.end_namespace()
        return cg

    @staticmethod
    def _get_arg(e, t):
        if t == Type.INT:
            return str(e)
        elif t == Type.STRING:
            return '"{}"'.format(e)
        elif t == Type.INT_LIST:
            return "new List<{}>(){{{}}}".format("int", ', '.join([str(ee) for ee in e]))
        elif t == Type.STRING_LIST:
            return "new List<{}>(){{{}}}".format("string", ', '.join(['"' + ee + '"' for ee in e]))
        return str(e)

if __name__ == "__main__":
    cg = GenGenerator.generate("Test", [["a", Type.STRING], ["b", Type.INT_LIST]], [["aafaf", [2, 3]]])
    print(cg.code)

