using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RssSourcesList : MonoBehaviour
{
    public List<RssSource> rssSources = new List<RssSource>();

    private float rssSourceHeight;
    private float height;
    public float rssSourceStartPos;
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        rssSourceHeight = 0;
        rssSourceStartPos = rect.rect.height / 2 - rssSourceHeight / 2;
        StartCoroutine(UpdateSize());
    }

    public void Add(RssSource source)
    {
        source.transform.Find("Remove").GetComponent<Button>().onClick.AddListener(() => Remove(source));
        rssSources.Add(source);
        if (rssSourceHeight == 0)
            rssSourceHeight = source.GetComponent<RssSource>().height;

        StartCoroutine(UpdateSize());
    }

    public void Remove(RssSource source)
    {
        rssSources.Remove(source);
        Destroy(source.gameObject);
        StartCoroutine(UpdateSize());
    }

    public IEnumerator UpdateSize()
    {
        yield return new WaitForEndOfFrame();
        height = Screen.height / 2;
        var sourceHeight = rssSourceHeight * rssSources.Count + rssSources.Count * 10;
        if (sourceHeight > height)
            height = sourceHeight;

        rect.sizeDelta = new Vector2(0, height);
        rssSourceStartPos = rect.rect.height / 2 - rssSourceHeight / 2;

        foreach (var rssSource in rssSources)
        {
            rssSource.UpdatePosition();
        }
    }

    public RssSource Get(string sourceName)
    {
        return rssSources.Find(x => x.gameObject.name == sourceName);
    }
}