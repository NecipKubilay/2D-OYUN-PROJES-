using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class lvlButton : MonoBehaviour
{
    public int levelId;
    public TMP_Text levelText;
    public Image lockedImage;
    public Button button;


    // Start is called before the first frame update
    void Start()
    {
        levelText.text = levelId.ToString();
        if (levelManagerKod.Instance.isLevelLocked(levelId))
        {
            //lockedImage.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
            button.interactable = false;
        }
        else
        {
            //lockedImage.gameObject.SetActive(false);
            levelText.gameObject.SetActive(true);
            button.interactable = true;
        }
    }

    

    public void OpenLevel()
    {
        SceneManager.LoadScene("Level-" + levelId.ToString());
    }
}
