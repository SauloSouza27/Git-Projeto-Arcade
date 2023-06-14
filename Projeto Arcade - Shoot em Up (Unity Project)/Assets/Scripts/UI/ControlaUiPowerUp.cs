using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ControlaUiPowerUp : MonoBehaviour
{
    private List<Button> listaPowerUPs;
    public GameObject controladorGame, buttonCentro, buttonEsq, buttonDir;
    private GameObject[] buttonsParaDestruir;

    public void OnEnable()
    {
        listaPowerUPs = AchaPowerUPButtons();
        buttonsParaDestruir = CarregaPowerUPsAleatoriamente();

        ControladorGame.instancia.ControladorNiveisFases();
    }

    private void OnDisable()
    {
        listaPowerUPs.Clear();
        foreach(GameObject button in buttonsParaDestruir)
        {
            Destroy(button);
        }
    }

    public List<Button> AchaPowerUPButtons()
    {
        List<Button> buttonsCena = new List<Button>();

        foreach (Button go in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
        {
            if (go.CompareTag("PowerUPButton"))
            {
                buttonsCena.Add(go);
            }
            if (controladorGame.GetComponent<ControladorGame>().armaDoubleAtivada)
            {
                if (go.CompareTag("PowerUPArmaPrincipal"))
                {
                    buttonsCena.Add(go);
                }
            }
            if (controladorGame.GetComponent<ControladorGame>().HP < 5)
            {
                if (go.CompareTag("PowerUPVida"))
                {
                    buttonsCena.Add(go);
                }
            }
            if (controladorGame.GetComponent<ControladorGame>().armaOrbeGiratorioAtivada)
            {
                if (go.CompareTag("PowerUPOrbe1"))
                {
                    buttonsCena.Add(go);
                }
            }
            if (controladorGame.GetComponent<ControladorGame>().armaPetAtivada)
            {
                if (go.CompareTag("PowerUPPet1"))
                {
                    buttonsCena.Add(go);
                }
            }
            if (controladorGame.GetComponent<ControladorGame>().defesaEscudoAtivada)
            {
                if (go.CompareTag("PowerUPEscudo"))
                {
                    buttonsCena.Add(go);
                }
            }
            if (controladorGame.GetComponent<ControladorGame>().armaSerraAtivada)
            {
                if (go.CompareTag("PowerUPSerra"))
                {
                    buttonsCena.Add(go);
                }
            }
        }
        return buttonsCena;
    }

    public GameObject[] CarregaPowerUPsAleatoriamente()
    {
        int index;
        Button itemLista;

        index = Random.Range(0, listaPowerUPs.Count);
        itemLista = listaPowerUPs[index];
        GameObject esq = Instantiate(itemLista.gameObject, buttonEsq.transform.position, buttonEsq.transform.rotation, this.transform);
        esq.SetActive(true);
        listaPowerUPs.Remove(itemLista);

        index = Random.Range(0, listaPowerUPs.Count);
        itemLista = listaPowerUPs[index];
        GameObject centro = Instantiate(itemLista.gameObject, buttonCentro.transform.position, buttonCentro.transform.rotation, this.transform);
        centro.SetActive(true);
        listaPowerUPs.Remove(itemLista);

        index = Random.Range(0, listaPowerUPs.Count);
        itemLista = listaPowerUPs[index];
        GameObject dir = Instantiate(itemLista.gameObject, buttonDir.transform.position, buttonDir.transform.rotation, this.transform);
        dir.SetActive(true);
        listaPowerUPs.Remove(itemLista);

        GameObject[] buttonsAtivos = new GameObject[] {esq, centro, dir};
        return buttonsAtivos;
    }
}
