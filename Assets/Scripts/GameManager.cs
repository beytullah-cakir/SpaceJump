using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public List<Transform> platforms;
    public bool isFirst;
    private Camera _cam;
    public static GameManager Instance;
    public Transform bg=null;

    private void Awake()=>Instance = this;

    void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
     bg.position=_cam.transform.position;   
    }

    public void RandomPos()
    {

        float randomX = Random.Range(2f, 3f);
        int currentIndex = isFirst ? 1 : 0;
        int nextIndex = isFirst ? 0 : 1;

        platforms[currentIndex].position = new(platforms[nextIndex].position.x + randomX, platforms[nextIndex].position.y);
        isFirst = !isFirst;
       


    }

    public void CameraPos()
    {
        float camX = (platforms[0].position.x + platforms[1].position.x) / 2;
        Vector3 newPos = new(camX, -2,-1);
        _cam.transform.position = Vector3.MoveTowards(_cam.transform.position, newPos, 10f * Time.deltaTime);
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    


}
