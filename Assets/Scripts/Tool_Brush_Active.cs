using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_Brush_Active : MonoBehaviour
{
    [SerializeField]ParticleSystem CleanerParticle;
    float random = .6f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CoreFunct_Abs.TeethModTag))
        {
            CleanerParticle.Play();
            Color Col = other.GetComponent<MeshRenderer>().material.color;
            if (Random.value > random)
            {
                if (Col.a >= 0.1f)
                {
                    Col.a -= .2f;
                    other.GetComponent<MeshRenderer>().material.color = Col;
                    GameManager_Active.PlayerCurrentScore += Random.Range(20, 40);
                    GameManager_Active.UpdateUIAction?.Invoke();
                }
            }
            if (Col.a < 0.1f)
            {
                GameManager_Active.ModdedTeethCount -= 1;
                GameManager_Active.PlayerCurrentScore += Random.Range(70, 120);
                other.GetComponent<BoxCollider>().enabled = false;
                other.transform.GetChild(1).gameObject.SetActive(false);
                if (GameManager_Active.ModdedTeethCount <= 0)
                {
                    CoreFunct_Abs.ChangeGameState(GameState.EMPTY);
                    GameManager_Active.EndgameActions?.Invoke();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CoreFunct_Abs.TeethModTag))
        {
            CleanerParticle.Play();
            Color Col = other.GetComponent<MeshRenderer>().material.color;
            if(Random.value > random)
            {
                if (Col.a >= 0.1f)
                {
                    Col.a -= .2f;
                    other.GetComponent<MeshRenderer>().material.color = Col;
                    GameManager_Active.PlayerCurrentScore += Random.Range(20, 40);
                    GameManager_Active.UpdateUIAction?.Invoke();
                }
            }
            if (Col.a < 0.1f)
            {
                GameManager_Active.ModdedTeethCount -= 1;
                GameManager_Active.PlayerCurrentScore += Random.Range(70, 120);
                other.GetComponent<BoxCollider>().enabled = false;
                other.transform.GetChild(1).gameObject.SetActive(false);
                if (GameManager_Active.ModdedTeethCount <= 0)
                {
                    CoreFunct_Abs.ChangeGameState(GameState.EMPTY);
                    GameManager_Active.EndgameActions?.Invoke();
                }
            }
        }
    }




}
