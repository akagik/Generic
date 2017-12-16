//
// Copyright 2011 Kazuki Oikawa
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MsgPack
{
    public class ReflectionCacheEntry
    {
        const BindingFlags FieldBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField;

        public ReflectionCacheEntry(Type t)
        {
            FieldInfo[] fields = t.GetFields(FieldBindingFlags);
            IDictionary<string, FieldInfo> map = new Dictionary<string, FieldInfo>();
            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo f = fields[i];

                // NotSerialized Field はここで弾く.
                if (!f.IsNotSerialized)
                {
                    string name = f.Name;
                    int pos;
                    if (name[0] == '<' && (pos = name.IndexOf('>')) > 1)
                        name = name.Substring(1, pos - 1); // Auto-Property (\<.+\>) <ab>
                    map[name] = f;
                }
            }
            FieldMap = map;
        }

        public IDictionary<string, FieldInfo> FieldMap { get; private set; }

        public override string ToString()
        {
            string ret = "ReflectionMap:\n";
            foreach (KeyValuePair<string, FieldInfo> pair in FieldMap)
            {
                ret += "\t- [" + pair.Key + "]\n";
            }
            return ret;
        }
    }
}
