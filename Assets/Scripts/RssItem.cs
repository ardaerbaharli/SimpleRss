using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRss
{
    public class RssItem : MonoBehaviour
    {
        [SerializeField] private Text subjectText;
        [SerializeField] private Text dateText;

        private RssItemProperty rssItemProperties;
        public void SetProperties(RssItemProperty properties) => rssItemProperties = properties;


        public float height;
        private RectTransform rect;
        private RssItemsList parent;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            parent = transform.parent.GetComponent<RssItemsList>();
            height = rect.rect.height;
        }

        public void SetGUI()
        {
            subjectText.text = rssItemProperties.Subject;

            if (rssItemProperties.Date != default)
            {
                var date = DateTime.Now - rssItemProperties.Date;
                var dateTextParsed = "";

                if (date.Days > 0)
                    dateTextParsed = date.Days.ToString() + "d ago";
                else if (date.Hours > 0)
                    dateTextParsed = date.Hours.ToString() + "h ago";
                else if (date.Minutes > 0)
                    dateTextParsed = date.Minutes.ToString() + "m ago";
                else if (date.Seconds > 0)
                    dateTextParsed = date.Seconds.ToString() + "s ago";

                dateText.text = dateTextParsed;
            }
        }

        public void SetButton()
        {
            GetComponent<Button>().onClick.AddListener(() => OnItemClick());
        }

        public void OnItemClick()
        {
            if (rssItemProperties.URL != null)
                Application.OpenURL(rssItemProperties.URL);
        }

        public void UpdatePosition()
        {
            rect.anchoredPosition3D = GetPos();
        }

        private Vector3 GetPos()
        {
            var index = transform.GetSiblingIndex();
            if (index == 0)
                return new Vector3(0, parent.rssItemStartPos, 0);
            var h = parent.rssItemStartPos - height * index - 10 * index;
            return new Vector3(0, h, 0);
        }
    }
}