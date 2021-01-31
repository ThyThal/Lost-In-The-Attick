using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlavorFullOldMan : MonoBehaviour
{

    [SerializeField] private AudioClip[] fatherDialogueClips;
    [SerializeField] private AudioClip[] childDialogueClips;   

    [SerializeField] private AudioSource fatherAudio;
    [SerializeField] private AudioSource childAudio;

    [SerializeField] private Image itemImage;

    [SerializeField] private Sprite golfClub;
    [SerializeField] private Sprite michiBag;
    [SerializeField] private Sprite mermidor;
    [SerializeField] private Sprite screwdriver;
   
  



    private void Start()
    {
        
    }

    public void CheckQuestItemNumberAndSprite (int number)
    {
        switch (number)
        {
            case 1:
                itemImage.sprite = golfClub;
                break;
            case 2:
                itemImage.sprite = michiBag;
                break;
            case 3:
                itemImage.sprite = mermidor;
                break;
            case 4:
                itemImage.sprite = screwdriver;
                break;

            default:
                break;
        }
    }
    public void TriggerDialogue()
    {

        if (!CanPlayAgain())
        {
            return;
        }

        int random1 = Random.Range(0, fatherDialogueClips.Length - 1);
        int random2 = Random.Range(0, childDialogueClips.Length - 1);

        fatherAudio.clip = fatherDialogueClips[random1];
        childAudio.clip = childDialogueClips[random2];        

        StartCoroutine(Dialogue());
    }


    public bool CanPlayAgain()
    {
        return !childAudio.isPlaying && !fatherAudio.isPlaying;
    }


    private IEnumerator Dialogue()
    {
        fatherAudio.Play();

        yield return new WaitForSeconds(1.5f);

        childAudio.Play();
    }






}
