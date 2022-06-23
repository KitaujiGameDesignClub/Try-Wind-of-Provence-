using System.Collections.Generic;
using UnityEngine;
using System.IO;
using YamlDotNet.Serialization;


/// <summary>
/// yaml��д���Լ�yaml��صĸ�ʽ����manifest֮��ģ�
/// </summary>
public class YamlAndFormat
{

    #region YAML
    
    /// <summary>
    /// yaml�汾���ƣ�˳����SubdirectoryTypesһ��
    /// </summary>
    readonly static int[] VersionControl = { 1, 1, 1, 1, 1 };
    
    /// <summary>
    /// ����չʾ������Ϣ���嵥����������Ļ���(YAML��
    /// </summary>
    public class Manifest
    {
        /// <summary>
        /// �ؿ����ơ������ļ��������Լ���λ
        /// </summary>
        public string Name = "Default Stages";

        /// <summary>
        /// �ؿ����ơ����ڶ����չʾ
        /// </summary>
        public string StageName = "Ĭ�Ϲؿ�";

        /// <summary>
        /// �ؿ�ͼ�ꡣĬ���ڶ�Ӧ�Ĺؿ�Ŀ¼�£�png��ʽ��������չ��
        /// </summary>
        public string Icon = "Icon";

        /// <summary>
        /// �ؿ��������ơ�
        /// </summary>
        public string Author = "��Ը͸¶�����Ĵ���";

        /// <summary>
        /// �򵥽��ܡ��Ҳ�ؿ�ѡ���б��У��Ա��ؿ����м򵥵Ľ���
        /// </summary>
        public string ShortInstr = "���ܰ׸���";

        /// <summary>
        /// ��ϸ�Ľ��ܡ�����������У��Ա��ؿ�������ϸ�Ľ���
        /// </summary>
        public string Instruction = "404 Not Found";

        /// <summary>
        /// �����Ѷȡ������趨һ���Ѷ�,����Easy Normal Hard Lunatic��˳��.
        /// </summary>
        public bool[] AllowedDifficulty = { false, true, false, false };

        /// <summary>
        /// �߼��ؿ���  dll��װ������Ϊtrue
        /// </summary>
        public bool IsAdvanced = false;
        /// <summary>
        /// �ùؿ��İ汾�������ڶ��Լ����Ĺؿ����а汾���ƣ�
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
        var all = ReadAllSubdirectory(DefaultDirectory.SubdirectoryTypes.Stages);

        //������Ч��valid�ļ�
        List<Manifest> valid = new List<Manifest>();

        //���嵥�ļ��ʹ��ȥ
        foreach (var item in all)
        {

            if (File.Exists(string.Format("{0}/Manifest.yaml", item.FullName)))
            {
                valid.Add(YamlRead<Manifest>(DefaultDirectory.SubdirectoryTypes.Stages, item.Name, "Manifest"));
            }
        }

        Debug.Log(valid[0].Name);
        return valid;
    }



    #region ��ȡ����ļ�


    /// <summary>
    /// yaml��ȡ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <param name="directoryName">ר���ļ�������</param>
    /// <returns></returns>
    public static T YamlRead<T>(DefaultDirectory.SubdirectoryTypes types, string directoryName, string fileName)
    {       
        //��ǰ׼���ļ���·��
        string path = string.Format("{0}/{1}/{2}/{3}.yaml", DefaultDirectory.UnityButNotAssets, types.ToString(), directoryName,fileName);

    
        
        if (File.Exists(path))
        {
            //yaml�ļ�������
            string content = File.ReadAllText(string.Format("{0}/{1}/{2}/{3}.yaml", DefaultDirectory.UnityButNotAssets, types.ToString(), directoryName, fileName), System.Text.Encoding.UTF8);


            //���yaml�İ汾���ڶ�ȡ�涨�İ汾�������һ������
            if(int.Parse(content.Split("#")[1]) < VersionControl[(int)types])
            {
                GameDebug.Log(string.Format("��ǰ�ļ��İ汾�ѹ�ʱ������ʱ���ܷ������󡣵Ͱ汾�����Ϊ��{0}����·��Ϊ{1}",types.ToString(),path), GameDebug.Level.Warning);
            }

            Deserializer read = new();
            return read.Deserialize<T>(content);

        }
        else
        {
            GameDebug.Log(string.Format("{0} �����ڡ�", path), GameDebug.Level.Error);
            return default;
        }

    }
    /// <summary>
    /// yamlд��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <param name="directoryName">ר���ļ�������</param>
    /// <param name="content">yaml����</param>
    public static void YamlWrite<T>(DefaultDirectory.SubdirectoryTypes types, string directoryName, string fileName, T content)
    {
        //��ǰ׼�����ļ��е�·�����������յ��ļ�)
        string path = string.Format("{0}/{1}/{2}", DefaultDirectory.UnityButNotAssets, types.ToString(), directoryName);
        //׼�������л���yaml����
        var write = new Serializer();
        var yaml = write.Serialize(content);


        //·�������ڲ����ھʹ�����Ӧ���ļ���
        if (!File.Exists(string.Format("{0}/{1}.yaml", path, fileName)))
        {
            Directory.CreateDirectory(path);
        }

        //ֱ�Ӵ���һ���µ��ļ����ˣ�˳��������ļ���д��ȥ
        var f = new FileStream(string.Format("{0}/{1}.yaml", path, fileName), FileMode.Create);
        StreamWriter sw = new(f, System.Text.Encoding.UTF8);
        sw.Write("#{0}\n# �벻Ҫֱ���޸ı��ļ�\n# �����޸ģ���ʹ����Ϸ�Դ��ı༭��\n\n{1}",VersionControl[(int)types].ToString(), yaml);
        sw.Close();
        f.Close();
    }

    #endregion


    #region  ˽�к���
    /// <summary>
    /// ��ȡȫ�����ļ��У����ܰ����Ƿ����ļ��У���Ҫ��Ӧ�Ĵ�������ж�
    /// </summary>
    /// <param name="subdirectoryTypes"></param>
    static DirectoryInfo[] ReadAllSubdirectory(DefaultDirectory.SubdirectoryTypes subdirectoryTypes)
    {
        string path = string.Format("{0}/{1}", DefaultDirectory.UnityButNotAssets, subdirectoryTypes.ToString());
        if (Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //�õ�SpellCards�ļ����µ��������ļ��У���һ����
        DirectoryInfo folder = new DirectoryInfo(path);
        return folder.GetDirectories();
    }

    #endregion

}
