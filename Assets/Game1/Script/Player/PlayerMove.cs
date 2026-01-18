using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 조이스틱 입력을 받기 위한 변수
    public bl_Joystick js;

    // 기본 이동 속도
    public float speed;

    // 보스 상태 여부
    public bool boss;

    // 보스 상태에서 사용하는 속도 값
    [SerializeField]
    private float speed_3;

    public float Speed_3
    {
        get => speed_3;
        set => speed_3 = Mathf.Max(0, value);
    }

    private void Update()
    {
        // 조이스틱 방향 벡터 계산
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0f);
        dir.Normalize();

        // 보스 상태일 경우
        if (boss == true)
        {
            // 저장된 속도 값 불러오기
            float spd = PlayerPrefs.GetFloat("Speed");

            // 조이스틱 이동
            transform.position += dir * spd * Time.deltaTime;

            // 키보드 좌우 이동
            transform.Translate(
                Vector2.right * Input.GetAxisRaw("Horizontal") * spd * Time.deltaTime,
                Space.Self
            );

            // 키보드 상하 이동
            transform.Translate(
                Vector2.up * Input.GetAxisRaw("Vertical") * spd * Time.deltaTime,
                Space.Self
            );
        }

        // 일반 상태일 경우
        if (boss == false)
        {
            // 조이스틱 이동
            transform.position += dir * speed * Time.deltaTime;

            // 키보드 좌우 이동
            transform.Translate(
                Vector2.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime,
                Space.Self
            );

            // 키보드 상하 이동
            transform.Translate(
                Vector2.up * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime,
                Space.Self
            );
        }
    }
}