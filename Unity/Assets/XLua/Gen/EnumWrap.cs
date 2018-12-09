﻿#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class UnityEngineLightTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.LightType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.LightType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.LightType), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Spot", UnityEngine.LightType.Spot);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Directional", UnityEngine.LightType.Directional);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Point", UnityEngine.LightType.Point);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Area", UnityEngine.LightType.Area);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.LightType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineLightType(L, (UnityEngine.LightType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Spot"))
                {
                    translator.PushUnityEngineLightType(L, UnityEngine.LightType.Spot);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Directional"))
                {
                    translator.PushUnityEngineLightType(L, UnityEngine.LightType.Directional);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Point"))
                {
                    translator.PushUnityEngineLightType(L, UnityEngine.LightType.Point);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Area"))
                {
                    translator.PushUnityEngineLightType(L, UnityEngine.LightType.Area);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.LightType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.LightType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineCameraClearFlagsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.CameraClearFlags), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.CameraClearFlags), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.CameraClearFlags), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Skybox", UnityEngine.CameraClearFlags.Skybox);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Color", UnityEngine.CameraClearFlags.Color);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SolidColor", UnityEngine.CameraClearFlags.SolidColor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Depth", UnityEngine.CameraClearFlags.Depth);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Nothing", UnityEngine.CameraClearFlags.Nothing);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.CameraClearFlags), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineCameraClearFlags(L, (UnityEngine.CameraClearFlags)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Skybox"))
                {
                    translator.PushUnityEngineCameraClearFlags(L, UnityEngine.CameraClearFlags.Skybox);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Color"))
                {
                    translator.PushUnityEngineCameraClearFlags(L, UnityEngine.CameraClearFlags.Color);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SolidColor"))
                {
                    translator.PushUnityEngineCameraClearFlags(L, UnityEngine.CameraClearFlags.SolidColor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Depth"))
                {
                    translator.PushUnityEngineCameraClearFlags(L, UnityEngine.CameraClearFlags.Depth);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Nothing"))
                {
                    translator.PushUnityEngineCameraClearFlags(L, UnityEngine.CameraClearFlags.Nothing);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.CameraClearFlags!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.CameraClearFlags! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineKeyCodeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.KeyCode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.KeyCode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.KeyCode), L, null, 322, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.KeyCode.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Backspace", UnityEngine.KeyCode.Backspace);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Delete", UnityEngine.KeyCode.Delete);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tab", UnityEngine.KeyCode.Tab);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Clear", UnityEngine.KeyCode.Clear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Return", UnityEngine.KeyCode.Return);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Pause", UnityEngine.KeyCode.Pause);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Escape", UnityEngine.KeyCode.Escape);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Space", UnityEngine.KeyCode.Space);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad0", UnityEngine.KeyCode.Keypad0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad1", UnityEngine.KeyCode.Keypad1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad2", UnityEngine.KeyCode.Keypad2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad3", UnityEngine.KeyCode.Keypad3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad4", UnityEngine.KeyCode.Keypad4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad5", UnityEngine.KeyCode.Keypad5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad6", UnityEngine.KeyCode.Keypad6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad7", UnityEngine.KeyCode.Keypad7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad8", UnityEngine.KeyCode.Keypad8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Keypad9", UnityEngine.KeyCode.Keypad9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadPeriod", UnityEngine.KeyCode.KeypadPeriod);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadDivide", UnityEngine.KeyCode.KeypadDivide);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadMultiply", UnityEngine.KeyCode.KeypadMultiply);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadMinus", UnityEngine.KeyCode.KeypadMinus);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadPlus", UnityEngine.KeyCode.KeypadPlus);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadEnter", UnityEngine.KeyCode.KeypadEnter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeypadEquals", UnityEngine.KeyCode.KeypadEquals);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpArrow", UnityEngine.KeyCode.UpArrow);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DownArrow", UnityEngine.KeyCode.DownArrow);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightArrow", UnityEngine.KeyCode.RightArrow);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftArrow", UnityEngine.KeyCode.LeftArrow);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Insert", UnityEngine.KeyCode.Insert);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Home", UnityEngine.KeyCode.Home);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "End", UnityEngine.KeyCode.End);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PageUp", UnityEngine.KeyCode.PageUp);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PageDown", UnityEngine.KeyCode.PageDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F1", UnityEngine.KeyCode.F1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F2", UnityEngine.KeyCode.F2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F3", UnityEngine.KeyCode.F3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F4", UnityEngine.KeyCode.F4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F5", UnityEngine.KeyCode.F5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F6", UnityEngine.KeyCode.F6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F7", UnityEngine.KeyCode.F7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F8", UnityEngine.KeyCode.F8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F9", UnityEngine.KeyCode.F9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F10", UnityEngine.KeyCode.F10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F11", UnityEngine.KeyCode.F11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F12", UnityEngine.KeyCode.F12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F13", UnityEngine.KeyCode.F13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F14", UnityEngine.KeyCode.F14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F15", UnityEngine.KeyCode.F15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha0", UnityEngine.KeyCode.Alpha0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha1", UnityEngine.KeyCode.Alpha1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha2", UnityEngine.KeyCode.Alpha2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha3", UnityEngine.KeyCode.Alpha3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha4", UnityEngine.KeyCode.Alpha4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha5", UnityEngine.KeyCode.Alpha5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha6", UnityEngine.KeyCode.Alpha6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha7", UnityEngine.KeyCode.Alpha7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha8", UnityEngine.KeyCode.Alpha8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha9", UnityEngine.KeyCode.Alpha9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Exclaim", UnityEngine.KeyCode.Exclaim);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DoubleQuote", UnityEngine.KeyCode.DoubleQuote);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Hash", UnityEngine.KeyCode.Hash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Dollar", UnityEngine.KeyCode.Dollar);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ampersand", UnityEngine.KeyCode.Ampersand);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Quote", UnityEngine.KeyCode.Quote);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftParen", UnityEngine.KeyCode.LeftParen);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightParen", UnityEngine.KeyCode.RightParen);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Asterisk", UnityEngine.KeyCode.Asterisk);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Plus", UnityEngine.KeyCode.Plus);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Comma", UnityEngine.KeyCode.Comma);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Minus", UnityEngine.KeyCode.Minus);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Period", UnityEngine.KeyCode.Period);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Slash", UnityEngine.KeyCode.Slash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Colon", UnityEngine.KeyCode.Colon);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Semicolon", UnityEngine.KeyCode.Semicolon);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Less", UnityEngine.KeyCode.Less);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Equals", UnityEngine.KeyCode.Equals);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Greater", UnityEngine.KeyCode.Greater);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Question", UnityEngine.KeyCode.Question);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "At", UnityEngine.KeyCode.At);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftBracket", UnityEngine.KeyCode.LeftBracket);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Backslash", UnityEngine.KeyCode.Backslash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightBracket", UnityEngine.KeyCode.RightBracket);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Caret", UnityEngine.KeyCode.Caret);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Underscore", UnityEngine.KeyCode.Underscore);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackQuote", UnityEngine.KeyCode.BackQuote);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "A", UnityEngine.KeyCode.A);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "B", UnityEngine.KeyCode.B);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "C", UnityEngine.KeyCode.C);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "D", UnityEngine.KeyCode.D);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E", UnityEngine.KeyCode.E);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "F", UnityEngine.KeyCode.F);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "G", UnityEngine.KeyCode.G);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "H", UnityEngine.KeyCode.H);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "I", UnityEngine.KeyCode.I);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "J", UnityEngine.KeyCode.J);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "K", UnityEngine.KeyCode.K);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "L", UnityEngine.KeyCode.L);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "M", UnityEngine.KeyCode.M);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "N", UnityEngine.KeyCode.N);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "O", UnityEngine.KeyCode.O);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "P", UnityEngine.KeyCode.P);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Q", UnityEngine.KeyCode.Q);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "R", UnityEngine.KeyCode.R);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "S", UnityEngine.KeyCode.S);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "T", UnityEngine.KeyCode.T);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "U", UnityEngine.KeyCode.U);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "V", UnityEngine.KeyCode.V);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "W", UnityEngine.KeyCode.W);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "X", UnityEngine.KeyCode.X);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Y", UnityEngine.KeyCode.Y);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Z", UnityEngine.KeyCode.Z);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Numlock", UnityEngine.KeyCode.Numlock);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CapsLock", UnityEngine.KeyCode.CapsLock);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScrollLock", UnityEngine.KeyCode.ScrollLock);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightShift", UnityEngine.KeyCode.RightShift);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftShift", UnityEngine.KeyCode.LeftShift);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightControl", UnityEngine.KeyCode.RightControl);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftControl", UnityEngine.KeyCode.LeftControl);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightAlt", UnityEngine.KeyCode.RightAlt);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftAlt", UnityEngine.KeyCode.LeftAlt);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftCommand", UnityEngine.KeyCode.LeftCommand);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftApple", UnityEngine.KeyCode.LeftApple);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftWindows", UnityEngine.KeyCode.LeftWindows);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightCommand", UnityEngine.KeyCode.RightCommand);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightApple", UnityEngine.KeyCode.RightApple);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightWindows", UnityEngine.KeyCode.RightWindows);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AltGr", UnityEngine.KeyCode.AltGr);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Help", UnityEngine.KeyCode.Help);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Print", UnityEngine.KeyCode.Print);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SysReq", UnityEngine.KeyCode.SysReq);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Break", UnityEngine.KeyCode.Break);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Menu", UnityEngine.KeyCode.Menu);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse0", UnityEngine.KeyCode.Mouse0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse1", UnityEngine.KeyCode.Mouse1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse2", UnityEngine.KeyCode.Mouse2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse3", UnityEngine.KeyCode.Mouse3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse4", UnityEngine.KeyCode.Mouse4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse5", UnityEngine.KeyCode.Mouse5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mouse6", UnityEngine.KeyCode.Mouse6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton0", UnityEngine.KeyCode.JoystickButton0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton1", UnityEngine.KeyCode.JoystickButton1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton2", UnityEngine.KeyCode.JoystickButton2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton3", UnityEngine.KeyCode.JoystickButton3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton4", UnityEngine.KeyCode.JoystickButton4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton5", UnityEngine.KeyCode.JoystickButton5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton6", UnityEngine.KeyCode.JoystickButton6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton7", UnityEngine.KeyCode.JoystickButton7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton8", UnityEngine.KeyCode.JoystickButton8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton9", UnityEngine.KeyCode.JoystickButton9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton10", UnityEngine.KeyCode.JoystickButton10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton11", UnityEngine.KeyCode.JoystickButton11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton12", UnityEngine.KeyCode.JoystickButton12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton13", UnityEngine.KeyCode.JoystickButton13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton14", UnityEngine.KeyCode.JoystickButton14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton15", UnityEngine.KeyCode.JoystickButton15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton16", UnityEngine.KeyCode.JoystickButton16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton17", UnityEngine.KeyCode.JoystickButton17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton18", UnityEngine.KeyCode.JoystickButton18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "JoystickButton19", UnityEngine.KeyCode.JoystickButton19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button0", UnityEngine.KeyCode.Joystick1Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button1", UnityEngine.KeyCode.Joystick1Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button2", UnityEngine.KeyCode.Joystick1Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button3", UnityEngine.KeyCode.Joystick1Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button4", UnityEngine.KeyCode.Joystick1Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button5", UnityEngine.KeyCode.Joystick1Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button6", UnityEngine.KeyCode.Joystick1Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button7", UnityEngine.KeyCode.Joystick1Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button8", UnityEngine.KeyCode.Joystick1Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button9", UnityEngine.KeyCode.Joystick1Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button10", UnityEngine.KeyCode.Joystick1Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button11", UnityEngine.KeyCode.Joystick1Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button12", UnityEngine.KeyCode.Joystick1Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button13", UnityEngine.KeyCode.Joystick1Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button14", UnityEngine.KeyCode.Joystick1Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button15", UnityEngine.KeyCode.Joystick1Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button16", UnityEngine.KeyCode.Joystick1Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button17", UnityEngine.KeyCode.Joystick1Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button18", UnityEngine.KeyCode.Joystick1Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick1Button19", UnityEngine.KeyCode.Joystick1Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button0", UnityEngine.KeyCode.Joystick2Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button1", UnityEngine.KeyCode.Joystick2Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button2", UnityEngine.KeyCode.Joystick2Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button3", UnityEngine.KeyCode.Joystick2Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button4", UnityEngine.KeyCode.Joystick2Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button5", UnityEngine.KeyCode.Joystick2Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button6", UnityEngine.KeyCode.Joystick2Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button7", UnityEngine.KeyCode.Joystick2Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button8", UnityEngine.KeyCode.Joystick2Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button9", UnityEngine.KeyCode.Joystick2Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button10", UnityEngine.KeyCode.Joystick2Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button11", UnityEngine.KeyCode.Joystick2Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button12", UnityEngine.KeyCode.Joystick2Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button13", UnityEngine.KeyCode.Joystick2Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button14", UnityEngine.KeyCode.Joystick2Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button15", UnityEngine.KeyCode.Joystick2Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button16", UnityEngine.KeyCode.Joystick2Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button17", UnityEngine.KeyCode.Joystick2Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button18", UnityEngine.KeyCode.Joystick2Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick2Button19", UnityEngine.KeyCode.Joystick2Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button0", UnityEngine.KeyCode.Joystick3Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button1", UnityEngine.KeyCode.Joystick3Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button2", UnityEngine.KeyCode.Joystick3Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button3", UnityEngine.KeyCode.Joystick3Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button4", UnityEngine.KeyCode.Joystick3Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button5", UnityEngine.KeyCode.Joystick3Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button6", UnityEngine.KeyCode.Joystick3Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button7", UnityEngine.KeyCode.Joystick3Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button8", UnityEngine.KeyCode.Joystick3Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button9", UnityEngine.KeyCode.Joystick3Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button10", UnityEngine.KeyCode.Joystick3Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button11", UnityEngine.KeyCode.Joystick3Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button12", UnityEngine.KeyCode.Joystick3Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button13", UnityEngine.KeyCode.Joystick3Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button14", UnityEngine.KeyCode.Joystick3Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button15", UnityEngine.KeyCode.Joystick3Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button16", UnityEngine.KeyCode.Joystick3Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button17", UnityEngine.KeyCode.Joystick3Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button18", UnityEngine.KeyCode.Joystick3Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick3Button19", UnityEngine.KeyCode.Joystick3Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button0", UnityEngine.KeyCode.Joystick4Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button1", UnityEngine.KeyCode.Joystick4Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button2", UnityEngine.KeyCode.Joystick4Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button3", UnityEngine.KeyCode.Joystick4Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button4", UnityEngine.KeyCode.Joystick4Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button5", UnityEngine.KeyCode.Joystick4Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button6", UnityEngine.KeyCode.Joystick4Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button7", UnityEngine.KeyCode.Joystick4Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button8", UnityEngine.KeyCode.Joystick4Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button9", UnityEngine.KeyCode.Joystick4Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button10", UnityEngine.KeyCode.Joystick4Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button11", UnityEngine.KeyCode.Joystick4Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button12", UnityEngine.KeyCode.Joystick4Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button13", UnityEngine.KeyCode.Joystick4Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button14", UnityEngine.KeyCode.Joystick4Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button15", UnityEngine.KeyCode.Joystick4Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button16", UnityEngine.KeyCode.Joystick4Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button17", UnityEngine.KeyCode.Joystick4Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button18", UnityEngine.KeyCode.Joystick4Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick4Button19", UnityEngine.KeyCode.Joystick4Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button0", UnityEngine.KeyCode.Joystick5Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button1", UnityEngine.KeyCode.Joystick5Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button2", UnityEngine.KeyCode.Joystick5Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button3", UnityEngine.KeyCode.Joystick5Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button4", UnityEngine.KeyCode.Joystick5Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button5", UnityEngine.KeyCode.Joystick5Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button6", UnityEngine.KeyCode.Joystick5Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button7", UnityEngine.KeyCode.Joystick5Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button8", UnityEngine.KeyCode.Joystick5Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button9", UnityEngine.KeyCode.Joystick5Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button10", UnityEngine.KeyCode.Joystick5Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button11", UnityEngine.KeyCode.Joystick5Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button12", UnityEngine.KeyCode.Joystick5Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button13", UnityEngine.KeyCode.Joystick5Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button14", UnityEngine.KeyCode.Joystick5Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button15", UnityEngine.KeyCode.Joystick5Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button16", UnityEngine.KeyCode.Joystick5Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button17", UnityEngine.KeyCode.Joystick5Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button18", UnityEngine.KeyCode.Joystick5Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick5Button19", UnityEngine.KeyCode.Joystick5Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button0", UnityEngine.KeyCode.Joystick6Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button1", UnityEngine.KeyCode.Joystick6Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button2", UnityEngine.KeyCode.Joystick6Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button3", UnityEngine.KeyCode.Joystick6Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button4", UnityEngine.KeyCode.Joystick6Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button5", UnityEngine.KeyCode.Joystick6Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button6", UnityEngine.KeyCode.Joystick6Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button7", UnityEngine.KeyCode.Joystick6Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button8", UnityEngine.KeyCode.Joystick6Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button9", UnityEngine.KeyCode.Joystick6Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button10", UnityEngine.KeyCode.Joystick6Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button11", UnityEngine.KeyCode.Joystick6Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button12", UnityEngine.KeyCode.Joystick6Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button13", UnityEngine.KeyCode.Joystick6Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button14", UnityEngine.KeyCode.Joystick6Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button15", UnityEngine.KeyCode.Joystick6Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button16", UnityEngine.KeyCode.Joystick6Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button17", UnityEngine.KeyCode.Joystick6Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button18", UnityEngine.KeyCode.Joystick6Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick6Button19", UnityEngine.KeyCode.Joystick6Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button0", UnityEngine.KeyCode.Joystick7Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button1", UnityEngine.KeyCode.Joystick7Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button2", UnityEngine.KeyCode.Joystick7Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button3", UnityEngine.KeyCode.Joystick7Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button4", UnityEngine.KeyCode.Joystick7Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button5", UnityEngine.KeyCode.Joystick7Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button6", UnityEngine.KeyCode.Joystick7Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button7", UnityEngine.KeyCode.Joystick7Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button8", UnityEngine.KeyCode.Joystick7Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button9", UnityEngine.KeyCode.Joystick7Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button10", UnityEngine.KeyCode.Joystick7Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button11", UnityEngine.KeyCode.Joystick7Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button12", UnityEngine.KeyCode.Joystick7Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button13", UnityEngine.KeyCode.Joystick7Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button14", UnityEngine.KeyCode.Joystick7Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button15", UnityEngine.KeyCode.Joystick7Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button16", UnityEngine.KeyCode.Joystick7Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button17", UnityEngine.KeyCode.Joystick7Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button18", UnityEngine.KeyCode.Joystick7Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick7Button19", UnityEngine.KeyCode.Joystick7Button19);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button0", UnityEngine.KeyCode.Joystick8Button0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button1", UnityEngine.KeyCode.Joystick8Button1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button2", UnityEngine.KeyCode.Joystick8Button2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button3", UnityEngine.KeyCode.Joystick8Button3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button4", UnityEngine.KeyCode.Joystick8Button4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button5", UnityEngine.KeyCode.Joystick8Button5);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button6", UnityEngine.KeyCode.Joystick8Button6);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button7", UnityEngine.KeyCode.Joystick8Button7);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button8", UnityEngine.KeyCode.Joystick8Button8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button9", UnityEngine.KeyCode.Joystick8Button9);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button10", UnityEngine.KeyCode.Joystick8Button10);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button11", UnityEngine.KeyCode.Joystick8Button11);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button12", UnityEngine.KeyCode.Joystick8Button12);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button13", UnityEngine.KeyCode.Joystick8Button13);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button14", UnityEngine.KeyCode.Joystick8Button14);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button15", UnityEngine.KeyCode.Joystick8Button15);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button16", UnityEngine.KeyCode.Joystick8Button16);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button17", UnityEngine.KeyCode.Joystick8Button17);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button18", UnityEngine.KeyCode.Joystick8Button18);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Joystick8Button19", UnityEngine.KeyCode.Joystick8Button19);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.KeyCode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineKeyCode(L, (UnityEngine.KeyCode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Backspace"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Backspace);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Delete"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Delete);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Tab"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Tab);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Clear"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Clear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Return"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Return);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Pause"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Pause);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Escape"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Escape);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Space"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Space);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Keypad9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Keypad9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadPeriod"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadPeriod);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadDivide"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadDivide);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadMultiply"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadMultiply);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadMinus"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadMinus);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadPlus"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadPlus);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadEnter"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadEnter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeypadEquals"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.KeypadEquals);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpArrow"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.UpArrow);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DownArrow"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.DownArrow);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightArrow"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightArrow);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftArrow"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftArrow);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Insert"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Insert);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Home"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Home);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "End"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.End);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PageUp"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.PageUp);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PageDown"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.PageDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Alpha9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Exclaim"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Exclaim);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DoubleQuote"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.DoubleQuote);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Hash"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Hash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Dollar"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Dollar);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Ampersand"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Ampersand);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Quote"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Quote);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftParen"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftParen);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightParen"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightParen);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Asterisk"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Asterisk);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Plus"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Plus);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Comma"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Comma);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Minus"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Minus);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Period"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Period);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Slash"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Slash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Colon"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Colon);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Semicolon"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Semicolon);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Less"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Less);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Equals"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Equals);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Greater"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Greater);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Question"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Question);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "At"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.At);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftBracket"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftBracket);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Backslash"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Backslash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightBracket"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightBracket);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Caret"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Caret);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Underscore"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Underscore);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackQuote"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.BackQuote);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "A"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.A);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "B"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.B);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "C"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.C);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "D"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.D);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.E);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "F"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.F);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "G"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.G);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "H"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.H);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "I"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.I);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "J"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.J);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "K"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.K);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "L"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.L);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "M"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.M);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "N"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.N);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "O"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.O);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "P"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.P);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Q"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Q);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "R"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.R);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "S"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.S);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "T"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.T);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "U"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.U);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "V"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.V);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "W"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.W);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "X"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.X);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Y"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Y);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Z"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Z);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Numlock"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Numlock);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CapsLock"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.CapsLock);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScrollLock"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.ScrollLock);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightShift"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightShift);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftShift"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftShift);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightControl"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightControl);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftControl"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftControl);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightAlt"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightAlt);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftAlt"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftAlt);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftCommand"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftCommand);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftApple"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftApple);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftWindows"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.LeftWindows);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightCommand"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightCommand);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightApple"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightApple);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightWindows"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.RightWindows);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AltGr"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.AltGr);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Help"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Help);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Print"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Print);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SysReq"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.SysReq);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Break"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Break);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Menu"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Menu);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mouse6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Mouse6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "JoystickButton19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.JoystickButton19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick1Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick1Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick2Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick2Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick3Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick3Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick4Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick4Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick5Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick5Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick6Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick6Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick7Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick7Button19);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button0"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button0);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button1"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button2"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button3"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button4"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button5"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button5);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button6"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button6);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button7"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button7);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button8"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button9"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button9);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button10"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button10);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button11"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button11);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button12"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button12);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button13"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button13);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button14"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button14);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button15"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button15);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button16"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button16);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button17"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button17);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button18"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button18);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Joystick8Button19"))
                {
                    translator.PushUnityEngineKeyCode(L, UnityEngine.KeyCode.Joystick8Button19);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.KeyCode!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.KeyCode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineSpaceWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.Space), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.Space), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.Space), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "World", UnityEngine.Space.World);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Self", UnityEngine.Space.Self);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.Space), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineSpace(L, (UnityEngine.Space)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "World"))
                {
                    translator.PushUnityEngineSpace(L, UnityEngine.Space.World);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Self"))
                {
                    translator.PushUnityEngineSpace(L, UnityEngine.Space.Self);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.Space!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.Space! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAnimationBlendModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AnimationBlendMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AnimationBlendMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AnimationBlendMode), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Blend", UnityEngine.AnimationBlendMode.Blend);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Additive", UnityEngine.AnimationBlendMode.Additive);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AnimationBlendMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAnimationBlendMode(L, (UnityEngine.AnimationBlendMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Blend"))
                {
                    translator.PushUnityEngineAnimationBlendMode(L, UnityEngine.AnimationBlendMode.Blend);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Additive"))
                {
                    translator.PushUnityEngineAnimationBlendMode(L, UnityEngine.AnimationBlendMode.Additive);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AnimationBlendMode!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AnimationBlendMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineQueueModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.QueueMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.QueueMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.QueueMode), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CompleteOthers", UnityEngine.QueueMode.CompleteOthers);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PlayNow", UnityEngine.QueueMode.PlayNow);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.QueueMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineQueueMode(L, (UnityEngine.QueueMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "CompleteOthers"))
                {
                    translator.PushUnityEngineQueueMode(L, UnityEngine.QueueMode.CompleteOthers);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PlayNow"))
                {
                    translator.PushUnityEngineQueueMode(L, UnityEngine.QueueMode.PlayNow);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.QueueMode!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.QueueMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEnginePlayModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.PlayMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.PlayMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.PlayMode), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "StopSameLayer", UnityEngine.PlayMode.StopSameLayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "StopAll", UnityEngine.PlayMode.StopAll);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.PlayMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEnginePlayMode(L, (UnityEngine.PlayMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "StopSameLayer"))
                {
                    translator.PushUnityEnginePlayMode(L, UnityEngine.PlayMode.StopSameLayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "StopAll"))
                {
                    translator.PushUnityEnginePlayMode(L, UnityEngine.PlayMode.StopAll);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.PlayMode!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.PlayMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineWrapModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.WrapMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.WrapMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.WrapMode), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Once", UnityEngine.WrapMode.Once);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Loop", UnityEngine.WrapMode.Loop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PingPong", UnityEngine.WrapMode.PingPong);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", UnityEngine.WrapMode.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ClampForever", UnityEngine.WrapMode.ClampForever);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Clamp", UnityEngine.WrapMode.Clamp);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.WrapMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineWrapMode(L, (UnityEngine.WrapMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Once"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Once);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Loop"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Loop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PingPong"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.PingPong);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ClampForever"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.ClampForever);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Clamp"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Clamp);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.WrapMode!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.WrapMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineBlendWeightsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.BlendWeights), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.BlendWeights), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.BlendWeights), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OneBone", UnityEngine.BlendWeights.OneBone);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TwoBones", UnityEngine.BlendWeights.TwoBones);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FourBones", UnityEngine.BlendWeights.FourBones);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.BlendWeights), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineBlendWeights(L, (UnityEngine.BlendWeights)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "OneBone"))
                {
                    translator.PushUnityEngineBlendWeights(L, UnityEngine.BlendWeights.OneBone);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TwoBones"))
                {
                    translator.PushUnityEngineBlendWeights(L, UnityEngine.BlendWeights.TwoBones);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FourBones"))
                {
                    translator.PushUnityEngineBlendWeights(L, UnityEngine.BlendWeights.FourBones);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.BlendWeights!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.BlendWeights! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRuntimePlatformWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RuntimePlatform), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RuntimePlatform), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RuntimePlatform), L, null, 35, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OSXEditor", UnityEngine.RuntimePlatform.OSXEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OSXPlayer", UnityEngine.RuntimePlatform.OSXPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WindowsPlayer", UnityEngine.RuntimePlatform.WindowsPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WindowsEditor", UnityEngine.RuntimePlatform.WindowsEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IPhonePlayer", UnityEngine.RuntimePlatform.IPhonePlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Android", UnityEngine.RuntimePlatform.Android);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinuxPlayer", UnityEngine.RuntimePlatform.LinuxPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinuxEditor", UnityEngine.RuntimePlatform.LinuxEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WebGLPlayer", UnityEngine.RuntimePlatform.WebGLPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerX86", UnityEngine.RuntimePlatform.WSAPlayerX86);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerX64", UnityEngine.RuntimePlatform.WSAPlayerX64);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerARM", UnityEngine.RuntimePlatform.WSAPlayerARM);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PSP2", UnityEngine.RuntimePlatform.PSP2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PS4", UnityEngine.RuntimePlatform.PS4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "XboxOne", UnityEngine.RuntimePlatform.XboxOne);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "tvOS", UnityEngine.RuntimePlatform.tvOS);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Switch", UnityEngine.RuntimePlatform.Switch);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RuntimePlatform), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRuntimePlatform(L, (UnityEngine.RuntimePlatform)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "OSXEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.OSXEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OSXPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.OSXPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WindowsPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WindowsPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WindowsEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WindowsEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IPhonePlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.IPhonePlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Android"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Android);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinuxPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.LinuxPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinuxEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.LinuxEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WebGLPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WebGLPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerX86"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerX86);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerX64"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerX64);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerARM"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerARM);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PSP2"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.PSP2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PS4"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.PS4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "XboxOne"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.XboxOne);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "tvOS"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.tvOS);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Switch"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Switch);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RuntimePlatform!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RuntimePlatform! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineEventSystemsEventTriggerTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, null, 18, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerEnter", UnityEngine.EventSystems.EventTriggerType.PointerEnter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerExit", UnityEngine.EventSystems.EventTriggerType.PointerExit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerDown", UnityEngine.EventSystems.EventTriggerType.PointerDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerUp", UnityEngine.EventSystems.EventTriggerType.PointerUp);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerClick", UnityEngine.EventSystems.EventTriggerType.PointerClick);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Drag", UnityEngine.EventSystems.EventTriggerType.Drag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Drop", UnityEngine.EventSystems.EventTriggerType.Drop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Scroll", UnityEngine.EventSystems.EventTriggerType.Scroll);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpdateSelected", UnityEngine.EventSystems.EventTriggerType.UpdateSelected);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Select", UnityEngine.EventSystems.EventTriggerType.Select);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Deselect", UnityEngine.EventSystems.EventTriggerType.Deselect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Move", UnityEngine.EventSystems.EventTriggerType.Move);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InitializePotentialDrag", UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BeginDrag", UnityEngine.EventSystems.EventTriggerType.BeginDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "EndDrag", UnityEngine.EventSystems.EventTriggerType.EndDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Submit", UnityEngine.EventSystems.EventTriggerType.Submit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Cancel", UnityEngine.EventSystems.EventTriggerType.Cancel);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineEventSystemsEventTriggerType(L, (UnityEngine.EventSystems.EventTriggerType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "PointerEnter"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerEnter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerExit"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerExit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerDown"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerUp"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerUp);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerClick"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerClick);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Drag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Drag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Drop"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Drop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Scroll"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Scroll);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpdateSelected"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.UpdateSelected);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Select"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Select);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Deselect"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Deselect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Move"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Move);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InitializePotentialDrag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BeginDrag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.BeginDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "EndDrag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.EndDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Submit"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Submit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Cancel"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Cancel);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.EventSystems.EventTriggerType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.EventSystems.EventTriggerType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class GameResourceLoadTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Game.ResourceLoadType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Game.ResourceLoadType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Game.ResourceLoadType), L, null, 9, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", Game.ResourceLoadType.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Persistent", Game.ResourceLoadType.Persistent);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Cache", Game.ResourceLoadType.Cache);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnLoad", Game.ResourceLoadType.UnLoad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Immediate", Game.ResourceLoadType.Immediate);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LoadBundleFromFile", Game.ResourceLoadType.LoadBundleFromFile);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LoadBundleFromWWW", Game.ResourceLoadType.LoadBundleFromWWW);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ReturnAssetBundle", Game.ResourceLoadType.ReturnAssetBundle);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Game.ResourceLoadType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushGameResourceLoadType(L, (Game.ResourceLoadType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Persistent"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.Persistent);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Cache"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.Cache);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UnLoad"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.UnLoad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Immediate"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.Immediate);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LoadBundleFromFile"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.LoadBundleFromFile);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LoadBundleFromWWW"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.LoadBundleFromWWW);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ReturnAssetBundle"))
                {
                    translator.PushGameResourceLoadType(L, Game.ResourceLoadType.ReturnAssetBundle);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Game.ResourceLoadType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Game.ResourceLoadType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class FairyGUIRelationTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(FairyGUI.RelationType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(FairyGUI.RelationType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(FairyGUI.RelationType), L, null, 26, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left_Left", FairyGUI.RelationType.Left_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left_Center", FairyGUI.RelationType.Left_Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left_Right", FairyGUI.RelationType.Left_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Center_Center", FairyGUI.RelationType.Center_Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right_Left", FairyGUI.RelationType.Right_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right_Center", FairyGUI.RelationType.Right_Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right_Right", FairyGUI.RelationType.Right_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top_Top", FairyGUI.RelationType.Top_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top_Middle", FairyGUI.RelationType.Top_Middle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top_Bottom", FairyGUI.RelationType.Top_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Middle_Middle", FairyGUI.RelationType.Middle_Middle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom_Top", FairyGUI.RelationType.Bottom_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom_Middle", FairyGUI.RelationType.Bottom_Middle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom_Bottom", FairyGUI.RelationType.Bottom_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Width", FairyGUI.RelationType.Width);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Height", FairyGUI.RelationType.Height);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftExt_Left", FairyGUI.RelationType.LeftExt_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftExt_Right", FairyGUI.RelationType.LeftExt_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightExt_Left", FairyGUI.RelationType.RightExt_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightExt_Right", FairyGUI.RelationType.RightExt_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopExt_Top", FairyGUI.RelationType.TopExt_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopExt_Bottom", FairyGUI.RelationType.TopExt_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomExt_Top", FairyGUI.RelationType.BottomExt_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomExt_Bottom", FairyGUI.RelationType.BottomExt_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Size", FairyGUI.RelationType.Size);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(FairyGUI.RelationType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushFairyGUIRelationType(L, (FairyGUI.RelationType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Left_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Left_Center"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Left_Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Left_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Left_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Center_Center"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Center_Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Right_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right_Center"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Right_Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Right_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Top_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top_Middle"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Top_Middle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Top_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Middle_Middle"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Middle_Middle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Bottom_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom_Middle"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Bottom_Middle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Bottom_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Width"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Width);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Height"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Height);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftExt_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.LeftExt_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftExt_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.LeftExt_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightExt_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.RightExt_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightExt_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.RightExt_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopExt_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.TopExt_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopExt_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.TopExt_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomExt_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.BottomExt_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomExt_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.BottomExt_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Size"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Size);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for FairyGUI.RelationType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for FairyGUI.RelationType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class FairyGUIEaseTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(FairyGUI.EaseType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(FairyGUI.EaseType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(FairyGUI.EaseType), L, null, 33, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", FairyGUI.EaseType.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SineIn", FairyGUI.EaseType.SineIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SineOut", FairyGUI.EaseType.SineOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SineInOut", FairyGUI.EaseType.SineInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuadIn", FairyGUI.EaseType.QuadIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuadOut", FairyGUI.EaseType.QuadOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuadInOut", FairyGUI.EaseType.QuadInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CubicIn", FairyGUI.EaseType.CubicIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CubicOut", FairyGUI.EaseType.CubicOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CubicInOut", FairyGUI.EaseType.CubicInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuartIn", FairyGUI.EaseType.QuartIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuartOut", FairyGUI.EaseType.QuartOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuartInOut", FairyGUI.EaseType.QuartInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuintIn", FairyGUI.EaseType.QuintIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuintOut", FairyGUI.EaseType.QuintOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuintInOut", FairyGUI.EaseType.QuintInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExpoIn", FairyGUI.EaseType.ExpoIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExpoOut", FairyGUI.EaseType.ExpoOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExpoInOut", FairyGUI.EaseType.ExpoInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CircIn", FairyGUI.EaseType.CircIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CircOut", FairyGUI.EaseType.CircOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CircInOut", FairyGUI.EaseType.CircInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ElasticIn", FairyGUI.EaseType.ElasticIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ElasticOut", FairyGUI.EaseType.ElasticOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ElasticInOut", FairyGUI.EaseType.ElasticInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackIn", FairyGUI.EaseType.BackIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackOut", FairyGUI.EaseType.BackOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackInOut", FairyGUI.EaseType.BackInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BounceIn", FairyGUI.EaseType.BounceIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BounceOut", FairyGUI.EaseType.BounceOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BounceInOut", FairyGUI.EaseType.BounceInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", FairyGUI.EaseType.Custom);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(FairyGUI.EaseType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushFairyGUIEaseType(L, (FairyGUI.EaseType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SineIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.SineIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SineOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.SineOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SineInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.SineInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuadIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuadIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuadOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuadOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuadInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuadInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CubicIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CubicIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CubicOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CubicOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CubicInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CubicInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuartIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuartIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuartOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuartOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuartInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuartInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuintIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuintIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuintOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuintOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuintInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuintInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExpoIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ExpoIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExpoOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ExpoOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExpoInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ExpoInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CircIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CircIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CircOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CircOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CircInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CircInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ElasticIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ElasticIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ElasticOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ElasticOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ElasticInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ElasticInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BackIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BackOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BackInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BounceIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BounceIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BounceOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BounceOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BounceInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BounceInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for FairyGUI.EaseType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for FairyGUI.EaseType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class XLuaTestMyEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(XLuaTest.MyEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(XLuaTest.MyEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(XLuaTest.MyEnum), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", XLuaTest.MyEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", XLuaTest.MyEnum.E2);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(XLuaTest.MyEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushXLuaTestMyEnum(L, (XLuaTest.MyEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushXLuaTestMyEnum(L, XLuaTest.MyEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushXLuaTestMyEnum(L, XLuaTest.MyEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for XLuaTest.MyEnum!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for XLuaTest.MyEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}