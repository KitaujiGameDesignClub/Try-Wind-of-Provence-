using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ӱ��ض�ȡ���ļ�������������л��档
/// </summary>

//ûɶ��
public class ResourceKeeper//ͳͳ����̬�ˣ�������������
{
    /// <summary>
    /// ���˵������滺��
    /// </summary>
    public class MenuTemp
    {
        /// <summary>
        /// �˵��е��嵥����
        /// </summary>
        public class Manifest
        {
            /// <summary>
            /// �������ƣ�����Ϸ��չʾ��
            /// </summary>
            public  string MusicName = "Ĭ�Ϸ���";
            /// <summary>
            /// ����ͼ��
            /// </summary>
            public Sprite Icon;
            /// <summary>
            /// �����������ơ�
            /// </summary>
            public string Author = "RSC���";

            /// <summary>
            /// BGM����/����ר����
            /// </summary>
            public string Origin = "Ĭ�ϳ���";
            /// <summary>
            /// Ԥ������
            /// </summary>
            public AudioClip PreviewBGM;
            /// <summary>
            /// �Ƿ�Ϊ�߼�������
            /// </summary>
            public bool IsAdvanced = false;
            /// <summary>
            /// ���ļ��İ汾�������ڶ��Լ����ķ������а汾���ƣ�
            /// </summary>
            public string Version = "1.0.0";
        }

        /// <summary>
        /// �˵�Ƥ������
        /// </summary>
        public class Skin
        {

        }
    }
}
