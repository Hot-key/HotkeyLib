﻿#Include HotkeyLib/HotkeyLib_Core.ahk
#NoEnv
HotkeyLib_Load()

user_id := "id12456"
user_pw := "pw12340" 

len := HotkeyLib.Auth.Login(user_id, user_pw)

msgbox,% len

token := HotkeyLib.Auth.CheckToken()

msgbox,% token

HotkeyLib_UnLoad()
ExitApp