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
    public class GameNetworkChannelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.NetworkChannel);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 22, 9);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Connect", _m_Connect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Send", _m_Send);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Name", _g_get_Name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Connected", _g_get_Connected);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkType", _g_get_NetworkType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LocalIPAddress", _g_get_LocalIPAddress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LocalPort", _g_get_LocalPort);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RemoteIPAddress", _g_get_RemoteIPAddress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RemotePort", _g_get_RemotePort);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SendPacketCount", _g_get_SendPacketCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SentPacketCount", _g_get_SentPacketCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReceivePacketCount", _g_get_ReceivePacketCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReceivedPacketCount", _g_get_ReceivedPacketCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ResetHeartBeatElapseSecondsWhenReceivePacket", _g_get_ResetHeartBeatElapseSecondsWhenReceivePacket);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MissHeartBeatCount", _g_get_MissHeartBeatCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "HeartBeatInterval", _g_get_HeartBeatInterval);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "HeartBeatElapseSeconds", _g_get_HeartBeatElapseSeconds);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReceiveBufferSize", _g_get_ReceiveBufferSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SendBufferSize", _g_get_SendBufferSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkChannelConnected", _g_get_NetworkChannelConnected);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkChannelClosed", _g_get_NetworkChannelClosed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkChannelMissHeartBeat", _g_get_NetworkChannelMissHeartBeat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkChannelError", _g_get_NetworkChannelError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetworkReceive", _g_get_NetworkReceive);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ResetHeartBeatElapseSecondsWhenReceivePacket", _s_set_ResetHeartBeatElapseSecondsWhenReceivePacket);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "HeartBeatInterval", _s_set_HeartBeatInterval);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ReceiveBufferSize", _s_set_ReceiveBufferSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SendBufferSize", _s_set_SendBufferSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NetworkChannelConnected", _s_set_NetworkChannelConnected);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NetworkChannelClosed", _s_set_NetworkChannelClosed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NetworkChannelMissHeartBeat", _s_set_NetworkChannelMissHeartBeat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NetworkChannelError", _s_set_NetworkChannelError);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NetworkReceive", _s_set_NetworkReceive);
            
			
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
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					string _name = LuaAPI.lua_tostring(L, 2);
					
					Game.NetworkChannel gen_ret = new Game.NetworkChannel(_name);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkChannel constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _elapseSeconds = (float)LuaAPI.lua_tonumber(L, 2);
                    float _realElapseSeconds = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.Update( _elapseSeconds, _realElapseSeconds );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Connect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _ip = LuaAPI.lua_tostring(L, 2);
                    int _port = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Connect( _ip, _port );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<object>(L, 6)) 
                {
                    string _ip = LuaAPI.lua_tostring(L, 2);
                    int _port = LuaAPI.xlua_tointeger(L, 3);
                    int _sendBuffer = LuaAPI.xlua_tointeger(L, 4);
                    int _receiveBuffer = LuaAPI.xlua_tointeger(L, 5);
                    object _userData = translator.GetObject(L, 6, typeof(object));
                    
                    gen_to_be_invoked.Connect( _ip, _port, _sendBuffer, _receiveBuffer, _userData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkChannel.Connect!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Send(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    int _type = LuaAPI.xlua_tointeger(L, 2);
                    byte[] _msg = LuaAPI.lua_tobytes(L, 3);
                    
                    gen_to_be_invoked.Send( _type, _msg );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    int _type = LuaAPI.xlua_tointeger(L, 2);
                    string _msg = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.Send( _type, _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkChannel.Send!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.Name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Connected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Connected);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetworkType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LocalIPAddress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LocalIPAddress);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LocalPort(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.LocalPort);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RemoteIPAddress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.RemoteIPAddress);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RemotePort(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.RemotePort);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SendPacketCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.SendPacketCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SentPacketCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.SentPacketCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReceivePacketCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ReceivePacketCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReceivedPacketCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ReceivedPacketCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResetHeartBeatElapseSecondsWhenReceivePacket(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ResetHeartBeatElapseSecondsWhenReceivePacket);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MissHeartBeatCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.MissHeartBeatCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_HeartBeatInterval(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.HeartBeatInterval);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_HeartBeatElapseSeconds(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.HeartBeatElapseSeconds);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReceiveBufferSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ReceiveBufferSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SendBufferSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.SendBufferSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkChannelConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetworkChannelConnected);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkChannelClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetworkChannelClosed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkChannelMissHeartBeat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetworkChannelMissHeartBeat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkChannelError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetworkChannelError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetworkReceive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetworkReceive);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ResetHeartBeatElapseSecondsWhenReceivePacket(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ResetHeartBeatElapseSecondsWhenReceivePacket = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_HeartBeatInterval(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.HeartBeatInterval = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ReceiveBufferSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ReceiveBufferSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SendBufferSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SendBufferSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NetworkChannelConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NetworkChannelConnected = translator.GetDelegate<System.Action<Game.NetworkChannel>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NetworkChannelClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NetworkChannelClosed = translator.GetDelegate<System.Action<Game.NetworkChannel>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NetworkChannelMissHeartBeat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NetworkChannelMissHeartBeat = translator.GetDelegate<System.Action<Game.NetworkChannel, int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NetworkChannelError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NetworkChannelError = translator.GetDelegate<System.Action<Game.NetworkChannel, Game.NetworkErrorCode, string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NetworkReceive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkChannel gen_to_be_invoked = (Game.NetworkChannel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NetworkReceive = translator.GetDelegate<System.Action<int, object>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
