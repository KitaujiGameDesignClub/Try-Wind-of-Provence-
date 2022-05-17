using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class MediaPlayer : MonoBehaviour
{
    public static AudioClip BGM;

    //���е�se��ЧҲ���������ҲԤ������audioclip������Զ���

    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="FileName">�ļ���������չ����</param>
    /// <returns></returns>
    public IEnumerator LoadMusicAndSE(Core.SubdirectoryTypes types, string FileName, AudioType audioType = AudioType.MPEG)
    {
        string filepath = string.Format("{0}/{1}/{2}", Core.UnityButNotAssets, types.ToString(), FileName);

        var uwr = UnityWebRequestMultimedia.GetAudioClip(filepath, audioType);

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success && uwr.result != UnityWebRequest.Result.InProgress)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            BGM = DownloadHandlerAudioClip.GetContent(uwr);
        }
    }

}
