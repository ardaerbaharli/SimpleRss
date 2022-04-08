using System;
using System.Collections;
using ardaerbaharli;
using SimpleRss;
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

    private SourceHandler sourceHandler;

    private void Start()
    {
        rssSourcesList = rssSourcesHolder.GetComponent<RssSourcesList>();
        sourceHandler = new SourceHandler();
        var rssSources = sourceHandler.LoadAll();
        rssSources.ForEach(CreateSource);
    }

    public void ShowRssSourceForm()
    {
        rssForm.SetActive(true);
    }

    private void HideRssSourceForm()
    {
        rssForm.SetActive(false);
    }

    public void AddRssSourceButton()
    {
        var itemURL = rssFormInput.text;
        if (!ValidateInput(itemURL)) return;


        var rssSourceProperty = new RssSourceProperty();
        rssSourceProperty.URL = itemURL;
        rssSourceProperty.Host = itemURL.GetHost();

        var startIndex = rssSourceProperty.Host.IndexOf('.') + 1;
        var subLength = rssSourceProperty.Host.LastIndexOf('.') - rssSourceProperty.Host.IndexOf('.') - 1;
        if (subLength < 0)
        {
            subLength = startIndex - 1;
            startIndex = 0;
        }

        rssSourceProperty.Title = rssSourceProperty.Host.Substring(startIndex, subLength);
        CreateSource(rssSourceProperty);
        sourceHandler.Save(rssSourceProperty);

        HideRssSourceForm();
    }

    private void CreateSource(RssSourceProperty rssSourceProperty)
    {
        var source = Instantiate(rssSourcePrefab, rssSourcesHolder).GetComponent<RssSource>();
        source.UpdatePosition();

        source.name = rssSourceProperty.Title;
        source.rssSourceProperty = rssSourceProperty;

        source.UpdateUI();
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