using UnityEngine;
using System.Collections;


public enum TalkMode {
		/**
		 * 自由发言模式
		 */
	Freedom = 0,
		
		/**
	 	 * 管理员发言模式
	 	 */
	AdministratorOnly = 1
}

public enum ErrorType {
	
	/**
	 * 网络断开
	 */
	ErrorNetworkInvalid = 0,
	
	/**
	 * APP不存在
	 */
	ErrorAppNotExsit = 1,
	
	/**
	  * 用户不存在
	  */
	ErrorUserNotExsit = 2,
	
	/**
	  * 无效的用户ID
	  */
	ErrorInvalidUserID = 3,
	
	/**
	  * 用户ID已在用
	  */
	ErrorUserIDInUse = 4,
	
	/**
	  * 频道成员已满
	  */
	ErrorChannelIsFull = 5,
	
	/**
	  * Server中用户数量已达上限
	  */
	ErrorServerIsFull = 6,
	
	/**
	  * 权限不够
	  */
	ErrorPermissionDenial = 7,

	/**
	  * 频道不存在
	  */
	ErrorChannelIsNotExist = 8,
	
	/**
	  * 用户密码错误
	  */
	ErrorWrongUserPassword = 9,

	/**
	  * 未知的异常
	  */
	ErrorUnkown = 10
}

public enum MemberType {
	
	/**
	 * 会长
	 */
	President = 0,
	
	/**
	 * 管理员
	 */
	Administrator = 1,
	
	/**
     * 普通用户
     */
	Common = 2              
}
