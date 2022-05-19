using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ����ҳ��UI�ű�
/// </summary>
public class Opening : MonoBehaviour
{
    private void Awake()
    {
        
    }


#if UNITY_EDITOR

    Core.Manifest text = new Core.Manifest
    {
        Name = "text",
        MusicName = "����",
        Author = "wdnmd",
        Origin = "�����",
        IsAdvanced = false,
        Version = "�������Եİ汾",
        PreviewBGM = "Pre.mp3",
    };

    /// <summary>
    /// дһ��ʵ�����ݡ�ѧϰ��
    /// </summary>
    [ContextMenu("Write")]
    public void Write()
    {
        Core.YamlWrite(Core.SubdirectoryTypes.SpellCards, text.Name,"Manifest", text);

    }

    [ContextMenu("Read")]
    public void Read()
    {
    var data = Core.YamlRead<Core.Manifest>(Core.SubdirectoryTypes.SpellCards, text.Name,"Manifest");
    }

    public void PlayPreBGM()
    {

    }

#endif
}
