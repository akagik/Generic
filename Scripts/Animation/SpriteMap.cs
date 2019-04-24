using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteMap : ScriptableObject {
    public Entry[] elements;

    [NonSerialized]
    Dictionary<string, Sprite> mapper;

    public Sprite GetSprite(Sprite pre) {
        if (mapper == null) {
            mapper = new Dictionary<string, Sprite>();

            for (int i = 0; i < elements.Length; i++) {
                mapper.Add(elements[i].pre.name, elements[i].post);
            }
        }

        Sprite sprite;
        if (mapper.TryGetValue(pre.name, out sprite)) {
            return sprite;
        }
        return pre;
    }

    [Serializable]
    public class Entry {
        public Sprite pre;
        public Sprite post;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Generic/SpriteMap")]
    public static void CreateInstance() {
        SpriteMap obj = ScriptableObject.CreateInstance<SpriteMap>();
        Generic.ScriptableObjectCreator.Create<SpriteMap>(obj, name: "NewSpriteMap");
    }
#endif
}
