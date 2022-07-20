using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ԡ���������UI����ʾ
/// </summary>
public class GameDebug
{
    /// <summary>
    ///���������еĿ���̨��Ϣ
    /// </summary>
    public static List<string> ConsoleContents = new List<string>();

    /// <summary>
    /// ��־�ȼ�
    /// </summary>
    public enum Level
    {
        None,
        Information,
        Warning,
        Error,
    }

    /// <summary>
    /// �����������̨����͵ȼ�
    /// </summary>
    public static Level ConsoleLevel = Level.Information;
    /// <summary>
    /// ���浽��־�ļ�����͵ȼ�
    /// </summary>
    public static Level SaveLevel = Level.Error;

    /// <summary>
    /// ��ʼ������̨
    /// </summary>
    public static void Initialization()
    {
        //����������Ϣ
        ConsoleContents = new List<string>();

#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }


    public static void Log(string content,Level level)
    {
#if UNITY_EDITOR
        Debug.Log(content);
#endif


        //��־�ȼ��㹻���ܹ����������̨
        if((int)level >= (int)ConsoleLevel)
        {
            //���һ��ʶ��ͷ��
            content = string.Format("<{0}> {1}��{2}",level.ToString() ,DateTime.Now, content);
           //���뵽����̨��־��
            ConsoleContents.Add(content);

            //�ȼ���������־�ļ�
            if ((int)level >= (int)SaveLevel)
            {
                //�ȷ����⣬�Ժ���д
            }

        }
    }
}
