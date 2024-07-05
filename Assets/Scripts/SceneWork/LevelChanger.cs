using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public int levelToLoad;
    [SerializeField] GameObject jelly;

    public Vector3 position;
    public VectoreValue playerStorage;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void FadeTolevel()
    {
        anim.SetTrigger("Fade");
      
    }

    public void OnFadeComplete()
    {
        playerStorage.lastPlayerPosition = jelly.transform.position;
        SceneManager.LoadScene(levelToLoad);
    }
}
