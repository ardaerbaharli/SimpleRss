using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using SimpleRss;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RSSManager : MonoBehaviour
{
    [SerializeField] private GameObject rssItemPrefab;
    [SerializeField] private Transform rssItemsHolder;

    private RssItemsList rssItemsList;

    public RssSource activeRssSource;
    public void SetActiveFeed(RssSource rssSource) => activeRssSource = rssSource;


    private void Start()
    {
        rssItemsList = rssItemsHolder.GetComponent<RssItemsList>();
        activeRssSource = null;
    }

    public async void LoadFeed()
    {
        var rssItemProperties = await GetFeedItems();
        foreach (var property in rssItemProperties)
        {
            if (SceneManager.GetActiveScene().name.Equals("Main"))
                break;
            var rssItemObject = Instantiate(rssItemPrefab, rssItemsHolder);
            var rssItem = rssItemObject.GetComponent<RssItem>();
            rssItem.SetProperties(property);
            rssItem.SetGUI();
            rssItem.SetButton();
            rssItemsList.Add(rssItem);
        }
    }

    private async Task<List<RssItemProperty>> GetFeedItems()
    {
        var url = activeRssSource.rssSourceProperty.URL;

        var feed = await FeedReader.ReadAsync(url);
        var rssItemProperties = new List<RssItemProperty>();

        foreach (var item in feed.Items)
        {
            var subject = item.Title;

            DateTime localTime = default;
            if (item.PublishingDate != null)
            {
                var date = (DateTime) item.PublishingDate;
                var utcTime = new DateTime(date.Ticks, DateTimeKind.Utc);
                localTime = utcTime.ToLocalTime();
            }

            var itemURL = item.Link;

            var feedItem = new RssItemProperty(subject, itemURL, localTime);
            rssItemProperties.Add(feedItem);
        }

        return rssItemProperties;
    }
}