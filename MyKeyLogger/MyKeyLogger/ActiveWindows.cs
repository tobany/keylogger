using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text; 

namespace MyKeyLogger
{
	public partial class ActiveWindows : Form
	{
		public ActiveWindows()
		{
			InitializeComponent();
		}
		
		// Utilise DllImport pour importer la fonction Win32 GetForegroundWindow
	    [DllImport("user32.dll")]
	    private static extern IntPtr GetForegroundWindow();
	    
	    // Utilise DllImport pour importer la fonction Win32 GetWindowText
	    [DllImport("user32.dll")]
	    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
	    
	    // retourne le nom de la fenêtre windows active
	    public static string ActiveWindowName() {
	           	
	    	// Fixe la capacité du StringBulder à 256 caractères
	    	int chars = 256;
	    	
	    	// Crée un StringBuilder qui s'attend à contenir 256 caractères maximum
	    	StringBuilder buffer = new StringBuilder(chars);
	
	        // La fonction GetForegroundWindow renvoie un handle de la fenêtre avec laquelle l'utilisateur travaille actuellement
	        IntPtr handle = GetForegroundWindow();

	        // La fonction GetWindowText copie le texte de la barre de titre de la fenêtre spécifiée, si elle en a une, dans un tampon (buffer)
	        return GetWindowText(handle, buffer, chars) > 0 ? buffer.ToString() : "No active window";
		
	    }
	}
}
