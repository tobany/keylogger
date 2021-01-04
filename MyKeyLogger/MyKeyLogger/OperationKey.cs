using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace MyKeyLogger
{
	public partial class OperationKey : Form
	{
		public OperationKey()
		{
			InitializeComponent();
		}
	
		[DllImport("user32.dll")]  
		// Cette fonction permet de déclencher un événement d'appui de touche clavier.
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);  
               
        public static void TurnOFFCapsLockKey()
        {  
        	// Contrôle si la touche clavier CAPITAL est activée
			if (Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
            {  
				// https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            	const byte VK_CAPITAL = 0x14; //Code hexadecimal de la touche clavier [Ver.Maj]
                
            	const int KEYEVENTF_EXTENDEDKEY = 0x1;
                const int KEYEVENTF_KEYUP = 0x2;  
 				 
                /*
                * https://docs.microsoft.com/fr-fr/windows/win32/inputdev/about-keyboard-input?redirectedfrom=MSDN#scan_code
				* Le paramètre bScan est la valeur que le clavier génère lorsque l'utilisateur appuie sur une touche.
 				* Il s'agit d'une valeur dépendante du périphérique qui identifie la touche enfoncée, par opposition au caractère représenté par la touche. 
				* Une application ignore généralement les codes de scan. 
				* Au lieu de cela, il utilise les codes de touche virtuelle indépendants du périphérique pour interpréter les messages de frappe.
				*/
                
                // Simule un appui sur la touche CAPITAL
                keybd_event(VK_CAPITAL, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
               	// Simule un relachement de la touche CAPITAL
                keybd_event(VK_CAPITAL, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
            }  
        }
	}
}
