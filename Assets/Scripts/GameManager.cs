using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] clientArr;
    [SerializeField]
    PacketMoverManager packetMoverManager;
    public static GameManager Instance;
    int ranIn;
    ArrayList ranArrList;
    int startClient = 3;
    float time = 1;
    int count = 1;
    float attackSpeed = 0.3f, nextAttackTime = 3f;
    int antivirusCount = 0, gpCount = 0, vtCount = 0, serverHealthCount = 20;
    bool onlyOnce;
    int antivrusPower = -1;

    [Space]
    [SerializeField] TextMeshProUGUI gpText;
    [SerializeField] TextMeshProUGUI vtText;
    [SerializeField] Slider shSlider, abSlider;


    [Space]
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject sheildimgObj;
    [SerializeField] TextMeshProUGUI gpGOPText, vtGOPText;
    void Awake()
    {
        Instance = this;
        time = 1;
        count = startClient;
        TurnOnClient();
        Invoke("CallNow", 2f);
    }

    void CallNow()
    {
        StartCoroutine(StartPackage());
    }

    void TurnOnClient()
    {
        for (int i = 0; i < clientArr.Length; i++)
        {
            clientArr[i].SetActive(false);
        }

        for (int i = 0; i < startClient; i++)
        {
            clientArr[i].SetActive(true);
        }
    }

    private void Update()
    {
        time += Time.deltaTime * 0.1f;

        if (2 == (int)time)
        {
            ++count;
            switch (count)
            {
                case 4:
                    if (vtCount >= 10)
                    {
                        ++startClient;
                        TurnOnClient();
                        Debug.Log('4');
                    }
                    else
                    {
                        --count;
                    }
                    break;
                case 5:
                    if (vtCount >= 15)
                    {
                        ++startClient;
                        TurnOnClient();
                        attackSpeed = 0.25f; nextAttackTime = 2f;
                        packetMoverManager.SetPackageSpeed(1.7f);
                        Debug.Log('5');
                    }
                    else
                    {
                        --count;
                    }
                    break;
                case 6:
                    if (vtCount >= 20)
                    {
                        ++startClient;
                        TurnOnClient();
                        attackSpeed = 0.2f; nextAttackTime = 1f;
                        packetMoverManager.SetPackageSpeed(1.5f);
                        Debug.Log('6');
                    }
                    else
                    {
                        --count;
                    }
                    break;
                case 7:
                    if (vtCount >= 25)
                    {
                        ++startClient;
                        TurnOnClient();
                        attackSpeed = 0.15f; nextAttackTime = 0.5f;
                        packetMoverManager.SetPackageSpeed(1f);
                        Debug.Log('7');
                    }
                    else
                    {
                        --count;
                    }
                    break;
            }
            time = 1f;
        }
    }

    IEnumerator StartPackage()
    {
        while (true)
        {
            ranArrList = new ArrayList();
            ranIn = Random.Range(0, startClient);
            ranArrList.Add(ranIn);
            packetMoverManager.CreatePackage(clientArr[ranIn]);
            yield return new WaitForSeconds(attackSpeed);
            do
            {
                ranIn = Random.Range(0, startClient);
            } while (ranArrList.IndexOf(ranIn) != -1);
            ranArrList.Add(ranIn);
            packetMoverManager.CreatePackage(clientArr[ranIn]);
            yield return new WaitForSeconds(attackSpeed);
            do
            {
                ranIn = Random.Range(0, startClient);
            } while (ranArrList.IndexOf(ranIn) != -1);
            ranArrList.Add(ranIn);
            packetMoverManager.CreatePackage(clientArr[ranIn]);
            yield return new WaitForSeconds(nextAttackTime);
        }
    }

    public void StopClientWire(Transform clientWireObj)
    {
        ClientWire clientWire = clientWireObj.GetComponent<ClientWire>();
        Color clientWireColor = clientWire.GetColor();
        TextStatus(clientWireColor);
        clientWire.StopThisClientWire();
    }

    public void IncAntivirues()
    {
        gpText.text = "GP:" + (++gpCount);
        abSlider.value = ++antivirusCount;
        if (abSlider.value == 20 && antivrusPower == -1)
        {
            antivrusPower = 5;
            sheildimgObj.SetActive(true);
        }
    }

    public void DecServerHealth()
    {
        if (antivrusPower > 0)
        {
            --antivrusPower;
            if (antivrusPower == 0)
            {
                abSlider.value = 0;
                antivirusCount = 0;
                antivrusPower = -1;
                sheildimgObj.SetActive(false);
            }
        }
        else
        {
            serverHealthCount -= 5;
            shSlider.value = serverHealthCount;
            if (serverHealthCount <= 0 && !onlyOnce)
            {
                onlyOnce = true;
                gpGOPText.text = "GP:" + gpCount;
                vtGOPText.text = "VT:" + vtCount;
                gameoverPanel.SetActive(true);
            }
        }
    }

    public void TextStatus(Color color_)
    {
        if (color_ == Color.red)
            vtText.text = "VT:" + (++vtCount);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
