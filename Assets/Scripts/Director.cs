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
        yield return null;
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                documentText = reader.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Debug.Log($"Exception {e}");
        }
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
        while (node != null);
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
            XmlNode child = node.FirstChild;
            do
            {
                if (child.NodeType != XmlNodeType.Text)
                {
                    return child;
                }
                child = child.NextSibling;
            } while (child != null);
        }
        XmlNode sibling = node.NextSibling;
        while (sibling != null)
        {
            if (sibling.NodeType != XmlNodeType.Text)
            {
                return sibling;
            }
            sibling = sibling.NextSibling;
        }
        return node.ParentNode.NextSibling;
    }
}