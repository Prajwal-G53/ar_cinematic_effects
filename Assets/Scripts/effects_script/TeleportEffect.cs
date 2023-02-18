using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeleportEffect : MonoBehaviour
{
    public Image teleportImage;
   

    enum View { Top, Bottom };

    // function called in all gameobject behaviour scripts and stepcontroller.cs
    public void ShowTeleportEffect(float fadeTime = 1.5f, float waitToLoad = 0f)
    {
        StartCoroutine(TeleportEffectRoutine(fadeTime,waitToLoad));
    }

    IEnumerator TeleportEffectRoutine(float fadeTime, float waitToLoad)
    {

        yield return new WaitForSeconds(waitToLoad);

        teleportImage.gameObject.SetActive(true);
        teleportImage.color = Color.clear;
        while (teleportImage.color.a < 0.95f)
        {
            teleportImage.color = Color.Lerp(teleportImage.color, Color.black, fadeTime * Time.deltaTime);
            yield return null;
        }

        teleportImage.color = Color.black;

        yield return new WaitForSeconds(0.8f);//0.6

        PerformSpecialOperationWhileTeleporting();
        

        while (teleportImage.color.a > 0.1f)
        {
            teleportImage.color = Color.Lerp(teleportImage.color, Color.clear, fadeTime * Time.deltaTime);
            yield return null;
        }
        teleportImage.color = Color.clear;
        teleportImage.gameObject.SetActive(false);

        //MainCameraController.CameraInstance.CameraPosAfterTeleport();
    }

    void PerformSpecialOperationWhileTeleporting()
    {
       // While coming into this function since it is called in a couroutine, the current step index is incremented
       //so we take the next count
      
    }

}
