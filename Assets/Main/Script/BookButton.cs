using UnityEngine;
using UnityEngine.UI;

public class BookButton : MonoBehaviour
{
    // 병합 데이터 관리 스크립트
    public Merge mg;

    // 활성화 표시용 이미지
    public GameObject image;

    // 책 번호
    public int booknum;


    private void Awake()
    {
        image.SetActive(false);
    }

    private void Update()
    {
        // 버튼 컴포넌트 가져오기
        Button button = GetComponent<Button>();

        // 버튼 색상 설정
        ColorBlock colors = button.colors;
        Color transparentColor = new Color(0f, 0f, 0f, 0f);

        colors.normalColor = transparentColor;
        colors.selectedColor = transparentColor;
        colors.disabledColor = transparentColor;
        colors.pressedColor = transparentColor;

        // 해당 아이템이 해금된 경우 처리
        if (GameObject.Find("ItemData").GetComponent<Merge>().itemdata[booknum].spawncheck == true)
        {
            button.colors = colors;

            // 이미지 표시
            image.SetActive(true);
        }
    }

    public void but_event()
    {
        // 아이템이 해금된 경우에만 패널 생성
        if (GameObject.Find("ItemData").GetComponent<Merge>().itemdata[booknum].spawncheck == true)
        {
            Instantiate(
                mg.itemdata[booknum].panel,
                Vector3.zero,
                Quaternion.identity,
                GameObject.Find("Canvas").transform
            );
        }
    }
}