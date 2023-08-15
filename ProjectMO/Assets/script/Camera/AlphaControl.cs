using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControl : MonoBehaviour
{
    public float maxDistance = 10f; // 레이의 최대 거리
    public float alphaValue = 0.5f; // 오브젝트의 투명도를 조절하는 값 (0: 완전 투명, 1: 불투명)
    public LayerMask layerMask; // 레이캐스트에서 충돌을 검사할 레이어 마스크

    private Material originalMaterial; // 오브젝트의 원래 머티리얼
    private Material transparentMaterial; // 투명 머티리얼

    private void Start()
    {
        // 오브젝트의 원래 머티리얼을 복사하여 투명 머티리얼을 생성합니다.
        originalMaterial = GetComponent<Renderer>().material;
        transparentMaterial = new Material(originalMaterial);
        // 투명 머티리얼 설정
        transparentMaterial.SetFloat("_Mode", 2);
        transparentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        transparentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        transparentMaterial.SetInt("_ZWrite", 0);
        transparentMaterial.DisableKeyword("_ALPHATEST_ON");
        transparentMaterial.EnableKeyword("_ALPHABLEND_ON");
        transparentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        transparentMaterial.renderQueue = 3000;

        // 오브젝트의 머티리얼을 투명 머티리얼로 바꿉니다.
        GetComponent<Renderer>().material = transparentMaterial;
    }

    private void Update()
    {
        HandleRaycasting();
    }

    private void HandleRaycasting()
    {
        // 카메라에서 레이를 쏘고, 오브젝트와의 충돌 여부를 확인합니다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            // 레이가 오브젝트에 충돌하면 오브젝트의 투명도를 조절합니다.
            if (hit.collider.gameObject == gameObject)
            {
                Color color = originalMaterial.color;
                color.a = alphaValue;
                transparentMaterial.color = color;
            }
            else
            {
                // 레이가 다른 오브젝트와 충돌하면 투명도를 원래대로 되돌립니다.
                Color color = originalMaterial.color;
                color.a = 1.0f;
                transparentMaterial.color = color;
            }
        }
    }
}
