using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Transform> platforms;
    public bool isFirst;
    private Camera _cam;
    public static GameManager Instance;

    private void Awake() => Instance = this;

    private void Start() => _cam = Camera.main;

    public void RandomPos()
    {
        int currentIndex = isFirst ? 1 : 0;
        int nextIndex = 1 - currentIndex;

        platforms[currentIndex].position = new Vector2(
            platforms[nextIndex].position.x + 2.5f,
            platforms[nextIndex].position.y + Random.Range(0f, 1f)
        );
        isFirst = !isFirst;
    }

    public void CameraPos()
    {
        Vector3 newPos = new Vector3(
            (platforms[0].position.x + platforms[1].position.x) * 0.5f,
            (platforms[0].position.y + platforms[1].position.y) * 0.5f + 5f,
            -1f
        );
        _cam.transform.position = Vector3.Lerp(_cam.transform.position, newPos, Time.deltaTime * 10f);
    }

    public void RestartGame() => LoadScene(SceneManager.GetActiveScene().name);

    public void ReturnMenu() => LoadScene("MainMenu");

    private void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusic("MainTheme");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() => Application.Quit();
}