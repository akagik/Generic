# Generic
Unity で使われる共通スクリプト.

# Localization
1. ローカリゼーションをするためには *PROJECT_ROOT*/Assets/Editor/Localization 以下に csv ファイルを用意する.
csv ファイルの 1行目は KEY,TYPE,en,ja,ko,zh のようにする.
TYPE には "Header", "Content" のいずれかをいれる。空にするとデフォルトで Header になる.
Localization フォルダ以下の全ての .csv ファイルが対象となる.

2. *PROJECT_ROOT*/Assets/Resources/Localization/en などに各言語で使われるフォントファイルを置く。
フォントファイル名は contentFont と headerFont にする。
特定の言語のフォントファイルがない場合はデフォルト言語のフォントファイルを使用する。
少なくともデフォルト言語にはフォントファイルを置く必要がある。

3. Editor/PyTools にディレクトリを移動し python3 extract_localization.py *PROJECT_ROOT* を実行する。
これにより *PROJECT_ROOT*/Assets/Resources/Localization/en などに strings.txt ファイルが自動生成される。

フォルダ構成
* *PROJECT_ROOT*/Generic
* *PROJECT_ROOT*/Editor/Localization
* *PROJECT_ROOT*/Resources/Localization

