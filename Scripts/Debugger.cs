using UnityEngine;
using System.Collections;
using System.IO;


namespace Generic
{
    public class Debugger : MonoBehaviour
    {
        private int count = 0;
        private static string savedKey = "Generic/screenshotId";

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.T))
            {
                //StartCoroutine(ScreenshotEncode());

                int id = PlayerPrefs.GetInt(savedKey);

                string tempdir = string.Format("{0}/{1}", Application.persistentDataPath, "temp");

                if (!Directory.Exists(tempdir))
                {
                    Directory.CreateDirectory(tempdir);
                }

                string path = string.Format("{0}/{1}{2}.png", tempdir, "screenshot", id);

                ScreenCapture.CaptureScreenshot(path);

                PlayerPrefs.SetInt(savedKey, id + 1);
                PlayerPrefs.Save();
                Debug.Log("CaptureScreenshot: " + path);
            }
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