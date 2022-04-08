using System;
using System.Collections;
using ardaerbaharli;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")] [SerializeField]
    private GameObject rssForm;

    [SerializeField] private InputField rssFormInput;
    [SerializeField] private Transform rssSourcesHolder;
    private RssSourcesList rssSourcesList;

    [Header("Prefabs")] [SerializeField] private GameObject rssSourcePrefab;


    private void Start()
    {
        rssSourcesList = rssSourcesHolder.GetComponent<RssSourcesList>();
    }

    public void ShowRssSourceForm()
    {
        rssForm.SetActive(true);
    }

    public void AddRssSource()
    {
        var itemURL = rssFormInput.text;
        if (!ValidateInput(itemURL)) return;

        var source = Instantiate(rssSourcePrefab, rssSourcesHolder).GetComponent<RssSource>();
        source.UpdatePosition();
        source.URL = itemURL;
        source.Host = itemURL.GetHost();
        
        var startIndex = source.Host.IndexOf('.') + 1;
        var subLength = source.Host.LastIndexOf('.') - source.Host.IndexOf('.') - 1;
        if (subLength < 0)
        {
            subLength = startIndex - 1;
            startIndex = 0;
        }

        source.Title = source.Host.Substring(startIndex, subLength);
        source.name = source.Title;
        source.UpdateTitleUI();
        rssSourcesList.Add(source);
    }


    private bool ValidateInput(string text)
    {
        return text.IsValidURL();
    }

    public void RemoveRssSource()
    {
        var itemName = EventSystem.current.currentSelectedGameObject.name;
        var item = rssSourcesList.Get(itemName);
        rssSourcesList.Remove(item);
    }
}