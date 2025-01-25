using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

class Director : Singleton<Director>
{
    private string documentText = null;
    private XmlDocument currentDocument = new XmlDocument();

    private Stack<ChapterElementHandler> chapterStack = new Stack<ChapterElementHandler>();

    public Action<string, ChapterElement> ElementReadEvent;

    private readonly Dictionary<string, Func<ChapterElementHandler>> handlerFactories = new Dictionary<string, Func<ChapterElementHandler>>
    {
        { "Chapter", () => new ChapterHandler() },
        { "Speech", () => new SpeechHandler() },
        { "Choice", () => new ChoiceHandler() },
    };

    public IEnumerator LoadScript(ChapterData script)
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
            throw;
        }
#endif
    }

    public void ReadScript()
    {
        //Debug.Log($"Read data begining: {documentText.Substring(0, 100)}");
        try
        {
            currentDocument.LoadXml(documentText);
        }
        catch (Exception e)
        {
            Debug.LogError($"Read XML exception: {e}");
            throw;
        }
    }

    public IEnumerator RunScript()
    {
        XmlNode node = currentDocument.FirstChild;
        do
        {
            node = ProcessNode(node);
            yield return null;
        }
        while (chapterStack.Count != 0);
    }

    private XmlNode ProcessNode(XmlNode node)
    {
        ChapterElementHandler handler;
        if (node == null)
        {
            handler = chapterStack.Pop();
        }
        else
        {
            if (handlerFactories.TryGetValue(node.Name, out Func<ChapterElementHandler> handlerFactory))
            {
                handler = handlerFactory();
            }
            else
            {
                handler = new ChapterElementHandler();
                Debug.LogWarning($"Unknown chapter element: {node.Name}");
            }
            handler.Init(node);
            handler.Enter();
        }

        XmlNode child = handler.GetNextChild();
        while (child != null && child.NodeType == XmlNodeType.Text)
        {
            child = handler.GetNextChild();
        }
        if (child != null)
        {
            chapterStack.Push(handler);
            return child;
        }

        handler.Exit();
        return null;
    }
}