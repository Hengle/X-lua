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
    public class GameConstSettingWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.ConstSetting);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 11, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FrameRate", Game.ConstSetting.FrameRate);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Resolution", Game.ConstSetting.Resolution);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Blend", Game.ConstSetting.Blend);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SleepTime", Game.ConstSetting.SleepTime);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResVersionFile", Game.ConstSetting.ResVersionFile);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResMD5File", Game.ConstSetting.ResMD5File);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HasDownloadFile", Game.ConstSetting.HasDownloadFile);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UrlConfig", Game.ConstSetting.UrlConfig);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LuaDir", Game.ConstSetting.LuaDir);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LuaMain", Game.ConstSetting.LuaMain);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Game.ConstSetting gen_ret = new Game.ConstSetting();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.ConstSetting constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        
        
		
		
		
		
    }
}
