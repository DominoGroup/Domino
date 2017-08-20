using UnityEngine.SceneManagement;
public class GameFlowManager : MonoSingleton<GameFlowManager>
{
    public GameFlow gameFlow { get; private set; }
#if UNITY_EDITOR
    public bool useBundleInEditor;
#endif
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        gameFlow = GameFlow.init;
#if UNITY_EDITOR
        if (!useBundleInEditor)
            gameObject.AddComponent<AssetHubEditor>();
        else
#endif
            gameObject.AddComponent<AssetHubRelease>();

        gameObject.AddComponent<GameDataHub>();
    }
    private void Start()
    {
        SceneManager.LoadScene("BasicScene");
    }
}
public enum GameFlow
{
    init,
    gameplay
}