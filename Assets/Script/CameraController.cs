using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 1000f; // 마우스 감도 조절을 위한 변수
    public Transform playerBody; // 플레이어의 Transform을 참조하기 위한 변수
    float xRotation = 0f; // 카메라의 X축 회전값을 저장하는 변수

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 게임 시작 시 마우스 커서 고정
    }

    void Update()
    {   
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 카메라 회전 구현
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 카메라의 회전 각도 제한 (-90도 ~ 90도)
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 카메라의 X축 회전 적용
        playerBody.Rotate(Vector3.up * mouseX); // 플레이어의 바디를 마우스 X축 입력에 따라 회전
    }
}
