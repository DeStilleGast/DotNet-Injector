using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNet_Injector
{
    class ControlBoxMenu
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool AppendMenu(IntPtr hMenu, MenuFlags uFlags, uint uIDNewItem, string lpNewItem);

        [Flags]
        public enum MenuFlags : uint
        {
            MF_STRING = 0,
            MF_BYPOSITION = 0x400,
            MF_SEPARATOR = 0x800,
            MF_REMOVE = 0x1000,
            WM_SYSCOMMAND = 0x112
        }

        [DllImport("user32.dll")]
        private static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, MenuFlags uFlags);

        [DllImport("user32.dll")]
        private static extern int GetMenuItemCount(IntPtr hMenu);

        private IntPtr menu;
        

        public ControlBoxMenu(IntPtr hwnd)
        {
            menu = GetSystemMenu(hwnd, false);
        }


        public Boolean DeleteMenuItemByPosition(uint position)
        {
            return DeleteMenu(menu, position, MenuFlags.MF_BYPOSITION);
        }

        public Boolean AddMenuItem(MenuFlags type, uint index, String content)
        {
            return AppendMenu(menu, type, index, content);
        }

        public void ClearMenu()
        {
            int count = GetMenuItemCount(menu);

            for (int i = 0; i < count; i++)
            {
                DeleteMenuItemByPosition((uint)i);
            }
        }

    }
}
