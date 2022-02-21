using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public static class TextureExtensions
{
    /// Transform Sprite to Texture
    public static Texture2D GitText(this Sprite sprite)
    {
        int w = Mathf.FloorToInt(sprite.rect.width);
        int h = Mathf.FloorToInt(sprite.rect.height);
        int ox = Mathf.RoundToInt(sprite.textureRect.x);
        int oy = Mathf.RoundToInt(sprite.textureRect.y);

        var texture = new Texture2D(w, h);
        var pixels = sprite.texture.GetPixels(ox, oy, w, h);

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }

}

public static class VectorExtensions 
{
    public static Vector3Int RoundToInt(this Vector3 vector3)
    {
        return new Vector3Int(
            Mathf.RoundToInt(vector3.x),
            Mathf.RoundToInt(vector3.y),
            Mathf.RoundToInt(vector3.z)
        );
    }

    public static Vector3 Flat3(this Vector2 v, float y = 0f)
    {
        return new Vector3(v.x, y, v.y);
    }

    public static Vector2 Flat2(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
}

public static class TransformExtensions
{
    //public static Transform FindDeepChild(this Transform tr, string name)
    //{
    //    var result = tr.Find(name);
    //    if (result)
    //        return result;

    //    int count = tr.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        result = tr.GetChild(i).FindDeepChild(name);
    //        if (result)
    //            return result;
    //    }

    //    return null;
    //}

    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }
}

public static class StringExtensions
{
    public static string CutEnd(this string source, string value)
    {
        if (!source.EndsWith(value))
            return source;

        return source.Remove(source.LastIndexOf(value));
    }

    public static string Title(this string input)
    {
        // TODO: replace all usages with I18N

        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}

public static class VisualElementExtensions
{
    public static void Hide(this VisualElement el)
    {
        el.style.display = DisplayStyle.None;
    }

    public static void Show(this VisualElement el)
    {
        el.style.display = DisplayStyle.Flex;
    }
}