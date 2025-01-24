using System;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

class Director : Singleton<Director>
{
    private string documentText = null;
    private XmlDocument currentDocument = new XmlDocument();

    public bool ScriptLoaded => documentText != null;

    public IEnumerator LoadScript(Script script)
    {
        string path = script.Path;
#if (UNITY_WEBGL && !UNITY_EDITOR)
        using (UnityWebRequest request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();
            Debug.Log($"Result of request to {script.Path}: {request.result}");
            documentText = request.downloadHandler.text;
        }
#else
        StreamReader reader = new StreamReader(path);
        documentText = reader.ReadToEnd();
        yield return null;
#endif
    }

    public void ReadScript()
    {
        Debug.Log($"Read data begining: {documentText.Substring(0, 100)}");
        try
        {
            currentDocument.LoadXml(documentText);
        }
        catch (Exception e)
        {
            Debug.LogError($"Read XML exception: {e}");
        }
    }

    public void RunScript()
    {
        Debug.Log("Run script");
        XmlNode node = currentDocument.FirstChild;
        do
        {
            node = ProcessNode(node);
        }
        while (node != currentDocument);
    }

    private XmlNode ProcessNode(XmlNode node)
    {
        Debug.Log(node.Name);
        return GetNextNode(node);
    }

    private XmlNode GetNextNode(XmlNode node)
    {
        if (node.HasChildNodes)
        {
            return node.FirstChild;
        }
        XmlNode sibling = node.NextSibling;
        if (sibling != null)
        {
            return sibling;
        }
        return node.ParentNode;
    }
}