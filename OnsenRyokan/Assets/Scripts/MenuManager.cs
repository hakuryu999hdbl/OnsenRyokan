using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public sealed class MenuManager : MonoBehaviour
{
    [SerializeField] private Text[] buttonLabels;

    private Font menuFont;

    private void Awake()
    {
        menuFont = Font.CreateDynamicFontFromOSFont(
            new[] { "Microsoft YaHei", "SimHei", "PingFang SC", "Noto Sans CJK SC" }, 30);

        if (menuFont == null)
            return;

        foreach (Text label in buttonLabels)
            label.font = menuFont;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
            OpenSaveFolder();
    }

    private void OnDestroy()
    {
        if (menuFont != null)
            Destroy(menuFont);
    }

    public void OpenSaveFolder()
    {
        string path = Application.persistentDataPath;

        try
        {
            Directory.CreateDirectory(path);
            Application.OpenURL(new Uri(path + Path.DirectorySeparatorChar).AbsoluteUri);
        }
        catch (IOException exception)
        {
            Debug.LogError($"无法打开存档目录 {path}：{exception.Message}", this);
        }
        catch (UnauthorizedAccessException exception)
        {
            Debug.LogError($"无法访问存档目录 {path}：{exception.Message}", this);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
