using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sky_animal1 : MonoBehaviour
{
    public Collider2D Area;  // Area 범위를 확인하기 위한 Collider2D

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 1.2f;  // 초기 y 위치 설정
        transform.position = newPosition;

        // "Area"라는 이름의 오브젝트를 찾아 Collider2D를 할당
        GameObject areaObject = GameObject.Find("Area");
        if (areaObject != null)
        {
            Area = areaObject.GetComponent<Collider2D>();
            if (Area == null)
            {
                Debug.LogWarning("Area 오브젝트에 Collider2D 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("이름이 'Area'인 오브젝트를 찾을 수 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Time.deltaTime * 4f;
        transform.Translate(-Vector2.right * speed);  // 오브젝트를 왼쪽으로 이동

        // Area 범위 밖으로 벗어나는지 확인
        if (Area == null)
        {
            return;
        }

        if (!Area.bounds.Contains(transform.position))
        {
            Vector3 newPosition = transform.position;

            // x축 위치를 반대쪽으로 이동
            if (transform.position.x > Area.bounds.max.x)
            {
                newPosition.x = Area.bounds.min.x;
            }
            else if (transform.position.x < Area.bounds.min.x)
            {
                newPosition.x = Area.bounds.max.x;
            }

            // 새 위치로 설정
            transform.position = newPosition;
        }
    }
}
