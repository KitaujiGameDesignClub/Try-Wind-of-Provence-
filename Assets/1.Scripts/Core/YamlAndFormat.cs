using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System.IO;
using YamlDotNet.Serialization;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


//�汾���ƻ�ûŪ�ð�������������������������������������

/// <summary>
/// yaml��д���Լ�yaml��صĸ�ʽ����manifest֮��ģ�
/// </summary>
public class YamlAndFormat
{
    #region YAML

    /// <summary>
    /// ����չʾ������Ϣ���嵥
    /// </summary>
    [System.Serializable]
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
        /// �ؿ�ͼ�ꡣ����addressable���address��һ�£�Ĭ������չ������·���������֣�
        /// </summary>
        [InspectorReadOnly] public string Icon = String.Empty;

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
        [TextArea] public string Instruction = "404 Not Found";

        /// <summary>
        /// �ùؿ��İ汾�������ڶ��Լ����Ĺؿ����а汾���ƣ�
        /// </summary>
        public string Version = "1.0.0";

        /// <summary>
        /// ����dll�������ļ������֣���addressable���addressһ�£�Ĭ������չ����
        /// </summary>
        public string[] DllBytesFileNames = new string[1];
    }

    #endregion

    /// <summary>
    /// Manifest����״̬ -1����ʧ�� 0��û������ 1�ɹ�
    /// </summary>
    public static int ManifestLoadStatue = 0;

    /// <summary>
    /// ��ȡ���е�Manifest�����ڵõ���Ϸ�б������ǿ���Եġ��ض����ab�����»�ȡ
    /// </summary>
    /// <returns></returns>
    public static void GetManifestList()
    {
        //״̬�ָ�
        ManifestLoadStatue = 0;
        Cache.cache.CashForManifestsList.Clear();

        //�첽��������labelΪmanifest���嵥
        Addressables.LoadAssetsAsync<TextAsset>(new List<object> { "Manifest", "Manifest" }, null,
            Addressables.MergeMode.Union).Completed += OnCompleteLoadAllManifest;
    }


    #region ��ȡ����ļ�

    /// <summary>
    /// yaml��ȡ�����ⲿyaml�ļ��ж�ȡ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <param name="directoryName">ר���ļ�������</param>
    /// <returns></returns>
    public static T YamlRead<T>(DefaultDirectory.SubdirectoryTypes types, string directoryName, string fileName)
    {
        //��ǰ׼���ļ���·��
        string path = string.Format("{0}/{1}/{2}/{3}.yaml", DefaultDirectory.UnityButNotAssets, types.ToString(),
            directoryName, fileName);


        if (File.Exists(path))
        {
            //yaml�ļ�������
            string content =
                File.ReadAllText(
                    string.Format("{0}/{1}/{2}/{3}.yaml", DefaultDirectory.UnityButNotAssets, types.ToString(),
                        directoryName, fileName), System.Text.Encoding.UTF8);


            /*
            //���yaml�İ汾���ڶ�ȡ�涨�İ汾�������һ������
            if (int.Parse(content.Split("#")[1]) < VersionControl[(int)types])
            {
                GameDebug.Log(string.Format("��ǰ�ļ��İ汾�ѹ�ʱ������ʱ���ܷ������󡣵Ͱ汾�����Ϊ��{0}����·��Ϊ{1}", types.ToString(), path),
                    GameDebug.Level.Warning);
            }*/


            return YamlRead<T>(content);
        }
        else
        {
            GameDebug.Log(string.Format("{0} �����ڡ�", path), GameDebug.Level.Error);
            return default;
        }
    }

    /// <summary>
    /// yaml��ȡ��ֱ�Ӵ�string��ȡ��
    /// </summary>
    /// <param name="content"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T YamlRead<T>(string content)
    {
        Deserializer read = new();
        return read.Deserialize<T>(content);
    }

    /// <summary>
    /// yaml���ⲿд��Э�� �첽��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <param name="directoryName">ר���ļ�������</param>
    /// <param name="content">yaml����</param>
    public static IEnumerator IEYamlWrite<T>(DefaultDirectory.SubdirectoryTypes types, string directoryName,
        string fileName, [NotNull] T content)
    {
        //��ǰ׼�����ļ��е�·�����������յ��ļ�)
        string path = string.Format("{0}/{1}/{2}", DefaultDirectory.UnityButNotAssets, types.ToString(), directoryName);
        Debug.Log(path);
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
        //�������࣬ͬ���Ͻ�д�����
        yield return sw.WriteAsync(string.Format("# �벻Ҫֱ���޸ı��ļ�\n# �����޸ģ���ʹ����Ϸ�Դ��ı༭��\n\n{0}", yaml));
        sw.Close();
        f.Close();
        yield return sw.DisposeAsync();
        yield return f.DisposeAsync();
    }

    /// <summary>
    /// yaml���ⲿд��ͬ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types">���ļ�������</param>
    /// <param name="fileName">�ļ�����������չ����</param>
    /// <param name="directoryName">ר���ļ������֡��� / ��ͷ</param>
    /// <param name="content">yaml����</param>
    public static void YamlWrite<T>(DefaultDirectory.SubdirectoryTypes types, string directoryName, string fileName,
        [NotNull] T content)
    {
        //��ǰ׼�����ļ��е�·�����������յ��ļ�)
        string path = string.Format("{0}/{1}/{2}", DefaultDirectory.UnityButNotAssets, types.ToString(), directoryName);
        Debug.Log(path);
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
        //�������࣬ͬ���Ͻ�д�����
        sw.Write("# �벻Ҫֱ���޸ı��ļ�\n# �����޸ģ���ʹ����Ϸ�Դ��ı༭��\n\n{0}", yaml);
        sw.Close();
        f.Close();
        sw.Dispose();
        f.Dispose();
    }

    #endregion


    #region ˽�к���

    /// <summary>
    /// �ص����������manifest�ļ������
    /// </summary>
    /// <param name="asyncOperationHandle"></param>
    static void OnCompleteLoadAllManifest(AsyncOperationHandle<IList<TextAsset>> asyncOperationHandle)
    {
        switch (asyncOperationHandle.Status)
        {
            case AsyncOperationStatus.Failed:
                GameDebug.Log(string.Format("{0}���ֻ����е�Manifest�ļ���ȡ����", asyncOperationHandle.OperationException),
                    GameDebug.Level.Warning);
                ManifestLoadStatue = -1;
                break;
            case AsyncOperationStatus.None:
                GameDebug.Log("�������κ�Manifest������û����Ϸ�ļ����߼��ص���Ϸ�ļ���Manifest����", GameDebug.Level.Warning);
                ManifestLoadStatue = -1;
                break;
            case AsyncOperationStatus.Succeeded:
                GameDebug.Log(string.Format("Manifest���سɹ�������{0}��������Ϸ", asyncOperationHandle.Result.Count.ToString()),
                    GameDebug.Level.Information);

                //string��ʽ��Ϊclass/struct
                for (int i = 0; i < asyncOperationHandle.Result.Count; i++)
                {
                    Cache.cache.CashForManifestsList.Add(YamlRead<Manifest>(asyncOperationHandle.Result[i].text));
                }

                //ж�ؼ��ص�yaml�ļ��������Ѿ�����ɿ��õ�����߽ṹ����
                Addressables.Release(asyncOperationHandle);
                ManifestLoadStatue = 1;
                break;
        }
    }

    #endregion
}