using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    public AudioSource AudioSource;
    public LayerMask LayerMask;
    public Image Pointer;
    public Image Img;
    Sequence sequence;
    private void Start()
    {
        sequence = DOTween.Sequence();
        ClickEvent(GetColor(), 0.3f);
        Pointer.color = GetColor();
        Cursor.visible = false;
    }
    private void Update()
    {
        bool isLeftMouse = Input.GetKeyDown(KeyCode.Mouse0);
        bool isRightMouse = Input.GetKeyDown(KeyCode.Mouse1);
        if ((isLeftMouse || isRightMouse) && isLeftMouse != isRightMouse)
        {
            ClickEvent(GetColor(), 0.3f);
            TryRaycast(isLeftMouse,Camera.main.ScreenPointToRay(Input.mousePosition), LayerMask);
        }
        Pointer.transform.localPosition = Input.mousePosition;
        Img.transform.localPosition = Input.mousePosition;
    }
    private void ClickEvent(Color color, float speed)
    {
        AudioSource.Play();
        sequence.Kill();
        sequence = DOTween.Sequence();
        sequence
            .Append(DOTween.To(()=>Pointer.color, x=> Pointer.color = x, color, speed))
            .Join(DOTween.To(() => Img.color, x => Img.color = x, color, speed))
            .Join(Pointer.transform.DOScale(0.3f, speed))
            .Append(Pointer.transform.DOScale(1, speed));
    }
    private Color GetColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r, g, b, 0.5f);
    }
    public void TryRaycast(bool isAcc,Ray ray, LayerMask layerMask, float distance = Mathf.Infinity)
    {
        bool isRaycast = Physics.Raycast(ray, out RaycastHit raycastHit, distance, layerMask);
        if (isRaycast)
        {
            if(raycastHit.transform.TryGetComponent<Rotate>(out Rotate rotate))
            {
                rotate.ChangeSpeed(isAcc);
            }
        }
    }

}
