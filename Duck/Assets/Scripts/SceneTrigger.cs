using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTrigger : MonoBehaviour
{

    public GameObject textToActivate;
    public GameObject loadingScreen;
    public Slider slider;
    public bool canCall = false;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        textToActivate.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E) && !canCall)
        {
            canCall = true;
        }
        if (canCall)
        {
            StartCoroutine(LoadAsync());
        }
    }


    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textToActivate.SetActive(false);
    }
}
