using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��Ϸ����ҳ��UI�ű�
/// </summary>
public class Opening : MonoBehaviour
{
    private void Awake()
    {
      
    }


#if UNITY_EDITOR

   

    /// <summary>
    /// дһ��ʵ�����ݡ�ѧϰ��
    /// </summary>
    [ContextMenu("Write")]
    public void Write()
    {
       

    }

    [ContextMenu("Read")]
    public void Read()
    {
   
    }

    public void PlayPreBGM()
    {

    }

#endif
}
