#if USE_UNI_LUA
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
    public class GamePlatformInterfaceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.Platform.Interface);
			Utils.BeginObjectRegister(type, L, translator, 0, 44, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Release", _m_Release);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAvailableMemory", _m_GetAvailableMemory);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Install", _m_Install);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSDKPlatform", _m_GetSDKPlatform);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSDKPlatformName", _m_GetSDKPlatformName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNetWorkMode", _m_GetNetWorkMode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitSDK", _m_InitSDK);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EscapeSDK", _m_EscapeSDK);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsSDKFinished", _m_IsSDKFinished);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Login", _m_Login);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Logout", _m_Logout);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SubmitUserInfo", _m_SubmitUserInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pay", _m_Pay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLoginStatus", _m_GetLoginStatus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUserId", _m_GetUserId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUserName", _m_GetUserName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetToken", _m_GetToken);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetToken", _m_SetToken);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUserInfo", _m_SetUserInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckGuestAccount", _m_CheckGuestAccount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeAccount", _m_ChangeAccount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartAccountHome", _m_StartAccountHome);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartForum", _m_StartForum);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowToolBar", _m_ShowToolBar);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideToolBar", _m_HideToolBar);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsCydia", _m_IsCydia);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsPad", _m_IsPad);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsHdVersion", _m_IsHdVersion);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartWebView", _m_StartWebView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenUrl", _m_OpenUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendWeibo", _m_SendWeibo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendWeChat", _m_SendWeChat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMemInfo", _m_GetMemInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUDID", _m_GetUDID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Voice_GetFileLength", _m_Voice_GetFileLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Voice_GetVolume", _m_Voice_GetVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Voice_Start", _m_Voice_Start);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Voice_Stop", _m_Voice_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Voice_GetAsrText", _m_Voice_GetAsrText);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitLogSystem", _m_InitLogSystem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MonitorBatteryState", _m_MonitorBatteryState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenAlbum", _m_OpenAlbum);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartScanCode", _m_StartScanCode);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 1, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Create", _m_Create_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Game.Platform.Interface does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Create_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.Platform.Interface.Create(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Release(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAvailableMemory(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.GetAvailableMemory(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Install(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _file = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.Install( _file );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSDKPlatform(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.GetSDKPlatform(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSDKPlatformName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetSDKPlatformName(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNetWorkMode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        Game.Platform.NetworkMode gen_ret = gen_to_be_invoked.GetNetWorkMode(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitSDK(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.InitSDK(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EscapeSDK(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.EscapeSDK(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsSDKFinished(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsSDKFinished(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Login(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Login(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Logout(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Logout(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SubmitUserInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _type = LuaAPI.lua_tostring(L, 2);
                    string _roleid = LuaAPI.lua_tostring(L, 3);
                    string _rolename = LuaAPI.lua_tostring(L, 4);
                    string _level = LuaAPI.lua_tostring(L, 5);
                    int _zoneid = LuaAPI.xlua_tointeger(L, 6);
                    string _zonename = LuaAPI.lua_tostring(L, 7);
                    string _createtime = LuaAPI.lua_tostring(L, 8);
                    
                    gen_to_be_invoked.SubmitUserInfo( _type, _roleid, _rolename, _level, _zoneid, _zonename, _createtime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _roleid = LuaAPI.lua_tostring(L, 2);
                    string _rolename = LuaAPI.lua_tostring(L, 3);
                    string _level = LuaAPI.lua_tostring(L, 4);
                    string _vip = LuaAPI.lua_tostring(L, 5);
                    string _orderId = LuaAPI.lua_tostring(L, 6);
                    int _serverid = LuaAPI.xlua_tointeger(L, 7);
                    string _servername = LuaAPI.lua_tostring(L, 8);
                    int _cash = LuaAPI.xlua_tointeger(L, 9);
                    string _pid = LuaAPI.lua_tostring(L, 10);
                    string _desc = LuaAPI.lua_tostring(L, 11);
                    
                    gen_to_be_invoked.Pay( _roleid, _rolename, _level, _vip, _orderId, _serverid, _servername, _cash, _pid, _desc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLoginStatus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.GetLoginStatus(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUserId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        long gen_ret = gen_to_be_invoked.GetUserId(  );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUserName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetUserName(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetToken(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetToken(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetToken(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _token = LuaAPI.lua_tostring(L, 2);
                    int _deadline = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetToken( _token, _deadline );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUserInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _userId = LuaAPI.lua_tostring(L, 2);
                    string _userName = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SetUserInfo( _userId, _userName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckGuestAccount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CheckGuestAccount(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeAccount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ChangeAccount(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartAccountHome(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartAccountHome(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartForum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartForum(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowToolBar(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ShowToolBar(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideToolBar(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.HideToolBar(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsCydia(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsCydia(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPad(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsPad(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsHdVersion(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsHdVersion(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartWebView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _strURL = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.StartWebView( _strURL );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.OpenUrl( _url );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendWeibo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SendWeibo( _content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendWeChat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SendWeChat( _content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMemInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        long gen_ret = gen_to_be_invoked.GetMemInfo(  );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUDID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetUDID(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Voice_GetFileLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.Voice_GetFileLength(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Voice_GetVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.Voice_GetVolume(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Voice_Start(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _record = LuaAPI.lua_toboolean(L, 2);
                    string _filename = LuaAPI.lua_tostring(L, 3);
                    bool _recognize = LuaAPI.lua_toboolean(L, 4);
                    
                        bool gen_ret = gen_to_be_invoked.Voice_Start( _record, _filename, _recognize );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Voice_Stop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Voice_Stop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Voice_GetAsrText(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.Voice_GetAsrText(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitLogSystem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _filename = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.InitLogSystem( _filename );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MonitorBatteryState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.MonitorBatteryState(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenAlbum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _fileNameBig = LuaAPI.lua_tostring(L, 2);
                    string _fileNameSmall = LuaAPI.lua_tostring(L, 3);
                    int _scaleFactorBig = LuaAPI.xlua_tointeger(L, 4);
                    int _scaleFactorSmall = LuaAPI.xlua_tointeger(L, 5);
                    string _resultCode = LuaAPI.lua_tostring(L, 6);
                    
                    gen_to_be_invoked.OpenAlbum( _fileNameBig, _fileNameSmall, _scaleFactorBig, _scaleFactorSmall, _resultCode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartScanCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Platform.Interface gen_to_be_invoked = (Game.Platform.Interface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartScanCode(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.Platform.Interface.Instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
