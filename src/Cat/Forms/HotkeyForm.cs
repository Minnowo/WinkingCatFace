﻿using System.Diagnostics;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Native;

namespace WinkingCat
{
    public class HotkeyForm : Form
    {
        public delegate void HotkeyEventHandler(ushort id, Keys key, Modifiers modifier);

        public event HotkeyEventHandler HotkeyPress;

        public int hotKeyRepeatLimit { get; set; }
        private Stopwatch repeatLimitTimer;

        public HotkeyForm()
        {
            hotKeyRepeatLimit = 1000;
            repeatLimitTimer = Stopwatch.StartNew();
        }

        public void RegisterHotkey(Hotkey hotkeyInfo)
        {
            if (hotkeyInfo == null || hotkeyInfo.Status == HotkeyStatus.Registered)
                return;

            if (!hotkeyInfo.IsValidHotkey)
            {
                hotkeyInfo.Status = HotkeyStatus.NotSet;
                return;
            }

            if (hotkeyInfo.ID == 0)
            {
                string uniqueID = Helper.GetUniqueID();
                hotkeyInfo.ID = NativeMethods.GlobalAddAtom(uniqueID);

                if (hotkeyInfo.ID == 0)
                {
                    hotkeyInfo.Status = HotkeyStatus.Failed;
                    return;
                }
            }

            if (!NativeMethods.RegisterHotKey(Handle, hotkeyInfo.ID, (uint)hotkeyInfo.ModifiersEnum, (uint)hotkeyInfo.KeyCode))
            {
                NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);
                hotkeyInfo.ID = 0;
                hotkeyInfo.Status = HotkeyStatus.Failed;
                return;
            }

            hotkeyInfo.Status = HotkeyStatus.Registered;
        }

        public bool UnRegisterHotkey(Hotkey hotkeyInfo)
        {
            if (hotkeyInfo == null)
                return false;

            if (hotkeyInfo.ID > 0)
            {
                bool result = NativeMethods.UnregisterHotKey(Handle, hotkeyInfo.ID);

                if (result)
                {
                    NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);
                    hotkeyInfo.ID = 0;
                    hotkeyInfo.Status = HotkeyStatus.NotSet;
                    return true;
                }
            }

            hotkeyInfo.Status = HotkeyStatus.Failed;

            return false;
        }

        public void KeyPressed(ushort id, Keys key, Modifiers modifier)
        {
            if (repeatLimitTimer.ElapsedMilliseconds > hotKeyRepeatLimit)
            {
                repeatLimitTimer.Restart();
                HotkeyPress(id, key, modifier);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowsMessages.HOTKEY)
            {
                ushort id = (ushort)m.WParam;
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                Modifiers modifier = (Modifiers)((int)m.LParam & 0xFFFF);
                KeyPressed(id, key, modifier);
                return;
            }

            base.WndProc(ref m);
        }
    }
}