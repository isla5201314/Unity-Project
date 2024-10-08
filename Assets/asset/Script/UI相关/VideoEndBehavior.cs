using UnityEngine;
using UnityEngine.Video;

public class VideoEndBehavior : MonoBehaviour
{
    public VideoPlayer videoPlayer; // �������Inspector����קVideoPlayer�������

    private void Start()
    {
        // ȷ����Ƶ�������ʱ����OnVideoEnded����
        videoPlayer.loopPointReached += OnVideoEnded;
        // ��ʼ������Ƶ�������Ҫ��Startʱ�Զ����ţ�
        videoPlayer.Play();
    }

    private void OnVideoEnded(VideoPlayer player)
    {
        // ����д���Ž�����Ĵ����߼�������ر���Ƶ��������������Ϸ����
        Debug.Log("Video has ended");
        videoPlayer.Stop(); // ֹͣ��Ƶ����
        videoPlayer.loopPointReached -= OnVideoEnded; // �Ƴ��¼���������ֹ�ظ�����
        // ʾ�������ػ�������Ϸ����
        // this.gameObject.SetActive(false); // ���ض���
        // Destroy(this.gameObject); // ���ٶ���ȷ��û����������ʱʹ��
    }
}