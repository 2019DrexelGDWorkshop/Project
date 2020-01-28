using System.Collections;

using UnityEngine;

using Rewired;

public class PauseManager : Singleton<PauseManager>
{
    #region Variables

    [Header("Rewired")]
    [SerializeField] private int rewiredPlayerId = 0;
    private Player rewiredPlayer;

    [Header("Input Settings")]

    [SerializeField, Tooltip("The Rewired action name for toggling whether the game is paused")]
    string pauseActionName = "Toggle Pause";

    [Header("Pause Status")]

    [SerializeField, Tooltip("Whether the player can currently toggle the paused state (set to false in any scene that cannot be paused)")]
    private bool pausable = true;

    [SerializeField, Tooltip("Whether the game is currently paused")]
    private bool paused = false;

    // Whether pause was toggled this frame
    bool toggledPauseThisFrame;

    [SerializeField, Tooltip("Pause Menu UI Game Object")]
    GameObject pauseMenu;

    [SerializeField, Tooltip("Main Pause Menu")]
    GameObject pauseMain;

    [SerializeField, Tooltip("Settings Menu")]
    GameObject settings;

    private GameObject player;

    #endregion Variables

    #region Properties
    /// <summary>
    /// Whether the player can currently toggle the paused state (set to false in any scene that cannot be paused)
    /// </summary>
    /// <value></value>
    public bool Pausable
    {
        get { return pausable; }
        set
        {
            pausable = value;
            ToggledPauseThisFrame = true;
        }
    }/// <summary>
     /// Whether the game is currently paused
     /// </summary>
    public bool Paused
    {
        get { return paused || toggledPauseThisFrame; }
        set
        {
            if (pausable)
            {
                // Check that the value of paused is being changed
                // This is to prevent events like OnPause and OnResume from firing when the game is already in the state that one of those events indicates
                if (paused != value)
                {
                    paused = value;

                    ToggledPauseThisFrame = true;

                    // Check if the game is now paused but wasn't before
                    if (paused)
                    {
                        // Check for any listeners to the OnPause event
                        // If any are found, call OnPause
                        OnPause?.Invoke();
                    }
                    // The game has been resumed
                    else
                    {
                        // Check for any listeners to the OnResume event
                        // If any are found, call OnResume
                        OnResume?.Invoke();
                    }

                    StartCoroutine(WaitOneFrameAfterPause());
                }
            }
        }
    }

    /// <summary>
    /// Whether pause was toggled this frame
    /// </summary>
    public bool ToggledPauseThisFrame
    {
        get { return toggledPauseThisFrame; }
        set
        {
            if (toggledPauseThisFrame != value)
            {
                toggledPauseThisFrame = value;

                if (toggledPauseThisFrame)
                {
                    StartCoroutine(WaitOneFrameAfterPause());
                }
            }
        }
    }
    #endregion Properties

    #region Events
    /// <summary>
    /// Function that listens to when the game is paused
    /// </summary>
    public delegate void PauseEventHandler();

    /// <summary>
    /// Broadcasts that the game has been resumed
    /// </summary>
    public event PauseEventHandler OnPause;

    /// <summary>
    /// Function that listens to when the game is resumed
    /// </summary>
    public delegate void ResumeEventHandler();

    /// <summary>
    /// Broadcasts that the game has been resumed
    /// </summary>
    public event ResumeEventHandler OnResume;

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);
    }
    void Update()
    {
        if (rewiredPlayer.GetButtonDown(RewiredConsts.Action.Pause))
        {
            TryPause();
        }

        if (paused)
        {
            //Time.timeScale = 0;
            //(player.GetComponent("Player Input") as MonoBehaviour).enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            //player.GetComponent<PlayerInput>().enabled = true;
        }
    }


    #endregion MonoBehaviour

    #region Public Methods
    /// <summary>
    /// Toggle whether the game is paused
    /// </summary>
    public void TogglePause()
    {
        Paused = !paused;

        if(Paused){
            OnPause?.Invoke();
        }
        else{
            OnResume?.Invoke();
        }
    }

    #endregion Public Methods

    #region Private Methods
    /// <summary>
    /// Try pausing when receiving Rewired input
    /// </summary>
    /// <param name="_eventData">The Rewired input event data</param>
    private void TryPause()
    {
        if (!toggledPauseThisFrame)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Wait one frame after toggling pause to set toggledPauseThisFrame to false
    /// </summary>
    private IEnumerator WaitOneFrameAfterPause()
    {
        yield return null;

        toggledPauseThisFrame = false;
    }
    #endregion Private Methods
}