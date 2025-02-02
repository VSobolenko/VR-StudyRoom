using UnityEngine;

[DefaultExecutionOrder(-10)]
public class PlatformDependency : MonoBehaviour
{
    public GameObject VRPlayer;
    public GameObject AndroidPlayer;
    public GameObject StandalonePplayer;

    public GameObject ActivePerson;
    public Canvas[] activeInfoCanvas;
    
    public static PlatformDependency Singleton;
    
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        
        SetupPlayer(AndroidPlayer, false);
        SetupPlayer(VRPlayer, false);
        SetupPlayer(StandalonePplayer, false);
        
#if UNITY_ANDROID
        SetupPlayer(VRPlayer, false);
        SetupPlayer(StandalonePplayer, false);
        ActivePerson = SetupPlayer(AndroidPlayer, true);
        if (ActivePerson.TryGetComponent<MainPerson>(out var activePerson))
        {
            SetupCanvasEventCamera(activePerson.activeCamera);
        }
#endif
        
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
        SetupPlayer(AndroidPlayer, false);
        SetupPlayer(VRPlayer, false);
        ActivePerson = SetupPlayer(StandalonePplayer, true);
#endif
        
#if UNITY_EDITOR
        SetupPlayer(AndroidPlayer, false);
        SetupPlayer(VRPlayer, false);
        ActivePerson = SetupPlayer(StandalonePplayer, true);
#endif
    }

    private GameObject SetupPlayer(GameObject player, bool isActive)
    {
        if (player == null)
        {
            return null;
        }
        
        player.gameObject.SetActive(isActive);
        if (player.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = isActive;
        }
        
        if (player.TryGetComponent<MainPerson>(out var person))
        {
            person.SetActiveObject(isActive);
        }
        
        return player;
    }

    [ContextMenu("Collect all canvas")]
    public void CollectAllCanvas()
    {
        activeInfoCanvas = GameObject.FindObjectsOfType<Canvas>();
    }
    
    public void SetupCanvasEventCamera(Camera eventCamera)
    {
        if (activeInfoCanvas == null)
        {
            return;
        }

        foreach (var activeCanvas in activeInfoCanvas)
        {
            activeCanvas.worldCamera = eventCamera;
        }
    }
}
