import os
import json
from mycsv import csv2list
from code_generator import CodeGenerator

CSV_PATH = "../Localization/strings.csv"
RESOURCES_PATH = "../../Resources/Localization"
SCRIPTS_PATH = "../../Scripts/Localization/LocalizationKey.cs"
ENUM_NAME = "LocalizationKey"

def get_enum_value(key):
    words = key.strip().split(" ")
    words = ["{}{}".format(w[0].upper(), w[1:]) for w in words]

    return "".join(words)

def get_key_value(key):
    return "_".join(key.lower().strip().split(" "))

def get_langs(keys):
    ret = []

    for key in keys:
        if key != "KEY" and key != "TYPE":
            ret.append(key)
    return ret

def get_json_list(l, lang):
    d = {"items": []}

    for item in l:
        jsonitem = {}
        jsonitem["key"] = get_key_value(item["KEY"])
        jsonitem["type"] = item["TYPE"]
        jsonitem["value"] = item[lang]
        d["items"].append(jsonitem)
    return d

if __name__ == "__main__":
    l = csv2list(CSV_PATH)

    cg = CodeGenerator()
    cg.put_using("UnityEngine")
    cg.put_using("System.Collections.Generic")
    cg.put_blank()
    cg.put_attributre("System.Serializable")
    cg.put_enum(ENUM_NAME)

    for item in l:
        cg.put_enum_elem(get_enum_value(item["KEY"]))

    cg.end_enum()


    cg.put_blank()
    cg.put_class("{}Extension".format(ENUM_NAME), is_static=True)

    exp = "private static string[] names = {{{}".format(cg.LINE_FEED_CODE)
    cg.indent += 1
    
    for item in l:
        exp += "{}\"{}\",{}".format(
            cg.indent_str,
            get_key_value(item["KEY"]),
            cg.LINE_FEED_CODE
        )
    cg.indent -= 1
    exp += "{}}}".format(cg.indent_str)

    cg.put_exp(exp)
    cg.put_blank()

    cg.put_extension_function("string", "GetKey", "LocalizationKey key")
    cg.put_exp("return names[(int)key]")
    cg.end_function()
    cg.end_class()

    with open(SCRIPTS_PATH, mode="w") as f:
        f.write(cg.code)
        print("write " + SCRIPTS_PATH)

    for lang in get_langs(list(l[0].keys())):
        dir_path = "{}/{}".format(RESOURCES_PATH, lang)
        path = "{}/strings.txt".format(dir_path)

        if not os.path.isdir(dir_path):
            os.makedirs(dir_path)
            print("mkdir {}".format(dir_path))

        with open(path, mode="w") as f:
            d = get_json_list(l, lang)
            json.dump(d, f,
                ensure_ascii=False,
                indent=4,
                sort_keys=True,
            )
            print("write " + path)


