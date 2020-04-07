#NoEnv
#Persistent

; HotkeyLib_Core..ahk
;
; The MIT License (MIT)
; Copyright (c) 2020 Hotkey
; https://raw.githubusercontent.com/Hot-key/HotkeyLib/master/LICENSE
;
;=========================구현된 함수들=========================
;---------------------------기본 함수---------------------------
; HotkeyLib_Load() - 함수를 사용하기 위한 dll 파일을 로드합니다.
; HotkeyLib_Login() - 일부 함수 사용을 위한 서버에 접속합니다.
; HotkeyLib_CheckToken() - 서버와의 통신에 사용하는 토큰을 확인합니다.
;==============================================================
;==============================================================

; 함수 설명
;==============================================================
; 함수명 - 함수의 이름입니다
; 설명 - 함수의 설명입니다.
; 사용예시 - 간단한 사용 예시 입니다.
; 인자 설명 - 함수에 필요한 인자 설명입니다.
; 반환값 - 함수의 반환값에 대한 설명입니다.
;          반환값이 문자열이면 쌍따음표를 이용하여 묶습니다.
;==============================================================

;==============================================================
; 함수명 - HotkeyLib_Load
; 설명 - 함수를 사용하기 위한 dll 파일을 로드합니다.
; 사용예시 - HotkeyLib_Load()
; 인자 설명
; 반환값
;==============================================================
HotkeyLib_Load()
{
	if !DllCall("GetModuleHandle", Str, "HotkeyLib/HotkeyLib.dll")
	{
		DllCall("LoadLibrary", Str, "HotkeyLib/HotkeyLib.dll")
	}
}

class HotkeyLib
{
	static path := "HotkeyLib\HotkeyLib_"
	class Auth
	{
		static path := HotkeyLib.path "Auth_"
		;==============================================================
		; 함수명 - Login
		; 설명 - 일부 함수 사용을 위한 서버에 접속합니다.
		; 사용예시 - HotkeyLib.Auth.Login(user_id, user_pw)
		; 인자 설명
		;			user_id - 아이디 값 입니다.
		;			user_pw - 비밀번호 값 입니다.
		; 반환값 - 로그인 성공 여부를 반환합니다.
		;			-1 - 실패	
		;			0 - 오류		
		;			1 - 성공
		;==============================================================
		Login(user_id, user_pw)
		{
			result := DllCall(this.path "Login", AStr, user_id, AStr, user_pw, Int)
			return result
		}
		
		;==============================================================
		; 함수명 - CheckLogin
		; 설명 - 로그인 여부를 확인합니다.
		; 사용예시 - isLogin := HotkeyLib.Auth.CheckLogin()
		; 인자 설명
		; 반환값 - 로그인 상태를 반환합니다.
		;			0 - 로그인 토큰오류 및 로그인 안함
		;			1 - 로그인 성공
		;==============================================================
		CheckLogin()
		{
			result := DllCall(this.path "CheckLogin", Int)
			return result
		}

		;==============================================================
		; 함수명 - CheckToken
		; 설명 - 서버와의 통신에 사용하는 토큰을 확인합니다.
		;		 주로 디버깅 용도로 사용합니다.
		; 사용예시 - token := HotkeyLib.Auth.CheckToken()
		; 인자 설명
		; 반환값 - 사용중인 토큰 값을 반환합니다.
		;			"-1" - 로그인 오류 및 토큰 없음	
		;			"{Token}" - 토큰 값
		;==============================================================
		CheckToken()
		{
			result := DllCall(this.path "CheckToken", AStr)
			return result
		}
	}
	class Window
	{
		static path := HotkeyLib.path "Window_"
		
		FindWindow(className, windowName)
		{
			result := DllCall(this.path "FindWindow",AStr, className, AStr, windowName "Int*")
			return result
		}
		
		FindWindowEx(hWnd1, className, windowName)
		{
			result := DllCall(this.path "FindWindowEx", "Int*", hWnd1, AStr, className, AStr, windowName, "Int*")
			return result
		}
	}
	
	class Keyboard
	{
		static path := HotkeyLib.path "Keyboard_"
		
		Click(hWnd1, x, y, delay = 0)
		{
			DllCall(this.path "Click", "Int*", hWnd1, Int, x, Int, y, Int, delay)
		}
	}
}


HotkeyLib_UnLoad()
{
	if moduleHandle := DllCall("GetModuleHandle", Str, "HotkeyLib.dll")
	{
		DllCall("FreeLibrary", Int, moduleHandle)
	}
}