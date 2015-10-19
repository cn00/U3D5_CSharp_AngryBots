using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace VoiceChannel {

	public class VoiceChannelPlugin {
#if UNITY_IPHONE
		/* Interface to native implementation */

		[DllImport ("__Internal")]
		private static extern void _SetDebugLog (bool enable);

		[DllImport ("__Internal")]
		private static extern void _AddCallback (string obje);
		
		[DllImport ("__Internal")]
		private static extern void _RemoveCallback (string obj);
		
		[DllImport ("__Internal")]
		private static extern void _RemoveAllCallback ();
		
		[DllImport ("__Internal")]
		private static extern void _Init (string appKey);
		
		[DllImport ("__Internal")]
		private static extern void _SetLoginInfo(string userID, string nickname, string password);

		[DllImport ("__Internal")]
		private static extern void _RequestUserNickname(string[] userIDArray, int length);

		[DllImport ("__Internal")]
		private static extern void _Exit_ ();

		[DllImport ("__Internal")]
		private static extern void _JoinChannel (string channelInfo, string password);

		[DllImport ("__Internal")]
		private static extern void _ExitChannel ();

		[DllImport ("__Internal")]
		private static extern void _KickMember (string userID);
		
		[DllImport ("__Internal")]
		private static extern void _StartTalking ();
		
		[DllImport ("__Internal")]
		private static extern void _StopTalking ();
		
		[DllImport ("__Internal")]
		private static extern void _Unsilence (string userID);
		
		[DllImport ("__Internal")]
		private static extern void _Silence (string userID);
		
		[DllImport ("__Internal")]
		private static extern void _Mute ();
		
		[DllImport ("__Internal")]
		private static extern void _Restore();
		
		[DllImport ("__Internal")]
		private static extern void _Elevate(string userID);
		
		[DllImport ("__Internal")]
		private static extern void _Demote(string userID);
		
		[DllImport ("__Internal")]
		private static extern void _SetChannelTalkMode(int talkmode);

		[DllImport ("__Internal")]
		private static extern long _getCurrentVoiceTrafficSend();

		[DllImport ("__Internal")]
		private static extern void _RequestChannelDetail(string token);
#endif

#if UNITY_ANDROID
		static class SingletonHolder {
			public static AndroidJavaClass instance_voicechannel;
			public static AndroidJavaObject instance_context;
			
			static SingletonHolder() {
				Debug.Log("init SingletonHolder");
				instance_voicechannel = new AndroidJavaClass("com.gotye.voicechannel.unity3d.VoiceChannelPlugin");
				Debug.Log("init instance_voicechannel " + instance_voicechannel);
				using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					instance_context = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
					Debug.Log("init instance_context " + instance_context);
				}
			}
		}
		
		private static AndroidJavaObject Context {
			get {
				return SingletonHolder.instance_context;
			}
		}
		
		private static AndroidJavaClass Plugin {
			get {
				return SingletonHolder.instance_voicechannel;
			}
		}
#endif

		/**
		 * 设置是否开启debug日志
		 * @param enable true开启日志，false关闭日志
		 */
		public static void SetDebugLog(bool enable) {
#if UNITY_ANDROID
			Plugin.CallStatic("setDebugLog", enable);
#endif

#if UNITY_IPHONE
			_SetDebugLog(enable);
#endif
		}

		/**
		 * 添加事件的回调
		 * @param obj 需要接收回调事件的GameObject的名字
		 */
		public static void AddCallback(string obj) {
#if UNITY_ANDROID
			Plugin.CallStatic("addCallback", obj);
#endif

#if UNITY_IPHONE
			_AddCallback(obj);
#endif
		}

		/**
		 * 移除事件的回调
		 * @param obj 接收回调事件的GameObject的名字
		 */
		public static void RemoveCallback(string obj) {
#if UNITY_ANDROID
			Plugin.CallStatic("removeCallback", obj);
#endif

#if UNITY_IPHONE
			_RemoveCallback(obj);
#endif
		}

		/**
		 * 移除所有事件的回调
		 */
		public static void RemoveAllCallback() {
#if UNITY_ANDROID
			Plugin.CallStatic("removeAllCallback");
#endif

#if UNITY_IPHONE
			_RemoveAllCallback();
#endif
		}
		
		/**
	 	 * 双向语音实例的初始化
	 	 * 
	 	 * @param appKey	在管理后台申请的appKey
	 	 */
		public static void Init(string appkey) {
#if UNITY_ANDROID
			Plugin.CallStatic("init", Context, appkey);
#endif

#if UNITY_IPHONE
			_Init(appkey);
#endif
		}
		
		/**
	 	 * 设置用户的登录信息
	 	 * @param userID 用户的登录信息
	 	 * @param nickname 用户的昵称
	 	 */
		public static void SetLoginInfo(string userID, string nickname, string password) {
#if UNITY_ANDROID
			Plugin.CallStatic("setLoginInfo", userID, nickname, password);
#endif

#if UNITY_IPHONE
			_SetLoginInfo(userID, nickname, password);
#endif
		}

		/**
		 *  获取用户昵称
		 *
		 *  @param userIdArray 需要获取昵称的用户id数组
	 	 */
		public static void RequestUserNickname(string[] userIDArray, int length) {
#if UNITY_ANDROID
//			Plugin.CallStatic("requestUserNickname", userIDArray);
			IntPtr jAryPtr = AndroidJNIHelper.ConvertToJNIArray(userIDArray);
			jvalue[] blah = new jvalue[1];
			blah[0].l = jAryPtr;
			
			IntPtr methodId = AndroidJNIHelper.GetMethodID(Plugin.GetRawClass(), "requestUserNickname", "([Ljava/lang/String;)V", true);
			AndroidJNI.CallStaticVoidMethod(Plugin.GetRawClass(), methodId, blah);
#endif
			
#if UNITY_IPHONE
			_RequestUserNickname(userIDArray, length);
#endif
		}

		/**
		 * 退出VoiceChannel Server
	 	 */
		public static void Exit() {
#if UNITY_ANDROID
			Plugin.CallStatic("exit");
#endif

#if UNITY_IPHONE
			_Exit_();
#endif
		}
		
		/**
	 	 * 登录频道
	 	 * 
	 	 * @param channelInfo   通过服务器接口创建频道返回的频道信息
	 	 */
		public static void JoinChannel(string channelInfo, string password) {
#if UNITY_ANDROID
			Plugin.CallStatic("joinChannel", channelInfo, password);
#endif

#if UNITY_IPHONE
			_JoinChannel(channelInfo, password);
#endif
		}
		
		/**
	 	 * 退出当前频道
	 	 */
		public static void ExitChannel() {
#if UNITY_ANDROID
			Plugin.CallStatic("exitChannel");
#endif

#if UNITY_IPHONE
			_ExitChannel();
#endif
		}
		
		/**
	 	 * 将某成员踢出当前频道
		 * 
	 	 * @param userID  被踢成员userID
	 	 */
		public static void KickMember(string userID) {
#if UNITY_ANDROID
			Plugin.CallStatic("kickMember", userID);
#endif

#if UNITY_IPHONE
			_KickMember(userID);
#endif
		}
		
		/**
	 	 * 开始说话
	 	 */
		public static void StartTalking() {
#if UNITY_ANDROID
			Plugin.CallStatic("startTalking");
#endif

#if UNITY_IPHONE
			_StartTalking();
#endif
		}
		
		/**
	 	 * 停止说话
	 	 */
		public static void StopTalking() {
#if UNITY_ANDROID
			Plugin.CallStatic("stopTalking");
#endif

#if UNITY_IPHONE
			_StopTalking();
#endif
		}
		
		/**
	 	 * 恢复他人的麦克风（也可对自己操作）
	 	 * 
	 	 * @param userID    被操作频道成员的userID
	 	 */
		public static void Unsilence(string userID) {
#if UNITY_ANDROID
			Plugin.CallStatic("unsilence", userID);
#endif

#if UNITY_IPHONE
			_Unsilence(userID);
#endif
		}
		
		/**
	 	 * 禁用他人的麦克风（也可对自己操作）
	 	 * 
		 * @param userID    被操作频道成员的userid
		 */
		public static void Silence(string userID) {
#if UNITY_ANDROID
			Plugin.CallStatic("silence", userID);
#endif

#if UNITY_IPHONE
			_Silence(userID);
#endif
		}
		
		/**
	 	 * 禁用自己的麦克风和扬声器
	 	 */
		public static void Mute() {
#if UNITY_ANDROID
			Plugin.CallStatic("mute");
#endif

#if UNITY_IPHONE
			_Mute();
#endif
		}
		
		/**
	 	 * 恢复自己的麦克风和扬声器
	 	 */
		public static void Restore() {
#if UNITY_ANDROID
			Plugin.CallStatic("restore");
#endif

#if UNITY_IPHONE
			_Restore();
#endif
		}
		
		/**
	 	 * 将某成员提升为管理员
	 	 * 
	 	 * @param userID    被操作频道成员的userID
	 	 */
		public static void Elevate(string userID) {
#if UNITY_ANDROID
			Plugin.CallStatic("elevate");
#endif

#if UNITY_IPHONE
			_Elevate(userID);
#endif
		}
		
		/**
		 * 将某管理员降为普通用户
	 	 * 
	 	 * @param userID   userID
	 	 */
		public static void Demote(string userID){
#if UNITY_ANDROID
			Plugin.CallStatic("demote", userID);
#endif

#if UNITY_IPHONE
			_Demote(userID);
#endif
		}
		
		/**
	 	 * 设置频道的发言模式
	 	 * 
	 	 * @param talkmode	频道的发言模式
	 	 */
		public static void SetChannelTalkMode(TalkMode talkmode){
#if UNITY_ANDROID
			Plugin.CallStatic("setChannelTalkMode", (int)talkmode);
#endif

#if UNITY_IPHONE
			_SetChannelTalkMode((int)talkmode);
#endif
		}

		/**
	 	 * 获取当前已发送的语音流量。在调用startTalking时开始统计(计数清零)，调用stopTalking时结束统计。
	 	 * 
	 	 * @return 已发送的语音流量，单位byte
	 	 */ 
		public static long GetCurrentVoiceTrafficSend() {
#if UNITY_ANDROID
			return Plugin.CallStatic<long>("getCurrentVoiceTrafficSend");
#endif
			
#if UNITY_IPHONE
			return _getCurrentVoiceTrafficSend();
#endif
			return 0l;
		}

		public static void RequestChannelDetail(string token) {
			#if UNITY_ANDROID
				Plugin.CallStatic("requestChannelDetail", token);
			#endif
			
			#if UNITY_IPHONE
				_RequestChannelDetail(token);
			#endif
		}
	}
}