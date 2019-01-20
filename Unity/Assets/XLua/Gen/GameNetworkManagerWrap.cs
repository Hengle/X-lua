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
    public class GameNetworkManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.NetworkManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 4, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasNetworkChannel", _m_HasNetworkChannel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNetworkChannel", _m_GetNetworkChannel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAllNetworkChannels", _m_GetAllNetworkChannels);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateNetworkChannel", _m_CreateNetworkChannel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyNetworkChannel", _m_DestroyNetworkChannel);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NetworkError", _e_NetworkError);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkChannelCount", _g_get_NetworkChannelCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnNetworkConnected", _g_get_OnNetworkConnected);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnNetworkClosed", _g_get_OnNetworkClosed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnNetworkMissHeartBeat", _g_get_OnNetworkMissHeartBeat);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnNetworkConnected", _s_set_OnNetworkConnected);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnNetworkClosed", _s_set_OnNetworkClosed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnNetworkMissHeartBeat", _s_set_OnNetworkMissHeartBeat);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Game.NetworkManager gen_ret = new Game.NetworkManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasNetworkChannel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.HasNetworkChannel( _name );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNetworkChannel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        Game.INetworkChannel gen_ret = gen_to_be_invoked.GetNetworkChannel( _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAllNetworkChannels(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        Game.INetworkChannel[] gen_ret = gen_to_be_invoked.GetAllNetworkChannels(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<Game.INetworkChannel>>(L, 2)) 
                {
                    System.Collections.Generic.List<Game.INetworkChannel> _results = (System.Collections.Generic.List<Game.INetworkChannel>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Game.INetworkChannel>));
                    
                    gen_to_be_invoked.GetAllNetworkChannels( _results );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkManager.GetAllNetworkChannels!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateNetworkChannel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        Game.INetworkChannel gen_ret = gen_to_be_invoked.CreateNetworkChannel( _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyNetworkChannel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.DestroyNetworkChannel( _name );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkChannelCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.NetworkChannelCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnNetworkConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnNetworkConnected);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnNetworkClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnNetworkClosed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnNetworkMissHeartBeat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnNetworkMissHeartBeat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnNetworkConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnNetworkConnected = translator.GetDelegate<System.Action<Game.NetworkChannel>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnNetworkClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnNetworkClosed = translator.GetDelegate<System.Action<Game.NetworkChannel>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnNetworkMissHeartBeat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnNetworkMissHeartBeat = translator.GetDelegate<System.Action<Game.NetworkChannel, int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_NetworkError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			Game.NetworkManager gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                System.Action<Game.NetworkChannel, Game.NetworkErrorCode, string> gen_delegate = translator.GetDelegate<System.Action<Game.NetworkChannel, Game.NetworkErrorCode, string>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<Game.NetworkChannel, Game.NetworkErrorCode, string>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.NetworkError += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.NetworkError -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkManager.NetworkError!");
            return 0;
        }
        
		
		
    }
}
