using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class GameInitialization : MonoBehaviour
{
    public int loadScene;
    public int openingScene;
    
    

#if UNITY_EDITOR
    public bool textGame = true;
#endif

    public virtual void Awake()
    {

        QualitySettings.vSyncCount = 1;
        
        OnDemandRendering.renderFrameInterval = 1;
     
    }

    public virtual void  Start()
    {
        
        //检查是否存在所需的游戏文件夹，不存在则创建
       YamlReadWrite.CheckAndCreateDirectory();
       //读取设置文件
       Settings.ReadSettings();
       //调整音量
       PublicAudioSource.publicAudioSource.UpdateMusicVolume();
       
       //加载场景
#if !UNITY_EDITOR
          SceneManager.LoadScene("Opening");
        
        #else
      
        SceneManager.LoadScene(textGame ? loadScene : openingScene);
#endif
    }

   
}
