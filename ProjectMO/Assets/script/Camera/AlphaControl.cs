using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControl : MonoBehaviour
{
    public float maxDistance = 10f; // ������ �ִ� �Ÿ�
    public float alphaValue = 0.5f; // ������Ʈ�� ������ �����ϴ� �� (0: ���� ����, 1: ������)
    public LayerMask layerMask; // ����ĳ��Ʈ���� �浹�� �˻��� ���̾� ����ũ

    private Material originalMaterial; // ������Ʈ�� ���� ��Ƽ����
    private Material transparentMaterial; // ���� ��Ƽ����

    private void Start()
    {
        // ������Ʈ�� ���� ��Ƽ������ �����Ͽ� ���� ��Ƽ������ �����մϴ�.
        originalMaterial = GetComponent<Renderer>().material;
        transparentMaterial = new Material(originalMaterial);
        // ���� ��Ƽ���� ����
        transparentMaterial.SetFloat("_Mode", 2);
        transparentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        transparentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        transparentMaterial.SetInt("_ZWrite", 0);
        transparentMaterial.DisableKeyword("_ALPHATEST_ON");
        transparentMaterial.EnableKeyword("_ALPHABLEND_ON");
        transparentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        transparentMaterial.renderQueue = 3000;

        // ������Ʈ�� ��Ƽ������ ���� ��Ƽ����� �ٲߴϴ�.
        GetComponent<Renderer>().material = transparentMaterial;
    }

    private void Update()
    {
        HandleRaycasting();
    }

    private void HandleRaycasting()
    {
        // ī�޶󿡼� ���̸� ���, ������Ʈ���� �浹 ���θ� Ȯ���մϴ�.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            // ���̰� ������Ʈ�� �浹�ϸ� ������Ʈ�� ������ �����մϴ�.
            if (hit.collider.gameObject == gameObject)
            {
                Color color = originalMaterial.color;
                color.a = alphaValue;
                transparentMaterial.color = color;
            }
            else
            {
                // ���̰� �ٸ� ������Ʈ�� �浹�ϸ� ������ ������� �ǵ����ϴ�.
                Color color = originalMaterial.color;
                color.a = 1.0f;
                transparentMaterial.color = color;
            }
        }
    }
}
