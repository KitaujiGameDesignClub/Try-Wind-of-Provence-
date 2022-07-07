using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

/// <summary>
/// ý�������
/// </summary>
public class MediaLoader
{
    public AudioClip AudioClip;
    public Sprite Sprite;

    //���е�se��ЧҲ���������ҲԤ������audioclip������Զ���

    /// <summary>
    /// �����ⲿogg��Ƶ
    /// </summary>
    /// <param name="types"></param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <returns></returns>
    public IEnumerator LoadSound()
    {
        yield return null;
    }

    /// <summary>
    /// �����ⲿpngͼƬ
    /// </summary>
    /// <param name="types"></param>
    /// <param name="fileName">�ļ�����������չ����</param>
    public IEnumerator LoadImage(DefaultDirectory.SubdirectoryTypes types, string fileName)
    {
        //����·��
        string filepath = string.Format("{0}/{1}/{2}.png", DefaultDirectory.UnityButNotAssets, types.ToString(), fileName);

        //ͼƬ�ļ�����
        if (File.Exists(filepath))
        {
            //�ӱ��ض�ȡ��Դ
            var uwr = UnityWebRequestTexture.GetTexture(string.Format("file://{0}", filepath));

            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success && uwr.result != UnityWebRequest.Result.InProgress)
            {
                GameDebug.Log(uwr.error, GameDebug.Level.Error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                Sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height), Vector2.zero);
            }

        }
        else
        {
            GameDebug.Log(string.Format("{0}�����ڻ��ļ���ʽ��֧��", filepath), GameDebug.Level.Error);
            yield return null;
        }
    }
    
   /// <summary>
   /// ����addressable�ڵ�ͼƬ
   /// </summary>
   /// <returns></returns>
    public IEnumerator LoadImage(string AddressableKey)
    {
     
        
        
        yield return null;
        
    }

}
