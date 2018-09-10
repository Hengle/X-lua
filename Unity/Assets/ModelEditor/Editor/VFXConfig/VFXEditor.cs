namespace CS.VFXConfig
{
    using System;
    using XmlCode.Skill;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Playables;
    using System.ComponentModel;

    [Serializable]
    public class VFXEditor
    {
        public static VFXEditor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VFXEditor();
                return _instance;
            }
        }
        static VFXEditor _instance;

        GeneralAction _action;
        public void OpenTimeline(GeneralAction action)
        {
            _action = action;

        }

        string GetDisplayName(PlayableAsset asset)
        {
            Type type = asset.GetType();
            object[] customAttributes = type.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            string displayName = type.FullName;
            if (customAttributes.Length > 0)
                displayName = (customAttributes[0] as DisplayNameAttribute).DisplayName;
            return displayName;
        }
    }
}