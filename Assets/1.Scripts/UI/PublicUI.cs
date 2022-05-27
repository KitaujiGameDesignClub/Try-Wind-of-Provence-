using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class PublicUI : MonoBehaviour
{
    public static PublicUI publicUI;

    public EventSystem eventSystem;

    AudioSource BGMPlayer;
    AudioSource preMusicPlayer;
    AudioSource SEPlayer;




    private void Awake()
    {
        publicUI = this;

        DontDestroyOnLoad(gameObject);

        //��ȡ���
        AudioSource[] audioSources = GetComponents<AudioSource>();
        BGMPlayer = audioSources[0];
        SEPlayer = audioSources[1];
        preMusicPlayer = audioSources[2];

        //�������
        BGMPlayer.loop = false;
        BGMPlayer.playOnAwake = false;
        SEPlayer.loop = false;
        SEPlayer.playOnAwake = false;
        preMusicPlayer.loop = true;
        preMusicPlayer.playOnAwake = false;
    }

    /// <summary>
    /// �趨EventSystem��FirstSelected����
    /// </summary>
    /// <param name="go"></param>
    public void SetEventSystemFirstSelected(GameObject go)
    {
        eventSystem.firstSelectedGameObject = go;
    }

    /// <summary>
    /// Ӧ������
    /// </summary>
    /// <param name="index">0=BGM��preBGM 1=SE  ����ֵ������</param>
    public void ApplyVolume(int index)
    {
        switch (index)
        {
            case 0:
                preMusicPlayer.volume = Settings.MasterVol * Settings.BGMvol;
                BGMPlayer.volume = preMusicPlayer.volume;
                break;

            case 1:
                SEPlayer.volume = Settings.MasterVol * Settings.SoundEffectsVol;
                break;

            default:
                Settings.MasterVol = 0f;
                SEPlayer.volume = 0f;
                BGMPlayer.volume = 0f;
                preMusicPlayer.volume = 0f;
                break;


        }
    }


    /// <summary>
    /// ��������BGM
    /// </summary>
    /// <param name="audioClip"></param>
   public void PlayFullBGM(AudioClip audioClip)
    {

    }

   

    /// <summary>
    /// ����Ԥ��BGM
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayPreBGM(AudioClip audioClip)
    {
        //��ֹ֮ͣǰ��bgm
        preMusicPlayer.Stop();
        //�������е�clip
        preMusicPlayer.clip = audioClip;
        //����
        preMusicPlayer.Play();


        //����Ӧ���е���Ч����
    }
}
