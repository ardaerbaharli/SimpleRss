using SimpleRss;
using UnityEngine;
using UnityEngine.UI;

public class RssSource : MonoBehaviour
{
    [Header("Fields")] public string URL;
    public string Title;
    public string Host;

    [Header("UI Elements")] [SerializeField]
    private Text titleText;

    [SerializeField] private Image image;

    public float height;
    private RssSourcesList parent;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        height = rect.rect.height;
        parent = transform.parent.GetComponent<RssSourcesList>();
    }

    public void LoadFeed()
    {
        FindObjectOfType<Manager>().LoadFeed(this);
    }

    public void UpdateTitleUI()
    {
        titleText.text = Title;
    }


    public void UpdatePosition()
    {
        rect.anchoredPosition3D = GetPos();
    }

    private Vector3 GetPos()
    {
        var index = transform.GetSiblingIndex();
        if (index == 0)
            return new Vector3(0, parent.rssSourceStartPos, 0);
        var h = parent.rssSourceStartPos - height * index - 10 * index;
        return new Vector3(0, h, 0);
    }
}