using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLock : MonoBehaviour
{
    [SerializeField] private CamCollider camCollider;

    [SerializeField] private GameObject enemyStart;
    
    [SerializeField] private TutorialSystem tutorialSystem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        if (tutorialSystem == null)
            tutorialSystem = FindObjectOfType<TutorialSystem>();

        if (tutorialSystem != null && tutorialSystem.EndTutorial)
        {
            enemyStart.SetActive(true);
            camCollider.NextLevel = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "CamLock")
        {
            camCollider.NextLevel = true;
        }
    }
}
