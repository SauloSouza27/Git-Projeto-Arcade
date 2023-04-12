using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ControlaUiPowerUp : MonoBehaviour
{
    List<int> ordemLista = new List<int>();
    public int quantidadePowerUps;
    List<Button> listaPowerUPs;
    public GameObject controladorGame, buttonCentro, buttonEsq, buttonDir;
    private GameObject[] buttonsParaDestruir;

    public void OnEnable()
    {
        listaPowerUPs = AchaPowerUPButtons();
        Debug.Log(listaPowerUPs.Count);
        quantidadePowerUps = listaPowerUPs.Count;
        for (int n = 0; n < quantidadePowerUps; n++)
        {
            ordemLista.Add(n);
            Debug.Log(n);
        }
        buttonsParaDestruir = CarregaPowerUPsAleatoriamente();
    }

    private void OnDisable()
    {
        listaPowerUPs.Clear();
        ordemLista.Clear();
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
                Debug.Log(go.name);
            }
        }
        return buttonsCena;
    }

    public GameObject[] CarregaPowerUPsAleatoriamente()
    {
        int index;
        Button itemLista;

        index = Random.Range(1, ordemLista.Count);
        int posicaoLista = ordemLista[index];
        ordemLista.RemoveAt(index);
        itemLista = listaPowerUPs[posicaoLista];
        GameObject esq = Instantiate(itemLista.gameObject, buttonEsq.transform.position, buttonEsq.transform.rotation, this.transform);
        esq.SetActive(true);

        index = Random.Range(1, ordemLista.Count);
        posicaoLista = ordemLista[index];
        ordemLista.RemoveAt(index);
        itemLista = listaPowerUPs[posicaoLista];
        GameObject centro = Instantiate(itemLista.gameObject, buttonCentro.transform.position, buttonCentro.transform.rotation, this.transform);
        centro.SetActive(true);

        index = Random.Range(1, ordemLista.Count);
        posicaoLista = ordemLista[index];
        ordemLista.RemoveAt(index);
        itemLista = listaPowerUPs[posicaoLista];
        GameObject dir = Instantiate(itemLista.gameObject, buttonDir.transform.position, buttonDir.transform.rotation, this.transform);
        dir.SetActive(true);

        GameObject[] buttonsAtivos = new GameObject[] {esq, centro, dir};
        return buttonsAtivos;
    }
}
