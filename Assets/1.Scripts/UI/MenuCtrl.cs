using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public static MenuCtrl menuCtrl;

    [Header("�����Ϣ��ʾ")]
    public TMP_Text MusicName;
    public Image MusicIcon;
    

    private void Awake()
    {
        menuCtrl = this;

        CreateList();
    }
   
    /// <summary>
    /// �����б�
    /// </summary>
    [ContextMenu("��ȡ�б�")]
    public void CreateList()
    {
        //������Ժ��б�
        ClearList();
        //��ȡ�������б�˳���趨EventSystem��FirstSelected����
        StartCoroutine(Load());      
    }


    /// <summary>
    /// ���汻ѡ����չʾ��Ϣ�벥��preBGM
    /// </summary>
    public void OnSelected(SongsInf songsInf)
    {
        //b����ѡ���������Ԥ��BGM
       PublicUI.publicUI.PlayPreBGM(songsInf.PreBGM);
        //չʾ����ͼ
        MusicIcon.sprite = songsInf.Icon.sprite;
        //չʾ����
        MusicName.text = songsInf.MusicName.text;
    }

    /// <summary>
    /// �ӱ��ؼ�����Դ�������б�
    /// </summary>
    /// <returns></returns>
    IEnumerator Load()
    {
        //�ȴӱ��ض�ȡ�ļ���
        var list = Core.ManifestList();

        //������Ƭ������Ƭ��д����Ϣ
        for (int i = 0; i < list.Count; i++)
        {
            //�õ�ý���ļ���ͼ���Ԥ��bgm��
           yield return StartCoroutine(mediaLoader.LoadSound(Core.SubdirectoryTypes.SpellCards, string.Format("{0}/{1}", list[i].Name, list[i].PreviewBGM)));
           yield return StartCoroutine(mediaLoader.LoadImage(Core.SubdirectoryTypes.SpellCards, string.Format("{0}/{1}", list[i].Name, list[i].Icon)));

            GameObject go = Instantiate(songsInf, ManifestParent, false);
            go.transform.SetParent(ManifestParent);
            go.GetComponent<SongsInf>().ApplyInf(list[0].MusicName, list[0].Author, list[0].Origin, list[0].Version, mediaLoader.sprite, list[0].IsAdvanced, mediaLoader.audioClip,list[0].AllowedDifficulty);
       
        
            if(i == 0)
            {
                PublicUI.publicUI.SetEventSystemFirstSelected(go);
            }
        }
    }

    /// <summary>
    /// ����б�
    /// </summary>
    [ContextMenu("����б�")]
    public void ClearList()
    {

        for (int i = ManifestParent.childCount - 1; i >= 0; i--)
        {
          DestroyImmediate(ManifestParent.GetChild(i).gameObject);

        }
    }
}
