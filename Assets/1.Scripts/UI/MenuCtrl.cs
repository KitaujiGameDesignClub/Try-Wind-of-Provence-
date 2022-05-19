using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCtrl : MonoBehaviour
{
    /// <summary>
    /// ý�������
    /// </summary>
    MediaLoader mediaLoader = new();
    /// <summary>
    /// Ԥ�������/���� ��Ƭ
    /// </summary>
    public GameObject songsInf;
    public Transform ManifestParent;

    private void Awake()
    {
        CreateList();
    }



    [ContextMenu("��ȡ�б�")]
    ///�����б�
    public void CreateList()
    {

        ClearList();
        StartCoroutine(Load());      
    }

    /// <summary>
    /// �ӱ��ؼ�����Դ
    /// </summary>
    /// <returns></returns>
    IEnumerator Load()
    {
        //�ȴӱ��ض�ȡ�ļ���
        var list = Core.ManifestList();

        //������Ƭ������Ƭ��д����Ϣ
        for (int i = 0; i < list.Count; i++)
        {
            //�õ�ý���ļ�
           yield return StartCoroutine(mediaLoader.LoadSound(Core.SubdirectoryTypes.SpellCards, string.Format("{0}/{1}", list[i].Name, list[i].PreviewBGM)));
           yield return StartCoroutine(mediaLoader.LoadImage(Core.SubdirectoryTypes.SpellCards, string.Format("{0}/{1}", list[i].Name, list[i].Icon)));

            GameObject go = Instantiate(songsInf, ManifestParent, false);
            go.transform.SetParent(ManifestParent);
            go.GetComponent<SongsInf>().ApplyInf(list[0].MusicName, list[0].Author, list[0].Origin, list[0].Version, mediaLoader.sprite, list[0].IsAdvanced, mediaLoader.audioClip);
        }
    }

    [ContextMenu("����б�")]
    /// <summary>
    /// ����б�
    /// </summary>
    public void ClearList()
    {

        for (int i = ManifestParent.childCount - 1; i >= 0; i--)
        {
          DestroyImmediate(ManifestParent.GetChild(i).gameObject);

        }
    }
}
