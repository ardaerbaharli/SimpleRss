using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRss
{
    public class RssItemsList : MonoBehaviour
    {
        public List<RssItem> rssItems = new List<RssItem>();

        private float rssItemHeight;
        private float height;
        public float rssItemStartPos;
        private RectTransform rect;

        private void Start()
        {
            rect = GetComponent<RectTransform>();
            rssItemHeight = 0;
            rssItemStartPos = rect.rect.height / 4 * 3 - rssItemHeight / 2;
            UpdateSize();
        }

        public void Add(RssItem item)
        {
            item.transform.Find("Remove").GetComponent<Button>().onClick.AddListener(() => Remove(item));

            rssItems.Add(item);
            if (rssItemHeight == 0)
                rssItemHeight = item.GetComponent<RssItem>().height;

            UpdateSize();
        }

        public void Remove(RssItem item)
        {
            rssItems.Remove(item);
            Destroy(item.gameObject);
            UpdateSize();
        }

        public void UpdateSize()
        {
            height = Screen.height / 2;
            var itemHeight = rssItemHeight * rssItems.Count + rssItems.Count * 10;
            if (itemHeight > height)
                height = itemHeight;

            rect.sizeDelta = new Vector2(0, height);
            rssItemStartPos = rect.rect.height / 2 - rssItemHeight / 2;

            foreach (var item in rssItems)
            {
                item.UpdatePosition();
            }
        }

        public RssItem Get(string sourceName)
        {
            return rssItems.Find(x => x.gameObject.name == sourceName);
        }
    }
}