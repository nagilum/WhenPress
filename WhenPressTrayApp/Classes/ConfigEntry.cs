using System.Windows.Forms;

namespace WhenPressTrayApp {
	/// <summary>
	/// Single entry of the JSON config.
	/// </summary>
	public class ConfigEntry {
		/// <summary>
		/// The key-modifier to hotkey-bind to.
		/// </summary>
		public ModifierKeys Modifier { get; set; }

		/// <summary>
		/// The key to hotkey-bind to.
		/// </summary>
		public Keys Key { get; set; }

		/// <summary>
		/// A list of parameters to pass along to the script.
		/// </summary>
		// public string Parameters { get; set; }

		/// <summary>
		/// Path and file to the script to execute.
		/// </summary>
		public string ScriptFile { get; set; }

		/// <summary>
		/// Text to use as the menu-item caption for the tray-menu.
		/// </summary>
		public string TrayMenuText { get; set; }
	}
}
