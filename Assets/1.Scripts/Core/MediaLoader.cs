using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// ý�������
/// </summary>
public class MediaLoader
{
    //���е�se��ЧҲ���������ҲԤ������audioclip������Զ���
    AudioClip AudioClip;
    Sprite Sprite;

    /// <summary>
    /// ͼƬ����״̬ -1����ʧ�� 0��û������ 1�ɹ�
    /// </summary>
    int ImageLoadStatue  = 0;



    /// <summary>
    /// ������ص�ͼ�񣬲��ָ�ԭʼ��״̬
    /// </summary>
    public void ClearLoadedImage()
    {
        Sprite = null;
        ImageLoadStatue = 0;
    }

    /// <summary>
    /// ��ȡͼ�����״̬   -1����ʧ�� 0��û������ 1�ɹ�
    /// </summary>
    public int GetImageLoadStatus()
    {
        return ImageLoadStatue;
    }
    
    
/// <summary>
/// ��ȡ���ص�ͼ�� 
/// </summary>
/// <returns></returns>
    public Sprite GetImage()
    {
        switch (ImageLoadStatue)
        {
            case 1:
                    return Sprite;
                break;
                default:
                    return null;
                break;
        }
    }
    
    
    
    
    
    /// <summary>
    /// �����ⲿogg��Ƶ
    /// </summary>
    /// <param name="types"></param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <returns></returns>
    public IEnumerator IELoadSound()
    {
        yield return null;
    }

    /// <summary>
    /// �����ⲿpngͼƬ
    /// </summary>
    /// <param name="types">���ļ�������</param>
    /// <param name="fileName">�ļ�����������չ������֧��png��</param>
    public IEnumerator IELoadImage(DefaultDirectory.SubdirectoryTypes types, string fileName)
    {
        //��ԭΪ��δ������ɵ�״̬
        ImageLoadStatue = 0;
        
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
                GameDebug.Log(uwr.error, GameDebug.Level.Warning);
                ImageLoadStatue = -1;
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                Sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height), Vector2.zero);
                ImageLoadStatue = 1;
            }

        }
        else
        {
            GameDebug.Log(string.Format("{0}�����ڻ��ļ���ʽ��֧��", filepath), GameDebug.Level.Error);
            ImageLoadStatue = -1;
            yield return null;
        }
    }
    
   /// <summary>
   /// ����addressable�ڵ�ͼƬ��
   /// </summary>
   /// <returns></returns>
    public IEnumerator LoadImage(string AddressableKey)
   {
       //��ԭΪ��δ������ɵ�״̬
       ImageLoadStatue = 0;
       
       Addressables.LoadAssetAsync<Texture2D>(AddressableKey).Completed += LoadImageFromAddressable;
        
        
        yield return null;
        
    }
   
   
   #region �ڲ�����

   private void LoadImageFromAddressable(AsyncOperationHandle<Texture2D> asyncOperationHandle)
   {
       switch (asyncOperationHandle.Status)
       {
           case AsyncOperationStatus.Failed:
               GameDebug.Log(string.Format("ͼƬ���ش���{0}",asyncOperationHandle.OperationException),GameDebug.Level.Warning);
               ImageLoadStatue = -1;
               break;
           case AsyncOperationStatus.None:
               GameDebug.Log(string.Format("ͼƬ�����ڣ�{0}",asyncOperationHandle.OperationException),GameDebug.Level.Warning);
               ImageLoadStatue = -1;
               break;
           case  AsyncOperationStatus.Succeeded:
               Sprite = Sprite.Create(asyncOperationHandle.Result, new Rect(0f,0f,asyncOperationHandle.Result.width,asyncOperationHandle.Result.height), Vector2.zero);
               ImageLoadStatue = 1;
               break;
               
       }
   }
   #endregion

}
