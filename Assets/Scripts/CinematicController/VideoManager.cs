using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public void SkipCutscene()
    {
        SceneManager.LoadScene("Game_Scene");
    }
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Agregar un listener al evento loopPointReached
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        // Cambiar de escena
        SceneManager.LoadScene("Game_Scene");
    }
}
