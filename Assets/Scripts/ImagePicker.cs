using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ImagePicker : MonoBehaviour/*, IPointerClickHandler*/
{
    [Tooltip("Ссылка на объект изображения")]
    public Image image;
    [Tooltip("Ссылка на объект кнопки")]
    public Button button;
    [Tooltip("Вызывается после загрузки изображения")]
    public UnityEvent OnImageLoad;

    // Функция, вызываемая при нажатии на кнопку
    public void OnPointerClick()
    {
        // Проверка, была ли кнопка нажата
        //if (eventData.button == PointerEventData.InputButton.Left)
        //{
        // Открытие диалогового окна выбора изображения
        NativeGallery.GetImageFromGallery((path) =>
        {
            // Проверка, был ли выбран файл
            if (path != null)
            {
                // Загрузка изображения из выбранного файла
                Texture2D texture = LoadTexture(path);

                // Установка выбранного изображения в объект Image
                if (texture != null)
                {
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    OnImageLoad.Invoke();
                }
                else
                {
                    Debug.LogError("Не удалось загрузить изображение из файла: " + path);
                }
            }
            else
            {
                Debug.Log("Пользователь отменил выбор изображения.");
            }
        });
        //}
    }

    // Функция загрузки изображения из файла
    private Texture2D LoadTexture(string path)
    {
        // Загрузка файла в виде байтов
        byte[] bytes = System.IO.File.ReadAllBytes(path);

        // Создание текстуры из байтов
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        return texture;
    }
}
