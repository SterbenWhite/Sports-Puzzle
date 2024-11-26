using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ImagePicker : MonoBehaviour/*, IPointerClickHandler*/
{
    [Tooltip("������ �� ������ �����������")]
    public Image image;
    [Tooltip("������ �� ������ ������")]
    public Button button;
    [Tooltip("���������� ����� �������� �����������")]
    public UnityEvent OnImageLoad;

    // �������, ���������� ��� ������� �� ������
    public void OnPointerClick()
    {
        // ��������, ���� �� ������ ������
        //if (eventData.button == PointerEventData.InputButton.Left)
        //{
        // �������� ����������� ���� ������ �����������
        NativeGallery.GetImageFromGallery((path) =>
        {
            // ��������, ��� �� ������ ����
            if (path != null)
            {
                // �������� ����������� �� ���������� �����
                Texture2D texture = LoadTexture(path);

                // ��������� ���������� ����������� � ������ Image
                if (texture != null)
                {
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    OnImageLoad.Invoke();
                }
                else
                {
                    Debug.LogError("�� ������� ��������� ����������� �� �����: " + path);
                }
            }
            else
            {
                Debug.Log("������������ ������� ����� �����������.");
            }
        });
        //}
    }

    // ������� �������� ����������� �� �����
    private Texture2D LoadTexture(string path)
    {
        // �������� ����� � ���� ������
        byte[] bytes = System.IO.File.ReadAllBytes(path);

        // �������� �������� �� ������
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        return texture;
    }
}
