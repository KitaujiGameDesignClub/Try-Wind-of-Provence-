using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SongsInf : MonoBehaviour
{
    //UI�е���ʾ
    [SerializeField]private TMP_Text StagesName;
    [SerializeField]private  TMP_Text Author;
    [SerializeField]private  TMP_Text ShortInstr;
    [SerializeField]private Image Icon;
  
/// <summary>
/// �����Ϣ��Ƭʹ�õ�manifest��������Ϣ������������
/// </summary>
   public YamlAndFormat.Manifest UsedManifestInf;
   
    /// <summary>
    /// ��ȡmanifest��Ϣ֮��Ӧ��֮�������Ҳ�С������ʾ
    /// </summary>
    /// <param name="manifest"></param>
    public void ApplyInf(YamlAndFormat.Manifest manifest,Sprite icon)
    {
        StagesName.text =manifest.StageName;
        Author.text = string.Format("{0} - {1}",manifest.Version,manifest.Author);
        ShortInstr.text = manifest.ShortInstr;
        Icon.sprite = icon;
        UsedManifestInf = manifest;
    }

    /// <summary>
    /// �õ���Ϣ��Ƭ������ͼ
    /// </summary>
    /// <returns></returns>
    public Sprite GetImage()
    {
        return Icon.sprite;
    }

    /// <summary>
    /// �Ҳ�С�����󣬰������ϸ��Ϣ����Ϊ��ѡ������
    /// </summary>
    public void OnClick()
    {
        MenuCtrl.menuCtrl.OnSelected(this);
    }
}
