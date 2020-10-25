using UnityEngine;
using DG.Tweening;

public class Package : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    ClientWire clientWire;
    float packageSpeed = 2f;
    Tweener tweener;
    public bool test;

    public void SetSRColor(Color color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    public void DoMove(Vector3 to)
    {
        tweener = transform.DOMove(to, packageSpeed).OnComplete(() =>
         {
             Color clientWireColor = clientWire.GetColor();
             --clientWire.usedBy;

             if (clientWireColor == Color.green)
             {
                 GameManager.Instance.IncAntivirues();
             }
             else
             {
                 GameManager.Instance.DecServerHealth();
             }
             if (clientWire.usedBy == 0)
                 clientWire.SetColor(2);
             Destroy(gameObject);
             // Debug.Log("Compltedddd");
         });
    }

    public void SetPackageSpeed(float packageSpeed)
    {
        this.packageSpeed = packageSpeed;
    }

    public void SetClientWire(ClientWire clientWire)
    {
        this.clientWire = clientWire;
    }

    public void KillPackage()
    {
        test = false;
        tweener.Kill();
        spriteRenderer.color = Color.white;
        Invoke("DestroyThis", 1f);
    }

    void DestroyThis()
    {
        clientWire.SetColor(2);
        Destroy(gameObject);
    }

}
