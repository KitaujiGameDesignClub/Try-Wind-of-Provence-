using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SongsInf : MonoBehaviour
{
    //�������ݣ��Ժ�����
    public TMP_Text MusicName;
    public TMP_Text Author;
    public TMP_Text Origin;
    public Image Icon;
    public bool IsAdvanced;

    //��ʱ���������Ϊ�˲���Ч�����Ժ�Ҫ�ģ���������
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void ApplyInf(string musicName,string author,string origin,string version,Sprite icon,bool isAdvanced,AudioClip preBGM)
    {
        MusicName.text = musicName;
        Author.text = string.Format("{0} - {1}",version, author);
        Origin.text = origin;
        Icon.sprite = icon;
        IsAdvanced = isAdvanced;
        audioSource.clip = preBGM;

    }

    public void OnClick()
    {
        audioSource.Play();
    }
}
