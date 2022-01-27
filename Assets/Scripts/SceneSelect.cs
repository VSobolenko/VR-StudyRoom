using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    public int electricScene = 2;
    public int mainScene;
    public int physicsScene = 1;

    public void LoadMainScene()
    {
        SceneManager.LoadSceneAsync(mainScene);
    }

    public void LoadPhysicsScene()
    {
        SceneManager.LoadSceneAsync(physicsScene);
    }

    public void LoadElectricScene()
    {
        SceneManager.LoadSceneAsync(electricScene);
    }
}