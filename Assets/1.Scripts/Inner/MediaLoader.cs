using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// ý�������
/// </summary>
public class MediaLoader
{
    public AudioClip audioClip = null;
    public Sprite sprite = null;

    //���е�se��ЧҲ���������ҲԤ������audioclip������Զ���

    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="FileName">�ļ�����������չ����</param>
    ///  <param name="DirectoryName">ר���ļ������֣�֧�ֹ�������ļ��У�</param>
    /// <returns></returns>
    public IEnumerator LoadSound(Core.SubdirectoryTypes types, string FileName)
    {
        //����·��
        string filepath = string.Format("{0}/{1}/{2}.ogg", Core.UnityButNotAssets,types.ToString(), FileName);
        //��Ƶ�ļ�����
        if(File.Exists(filepath))
        {
            //�ӱ��ض�ȡ��Դ
            var uwr = UnityWebRequestMultimedia.GetAudioClip(string.Format("file://{0}", filepath), AudioType.OGGVORBIS);
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success && uwr.result != UnityWebRequest.Result.InProgress)
            {
                GameDebug.Log(uwr.error, GameDebug.Level.Error);
            }
            else
            {
                audioClip = DownloadHandlerAudioClip.GetContent(uwr);
            }

        }
        else
        {
            GameDebug.Log(string.Format("{0}�����ڻ��ļ���ʽ��֧��", filepath),GameDebug.Level.Error);
            yield return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="FileName">�ļ�����������չ����</param>
    public IEnumerator LoadImage(Core.SubdirectoryTypes types, string FileName)
    {
        //����·��
        string filepath = string.Format("{0}/{1}/{2}.png", Core.UnityButNotAssets, types.ToString(), FileName);

        //ͼƬ�ļ�����
        if (File.Exists(filepath))
        {
            //�ӱ��ض�ȡ��Դ
            var uwr = UnityWebRequestTexture.GetTexture(string.Format("file://{0}", filepath));
            Debug.Log(uwr.url);
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success && uwr.result != UnityWebRequest.Result.InProgress)
            {
                GameDebug.Log(uwr.error, GameDebug.Level.Error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height), Vector2.zero);
            }

        }
        else
        {
            GameDebug.Log(string.Format("{0}�����ڻ��ļ���ʽ��֧��", filepath), GameDebug.Level.Error);
            yield return null;
        }
    }

}
