using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleRss
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private RSSManager rssManager;
        public static Manager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void LoadFeed(RssSource rssSource)
        {
            SceneManager.LoadScene("Feed");
            StartCoroutine(FindRssManager(rssSource));
        }

        private IEnumerator FindRssManager(RssSource rssSource)
        {
            while (rssManager == null)
            {
                rssManager = FindObjectOfType<RSSManager>();
                yield return new WaitForEndOfFrame();
            }

            rssManager.SetActiveFeed(rssSource);
            rssManager.LoadFeed();
        }
    }
}