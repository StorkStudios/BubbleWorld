using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

class Director : Singleton<Director>
{
    private XmlDocument currentDocument = new XmlDocument();

    public IEnumerator LoadScript(Script script)
    {
        string path = script.Path;
#if UNITY_WEBGL
        using (UnityWebRequest request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();
            Debug.Log($"Result of request to {script.Path}: {request.result}");
            string text = request.downloadHandler.text;
            Debug.Log($"Read data begining: {text.Substring(0, 100)}");
            currentDocument.LoadXml(text);
        }
#else
            StreamReader reader = new StreamReader(path);
            string text = reader.ReadToEnd();
            Debug.Log($"Read data begining: {text.Substring(0, 100)}");
            currentDocument.LoadXml(text);
#endif
    }
}