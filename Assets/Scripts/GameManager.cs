using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        EventsManager.Instance.SubscribeTo(EventsManager.EventType.TARGET_DEATH, OnPlayerDeath);
        EventsManager.Instance.SubscribeTo(EventsManager.EventType.NEXT_LEVEL, OnNextLevel);
    }

    private void OnPlayerDeath(object sender, BaseEvent e)
    {
        SceneManager.LoadScene("GameOver");
    }

    private void OnNextLevel(object sender, BaseEvent e) {
        var realEvent = (NextLevelEvent)e;
        SceneManager.LoadScene(realEvent.nextLevel);
    }
}
