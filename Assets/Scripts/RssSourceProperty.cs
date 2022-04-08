using System.Collections.Generic;
using UnityEngine;

public class RssSourceProperty
{
    public string URL { get; set; }
    public string Title { get; set; }
    public string Host { get; set; }

    public RssSourceProperty(string url, string title, string host)
    {
        URL = url;
        Title = title;
        Host = host;
    }

    public RssSourceProperty()
    {
    }

    public List<RssSourceProperty> ToList()
    {
        return new List<RssSourceProperty> {this};
    }
}