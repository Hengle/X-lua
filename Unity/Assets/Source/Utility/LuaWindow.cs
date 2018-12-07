using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Game
{
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
            _DoHideAnimation = table.Get<LuaFunction>("DoShowTween");
            _DoShowAnimation = table.Get<LuaFunction>("DoHideTween");
            _OnShown = table.Get<LuaFunction>("OnShow");
            _OnHide = table.Get<LuaFunction>("OnHide");
        }
        /// <summary>
        /// 仅在显示动画完成时调用
        /// </summary>
        public void ShowImmediately()
        {
            OnShown();
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
                _OnInit.Action(_table);
        }

        protected override void DoHideAnimation()
        {
            if (_DoHideAnimation != null)
                _DoHideAnimation.Action(_table);
            else
                base.DoHideAnimation();
        }

        protected override void DoShowAnimation()
        {
            if (_DoShowAnimation != null)
                _DoShowAnimation.Action(_table);
            else
                base.DoShowAnimation();
        }

        protected override void OnShown()
        {
            base.OnShown();

            if (_OnShown != null)
                _OnShown.Action(_table);
        }

        protected override void OnHide()
        {
            base.OnHide();

            if (_OnHide != null)
                _OnHide.Action(_table);
        }
    }
}