using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SongsInf : MonoBehaviour
{
    //�������ݣ��Ժ�����
    public TMP_Text StagesName;
    public TMP_Text Author;
    public TMP_Text ShortInstr;
    public Image Icon;
   [HideInInspector]public string Instruction;
    
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
        Instruction = manifest.Instruction;

    }

    /// <summary>
    /// �Ҳ�С�����󣬰������ϸ��Ϣ����Ϊ��ѡ������
    /// </summary>
    public void OnClick()
    {
        MenuCtrl.menuCtrl.OnSelected(this);
    }
}
