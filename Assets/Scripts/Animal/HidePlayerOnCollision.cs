using UnityEngine;
using System.Collections; // 필요 네임스페이스 추가

public class HidePlayerOnCollision : MonoBehaviour
{
    public bool bActive = true; // 활성 상태 추적
    public Material newMaterial; // 다른 매터리얼을 설정
    public Sig sigInstance; // Sig 클래스 인스턴스 참조
    public GameObject gunObject;
    public bool isfly;
    private Renderer objectRenderer;

    // 트리거 콜라이더에 다른 콜라이더가 닿을 때 호출되는 메소드
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered"); // 디버깅용 로그

        // 충돌한 객체가 "Ball" 태그를 가지고 있는지 확인
        if (other.CompareTag("Ball"))
        {
            // 오브젝트의 Renderer 컴포넌트를 가져옴
            objectRenderer = GetComponent<Renderer>();
            // 코루틴 시작
            StartCoroutine(HideAndMoveBack(other.gameObject));
        }
    }

    private IEnumerator HideAndMoveBack(GameObject obj)
    {
        //user input 0

        if (this.tag != "Ground")
        {
            // 오브젝트의 모습을 화면에서 안보이게 하기 위해 Renderer를 비활성화
            objectRenderer.enabled = false;
        }

        //user input 0 end

        // 객체의 렌더러 컴포넌트를 가져옴
        Renderer objRenderer = obj.GetComponent<Renderer>();

        // 원래 매터리얼 저장
        Material originalMaterial = objRenderer.material;

        // 다른 매터리얼로 변경
        objRenderer.material = newMaterial;
        Debug.Log("Material changed");

        // 객체의 Rigidbody2D 컴포넌트를 가져옴
        Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();

        // 객체를 비활성화하기 전에 Sig 클래스의 Reloadingcome 함수 호출
        if (sigInstance != null)
        {
            sigInstance.Reloadingcome(true);
        }

        // 객체를 비활성화
        obj.SetActive(false);
        Debug.Log("Ball deactivated");

        // 객체를 (0,0) 위치로 이동
        objRigidbody.gravityScale = 0f;
        if (gunObject != null)
        {
            // Get the position of the target object
            obj.transform.position = gunObject.transform.position;

        }
        //user input 1

        Animal animal = GetComponent<Animal>();
        switch (this.tag)
        {
            case "Sarg":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(0);
                }
                break;
            case "Bebsae":
                isfly = true;
                if (animal != null)
                {
                    animal.Ani(1);
                }
                break;
            case "Fox":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(2);
                }
                break;
            case "Dambe":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(3);
                }
                break;
            case "Quokka":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(4);
                }
                break;
            case "MangCong":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(5);
                }
                break;
            case "Namsang":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(6);
                }
                break;
            case "Bat":
                isfly = true;
                if (animal != null)
                {
                    animal.Ani(7);
                }
                break;
            case "Osori":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(8);
                }
                break;
            case "Goat":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(9);
                }
                break;
            case "Bigbard":
                isfly = true;
                if (animal != null)
                {
                    animal.Ani(10);
                }
                break;
            case "Therebird":
                isfly = true;
                if (animal != null)
                {
                    animal.Ani(11);
                }
                break;
            case "Pigeon":
                isfly = true;
                if (animal != null)
                {
                    animal.Ani(12);
                }
                break;
            case "Glassmonkey":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(13);
                }
                break;
            case "Coyote":
                isfly = false;
                if (animal != null)
                {
                    animal.Ani(14);
                }
                break;
            default:
                break;
        }

        //user input 1 end

        // 3초 대기
        yield return new WaitForSeconds(3);

        //user input 2

        Scription.instance.Turnoff(); // 동물 배너 숨기기

        //user input 2 end

        // 원래 매터리얼로 되돌림
        objRenderer.material = originalMaterial;
        Debug.Log("Material reverted");

        // 객체를 다시 활성화
        obj.SetActive(true);
        Debug.Log("Ball reactivated at (-2.5,)");

        // Rigidbody2D를 다시 활성화
        if (objRigidbody != null)
        {
            objRigidbody.simulated = true;
        }

        // Sig 클래스의 Reloadingcome 함수 호출
        if (sigInstance != null)
        {
            sigInstance.Reloadingcome(false);
        }

        bActive = true; // 활성 상태 업데이트

        if (this.tag != "Ground")
        {
            // 새 오브젝트 소환
            if (isfly == true)
            {
                Spawner.instance.Fly_Spawn();
            }
            else
            {
                Spawner.instance.Ground_Spawn();
            }
            isfly = false;

            // 이 게임 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
