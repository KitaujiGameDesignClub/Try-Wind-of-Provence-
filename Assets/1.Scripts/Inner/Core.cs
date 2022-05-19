using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using YamlDotNet.Serialization;


public class Core
{
    /// <summary>
    /// Ԥ������ļ���
    /// </summary>
    public enum SubdirectoryTypes
    {
        SpellCards,
        Skins,
        Settings,
        PlayerProfile,
        Log,
    }

    /// <summary>
    /// �汾���ƣ�˳����SubdirectoryTypesһ��
    /// </summary>
    readonly static int[] VersionControl = { 1, 1, 1, 1, 1 };


    /// <summary>
    /// Assets��һ����Ŀ¼
    /// </summary>
    /// <returns></returns>
    internal static string UnityButNotAssets
    {
        get
        {
            string[] raw = Application.dataPath.Split("/");

            string done = string.Empty;
            for (int i = 1; i < raw.Length - 1; i++)
            {
                //�õ���unity��ʼ��·��
                done = string.Format("{0}/{1}", done, raw[i]);
            }

            DirectoryInfo directoryInfo = new(done);
            return directoryInfo.FullName;
        }
    }


    #region YAML�õ���Ķ��岿��
    /// <summary>
    /// ����չʾ������Ϣ���嵥����������Ļ���(YAML��
    /// </summary>
    public class Manifest
    {
        /// <summary>
        /// Ӧ�������ڵ��ļ���ͬ����
        /// </summary>
        public string Name = "Default SpellCard";
        /// <summary>
        /// �������ƣ�����Ϸ��չʾ��
        /// </summary>
        public string MusicName = "Ĭ�Ϸ���";
        /// <summary>
        /// ����ͼ�꣨������չ����
        /// </summary>
        public string Icon = "Icon";
        /// <summary>
        /// �����������ơ�
        /// </summary>
        public string Author = "RSC���";

        /// <summary>
        /// BGM����/����ר����
        /// </summary>
        public string Origin = "Ĭ�ϳ���";
        /// <summary>
        /// Ԥ�����֣�������չ����
        /// </summary>
        public string PreviewBGM = "Pre";
        /// <summary>
        /// �Ƿ�Ϊ�߼�������
        /// </summary>
        public bool IsAdvanced = false;
        /// <summary>
        /// ���ļ��İ汾�������ڶ��Լ����ķ������а汾���ƣ�
        /// </summary>
        public string Version = "1.0.0";
    }

    #endregion

    /// <summary>
    /// ��SpellCards�ļ����ֶ�ȡ���е��嵥�ļ��������γ������б�
    /// </summary>
    /// <returns></returns>
    public static List<Manifest> ManifestList()
    {
        var all = ReadAllSubdirectory(SubdirectoryTypes.SpellCards);

        //������Ч��valid�ļ�
        List<Manifest> valid = new List<Manifest>();

        //���嵥�ļ��ʹ��ȥ
        foreach (var item in all)
        {

            if (File.Exists(string.Format("{0}/Manifest.yaml", item.FullName)))
            {
                valid.Add(YamlRead<Manifest>(SubdirectoryTypes.SpellCards, item.Name, "Manifest"));
            }
        }

        return valid;
    }



    #region ��ȡ����ļ�


    /// <summary>
    /// yaml��ȡ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="FileName">�ļ�����������չ����</param>
    /// <param name="DirectoryName">ר���ļ�������</param>
    /// /// <param name="Version">�涨����Ͱ汾</param>
    /// <returns></returns>
    public static T YamlRead<T>(SubdirectoryTypes types, string DirectoryName, string FileName)
    {       
        //��ǰ׼���ļ���·��
        string Path = string.Format("{0}/{1}/{2}/{3}.yaml", UnityButNotAssets, types.ToString(), DirectoryName,FileName);

        if (File.Exists(Path))
        {
            //yaml�ļ�������
            string content = File.ReadAllText(string.Format("{0}/{1}/{2}/{3}.yaml", UnityButNotAssets, types.ToString(), DirectoryName, FileName, System.Text.Encoding.UTF8));


            //���yaml�İ汾���ڶ�ȡ�涨�İ汾�������һ������
            if(int.Parse(content.Split("#")[1]) < VersionControl[(int)types])
            {
                GameDebug.Log(string.Format("��ǰ�ļ��İ汾�ѹ�ʱ������ʱ���ܷ������󡣵Ͱ汾�����Ϊ��{0}����·��Ϊ{1}",types.ToString(),Path), GameDebug.Level.Warning);
            }

            Deserializer read = new();
            return read.Deserialize<T>(content);

        }
        else
        {
            Debug.LogError(string.Format("{0} �����ڡ�", Path));
            return default;
        }

    }
    /// <summary>
    /// yamlд��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="FileName">�ļ�����������չ����</param>
    /// <param name="DirectoryName">ר���ļ�������</param>
    /// <param name="Content">yaml����</param>
    /// <param name="Version">�涨����Ͱ汾</param>
    public static void YamlWrite<T>(SubdirectoryTypes types, string DirectoryName, string FileName, T Content)
    {
        //��ǰ׼�����ļ��е�·�����������յ��ļ�)
        string Path = string.Format("{0}/{1}/{2}", UnityButNotAssets, types.ToString(), DirectoryName);
        //׼�������л���yaml����
        var write = new Serializer();
        var yaml = write.Serialize(Content);


        //·�������ڲ����ھʹ�����Ӧ���ļ���
        if (!File.Exists(string.Format("{0}/{1}.yaml", Path, FileName)))
        {
            Directory.CreateDirectory(Path);
        }

        //ֱ�Ӵ���һ���µ��ļ����ˣ�˳��������ļ���д��ȥ
        var f = new FileStream(string.Format("{0}/{1}.yaml", Path, FileName), FileMode.Create);
        StreamWriter sw = new(f, System.Text.Encoding.UTF8);
        sw.Write(string.Format("#{0}\n# �벻Ҫֱ���޸ı��ļ�\n# �����޸ģ���ʹ����Ϸ�Դ��ı༭��\n\n{1}",VersionControl[(int)types].ToString(), yaml.ToString()));
        sw.Close();
        f.Close();
    }

    #endregion


    #region  �ڲ�����
    /// <summary>
    /// ��ȡȫ�����ļ��У����ܰ����Ƿ����ļ��У���Ҫ��Ӧ�Ĵ�������ж�
    /// </summary>
    /// <param name="subdirectoryTypes"></param>
    internal static DirectoryInfo[] ReadAllSubdirectory(SubdirectoryTypes subdirectoryTypes)
    {
        string path = string.Format("{0}/{1}", UnityButNotAssets, subdirectoryTypes.ToString());
        if (Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //�õ�SepllCards�ļ����µ��������ļ��У���һ����
        DirectoryInfo folder = new DirectoryInfo(path);
        return folder.GetDirectories();
    }

    #endregion

}
