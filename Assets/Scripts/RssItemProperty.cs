using System;
using UnityEngine;

namespace SimpleRss
{
    public class RssItemProperty
    {
        public string Subject;
        public string URL { get; set; }
        public DateTime Date { get; set; }

        public RssItemProperty(string subject, string url, DateTime localTime)
        {
            Subject = subject;
            URL = url;
            Date = localTime;
        }
    }
}