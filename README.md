# Generic
Unity で使われる共通スクリプト.

# Localization
1. ローカリゼーションをするためには PROJECT_DIR/Editor/Localization/strings.csv を追加する。
csv ファイルの 1行目は KEY,TYPE,en,ja,ko,zh のようにする。
TYPE には "Header", "Content" のいずれかをいれる。

2. PROJECT_DIR/Resources/Localization/en などに各言語で使われるフォントファイルを置く。
フォントファイル名は contentFont と headerFont にする。
特定の言語のフォントファイルがない場合はデフォルト言語のフォントファイルを使用する。
少なくともデフォルト言語にはフォントファイルを置く必要がある。

3. Editor/PyTools にディレクトリを移動し python3 extract_localization.py GENERICPATH PROJECT_DIR を実行する。
これにより Resources/Localization/en などに strings.csv ファイルが自動生成される。

