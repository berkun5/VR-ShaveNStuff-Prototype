using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public ExampleImage characterReferences;
    public Action<GameState> onGameStateChanged = delegate { };
    private GameState _gs;
    public GameState gameState
    {
        get { return _gs; }
        set
        {
            _gs = value;
            onGameStateChanged(value);
        }
    }

    public override void Awake()
    {
        base.Awake();
    }
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SetGameState(GameState.started);
    }
    private void OnEnable()
    {
        Manager.gameManager = this;
        //Application.targetFrameRate = 60;
    }
    public void SetGameState(GameState gs)
    {
        Manager.gameManager.gameState = gs;
    }

    public void RestartGameIn(int time)
    {
        StartCoroutine(Restart(time));
        //showcountdown
    }
    private IEnumerator Restart(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        FindObjectOfType<Curtain>().ShowCurtain(true);
        yield return new WaitForSecondsRealtime(1.5f);
        AsyncOperation ac = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        while (!ac.isDone) { yield return null; } //show progress
        yield return new WaitForEndOfFrame();
        SetGameState(GameState.started);


    }
}

public enum GameState
{
    started,
    paused,
    ended
}
