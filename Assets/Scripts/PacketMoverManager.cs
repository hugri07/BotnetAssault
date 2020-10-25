using UnityEngine;

public class PacketMoverManager : MonoBehaviour
{
    //[SerializeField]
    //GameObject[] clientArr;
    [SerializeField]
    GameObject packagePrefab;
    GameObject packageObj;
    Package package;
    [SerializeField] Transform serverPos;
    public float packageSpeed = 2f;

    void Start()
    {
        //InvokeRepeating("CreatePackage", 2f, 2f);
    }

    public void SetPackageSpeed(float packageSpeed)
    {
        this.packageSpeed = packageSpeed;
    }

    public void CreatePackage(GameObject ranClientObj)
    {
        bool isGreen = (Random.value < 0.6f);
        ClientWire clientWire = ranClientObj.GetComponent<ClientWire>();
        ++clientWire.usedBy;
        packageObj = Instantiate(packagePrefab, ranClientObj.transform.position, Quaternion.identity);
        package = packageObj.GetComponent<Package>();
        package.SetPackageSpeed(packageSpeed);
        package.SetClientWire(clientWire);
        clientWire.SetPackage(package);
        if (isGreen)
        {
            //Debug.Log("Green Green");
            package.SetSRColor(Color.green);
            clientWire.SetColor(2);
        }
        else
        {
            //Debug.Log("red red");
            package.SetSRColor(Color.red);
            clientWire.SetColor(1);
        }
        package.DoMove(serverPos.position);
    }


}
