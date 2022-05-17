using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using YamlDotNet.Serialization;


/// <summary>
/// �����ķ�������ɶ��
/// </summary>
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
    }

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
                done = string.Format("{0}/{1}", done, raw[i]);
            }
            return done;
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
        public string Name { get; set; }
        /// <summary>
        /// �������ƣ�����Ϸ��չʾ��
        /// </summary>
        public string MusicName { get; set; }
        /// <summary>
        /// �����������ơ�
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// BGM����/����ר����
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// Ԥ�����֣�����չ����
        /// </summary>
        public string PreviewBGM { get; set; }
        /// <summary>
        /// ���ļ��İ汾��
        /// </summary>
        public string Version { get { return "1.0.0"; } set { } }
    }

    #endregion

    /// <summary>
    /// ��ȡȫ�����ļ��У����ܰ����Ƿ����ļ��У���Ҫ��Ӧ�Ĵ�������ж�
    /// </summary>
    /// <param name="subdirectoryTypes"></param>
    public static DirectoryInfo[] ReadAllSubdirectory(SubdirectoryTypes subdirectoryTypes)
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
    /// <param name="DirectoryName">ר���ļ������֣�֧�ֹ�������ļ��У�</param>
    /// <returns></returns>
    public static T YamlRead<T>(SubdirectoryTypes types, string DirectoryName, string FileName)
    {       
        //��ǰ׼���ļ���·��
        string Path = string.Format("{0}/{1}/{2}/{3}.yaml", UnityButNotAssets, types.ToString(), DirectoryName,FileName);

        if (File.Exists(Path))
        {
            Deserializer read = new();
            return read.Deserialize<T>(File.ReadAllText(string.Format("{0}/{1}/{2}/{3}.yaml", UnityButNotAssets, types.ToString(), DirectoryName, FileName, System.Text.Encoding.UTF8)));

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
    /// <param name="DirectoryName">ר���ļ������֣�֧�ֹ�������ļ��У�</param>
    /// <param name="Content">yaml����</param>
    public static void YamlWrite<T>(SubdirectoryTypes types, string DirectoryName, string FileName, T Content)
    {
        //��ǰ׼�����ļ��е�·�����������յ��ļ�)
        string Path = string.Format("{0}/{1}/{2}", UnityButNotAssets, types.ToString(), DirectoryName);
        //׼�������л���yaml����
        Debug.Log(Content);
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
        sw.Write(string.Format("{0}\n#�벻Ҫֱ���޸ı��ļ���",yaml.ToString()));
        sw.Close();
        f.Close();
    }

    #endregion
}
