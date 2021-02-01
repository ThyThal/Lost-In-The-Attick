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

    [SerializeField] private GameObject dialogueGameObject;

    [SerializeField] private Image itemImage;
    [SerializeField] private Text eltepsto;

    [SerializeField] private Sprite golfClub;
    [SerializeField] private Sprite michiBag;
    [SerializeField] private Sprite mermidor;
    [SerializeField] private Sprite screwdriver;

    [SerializeField] private string item1Dialogue;
    [SerializeField] private string item2Dialogue;
    [SerializeField] private string item3Dialogue;
    [SerializeField] private string item4Dialogue;

    [SerializeField] private Animator animator;



    private void Start()
    {
        
    }

    public void CheckQuestItemNumberAndSprite (int number)
    {
        switch (number)
        {
            case 1:
                itemImage.sprite = golfClub;
                eltepsto.text = item1Dialogue;
                break;
            case 2:
                itemImage.sprite = michiBag;
                eltepsto.text = item2Dialogue;
                break;
            case 3:
                itemImage.sprite = mermidor;
                eltepsto.text = item3Dialogue;
                break;
            case 4:
                itemImage.sprite = screwdriver;
                eltepsto.text = item4Dialogue;
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

        yield return new WaitForSeconds(2);

        animator.SetTrigger("Disable");

    }

    public void Deactivate()
    {
        dialogueGameObject.SetActive(false);
    }
}
