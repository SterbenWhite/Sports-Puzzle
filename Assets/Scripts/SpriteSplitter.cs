using UnityEngine;

public class SpriteSplitter : MonoBehaviour
{
    public static Sprite[] SplitTexture(Texture2D texture, int columns, int rows)
    {
        Sprite[] spritesArray = new Sprite[rows * columns];
        int width = texture.width / columns;
        int height = texture.height / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Rect rect = new Rect(x * width, y * height, width, height);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                spritesArray[y * columns + x] = Sprite.Create(texture, rect, pivot);
            }
        }
        return spritesArray;
    }
}
