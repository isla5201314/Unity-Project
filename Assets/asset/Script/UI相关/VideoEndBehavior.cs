using UnityEngine;
using UnityEngine.Video;

public class VideoEndBehavior : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 这个将在Inspector中拖拽VideoPlayer组件进来

    private void Start()
    {
        // 确保视频播放完毕时调用OnVideoEnded方法
        videoPlayer.loopPointReached += OnVideoEnded;
        // 开始播放视频（如果需要在Start时自动播放）
        videoPlayer.Play();
    }

    private void OnVideoEnded(VideoPlayer player)
    {
        // 这里写播放结束后的处理逻辑，比如关闭视频播放器或隐藏游戏对象
        Debug.Log("Video has ended");
        videoPlayer.Stop(); // 停止视频播放
        videoPlayer.loopPointReached -= OnVideoEnded; // 移除事件监听，防止重复调用
        // 示例：隐藏或销毁游戏对象
        // this.gameObject.SetActive(false); // 隐藏对象
        // Destroy(this.gameObject); // 销毁对象，确保没有其他引用时使用
    }
}