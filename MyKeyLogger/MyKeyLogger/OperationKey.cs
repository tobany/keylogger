/*
 * Created by SharpDevelop.
 * User: ADMIN
 * Date: 26/12/2020
 * Time: 02:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyKeyLogger
{
	/// <summary>
	/// Description of OperationKey.
	/// </summary>
	public partial class OperationKey : Form
	{
		public OperationKey()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-keybd_event
		[DllImport("user32.dll")]  
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);  
        
        public static void TurnOFFCapsLockKey()
        {  
            if (Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))  
            {  
            	// https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            	const byte VK_CAPSLOCKKEY = 0x14;
                const int KEYEVENTF_EXTENDEDKEY = 0x1;  
                const int KEYEVENTF_KEYUP = 0x2;  
 				
                // Simulate a key press
 				keybd_event(VK_CAPSLOCKKEY, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
				// Simulate a key release               
 				keybd_event(VK_CAPSLOCKKEY, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }  
            
        }
	}
}
