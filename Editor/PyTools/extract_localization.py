#!python3
import os
import os.path
import json
import sys
from mycsv import csv2list
from code_generator import CodeGenerator

if len(sys.argv) <= 1:
    print("Project Root へのパスを引数に与えてください.")
    sys.exit(1)

PROJECT_ROOT = sys.argv[1]

ASSETS_DIR = os.path.join(PROJECT_ROOT, "Assets")
GENERIC_ROOT = os.path.join(PROJECT_ROOT, "Assets", "Generic")

CSV_DIR = ASSETS_DIR + "/Editor/Localization"
RESOURCES_PATH = ASSETS_DIR + "/Resources/Localization"
SCRIPTS_PATH = GENERIC_ROOT + "/Scripts/Localization/LocalizationKey.cs"
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

def check_validation(l):
    print("check validation")

    # TYPE の値が不正な場合は Header を代入する
    for e in l:
        if 'TYPE' not in e:
            e["TYPE"] = "Header"
        elif e["TYPE"] != "Header" and e["TYPE"] != "Content":
            e["TYPE"] = "Header"

    # キーが重複してないかチェック
    s = set()

    for e in l:
        if e['KEY'] in s:
            print("ERROR!!! Duplical Key: {0}".format(e['KEY']))
            sys.exit(1)
        else:
            s.add(e['KEY'])

if __name__ == "__main__":
    print("CSV_DIR='" + CSV_DIR + "'")

    # 読み込みデータのリスト.
    # 各要素は1キーワードを表す辞書.
    l = []
    if os.path.exists(CSV_DIR) and os.path.isdir(CSV_DIR):
        for f in os.listdir(CSV_DIR):
            new_path = os.path.join(CSV_DIR, f)
            storage, ext = os.path.splitext(new_path)

            if not os.path.isdir(new_path) and ext == ".csv":
                print("Loading {}".format(new_path))
                l.extend(csv2list(new_path))
    if len(l) == 0:
        print("Value is empyt, and set default value.")
        l = [{"KEY": "test", "TYPE": "Header", "en": "test"}]

    check_validation(l)

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

    # LocalizationKey.cs を保存する.
    with open(SCRIPTS_PATH, mode="w") as f:
        f.write(cg.code)
        print("write " + SCRIPTS_PATH)

    # 各言語フォルダに strings.txt を保存する.
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


