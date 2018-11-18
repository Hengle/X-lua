using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using XLua;

namespace FairyGUI
{
    public sealed class LuaUIHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="luaClass"></param>
        public static void SetExtension(string url, System.Type baseType, LuaFunction extendFunction)
        {
            UIObjectFactory.SetPackageItemExtension(url, () =>
            {
                GComponent gcom = (GComponent)Activator.CreateInstance(baseType);
                gcom.data = extendFunction;
                return gcom;
            });
        }

        [BlackListAttribute]
        public static LuaTable ConnectLua(GComponent gcom)
        {
            LuaTable table = null;
            LuaFunction extendFunction = gcom.data as LuaFunction;
            if (extendFunction != null)
            {
                gcom.data = null;
                table = extendFunction.Func<GComponent, LuaTable>(gcom);
            }

            return table;
        }
    }

    public class GLuaComponent : GComponent
    {
        LuaTable _table;

        [BlackListAttribute]
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            _table = LuaUIHelper.ConnectLua(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
        }
    }

    public class GLuaLabel : GLabel
    {
        LuaTable _table;

        [BlackListAttribute]
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            _table = LuaUIHelper.ConnectLua(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
        }
    }

    public class GLuaButton : GButton
    {
        LuaTable _table;

        [BlackListAttribute]
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            _table = LuaUIHelper.ConnectLua(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
        }
    }

    public class GLuaProgressBar : GProgressBar
    {
        LuaTable _table;

        [BlackListAttribute]
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            _table = LuaUIHelper.ConnectLua(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
        }
    }

    public class GLuaSlider : GSlider
    {
        LuaTable _table;

        [BlackListAttribute]
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            _table = LuaUIHelper.ConnectLua(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
        }
    }

    public class GLuaComboBox : GComboBox
    {
        LuaTable _table;

        [BlackListAttribute]
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            _table = LuaUIHelper.ConnectLua(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
        }
    }

    /// <summary>
    /// FairyGUI Window继承
    /// </summary>
	public class LuaWindow : Window
    {
        LuaTable _table;
        LuaFunction _OnInit;
        LuaFunction _DoHideAnimation;
        LuaFunction _DoShowAnimation;
        LuaFunction _OnShown;
        LuaFunction _OnHide;

        public void ConnectLua(LuaTable table)
        {
            _table = table;
            _OnInit = table.Get<LuaFunction>("OnInit");
            _DoHideAnimation = table.Get<LuaFunction>("DoHideAnimation");
            _DoShowAnimation = table.Get<LuaFunction>("DoShowAnimation");
            _OnShown = table.Get<LuaFunction>("OnShown");
            _OnHide = table.Get<LuaFunction>("OnHide");
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_table != null)
                _table.Dispose();
            if (_OnInit != null)
                _OnInit.Dispose();
            if (_DoHideAnimation != null)
                _DoHideAnimation.Dispose();
            if (_DoShowAnimation != null)
                _DoShowAnimation.Dispose();
            if (_OnShown != null)
                _OnShown.Dispose();
            if (_OnHide != null)
                _OnHide.Dispose();
        }

        protected override void OnInit()
        {
            if (_OnInit != null)
                _OnInit.Action(this);
        }

        protected override void DoHideAnimation()
        {
            if (_DoHideAnimation != null)
                _DoHideAnimation.Action(this);
            else
                base.DoHideAnimation();
        }

        protected override void DoShowAnimation()
        {
            if (_DoShowAnimation != null)
                _DoShowAnimation.Action(this);
            else
                base.DoShowAnimation();
        }

        protected override void OnShown()
        {
            base.OnShown();

            if (_OnShown != null)
                _OnShown.Action(this);
        }

        protected override void OnHide()
        {
            base.OnHide();

            if (_OnHide != null)
                _OnHide.Action(this);
        }
    }
}