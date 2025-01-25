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
    public Action<string> DirectorStepEvent;

    private XmlNode currentNode;

    private readonly Dictionary<string, Func<ChapterElementHandler>> handlerFactories = new Dictionary<string, Func<ChapterElementHandler>>
    {
        { "Chapter", () => new IterationHandler() },
        { "Speech", () => new SpeechHandler() },
        { "Choice", () => new ChoiceHandler() },
        { "Case", () => new IterationHandler() },
        { "Enter", () => new DeserializeHandler<Enter>() },
        { "Exit", () => new DeserializeHandler<Exit>() },
        { "Update", () => new DeserializeHandler<Update>() },
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
        currentNode = currentDocument.FirstChild;
        do
        {
            yield return ProcessNode();
        }
        while (chapterStack.Count != 0);
    }

    private IEnumerator ProcessNode()
    {
        ChapterElementHandler handler;
        if (currentNode == null)
        {
            handler = chapterStack.Pop();
        }
        else
        {
            if (handlerFactories.TryGetValue(currentNode.Name, out Func<ChapterElementHandler> handlerFactory))
            {
                handler = handlerFactory();
            }
            else
            {
                handler = new ChapterElementHandler();
                Debug.LogWarning($"Unknown chapter element: {currentNode.Name}");
            }
            handler.Init(currentNode);
            yield return handler.Enter();
        }

        XmlNode child = handler.GetNextChild();
        while (child != null && child.NodeType == XmlNodeType.Text)
        {
            child = handler.GetNextChild();
        }
        if (child != null)
        {
            chapterStack.Push(handler);
            currentNode = child;
        }
        else
        {
            yield return handler.Exit();
            currentNode = null;
        }
    }
}