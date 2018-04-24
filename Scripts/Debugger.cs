using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using MsgPack;


namespace Generic
{
    public class Debugger : MonoBehaviour
    {
        private int count = 0;

        protected void Start()
        {

        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.T))
            {
                //StartCoroutine(ScreenshotEncode());

                string tempdir = string.Format("{0}/{1}", Application.persistentDataPath, "temp");
                string file = "medadata";
                string metapath = string.Format("{0}/{1}", tempdir, file);

                if (!Directory.Exists(tempdir))
                {
                    Directory.CreateDirectory(tempdir);
                }

                int id = load(metapath);

                string path = string.Format("{0}/{1}{2}.png", tempdir, "screenshot", id);

                ScreenCapture.CaptureScreenshot(path);

                save(id + 1, metapath);
                Debug.Log("CaptureScreenshot: " + path);
            }
        }

        private void save(int id, string path)
        {
            ObjectPacker packer = new ObjectPacker();
            byte[] pack = packer.Pack(new List<int> { id });

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(pack.Length);
                bw.Write(pack);
            }
        }

        private int load(string path)
        {
            byte[] ivBytes = null;

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int length = br.ReadInt32();
                    ivBytes = br.ReadBytes(length);
                }

                ObjectPacker packer = new ObjectPacker();
                return packer.Unpack<List<int>>(ivBytes)[0];
            }
            catch (FileNotFoundException)
            {
                save(0, path);
            }
            return 0;
        }

        IEnumerator ScreenshotEncode()
        {
            // wait for graphics to render
            yield return new WaitForEndOfFrame();

            // create a texture to pass to encoding
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            // put buffer into texture
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
            yield return 0;

            byte[] bytes = texture.EncodeToPNG();

            // save our test image (could also upload to WWW)
            File.WriteAllBytes(Application.dataPath + "/../testscreen-" + count + ".png", bytes);
            count++;

            // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
            DestroyObject(texture);

            //Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
        }
    }
}