using UnityEngine;
using System.Collections; // �ʿ� ���ӽ����̽� �߰�

public class HidePlayerOnCollision : MonoBehaviour
{
    public bool bActive = true; // Ȱ�� ���� ����
    public Material newMaterial; // �ٸ� ���͸����� ����
    public Sig sigInstance; // Sig Ŭ���� �ν��Ͻ� ����
    public GameObject gunObject;
    public bool isfly;
    private Renderer objectRenderer;

    // Ʈ���� �ݶ��̴��� �ٸ� �ݶ��̴��� ���� �� ȣ��Ǵ� �޼ҵ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered"); // ������ �α�

        // �浹�� ��ü�� "Ball" �±׸� ������ �ִ��� Ȯ��
        if (other.CompareTag("Ball"))
        {
            // ������Ʈ�� Renderer ������Ʈ�� ������
            objectRenderer = GetComponent<Renderer>();
            // �ڷ�ƾ ����
            StartCoroutine(HideAndMoveBack(other.gameObject));
        }
    }

    private IEnumerator HideAndMoveBack(GameObject obj)
    {
        //user input 0

        if (this.tag != "Ground")
        { 
            // ������Ʈ�� ����� ȭ�鿡�� �Ⱥ��̰� �ϱ� ���� Renderer�� ��Ȱ��ȭ
            objectRenderer.enabled = false;
        }

        //user input 0 end

        // ��ü�� ������ ������Ʈ�� ������
        Renderer objRenderer = obj.GetComponent<Renderer>();

        // ���� ���͸��� ����
        Material originalMaterial = objRenderer.material;

        // �ٸ� ���͸���� ����
        objRenderer.material = newMaterial;
        Debug.Log("Material changed");

        // ��ü�� Rigidbody2D ������Ʈ�� ������
        Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();

        // ��ü�� ��Ȱ��ȭ�ϱ� ���� Sig Ŭ������ Reloadingcome �Լ� ȣ��
        if (sigInstance != null)
        {
            sigInstance.Reloadingcome(true);
        }

        // ��ü�� ��Ȱ��ȭ
        obj.SetActive(false);
        Debug.Log("Ball deactivated");

        // ��ü�� (0,0) ��ġ�� �̵�
        objRigidbody.gravityScale = 0f;
        if (gunObject != null)
        {
            // Get the position of the target object
            obj.transform.position = gunObject.transform.position;

        }
        //user input 1

        Animal animal = GetComponent<Animal>();
        if (this.tag == "Dambe")
        {
            isfly = false;
            if (animal != null)
            {
                animal.Dam();
            }
        }
        else if (this.tag == "Bebsae")
        {
            isfly = true;
            if (animal != null)
            {
                animal.Beb();
            }
        }

        if (this.tag != "Ground")
        {
            Scription.instance.Turnon();

            // �ı��ϱ� ���� �θ� ���� ����
            transform.SetParent(null);
        }

        //user input 1 end

        // 3�� ���
        yield return new WaitForSeconds(3);

        //user input 2

        Scription.instance.Turnoff();

        //user input 2 end

        // ���� ���͸���� �ǵ���
        objRenderer.material = originalMaterial;
        Debug.Log("Material reverted");

        // ��ü�� �ٽ� Ȱ��ȭ
        obj.SetActive(true);
        Debug.Log("Ball reactivated at (-2.5,)");

        // Rigidbody2D�� �ٽ� Ȱ��ȭ
        if (objRigidbody != null)
        {
            objRigidbody.simulated = true;
        }

        // Sig Ŭ������ Reloadingcome �Լ� ȣ��
        if (sigInstance != null)
        {
            sigInstance.Reloadingcome(false);
        }

        bActive = true; // Ȱ�� ���� ������Ʈ

        if (this.tag != "Ground")
        {
            // �� ������Ʈ ��ȯ
            if (isfly == true)
            {
                Spawner.instance.Fly_Spawn();
            }
            else
            {
                Spawner.instance.Ground_Spawn();
            }
            isfly = false;

            // �� ���� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
