using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        EventsManager.Instance.SubscribeTo(EventsManager.EventType.TARGET_DEATH, OnPlayerDeath);
    }

    private void OnPlayerDeath(object sender, BaseEvent e)
    {
        SceneManager.LoadScene("GameOver");
    }
}
