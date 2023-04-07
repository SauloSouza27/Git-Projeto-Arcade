using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaUiPowerUp : MonoBehaviour
{
    public int quantidadePowerUPs;
    
    void Start()
    {
        
    }

    void Update()
    {
        AchaPowerUPButtons();
    }

    public void AchaPowerUPButtons()
    {
        var listaPowerUPButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        Debug.Log(listaPowerUPButtons + " : " + listaPowerUPButtons.Length);
    }
}
