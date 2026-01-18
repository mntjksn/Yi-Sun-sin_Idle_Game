using UnityEngine;

namespace other
{
    public class Bullet : MonoBehaviour
    {
        // 탄환 이동 속도
        public float Speed = 10f;

        private void Update()
        {
            // 현재 회전 방향 기준으로 앞으로 이동
            transform.Translate(Vector2.right * Speed * Time.deltaTime, Space.Self);
        }
    }
}